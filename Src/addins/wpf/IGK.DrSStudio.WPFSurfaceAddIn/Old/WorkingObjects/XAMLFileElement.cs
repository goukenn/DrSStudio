

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XAMLFileElement.cs
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
file:XAMLFileElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    [WPFElement ("XAMLFile", null, IsVisible =false )]
    public class XAMLFileElement : WPFTransformableElement 
    {
        public override Rectangled GetBound()
        {
            if (this.ParentLayer != null)            
            {
                return this.ParentLayer.GetBound();
            }
            return base.GetBound();
        }
        protected override void InitPath()
        {            
        }
        public static XAMLFileElement Create(UIElement obj)
        {
            //this.CurrentDocument 
            if (obj == null)
                return null;
            XAMLFileElement f = new XAMLFileElement();
            f.Shape = obj;
            return f;
        }
        public sealed class Mecanism : WPFBaseMecanism 
        { 
        }
    }
}

