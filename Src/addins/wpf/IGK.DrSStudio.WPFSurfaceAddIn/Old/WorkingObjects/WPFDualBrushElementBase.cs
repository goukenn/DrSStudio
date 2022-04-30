

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFDualBrushElementBase.cs
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
file:WPFDualBrushElementBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    public  abstract class WPFDualBrushElementBase : WPFShapeElement 
    {
        private ICoreBrush m_FillBrush;
        private ICoreBrush m_StrokeBrush;
        [IGK.DrSStudio.Codec .CoreXMLAttribute ()]
        public ICoreBrush StrokeBrush
        {
            get { return m_StrokeBrush; }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public ICoreBrush FillBrush
        {
            get { return m_FillBrush; }
        }
        public WPFDualBrushElementBase()
        {
            this.m_FillBrush = new CoreBrush(this);
            this.m_StrokeBrush = new CorePen(this);
            this.m_FillBrush.BrushDefinitionChanged += new EventHandler(_FillBrush_BrushDefinitionChanged);
            this.m_StrokeBrush.BrushDefinitionChanged += new EventHandler(_StrokeBrush_BrushDefinitionChanged);
        }
        void _StrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.Shape.Stroke = this.StrokeBrush.ToWPFDefinition();
        }
        void _FillBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.Shape.Fill = this.FillBrush .ToWPFDefinition();
        }
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.All;
            }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            switch (mode)
            {
                case enuBrushMode.Stroke :
                    return this.StrokeBrush;
                case enuBrushMode.Fill :
                default :
                    return this.FillBrush;
            }
        }
        public new class Mecanism : WPFShapeElement.Mecanism
        {
            public new WPFDualBrushElementBase Element { get { return base.Element as WPFDualBrushElementBase; }
                set { base.Element = value as WPFDualBrushElementBase; }
            }
            protected override void InitNewCreateElement(WPFElementBase v_element)
            {
                base.InitNewCreateElement(v_element);
                WPFDualBrushElementBase l = v_element as WPFDualBrushElementBase;
                l.m_FillBrush.Copy(this.CurrentSurface.FillBrush);
                l.m_StrokeBrush.Copy(this.CurrentSurface.StrokeBrush );
            }
        }
    }
}

