
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using IGK.ICore;
using IGK.ICore.GraphicModels;
using IGK.ICore.IO.Font.Native;

namespace IGK.ICore.IO.Font
{
    public class CoreFontFile
    {

        private FontFileHeader m_header;
        private List<font_tabledirectory> m_tableDirectory;
        private byte[] m_data;
        private long m_startoffset;
        private string m_fontName;
        private ushort m_numberOfGlyf;
        private CoreFontGlyphInfo[] m_glyfs;
        private ushort[] m_locaTableIndex;
        private Dictionary<ushort, int> m_chars;

        /// <summary>
        /// .ctr
        /// </summary>
        private CoreFontFile()
        {
            m_tableDirectory = new List<font_tabledirectory>();
            m_chars = new Dictionary<ushort, int>();
        }
        
        /// <summary>
        /// get number of char
        /// </summary>
        public int NumberOfGlyphs {
            get {
                return this.m_numberOfGlyf;
            }
        }
        /// <summary>
        /// get the unicode list
        /// </summary>
        /// <returns></returns>
        public ushort[] UnicodeList() {
            return this.m_chars.Keys.ToArray();
        }
        public static CoreFontFile FontParser(string fileName, bool parseglyph)
        {
            if (!File.Exists(fileName))
                return null;
            using (var mem = File.ReadAllBytes(fileName).ToMemoryStream())
            {

                BinaryReader binR = new BinaryReader(mem);// File.Open(fileName, FileMode.Open, FileAccess.Read , FileShare.Read));
                var T = typeof(FontFileHeader);
                byte[] t = new byte[Marshal.SizeOf(T)];
                binR.Read(t, 0, t.Length);
                IntPtr v_alloc = Marshal.AllocCoTaskMem(t.Length);
                Marshal.Copy(t, 0, v_alloc, t.Length);
                FontFileHeader v_h = new FontFileHeader();
                v_h = (FontFileHeader)Marshal.PtrToStructure(v_alloc, T);
                Marshal.FreeCoTaskMem(v_alloc);

                if ((v_h.Identifier != "OTTO") && (v_h.signature != 0x00000100))
                    return null;

                CoreFontFile c = new CoreFontFile();
                c.m_header = v_h;
                int h = CoreFontUtility.R16(c.m_header.numTables);
                //load table directoryWOFFFileTableDirectory
                for (int i = 0; i < h; i++)
                {
                    font_tabledirectory dir = ReadInfo<font_tabledirectory>(binR);
                    c.m_tableDirectory.Add(dir);
                }
                c.m_startoffset = binR.BaseStream.Position;
                byte[] data = new byte[binR.BaseStream.Length - binR.BaseStream.Position];
                binR.Read(data, 0, data.Length);
                c.m_data = data;
                binR.BaseStream.Close();
                binR.Close();
                binR.Dispose();
                c._initFont(parseglyph);
                return c;
            }
            
        }

        private void _initFont(bool parseglyph)
        {
            this.ParseName();            
            this.ParseMaxp();
            this.ParseLocaTable();
            this.ParseCmap();
            if (parseglyph)
            this.ParseGlyph();
            
        }

        private void ParseCmap()
        {
           
            var tab = getData("cmap");
            if (tab == null)
                return;
          this.m_chars = ParseCmap(tab);
           
        }

