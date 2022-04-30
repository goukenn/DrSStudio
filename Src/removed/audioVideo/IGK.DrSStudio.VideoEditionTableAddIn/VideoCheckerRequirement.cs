

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoCheckerRequirement.cs
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
file:VideoCheckerRequirement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace IGK.DrSStudio.VideoEditionTableAddIn
{
    class VideoCheckerRequirement
    {
        public static bool Check(bool c)
        {
             return true ;
        }
        public static  bool InitAssembly()
        {
#if DEBUG
            string file = @"D:\DRSStudio 7.b Src\IGK.AudioVideoAPI\bin\Debug\drsAVApi.dll";
            if (File.Exists(file))
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFile(file);
                if (asm != null)
                    return true;
            }
#endif
            return false;
        }
    }
}

