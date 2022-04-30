

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayoutBase.cs
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
file:LayoutBase.cs
*/
using IGK.ICore;using IGK.DrSStudio.Core;
using IGK.DrSStudio.Core.Layers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.LayoutAddIn
{
    /// <summary>
    /// represent a layout base class
    /// </summary>
    public class LayoutBase : Core2DDrawingLayeredElement , ICore2DDrawingLayer 
    {
        Core2DDrawingLayer m_layer;
        /// <summary>
        /// .ctr
        /// </summary>
        public LayoutBase()
        {
            m_layer = new Core2DDrawingLayer();
        }
        public override enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.None; }
        }
        public override void Draw(Graphics g)
        {
        }
        protected override void GeneratePath()
        {
            this.SetPath(null);
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            return null;
        }
        public bool ChangeIdOf(ICore2DDrawingLayeredElement iCore2DDrawingLayeredElement, string p)
        {
            return m_layer.ChangeIdOf(iCore2DDrawingLayeredElement, p);
        }
        public void Clear()
        {
            this.m_layer.Clear();
        }
        public event EventHandler ClippedChanged
        {
            add
            {
                this.m_layer.ClippedChanged += value;
            }
            remove
            {
                this.m_layer.ClippedChanged -= value;
            }
        }
        public event Core2DDrawingElementEventHandler ElementAdded {
            add {
                this.m_layer.ElementAdded += value;
            }
            remove {
                this.m_layer.ElementAdded -= value;
            }
        }
        public event Core2DDrawingElementEventHandler ElementRemoved 
             {
            add {
                this.m_layer.ElementRemoved += value;
            }
            remove {
                this.m_layer.ElementRemoved -= value;
            }
        }
        public event CoreWorkingObjectZIndexChangedHandler ElementZIndexChanged
        {
            add
            {
                this.m_layer.ElementZIndexChanged += value;
            }
            remove
            {
                this.m_layer.ElementZIndexChanged -= value;
            }
        }
        public ICore2DDrawingLayeredElementCollections Elements
        {
            get { return this.m_layer.Elements; }
        }
        public Region GetClippedRegion()
        {
            return this.m_layer.GetClippedRegion();
        }
        public ICore2DDrawingObject GetElementById(string p)
        {
            return this.m_layer.GetElementById(p);
        }
        public System.Drawing.Imaging.ImageAttributes GetImageAttributes()
        {
            return this.m_layer.GetImageAttributes();
        }
        public bool IsClipped
        {
            get { return this.m_layer.IsClipped;  }
        }
        public enuCoreLayerOperation LayerOption
        {
            get
            {
                return this.m_layer.LayerOption;
            }
            set
            {
                this.m_layer.LayerOption = value;
            }
        }
        public float Opacity
        {
            get
            {
                return this.m_layer.Opacity ;
            }
            set
            {
                this.m_layer.Opacity = value;
            }
        }
        public new ICore2DDrawingDocument Parent
        {
            get
            {
                return null;// this.ParentLayer.ParentDocument as ICore2DDrawingDocument;
            }
            set
            {
                //this.ParentLayer.ParentDocument = value;
            }
        }
        public void Select(params ICore2DDrawingLayeredElement[] items)
        {
            this.m_layer.Select(items);
        }
        public event EventHandler SelectedElementChanged {
            add {
                this.m_layer.SelectedElementChanged += value;
            }
            remove {
                this.m_layer.SelectedElementChanged -= value;
            }
        }
        public ICore2DDrawingSelectedElementCollections SelectedElements
        {
            get { return this.m_layer.SelectedElements; }
        }
        public void SetClip(ICore2DDrawingLayeredElement v_element)
        {
            this.m_layer.SetClip(v_element);
        }
        public void Translate(float dx, float dy)
        {
        }
        ICoreLayerElementCollections Core.Layers.ICoreLayer.Elements
        {
            get { return this.m_layer.Elements; }
        }
        public new ICoreLayeredDocument ParentDocument
        {
            get {
                return this.Parent as ICoreLayeredDocument;
            }
        }
        ICoreLayerSelectedElementCollections Core.Layers.ICoreLayer.SelectedElements
        {
            get { return this.m_layer.SelectedElements; }
        }
        private ICoreWorkingObjectIdManager m_IdManager;
        public ICoreWorkingObjectIdManager IdManager
        {
            get
            {
                return this.m_IdManager;
            }
            set
            {
                this.m_IdManager = value;
                OnIdManagerChanged(EventArgs.Empty);
            }
        }
        public event EventHandler IdManagerChanged;
        protected virtual void OnIdManagerChanged(EventArgs e){
            if (IdManagerChanged !=null)
                IdManagerChanged (this, e);
        }
    }
}