        /// <summary>
        /// parse the cmap table
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public static Dictionary<ushort, int> ParseCmap(byte[] tab)
        {
            Dictionary<ushort, int> v_chars = new Dictionary<ushort, int>();
            var v_font_cmap = ReadInfo<font_cmap>(tab, 0);
            byte[] v_glyfids = new byte[256];
            //this.Skip(2); // version
            int numTables = CoreFontUtility.R16(v_font_cmap.numTables);//.this.ReadUShort();
            int offset31 = 0;
            int offset30 = 0;
            int offset03 = 0;
            int offset10 = 0;
            int toffset = 4;
            int i = 0;
            for (i = 0; i < numTables; i++)
            {
                font_cmap_table t = ReadInfo<font_cmap_table>(tab, toffset);

                var platformID = CoreFontUtility.R16(ReadInfo<ushort>(tab, toffset));//this.ReadUShort();
                toffset += 2;
                var encodingID = CoreFontUtility.R16(ReadInfo<ushort>(tab, toffset));
                toffset += 2;
                var offset = ReadInfo<uint>(tab, toffset);
                toffset += 4;
                if (platformID == 3 && encodingID == 1)
                {
                    //unicode
                    offset31 = (int)CoreFontUtility.R32(offset);
                }
                if ((platformID == 3) && (encodingID == 0))
                {

                    offset30 = (int)CoreFontUtility.R32(offset);

                }
                if ((platformID == 0) && (encodingID == 3))
                {
                    //Unicode - Version Unicode 2.0 BMP only
                    offset03 = (int)CoreFontUtility.R32(offset);
                }
                if ((platformID == 1) && (encodingID == 0))
                {
                    //macintosh default semantic
                    offset10 = (int)CoreFontUtility.R32(offset);
                }
            }
            if (offset31 == 0)
            {
                //this.Error('No Unicode encoding found');
                //throw new ArgumentException ("no unicode encoding found");
                if (offset30 > 0)
                {

                    //symbol window
                    toffset = offset30;

                    int v_format = CoreFontUtility.R16(ReadInfo<ushort>(tab, toffset));
                    toffset += 2;
                    int v_length = CoreFontUtility.R16(ReadInfo<ushort>(tab, toffset));
                    toffset += 2;
                    switch (v_format)
                    {
                        case 4:
                            offset31 = offset30;
                            //int v_segCountX2 = FontUtility.R16(ReadInfo<ushort>(tab, toffset)) * 2;
                            //toffset += 2;
                            //int v_searchRange = FontUtility.R16(ReadInfo<ushort>(tab, toffset));
                            //toffset += 2;
                            //int v_searchRange = FontUtility.R16(ReadInfo<ushort>(tab, toffset));
                            //toffset += 2;
                            break;
                        default:
                            break;
                    }
                }
                else
                    return null;
            }

            if (offset10 > 0) {
                //read mac data 
                toffset = offset10;
                int v_macformat = CoreFontUtility.R16(ReadInfo<short>(tab, toffset));
                toffset += 2;
                int v_macLength = CoreFontUtility.R16(ReadInfo<short>(tab, toffset));
                toffset += 2;
                int v_maclanguage = CoreFontUtility.R16(ReadInfo<short>(tab, toffset));
                toffset += 2;



                ///read format 0
                if ((v_macformat == 0) && (v_macLength == 262)) {
                    
                    
                    for (int ci = 0; ci < 256; ci++)
                    {
                        v_glyfids[ci] = tab[toffset];
                        toffset++;
                    }
                }
            }




            //
            //startCount = array();
            //endCount = array();
            //idDelta = array();
            //idRangeOffset = array();
            //this.chars = array();
            toffset = offset31;
            //fseek(this.f, this.tables['cmap']+offset31, SEEK_SET);
            int format = CoreFontUtility.R16(ReadInfo<short>(tab, toffset));// this.ReadUShort();
            int len;
            int lang;
            toffset += 2;
            if (format != 4)
                throw new ArgumentException("Unexpected subtable format: '.format");
            //this.Skip(2*2); // length, language
            len = CoreFontUtility.R16(ReadInfo<short>(tab, toffset));
            lang = CoreFontUtility.R16(ReadInfo<short>(tab, toffset + 2));


            toffset += 4;
            int segCount = CoreFontUtility.R16(ReadInfo<ushort>(tab, toffset)) / 2;
            toffset += 2;
            //this.Skip(3*2); // searchRange, entrySelector, rangeShift
            toffset += 6;
            if (segCount == 0)
                return null;
            ushort[] endCount = new ushort[segCount];
            ushort[] startCount = new ushort[segCount];
            ushort[] idDelta = new ushort[segCount];
            ushort[] idRangeOffset = new ushort[segCount];
            ushort[] glyphIdArray = null;
            for (i = 0; i < segCount; i++)
            {
                endCount[i] = CoreFontUtility.R16(ReadInfo<ushort>(tab, toffset));
                toffset += 2;
            }
            //this.Skip(2); // reservedPad
            toffset += 2;
            for (i = 0; i < segCount; i++)
            {
                startCount[i] = CoreFontUtility.R16(ReadInfo<ushort>(tab, toffset));
                toffset += 2;
            }
            for (i = 0; i < segCount; i++)
            {
                idDelta[i] = CoreFontUtility.R16(ReadInfo<ushort>(tab, toffset));
                toffset += 2;
            }
            int rangeOffsetAddressStart = toffset;
            //offset = ftell(this.f);
            for (i = 0; i < segCount; i++)
            {
                idRangeOffset[i] = CoreFontUtility.R16(ReadInfo<ushort>(tab, toffset));
                toffset += 2;
            }
            int gid = 0;
            int rangeOffsetStart = toffset;
            glyphIdArray = new ushort[(tab.Length - toffset)/2];
            for (i = 0; i < glyphIdArray.Length; i++)
            {
                glyphIdArray[i]= CoreFontUtility.R16(ReadInfo<ushort>(tab, toffset));
                toffset+=2;
            }
            for (i = 0; i < segCount; i++)
            {
                var c1 = startCount[i];
                var c2 = endCount[i];
                var d = idDelta[i];
                var ro = idRangeOffset[i];
                //if (ro > 0)
                //{
                //    //   fseek(this.f, offset + 2 * i + ro, SEEK_SET);
                //    toffset = offset31 + (2 * i) + ro;
                //}
                for (var c = c1; c <= c2; c++)
                {
                    if (c == 0xFFFF)
                        break;
                    gid = 0;
                    if (ro > 0)
                    {
                        //true type specification formula
                        var arrayindex = (ro / 2) + (c - c1) - (idRangeOffset.Length);
                        //if (c1 == 40) {

                        //}
                        if (arrayindex >= 0) {
                            //arrayindex--;//= idRangeOffset.Length+1;
                            if ((arrayindex >= 0) && (arrayindex < glyphIdArray.Length))
                            {
                                var pid = glyphIdArray[arrayindex];
                                gid = pid + d;
                                if (gid >= 65536)
                                    gid -= 65536;
                                v_chars[c] = gid;
                            }
                            else
                            {
                                v_chars[c] = 0;
                                //throw new Exception("can't read glypf id");
                            }
                            continue;
                        }

                        //gid = CoreFontUtility.R16(ReadInfo<ushort>(tab, rangeOffsetStart + ro));
                        v_chars[c] = gid;
                        //toffset += 2;
                        continue;
                        //if (gid > 0)
                        //    gid += d;
                    }
                    else
                        gid = c + d;
                    if (gid >= 65536)
                        gid -= 65536;
                    // System.Diagnostics.Debug.WriteLine("gid " + gid);
                    if (gid >= 0)
                        v_chars[c] =  gid;
                    else
                    {

                        CoreLog.WriteDebug($"Can't load gid from font char 0x{IGK.ICore.CoreExtensions.ToBase(c, 16, 4)}");
                        // IGK.ICore.CoreExtensions(c, 16,4);
                        v_chars[c] = 0;
                    }
                }
            }
            return v_chars;
        }

