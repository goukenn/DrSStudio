

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ToolEditBrushStyle.cs
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
file:ToolEditBrushStyle.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.PathBrushEditorAddIn.Tools
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using WinUI;
    [IGK.DrSStudio.CoreTools("Tool.PathBrushEditor", ImageKey = PathBrushConstant.IMG_KEY)]
    public sealed class ToolEditBrushStyle : IGK.DrSStudio.Drawing2D.Tools.Core2DDrawingToolBase 
    {
        private static ToolEditBrushStyle sm_instance;
        private ICore2DPathBrushStyleElement  m_Element;
        public ICore2DPathBrushStyleElement Element
        {
            get { return m_Element; }
            set
            {
                if (m_Element != value)
                {
                    if (m_Element != null) UnregisterElementEvent();
                    m_Element = value;
                    if (m_Element != null) RegisterElementEvent();
                    UpdateEditStyle();
                }
            }
        }
        private ToolEditBrushStyle()
        {
        }
private void RegisterElementEvent()
{
    this.m_Element.PropertyChanged += new IGK.DrSStudio.CoreWorkingObjectPropertyChangedEventHandler(m_Element_PropertyChanged);
}
void m_Element_PropertyChanged(object o, IGK.DrSStudio.CoreWorkingObjectPropertyChangedEventArgs e)
{
    if (e.ID == IGK.DrSStudio.enuPropertyChanged.Definition)
    {
        UpdateEditStyle();
    }
}
private void UpdateEditStyle()
{
    if (this.Element != null)
    {
        this.HostedControl.EditStyle = (this.Element.PathBrushStyle != null);
        this.HostedControl.RemoveStyle = (this.Element.PathBrushStyle != null);
    }
    else
    {
        this.HostedControl.EditStyle = false;
        this.HostedControl.RemoveStyle = false;
    }
}
private void UnregisterElementEvent()
{
    this.m_Element.PropertyChanged -= new IGK.DrSStudio.CoreWorkingObjectPropertyChangedEventHandler(m_Element_PropertyChanged);
} 
        public static ToolEditBrushStyle Instance
        {
            get
            {
                return sm_instance;
            }
        }
        new PathBrushEditorHostControl HostedControl
        {
            get {
                return base.HostedControl as PathBrushEditorHostControl;
            }
            set {
                base.HostedControl = value;
            }
        }
        static ToolEditBrushStyle()
        {
            sm_instance = new ToolEditBrushStyle();
        }
        protected override void GenerateHostedControl()
        {
            PathBrushEditorHostControl v_ct =new PathBrushEditorHostControl();
            v_ct.PathBrushStyleChanged += new EventHandler(v_ct_PathBrushStyleChanged);
            v_ct.EditStyle = false;
            v_ct.EditStyleClick += new EventHandler(v_ct_EditStyleClick);
            v_ct.RemoveStyle = false;
            v_ct.RemoveStyleClick += new EventHandler(v_ct_RemoveStyleClick);
            v_ct.CaptionKey = this.Id;          
            this.HostedControl = v_ct;
        }
        void v_ct_RemoveStyleClick(object sender, EventArgs e)
        {
            if (this.Element != null && (this.Element.PathBrushStyle != null))
            {
                this.Element.PathBrushStyle = null;
            }
        }
        void v_ct_EditStyleClick(object sender, EventArgs e)
        {
            if (this.Element != null && (this.Element.PathBrushStyle !=null))
            {
                Workbench.ConfigureWorkingObject(this.Element.PathBrushStyle);
            }
        }
        void v_ct_PathBrushStyleChanged(object sender, EventArgs e)
        {
            if (this.Element != null)
            {
                this.Element.PathBrushStyle = this.HostedControl.PathBrushStyle.Clone () as
                    IGK.DrSStudio.Drawing2D.CorePathBrushStyleBase ;
            }
        }
        protected override void RegisterLayerEvent(IGK.DrSStudio.Drawing2D.ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            layer.SelectedElementChanged += new EventHandler(layer_SelectedElementChanged);
        }
        protected override void UnRegisterLayerEvent(IGK.DrSStudio.Drawing2D.ICore2DDrawingLayer layer)
        {
            layer.SelectedElementChanged -= new EventHandler(layer_SelectedElementChanged);
            base.UnRegisterLayerEvent(layer);
        }
        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
             var t = (sender as ICore2DDrawingLayer).SelectedElements.ToArray();
             if (t.Length == 1)
             {
                 this.Element = t[0] as ICore2DPathBrushStyleElement;
             }
             else
                 this.Element = null;
        }
    }
}

