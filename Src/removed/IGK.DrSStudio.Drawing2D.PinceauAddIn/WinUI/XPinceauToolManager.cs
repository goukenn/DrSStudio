

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XPinceauToolManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:XPinceauToolManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Resources;
    /// <summary>
    /// represent a control item manager
    /// </summary>
    internal class XPinceauToolManager : IGK.DrSStudio.WinUI.UIXToolConfigControlBase 
    {
        XToolStrip c_toolStrip;
        XToolStripButton c_editPinceau;
        XToolStripButton c_chooseDir;
        XPanel c_panel;
        private string m_CurrentDirectory;
        /// <summary>
        /// get or  set the current directory
        /// </summary>
        public string CurrentDirectory
        {
            get { return m_CurrentDirectory; }
            set
            {
                if (m_CurrentDirectory != value)
                {
                    m_CurrentDirectory = value;
                    OnDirectoryChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler DirectoryChanged;
        ///<summary>
        ///raise the DirectoryChanged 
        ///</summary>
        protected virtual void OnDirectoryChanged(EventArgs e)
        {
            if (DirectoryChanged != null)
                DirectoryChanged(this, e);
        }
        private ICore2DDrawingLayeredElement  m_SelectedSymbol;
        public ICore2DDrawingLayeredElement  SelectedSymbol
        {
            get { return m_SelectedSymbol; }
            set
            {
                m_SelectedSymbol = value;
                OnSelectedSymbolClick(EventArgs.Empty);
            }
        }
        public event EventHandler SelectedSymbolClick;
        private void OnSelectedSymbolClick(EventArgs eventArgs)
        {
            if (this.SelectedSymbolClick != null)
                this.SelectedSymbolClick(this, eventArgs);
        }
        public bool EditPinceau { get { return c_editPinceau.Enabled; }
            set { c_editPinceau.Enabled = value; }
        }
        /// <summary>
        /// uses to catch edit pinceau click 
        /// </summary>
        public event EventHandler EditPinceauClick {
            add { c_editPinceau.Click += value; }
            remove { c_editPinceau.Click -= value; }
        }
        public event EventHandler ChooseDirClick {
            add { c_chooseDir.Click += value; }
            remove { c_chooseDir.Click -= value; }
        }
        public XPinceauToolManager():this(Tools.ToolPinceauManager.Instance )
        {
        }
        public XPinceauToolManager(Tools.ToolPinceauManager tool):base(tool )
        {
            this.InitializeComponent();
            this.Load += new EventHandler(_Load);
            this.DirectoryChanged += new EventHandler(_DirectoryChanged);
            this.m_CurrentDirectory = Path.GetFullPath (IGK.DrSStudio.Settings.CoreApplicationSetting.Instance.GetValue("PencilsDir",Application.StartupPath+"/Pencils") as String);
            this.c_panel.SizeChanged += new EventHandler(c_panel_SizeChanged);
        }
        void c_panel_SizeChanged(object sender, EventArgs e)
        {
            this.c_panel.SuspendLayout();
            this.UpdateCPanelBound();
            this.c_panel.ResumeLayout();
        }
        void _DirectoryChanged(object sender, EventArgs e)
        {
            LoadPinceau();
        }
        void _Load(object sender, EventArgs e)
        {
            this.c_editPinceau.ImageDocument = CoreResources.GetDocument("btn_edit");
            this.c_chooseDir.ImageDocument = CoreResources.GetDocument("directory");
            //load system reg pinceau
            LoadPinceau();
        }
        private void LoadPinceau()
        {
            this.c_panel.Controls.Clear();
            if (Directory.Exists(this.CurrentDirectory )==false )
                return ;
            List<ICore2DDrawingLayeredElement> ml = new List<ICore2DDrawingLayeredElement>();
            foreach (string f in System.IO.Directory.GetFiles(this.CurrentDirectory))
            {
                try
                {
                    ml.AddRange(PinceauDocument.GetStyle(f));
                }
                catch (Exception)
                { }
            }
                XPinceauToolItem v_citem = null;
                this.c_panel.SuspendLayout();
                foreach (var item in ml)
                {
                    if (item != null)
                    {
                        v_citem = new XPinceauToolItem(item.Clone() as ICore2DDrawingLayeredElement);
                        v_citem.Click += new EventHandler(v_citem_Click);
                        this.c_panel.Controls.Add(v_citem);
                    }
                }
                this.UpdateCPanelBound();
                this.c_panel.ResumeLayout();
        }
        private void UpdateCPanelBound()
        {
            int x = 1;
            int y = 1;
            foreach (Control item in c_panel.Controls )
            {
                item.Bounds = new Rectangle(
                    x,y,
                    item.Width,
                    item.Height
                    );
                x += item.Width+1;
                if (x >= c_panel.Width)
                {
                    x = 0;
                    y += item.Height +1;
                }
            }
        }
        void v_citem_Click(object sender, EventArgs e)
        {
            this.SelectedSymbol = (sender as XPinceauToolItem).PinceauElement;
        }
        void UpdateItemBoundSize()
        {
            this.c_panel.SuspendLayout();
            int v_x = 3;
            int v_y = 3;
            foreach (XPinceauToolItem  item in this.c_panel.Controls )
            {
                item.Bounds = new System.Drawing.Rectangle(v_x, v_y,
                    32, 32);
                v_x += 32;
                if (v_x > this.Width)
                {
                    v_x = 3;
                    v_y += 32;
                }
            }
            this.c_panel.ResumeLayout();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            c_toolStrip = new XToolStrip();
            c_chooseDir = new XToolStripButton();
            c_editPinceau = new XToolStripButton();
            c_panel = new XPanel();
            c_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(c_panel);
            this.Controls.Add(c_toolStrip);
            c_toolStrip.Items.Add(c_chooseDir);
            c_toolStrip.Items.Add(c_editPinceau);
            // 
            // XPinceauToolManager
            // 
            this.Name = "XPinceauToolManager";
            this.Size = new System.Drawing.Size(293, 246);
            this.ResumeLayout(false);
        }
    }
}

