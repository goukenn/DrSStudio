

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Pogram.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Pogram.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebProjectAddIn
{
    using IGK.DrSStudio.WebProjectAddIn.Css;
    class Pogram
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("...............................................");
            //foreach (var c in IGK.DrSStudio.WebProjectAddIn.Css.CssDefinition.Enums)
            //{
            //    Console.WriteLine(c);
            //}
            //Console.WriteLine("...............................................");
            //foreach(var c in IGK.DrSStudio.WebProjectAddIn.Css.CssDefinition.Properties)
            //{
            //    Console.WriteLine(c);
            //}
            WebRectangle rc = WebRectangle.Empty;
            rc.Height = 45;
            rc.Width = "100%";
            Console.WriteLine(rc);
            Console.ReadLine();
        }
    }
}

