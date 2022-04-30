

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXProjectSelectorGUI.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
    using IGK.ICore.Resources;
    using System.IO;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinCore.WinUI;

    [Designer(WinCoreConstant.CTRL_DESIGNER)]
    public class UIXProjectSelectorGUI : IGKXUserControl
    {
        private IGKXExpenderBox c_expanderBox;
        private Splitter splitter1;
        private Label lb_choosecat;
        private IGKXPanel c_pan_content;
    
        public UIXProjectSelectorGUI()
        {
            this.InitializeComponent();
            this.Load += _Load;
        }

        void _Load(object sender, EventArgs e)
        {
            this.InitProjectList();
            this.LoadDisplayText();
            
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            this.lb_choosecat.Text = "msg.pleasechoose.cat".R();
        }

        private void InitProjectList()
        {
            this.c_expanderBox.Clear();
            var g = this.c_expanderBox.AddGroup("Project");
            g.CaptionKey = "lb.project.all";

            //load all registrated project project
            CoreWorkingProjectTemplateAttribute[] project = CoreWorkingProjectTemplateAttribute.GetAllProjects();
            if (project != null)
            {
                foreach (CoreWorkingProjectTemplateAttribute item in project)
                {
                    if ((item is CoreWorkingProjectItemTemplateAttribute))
                    {
                        continue;
                    }
                    IGKXExpenderBoxGroupItem r = new IGKXExpenderBoxGroupItem
                    {
                        Name = item.Name,
                        CaptionKey = "TemplateProject." + item.Name,
                        Tag = item,
                        Height = 16
                    };
                    r.Click += r_Click;

                    g.Items.Add(r);
                }
            }
          

            //add registrated group category
            g = this.c_expanderBox.AddGroup("Templates");
            g.CaptionKey = "lb.project.templates";

            //additional template
            //g = this.c_expanderBox.AddGroup("CustomTemplates");
            //g.CaptionKey = "lb.customtemplates.all";
        }

        void r_Click(object sender, EventArgs e)
        {
            //selected item click
            IGKXExpenderBoxItem i = sender as IGKXExpenderBoxItem;
            if (i.Tag is CoreWorkingProjectTemplateAttribute p)
            {
                if (p.ConfigType != null)
                {
                    if (p.ConfigType.Assembly.CreateInstance(p.ConfigType.FullName) is ICoreWorkingProjectConfiguration n)
                    {
                        //Form frm = this.FindForm();
                        //frm.Hide();
                        switch (n.GetConfigType())
                        {
                            case enuParamConfigType.ParameterConfig:
                                this.c_pan_content.Controls.Clear();
                                CoreSystem.GetWorkbench().BuildWorkingProperty(
                                       this.c_pan_content,
                                    n);
                                break;
                            case enuParamConfigType.CustomControl:
                                ICoreControl r = n.GetConfigControl();
                                this.c_pan_content.Controls.Clear();
                                var ctr = r as Control;
                                ctr.Dock = DockStyle.Fill;
                                this.c_pan_content.Controls.Add(ctr);
                                break;
                        }
                    }
                    this.SelectedProject = p;
                }
                else
                {
                    //show child properties
                    if (this.SelectedProject != p)
                    {
                        this.ShowChildProjectItem(p);
                    }
                }
            }
        }

        private void ShowChildProjectItem(CoreWorkingProjectTemplateAttribute p)
        {
            CoreWorkingProjectTemplateAttribute[] d =  CoreWorkingProjectTemplateAttribute.GetChilds(p.Name);

            if ((d != null) && (d.Length > 0))
            {
                this.c_pan_content.SuspendLayout();
                this.c_pan_content.Controls.Clear();
                ListView v_listView = new ListView
                {
                    Dock = DockStyle.Fill
                };

                ImageList imgList = new ImageList
                {
                    ImageSize = new Size(72, 72),
                    TransparentColor = Color.Transparent,
                    ColorDepth = ColorDepth.Depth32Bit
                };


                v_listView.View = View.LargeIcon;
                v_listView.LargeImageList = imgList;
                v_listView.SmallImageList = imgList;
                v_listView.DoubleClick += v_listView_Click;
                v_listView.KeyPress += v_listView_KeyPress;
                v_listView.MultiSelect = false;
                ListViewItem v_litem = null;
                //add default templates
                v_litem = new ListViewItem
                {
                    ImageKey = CoreImageKeys.TEMPLATE_DEFAULT_GKDS,
                    Text = ("template.default").R(),
                    Tag = p
                };
                v_listView.Items.Add(v_litem);

                RegisterDocument(imgList , CoreImageKeys.TEMPLATE_DEFAULT_GKDS);
                RegisterDocument(imgList, CoreImageKeys.TEMPLATE_ITEM_GKDS);
                string imgKey = string.Empty;
                //add additional template
                foreach (var item in d)
                {
                    v_litem = new ListViewItem();
                    imgKey = item.ImageKey ?? "template_" + Path.GetExtension(item.Name).Substring(1);
                    RegisterDocument(imgList, imgKey);
                    if (!imgList.Images.ContainsKey(imgKey))
                        imgKey = CoreImageKeys.TEMPLATE_ITEM_GKDS;
                    else {

                    }                 
                    v_litem.ImageKey = imgKey;                    
                    v_litem.Text = ("template." + item.Name).R();
                    v_litem.Tag = item;
                    if (!string.IsNullOrEmpty(item.Group))
                    {
                        //v_litem.Group.
                        var g = v_listView.Groups[item.Group];
                        if (g == null)
                        {
                            g = v_listView.Groups.Add(item.Group, item.Group.R());
                        }
                        v_litem.Group = g;
                    }
                    
                    v_listView.Items.Add(v_litem);
                }


                this.c_pan_content.Controls.Add(v_listView);
                this.c_pan_content.ResumeLayout();
                this.SelectedProject = p;
            }
            else
            {
                CallAndInit(p);
            }
            
        }

        void v_listView_KeyPress(object sender, KeyPressEventArgs e)
        {
            ListView v_lsv = sender as ListView;
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                this.v_listView_Click(v_lsv, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void CallAndInit(CoreWorkingProjectTemplateAttribute p)
        {
            if ((p == null) ||  (p.TargetSurfaceType==null))
                return;
            var bench = CoreSystem.GetWorkbench() as ICoreSurfaceManagerWorkbench ;
            if (p.ConfigType != null)
            {
                if (p.ConfigType.Assembly.CreateInstance(p.ConfigType.FullName) is ICoreWorkingProjectWizard n)
                {
                    this.FindForm().Hide();
                    if (n.RunConfigurationWizzard(bench) == enuDialogResult.OK)
                    {
                        if (n.IsWellConfigured)
                        {
                            //init config control
                            bench.AddSurface(n.Surface, true);
                            this.FindForm().DialogResult = DialogResult.OK;
                            return;
                        }
                    }
                    this.FindForm().Show();
                }
            }
            else
            {
                //create a new surface 

                var frm = this.FindForm();
                if ((p.TargetSurfaceType.Assembly.CreateInstance(p.TargetSurfaceType.FullName) is ICoreWorkingSurface s) && (bench is ICoreSurfaceManagerWorkbench b))
                {
                    try
                    {
                        s.SetParam(p.GetInitializationParams());
                        b.Surfaces.Add(s);
                        if (b.Surfaces.Contains(s))
                        {
                            b.CurrentSurface = s;
                            //init config control
                            frm.DialogResult = DialogResult.OK;
                        }
                        else
                            frm.DialogResult = DialogResult.No;
                    }
                    catch(Exception ex) {
                        CoreLog.WriteError(ex.Message);
                        frm.DialogResult = DialogResult.No;
                    }
                    finally {
                        //free on dispose
                        if(frm.DialogResult==  DialogResult.No)
                            s.Dispose();

                    }
                }
            }
        }

        private void RegisterDocument(ImageList imgList, string name)
        {
            if (imgList.Images.ContainsKey(name))
                return;
            ICore2DDrawingDocument document = CoreResources.GetDocument(name);
            if (document != null)
            {
                Bitmap v_bmp =  WinCoreBitmapOperation.GetBitmap(document,
                    CoreScreen.DpiX, CoreScreen.DpiY).ToGdiBitmap(true);
                if (v_bmp != null)
                {
                    imgList.Images.Add(name, v_bmp);
                }
            }
        }

        void v_listView_Click(object sender, EventArgs e)
        {
            ListView l = (ListView)sender;
            if (l.SelectedItems.Count == 1)
            {
                CoreWorkingProjectTemplateAttribute r = l.SelectedItems[0].Tag as CoreWorkingProjectTemplateAttribute;
                CallAndInit(r);
            }
        }

        private void InitializeComponent()
        {
            this.c_expanderBox = new IGKXExpenderBox();
            this.c_pan_content = new IGKXPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.lb_choosecat = new System.Windows.Forms.Label();
            this.c_pan_content.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_expanderBox
            // 
            this.c_expanderBox.CaptionKey = null;
            this.c_expanderBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_expanderBox.Location = new System.Drawing.Point(0, 0);
            this.c_expanderBox.Margin = new System.Windows.Forms.Padding(0);
            this.c_expanderBox.Name = "c_expanderBox";
            this.c_expanderBox.SelectedGroup = null;
            this.c_expanderBox.Size = new System.Drawing.Size(213, 322);
            this.c_expanderBox.TabIndex = 0;
            // 
            // c_pan_content
            // 
            this.c_pan_content.CaptionKey = null;
            this.c_pan_content.Controls.Add(this.lb_choosecat);
            this.c_pan_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_content.Location = new System.Drawing.Point(213, 0);
            this.c_pan_content.Name = "c_pan_content";
            this.c_pan_content.Size = new System.Drawing.Size(396, 322);
            this.c_pan_content.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(213, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 322);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // lb_choosecat
            // 
            this.lb_choosecat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_choosecat.Font = new System.Drawing.Font("Arial Black", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_choosecat.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lb_choosecat.Location = new System.Drawing.Point(0, 0);
            this.lb_choosecat.Name = "lb_choosecat";
            this.lb_choosecat.Size = new System.Drawing.Size(396, 322);
            this.lb_choosecat.TabIndex = 1;
            this.lb_choosecat.Text = "Please Choose a Category";
            this.lb_choosecat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIXProjectSelectorGUI
            // 
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.c_pan_content);
            this.Controls.Add(this.c_expanderBox);
            this.Name = "UIXProjectSelectorGUI";
            this.Size = new System.Drawing.Size(609, 322);
            this.c_pan_content.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public CoreWorkingProjectTemplateAttribute SelectedProject { get; set; }
    }
}
