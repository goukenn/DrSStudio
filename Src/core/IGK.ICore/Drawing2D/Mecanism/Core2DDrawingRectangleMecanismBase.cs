

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingRectangleMecanismBase.cs
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
file:Core2DDrawingRectangleMecanismBase.cs
*/
using IGK.ICore;using IGK.ICore.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.Mecanism
{
    public abstract class Core2DDrawingRectangleMecanismBase<T> : 
        Core2DDrawingMecanismBase<T, ICore2DDrawingSurface>
        where T : class ,ICore2DDrawingLayeredElement 
    {
        protected override void OnElementPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {            
            switch ((enu2DPropertyChangedType)e.ID)
            {
                case enu2DPropertyChangedType.MatrixChanged:
                    //for 
                    this.Element.SuspendLayout();
                    this.Element.ResetTransform();
                    this.Element.ResumeLayout();
                    this.InitSnippetsLocation();
                    break;
            }
        }
        protected override void OnElementChanged(CoreWorkingElementChangedEventArgs<T> e)
        {
            base.OnElementChanged(e);
        }
        public Core2DDrawingRectangleMecanismBase()
        {
            this.AllowContextMenu = false;
        }
    }
}