        internal  byte[] getData(font_tabledirectory item, bool decompress = true)
        {
            if (m_tableDirectory.Contains(item))
            {
                byte[] data = new byte[item.Length];
                Array.Copy(this.m_data,
                    (int)Math.Abs(item.Offset - this.m_startoffset),
                    data, 0,
                    data.Length);
              
                return data;
            }
            return null;
        }
        public byte[] getData(string name, bool decompress)
        {
            foreach (var item in this.m_tableDirectory)
            {
                if (item.Identifier == name)
                {
                    return getData(item, decompress);
                }
            }
            return null;
        }
        /// <summary>
        /// get data by identifier name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public byte[] getData(string name)
        {
            foreach (var item in this.m_tableDirectory)
            {
                if (item.Identifier == name)
                {
                    return getData(item);
                }
            }
            return null;
        }
        private static T ReadInfo<T>(BinaryReader binR)
        {
            var v_type = typeof(T);
            byte[] t = new byte[Marshal.SizeOf(v_type)];
            binR.Read(t, 0, t.Length);
            IntPtr v_alloc = Marshal.AllocCoTaskMem(t.Length);
            Marshal.Copy(t, 0, v_alloc, t.Length);
            var v_h = (T)Marshal.PtrToStructure(v_alloc, v_type);
            Marshal.FreeCoTaskMem(v_alloc);
            return v_h;
        }
        private static T ReadInfo<T>(byte[] data, int offset)
        {
            var v_type = typeof(T);
            int t = Marshal.SizeOf(v_type);
            if (offset + t > data.Length)
                return default(T);
            IntPtr v_alloc = Marshal.AllocCoTaskMem(t);

            Marshal.Copy(data, offset, v_alloc, t);
            var v_h = (T)Marshal.PtrToStructure(v_alloc, v_type);
            Marshal.FreeCoTaskMem(v_alloc);
            return v_h;
        }
        private static T ReadInfo<T>(byte[] data, int offset, int size)
        {
            if (offset + size >= data.Length)
                return default(T);

            var v_type = typeof(T);
            IntPtr v_alloc = Marshal.AllocCoTaskMem(size);
            Marshal.Copy(data, offset, v_alloc, size);
            var v_h = (T)Marshal.PtrToStructure(v_alloc, v_type);
            Marshal.FreeCoTaskMem(v_alloc);
            return v_h;
        }
        private void ParseMaxp() {
            var tab = getData("maxp");
            if (tab == null)
                return;
            var p = ReadInfo<font_maxp>(tab, 0);
            this.m_numberOfGlyf = CoreFontUtility.R16 (p.numberOfGlyf);
        }
        private void ParseLocaTable()
        {
            var tab = getData("loca");
            if (tab == null)
                return;
            this.m_locaTableIndex = ParseLocaTable(tab, this.m_numberOfGlyf);
          
        }

