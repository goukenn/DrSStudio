

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EnvironmentToolSetting.cs
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
file:EnvironmentToolSetting.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
namespace IGK.DrSStudio.WinLauncher
{
    using IGK.ICore;using IGK.DrSStudio.Settings;
    using IGK.DrSStudio.WinUI ;
    sealed class EnvironmentToolSetting : CorePropertySetting
    {
        public string ToolName {
            get { return (string)this["Name"].Value ; }
        }
        public enuLayoutToolDisplay Display {
            get {
                return (enuLayoutToolDisplay)this["Display"].Value ;
            }
            set {
                this["Display"].Value = value ;
            }
        }
        public Vector2i Location {
            get {
                return (Vector2i)this["Location"].Value;
            }
            set {
                this["Location"].Value =value ;
            }
        }
        public Size2i Size
        {
            get
            {
                return (Size2i)this["Size"].Value;
            }
            set
            {
                this["Size"].Value = value;
            }
        }
        public int Index
        {
            get { return (int)this["Index"].Value ; }
            set { this["Index"].Value  = value; }
        }
        public string SelectedToolName
        {
            get { return this["SelectedToolName"].Value as string ; }
            set { this["SelectedToolName"].Value = value; }
        }
        public EnvironmentToolSetting():base("Tool")
        {
        }
        /// <summary>
        /// represent the panel property
        /// </summary>
        private WinLauncherLayoutManager.WinLauncherToolPanelProperty panel;
        /// <summary>
        /// for loading 
        /// </summary>
        /// <param name="name"></param>
        internal EnvironmentToolSetting(string name):base(name)
        {
            this.Add("Location", Vector2i.Zero, null);
            this.Add("Display", enuLayoutToolDisplay.Left, null);            
            this.Add("Size",  Size2i.Empty, null);
            this.Add("Index", -1, null);
            this.Add("SelectedToolName", null, null);
        }
        public EnvironmentToolSetting(WinLauncherLayoutManager.WinLauncherToolPanelProperty panel)
            : base(panel.Tool.Id )
        {
            this.Add("Display", panel.ToolDisplay , null);
            this.Add ("Location", panel.Location,null);
            this.Add("Size", panel.Size , null );
            this.Add("Index", 0 , null);
            this.panel = panel;
            this.Bind(panel);
        }
        internal void Bind(WinLauncherLayoutManager.WinLauncherToolPanelProperty panel)
        {
            if (this.panel == null)
            this.panel = panel;
            panel.ToolDisplayChanged += new EventHandler(panel_ToolDisplayChanged);
            panel.LocationChanged += new EventHandler(panel_LocationChanged);
            panel.SizeChanged += new EventHandler(panel_SizeChanged);
        }
        void _IndexChanged(object o, EventArgs e)
        {
            //this.Index = panel.Page.Index;
            if ((panel.Page != null) && (panel.Page.Tool != null))
            {
                this.SelectedToolName = panel.Page.Tool.Id;
            }
            else
                this.SelectedToolName = null;
        }
        void panel_ToolDisplayChanged(object sender, EventArgs e)
        {
            this.Display = panel.ToolDisplay;
        }
        internal void Load(ICoreApplicationSetting dummySetting)
        {
            if (dummySetting.HasChild)
            {
                TypeConverter v_tconv = null;
                object v = null;
                foreach (KeyValuePair<string, ICoreApplicationSetting > i in dummySetting)
                {
                    if (!this.Contains(i.Key)) continue;
                        v = this[i.Key].Value;
                        v_tconv = TypeDescriptor.GetConverter(v);
                        if ((i.Value != null)&&(v_tconv !=null))
                        {
                            this[i.Key].Value  = v_tconv.ConvertFromString(i.Value.Value.ToString());
                        }
                }
            }
        }
        /// <summary>
        /// initialize the panel property
        /// </summary>
        /// <param name="panelProperty"></param>
        internal void AttachTo(WinLauncherLayoutManager.WinLauncherToolPanelProperty panelProperty)
        {
            if (this.panel !=null)
            {
                //unbind
                UnBind(this.panel );
            }
            this.panel = panelProperty;
            if (this.panel != null)
            {
                panel.ToolDisplay = (enuLayoutToolDisplay ) this["Display"].Value;
                panel.Size = (Size2i) this["Size"].Value ;
                panel.Location = (Vector2i)this["Location"].Value;
                this.Bind(this.panel);
            }
        }
        private void UnBind(WinLauncherLayoutManager.WinLauncherToolPanelProperty panelProperty)
        {
            panel.ToolDisplayChanged -= new EventHandler(panel_ToolDisplayChanged);
            panel.LocationChanged -= new EventHandler(panel_LocationChanged);
            panel.SizeChanged -= new EventHandler(panel_SizeChanged);
        }
        void panel_SizeChanged(object sender, EventArgs e)
        {
           this.Size = panel.Size;
        }
        void panel_LocationChanged(object sender, EventArgs e)
        {
            this.Location= panel.Location;
        }
    }
}

