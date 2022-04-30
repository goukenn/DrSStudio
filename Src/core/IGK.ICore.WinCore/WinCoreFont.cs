

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreFont.cs
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
file:WinCoreFont.cs
*/
using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinCore
{
    public static  class WinCoreFont
    {
        /// <summary>
        /// create a new font from definition
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public static CoreFont CreateFont(string definition)
        {
            return CoreFont.CreateFrom(definition, null);
        }
        public static CoreFont CreateFont(string name, float size, enuFontStyle style, enuRenderingMode mode)
        {
            if (CoreFont.Exists(name))
            {
                CoreFont c = CoreFont.CreateFont(name, size, style, mode);
                return c;
            }
            else { 
                //default font 
                string[] c = CoreFont.GetInstalledFamilies();
                if ((c!=null) && (c.Length > 0))
                {
                    CoreFont ft = CoreFont.CreateFont(c[0], size, style, 
                        mode);
                    return ft;
                }
               
            }
            return null;
        }

        
        
    }
}