        /// <summary>
        /// static parse loca table if found
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="numberofglyf"></param>
        /// <returns></returns>
        public static ushort[] ParseLocaTable(byte[] tab, int numberofglyf) {
            int version = 1; // version of the loca table
            var v_locaTableIndex = new ushort[numberofglyf+1];
            int offset = 0;
            switch (version)
            {
                case 1:
                    for (int i = 0; i <= numberofglyf; i++)
                    {
                        v_locaTableIndex[i] = (ushort)(CoreFontUtility.R16(ReadInfo<ushort>(tab, offset)) * 2);
                        offset += 2;
                    }
                    break;
                default:
                    break;
            }
            return v_locaTableIndex;
        }
        private void ParseGlyph()
        {
            var tab = getData("glyf");
            if (tab == null)
                return;
            var glyf = ParseGlyphData(tab, this.m_numberOfGlyf, this.m_locaTableIndex);


            this.m_glyfs = glyf;
        }

        public static CoreFontGlyphInfo[] ParseGlyphData(byte[] tab, int numberofglyf, ushort[] m_locaTableIndex)
        {

            List<CoreFontGlyphInfo> glyf = new List<CoreFontGlyphInfo>();
            int offset = 0;
            for (int x = 0; x < numberofglyf; x++)
            {
               
                offset = m_locaTableIndex[x];
                CoreFontGlyphInfo c = new CoreFontGlyphInfo();
                glyf.Add(c);

                var p = ReadInfo<font_glyh>(tab, offset);
                short num = CoreFontUtility.R16(p.numberOfContour);
                short v_xmin = CoreFontUtility.R16(p.xMin);
                short v_ymin = CoreFontUtility.R16(p.yMin);
                short v_xmax = CoreFontUtility.R16(p.xMax);
                short v_ymax = CoreFontUtility.R16(p.yMax);


                offset += 5 * 2;//short is 2 bytes 
                c.minX = v_xmin;
                c.minY = v_ymin;
                c.maxX = v_xmax;
                c.maxY = v_ymax;
                //READ GLYPH
                if (num > 0)
                {
                    if (num > 50)
                    {
                        c.m_contours = new int[0];
                        c.m_flags = new byte[0][];
                        c.m_points = new CoreFontGlyphPoint[0];
                        continue;
                    }
                    font_simpleglyfDesc bc = new font_simpleglyfDesc();
                    bc.endPtsOfContours = new ushort[num];
                    int[] v_countourpoints = new int[num];
                    byte[][] flags = new byte[num][];
                    int lastpointIndex = 0;
                    int count = 0;
                    for (int i = 0; i < num; i++)
                    {
                        bc.endPtsOfContours[i] = CoreFontUtility.R16(ReadInfo<ushort>(tab, offset + (i * 2)));
                        v_countourpoints[i] = Math.Abs(bc.endPtsOfContours[i] - lastpointIndex + (i == 0 ? 1 : 0));
                        flags[i] = new byte[v_countourpoints[i]];//flags for the point

                        lastpointIndex = bc.endPtsOfContours[i];//store the last point countour
                        count += v_countourpoints[i]; //update number of point in the glyf
                        
                    }
                    c.m_contours = v_countourpoints;//store point to read
                    offset += num * 2;//advence offset 2 because of short

                    //var k = ReadInfo<ushort>(tab, offset);
                    bc.instructionLength = CoreFontUtility.R16(ReadInfo<ushort>(tab, offset));
                    offset += 2;
                    bc.instructions = new byte[bc.instructionLength];

                    //read instructions
                    for (int i = 0; i < bc.instructionLength; i++)
                    {
                        bc.instructions[i] = ReadInfo<byte>(tab, offset + i);
                    }

                    offset += bc.instructionLength;
                    //read flags
                    bc.flags = new byte[num];//start with fixed number of flags entry

                    ///number of flag per point
                    byte[] v_tflags = new byte[count];
                    byte g = 0;
                    int pts = 0;
                    for (; pts < v_tflags.Length; pts++)
                    {
                        if (offset >= tab.Length)
                            return null;
                         g = tab[offset];
                        offset++;
                        if ((g & ttf_native.REPEAT) == ttf_native.REPEAT)
                        {
                            byte bk = (byte)(g & ~ttf_native.REPEAT);//remove repeat flag
                            var repeats = tab[offset];
                            offset++;
                            v_tflags[pts] = bk;
                            for (int m = 1; m <= repeats && (pts < v_tflags.Length-1); m++)
                            {
                                v_tflags[pts + 1] = bk;
                                pts++;
                            }
                        }
                        else {
                            v_tflags[pts] = g;
                        }
                    }
                    //if (x == 5) {
                    //    string[] m = new string[v_tflags.Length];
                    //    for (int si = 0; si < v_tflags.Length; si++)
                    //    {
                    //        m[si] = (v_tflags[si] & 0x1) == 1 ? "on curve" : "not on curve";
                    //    }
                    //}

                    if (pts < v_tflags.Length) {
                        CoreLog.WriteDebug("no all flags read");
                    }
                    //update flags
                    int v_mc = 0;
                    for (int i = 0; i < v_countourpoints.Length; i++)
                    {                      
                        for (int s = 0; s < v_countourpoints[i]; s++) {
                            flags[i][s] = v_tflags[v_mc];
                            v_mc++;
                        }
                    }

                    #region "OLD"

                    //for (int i = 0; i < num /*bc.flags.Length*/; i++)
                    //{
                    //    //bc.flags[i] = ReadInfo<byte>(tab, offset + i);

                    //    for (int j = 0; (i < num) && (j < flags[i].Length); j++)
                    //    {
                    //        var g = tab[offset];
                    //        flags[i][j] = g;

                    //        if ((g & ~1) == g) {
                    //            //not on curve
                    //        }
                    //        offset++;
                    //        if ((g & ttf_native.REPEAT) == ttf_native.REPEAT)
                    //        {
                    //            var repeats = tab[offset];
                    //            offset++;
                    //            if (j + 1 < v_countourpoints[i])
                    //            {
                    //                //List<byte> cb = new List<byte>();
                    //                //cb.AddRange(flags[i]);
                    //                byte bk = (byte)(g & ~ttf_native.REPEAT);
                    //                flags[i][j] = bk;
                    //                for (int m = 1; m <= repeats; m++)
                    //                {
                    //                    flags[i][j+1] = bk;
                    //                    //if ((m + j) < cb.Count)
                    //                    //{
                    //                    //    cb[m + j] = bk;
                    //                    //}
                    //                    //else
                    //                    //{
                    //                    //    cb.Add(bk);
                    //                    //}
                    //                    j++;
                    //                }
                    //                //change flags list
                    //               // flags[i] = cb.ToArray();
                    //            }

                    //            /*
                    //            for (int m = 1; m <= repeats; m++)
                    //            {
                    //                if (j + m < flags[i].Length)
                    //                    flags[i][j + m] = g;
                    //                else
                    //                {

                    //                    j = 0;
                    //                    i++;
                    //                    repeats = (byte)(repeats - m);
                    //                    m = -1;
                    //                    if (i >= flags.Length)
                    //                        break;
                    //                }
                    //            }*/
                    //            // j += repeats;
                    //        }

                    //    }
                    //    // offset += points[i];
                    //}
                    //offset += bc.flags.Length;
#endregion


                    //read point
                    c.Count = count;
                    int[] v_px = new int[count];
                    int[] v_py = new int[count];
                    int r = 0;
                    int k = 0;
                    c.m_points = new CoreFontGlyphPoint[c.Count];


                    //Read x
                    r = 0;
                    //int toffset = offset;
                    //List<int> svalue = new List<int>();
                    for (int i = 0; i < v_countourpoints.Length; i++)
                    {
                      
                        for (int j = 0; j < flags[i].Length; j++)
                        {
                           
                            var s_flag = flags[i][j];                           
                            //svalue.Add(CoreFontUtility.R16(ReadInfo<short>(tab, toffset)));
                           // toffset+=2;
                            if ((s_flag & ttf_native.XBYTE) == ttf_native.XBYTE)
                            {
                                if ((s_flag & ttf_native.XSIGNF) == ttf_native.XSIGNF)
                                {
                                    r += ReadInfo<byte>(tab, offset);
                                }
                                else
                                {
#pragma warning disable IDE0054 // Use compound assignment
                                    r = r - ReadInfo<byte>(tab, offset);
#pragma warning restore IDE0054 // Use compound assignment
                                }
                                offset++;
                            }
                            else
                            {

                                if ((s_flag & ttf_native.XSIGNF) == ttf_native.XSIGNF)
                                {
                                    //same as previous coordinate
                                    if (k == 0)
                                    {
                                        v_px[k] =  v_xmin; //0

                                    }
                                    else
                                    {
                                        
                                        v_px[k] = r - v_xmin;// v_px[k - 1];
                                    }
                                }
                                else
                                {
                                    r += CoreFontUtility.R16(ReadInfo<Int16>(tab, offset));
                                    offset += 2;
                                    v_px[k] = r - v_xmin;                                   
                                }
                                k++;
                                continue;
                            }
                            v_px[k] = r - v_xmin;
                            k++;
                        }
                    }
                    //Read y
                    r = 0;
                    k = 0;
                    for (int i = 0; i < v_countourpoints.Length; i++)
                    {
                        //r = 0;
                        for (int j = 0; j < flags[i].Length; j++)
                        {
                            var s_flag = flags[i][j];
                            if (k == 5) {
                            }
                            if ((s_flag & ttf_native.YBYTE) == ttf_native.YBYTE)
                            {
                                if ((s_flag & ttf_native.YSIGNF) == ttf_native.YSIGNF)
                                {
                                    r += ReadInfo<byte>(tab, offset);
                                }
                                else
                                {
                                    r += -ReadInfo<byte>(tab, offset);
                                }
                                offset++;
                            }
                            else
                            {
                                if ((s_flag & ttf_native.YSIGNF) != ttf_native.YSIGNF)
                                {
                                    r += CoreFontUtility.R16(ReadInfo<short>(tab, offset));
                                    offset += 2;
                                    v_py[k] = r - v_ymin;                                                                        
                                }
                                else
                                {
                                    //same as previous coordinate
                                    if (k == 0)
                                    {
                                        v_py[k] = v_ymin;                                       
                                    }
                                    else
                                    {
                                        v_py[k] = r- v_ymin;// v_py[k - 1];
                                    }                                   
                                    
                                }
                               
                                k++;
                                continue;
                            }

                            v_py[k] = r - v_ymin;
                            k++;
                        }
                    }
                    //update point
                    for (int i = 0; i < count; i++)
                    {
                        c.m_points[i] = new CoreFontGlyphPoint()
                        {
                            X = v_px[i],
                            Y = v_py[i]
                        };
                    }
                    c.m_flags = flags;
                }
                else if (num < 0)
                {

                    //Debug.riteLine("composite glyph " + x + " :::: " + num + " ::: " + this.m_numberOfGlyf);
                    //break;
                    //composite glyf
                }
            }
            return glyf.ToArray();
        }

