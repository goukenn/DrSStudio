

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XResourceSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XResourceSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using IGK.ICore;
using IGK.DrSStudio.WinUI;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.ResourcesManager.WinUI
{
    [XResourceSurface(
        "XResourceSurface",
        RSConstant.SURFACE_ID,
        EnvironmentName = RSConstant.SURFACE_ENVIRONMENT)]
    public class XResourceSurface : IGKXWinCoreWorkingSurface , 
        IRSSurface ,
        ICoreWorkingFilemanagerSurface 
    {
        private bool m_ShowOnlyNewMatch;

        public bool ShowOnlyNewMatch
        {
            get { return m_ShowOnlyNewMatch; }
            set
            {
                if (m_ShowOnlyNewMatch != value)
                {
                    m_ShowOnlyNewMatch = value;
                    Reload();
                    OnShowOnlyNewMatch(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ShowOnlyNewMatchChanged;
        private void OnShowOnlyNewMatch(EventArgs eventArgs)
        {
            if (ShowOnlyNewMatchChanged != null)
                this.ShowOnlyNewMatchChanged(this, eventArgs);
        }
        class ResourceInfo
        {
            private string m_Name;
            private Type m_Type;
            private object m_Value;

            public object Value
            {
                get { return m_Value; }
                set
                {
                    if (m_Value != value)
                    {
                        m_Value = value;
                    }
                }
            }
            public Type Type
            {
                get { return m_Type; }
                set
                {
                    if (m_Type != value)
                    {
                        m_Type = value;
                    }
                }
            }
            public string Name
            {
                get { return m_Name; }
                set
                {
                    if (m_Name != value)
                    {
                        m_Name = value;
                    }
                }
            }
        }
        private XResourcesListView c_lsv_items;
        private IGKXPanel c_pan_bottom;
        private IGKXPanel c_pan_top;
        private IGKXTextBox c_txb_searchBox;
        private Dictionary<string, ResourceInfo> m_Items;
        private Thread m_th;

        public override void LoadDisplayText()
        {
            base.LoadDisplayText();

            this.Title = "title.ResourceManagerSurface".R();
            this.c_lsv_items.LoadDisplayText();

        }
        
        public override string Title
        {
            get
            {
                return base.Title;
            }
            protected set
            {
                base.Title = value;
            }
        }
        public XResourceSurface()
        {
            InitializeComponent();
            this.InitControl();
         
        }

        private void InitControl()
        {
            this.m_Items = new Dictionary<string, ResourceInfo>();
            this.c_lsv_items.EndEditEvent += c_lsv_items_EndEditEvent;
            this.c_lsv_items.KeyPress += c_lsv_items_KeyPress;
            this.c_lsv_items.KeyUp += c_lsv_items_KeyUp;
            this.Load += XResourceSurface_Load;
            this.LoadDisplayText();
        }

        void XResourceSurface_Load(object sender, EventArgs e)
        {
            this.Reload();
        }

        void c_lsv_items_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedItem();
                e.Handled = true;
            }
        }

        private void DeleteSelectedItem()
        {
            //delet selected item by key change
            if (this.c_lsv_items.SelectedItems.Count > 0)
            {
                ListView.SelectedListViewItemCollection v_ee = this.c_lsv_items.SelectedItems;
                List<ListViewItem> rt = new List<ListViewItem>();
                for (int i = 0; i < v_ee.Count; i++)
                {
                    ListViewItem item = v_ee[i];
                    if (this.m_Items.ContainsKey(item.Name))
                    {
                        this.m_Items.Remove(item.Name);
                        rt.Add(item);
                    }
                }
                foreach (ListViewItem  item in rt)
                {
                    this.c_lsv_items.Items.Remove(item);
                }
                this.c_lsv_items.SelectedItems.Clear();
             
            }
        }

        void c_lsv_items_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((Keys)e.KeyChar)
            { 
                case Keys.Delete:
                    DeleteSelectedItem();
                    e.Handled = true;
                    break;
            }
        }

        private void loadResources()
        {
            this.m_Items.Clear();
            Dictionary<string, string>.Enumerator e = CoreResourcesManager.GetStrings();
            c_lsv_items.SuspendLayout();
            ResourceInfo rs = null;
            while (e.MoveNext())
            {
                if (this.m_Items.ContainsKey(e.Current.Key))
                    continue;
               
                rs = new ResourceInfo()
                {
                    Name = e.Current.Key,
                    Type = e.Current.Value != null ? e.Current.Value.GetType() : typeof(string),
                    Value = e.Current.Value
                };
                
                this.m_Items.Add(rs.Name, rs);
                this.c_lsv_items.Items.Add(NewResItem(rs));
            }
            c_lsv_items.ResumeLayout();
        }

        private void c_lsv_items_EndEditEvent(object sender, XResourcesItemChangedEventArgs e)
        {
            this.m_Items[e.ListViewItem.Name].Value = e.SubItem.Text;
        }

      
        private ListViewItem NewResItem(ResourceInfo rs)
        {
            ListViewItem  v_item = new ListViewItem(rs.Name);
            v_item.Name = rs.Name;
            v_item.Tag = rs.Type;
            v_item.SubItems.Add(rs.Type.ToString());
            v_item.SubItems.Add(rs.Value != null ? rs.Value.ToString() : "");
            
            return v_item;
        }




        private void InitializeComponent()
        {
            this.c_lsv_items = new IGK.DrSStudio.ResourcesManager.WinUI.XResourcesListView();
            this.c_pan_bottom = new IGKXPanel();
            this.c_pan_top = new IGKXPanel();
            this.c_txb_searchBox = new IGKXTextBox();
            this.c_pan_top.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_lsv_items
            // 
            this.c_lsv_items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_lsv_items.FullRowSelect = true;
            this.c_lsv_items.GridLines = true;
            this.c_lsv_items.LabelEdit = true;
            this.c_lsv_items.Location = new System.Drawing.Point(0, 38);
            this.c_lsv_items.Name = "c_lsv_items";
            this.c_lsv_items.Size = new System.Drawing.Size(632, 194);
            this.c_lsv_items.TabIndex = 0;
            this.c_lsv_items.UseCompatibleStateImageBehavior = false;
            this.c_lsv_items.View = System.Windows.Forms.View.Details;
            // 
            // c_pan_bottom
            // 
            this.c_pan_bottom.CaptionKey = null;
            this.c_pan_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c_pan_bottom.Location = new System.Drawing.Point(0, 232);
            this.c_pan_bottom.Name = "c_pan_bottom";
            this.c_pan_bottom.Size = new System.Drawing.Size(632, 84);
            this.c_pan_bottom.TabIndex = 1;
            // 
            // c_pan_top
            // 
            this.c_pan_top.CaptionKey = null;
            this.c_pan_top.Controls.Add(this.c_txb_searchBox);
            this.c_pan_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.c_pan_top.Location = new System.Drawing.Point(0, 0);
            this.c_pan_top.Name = "c_pan_top";
            this.c_pan_top.Size = new System.Drawing.Size(632, 38);
            this.c_pan_top.TabIndex = 2;
            // 
            // c_txb_searchBox
            // 
            this.c_txb_searchBox.Location = new System.Drawing.Point(3, 12);
            this.c_txb_searchBox.Name = "c_txb_searchBox";
            this.c_txb_searchBox.Size = new System.Drawing.Size(288, 20);
            this.c_txb_searchBox.TabIndex = 0;
            this.c_txb_searchBox.TextChanged += new System.EventHandler(this.c_txb_searchBox_TextChanged);
            // 
            // XResourceSurface
            // 
            this.Controls.Add(this.c_lsv_items);
            this.Controls.Add(this.c_pan_top);
            this.Controls.Add(this.c_pan_bottom);
            this.Name = "XResourceSurface";
            this.Size = new System.Drawing.Size(632, 316);
            this.c_pan_top.ResumeLayout(false);
            this.c_pan_top.PerformLayout();
            this.ResumeLayout(false);

        }
        #region "Available action"
        /// <summary>
        /// st
        /// </summary>
        internal void Store()
        {
            System.IO.MemoryStream mem = new System.IO.MemoryStream ();
            ResourceWriter rsw = new ResourceWriter (mem );
            string value = string.Empty;
            foreach (KeyValuePair<string, ResourceInfo >  item in this.m_Items)
            {
                try
                {
                    rsw.AddResource(item.Key, item.Value.Value);
                }
                catch {
                    CoreLog.WriteError (item.Key);
                }
            }
     
            rsw.Generate();
            mem.Seek(0, System.IO.SeekOrigin.Begin);
            ResourceReader rsr = new ResourceReader(mem);
            CoreResourcesManager.StoreStringResources(rsr);
            rsr.Close();
            rsw.Close();
            mem.Dispose();
        }
        #endregion

        private void c_txb_searchBox_TextChanged(object sender, EventArgs e)
        {
            StartSearch();
        }

        private void StartSearch()
        {
            AbortSearch();
            m_th = new Thread(_searching);
            m_th.IsBackground = false;
            m_th.Start();
        }
        private void _searching()
        {
            string s = this.c_txb_searchBox.Text;
            if (string.IsNullOrEmpty(s))
            {
                this.c_lsv_items.Invoke((MethodInvoker)delegate()
                {
                    this.loadAll();
                });
            }
            else {
                s = s.ToRegex();
                List<ListViewItem> it = new List<ListViewItem>();
                foreach (KeyValuePair<string, ResourceInfo >  inf in this.m_Items)
                {
                    if (this.ShowOnlyNewMatch)
                    {
                        if (inf.Value.Type == typeof(string))
                        {
                            if (inf.Value.Name.ToLower() != inf.Value.Value.ToString().ToLower())
                            {
                                continue;
                            }
                        }
                    }
                    if (Regex.IsMatch(inf.Key, s, RegexOptions.IgnoreCase))
                    {
                        it.Add (NewResItem(inf.Value));
                    }
                }
                this.c_lsv_items.Invoke((MethodInvoker)delegate()
                {
                    this.c_lsv_items.Items.Clear();
                    this.c_lsv_items.Items.AddRange(it.ToArray());
                });
            }
        }

        private void loadAll()
        {
            List<ListViewItem> it = new List<ListViewItem>();
            foreach (KeyValuePair<string, ResourceInfo> inf in this.m_Items)
            {
                if (this.ShowOnlyNewMatch)
                {
                    if (inf.Value.Type == typeof(string))
                    {
                        if (inf.Value.Name.ToLower() != inf.Value.Value.ToString().ToLower())
                        {
                            continue;
                        }
                    }
                }
                it.Add(NewResItem(inf.Value));              
            }
            this.c_lsv_items.Invoke((MethodInvoker)delegate()
            {
                this.c_lsv_items.Items.Clear();
                this.c_lsv_items.Items.AddRange(it.ToArray());
            });
        }

        private void AbortSearch()
        {
            if (this.m_th != null)
            {
                this.m_th.Abort();
                this.m_th.Join();
            }
        }

        internal void Reload()
        {
            this.loadResources();
            //raise text search box
            this.c_txb_searchBox_TextChanged(this.c_txb_searchBox, EventArgs.Empty);
        }

        internal void AddText(string key, string value)
        {
            if ((string.IsNullOrEmpty (key)) ||(this.m_Items.ContainsKey(key)))
            {
                return;
            }
            this.m_Items.Add(key, new ResourceInfo() { 
                Name = key,
                Value = value ,
                Type = typeof (string)
            });
        }

        internal void ClearAndReload()
        {
            this.m_Items.Clear();
            this.Store();
            this.Reload();
        }

        public string FileName
        {
            get
            {
                return this.m_fileName;
            }
            set
            {
                this.m_fileName = value;
            }
        }

        public event EventHandler FileNameChanged;
        ///<summary>
        ///raise the FileNameChanged 
        ///</summary>
        protected virtual void OnFileNameChanged(EventArgs e)
        {
            if (FileNameChanged != null)
                FileNameChanged(this, e);
        }

        public void ReloadFileFromDisk()
        {
        }

        public void RenameTo(string name)
        {
        }

        public bool NeedToSave
        {
            get
            {
                return this.m_needToSave;
            }
            set
            {
                this.m_needToSave = value;
            }
        }

        
        public event EventHandler NeedToSaveChanged;
        ///<summary>
        ///raise the NeedToSaveChanged 
        ///</summary>
        protected virtual void OnNeedToSaveChanged(EventArgs e)
        {
            if (NeedToSaveChanged != null)
                NeedToSaveChanged(this, e);
        }


        public event EventHandler Saved;
        ///<summary>
        ///raise the Saved 
        ///</summary>
        protected virtual void OnSaved(EventArgs e)
        {
            if (Saved != null)
                Saved(this, e);
        }


        private bool m_needToSave;
        private string m_fileName;
        private bool m_saving;

        public bool Saving
        {
            get { return this.m_saving; }
        }

        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("title.saveResources".R(),
                "Resources Files| *.resources",
                this.FileName);
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(this.FileName))
            {
                this.SaveAs(this.FileName);
            }
        }

        public void SaveAs(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return;

            this.m_saving = true;
            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            ResourceWriter rsw = new ResourceWriter(mem);
            string value = string.Empty;
            foreach (KeyValuePair<string, ResourceInfo> item in this.m_Items)
            {
                try
                {
                    rsw.AddResource(item.Key, item.Value.Value);
                }
                catch
                {
                    CoreLog.WriteError(item.Key);
                }
            }

            rsw.Generate();
            mem.Seek(0, System.IO.SeekOrigin.Begin);
            var fs = File.Create(filename);
            mem.WriteTo(fs);
            fs.Flush();
            fs.Close ();
            mem.Dispose();
            this.m_saving = false;
        }
    }
}

