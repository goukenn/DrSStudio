

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDragDropManager.cs
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
file:IGKD2DDragDropManager.cs
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D
{
    class IGKD2DDragDropManager
    {
        internal const string DATA = "Shell IDList Array";
        [DllImport("shell32.dll")]
        public static extern Int32 SHGetPathFromIDList(
        IntPtr pidl, StringBuilder pszPath);
        [DllImport("shell32.dll")]
        public static extern IntPtr ILCombine(
        IntPtr pidl1, IntPtr pidl2);
        [DllImport("shell32.dll")]
        public static extern void ILFree(
        IntPtr pidl);
        public static string[] GetFileFormIdList(Stream stream)
        {
            List<string> v_list = new List<string>();
            // Copy clipboard data into unmanaged memory.
            MemoryStream data = (MemoryStream)stream;
            byte[] b = data.ToArray();
            IntPtr p = Marshal.AllocHGlobal(b.Length);
            Marshal.Copy(b, 0, p, b.Length);
            // Get number of items.
            UInt32 cidl = (UInt32)Marshal.ReadInt32(p);
            // Get parent folder.
            int offset = sizeof(UInt32);
            IntPtr parentpidl = (IntPtr)((int)p + (UInt32)Marshal.ReadInt32(p, offset));
            StringBuilder path = new StringBuilder(256);
            SHGetPathFromIDList(parentpidl, path);
            // Get subitems.
            for (int i = 1; i <= cidl; ++i)
            {
                offset += sizeof(UInt32);
                IntPtr relpidl = (IntPtr)((int)p + (UInt32)Marshal.ReadInt32(p, offset));
                IntPtr abspidl = ILCombine(parentpidl, relpidl);
                SHGetPathFromIDList(abspidl, path);
                ILFree(abspidl);
                v_list.Add(path.ToString());
            }
            return v_list.ToArray();
        }
    }
}

