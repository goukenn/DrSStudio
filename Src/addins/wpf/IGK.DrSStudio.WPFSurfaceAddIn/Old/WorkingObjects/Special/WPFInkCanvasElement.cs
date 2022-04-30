

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFInkCanvasElement.cs
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
file:WPFInkCanvasElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.Special
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    [WPFSpecialGroupItem ("InkWPFCanvas", typeof (Mecanism), ImageKey="DE_Pencil")]
    sealed class WPFInkCanvasElement : WPFLayeredElement 
    {
        protected override void InitPath()
        {
        }
        public WPFInkCanvasElement()
        {
            this.Shape = new System.Windows.Controls.InkCanvas();
        }
        public override ICoreControl GetConfigControl()
        {            
            return base.GetConfigControl();
        }
        public override enuParamConfigType GetConfigType()
        {
            return base.GetConfigType();
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            return base.GetParameters(parameters);
        }
        public  class Mecanism : WPFBaseMecanism 
        { 
        }
    }
}

