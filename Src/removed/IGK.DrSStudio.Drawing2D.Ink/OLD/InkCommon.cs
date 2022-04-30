

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: InkCommon.cs
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
file:InkCommon.cs
*/
using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Ink
{
    static class InkCommon
    {
        public static void SaveAsISF(Stream stream, Microsoft.Ink.Ink ink)
        {
            byte[] v_data = ink.Save(PersistenceFormat.InkSerializedFormat);
            stream.Write(v_data, 0, v_data.Length);
        }
        public static void SaveAsISF(Stream stream, InkCollector collector)
        {
            SaveAsISF(stream, collector.Ink);
        }
        public  static string SaveAsISFToString(Microsoft.Ink.Ink ink)
        {
                MemoryStream mem = new MemoryStream ();
                SaveAsISF (mem, ink);
                mem.Seek(0, SeekOrigin.Begin);
                Byte[] t = new Byte[mem .Length ];
            mem.Read (t,  0, t.Length );
                string ht = Convert.ToBase64String(t, Base64FormattingOptions.None);
                mem.Close();
                return ht;
        }
        public static byte[] GetInkDataFromISF(string p)
        {
            Byte[] t = null;
            MemoryStream mem = new MemoryStream();
            StreamWriter sm = new StreamWriter(mem);
            sm.Write(p);
            mem.Seek(0, SeekOrigin.Begin);
            t = new byte[mem.Length];
            mem.Read (t, 0, t.Length );
            return t;
        }
        public static Point ToPixel(this Vector2i pt)
        {
            Point t = new Point();
            t.X = (int)((ICoreUnitPixel)((CoreUnit)(pt.X+"cm"))).Value;
            t.Y = (int)((ICoreUnitPixel)((CoreUnit)(pt.Y+"cm"))).Value;
            return t;
        }
    }
}

