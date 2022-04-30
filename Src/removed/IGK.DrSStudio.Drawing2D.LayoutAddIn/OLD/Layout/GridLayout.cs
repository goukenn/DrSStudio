

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GridLayout.cs
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
file:GridLayout.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.LayoutAddIn.Layout
{
    [LayoutElement("GridView", typeof(Mecanism), ImageKey = "DE_Grid")]
    class GridLayout :  LayoutBase 
    {
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue(enuGridLayoutDisposition.Vertical)]
        public enuGridLayoutDisposition Disposition{
            get{
                return (enuGridLayoutDisposition )this.GetValue (DispositionProperty);
            }
            set{
                this.SetValue (DispositionProperty, value );
            }
        }
         public static readonly CoreDependencyProperty DispositionProperty = CoreDependencyProperty.Register(
         "Disposition",
         typeof(float),
         typeof(GridLayout),
         new CoreDependencyPropertyMetadata(enuGridLayoutDisposition.Vertical )
         );
        public override void Draw(System.Drawing.Graphics g)
        {
 	            base.Draw(g);
        }
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