        private void ParseName()
	{
            var tab = getData ("name");
            if (tab == null)
                return;
            var p =  ReadInfo<font_name>(tab, 0);
            int count = CoreFontUtility.R16(p.count);
            int stringOffset = CoreFontUtility.R16(p.startOffset);
            for (int i = 0; i < count; i++)
            {
                var v_ip = ReadInfo<font_name_info>(tab, (3*6) + (i+1)*(3*6) );
                if (CoreFontUtility.R16(v_ip.nameId) == 6)
                {
                    string b = Encoding.UTF8.GetString(tab, stringOffset + CoreFontUtility.R16(v_ip.startOffset),
                        CoreFontUtility.R16(v_ip.length)).Replace ('\0'.ToString(), "");
                    this.m_fontName = b;
                    break;
                }
            }

            //this.Seek('name');
            //tableOffset = ftell(this.f);
            //this.postScriptName = '';
            //this.Skip(2); // format
            //count = this.ReadUShort();
            //stringOffset = this.ReadUShort();
            //for(i=0;i<count;i++)
            //{
            //    this.Skip(3*2); // platformID, encodingID, languageID
            //    nameID = this.ReadUShort();
            //    length = this.ReadUShort();
            //    offset = this.ReadUShort();
            //    if(nameID==6)
            //    {
            //        // PostScript name
            //        fseek(this.f, tableOffset+stringOffset+offset, SEEK_SET);
            //        s = this.Read(length);
            //        s = str_replace(chr(0), '', s);
            //        s = preg_replace('|[ \[\](){}<>/%]|', '', s);
            //        this.postScriptName = s;
            //        break;
            //    }
            //}
            //if(this.postScriptName=='')
            //    this.Error('PostScript name not found');
	}


