

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Canvas.cs
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
file:Canvas.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.LayoutAddIn
{
    [LayoutElement ("Canvas", typeof (Mecanism ), ImageKey = "DE_Canvas")]
    /// <summary>
    /// represent a canvas 
    /// </summary>
    public class Canvas : LayoutBase 
    {
        static readonly CoreDependencyProperty  MarginProperty = CoreDependencyProperty.Register(
            "Margin", 
            typeof (Margin),
            typeof (Canvas ),
            new CoreDependencyPropertyMetadata (Margin.Empty)
            );
        static readonly CoreDependencyProperty XProperty = CoreDependencyProperty.Register(
         "X",
         typeof(float),
         typeof(Canvas),
         new CoreDependencyPropertyMetadata(Margin.Empty)
         );
        static readonly CoreDependencyProperty YProperty = CoreDependencyProperty.Register(
       "Y",
       typeof(float),
       typeof(Canvas),
       new CoreDependencyPropertyMetadata(Margin.Empty)
       );
        public Margin Margin { get { return (Margin)this.GetValue(MarginProperty); } set {
            this.SetValue(MarginProperty, value);
        } }
        class Mecanism : Core2DDrawingMecanismBase 
        {
            protected override Core2DDrawingLayeredElement CreateNewElement()
            {
                return base.CreateNewElement();
            }
            protected override void OnMouseDown(WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseDown(e);
            }
            protected override void OnMouseMove(WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseMove(e);
            }
        }
    }
}

