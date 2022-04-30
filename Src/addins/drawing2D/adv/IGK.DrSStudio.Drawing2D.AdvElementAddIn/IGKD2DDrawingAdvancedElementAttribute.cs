

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingAdvancedElementAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Core2DDrawingAdvanceItemAttribute.cs
*/
using System; using IGK.ICore; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    [AttributeUsage (AttributeTargets.Class , Inherited=false, AllowMultiple=false )]
    public class IGKD2DDrawingAdvancedElementAttribute : Core2DDrawingStandardElementAttribute  
    {
        public override string GroupName
        {
            get
            {
                return IGKD2DAdvConstant.EXTENSION_GROUPADVANCED;
            }
        }
        public IGKD2DDrawingAdvancedElementAttribute(string name, Type mecanism):base(name , mecanism )
        {
        }
    }
}

