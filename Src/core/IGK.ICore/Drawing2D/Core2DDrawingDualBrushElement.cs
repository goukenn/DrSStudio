

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingDualBrushElement.cs
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
file:Core2DDrawingDualBrushElement.cs
*/
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.Actions;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// abstract drawing layered element that support dual brush definition
    /// </summary>
    public abstract class Core2DDrawingDualBrushElement : 
        Core2DDrawingLayeredElement,
        ICore2DDrawingDualBrushElement , 
        ICoreBrushOwner 
    {
        ICoreBrush m_FillBrush;
        ICorePen m_StrokeBrush;
      
        public override void Dispose()
        {
            this.m_FillBrush.Dispose();
            this.m_StrokeBrush.Dispose();
            base.Dispose();
        }
        public Core2DDrawingDualBrushElement():base()
        {            
        }
        protected override void InitializeElement(){
            base.InitializeElement();
            this.m_FillBrush = new CoreBrush(this);
            this.m_StrokeBrush = new CorePen(this);
            this.m_FillBrush.BrushDefinitionChanged += _BrushDefinitionChanged;
            this.m_StrokeBrush.BrushDefinitionChanged += _BrushDefinitionChanged;
          
        }
        void _BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BrushChanged,
                sender));
        }
        protected override CoreGraphicsPath CreateGraphicsPath()
        {
            CoreGraphicsPath p = new CoreGraphicsPath(this); 
            return p;
        }
        [CoreXMLAttribute ()]
        [CoreXMLDefaultAttributeValue("Type:Solid;Colors:#FFFF;")]
        [Category("Brush")]
        [Browsable(false)]
        public ICoreBrush FillBrush
        {
            get { return m_FillBrush; }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue("Type:Solid;Colors:#F000;")]
        [Category("Brush")]
        [Browsable(false)]
        public ICorePen StrokeBrush
        {
            get { return m_StrokeBrush; }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            switch (mode)
            {
                case enuBrushMode.Stroke:
                    return this.m_StrokeBrush;                    
            }
            return this.m_FillBrush;
        }
        public virtual  ICoreBrush[] GetBrushes()
        {
            return new ICoreBrush[] { 
                this.FillBrush,
                this.StrokeBrush 
            };
        }
        [Browsable(false)]
        public override enuBrushSupport BrushSupport => enuBrushSupport.All;

        //protected override void OnLoadingComplete(EventArgs eventArgs)
        //{
        //    base.OnLoadingComplete(eventArgs);
        //}

        public class Mecanism : Core2DDrawingSurfaceMecanismBase<Core2DDrawingDualBrushElement>
        { 

        }

        
    }
}

