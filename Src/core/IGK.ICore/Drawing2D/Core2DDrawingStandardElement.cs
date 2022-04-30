

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingStandardElement.cs
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
file:Core2DDrawingStandardElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    [AttributeUsage (AttributeTargets.Class , AllowMultiple = false , Inherited = false)]
    public class Core2DDrawingStandardElementAttribute : Core2DDrawingGroupAttribute 
    {
        public override string GroupName
        {
            get { return "Basics"; }
        }
        public override string Environment
        {
            get { return CoreConstant.DRAWING2D_ENVIRONMENT; }
        }
        public override string GroupImageKey
        {
            get { return "Basics"; }
        }
       

        public Core2DDrawingStandardElementAttribute(string name, Type mecanism):base(name,mecanism )
        {
        }
    }
}

