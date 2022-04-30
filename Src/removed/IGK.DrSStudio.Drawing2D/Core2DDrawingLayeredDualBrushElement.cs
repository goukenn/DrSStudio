

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingLayeredDualBrushElement.cs
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
file:Core2DDrawingLayeredDualBrushElement.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK ;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D.Codec;
    using System.ComponentModel;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D.Actions;
    /// <summary>
    /// 
    /// </summary>
    public abstract class Core2DDrawingLayeredDualBrushElement:
        Core2DDrawingLayeredElement ,
        ICore2DDrawingDualBrushElement
    {
        private ICoreBrush m_FillBrush;//fill brush
        private ICorePen m_StrokeBrush;//stroke brush
        public override  ICoreBrush[] GetBrushes() { 
            return new ICoreBrush[]{
                m_FillBrush ,
                m_StrokeBrush 
            };
        }
        public override enuBrushSupport BrushSupport {
            get {
                return enuBrushSupport.All;
            }
        }
        public override DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
        {
            return base.GetConfigType();
        }
        public override ICoreParameterConfigCollections GetParameters(DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters  =  base.GetParameters(parameters);
            var group = parameters.AddGroup("Brush");
            if ((this.BrushSupport & enuBrushSupport.Fill) == enuBrushSupport.Fill)
            { 
                group.AddActions (new CoreParameterAction("FillBrush", "lb.FillBrushConfig.caption", new EditBrushAction(this.FillBrush, this.BrushSupport )));
            }
            if ((this.BrushSupport & enuBrushSupport.Stroke ) == enuBrushSupport.Stroke )
            {
                group.AddActions(new CoreParameterAction("StrokeBrush", "lb.StrokeBrushConfig.caption", new EditBrushAction(this.StrokeBrush, this.BrushSupport)));
            }
            return parameters;
        }
        ///// <summary>
        ///// get the bound of the current path
        ///// </summary>
        ///// <returns></returns>
        //public Rectanglef GetPathBound()
        //{
        //    CoreGraphicsPath v_path = this.GetPath();
        //    if (v_path == null)
        //        return Rectanglef.Empty;
        //    return v_path.GetBounds();
        //}
        /// <summary>
        /// get global bound
        /// </summary>
        /// <returns></returns>
        public override Rectanglef GetBound()
        {
            CoreGraphicsPath v_path = this.GetPath();
            if (v_path == null)
                return Rectanglef.Empty;
            return v_path.GetBounds();
        }
        /// <summary>
        /// get the blobal surrounding bound
        /// </summary>
        /// <returns></returns>
        public override Rectanglef GetGlobalBound()
        {
            Rectanglef rc = this.GetBound();
            if (this.m_StrokeBrush.Alignment == enuPenAlignment.Inset)
                rc.Inflate(1, 1);
            else
            {
                float w = this.m_StrokeBrush.Width / 2.0f;
                rc.Inflate(w, w);
            }
            if (this.AllowShadow)
            {
                rc = CoreMathOperation.GetBounds(GetShadowPath().GetBounds(),
                    rc);
            }
            return rc;
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (mode == enuBrushMode.Stroke)
                return this.StrokeBrush;
            return this.FillBrush ;
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultStrokeAttribute()]
        [Browsable(false)]
        public ICorePen StrokeBrush
        {
            get { return m_StrokeBrush; }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultFillBrushAttribute()]
        [Browsable(false)]
        public ICoreBrush FillBrush
        {
            get { return m_FillBrush; }
        }
        public override object Clone()
        {
            Core2DDrawingLayeredDualBrushElement c = base.Clone()
                as Core2DDrawingLayeredDualBrushElement;
            c.m_FillBrush.Copy(this.m_FillBrush);
            c.m_StrokeBrush.Copy(this.m_StrokeBrush);
            return c;
        }
        public Core2DDrawingLayeredDualBrushElement()
        { 
            this.m_FillBrush = new CoreBrush (this);
            this.m_StrokeBrush = new CorePen (this);
            this.m_FillBrush.SetSolidColor(Colorf.White);
            this.m_StrokeBrush.SetSolidColor(Colorf.Black);
            this.m_FillBrush.BrushDefinitionChanged += new EventHandler(_BrushDefinitionChanged);
            this.m_StrokeBrush.BrushDefinitionChanged +=new EventHandler(_BrushDefinitionChanged);
        }
        void _BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new CoreWorkingObjectPropertyChangedEventArgs(
                (enuPropertyChanged)enu2DPropertyChangedType.BrushChanged ));
        }
        protected void DisposeBrush()
        {
            if (this.m_StrokeBrush !=null)
            {
                this.m_StrokeBrush .Dispose ();
                this.m_StrokeBrush = null;
            }
            if (this.m_FillBrush !=null)
            {
                this.m_FillBrush .Dispose ();
                this.m_FillBrush = null;
            }
        }
        protected override void WriteAttributes(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteAttributes(xwriter);
        }
        public override bool Contains(Vector2f position)
        {
            CoreGraphicsPath v_p = this.GetPath();
            Pen v_pen = this.StrokeBrush.GetPen();
            if ((v_p != null)&&(v_pen !=null))
            {
                return v_p.IsVisible (position) || v_p.IsOutlineVisible(position,
                    v_pen );
            }
            return false;
        }
        public override void Draw(Graphics g)
        {
            //cheking for visibility
            CoreGraphicsPath v_path = this.GetPath();
            Region rg = g.Clip;
            if ((v_path == null)|| (rg == null))
                return;
            rg.Intersect(this.GetBound());
            if (rg.IsEmpty(g)) 
                return;
            RectangleF v_rc = v_path.GetBounds(g.Transform);
            if ((v_rc.Width < 1) || (v_rc.Height < 1))
                return;
            GraphicsState v_s = g.Save();
            this.SetGraphicsProperty(g);
            Brush v_br = this.FillBrush.GetBrush();
            Pen v_pen = this.StrokeBrush.GetPen();
            if (v_br != null)
            {
                try
                {
                    g.FillPath(v_br, v_path);
                }
                catch
                {
                    CoreLog.WriteDebug("can't fill path");
                }
            }
            if (v_pen != null)
            {        
                try
                {
                    g.DrawPath(v_pen, v_path);
                }
                catch {
                    CoreLog.WriteDebug("can't draw path");
                }
                v_pen.ResetTransform();
            }
            g.Restore(v_s);
        }
        /// <summary>
        /// represnet the base mecanism of dual brush color element
        /// </summary>
        public abstract class Mecanism : Core2DDrawingMecanismBase
        {
            public new Core2DDrawingLayeredDualBrushElement Element {
                get {
                    return base.Element as Core2DDrawingLayeredDualBrushElement;
                }
                set {
                    base.Element = value;
                }
            }
            protected override void InitNewCreateElement(ICore2DDrawingElement element)
            {
                base.InitNewCreateElement(element);
                Core2DDrawingLayeredDualBrushElement v_e = element as Core2DDrawingLayeredDualBrushElement;
                if (v_e != null)
                {
                    v_e.m_FillBrush.Copy(this.CurrentSurface.FillBrush);
                    v_e.m_StrokeBrush.Copy(this.CurrentSurface.StrokeBrush);
                }
            }
            protected override void EndSnippetElement(WinUI.CoreMouseEventArgs e)
            {
                UpdateSnippetElement(e);
                this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                this.InitSnippetsLocation();
             }
        }
        public abstract class Mecanism<T> : Mecanism
            where T : Core2DDrawingLayeredDualBrushElement
        {
            public new T Element
            {
                get
                {
                    return (T)base.Element;
                }
                set
                {
                    base.Element = value;
                }
            }
        }
    }
}

