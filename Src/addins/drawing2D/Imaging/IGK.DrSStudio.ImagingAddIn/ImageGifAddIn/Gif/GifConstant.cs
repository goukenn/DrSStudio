

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifConstant.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
file:GifConstant.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
namespace IGK.DrSStudio.GifAddIn.Gif
{
    static class GifConstant
    {
        public const string GIF_GLOBAL_COLOR = "GifGlobalColorsEntity";
        public const string GIF_LOCAL_COLOR = "GifLocalColorsEntity";
        public const string GIF_SCREEN_DESC = "GifScreenDescEntity";
        public const string GIF_HEADER = "GifHeaderEntity";
        public const string GIF_DATAENTITY = "GifDataEntity";
        public const string FILE_EXTENSION = "gif";
        internal  static void WriteStruct(Stream mem, object obj)
        {
            int v_size = Marshal.SizeOf(obj);
            byte[] b = new byte[v_size];
            IntPtr v_alloc = Marshal.AllocCoTaskMem(v_size);
            Marshal.StructureToPtr(obj, v_alloc, true);
            Marshal.Copy(v_alloc, b, 0, v_size);
            mem.Write(b, 0, v_size);
            Marshal.FreeCoTaskMem(v_alloc);
        }
        internal static object  ReadStruct(Stream mem, Type t)
        {
            object  obj = null;
            int v_size = Marshal.SizeOf(t);
            byte[] b = new byte[v_size];
            IntPtr v_alloc = Marshal.AllocCoTaskMem(v_size);
            mem.Read(b, 0, v_size);
            Marshal.Copy(b, 0, v_alloc, v_size);
            obj = Marshal.PtrToStructure(v_alloc, t);            
            Marshal.FreeCoTaskMem(v_alloc);
            return obj;
        }
    }
}

