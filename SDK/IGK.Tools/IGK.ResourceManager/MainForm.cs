/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using System.Collections ;
using System.Xml;

namespace IGK.ResMan
{
    public partial class MainForm : Form
    {
        string filename;

        public MainForm()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string[] t =Environment.GetCommandLineArgs();
            if ((t != null) && (t.Length == 2))
            {
                OpenRessource(t[1]);
            }
            this.c_lsv_resexplorer.ListViewItemSorter = new MyListViewSorter();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create new Ressources
            this.c_lsv_resexplorer.Items.Clear();
            this.filename = string.Empty;
            this.SetTitle();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog()) 
            {
                ofd.Filter = "Resources Files| *.resources; *.res; *.resx; *.xml|android resources|*.xml|*.*|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    OpenRessource(ofd.FileName);
                }
            }
        }

        private void SetTitle()
        {
            this.Text = "GK Single Resources Manager";
        }

        private void OpenRessource(string p)
        {
            if (!File.Exists(p))
                return;

            SetTitle();
            Text += "- [ " + Path.GetFileName(p) + " ]";
            this.filename = Path.GetFullPath(p);


            try
            {
                this.c_lsv_resexplorer.Items.Clear();
                this.LoadResourceFile(p);
               
            }
            catch
            {
                MessageBox.Show("Unabled to Load Resources", "Error", 
                    MessageBoxButtons .OK, MessageBoxIcon.Error );
            }
        }

        private void LoadResourceFile(string p)
        {

            ListViewItem item = null;
            switch (Path.GetExtension(p).ToLower())
            {
                case ".xml":
                    //load android xml extension

                    this.SuspendLayout();
                    XmlReader v_reader = XmlReader.Create(p);
                    if (v_reader != null)
                    {
                        while (v_reader.Read())
                        {
                            switch (v_reader.NodeType)

                            {
                                
                                case XmlNodeType.Element:
                                    if (v_reader.Name == "string")
                                    {
                                        string n = v_reader.GetAttribute("name");
                                        string v = v_reader.ReadElementContentAsString().Trim().Replace ("\\'", "'");
                                        item = new ListViewItem(new string[]{
                    n,
                   typeof(string).ToString(),
                    v
                    });

                                        item.Name = item.Text;
                                        item.Tag = v;
                                        this.c_lsv_resexplorer.Items.Add(item);
                                    }
                                    break;
                            }
                        }
                    }

                    this.ResumeLayout();
                    break;
                default:

                    ResourceReader r = new ResourceReader(p);
                    System.Collections.IDictionaryEnumerator e = r.GetEnumerator();


                    this.SuspendLayout();
                    while (e.MoveNext())
                    {
                        if (e.Value == null)
                        {
                            continue;
                        }
                        if (this.c_lsv_resexplorer.Items.ContainsKey(e.Key.ToString()))
                            continue;
                        item = new ListViewItem(new string[]{
                    e.Entry .Key .ToString(),
                    e.Value.GetType ().ToString (),
                    e.Value.ToString()
                    });

                        item.Name = item.Text;
                        item.Tag = e.Value;
                        this.c_lsv_resexplorer.Items.Add(item);

                    }
                    this.c_lsv_resexplorer.Sorting = SortOrder.Ascending;
                    this.c_lsv_resexplorer.Sort();

                    this.ResumeLayout();
                    r.Close();
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ////Create sample ressources
            //ResourceWriter rw = new ResourceWriter("p.resources");

            //rw.AddResource("MainForm_Title", "GK.Paint.NET");
            //rw.AddResource("MainForm_SplashScreen", new Bitmap("d:\\images\\naruto\\naruto 01.jpg"));
            //rw.Generate();
            //rw.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.c_lsv_resexplorer.SelectedItems.Count <= 0)
            {
                this.pictureBox1.Image = null;
                return;
            }

            ListViewItem item = this.c_lsv_resexplorer.SelectedItems[0];

            if (item.Tag is System.Drawing.Image  )
            {
                this.pictureBox1.Image = item.Tag as System.Drawing.Image;
            }
        }

        private void bt_addtext_Click(object sender, EventArgs e)
        {
            AddText();
        }

        void bt_add_Click(object sender, EventArgs e)
        {
            AddText c = (sender as Control).Parent as AddText;
            if (c != null)
            {
                if (string.IsNullOrEmpty(c.ResTitle))
                {
                    MessageBox.Show("impossible d'ajouter un élément vide");
                    return;
                }
                if (this.c_lsv_resexplorer.Items.ContainsKey(c.ResTitle))
                {
                    MessageBox.Show("Un élément possédant la clef existe déjà dans la collection");
                    return;
                }

                string vt = TreatText(c.ResTitle);


                ListViewItem item = new ListViewItem();
                item.Text = vt;
                item.Name = vt;
                item.Tag = c.ResValue;
                item.SubItems.Add(typeof(string).ToString());
                item.SubItems.Add(c.ResValue);
                this.c_lsv_resexplorer.Items.Add(item);
                c.SetClearFocus();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save dialog
            if (string.IsNullOrEmpty(this.filename))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Resources Files|*.resources";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        this.filename = sfd.FileName;
                    }
                }
            }
            SaveResources();
        }

        private void SaveResources()
        {
            string ext = Path.GetExtension(this.filename);
            switch (ext.ToLower ())
            {
                case ".xml":
                      Dictionary<string, string> m_v = new Dictionary<string, string>();
                    foreach (ListViewItem item in this.c_lsv_resexplorer.Items)
                    {
                        if (item.SubItems[1].Text == typeof (string).ToString ())
                        {
                            m_v.Add(item.Name, item.SubItems[2].Text);
                        }
                    }
                    AndroidUtils.ExportToFile(filename , m_v );
            
                    break;
                default:
             
            ResourceWriter rw = new ResourceWriter(this.filename+".bck");

            foreach (ListViewItem item in this.c_lsv_resexplorer.Items)
            {
                rw.AddResource(item.Text, item.Tag); 
            }
            rw.Generate();
            rw.Close();
            if (File.Exists(filename))
                File.Delete(filename);
            File.Copy(this.filename + ".bck", filename);
            File.Delete(this.filename + ".bck");
            break;
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.c_lsv_resexplorer.SelectedItems.Count != 1)
            {

                return;
            }

            ListViewItem item = this.c_lsv_resexplorer.SelectedItems[0];

            if (item.Tag is System.String)
            {
                using (Form frm = new Form())
                {
                    frm.Text = "Edit Text";
                    frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    frm.MaximizeBox = false;
                    frm.MinimizeBox = false;
                    AddText c = new AddText();
                    c.bt_add.Visible = true;
                    c.bt_add.Text = "Modifier";
                    c.bt_add.Click += (oo,ee)=>{
                        frm.DialogResult = System.Windows.Forms.DialogResult.OK;

                    };
                    c.ResTitle = item.Text;
                    c.ResValue = item.Tag.ToString();
                    
                    frm.ClientSize = c.Size;
                    frm.AcceptButton = null;// c.bt_close;
                    frm.CancelButton = c.bt_close;
                    frm.ShowInTaskbar = false;
                    frm.Owner = this;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    c.Dock = DockStyle.Fill;
                    frm.Controls.Add(c);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {

                        if (item.Text != c.ResTitle)
                        {
                                item.Text = c.ResTitle;
                                item.Name = c.ResTitle;
                         
                        }
                        item.Tag = c.ResValue;
                        item.SubItems[2].Text = c.ResValue;
                    }
                }
            }
            else if (item.Tag is System.Drawing.Image)
            {
                
                
                this.pictureBox1.Image = item.Tag as System.Drawing.Image;
            }
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddText();
        }

        private void AddText()
        {

            using (Form frm = new Form())
            {
                frm.Text = "Add Text";
                frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;
                AddText c = new AddText();
                c.bt_add.Click += new EventHandler(bt_add_Click);
                frm.ClientSize = c.Size;
                //frm.AcceptButton = c.bt_add;
                frm.CancelButton = c.bt_close;
                frm.ShowInTaskbar = false;
                frm.Owner = this;
                frm.StartPosition = FormStartPosition.CenterParent;
                c.Dock = DockStyle.Fill;
                frm.Controls.Add(c);               
                frm.ShowDialog();
            }
            
            
        }

        private string TreatText(string p)
        {
            //p = p.Replace('.', '_');
            p = p.Replace(" ", "");
            return p;

        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffect & DragDropEffects.Copy )== DragDropEffects.Copy)
            {
                e.Effect = DragDropEffects.Copy;                
            }
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                string[] strTab = e.Data.GetFormats ();
                string[] strFiles = e.Data. GetData ("FileDrop") as string[];
                string str = string.Empty ;
                object obj =null ;
                string txt = "";
                if ((strFiles != null) && (strFiles.Length > 0))
                {
                    ListViewItem item = null;
                    for (int i = 0; i < strFiles.Length; i++)
                    {
                        str = strFiles[i];
                        item = new ListViewItem();

                        item.Text = Path.GetFileName(TreatText (Path .GetFileName (str)));
                        item.Name = item.Text ;

                        switch (Path.GetExtension (str).ToLower ())
                        {
                            case ".gkds":
                                {
                                    Stream m = File.OpenRead(str);
                                    Byte[] t = new byte[m.Length];
                                    m.Read(t, 0, t.Length);
                                    m.Close();
                                    obj = t;
                                }
                                break;
                            case ".bmp":
                            case ".jpg":
                            case ".png":
                            case ".tiff":
                                try
                                {
                                    Bitmap temp = new Bitmap(str);
                                    obj = new Bitmap(temp);
                                    temp.Dispose();
                                    txt = obj.ToString();
                                }
                                catch {
                                    continue;
                                }
                                break;
                            case ".ico":
                                try
                                {
                                    Icon temp = new Icon(str);
                                    obj = temp.Clone () as Icon ;
                                    temp.Dispose();
                                    txt = obj.ToString();
                                }
                                catch {
                                    continue;
                                }
                                break;
                            case ".cur":
                                
                                FileStream fs = File.OpenRead(str);
                                byte[] tab = new byte [fs.Length ];
                                fs.Read (tab, 0,tab.Length );
                                fs.Close ();
                                obj = tab;
                                
                                //BinaryReader binR = new BinaryReader(fs);
                                //BinaryWriter binW = new BinaryWriter(mem);
                                //binW.Write((short)binR.ReadInt16 ());
                                //int hh  = binR .ReadInt16 ();
                                //binW.Write((short)1);
                                //binW .Write (binR.ReadInt16 ());
                                //Byte[] tab = new  Byte[4096];
                                //int count = 0;
                                //while ((count = binR.Read(tab, 0, tab.Length ))>0)
                                //{
                                //    binW.Write (tab,0, count);
                                //}
                                //binW.Flush();
                                //binR.Close();
                                //mem.Seek(0, SeekOrigin.Begin);

                                //Icon ico = new Icon(mem);
                                //mem.Close();
                                //fs.Close();

                                //Bitmap bm = ico.ToBitmap();
                                //Cursor ct = new Cursor ( IconInfo.CreateCursor(bm, 1, 1);
                                ////bm.Dispose();
                                //ico.Dispose();
                                //obj = ct;
                                //txt = ct.GetType().ToString();
                                
                                break;
                            default :
                                continue;                               
                        }

                        

                        item.SubItems.Add (obj.GetType ().ToString());
                        item.SubItems.Add(txt);
                        item.Tag = obj ;

                            this.c_lsv_resexplorer.Items.Add (item );
                    }
                }
            }

        }

        private void listView1_DragLeave(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GK Simple Ressources Manager \n" +
                "Bondje Doue charles");
        }

        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //action
            Keys k = (Keys)e.KeyChar;
            switch (k)
            {
                case Keys .Delete :
                    deleteElement(e);                    
                    break ;
                case Keys.F2 :
                    listView1_DoubleClick(this.c_lsv_resexplorer, EventArgs.Empty);
                    break;
            }
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            Keys k = (Keys)e.KeyData; 
            switch (k)
            {
                case Keys.Delete:
                    deleteElement(null);
                    break;
                case Keys.F2:
                    listView1_DoubleClick(this.c_lsv_resexplorer, EventArgs.Empty);
                    break;
            }
        }

        private void deleteElement(KeyPressEventArgs e)
        {
            if (this.c_lsv_resexplorer.SelectedItems.Count > 0)
            {
                ListView.SelectedListViewItemCollection item = this.c_lsv_resexplorer.SelectedItems;
                foreach (ListViewItem i in item)
                {
                    this.c_lsv_resexplorer.Items.Remove(i);
                }
                item.Clear();
                if (e!=null)
                e.Handled = true;
            }
        }

        private void lsv_resexplorer_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem item = c_lsv_resexplorer.Items[e.Item];

            if (string.IsNullOrEmpty(e.Label))
                return;
            if (!this.c_lsv_resexplorer.Items.ContainsKey(e.Label))
            {
                item .Text = e.Label ;
                item.Name = e.Label ;
            }
            else
            {
                e.CancelEdit = true;
                MessageBox.Show("un élément avec la même clef existe dans la collection", "Erreur");
                return;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Resources files | *.resources";
                if (sfd .ShowDialog () == DialogResult.OK )
                {
                    this.filename = sfd.FileName;
                    SaveResources();
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //remove selected items
            if (this.c_lsv_resexplorer.SelectedItems.Count == 0)
                return;

            if (MessageBox.Show("Are You Shure?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                RemoveSelectedItems();
            }
        }

        private void RemoveSelectedItems()
        {
            ListViewItem[] v_itemtb = new ListViewItem[this.c_lsv_resexplorer.SelectedItems.Count];
            this.c_lsv_resexplorer.SelectedItems.CopyTo(v_itemtb, 0);
            foreach (ListViewItem i in v_itemtb )
            {
                this.c_lsv_resexplorer.Items.Remove(i);
            }
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //remove all data
            ListViewItem[] v_itemtb = new ListViewItem[this.c_lsv_resexplorer.Items.Count];
            this.c_lsv_resexplorer.Items.CopyTo(v_itemtb, 0);
            foreach (ListViewItem i in v_itemtb)
            {
                if (i.Tag is byte[])
                {
                    this.c_lsv_resexplorer.Items.Remove(i);
                }
            }
        }

        private void stringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //remove all string
            ListViewItem[] v_itemtb = new ListViewItem[this.c_lsv_resexplorer.Items.Count];
            this.c_lsv_resexplorer.Items.CopyTo(v_itemtb, 0);
            foreach (ListViewItem i in v_itemtb)
            {
                if (i.Tag is string)
                {
                    this.c_lsv_resexplorer.Items.Remove(i);
                }
            }
        }

        private void pictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //remove all data
            ListViewItem[] v_itemtb = new ListViewItem[this.c_lsv_resexplorer.Items.Count];
            this.c_lsv_resexplorer.Items.CopyTo(v_itemtb, 0);
            foreach (ListViewItem i in v_itemtb)
            {
                if (i.Tag is Image )
                {
                    this.c_lsv_resexplorer.Items.Remove(i);
                }
            }
        }

        private void filesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)                
                {
                    InsertFiles(ofd.FileNames);
                }
            }
        }

        private void InsertFiles(string[] p)
        {
            ListViewItem v_item = null;
            string v_n = null;
            foreach (string  i in p)
            {
                v_n = Path.GetFileNameWithoutExtension(i).Replace (".","_").Replace (" ","_");
                if (this.c_lsv_resexplorer.Items.ContainsKey(v_n ))
                    continue ;
                v_item = new ListViewItem(v_n);
                v_item.SubItems.Add("Files");
                
                v_item.Name = v_n;
                v_item.Tag = File.ReadAllBytes(i);
                v_item.SubItems.Add(v_item.Tag.ToString());
                this.c_lsv_resexplorer.Items.Add(v_item);
            }
        }

        private void iconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "All Icons|*.ico";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    InsertIcons(ofd.FileNames);
                }
            }
        }

        private void InsertIcons(string[] p)
        {
            ListViewItem v_item = null;
            string v_n = null;
            Icon v_ic = null;
            foreach (string i in p)
            {
                v_n = Path.GetFileNameWithoutExtension(i).Replace(".", "_").Replace(" ", "_");
                if (this.c_lsv_resexplorer.Items.ContainsKey(v_n))
                    continue;
                try
                {
                    v_ic = new Icon(i);
                }
                catch {
                    v_ic = null;
                    continue;
                }
                v_item = new ListViewItem(v_n);
                v_item.Name = v_n;
                v_item.SubItems.Add("Icons");
                v_item.SubItems.Add("");
                v_item.Tag = v_ic;
                this.c_lsv_resexplorer.Items.Add(v_item);
            }
        }

        private void imagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "All Pictures|*.bmp; *.jpg;*.tiff;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    InsertPictures(ofd.FileNames);
                }
            }
        }

        private void InsertPictures(string[] p)
        {
            ListViewItem v_item = null;
            string v_n = null;
            Image  v_ic = null;
            foreach (string i in p)
            {
                v_n = Path.GetFileNameWithoutExtension(i).Replace(".", "_").Replace(" ", "_");
                if (this.c_lsv_resexplorer.Items.ContainsKey(v_n))
                    continue;
                try
                {
                    v_ic = Image.FromFile (i);
                }
                catch
                {
                    v_ic = null;
                    continue;
                }
                v_item = new ListViewItem(v_n);
                v_item.SubItems.Add("Picture");
                v_item.SubItems.Add("");
                v_item.Name = v_n;
                v_item.Tag = v_ic;
                this.c_lsv_resexplorer.Items.Add(v_item);
            }
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //extract selected items
            if (this.c_lsv_resexplorer.SelectedItems.Count == 0)
                return;
            string f = string.Empty;
            using (FolderBrowserDialog sfd = new FolderBrowserDialog())
            {
                
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    f = sfd.SelectedPath;
                    foreach (ListViewItem item in this.c_lsv_resexplorer.SelectedItems )
                    {
                        if (item.Tag is byte[])
                        {
                            f = sfd.SelectedPath + "\\" + item.Text + ".data";
                            File.WriteAllBytes (f,
                                item.Tag as byte[]);
                        }
                        else if (item.Tag is Image)
                        {
                            f = sfd.SelectedPath + "\\" + item.Text + ".bmp";
                            Image img = (Image)item.Tag ;
                            img.Save(f);
                        }
                        else if (item.Tag is Icon)
                        {
                            f = sfd.SelectedPath + "\\" + item.Text + ".ico";
                            Icon ic = (Icon)item.Tag;
                            FileStream fs = File.Create (f);
                            ic.Save(fs);
                            fs.Close();
                        }
                    }
                }
            }
        }

        private void c_lsv_resexplorer_ColumnClick(object sender, ColumnClickEventArgs e)
        {
           ColumnHeader  col = this.c_lsv_resexplorer.Columns[e.Column];
           MyListViewSorter c = this.c_lsv_resexplorer.ListViewItemSorter as MyListViewSorter;
           c.index = e.Column;
           this.c_lsv_resexplorer.Sorting = SortOrder.Ascending;
           this.c_lsv_resexplorer.Sort();
        }

        private void viewMatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ListViewItem> v_list = new List<ListViewItem>();
            foreach (ListViewItem item in c_lsv_resexplorer.Items)
            {
                if (item.Text == item.SubItems[2].Text )
                {
                    v_list.Add (item );
                }
            }
            c_lsv_resexplorer.Items.Clear ();
            c_lsv_resexplorer.Items.AddRange (v_list.ToArray ());
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ResManAboutBox box = new ResManAboutBox ())
            {
               box.Owner = this;
                box.ShowDialog();
            }
        }

        private void mergeAllResourcesFielsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resources | *.resources";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string item in ofd.FileNames)
                    {
                        this.LoadResourceFile(item); 
                    }
                }
            }
        }

        private void exportToAndroid_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> m_v = new Dictionary<string, string>();
            foreach (ListViewItem item in this.c_lsv_resexplorer.Items)
            {
                if (item.SubItems[1].Text == typeof (string).ToString ())
                {
                    m_v.Add(item.Name, item.SubItems[2].Text);
                }
            }
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Android xml resources string | *.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    AndroidUtils.ExportToFile(sfd.FileName, m_v );
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Form frm = new Form())
            {
                UIEditTextControl ctr = new UIEditTextControl();
                ctr.Index = (this.c_lsv_resexplorer.SelectedItems.Count > 0) ? this.c_lsv_resexplorer.SelectedItems[0].Index : 0; 
                ctr.ListView = this.c_lsv_resexplorer;
                frm.Controls.Add(ctr);

                frm.Owner = this;
                frm.Text = "Modifier en cascade";
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }


    }

    class MyListViewSorter : IComparer
    {
        internal int index;

        public MyListViewSorter()
        {
            index = 0;
        }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            ListViewItem item1 = x as ListViewItem;
            ListViewItem item2 = y as ListViewItem;
            if (index == 0) 
            return item1.Text.CompareTo(item2.Text);

            return item1.SubItems[index].Text.CompareTo (item2.SubItems[index].Text);

        }

        #endregion
    }
}