        public string FontName { get { return this.m_fontName; } }

        internal int ExtractGlyphGraphics(int p, out Vector2f[] pts, out byte[] types)
        {
            var c = this.m_glyfs[p];
            if (c!=null)
                return c.ExtractGlyphGraphics(out pts, out types);
            pts = null;
            types = null;
            return 0;
        }
        public object  GetGlyph(int index) {
            return this.m_glyfs[index];
        }
        public object  GetGlyph(char glyph)
        {
            if (this.m_chars.ContainsKey(glyph))
                return this.m_glyfs[this.m_chars[glyph]];
            return null;
        }

        /// <summary>
        /// get glyph from unicode
        /// </summary>
        /// <param name="glyph"></param>
        /// <returns></returns>
        public object  GetGlyphUnicode(ushort glyph)
        {
            if (this.m_chars.Count > 0)
            {
                if (this.m_chars.ContainsKey(glyph))
                {
                    int index = this.m_chars[glyph];
                    if ((index > 0) && (index < this.m_glyfs.Length))
                        return this.m_glyfs[index];
                    else {
                        index = 0;
                        foreach (var item in this.m_chars)
                        {
                            if (item.Key == glyph)
                            {
                                return this.m_glyfs[index];
                            }
                            index++;
                        }
                        
                    }
                }
                return null;
            }
            else {
                return this.m_glyfs[glyph];
            }
        }

    }
}
