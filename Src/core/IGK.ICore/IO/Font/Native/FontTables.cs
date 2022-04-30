/*
 * Author: C.A.D BONDJE DOUE
 * Font table definition
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.IO.Font.Native
{
    internal class ttf_native
    {
        internal const int XBYTE = 0x2;
        internal const int YBYTE = 0x4;
        internal const int REPEAT = 0x1 << 3;
        internal const int XSIGNF = 0x10;
        internal const int YSIGNF = 0x20;
        internal const int MASK_ON_CURVE = 0x1;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct font_cmap
    {
        internal ushort version;
        internal ushort numTables;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct font_cmap_table
    {
        internal ushort platformId;
        internal ushort encodingId;
        internal uint offset;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct FontFileHeader
    {
        internal int signature;
        internal ushort numTables;
        internal ushort searchRange, entrySelector, rangeShift;
        #region WOFF
        //internal int signature; //= 0x774F4646 will write 46464f77
        //internal UInt32 flavor;	//The "sfnt version" of the input font.
        //internal UInt32 length;//Total size of the WOFF file.
        //internal UInt16 numTables;//Number of entries in directory of font tables.
        //internal UInt16 reserved;//Reserved; set to zero.
        //internal UInt32 totalSfntSize;//	Total size needed for the uncompressed font data, including the sfnt header, directory, and font tables (including padding).
        //internal UInt16 majorVersion;//	Major version of the WOFF file.
        //internal UInt16 minorVersion;//	Minor version of the WOFF file.
        //internal UInt32 metaOffset;//Offset to metadata block, from beginning of WOFF file.
        //internal UInt32 metaLength;//Length of compressed metadata block.
        //internal UInt32 metaOrigLength;//	Uncompressed size of metadata block.
        //internal UInt32 privOffset;//Offset to private data block, from beginning of WOFF file.
        //internal UInt32 privLength;	//Length of private data block.
        #endregion

        public string Identifier
        {
            get
            {
                return tff_util.mmoiToString(tff_util.R32(signature));
            }
        }
        public int Length
        {
            get
            {
                return 0;// FontUtility.R32((int)length);
            }
        }
        public int TotalSfntLength
        {
            get
            {
                return 0;// FontUtility.R32((int)totalSfntSize);
            }
        }
    }


    [StructLayout(LayoutKind.Sequential)]
    internal struct font_htmx
    {
        internal ushort advancedWidth;
        internal short lsb;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct font_glyh
    {
        internal short numberOfContour;
        internal short xMin;
        internal short yMin;
        internal short xMax;
        internal short yMax;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct font_simpleglyfDesc
    {
        internal ushort[] endPtsOfContours;
        internal ushort instructionLength;
        internal byte[] instructions;
        internal byte[] flags;
        //coordinate
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct font_maxp
    {
        internal ushort reserver1;
        internal ushort reserver2;
        internal ushort numberOfGlyf;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct font_name
    {
        internal ushort format;
        internal ushort count;
        internal ushort startOffset;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct font_name_info
    {
        internal ushort platformID, encodingID, languageID;
        internal ushort nameId;
        internal ushort length;
        internal ushort startOffset;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct font_tabledirectory
    {
        internal UInt32 tag;//	4-byte sfnt table identifier.
        internal UInt32 checksum;//
        internal UInt32 offset;//	Offset to the data, from beginning of WOFF file.
        internal UInt32 length;
        //internal UInt32 compLength;//	Length of the compressed data, excluding padding.
        //internal UInt32 origLength;//	Length of the uncompressed table, excluding padding.
        //internal UInt32 origChecksum;//	Checksum of the uncompressed table.

        public string Identifier
        {
            get
            {
                return tff_util.mmoiToString((int)tag);
            }
        }
        //public bool Compressed
        //{
        //    get
        //    {
        //        return this.compLength != this.origLength;
        //    }
        //}
        public int Length
        {
            get
            {
                return tff_util.R32((int)this.length);
            }
        }

        //public int CompressedLength
        //{
        //    get
        //    {
        //        return FontUtility.R32((int)this.compLength);
        //    }
        //}
        public int Offset
        {
            get
            {
                return tff_util.R32((int)this.offset);
            }
        }
        public override string ToString()
        {
            return GetType().Name + "[" + string.Format("{0}, Length:{1}", this.Identifier, this.Length) + "]";
        }
    }

    internal static class tff_util
    {
        public static string mmoiToString(int v)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((char)(v & 255));
            sb.Append((char)((v >> 8) & 255));
            sb.Append((char)((v >> 16) & 255));
            sb.Append((char)((v >> 24) & 255));
            return sb.ToString();
        }

        internal static short R16(short p)
        {
            return (short)(((p & 0xFF) << 8) +
                     (((p & 0xFF00) >> 8)));
        }
        internal static ushort R16(ushort p)
        {
            return (ushort)(((p & 0xFF) << 8) +
                     (((p & 0xFF00) >> 8)));
        }
        internal static int R32(uint p)
        {
            return R32((int)p);
        }
        internal static int R32(int p)
        {
            return ((p & 0xFF) << 24) +
                   (((p & 0xFF00) >> 8) << 16) +
                   (((p & 0xFF0000) >> 16) << 8) +
                   (int)((p & 0xFF000000) >> 24);
        }
    }
}
