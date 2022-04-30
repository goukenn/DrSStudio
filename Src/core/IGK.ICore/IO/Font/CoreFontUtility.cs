
using IGK.ICore.IO.Font.Native;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.IO.Font
{
    /// <summary>
    /// core font utility class; for reading ttf data
    /// </summary>
    class CoreFontUtility
    {
        public static readonly int SFNT_VERSION_true = mmoiCount('t', 'r', 'u', 'e');
        public static readonly int SFNT_VERSION_CFF = mmoiCount('O', 'T', 'T', 'O');
        public static readonly int SFNT_VERSION_TT = 0x00010000;
        public static readonly int TABLE_TAG_head = R32(mmoiCount('h', 'e', 'a', 'd'));
        public static readonly int TABLE_TAG_bhed = R32(mmoiCount('b', 'h', 'e', 'd'));

        public static readonly uint SFNT_CHECKSUM_CALC_CONST = 0xB1B0AFBAU;

        public const int HEAD_TABLE_SIZE = 54;
        /* Success */
        public const int eWOFF_ok = 0;
        /* Errors: no valid result returned */
        public const int eWOFF_out_of_memory = 1;       /* malloc or realloc failed */
        public const int eWOFF_invalid = 2;             /* invalid input file (e.g., bad offset) */
        public const int eWOFF_compression_failure = 3; /* error in zlib call */
        public const int eWOFF_bad_signature = 4;       /* unrecognized file signature */
        public const int eWOFF_buffer_too_small = 5;    /* the provided buffer is too small */
        public const int eWOFF_bad_parameter = 6;       /* bad parameter (e.g., null source ptr) */
        public const int eWOFF_illegal_order = 7;       /* improperly ordered chunks in WOFF font */

        /* Warnings: call succeeded but something odd was noticed.
           Multiple warnings may be OR'd together. */
        public const int eWOFF_warn_unknown_version = 0x0100;   /* unrecognized version of sfnt,
                                     not standard TrueType or CFF */
        public const int eWOFF_warn_checksum_mismatch = 0x0200; /* bad checksum, use with caution;
                                     any DSIG will be invalid */
        public const int eWOFF_warn_misaligned_table = 0x0400;  /* table not long-aligned; fixing,
                                     but DSIG will be invalid */
        public const int eWOFF_warn_trailing_data = 0x0800;     /* trailing junk discarded,
                                     any DSIG may be invalid */
        public const int eWOFF_warn_unpadded_table = 0x1000;    /* sfnt not correctly padded,
                                     any DSIG may be invalid */
        public const int eWOFF_warn_removed_DSIG = 0x2000;      /* removed digital signature
                                            while fixing checksum errors */

        public static int mmoiCount(char ch0, char ch1, char ch2, char ch3)
        {
            return ((int)ch0 | ((int)ch1 << 8) | ((int)ch2 << 16) | ((int)ch3 << 24));
        }
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
        internal static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
        internal static byte[] DeflateData(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
       
        internal static font_name GetHead(CoreFontFile file)
        {
            byte[] r = file.getData("name", true);

            font_name tab = new font_name();
            tab = ReadInfo<font_name>(r, 0);
            var l = R16(tab.count);
            var offset = R16(tab.startOffset);
            return tab;
        }

        internal static T ReadInfo<T>(byte[] data, int offset)
        {
            var v_type = typeof(T);
            int t = Marshal.SizeOf(v_type);
            IntPtr v_alloc = Marshal.AllocCoTaskMem(t);
            Marshal.Copy(data, offset, v_alloc, t);
            var v_h = (T)Marshal.PtrToStructure(v_alloc, v_type);
            Marshal.FreeCoTaskMem(v_alloc);
            return v_h;
        }
    }
}
