using IGK.ICore.Drawing2D;
using IGK.ICore.IO.Font;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


#pragma warning disable IDE1006

namespace IGK.ICore.IO
{
   
    public delegate void WOFFGlyfCallback(int width, int height, Vector2f[] point, byte[] type, int index );


    /// <summary>
    /// represent a woff file manager
    /// </summary>
    public class WOFFFile
    {
        private enuWOFFFIleType m_type;
        private WOFFFileHeaderStruct m_header;


        private object m_data;
        private int m_numberOfGlyhp;
        private ushort[] m_loca;
        private CoreFontGlyphInfo[] m_glyphData;
        private bool m_created;
        public enuWOFFFIleType Type => m_type;
        public int NumberOfGlyf => m_numberOfGlyhp;
        public bool NoCompress { get; private set; }
        public static int TOTAL_LN { get; private set; }


        //internal class WOFFFileInfo
        //{
        //    public WOFFFileInfo()
        //    {
        //    }

        //    public DateTime CreatedAt { get; internal set; }


        //}

        ///<summary>
        ///public .ctr
        ///</summary>
        private WOFFFile()
        {
            m_created = false;
        }



        public static WOFFFile CreateNew(enuWOFFFIleType t) {
            var o = new WOFFFile()
            {
                m_type = t,
                m_created = true
            };
            return o;
        }
        /// <summary>
        /// save the new created woff file
        /// </summary>
        /// <param name="outfile"></param>
        /// <returns></returns>
        public bool Save(string outfile, WOFFFileInfo info) {
            if (!m_created)
                return false;
            try
            {
                this.NoCompress = this.m_type == enuWOFFFIleType.TTF;
                InitUnicodeRange(info);

                WOFFFileTableDirectory[] dics = GetFileTableEntries(info, out byte[] data);// new WOFFFileTableDirectory[0];
                var cT = dics.Length;
                int ln = cT * Marshal.SizeOf(typeof(WOFFFileTableDirectory));
                int offset = 0;
                long p = 0;
                Dictionary<string, KeyValuePair<long, ttf_tabledirectory>> mdir =
                    new Dictionary<string, KeyValuePair<long, ttf_tabledirectory>>();
                //new Dictionary<string, ttf_tabledirectory>();
                KeyValuePair<long, ttf_tabledirectory> loca;
                byte[] bdata = null;
              
                    using (MemoryStream mem = new MemoryStream())
                    {

                        BinaryWriter binW = new BinaryWriter(mem);
                        switch (this.m_type)
                        {
                            case enuWOFFFIleType.TTF:
                                {
                                //save as TTF to memory stream
                                ttf_fileheader c = new ttf_fileheader()
                                {
                                    signature = (int)0x00000100,
                                    numTables = URead((ushort)(cT))// g.numTables;
                                };
                                var exp = Math.Floor(Math.Log((int)cT) / Math.Log(2));
                                    var sR = Math.Pow(2, exp) * 16;
                                    c.entrySelector = (ushort)URead((ushort)exp);
                                    c.searchRange = (ushort)URead((ushort)sR);//(Math.Pow(2, exp)*16);//.Floor(Math.Log((int)cT) / Math.Log(2));
                                    c.rangeShift = (ushort)URead((ushort)((cT * 16) - sR));
                                    c.WriteTo(binW);
                                    p = binW.BaseStream.Position;
                                    offset = (int)p + ln;
                                    ttf_tabledirectory[] dirsi = new ttf_tabledirectory[cT];
                                    int hdata = (Marshal.SizeOf(typeof(ttf_tabledirectory)) * cT);

                                    offset = (int)(binW.BaseStream.Position + hdata);// (Marshal.SizeOf(typeof(ttf_tabledirectory)) * cT));
                                    int soffset = (int)binW.BaseStream.Position;

                                    
                                    binW.Write(new byte[hdata], 0, hdata);//add empty hdata
                                    binW.BaseStream.Seek(soffset, SeekOrigin.Begin);
                                    ttf_tabledirectory header = new ttf_tabledirectory ();
                                    bool fhead = false;
                                    long ttfdiroffset = 0; //store the position of this directory
                                    //write header
                                    for (int i = 0; (i < cT); i++)
                                    {
                                        var ttfdir = dirsi[i];
                                        ttfdir.checksum = dics[i].oriChecksum;
                                        ttfdir.tag = dics[i].tag;
                                        ttfdir.offset = URead( (uint)( offset + dics[i].Offset));

                                        var v_ln = URead(dics[i].origLength);
                                        ttfdir.length = dics[i].origLength;

                                        //int padlength = dics[i].OriginLength % 4;
                                        //if (padlength > 0)
                                        //{

                                        //}
                                        //if (ttfdir.TagName == "post")
                                        //{

                                        //}
                                        //ttfdir.offset = URead((uint)offset);
                                        dirsi[i] = ttfdir;
                                        ttfdiroffset = binW.BaseStream.Position;
                                        ttfdir.WriteTo(binW);
                                        if (ttfdir.TagName == "head") {
                                            header = ttfdir;
                                            fhead = true;
                                        }
                                        mdir.Add(ttfdir.TagName, new KeyValuePair<long, ttf_tabledirectory>(ttfdiroffset, ttfdir));
                                    }
                                    
                                    binW.Write(data, 0, data.Length);
                                    binW.Flush();
                                    //updaqte loca table important
                                    //.... update loca table
                                    loca = mdir["loca"];
                                    var glyphsoffset = URead(mdir["glyf"].Value.length);

                                    mem.Seek(URead(loca.Value.offset), SeekOrigin.Begin);
                                    bdata = new Byte[URead(loca.Value.length)];
                                    mem.Read(bdata, 0, bdata.Length);

                                    mem.Seek(URead(loca.Value.offset), SeekOrigin.Begin);

                                    //before checksum
                                    var vefchecksum = CalculateChecksum(bdata);

                                    int j = 2;
                                    foreach (var item in info.GlyfOffsets)
                                    {
                                        var cj = item/ 2;
                                        bdata[j] = (byte)((cj & 0xff00) >> 8);
                                        j++;
                                        bdata[j] = (byte)(cj & 0x00ff);
                                        j++;
                                    }


                                    glyphsoffset /= 2;
                                    bdata[bdata.Length - 2] = (byte)((glyphsoffset & 0xff00)>>8);  //.Write<ushort>(glyphsoffset, bdata.Length - 2);
                                    bdata[bdata.Length - 1] = (byte)(glyphsoffset & 0x00ff); //0x22 / 2;


                                    vefchecksum = CalculateChecksum(bdata);

                                    var sloca = loca.Value;
                                    sloca.checksum = URead((uint)vefchecksum);

                                    //update loca
                                    mem.Write(bdata, 0, bdata.Length);
                                    mem.Seek(loca.Key, SeekOrigin.Begin);
                                    sloca.WriteTo(binW);
                                    //....

                                    updateTableOperation(info, binW , mdir);

                                    if (fhead)
                                    {
                                        mem.Seek(0, SeekOrigin.Begin);
                                        var off = URead(header.offset);
                                        var cm = (Int32)(0xB1B0AFBA - CalculateChecksum(mem.ToArray()));
                                        mem.Seek(off + 8, SeekOrigin.Begin);
                                        binW.Write((UInt32)URead(cm));
                                        //mem.Seek(off, SeekOrigin.Begin);
                                        //update table checksum
                                        //mem.Seek(URead(header.offset)
                                    }
                                }
                                
                                break;
                            default:
                                {

                                //build woff file header
                                WOFFFileHeaderStruct h = new WOFFFileHeaderStruct()
                                {
                                    signature = SIGN_WOFF,
                                    flavor = FLAVOR_true// OTTO;//flavor one. one not valid
                                };
                                //write header
                                //h.flavor = FLAVOR_OTTO;
                                h.WriteTo(binW);

                                //NoCompress = true;

                                ////CoreByteArrayExtension.WriteTo(h, binW);

                                //write additional  header
                                WOFFFileHeaderData c = new WOFFFileHeaderData()
                                {
                                    majorVersion = URead((ushort)1),// 0x0100;
                                    minorVersion = 0x0000
                                };
                                CoreByteArrayExtension.WriteTo(c, binW);

                                    p = binW.BaseStream.Position;
                                    binW.Write(new byte[ln], 0, ln);
                                    binW.BaseStream.Position = p;
                                    offset = (int)p + ln;
                                    int gsize = 0;// Marshal.SizeOf(typeof(WOFFFileHeaderStruct));
                                    int compressl = 0;
                                    int ol = 0;
                                   
                                    var pt = Marshal.SizeOf(dics[0]);

                                    for (int i = 0; i < dics.Length; i++)
                                    {
                                    
                                        switch (dics[i].TagName)
                                        {

                                            case "head":
                                                //dics[i].oriChecksum = 0;
                                                break;
                                            case "loca":
                                                //update loca table
                                                byte[] v_tvc = new byte[woffData.LocaSize];
                                                var glyphsoffset = woffData.GlyfLength;
                                                int j = 2;
                                                foreach (var item in info.GlyfOffsets)
                                                {
                                                    var cj = item / 2;
                                                    v_tvc[j] = (byte)((cj & 0xff00) >> 8);
                                                    j++;
                                                    v_tvc[j] = (byte)(cj & 0x00ff);
                                                    j++;
                                                }

                                                //not require beause to th calculation
                                                glyphsoffset /= 2;
                                                v_tvc[v_tvc.Length - 2] = (byte)((glyphsoffset & 0xff00) >> 8);  //.Write<ushort>(glyphsoffset, bdata.Length - 2);
                                                v_tvc[v_tvc.Length - 1] = (byte)(glyphsoffset & 0x00ff); //0x22 / 2;


                                                var vefchecksum = CalculateChecksum(v_tvc);

                                                dics[i].oriChecksum = URead((uint)vefchecksum);
                                                v_tvc = !NoCompress  ? Compress(v_tvc) : v_tvc;
                                                //if (v_tvc.Length != dics[i].OriginLength)
                                                //{
                                                //}
                                                //replace data
                                                var coffset = URead(dics[i].offset);
                                                Array.Copy(v_tvc, 0, data, coffset, v_tvc.Length);
                                                break;
                                        }
                                        //    //set the new offset
                                        dics[i].offset = URead((uint)offset);
                                        dics[i].WriteTo(binW);
                                        ol = dics[i].OriginLength;
                                        //if ((s = (ol % 4)) > 0) {
                                        //   ol += 4 - s;
                                        //}
                                        gsize += (int)((ol + 3) & 0xFFFFFFFC);
                                        offset += (int)((dics[i].CompLength + 3) & 0xFFFFFFFC);
                                        compressl += dics[i].CompLength;
                                    }


                                    binW.Write(data, 0, data.Length);
                                    var rdata = Inflate(data);
                                    gsize += (12 + (16 * dics.Length));
                                    //update sizeinfo
                                    binW.BaseStream.Position = 0;
                                    h.numTables = (ushort)URead((ushort)dics.Length);
                                    h.length = (uint)URead((uint)binW.BaseStream.Length);
                                    h.totalSfntSize = URead((uint)(gsize));//( (12 + (16 *11)+2325 )+ 3) & 0xfffffffc) );// binW.BaseStream.Length);
                                    h.WriteTo(binW);
                                    ////CoreByteArrayExtension.WriteTo(h, binW);
                                }
                                break;
                        }
                        binW.Flush();
                        using (FileStream sm = new FileStream(outfile, FileMode.Create))
                        {
                            mem.WriteTo(sm);
                            sm.Flush();
                            sm.Dispose();
                        }
                    //sm.Flush();
                    //sm.Dispose();
                    return true;
                    }
                    //sm.Flush();

                
            }
            catch(Exception ex)
            {
                CoreLog.WriteLine($"[{System.Windows.Forms.Application.ProductName}] - Exception : " + ex.Message);
            }
            return false;
        }

     
        struct CharRange : IRangeList {
            public int From { get; set; }
            public int To { get; set; }
        }

        private void InitUnicodeRange(WOFFFileInfo info)
        {
            List<IRangeList> c = new List<IRangeList>();
            for (int ti = 0; ti < info.CharRanges.Count; ti++)
            {
                c.Add(new CharRange()
                {
                    From = info.CharRanges[ti].MinChar,
                    To = info.CharRanges[ti].MaxChar
                });
            }
            var ut = WOFFRangeUtility.GetUnicodeRangeDefintion(c);

            info.CharRanges.Range1 |= (enuWOFFFileUnicodeRange1) ut[0];// enuWOFFFileUnicodeRange1.BasicLatin | enuWOFFFileUnicodeRange1.LatinSupplement;
            info.CharRanges.Range2 |= (enuWOFFFileUnicodeRange2)ut[1];//.CurrencySymbol | enuWOFFFileUnicodeRange2.PrivateUseArea;
            info.CharRanges.Range3 |= (enuWOFFFileUnicodeRange3)ut[2];//.CurrencySymbol | enuWOFFFileUnicodeRange2.PrivateUseArea;
            info.CharRanges.Range4 |= (enuWOFFFileUnicodeRange4)ut[3];//.CurrencySymbol | enuWOFFFileUnicodeRange2.PrivateUseArea;
            if (info.Panose.FamilyType == enuWOFFFileFamilyType.None)
            {
                if (info.CharRanges.Range2 == 0)
                {
                    info.Panose.FamilyType = enuWOFFFileFamilyType.Latin;
                }
                else
                {
                    info.Panose.FamilyType = enuWOFFFileFamilyType.Pictorial;
                }
            }
        }

        private void updateTableOperation(WOFFFileInfo info, BinaryWriter binw, Dictionary<string, KeyValuePair<long, ttf_tabledirectory>> mdir)
        {
            
            foreach (var item in mdir)
            {
                string v_fname = string.Format("update{0}", item.Key);
                var m = this.GetType().GetMethod(v_fname, BindingFlags.NonPublic | 
                    BindingFlags.Public | 
                    BindingFlags.IgnoreCase| BindingFlags.Instance);
                if (m != null) {
                    
                    var loca = item.Value; // mdir["loca"];
                    var glyphsoffset = URead(loca.Value.length);
                    uint v_dataoffset = URead(loca.Value.offset);
                    binw.BaseStream.Seek(v_dataoffset, SeekOrigin.Begin);

                    Byte[] bdata = new Byte[URead(loca.Value.length)];
                    binw.BaseStream.Read(bdata, 0, bdata.Length);

                    //binw.BaseStream.Seek(URead(loca.Value.offset), SeekOrigin.Begin);


                    Type t = m.GetParameters()?[1].ParameterType;
                    if (t.Name.EndsWith("&")) {
                        t = t.Assembly.GetType(t.FullName.Replace("&",""));//.ReflectedType;
                    }

                    var trans = typeof(CoreByteArrayExtension).GetMethod(nameof(CoreByteArrayExtension.ToStructure)).MakeGenericMethod(t);
                    object d = trans.Invoke(null, new object[] { bdata,0});
                    

                    //object d = null;// bdata.ToStructure;// t.Assembly.CreateInstance(t.FullName);


                    object[] tb = new object[] {
                        info,
                        d
                    };

                    m.Invoke(this, tb);
                    binw.BaseStream.Seek(v_dataoffset, SeekOrigin.Begin);

                    var meth2 = typeof(CoreByteArrayExtension).GetMethod(nameof(CoreByteArrayExtension.WriteTo),
                         BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public,null,
                        new Type[] {                       
                       typeof(object),
                       typeof(BinaryWriter)
                    },null);


                   meth2?.Invoke(null,new object[]{
                        tb[1],
                        binw
                    });

                    binw.BaseStream.Seek(v_dataoffset, SeekOrigin.Begin);
                    binw.BaseStream.Read(bdata, 0, bdata.Length);

                    //calculate the new checksum
                    var checksum = CalculateChecksum(bdata);
                    var tabledir = item.Value.Value;
                    tabledir.checksum = URead((uint)checksum);

                    binw.BaseStream.Seek(item.Value.Key, SeekOrigin.Begin);
                    tabledir.WriteTo(binw);



                    // CoreByteArrayExtension.WriteTo<t>(tb[1], binw);
                    //((ttf_tabledirectory)tb[1]).WriteTo(binw);


                }
            }
            binw.Flush();
        }

        /// <summary>
        /// used to update on woff saving
        /// </summary>
        private class WOFFData
        {
            public int LocaSize { get; internal set; }
            public int GlyfLength { get; internal set; }
        }

        private WOFFFileTableDirectory[] GetFileTableEntries(WOFFFileInfo winf, out byte[] data)
        {
            woffData = new WOFFData();
            List<WOFFFileTableDirectory> g = new List<WOFFFileTableDirectory>();
            data = new byte[0];
            MemoryStream mem = new MemoryStream();
            //add FFTM
            //add_FFTM(winf, g, mem);
            //add_GDEF(winf, g, mem);

            add<font_FFTM>(winf, g, mem);
            //add<font_GDEF1>(winf, g, mem);//*optionnal
            add<font_OS2B>(winf, g, mem);

            //1/ build in ascending order
            add<font_cmap>(winf, g, mem);
            //if (this.m_type == enuWOFFFIleType.TTF ){
            add<font_glyf>(winf, g, mem);
            add<font_head>(winf, g, mem);
           

            //linked
            add<font_hhea>(winf, g, mem);
            add<font_hmtx>(winf, g, mem);



            //if (this.m_type == enuWOFFFIleType.TTF ){
           // if (this.m_type == enuWOFFFIleType.TTF)
                add<font_loca>(winf, g, mem);
            //else
            //{
            //}
            add<font_maxp>(winf, g, mem);
            add<font_name>(winf, g, mem);            
            add<font_post>(winf, g, mem);


            //extra table operation...

          
            mem.Position = 0;
            data = mem.ToArray();
            return g.ToArray();

        }

      

        private byte[] m_addExtraData; //extra data to add info

        private void addInfo(WOFFFileInfo winf, ref font_FFTM inf, ref WOFFFileTableDirectory dir)
        {
            WOFFFileTableDirectory c = dir;
            c.tag = HTag_FFTM;
            c.origLength = URead((uint)Marshal.SizeOf(typeof(font_FFTM)));

            font_FFTM cc = new font_FFTM()
            {
                version = FLAVOR_one
            };
            DateTime t = winf.CreatedAt;
            t.AddSeconds(-WOFFFileInfo.StartTime.Second);

            cc.creationTimeSpan = (ulong)URead((ulong)t.Ticks);
            cc.fontOwnTimeSpan = (ulong)URead((ulong)t.Ticks);
            cc.modifficationTimeSpan = (ulong)URead((ulong)t.Ticks);

            //byte[] rdata = Compress(cc);
            //c.compLength = URead((uint) rdata.Length);
            inf = cc;
            dir = c;
            //mem.Write(rdata, 0, rdata.Length);
            //    g.Add(c);
        }
        private void add<T>(WOFFFileInfo winf, List<WOFFFileTableDirectory> g, MemoryStream mem) where T : struct {
            T c = (T)typeof(T).Assembly.CreateInstance(typeof(T).FullName);
            int diroffset = 0;
            WOFFFileTableDirectory cc = new WOFFFileTableDirectory();
        

            //var v_addInfo = new WOFFFileAddInfo<T>() {
            //    File=winf,
            //    Dir=cc,
            //    Memo=mem,
            //    Table=c
            //};

            //var m = MethodInfo.GetCurrentMethod().DeclaringType.GetMethod("addInfo", BindingFlags.NonPublic | BindingFlags.Instance,null,
            //      new Type[] {
            //          v_addInfo.GetType().MakeByRefType()
            //      },null);
            m_addExtraData = null;
            var m = MethodInfo.GetCurrentMethod().DeclaringType.GetMethod("addInfo", BindingFlags.NonPublic | BindingFlags.Instance, null,
               new Type[] {
                      winf.GetType(),
                      c.GetType().MakeByRefType(),
                      cc.GetType().MakeByRefType ()
               }, null);
            if (m != null)
            {
                object[] ST = new object[] {
                    winf,
                    c,
                    cc
                };
                m.Invoke(this, ST);
                c = (T)ST[1];
                cc = (WOFFFileTableDirectory)ST[2];
            }
            else {
                CoreLog.WriteLine("No AddInfo for " + c.GetType().Name);
            }
            byte[] rdata = null;
            //cc = v_addInfo.Dir;
            if (m_addExtraData != null)
            {
                using (var gm = new MemoryStream()) {
                    BinaryWriter binW = new BinaryWriter(gm);
                    c.WriteTo(binW);
                    binW.Write(m_addExtraData, 0, m_addExtraData.Length);
                    binW.Flush();
                    gm.Seek(0, SeekOrigin.Begin);

                    rdata = gm.ToArray();
                    cc.origLength = URead((uint)rdata.Length);
                    cc.oriChecksum = URead((uint)CalculateChecksum(rdata));                   
                    rdata = Compress(gm.ToArray());
                    cc.compLength = URead((uint)rdata.Length);
                }
            }
            else
            {
                byte[] v_tdata;
                using (var gm = new MemoryStream())
                {
                    BinaryWriter binW = new BinaryWriter(gm);
                    c.WriteTo(binW);
                    binW.Flush();
                    v_tdata = gm.ToArray();
                }

                cc.origLength = URead((uint)Marshal.SizeOf(typeof(T)));
                cc.oriChecksum = URead((uint)CalculateChecksum(v_tdata));
                rdata = Compress(c);
                cc.compLength = URead((uint)rdata.Length);
            }
            diroffset = (int)mem.Position;
            
            //cc.compLength = URead((uint)rdata.Length);
            //cc.oriChecksum = URead((uint)CalculateChecksum(rdata));

            //if (cc.TagName == "post") {
            //    var vvg = CalculateChecksum(rdata);
                    
            //    CoreLog.WriteLine("check sum 0x" + Convert.ToString(cc.oriChecksum, 16));

            //  //  cc.oriChecksum = 0;// (uint)0xacd40954;
            //}

            mem.Write(rdata, 0, rdata.Length);
            int paddLength = (rdata.Length % 4);
            if (paddLength > 0) {
                //diroffset += paddLength;
                paddLength = 4 - paddLength;
                mem.Write(new byte[paddLength], 0, paddLength);
            }
            cc.offset = URead((uint)diroffset);
            g.Add(cc);
            //v_addInfo = null;
        }

        internal static int CalculateChecksum(byte[] rData) {
            int sum = 0;
            //int sum2 = 0;
            int padd = (rData.Length % 4);
            int r = 0;
            //int r2 = 0;
            //int o = 0;
            using (var binR  = new BinaryReader(rData.ToMemoryStream())){
                if (padd > 0) {//append t o end
                    padd = 4 - padd;
                    binR.BaseStream.Seek(0, SeekOrigin.End);
                    binR.BaseStream.Write(new byte[padd], 0, padd);
                    binR.BaseStream.Seek(0, SeekOrigin.Begin);
                  //  rData = ((MemoryStream)binR.BaseStream).ToArray();
                }                
                while (binR.BaseStream.Position < (binR.BaseStream.Length))
                {
                    //r2 = URead(rData.ToStructure<Int32>(o));
                    //o += 4;

                    r =  URead(binR.ReadInt32());
                    sum += r;
                    //sum2 += r2;
                }
            }
            //if (sum != sum2)
            //{
            //}
            return sum;
        }
        private void addInfo(WOFFFileInfo winf, ref font_GDEF1 inf, ref WOFFFileTableDirectory dir)
        {
            inf.attachList = 0;
            inf.marjorVersion = 0x0100;
            inf.minorVersion = 0x0000;
            dir.tag = HTag_GDEF;


            inf.LigCaretList = URead((short)22);
            inf.glyhpClassDef = URead((short)12);
            var i = inf.ToLSB();

        }
        private void addInfo(WOFFFileInfo winf, ref font_hmtx inf, ref WOFFFileTableDirectory dir)
        {//write valid hmtx no advancewidth
            dir.tag = HTag_hmtx;
            int s = Marshal.SizeOf(inf);
            int num = winf.Glyfs.Count;
            bool hm = winf.NumberOfHMetrics > 0;
            inf.advanceWidth = URead((ushort)winf.HGlobalAdvanceWidth);// 100;
            inf.leftSideBearing = URead((short)winf.HGlobalLSB);
           // inf.lsb = 0;
                using (var m = new MemoryStream())
                {
                    BinaryWriter binW = new BinaryWriter(m);
              

                    for (int i = 0; i < winf.NumberOfHMetrics-1; i++)
                    {
                    //if (i == 0) {
                    //    // binW.Write((byte)0);
                    //    binW.Write((ushort)URead(winf.HGlobalLSB));// 0);
                    //continue;
                    //}

                    //if (i == 0)
                    //{
                    //    binW.Write((short)0);
                    //    binW.Write((short)0);
                    //}
                    //else {
                    if (winf.Glyfs.HMetrix.Contains(i))
                    {
                        var cm = winf.Glyfs.HMetrix.GetMetrix(i);
                        binW.Write(URead((ushort)cm.AdvanceWidth));
                        binW.Write(URead((short)cm.LSB));
                    }
                    else
                    {
                        binW.Write(URead((ushort)winf.HGlobalAdvanceWidth));
                        binW.Write(URead((short)winf.HGlobalLSB));
                    }
                    //}
                    continue;

                        //var hmetric = winf.Glyfs.GetHGMetrics(i);

                        //binW.Write(URead((short)hmetric.AdvanceWidth));//advanceWidth
                        //binW.Write(URead((short)hmetric.Lsb));// 0);//lsb

                        //binW.Write((ushort)URead(500));//advanceWidth
                        //binW.Write((ushort)URead(0));// 0);//lsb
                }
                    int j = winf.Glyfs.Count - winf.NumberOfHMetrics;
                    if (j > 0)
                    {
                    if (!hm)
                        j--;
                    var iv = URead((short)-1);
                        for (int i = 0; i < j; i++)
                        {
                            binW.Write(iv);// URead((short)-1)); // Lsb left side bearing
                        }
                    }

                    m_addExtraData = m.ToArray();
                }
                //return;


         
           
                ////inf.LeftSideBearing = 0;
                ////inf.LeftSideBearing = 256;
                //using (var m = new MemoryStream())
                //{

                //    BinaryWriter binW = new BinaryWriter(m);

                //    if (num == 1)
                //    {
                //        binW.Write((short)0);
                //    }
                //    else
                //    {
                //        for (int i = 1; i < num; i++)
                //        {
                //            font_hmtx g = new font_hmtx();
                //            g.WriteTo(binW);
                //            //binW.Write((short)0);
                //        }
                //    }
                //    binW.Flush();
                    
                //int cg = Convert.ToInt32(m.Length % 4);
                //if ( cg > 0) {
                //    m.Write(new byte[cg], 0, cg);
                //}
                //m.Seek(0, SeekOrigin.Begin);
                //m_addExtraData = m.ToArray();
                //}
            
        }


        private void addInfo(WOFFFileInfo winf, ref font_maxp inf, ref WOFFFileTableDirectory dir)
        {
            dir.tag = HTag_maxp;
            inf.majorVersion = URead((ushort)0x0001);
            inf.numberOfGlyf = URead((ushort) winf.Glyfs.Count);
            inf.maxContours=URead((ushort)10); //default max countour
            inf.maxPoints = URead((ushort)40);// default max moint
        }

        private void updateMaxp(WOFFFileInfo winf, ref font_maxp inf) {

            inf.maxPoints = URead((ushort)winf.Glyfs.MaxPoints);
            inf.maxContours = URead((ushort)winf.Glyfs.MaxContours);
        }

        private void updateHead(WOFFFileInfo winf, ref font_head inf)
        {
            inf.xMin = URead((short)winf.Glyfs.MinX);
            inf.yMin = URead((short)winf.Glyfs.MinY);
            inf.xMax = URead((short)winf.Glyfs.MaxX);
            inf.yMax = URead((short)winf.Glyfs.MaxY);
        }

        private void updateHhea(WOFFFileInfo winf, ref font_hhea inf)
        {
            inf.Descender = URead((short)winf.Descender);
            //if not set put the calculated value
            inf.minRightSideBearing = URead((short)(winf.Glyfs.MinX - winf.Glyfs.MaxX));//calculated
            inf.minLeftSideBearing = URead((short)winf.HGlobalLSB);
            inf.xMaxExtent = URead((short)(winf.Glyfs.MaxX - winf.Glyfs.MinX));
            inf.caretSlopeRise = 0x1;
        }
        //var vvs = Decompress(binR, vg).ToStructure<font_maxp>();
        //int numOfGlyhp = URead(vvs.numberOfGlyf);
        //mf.m_numberOfGlyhp = numOfGlyhp;

        private void addInfo(WOFFFileInfo winf, ref font_loca inf, ref WOFFFileTableDirectory dir)
        {
            dir.tag = HTag_loca;
            inf.offsets = 0x0;//empty glyf 1 offset in the glyfdata

            if (this.m_type != enuWOFFFIleType.TTF)
            {
                using (var m = new MemoryStream())
                {
                    BinaryWriter binW = new BinaryWriter(m);
                    for (int i = 1; i <= winf.Glyfs.Count; i++)
                    {
                        if (winf.LocaIndexFormat == enuWOFFLocaIndexFormat.Short)
                        {                            
                            binW.Write(URead((ushort)i));// winf.Glyfs[i].Unicode));
                        }
                        else
                        {
                            binW.Write(URead((Int32)1));// winf.Glyfs[i].Unicode));
                        }
                    }
                    m_addExtraData = m.ToArray();
                }
                woffData.LocaSize = ((winf.LocaIndexFormat == enuWOFFLocaIndexFormat.Short) ? 2 : 4)
                    + m_addExtraData.Length;
                return;
            }

            if (winf.Glyfs.Count > 0) {
                using (var m = new MemoryStream())
                {
                    BinaryWriter binW = new BinaryWriter(m);
                    //because of every format tag . 
                    //add glyhs to target the first glyph
                    //if (
                    if (winf.LocaIndexFormat != enuWOFFLocaIndexFormat.Short)
                    {
                        binW.Write(URead((ushort)1));///0                        
                    }
                    for (int i = 1; i <= winf.Glyfs.Count; i++)
                    {
                        if (i == winf.Glyfs.Count) {
                            //store extra data
                            binW.Write(URead((ushort)0xffff));///1
                            continue;
                        }
                        if (winf.LocaIndexFormat == enuWOFFLocaIndexFormat.Short) {
                            //binW.Write(URead((ushort)1));// 0xffff));// winf.Glyfs[i].Unicode));
                            binW.Write(URead((ushort) 0xffff));// winf.Glyfs[i].Unicode));
                        }
                        else {
                            binW.Write(URead((Int32)1));// winf.Glyfs[i].Unicode));
                        }
                    }
                    m_addExtraData = m.ToArray();// null;// new byte[winf.Glyfs.Count];
                }
            }
            if (this.m_type != enuWOFFFIleType.TTF)
            {
            }

        }
        private void addInfo(WOFFFileInfo winf, ref font_glyf inf, ref WOFFFileTableDirectory dir)
        {
            winf.GlyfOffsets.Clear();
            dir.tag = HTag_glyf;
         
            //the first glyf
            //inf.numberOfContour = URead((short)1);
            //inf.xMin = 0;
            //inf.yMin = 0;
            //inf.xMax = URead((short)winf.BoxSize.Width); 
            //inf.yMax = URead((short)winf.BoxSize.Height);
            int numOfcountour = 0;
            int w = 0;
            int h = 0;
            int v_base = Marshal.SizeOf(inf.GetType());
            //int v_padd;
            int minx, miny;

            using (MemoryStream mem = new MemoryStream())
            {
                BinaryWriter binW = new BinaryWriter(mem);
                for (int i = 0; i <  winf.Glyfs.Count; i++)
                {

                    var gl = winf.Glyfs[i];//>0?i-1:i];
                    minx = 0;
                    miny = 0;
                    var c = GetGflyDesc(gl, winf.BoxSize, ref numOfcountour, ref w, ref h, ref minx, ref miny );

                    winf.Glyfs.UpdateDimension(minx, miny, w, h);
                    winf.Glyfs.MaxContours = (ushort)Math.Max(numOfcountour, winf.Glyfs.MaxContours);
                    if (i == 0)
                    {
                        inf.numberOfContour = URead((short)numOfcountour);
                        inf.xMin = URead((short)minx);
                        inf.yMin = URead((short)miny);
                        inf.xMax = URead((short)(minx + w));
                        inf.yMax = URead((short)(miny + h));
                    }
                    else {
                        var ginf = new font_glyf()
                        {
                            numberOfContour = URead((short)numOfcountour),
                            xMin = URead((short)minx),
                            yMin = URead((short)miny),
                            xMax = URead((short)(minx + w)),
                            yMax = URead((short)(miny + h))
                        };
                        ginf.WriteTo(binW);

                    }

                    c.endPtsOfContours.WriteTo(binW);
                    binW.Write((UInt16)c.instructionLength);
                    if (c.instructionLength > 0)
                    {
                        c.instructions.WriteTo(binW);
                    }
                    c.flags.WriteTo(binW);
                    c.xCoords.WriteTo(binW);
                    c.yCoords.WriteTo(binW);

                    var eoffset = v_base + binW.BaseStream.Position; //expected offset
                    var c_padd = (eoffset % 4);
                    if (c_padd > 0) {
                        c_padd = 4 - c_padd;
                        binW.Write(new byte[c_padd], 0, (int)c_padd);
                        eoffset += c_padd;
                    }
                    //v_padd = Convert.ToInt32((10 + binW.BaseStream.Length) % 4);
                    //if (v_padd > 0)
                    //{
                    //  //  binW.Write(new byte[v_padd], 0, v_padd);
                    //  //!!!firefox and chrome read non padded data remove it
                    //}

                    winf.GlyfOffsets.Add((int)(eoffset));
                }
                int c_off = (int)(10 + binW.BaseStream.Length) % 4;
                if (c_off>0) {
                    binW.Write(new byte[c_off], 0, c_off);
                }
                binW.Flush();
                binW.BaseStream.Seek(0, SeekOrigin.Begin);
                m_addExtraData = mem.ToArray();
            }


            woffData.GlyfLength = m_addExtraData.Length + Marshal.SizeOf(inf);
        }

        private  static font_simpleglyfDesc GetGflyDesc(WOFFFileGlyfData gl, WOFFBoxSize boxsize, 
            ref int numOfcountour,
            ref int w,
            ref int h,
            ref int minx,
            ref int miny)
        {
            //if (gl.Unicode == 60) {

            //}

            var g = new font_simpleglyfDesc();
            numOfcountour = 0;
            var v_glT = gl.Types;
            var gPts = gl.Points;
            List<ushort> lastIdx = new List<ushort>();

            const byte ONCURE_VECTOR = 0x1;
            const byte UBYTEX_VECTOR = 0x2; //use int16 value for x
            const byte UBYTEY_VECTOR = 0x4; //use int16 value for y
            //const byte RPT = 0x8;
            const byte SBYTEX_VECTOR = 0x10;
            const byte SBYTEY_VECTOR = 0x20;
            Dictionary<string, int> cote = new Dictionary<string, int>();
            int c_count = 1;//check if end of countour riched
            for (int i = 0; i < v_glT.Length; i++)
            {
                if (((i > 0) && (i + 1) < v_glT.Length && (v_glT[i + 1] == 0)) || ((v_glT[i] & 0x80) == 0x80))
                {
                    numOfcountour++;
                    lastIdx.Add(URead((ushort)i));//add uread
                    c_count = 0;
                }
                else
                    c_count = 1;
            }
            if (c_count == 1)
            {
                numOfcountour++;// = 1;//consider the last point as the contour close point
                lastIdx.Add((ushort)URead((ushort)(v_glT.Length -1)));
            }

            
                //throw new Exception("the glyf does't have a contour");
            g.endPtsOfContours = lastIdx.ToArray();
            g.instructionLength = 0;
            g.instructions = null;

            byte[] flags = new byte[gPts.Length];
            byte[] xs = new byte[gPts.Length * 2];
            byte[] ys = new byte[gPts.Length * 2];
            int dx = 0;
            int dy = 0;
            int xln = 0;
            int yln = 0;
            //int checkpx = 0;
            //int checkpy = 0;
            w = gl.Size.X;
            h = gl.Size.Y;
            int v_minw = 0;
            int v_minh = 0;
            int v_maxw = 0;
            int v_maxh = 0;
            int v_abs = 0;
            string v_keyc = string.Empty;
            //int curve_p = 0;
            bool v_start = true;
            int c_index = 0;
            int k = 0;
            //int curved = 0;
            bool debug = gPts.Length == 8;
            if (debug) {
            }
            for (int i = 0; i < gPts.Length; i++)
            {

                byte f = 0;// SHORTX_VECTOR | SHORTY_VECTOR;
                short x = (short)Math.Floor(gPts[i].X);
                short y = (short)Math.Floor(gPts[i].Y);
                v_keyc = $"{x}:{y}";
                //if (cote.ContainsKey(v_keyc)) {
                //    throw new Exception($"duplicate cote found at {i} previous index {cote[v_keyc]}");
                //}
                //cote.Add(v_keyc, i);
                if ((v_glT[i] & 0x3) != 0x3)
                {
                    f |= ONCURE_VECTOR;
                    //curve_p = 0;
                }
                if (c_index == i) {
                    //get next index
                    if (k < lastIdx.Count)
                    {
                        c_index = URead(lastIdx[k])+1;
                        k++;
                    }
                    v_start = true;
                }
                //else
                //{
                //    curve_p++;
                //    if (curve_p >= 2) {
                //       // f |= ONCURE_VECTOR;
                //        curve_p = 0;
                //    }
                //}
                //if ((v_glT[i] & 0x3) != 0x3)
                //{
                    //f |= ONCURE_VECTOR;
                //}

                //if (gl.Unicode == 61) {

                //if ((v_glT[i] == 0) && (i > 0)) {
                //    Console.WriteLine($"{x}:{y}");
                //    dx = x;// 90;
                //    dy = y;// -46;
                //}
                //}
                if (i==0) {
                    //int min max point
                    v_minh = v_maxh = y;
                    v_minw = v_maxw = x;
                }
                if (//(i == 0)||((v_glT[i] == 0)
                    v_start
                    && (i > 0)
                    )
                {
                    //dx = -x;
                    //dy = -y;

                    dx = x - dx;
                    dy = y - dy;
                }
                else {
                    //if ((f & ONCURE_VECTOR) == ONCURE_VECTOR)
                    //{
                        v_minh = Math.Min(v_minh, y);
                        v_maxh = Math.Max(v_maxh, y);

                        v_minw = Math.Min(v_minw, x);
                        v_maxw = Math.Max(v_maxw, x);

                   
                        //curved = 0;
                    //    f |= ONCURE_VECTOR;
                    //}
                    //else {
                    //   // curved++;
                    //}
                    dx = x - dx;
                    dy = y - dy;
                }


                v_start = false;


                //x = URead((short)Math.Abs(dx));
                //y = URead((short)Math.Abs(dy));
                v_abs = Math.Abs(dx);
                if (v_abs <= 0xFF)
                {
                    f |= UBYTEX_VECTOR;
                    if (dx >= 0)
                    {
                        f |= SBYTEX_VECTOR;
                    }
                    xs[xln] = (byte)((v_abs & 0xFF));
                    xln += 1;
                }
                else {
                    v_abs = (short)dx;
                    xs[xln] = (byte)((v_abs & 0xFF00) >> 8);
                    xs[xln + 1] = (byte)(v_abs & 0xFF);
                    xln += 2;
                }
                v_abs = Math.Abs(dy);
                if (v_abs <= 0xFF)
                {
                    f |= UBYTEY_VECTOR;
                    if (dy >= 0)
                    {
                        f |= SBYTEY_VECTOR;
                    }
                    ys[yln] = (byte)((v_abs & 0xFF));
                    yln += 1;
                }
                else
                {
                    v_abs = (short)dy;
                    ys[yln] = (byte)((v_abs & 0xFF00) >> 8);
                    ys[yln + 1] = (byte)(v_abs & 0xFF);
                    yln += 2;
                }
                dx = x;
                dy = y;
                flags[i] = f;
              
            }
            /*
             if (i > 0)
             {
                 //if (dx==0)//don't change
                 //{
                 //    f |= SHORTXN_VECTOR;
                 //}
                 //if (dy==0)//don't change
                 //{
                 //    f |= SHORTYN_VECTOR;
                 //}
             }
             checkpx = dx;
             checkpy = dy;
             if ((Math.Abs(dx) < 255))
             {
                 f |= UBYTEX_VECTOR;
             }
             if (Math.Abs(dy) < 255)
             {
                 f |= UBYTEY_VECTOR;
             }

             //if (dx < 0) {
             //    f |= SHORTXN_VECTOR;//negate
             //    dx *= -1;
             //}
             //if (dy < 0) {
             //    f |= SHORTYN_VECTOR;//negate sign
             //    dy *= -1;
             //}



             if ((glT[i] & 3) != 3)
             {
                 f |= ONCURE_VECTOR;
             }

             flags[i] = f;
             x = URead((short)dx);
             y = URead((short)dy);
             if (!(((f & SHORTXN_VECTOR) == SHORTXN_VECTOR) &&
                   ((f & ~UBYTEX_VECTOR) > 0)))
             {
                 //store the data x 
                 if ((f & UBYTEX_VECTOR) == UBYTEX_VECTOR)
                 {
                     xs[xln] = (byte)((x & 0xFF00) >> 8);
                     xln += 1;
                 }
                 else
                 {
                     xs[xln] = (byte)((x & 0xFF00) >> 8);
                     xs[xln + 1] = (byte)((x & 0xFF));
                     xln += 2;
                 }
             }
             else
             {
                 CoreLog.WriteDebug("Skip X Data : " + i);
                 xln += ((f & UBYTEX_VECTOR) == UBYTEX_VECTOR) ? 1 : 2;
             }
             if (!(((f & SHORTYN_VECTOR) == SHORTYN_VECTOR) &&
                     ((f & ~UBYTEY_VECTOR) > 0)))
             {
                 //store the data y
                 if ((f & UBYTEY_VECTOR) == UBYTEY_VECTOR)
                 {
                     ys[yln] = (byte)((y & 0xFF00) >> 8);
                     yln += 1;
                 }
                 else
                 {

                     ys[yln] = (byte)((y & 0xFF00) >> 8);
                     ys[yln + 1] = (byte)((y & 0xFF));
                     yln += 2;
                 }
             }
             else
             {
                 CoreLog.WriteDebug("Skip Y Data : " + i);
                 yln += ((f & UBYTEY_VECTOR) == UBYTEY_VECTOR) ? 1 : 2;
             }

         }
            flags[i] = ONCURE_VECTOR | UBYTEX_VECTOR | UBYTEY_VECTOR;
            //xs[i] =(byte)(i==0?10: (byte)(10+(i*3)));
            //ys[i] =(byte)( i == 0 ? 10 : (byte)(10+(i*4)));
            if (i == 3)
                flags[i] |= SBYTEX_VECTOR;
            xln++;
            yln++;
        }
        xs[0] = 0;
        xs[1] = 90;
        xs[2] = 5;
        xs[3] = 10;//-100


        ys[0] = 0;
        ys[1] = 10;
        ys[2] = 90;
        ys[3] = 0;




        //   int t = Marshal.SizeOf(g);

        if (gl.Size.Equals(Vector2i.Zero))
        {
            w = boxsize.Width;
            h = boxsize.Height;
        }
        else {
            w = gl.Size.X;
            h = gl.Size.Y;
        }

        */

            g.flags = flags;
            g.xCoords = xs.Length > xln ? xs.Slice(0, xln) : xs;
            g.yCoords = ys.Length > yln ? ys.Slice(0, yln) : ys;

            w = Math.Abs(v_maxw - v_minw);
            h = Math.Abs(v_maxh - v_minh);
          
            minx = v_minw; //E1112
            miny = v_minh;

            return g;
            /*
            numOfcountour = 0x1;
            ////g.endPtsOfContours =
            g.instructionLength = 0;
            g.endPtsOfContours = new ushort[] {URead((ushort)3) };
            g.flags = new byte[] { 0x37, 0x37, 0x07 | 0x10 , 0x07 | 0x3};
            //mis oriented
            //g.xCoords = new byte[] { 0, 20, 10};
            //g.yCoords = new byte[] { 0, 10,20};
            //...
            //g.xCoords = new byte[] { 0, 10, 20};
            //g.yCoords = new byte[] { 0, 20,10};

            //offset
            g.xCoords = new byte[] { 0, 10, 90 , 99};
            g.yCoords = new byte[] { 0, 100, 50 ,50};

            w = 100;
            h = 100;
            */

            //     internal ushort[] endPtsOfContours;
            //internal ushort instructionLength;
            //internal byte[] instructions;
            //internal byte[] flags;
            ////coordinate
            //internal byte[] xCoords;
            //internal byte[] yCoords;


        }

        private void addInfo(WOFFFileInfo winf, ref font_cmap inf, ref WOFFFileTableDirectory dir)
        {
            var scount = winf.CharRanges.Count+1;//min 2???? +1 is the extrat segement to end the glyph data
            if (scount < 2)
                throw new ArgumentException("char range not in the correct range count");
            dir.tag = HTag_cmap;
            int numTable = 3;
            //byte[] format_0 = GetCmapData(0, winf);


            //create a demp reserverved cmap data

            BinaryWriter binw = new BinaryWriter(new MemoryStream());
            inf.version = 0;
            inf.numTables = URead((ushort)numTable);
            inf.WriteTo(binw);

            var pos = binw.BaseStream.Position;
            int ln = 8 * numTable;
            binw.Write(new byte[ln], 0, ln);
            long v_sdatapos = binw.BaseStream.Position;
            ln = 0;


            //write format 0
            long offset_format0 = v_sdatapos;
            long format0_length = 0;
            binw.Write((short)0);//format
            binw.Write((short)URead((short)262));//length
            binw.Write((short)0);//language

            byte[] data = new byte[256];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 1;

            }
            data[219] = winf.EuroCharacterIndex;//the euro character
            data[240] = winf.AppleLogoCharacterIndex;
            binw.Write(data, 0, data.Length);
            format0_length = binw.BaseStream.Position - v_sdatapos;

            //write unicode format
            var format4_pos = binw.BaseStream.Position;


            font_camp_format_4 format = new font_camp_format_4()
            {
                format = URead((ushort)4),
                length = 0//we dont know the length of the format
            };
            format.setSegCount((ushort)scount);
            format.WriteTo(binw);
            var endcode = new ushort[scount];
            var startcode = new ushort[scount];
            var idelta = new ushort[scount];
            var rangeOffset = new ushort[scount];

            
            for (int i = 0; i < scount; i++)
            {
                if (i + 1 == scount)
                {
                    //must be 0xFFFF for missing glyf
                    endcode[i] = 0xFFFF;// 0100;
                                        //must be 0xFFFF for missing glyf
                    startcode[i] = 0xFFFF;// 0100;
                    idelta[i] = 0x0100;//add one

                }
                else
                {
                    var ri = winf.CharRanges[i];
                    endcode[i] = URead((ushort)ri.MaxChar);//.LastCharId);// 0x000a);


                    startcode[i] = URead((ushort)(ri.MinChar)); //.FirstCharId));//x0000
                    idelta[i] =  URead((ushort)(ri.ShiftRange));//.RangeShift));// 0xffff));
                    unchecked
                    {
                        if (ri.OffsetRange > 0)
                        {
                            rangeOffset[i] = URead((ushort)//(0x4));// 
                                //((3 + 2) * 2)) //formular is  index (start at 0) + 2bytes for offset * 2 because of storage in short
                                (((ri.OffsetRange-1) + 2) * 2));
                        }
                    }
                }
            }

            //for (int i = 0; i < scount; i++)
            //{
            //    if (i + 1 == scount)
            //    {
                 
            //    }
            //    else
            //    {
         
            //    }
            //}
            endcode.WriteTo(binw);
            binw.Write((UInt16)0);//reserved
            startcode.WriteTo(binw);
            idelta.WriteTo(binw);
            rangeOffset.WriteTo(binw);

            List<ushort> tl = new List<ushort>();
            if (winf.CharRanges.AutoIndex)
            {
                // tl.Add(URead((ushort)1));
                for (int i = 0; i < scount; i++)
                {
                    int lns = URead(endcode[i]) - URead(startcode[i]) + 1;

                    //add 2 triangle
                    //tl.Add(URead((ushort)1));
                    //tl.Add(URead((ushort)1));
                    for (int j = 0; j < lns; j++)
                    {
                        tl.Add(URead((ushort)j));// URead((ushort)(1))); //store the id of the glyf
                    }
                }

            }
            else {
                var t = winf.CharRanges.Indexes;
                for (int i = 0; i < t.Length; i++)
                {
                    tl.Add(URead(t[i]));
                }
            }

            if (tl.Count > 0)
            {
                var glyfIndex = tl.ToArray();
                glyfIndex.WriteTo(binw);
            }
            long tln = binw.BaseStream.Position - format4_pos;// binw.BaseStream.Position - pos;
            //update length
            binw.BaseStream.Seek(format4_pos + 2, SeekOrigin.Begin);
            binw.Write((ushort)URead((ushort)tln));
           

            binw.BaseStream.Position = pos;//reset position

            //order 03-10-31

            var cmap = new font_cmap_info()
            {
                platformID = URead((UInt16)0),
                encodingID = URead((UInt16)3),
                offset = URead((UInt32)(format4_pos))
            };
            //binw.BaseStream.Position = pos;
            cmap.WriteTo(binw);

            cmap = new font_cmap_info()
            {
                platformID = URead((UInt16)1),
                encodingID = URead((UInt16)0),
                offset = URead((UInt32)(offset_format0))
            };
            cmap.WriteTo(binw);

            cmap = new font_cmap_info()
            {
                platformID = URead((UInt16)3),
                encodingID = URead((UInt16)1),
                offset = URead((UInt32)(format4_pos))
            };
            //binw.BaseStream.Position = pos;
            cmap.WriteTo(binw);
            binw.Flush();
            binw.BaseStream.Seek(0, SeekOrigin.Begin);
            m_addExtraData = ((MemoryStream)binw.BaseStream).ToArray();
            //...check cmap data
            //{
            //var g = CoreFontFile.ParseCmap(m_addExtraData);
            //}
            m_addExtraData = m_addExtraData.Slice(4);            
        }

        private byte[] GetCmapData(int v, WOFFFileInfo winf)
        {
            byte[] v_odata = null;
            using (MemoryStream mem = new MemoryStream()) {
                BinaryWriter binw = new BinaryWriter(mem);
                switch (v) {
                    case 0:
                        //font_cmap_info cmap = new font_cmap_info();
                        //cmap.platformID = URead((UInt16)1);
                        //cmap.encodingID = URead((UInt16)0);
                        //cmap.offset = URead((UInt32)(v_sdatapos));
                        //cmap.WriteTo(binw);
                        ////pos += 8;
                        //pos = binw.BaseStream.Position;

                        //binw.BaseStream.Position = v_sdatapos;

                        binw.Write((short)0);//format
                        binw.Write((short)URead((short)262));//length
                        binw.Write((short)0);//language

                        byte[] data = new byte[256];
                        for (int i = 0; i < data.Length; i++)
                        {
                            data[i] = 1;

                        }
                        data[219] = winf.EuroCharacterIndex;//the euro character
                        binw.Write(data, 0, data.Length);
                        break;

                    case 4: //format 4 data

                        var scount = winf.CharRanges.Count + 1; // 2;//min 2
                        font_camp_format_4 format = new font_camp_format_4()
                        {
                            format = URead((ushort)4),
                            length = 0//we dont know the lenth of the format
                        };
                        format.setSegCount((ushort)scount);
                        format.WriteTo(binw);
                        var endcode = new ushort[scount];
                        var startcode = new ushort[scount];
                        var idelta = new ushort[scount];
                        var rangeOffset = new ushort[scount];

                        
                        for (int i = 0; i < scount; i++)
                        {
                            if (i + 1 == scount)
                            {
                                //must be 0xFFFF for missing glyf
                                endcode[i] = 0xFFFF;// 0100;
                            }
                            else
                            {
                                endcode[i] = URead((ushort)winf.CharRanges[i].MaxChar);//.LastCharId);// 0x000a);
                            }
                        }

                        for (int i = 0; i < scount; i++)
                        {
                            if (i + 1 == scount)
                            {
                                //must be 0xFFFF for missing glyf
                                startcode[i] = 0xFFFF;// 0100;
                                idelta[i] = 0x0100;//add one
                            }
                            else
                            {
                                startcode[i] = URead((ushort)(winf.CharRanges[i].MinChar)); //.FirstCharId));//x0000
                                idelta[i] = URead((ushort)(winf.CharRanges[i].ShiftRange));//.RangeShift));// 0xffff));
                            }
                        }
                        endcode.WriteTo(binw);
                        binw.Write(URead((UInt16)0));//reserved
                        startcode.WriteTo(binw);
                        idelta.WriteTo(binw);
                        rangeOffset.WriteTo(binw);

                        List<ushort> tl = new List<ushort>();
                        for (int i = 0; i < scount; i++)
                        {
                            int lns = URead(endcode[i]) - URead(startcode[i]);
                            for (int j = 0; j < lns; j++)
                            {
                                tl.Add(0);
                            }

                        }
                        //tl.Add(4);
                        var glyfIndex = tl.ToArray();// new ushort[] {0};
                        glyfIndex.WriteTo(binw);

                        break;

                }
                mem.Seek(0, SeekOrigin.Begin);
                v_odata =  mem.ToArray();
            }
            return v_odata;
        }

        private void addInfo(WOFFFileInfo winf, ref font_hhea inf, ref WOFFFileTableDirectory dir)
        {
            dir.tag = HTag_hhea;
            inf.majorVersion = URead((ushort)0x0001);
            inf.Ascender = URead(winf.Ascender);                 
            inf.Descender = (short)URead((ushort)winf.Descender);
            inf.advanceWidthMax = URead((ushort)100);

            inf.numberOfHMetrics = URead((ushort) winf.NumberOfHMetrics);//.Glyfs.Count);
            
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct font_simpleglyfDesc
        {            
            internal ushort[] endPtsOfContours;
            internal ushort instructionLength;
            internal byte[] instructions;
            internal byte[] flags;
            //coordinate
            internal byte[] xCoords;
            internal byte[] yCoords;
        }


        private void addInfo(WOFFFileInfo winf, ref font_post inf, ref WOFFFileTableDirectory dir)
        {
            dir.tag = HTag_post;
            inf.majorVersion = URead((ushort)0x0002);
            inf.isFixedPicth = URead((int)(winf.FontProportional ? 0 : 1));


            //because we selected the version 2
            using (MemoryStream mem = new MemoryStream())
            {
                int v_count = winf.Glyfs.Count;
                BinaryWriter binW = new BinaryWriter(mem);
                binW.Write(URead((ushort) winf.Glyfs.Count));
                UInt16[] ids = new UInt16[v_count];
                byte[] names = null;// new Byte[v_count > 258? 0 : Math.Abs(258-v_count)];

                //by default
                int i = 0;
                for (; i < ids.Length && (i < 258); i++)
                {
                    //write standart name index
                    //ids[i] = i;
                    // names[i] = 1;
                    //if (i ==0)
                    //{
                        binW.Write((ushort)1);
                    //}
                    //else
                    //    binW.Write(URead((ushort)(258 )));
                }
                //binW.Write(ids, 0, ids.Length);
                //write name
                List<byte> cb = new List<byte>
                {
                    4
                };
                cb.AddRange(Encoding.ASCII.GetBytes("igk1"));
                cb.Add(4);
                cb.AddRange(Encoding.ASCII.GetBytes("igk2"));
                names = cb.ToArray();
                //for (i=0; i < names.Length; i++)
                //{
                //    //build name
                //    //names[i] = 0;
                //}
                binW.Write(names, 0, names.Length);

                //no name
                //binW.Write(names, 0, names.Length);


                //if private name is in used substract 258 to custom name and used that index for new glyf

                //for (int i = 0; i < winf.Glyfs.Count; i++)
                //{
                //    binW.Write((short)9);// ids[i]);
                //   // names[i] = 1;
                //}

                //binW.Write(names, 0, names.Length);
                binW.Flush();
                mem.Seek(0, SeekOrigin.Begin);
                m_addExtraData = mem.ToArray();
            }
            int s = Marshal.SizeOf(inf) + m_addExtraData.Length;

            //if ((s % 4) > 0) {
            //}
        }

        
        private void addInfo(WOFFFileInfo winf, ref font_OS2B inf, ref WOFFFileTableDirectory dir)
        {
            dir.tag = HTag_OS_2;
            inf.version = 0x0300;//version 3.0
            inf.usBreakChar = 0x2000;//mapped to first glyf

            inf.usWeightClass = URead((ushort)winf.Weight);// 0x400
            inf.usWidthClass = URead((ushort)winf.WidthType);//
            inf.sTypoAscender = URead((short)winf.BoxSize.Height);

            //...value might be the same as hhead
            inf.usWinAscent = URead((ushort)winf.Ascender);
            inf.usWinDescent = URead((ushort)-winf.Descender);

            inf.xAvgCharWidth = URead((short)winf.BoxSize.Width);

            //inf.usFirstCharIndex = URead((ushort)winf.FirstCharId);
            //inf.usLastCharIndex = URead((ushort)winf.LastCharId);

            inf.usFirstCharIndex = URead((ushort)winf.CharRanges.GetFirstChar());// FirstCharId);
            inf.usLastCharIndex = URead((ushort)winf.CharRanges.Max);//.LastCharId);


            inf.panose = new byte[10];
            inf.panose[0] = (byte)winf.Panose.FamilyType;// 2;// = toArray();///latin

            if (winf.Panose.FamilyType == enuWOFFFileFamilyType.Pictorial) {
                inf.panose[2] = 1;//weight
                inf.panose[4] = 1;//aspect ratio
            }

            inf.panose[3] = (byte)winf.Panose.Proportion; //proportion


            inf.ulUnicodeRange1 = URead((uint)winf.CharRanges.Range1);
            inf.ulUnicodeRange2 = URead((uint)winf.CharRanges.Range2);
            inf.ulUnicodeRange3 = URead((uint)winf.CharRanges.Range3);
            inf.ulUnicodeRange4 = URead((uint)winf.CharRanges.Range4);

            inf.ySubscriptXSize = URead((short)winf.SubSize.X);
            inf.ySubscriptYSize = URead((short)winf.SubSize.Y);
            inf.ySuperscriptXSize = URead((short)winf.SuperSize.X);
            inf.ySuperscriptYSize = URead((short)winf.SuperSize.Y);

            inf.yStrikeoutPosition = URead((short)winf.StrikeOutPosition);

            inf.sCapHeight = URead((short)100);
            inf.sxHeight = URead((short)100);
            var i =  inf.ToLSB();
        }
        private void addInfo(WOFFFileInfo winf, ref font_head inf, ref WOFFFileTableDirectory dir)
        {
            dir.tag = HTag_head;
            inf.majorVersion = 0x0100;
            inf.minorVersion = 0;
            inf.created = (long)URead((ulong)winf.GetTimeFromStart(winf.CreatedAt));
            inf.modified = inf.created;
            inf.checkSumAdjustment = 0;//must be update at the end of file calculation
            inf.magicNumber = URead((uint)0x5f0f3cf5);

            inf.flags = URead((ushort)0x0b); //read : http://www.microsoft.com/typography/otspec/head.htm 
            inf.fontDirectionHint = URead((short)2); //read : http://www.microsoft.com/typography/otspec/head.htm 


            inf.xMin = 0;// URead(short.MinValue);
            inf.yMin = 0;// URead(short.MinValue);
            inf.xMax = URead((short)winf.BoxSize.Width); //URead(short.MaxValue); //URead((short)winf.BoxSize.Width);
            inf.yMax = URead((short)winf.BoxSize.Height);// URead(short.MaxValue); //URead((short)winf.BoxSize.Height);
            inf.unitsPerEm = URead((ushort)winf.UnitPerEm);
            inf.lowestRecPPEM = URead((ushort)8);

            
            inf.fontRevision = URead((ushort)1); //font revision must be conform to version string

            inf.indexToLocFormat = (short)URead((ushort)winf.LocaIndexFormat); // 1 for long offset

            var c = inf.ToLSB();
        }
        private void addInfo(WOFFFileInfo winf, ref font_name inf, ref WOFFFileTableDirectory dir)
        {
            dir.tag = HTag_name;
            //var ArrayWithOffset = (int)URead(sm_dic["name"].offset);
            //inf = (font_name)sm_data.ToStructure<font_name>(ArrayWithOffset);
            //return;

            int v_lang = 2; // 0 , 1033
            int sdata = (v_lang*  winf.Names.Count * Marshal.SizeOf(typeof(font_nameRecord)));//data size
            short s =(short)( sdata + Marshal.SizeOf(typeof(font_name)));//offset to start
            inf.version = 0;
            inf.count = (short)URead((short)(winf.Names.Count * v_lang));
            inf.offset = URead(s);
            
            if ((s > 0)&&(sdata>0))
            {

                m_addExtraData = new byte[sdata];
                int offset = 0;
                List<Byte[]> uni_string = new List<Byte[]>();
                List<Byte[]> utf8_sting = new List<Byte[]>();
                using (var mem = m_addExtraData.ToMemoryStream())
                {
                    BinaryWriter binW = new BinaryWriter(mem);


                    var v_rc = winf.Names.GetRecords();
                    List<font_nameRecord> b = new List<font_nameRecord>();

                    for (int i = 0; i < winf.Names.Count; i++)
                    {
                        var m = v_rc[i] as object[];
                        
                        byte[] v_str = GetUnicode(m[2].ToString());
                        byte[] v_utf8 = Encoding.UTF8.GetBytes(m[2].ToString());

                        var j = new font_nameRecord()
                        {

                            //for utf8 strubg
                            platformID = URead((short)0x01),
                            encodingID = 0x0, //
                            languageID = 0x0,// URead((short)0);
                            nameID = URead((short)m[1]),


                            offset = URead((short)offset),
                            length = URead((short)v_utf8.Length)
                        };
                        b.Add(j);
                        //j.WriteTo(binW);

                        offset += v_utf8.Length;

                        j = new font_nameRecord()
                        {

                            //unicode string
                            platformID = 0x0300,//microsoft platform
                            encodingID = 0x0100, //micorosft - Unicode BMP(UCS-2)
                            nameID = URead((short)m[1]),
                            languageID = URead((short)m[0]),
                            offset = URead((short)offset),
                            length = URead((short)v_str.Length)
                        };
                        //j.WriteTo(binW);
                        b.Add(j);




                        utf8_sting.Add(v_utf8);
                        uni_string.Add(v_str);
                        
                        offset += v_str.Length;
                    }

                    b.Sort(new Comparison<font_nameRecord>((x, y) => {
                        if (x.platformID == y.platformID) {
                            return x.nameID.CompareTo(y.nameID);
                        }
                        return x.platformID.CompareTo(y.platformID);
                    }));

                    for (int i = 0; i < b.Count; i++)
                    {
                        b[i].WriteTo(binW);
                    }

                    for (int i = 0; i < uni_string.Count; i++)
                    {
                        mem.Write(utf8_sting[i], 0, utf8_sting[i].Length);
                        mem.Write(uni_string[i], 0, uni_string[i].Length);
                    }
                    mem.Seek(0, SeekOrigin.Begin);
                   // mem.Read(m_addExtraData, 0, m_addExtraData.Length);
                    m_addExtraData = mem.ToArray();
                }

            }
        }

        private byte[] GetUnicode(string v)
        {            
            var m = UTF7Encoding.UTF8.GetBytes(v);
            var tab = new Byte[m.Length *2];
            for (int i = 0; i < m.Length; i++)
            {
                tab[(i*2)] = 0;
                tab[(i*2) + 1] = m[i];
            }
            return tab;
        }



        //private void addInfo(ref WOFFFileAddInfo<font_GDEF1> r)
        //{
        //    var inf = r.Table;
        //    var dir = r.Dir;

        //    inf.attachList = 4;            
        //    dir.tag = HTag_GDEF;


        //    //update;
        //    r.Dir = dir;
        //    r.Table = inf;
        //}
        //private void add_GDEF(WOFFFileInfo winf, List<WOFFFileTableDirectory> g, MemoryStream mem)
        //{

        //}

        //private void add_FFTM(WOFFFileInfo winf, List<WOFFFileTableDirectory> g, MemoryStream mem)
        //{
        //    WOFFFileTableDirectory c = new WOFFFileTableDirectory();
        //    c.tag = HTag_FFTM;
        //    c.origLength = URead((uint)Marshal.SizeOf(typeof(font_FFTM)));

        //    font_FFTM cc = new font_FFTM();
        //    cc.version = FLAVOR_one;

        //    DateTime t = winf.CreatedAt;
        //    t.AddSeconds(-WOFFFileInfo.StartTime.Second);

        //    cc.creationTimeSpan = (ulong)URead((ulong)t.Ticks);
        //    cc.fontOwnTimeSpan = (ulong)URead((ulong)t.Ticks);
        //    cc.modifficationTimeSpan = (ulong)URead((ulong)t.Ticks);

        //    byte[] rdata = Compress(cc);
        //    c.compLength = URead((uint)rdata.Length);
        //    mem.Write(rdata, 0, rdata.Length);
        //    g.Add(c);
        //}
        private static byte[] Inflate(byte[] data) {
            //decompress string data that have been compressed width compress2 function
            byte[] odata = null;
            using (var compressedStream = new MemoryStream(data))
            {
                compressedStream.Position = 0;
                var s = new ZLibNet.ZLibStream(compressedStream, ZLibNet.CompressionMode.Decompress);
                using (var resultStream = new MemoryStream())
                {
                    s.CopyTo(resultStream);
                    odata = resultStream.ToArray();
                }
            }
            return odata;
        }
        private byte[] Compress(byte[] t) {
            if (NoCompress)
                return t;
            //using (var compressedStream = new MemoryStream(t))
            //{
            // compressedStream.Position = 0;
                using (var s = new ZLibNet.ZLibStream(new MemoryStream(), ZLibNet.CompressionMode.Compress))
            {

                using (var resultStream = new MemoryStream())
                {
                    s.Write(t, 0, t.Length);
                    s.Flush();

                    if (s.CompressionRatio > 0)
                    {

                        var od = new byte[s.TotalOut];
                        s.BaseStream.Position = 0;
                        s.BaseStream.Read(od, 0, od.Length);
                        t = od;
                    }
                    //s.CopyTo(resultStream);
                    //t = resultStream.ToArray();
                }
            }
            //s.CopyTo(
            //using (var zipStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            //using (var resultStream = new MemoryStream())
            //{
            //    zipStream.CopyTo(resultStream);
            //    odata = resultStream.ToArray();
            //}
            //}


            return t;
        }
        private byte[] Compress<T>(T cc) where T : struct
        {

            byte[] t = new byte[Marshal.SizeOf(typeof(T))];
            var alloc = Marshal.AllocCoTaskMem(t.Length);
            Marshal.StructureToPtr(cc, alloc, false);
            Marshal.Copy(alloc, t, 0, t.Length);
            Marshal.FreeCoTaskMem(alloc);
            return Compress(t);
           
        }

      
        /// <summary>
        /// convert file woff file to ttf
        /// </summary>
        /// <param name="file"></param>
        /// <param name="v"></param>
        public static bool ConvertToTTF(string file, string v)
        {
            if (!File.Exists(file))
                return false;
            bool v_out = false;
            using (var b = File.Open(file, FileMode.Open))
            {
                using (BinaryReader binR = new BinaryReader(b))
                {


                    int s = Marshal.SizeOf(typeof(WOFFFileHeaderStruct));
                    if (binR.BaseStream.Length > s)
                    {

                        byte[] tab = binR.ReadBytes(s);

                        var g = tab.ToStructure<WOFFFileHeaderStruct>();
                        if (g.signature
                            == SIGN_WOF2)
                        {
                            //woff 2 not implement
                            return false;
                        }

                        if (g.signature != SIGN_WOFF)
                        {
                            return false;

                        }

                        var h = binR.ReadBytes(Marshal.SizeOf(typeof(WOFFFileHeaderData))).ToStructure<WOFFFileHeaderData>();

                        Dictionary<string, WOFFFileTableDirectory> dic = new Dictionary<string, WOFFFileTableDirectory>();
                        var cT = URead(g.numTables);
                        var vs = URead(g.length); //is corrent vs == binR.BaseStream.Length


                        using (BinaryWriter binW = new BinaryWriter(File.Create(v)))
                        {
                            byte[] data = null;

                            ttf_fileheader c = new ttf_fileheader()
                            {
                                signature = (int)0x00000100,
                                numTables = g.numTables
                            };
                            var exp = Math.Floor(Math.Log((int)cT) / Math.Log(2));
                            var sR = Math.Pow(2, exp) * 16;

                            c.entrySelector = (ushort)URead((ushort)exp);
                            c.searchRange = (ushort)URead((ushort)sR);//(Math.Pow(2, exp)*16);//.Floor(Math.Log((int)cT) / Math.Log(2));
                            c.rangeShift = (ushort)URead((ushort)((cT * 16) - sR));
                            c.WriteTo(binW);


                            ttf_tabledirectory[] dirsi = new ttf_tabledirectory[cT];
                            int hdata = (Marshal.SizeOf(typeof(ttf_tabledirectory)) * cT);

                            int offset = (int)(binW.BaseStream.Position + hdata);// (Marshal.SizeOf(typeof(ttf_tabledirectory)) * cT));
                            int soffset = (int)binW.BaseStream.Position;
                            binW.Write(new byte[hdata], 0, hdata);
                            for (int i = 0; (i < cT) && (binR.BaseStream.Position < binR.BaseStream.Length); i++)
                            {
                                var vg = binR.ReadBytes(Marshal.SizeOf(typeof(WOFFFileTableDirectory))).ToStructure<WOFFFileTableDirectory>();
                                dic.Add(vg.TagName, vg);
                                data = Decompress(binR, vg);


                                if (vg.TagName == "OS/2")
                                {
                                    //make it embeded and installable
                                    font_OS2B v_osg = data.ToStructure<font_OS2B>();
                                    v_osg.fsType = 0x0000;
                                    MemoryStream hmem = new MemoryStream();
                                    CoreByteArrayExtension.WriteTo(v_osg, new BinaryWriter(hmem));
                                    hmem.Seek(0, SeekOrigin.Begin);//.Seek(0);
                                    data = hmem.ToArray();
                                }


                                binW.Write(data, 0, data.Length);


                                var m = dirsi[i];
                                m.tag = vg.tag;
                                m.offset = (uint)URead(offset);
                                m.checksum = (uint)vg.oriChecksum;
                                m.length = (uint)URead(data.Length);

                                dirsi[i] = m;
                                int padlength = vg.OriginLength % 4;
                                if (padlength > 0)
                                {//add padding to be a well formed font
                                    offset += data.Length + padlength;
                                    binW.Write(new byte[padlength], 0, padlength);
                                }
                                else
                                {
                                    offset += data.Length;
                                }
                            }

                            binW.BaseStream.Position = soffset;
                            for (int i = 0; i < dirsi.Length; i++)
                            {
                                dirsi[i].WriteTo(binW);
                            }


                            binW.Flush();
                            v_out = true;
                        }



                    }
                }
            }
            return v_out;
        }


        internal static WOFFFile OpenTTF(string f, bool global =false)
        {
            if (global)
            {
                sm_data = File.ReadAllBytes(f);
            }

            int v_numberofHMetrics = 0;
            int v_indexToLocFormat = 0; //short version
            int v_maxglyfs = 0;
            using (BinaryReader binR = new BinaryReader(File.Open(f, FileMode.Open)) {
                 
            })
            {

                ttf_fileheader h = CoreByteArrayExtension.Read<ttf_fileheader>(binR);
                Dictionary<string, ttf_tabledirectory> dics = new Dictionary<string, ttf_tabledirectory>();
                //StringBuilder sb = new StringBuilder();
                for (int i = 0; i < h.NumTables; i++)
                {
                    var g = CoreByteArrayExtension.Read<ttf_tabledirectory>(binR);
                    //Console.WriteLine(g.TagName);
                    //WOFFLog.Log(g);
                    dics[g.TagName] = g;

                    WOFFLog.Log(f + ".log", g);
                    var pos = binR.BaseStream.Position;
                    binR.BaseStream.Position = URead(g.offset);
                    
                    //visit data according to tag



                    switch (g.TagName)
                    {
                        case "name":
                            {
                                byte[] t = new byte[URead(g.length)];
                                binR.Read(t, 0, t.Length);
                                WOFFLog.Log(f + ".log", Encoding.UTF8.GetString(t));

                               // VisitName(new WOFFFile(), new BinaryReader(new MemoryStream(t)), g);

                                var p = t.ToStructure<font_name>().ToLSB();
                                using (var mm = t.ToMemoryStream()){

                                    mm.Seek(Marshal.SizeOf(p), SeekOrigin.Begin );

                                    font_nameRecord rc = new font_nameRecord();

                                    BinaryReader binr = new BinaryReader(mm);
                                    List<font_nameRecord> lst = new List<font_nameRecord>();
                                    int ln = p.count;
                                    int stringOffset = (ln * Marshal.SizeOf(typeof(font_nameRecord))) + Marshal.SizeOf(p);

                                    for (int ig = 0; ig < ln; ig++)
                                    {
                                        rc = binr.Read<font_nameRecord>().ToLSB();
                                        lst.Add(rc);
                                    }
                                    List<string> data = new List<string>();
                                    for (int ii = 0; ii < lst.Count;      ii++)
                                    {
                                        byte[] gd= new byte[lst[ii].length];
                                        mm.Position = stringOffset + lst[ii].offset;
                                        binr.Read(gd, 0, gd.Length);


                                        if (lst[ii].encodingID == 0)
                                        {
                                            data.Add(lst[ii].nameID + " : " + UnicodeEncoding.UTF8.GetString(gd));
                                        }
                                        else
                                        {
                                            Byte[] tbytes = new byte[gd.Length];
                                            //invert order
                                            for (int jj = 0; jj < gd.Length - 1; jj += 2)
                                            {
                                                tbytes[jj] = gd[jj + 1];
                                                tbytes[jj + 1] = gd[jj];
                                            }
                                            data.Add(lst[ii].nameID + " : " + UnicodeEncoding.Unicode.GetString(tbytes));
                                        }

                                        //data.Add( GetString(gd));
                                    }
                                 }
                                //int count = CoreFontUtility.R16(p.count);
                                //int stringOffset = CoreFontUtility.R16(p.startOffset);
                                //for (int i = 0; i < count; i++)
                                //{
                                //    var v_ip = ReadInfo<font_name_info>(tab, (3 * 6) + (i + 1) * (3 * 6));
                                //    if (CoreFontUtility.R16(v_ip.nameId) == 6)
                                //    {
                                //        string b = Encoding.UTF8.GetString(tab, stringOffset + CoreFontUtility.R16(v_ip.startOffset),
                                //            CoreFontUtility.R16(v_ip.length)).Replace('\0'.ToString(), "");
                                //        this.m_fontName = b;
                                //        break;
                                //    }
                                //}



                            }
                            break;
                        case "loca":
                            {
                                byte[] t = new byte[URead(g.length)];
                                binR.Read(t, 0, t.Length);
                                WOFFLog.Log(f + ".log", Encoding.UTF8.GetString(t));
                            }
                            break;
                        case "cmap": {
                                byte[] t = new byte[URead(g.length)];
                                binR.Read(t, 0, t.Length);
                                var m =   CoreFontFile.ParseCmap(t);
                                
                         }
                            break;
                        case "maxp":
                            {
                                binR.BaseStream.Position = URead(g.offset);
                                var ss = CoreByteArrayExtension.Read<font_maxp>(binR);
                                var si = WOFFUtils.ToLSB(ss);
                                v_maxglyfs = si.numberOfGlyf;
                            }
                            break;
                        case "head":
                            {
                                
                                binR.BaseStream.Position = URead(g.offset);
                                var ss = CoreByteArrayExtension.Read<font_head>(binR);
                                var si = WOFFUtils.ToLSB(ss);

                                v_indexToLocFormat = si.indexToLocFormat;
                            }
                            break;
                        case "hhea":
                            {
                                binR.BaseStream.Position = URead(g.offset);
                                var ss = CoreByteArrayExtension.Read<font_hhea>(binR);
                                var si = WOFFUtils.ToLSB(ss);

                                v_numberofHMetrics = si.numberOfHMetrics;
                            }
                            break;
                        case "hmtx":
                            {
                                binR.BaseStream.Position = URead(g.offset);
                                var ss = CoreByteArrayExtension.Read<font_hmtx>(binR);
                                int s = (int) URead(g.length);
                                List<font_hmtx> m = new List<font_hmtx>();
                                List<short> mt = new List<short>();
                                m.Add(ss);
                                for (int ii = 0; ii < v_numberofHMetrics-1; ii++)
                                {
                                    var advw =  URead(CoreByteArrayExtension.Read<ushort>(binR));
                                    var lsb = URead(CoreByteArrayExtension.Read<short>(binR));
                                    m.Add(new font_hmtx() {
                                        advanceWidth=advw,
                                        leftSideBearing = lsb

                                    });
                                   // m.Add(advw);
                                    mt.Add(lsb);
                                }
                                List<short> co = new List<short>();
                                s -= (v_numberofHMetrics * 4);

                                while (s > 0) {
                                    co.Add(URead(CoreByteArrayExtension.Read<short>(binR)));
                                    s -= 2;
                                }

                                //get to know wath is on 

                                //var ss = CoreByteArrayExtension.Read<font_hmtx>(binR);
                                //var si = WOFFUtils.ToLSB(ss);
                                //internal UInt16 lsb;
                                //var gs = CoreByteArrayExtension.Read<UInt16>(binR);
                            
                            }
                            break;
                    }
                    //binR.BaseStream.Dispose();
                    //  sb.AppendLine(g.TagName);

                    binR.BaseStream.Position = pos;
                }
                
                var c = URead(CoreByteArrayExtension.Read<uint>(binR));

                if (global) {
                    sm_dic = dics;
                   
                }
            }
            return null;
        }

        /// <summary>
        /// open a woff file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static WOFFFile Open(string file)
        {
            if (!File.Exists(file))
                return null;


            using (BinaryReader binR = new BinaryReader(File.Open(file, FileMode.Open)))
            {


                int s = Marshal.SizeOf(typeof(WOFFFileHeaderStruct));
                WOFFFile o = new WOFFFile();
                if (binR.BaseStream.Length > s)
                {

                    byte[] tab = binR.ReadBytes(s);

                    var g = tab.ToStructure<WOFFFileHeaderStruct>();

                    if (g.signature == SIGN_WOFF)
                    {
                        o.m_type = enuWOFFFIleType.Woff;
                        o.m_data = binR.ReadBytes(Marshal.SizeOf(typeof(WOFFFileHeaderData))).ToStructure<WOFFFileHeaderData>();

                        Dictionary<string, WOFFFileTableDirectory> dic = new Dictionary<string, WOFFFileTableDirectory>();
                        var cT = URead(g.numTables);

                        var vs = URead(g.length); //is corrent vs == binR.BaseStream.Length

                        TOTAL_LN = 0;
                        //StringBuilder sb = new StringBuilder();
                        for (int i = 0; (i < cT) && (binR.BaseStream.Position < binR.BaseStream.Length); i++)
                        {
                            var vg = binR.ReadBytes(Marshal.SizeOf(typeof(WOFFFileTableDirectory))).ToStructure<WOFFFileTableDirectory>();
                            dic.Add(vg.TagName, vg);


                            Visit(o, vg.TagName, binR, vg);
                        }
                        

                        if (dic.ContainsKey("maxp")) {

                            var vg = dic["maxp"];
                            if (IsCompressed(vg)) {

                                var vvs = Decompress(binR, vg).ToStructure<font_maxp>();
                                int numOfGlyhp = URead(vvs.numberOfGlyf);
                                o.m_numberOfGlyhp = numOfGlyhp;
                            }

                        }
                        if (dic.ContainsKey("cmap"))
                        {

                            var vg = dic["cmap"];

                            byte[] data = Decompress(binR, vg);
                            var vvs = data.ToStructure<font_cmap>();
                            int table = URead(vvs.numTables);
                            //read in data tables

                            //store cmap data
                            File.WriteAllBytes(
                                $"{Path.GetFileNameWithoutExtension(file)}_cmap.data",
                                data);
                        }

                        if (dic.ContainsKey("loca")) {
                            var vg = dic["loca"];

                            byte[] data = Decompress(binR, vg);
                            o.m_loca = CoreFontFile.ParseLocaTable(data, o.m_numberOfGlyhp);

                        }

                        if ((o.m_numberOfGlyhp > 0) && dic.ContainsKey("glyf")) {
                            ReadGlyf(o, Decompress(binR, dic["glyf"]));
                        }
                        
                    }
                    else if (g.signature == SIGN_WOF2)
                    {
                        o.m_type = enuWOFFFIleType.Woff;
                        o.m_data = binR.ReadBytes(Marshal.SizeOf(typeof(WOF2FileHeaderData))).ToStructure<WOF2FileHeaderData>();


                    }
                    else {
                        return null;
                    }
                    o.m_header = g;



                }
                return o;

            }

        }

        private static void Visit(WOFFFile mf, string tagName, BinaryReader binR, WOFFFileTableDirectory vg)
        {
            MethodInfo m = MethodInfo.GetCurrentMethod().DeclaringType.GetMethod("Visit" + tagName.Replace("/", ""),
                 BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.IgnoreCase);
            if (m != null)
            {
                m.Invoke(null, new object[] {
                mf,
                binR,
                vg
            });
            }
            else {
                CoreLog.WriteLine($"No function define to visit {tagName}");
            }
        }
        private static void VisitCmap(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg)
        {
            byte[] data = Decompress(binR, vg);
            var vvs = data.ToStructure<font_cmap>();
            int v_numTable = URead(vvs.numTables);

            int offset = 0;
            offset = Marshal.SizeOf(vvs);
            ushort platformID, encodngID;
            uint boffset;

            
            List<font_cmap_info> inf = new List<font_cmap_info>();
            for (int i = 0; i < v_numTable; i++)
            {
                platformID = URead(data.ToStructure<UInt16>(offset));
                encodngID = URead(data.ToStructure<UInt16>(offset + 2));
                boffset = URead(data.ToStructure<UInt32>(offset + 4));

                inf.Add(new font_cmap_info() {
                     encodingID = encodngID,
                     offset = boffset ,
                     platformID = platformID 
                });
                offset += 8;
            }
            //subtable infor
            UInt16 format, vlength, vlang;
            for (int i = 0; i < inf.Count; i++)
            {
                offset = (int)inf[i].offset;
                format = URead(data.ToStructure<UInt16>(offset));
                if (format == 0)
                {

                    vlength = URead(data.ToStructure<UInt16>(offset + 2));//length
                    vlang = URead(data.ToStructure<UInt16>(offset + 4));//length

                    byte[] g = data.Slice(offset + 6, vlength-6);
                }
            }

        }
        private static void VisitMaxp(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg) {
            var vvs = Decompress(binR, vg).ToStructure<font_maxp>();
            int numOfGlyhp = URead(vvs.numberOfGlyf);
            mf.m_numberOfGlyhp = numOfGlyhp;
        }
        private static void VisitLoca(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg){
        byte[] data = Decompress(binR, vg);
        //o.m_loca = CoreFontFile.ParseLocaTable(data, o.m_numberOfGlyhp);
         }
        private static void VisitGlyf(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg)
        {
           // byte[] data = Decompress(binR, vg);
            //if ((o.m_numberOfGlyhp > 0) && dic.ContainsKey("glyf"))
            //{
             //   ReadGlyf(mf, data);


            //}
        }
        private static void SumHeader(BinaryWriter binW, BinaryWriter headerHoset) {
           // byte[] data = Decompress(binR, vg);

        }
        private static void VisitWebf(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg)
        {
            byte[] data = Decompress(binR, vg);
        }
        private static void Visitpost(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg)
        {
            byte[] data = Decompress(binR, vg);
        }
        private static void Visithmtx(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg)
        {//horizontal metrics
            byte[] data = Decompress(binR, vg);
            var b = data.ToStructure<font_hmtx>();
        }
        private static void VisithHea(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg)
        {
            byte[] data = Decompress(binR, vg);
            var b = data.ToStructure<font_hhea>();

        

        }
            private static void VisitHead(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg)
        {
            byte[] data = Decompress(binR, vg);
            var b = data.ToStructure<font_head>();

            var g = URead((ulong)b.created);
            var gg = TimeSpan.FromSeconds((double)g);
            DateTime created = new DateTime(1904, 1, 1).AddSeconds(g);
            var uperEm = URead(b.unitsPerEm);
            var width = URead(b.xMax) - URead(b.xMin);
            var height = URead(b.yMax) - URead(b.yMin);
            //if ((o.m_numberOfGlyhp > 0) && dic.ContainsKey("glyf"))
            //{
            //   ReadGlyf(mf, data);


            //}
        }
        private static void VisitGasp(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg)
        {
            byte[] data = Decompress(binR, vg);
            //if ((o.m_numberOfGlyhp > 0) && dic.ContainsKey("glyf"))
            //{
            //   ReadGlyf(mf, data);


            //}
        }
        private static void VisitFFTM(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg) {
            byte[] data = Decompress(binR, vg);
            var vvs = data.ToStructure<font_FFTM>();

            //System.DateTime tr = new System.DateTime(vvs.CreationTime);
            //TimeSpan t = TimeSpan.FromTicks(vvs.FontOwnTimeSpan);
        }
        private static void VisitGDEF(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg) {

            byte[] data = Decompress(binR, vg);
            var majorversion = URead(data.ToStructure<ushort>());
            var minorversion = URead(data.ToStructure<ushort>(2));

            if ((majorversion == 1) && (minorversion == 0)) {

                var s = data.ToStructure<font_GDEF1>();

                if (s.LigCaretList != 0) {
                    int offset = URead(s.LigCaretList);
                }
            }
        }

        private static void VisitOS2(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg) {
            byte[] data = Decompress(binR, vg);

            var version = URead(data.ToStructure<ushort>());
            if (version < 5)
            {
                var s = data.ToStructure<font_OS2B>();

            }
            else {
                var s = data.ToStructure<font_OS2>();
            }
        }
        internal static void VisitName(WOFFFile mf, BinaryReader binR, WOFFFileTableDirectory vg) {
            byte[] data = Decompress(binR, vg);
            var version = URead(data.ToStructure<ushort>());

            switch (version)
            {
                case 0:
                    var v_fname = data.ToStructure<font_name>();
                    int Ct = URead((ushort)v_fname.count);
                    var offset = URead(v_fname.offset);
                    var v_boffset = Marshal.SizeOf(typeof(font_name)); 
                    font_nameRecord[] g = new font_nameRecord[Ct];
                    var v_m = Marshal.SizeOf(typeof(font_nameRecord));
                    Dictionary<int, Dictionary<int, string>> dics = new Dictionary<int, Dictionary<int, string>>();
                    Dictionary<int, string> v_dim = null; 
                    using (var s = data.ToMemoryStream())
                    {
                        for (int i = 0; i < Ct; i++)
                        {
                            g[i] = data.ToStructure<font_nameRecord>(v_boffset);
                            v_boffset += v_m;


                            byte[] buffer = new byte[URead(g[i].length)];
                            s.Position = URead(g[i].offset)+offset ;
                            int ci = s.Read(buffer, 0, buffer.Length);

                            if (buffer.Length == ci) {

                                var encid = URead(g[i].encodingID);
                                var langid = URead(g[i].languageID);
                                //System.Diagnostics.Debug.WriteLine("language id " + langid);
                                //System.Diagnostics.Debug.WriteLine("encoding id " + encid);
                                //if (langid == 0)//1033)
                                //{
                                //get value
                                if (!dics.ContainsKey(langid))
                                    dics[langid]= new Dictionary<int, string>();
                                v_dim = dics[langid];

                             //   string v_uni = UnicodeEncoding.Unicode.GetString(buffer);
                                
                               

                                switch (langid)
                                {
                                    case 0:
                                    default:
                                        v_dim.Add(URead(g[i].nameID), UnicodeEncoding.UTF8.GetString(buffer));
                                        break;
                                    case 1033:
                                        Byte[] tbytes = new byte[buffer.Length];
                                        //invert order
                                        for (int jj = 0; jj < buffer.Length - 1; jj += 2)
                                        {
                                            tbytes[jj] = buffer[jj + 1];
                                            tbytes[jj + 1] = buffer[jj];
                                        }
                                        v_dim.Add(URead(g[i].nameID), UnicodeEncoding.Unicode.GetString(tbytes));
                                        break;
                                }
                                //}
                            }


                    }
                    }


                    break;
                default:
                    break;
            }
        }

        private static void ReadGlyf(WOFFFile o, byte[] tab)
        {
           var g =   CoreFontFile.ParseGlyphData(tab, o.m_numberOfGlyhp, o.m_loca);
            o.m_glyphData = g;
        }
        public void EnumerateGlyf(WOFFGlyfCallback callback) {
            int i = 0;
            foreach (var item in m_glyphData)
            {
                if (item.NumberOfContour == 1) {
                }
                if (i == 36)
                {
                }
                item.ExtractGlyphGraphics(out Vector2f[] v, out byte[] t);
                try
                {
                    callback(item.Width>0? item.Width:100,
                        item.Height>0? item.Height : 100, v, t, i);
                }
                catch {
                    CoreLog.WriteLine("error on call back " + i);
                }
                i++;
            }
        }

        private static byte[] Decompress(BinaryReader binR, WOFFFileTableDirectory vg)
        {
            long bck = binR.BaseStream.Position;//backup 
            byte[] odata = null;
            binR.BaseStream.Position = vg.Offset;
            if (IsCompressed(vg))
            {

                //internal static byte[] Decompress(

                
                byte[] data = binR.ReadBytes(vg.CompLength);
                {

                    //using (var compressedStream = new MemoryStream(data))
                    //using (var zipStream = new System.IO.Compression.GZipStream(compressedStream, CompressionLevel.Fastest))// CompressionMode.Decompress))
                    //using (var resultStream = new MemoryStream())
                    //{
                    //    zipStream.CopyTo(resultStream);
                    //    return resultStream.ToArray();
                    //}




                    //decompress string data that have been compressed width compress2 function
                    using (var compressedStream = new MemoryStream(data))
                    {
                        compressedStream.Position = 0;
                        var s = new ZLibNet.ZLibStream(compressedStream, ZLibNet.CompressionMode.Decompress);
                        using (var resultStream = new MemoryStream())
                        {
                            s.CopyTo(resultStream);
                            odata = resultStream.ToArray();
                        }
                        //s.CopyTo(
                        //using (var zipStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
                        //using (var resultStream = new MemoryStream())
                        //{
                        //    zipStream.CopyTo(resultStream);
                        //    odata = resultStream.ToArray();
                        //}
                    }
                }


                //System.IO.Compression.GZipStream g = new System.IO.Compression.GZipStream(binR.BaseStream,
                //     System.IO.Compression.CompressionMode.Decompress);

                //g.
            }
            else {
                binR.BaseStream.Position = vg.Offset;
                odata = binR.ReadBytes(vg.CompLength);
            }
            binR.BaseStream.Position = bck;
            if (odata.Length != vg.OriginLength)
            {
            }
            TOTAL_LN += odata.Length; 
            return odata;
        }

        private static bool IsCompressed(WOFFFileTableDirectory vg)
        {
            return vg.OriginLength > vg.CompLength;
        }

        #region "internal struct";
        [StructLayout(LayoutKind.Sequential)]
        struct WOFFFileHeaderStruct {
            internal UInt32 signature;//must be WOFF
            internal UInt32 flavor;
            internal UInt32 length;
            internal UInt16 numTables;
            internal UInt16 reserved;
            internal UInt32 totalSfntSize;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct WOFFFileHeaderData {
            internal UInt16 majorVersion;
            internal UInt16 minorVersion;
            internal UInt32 metaOffset;
            internal UInt32 metaLength;
            internal UInt32 metaOriginLength;
            internal UInt32 privOffset;
            internal UInt32 privLength;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct WOF2FileHeaderData
        {
            internal UInt32 totalCompressedSize;//one extra field on the wof2 
            internal WOFFFileHeaderData WOFFData;
        }


        [StructLayout(LayoutKind.Sequential)]
        internal struct WOFFFileTableDirectory {
            internal UInt32 tag;
            internal UInt32 offset;
            internal UInt32 compLength;
            internal UInt32 origLength;
            internal UInt32 oriChecksum;

            public string TagName => mmoiToString((int)tag);
            public int Offset => (int)URead(offset);
            public int CompLength=> (int)URead(compLength);
            public int OriginLength=> (int)URead(origLength);
            public int OriginChecksum=> (int)URead(oriChecksum);

            public override string ToString()
            {
                return $"{GetType().Name} : {TagName}";
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack =1)]
        internal struct font_name
        {
            internal short version;//format version = 0
            internal short count;//number of nameRecord
            internal short offset;//offset where string start
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_nameRecord
        {
            internal short platformID;// Platform ID.
            internal short encodingID;// Platform-specific encoding ID.
            internal short languageID;//  Language ID.
            internal short nameID;//  Name ID.
            internal short length;
            internal short offset;

            public override string ToString()
            {
                return $"[{nameID}],[{length}]";
            }
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_glyf
        {
            internal short numberOfContour;
            internal short xMin;
            internal short yMin;
            internal short xMax;
            internal short yMax;
        }
        [StructLayout(LayoutKind.Sequential, Pack =1)]
        internal struct font_loca
        {
            internal short offsets;//must be followed with array or offset short
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_maxp
        {   
            //for version 1.0
            internal ushort majorVersion;
            internal ushort minorVersion;
            internal ushort numberOfGlyf;
            internal UInt16 maxPoints;
            internal UInt16 maxContours;
            internal UInt16 maxCompositePoints;
            internal UInt16 maxCompositeContours;
            internal UInt16 maxZones;
            internal UInt16 maxTwilightPoints;
            internal UInt16 maxStorage;
            internal UInt16 maxFunctionDefs;
            internal UInt16 maxInstructionDefs;
            internal UInt16 maxStackElements;
            internal UInt16 maxSizeOfInstructions;
            internal UInt16 maxComponentElements;
            internal UInt16 maxComponentDepth;

        }

        [StructLayout(LayoutKind.Sequential, Pack =1)]
        internal struct font_cmap
        {
            internal ushort version;//mu
            internal ushort numTables;
            //follow by array of encodingRecords[numTables]
            //encoding record :
            //  uint16 platformID
            //  uint16 encodingID
            //  uint32 offset

        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_cmap_info
        {
            internal UInt16 platformID;
            internal UInt16 encodingID;
            internal UInt32 offset;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_camp_format_4 {
            internal ushort format;
            internal ushort length;
            internal ushort language;
            internal ushort segCount;
            internal ushort searchRange;
            internal ushort entrySelector;
            internal ushort rangeShift;

            public void setSegCount(ushort i) {
                segCount = URead((ushort)(i * 2));
                var srange= (ushort)(2 * Math.Pow(2, Math.Floor(Math.Log(i) / Math.Log(2))));
                searchRange = URead(srange);
                entrySelector = URead((ushort)(Math.Log(srange/2.0f) / Math.Log(2)));
                rangeShift = URead((ushort)((2 * i) - srange));
            }
            
        }
        [StructLayout(LayoutKind.Sequential, Pack =1)]
        internal struct font_head
        {
            internal UInt16 majorVersion;
            internal UInt16 minorVersion;
            internal UInt32 fontRevision;//fixed
            internal UInt32 checkSumAdjustment; //sum of all int32 of the sum
            internal UInt32 magicNumber;//specification 0x5F0F3CF5 but 0x85ea5f0f found in 
            internal UInt16 flags;
            internal UInt16 unitsPerEm;
            internal long created;//LONGDATETIME
            internal long modified;//LONGDATETIME
            internal Int16 xMin;
            internal Int16 yMin;
            internal Int16 xMax;
            internal Int16 yMax;
            internal UInt16 macStyle;
            internal UInt16 lowestRecPPEM;
            internal Int16 fontDirectionHint;
            internal Int16 indexToLocFormat;
            internal Int16 glyphDataFormat;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_hhea
        {//FWORD = int16
            //UFWOR = UInt16
            internal UInt16 majorVersion;
            internal UInt16 minorVersion;
            internal Int16 Ascender;//FWORD 
            internal Int16 Descender;// FWORD
            internal Int16 LineGap;//FWORD
            internal UInt16 advanceWidthMax;//// UFWORD
            internal Int16 minLeftSideBearing;// FWORD
            internal Int16 minRightSideBearing;// FWORD
            internal Int16 xMaxExtent;// FWORD
            internal Int16 caretSlopeRise;
            internal Int16 caretSlopeRun;
            internal Int16 caretOffset;
            internal Int16 reserved1;
            internal Int16 reserved2;
            internal Int16 reserved3;//set to 0
            internal Int16 reserved4;//set to 0
            internal Int16 metricDataFormat;
            internal UInt16 numberOfHMetrics;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_hmtx {
            //internal Int16 hMetrics;
            //internal Int16 LeftSideBearing;
            internal UInt16 advanceWidth;
            internal Int16 leftSideBearing; 
            //internal UInt16 lsb;

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_panose_data {
           internal byte bFamilyType;
           internal byte bSerifStyle;
           internal byte bWeight;
           internal byte bProportion;
           internal byte bContrast;
           internal byte bStrokeVariation;
           internal byte bArmStyle;
           internal byte bLetterform;
           internal byte bMidline;
           internal byte bXHeight;
        }
        [StructLayout(LayoutKind.Sequential, Pack =1)]
        internal struct font_FFTM
        {
         //   [FieldOffset(0)]
            internal UInt32 version;
           // [FieldOffset(4)]
            internal UInt64 fontOwnTimeSpan;
           // [FieldOffset(12)]
            internal UInt64 creationTimeSpan;
            //[FieldOffset(20)]
            internal UInt64 modifficationTimeSpan;

            public uint Version => URead(version);
            public long FontOwnTimeSpan =>(long) URead(fontOwnTimeSpan);
            public long CreationTime => (long)URead(creationTimeSpan);
            public long ModificationTime => (long)URead(modifficationTimeSpan);
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_GDEF1 {
            internal short marjorVersion;
            internal short minorVersion;
            internal short glyhpClassDef;
            internal short attachList;
            internal short LigCaretList;
            internal short markAttachClassDef;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_gasp
        {
            internal short version; //set to 1
            internal short numRange;
            //follow array of gasp range function of numRange
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_gasp_range
        {
            internal short rangeMaxPPEM;
            internal short rangeGaspBehaviour;            
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_post
        {
            internal UInt16 majorVersion;
            internal UInt16 minorVersion;
            internal Int16 italicAngleX;//Fixed
            internal Int16 italicAngleY;//Fixed

            internal short underlinePosition;
            internal short underlineThickness;
            internal Int32 isFixedPicth;
            internal Int32 mimMemType42;
            internal Int32 maxMemType42;
            internal Int32 mimMemType1;
            internal Int32 maxMemType1;
        } 
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_OS2B //for  version<5
        {
            internal UInt16 version;// 	0x0100 for version 1
            internal short xAvgCharWidth;
            internal UInt16 usWeightClass;
            internal UInt16 usWidthClass;
            internal UInt16 fsType; //for embeding security restriction
            internal short ySubscriptXSize;
            internal short ySubscriptYSize;
            internal short ySubscriptXOffset;
            internal short ySubscriptYOffset;
            internal short ySuperscriptXSize;
            internal short ySuperscriptYSize;
            internal short ySuperscriptXOffset;
            internal short ySuperscriptYOffset;
            internal short yStrikeoutSize;
            internal short yStrikeoutPosition;
            internal short sFamilyClass;
            [System.Runtime.InteropServices.MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10)]
            internal byte[] panose;// panose[10];
            internal UInt32 ulUnicodeRange1;//	Bits 0–31
            internal UInt32 ulUnicodeRange2;//	Bits 32–63
            internal UInt32 ulUnicodeRange3;//	Bits 64–95
            internal UInt32 ulUnicodeRange4;//	Bits 96–127
            internal UInt32 achVendID;
            internal UInt16 fsSelection;
            internal UInt16 usFirstCharIndex;
            internal UInt16 usLastCharIndex;
            internal short sTypoAscender;
            internal short sTypoDescender;
            internal short sTypoLineGap;
            internal UInt16 usWinAscent;
            internal UInt16 usWinDescent;
            internal UInt32 ulCodePageRange1;//	Bits 0–31
            internal UInt32 ulCodePageRange2;//	Bits 32–63
            internal short sxHeight;
            internal short sCapHeight;
            internal UInt16 usDefaultChar;
            internal UInt16 usBreakChar;
            internal UInt16 usMaxContext;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct font_OS2 //for 5 version
        {
            internal font_OS2B os_2;
            internal UInt16 usLowerOpticalPointSize;
            internal UInt16 usUpperOpticalPointSize;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct ttf_fileheader
        {
                internal int signature;
                internal ushort numTables;
                internal ushort searchRange, entrySelector, rangeShift;

                public int Signature => URead(signature);
                public int NumTables => URead (numTables);
                public int SearchRange => URead(searchRange);
                public int EntrySelector => URead(entrySelector);
                public int RangeShift => URead(rangeShift);


        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct ttf_tabledirectory
        {
            internal UInt32 tag;//	4-byte sfnt table identifier.
            internal UInt32 checksum;//
            internal UInt32 offset;//
            internal UInt32 length;

            public string TagName => mmoiToString((int)tag);
        }
            #endregion


            static readonly uint SIGN_WOFF = (uint)mmoiCount('w', 'O', 'F', 'F');
        static readonly uint SIGN_WOF2 = (uint)mmoiCount('w', 'O', 'F', '2');
        static readonly uint FLAVOR_OTTO = (uint)mmoiCount('O', 'T', 'T', 'O');
        static readonly uint FLAVOR_true = (uint)mmoiCount('t', 'r', 'u', 'e');
        static readonly uint FLAVOR_one = (uint)0x01000000;
        //old= 00010000


        static readonly uint HTag_glyf = (uint)mmoiCount('g', 'l', 'y', 'f');
        static readonly uint HTag_loca = (uint)mmoiCount('l', 'o', 'c', 'a');
        static readonly uint HTag_FFTM = (uint)mmoiCount('F', 'F', 'T', 'M');
        static readonly uint HTag_GDEF = (uint)mmoiCount('G', 'D', 'E', 'F');
        static readonly uint HTag_OS_2 = (uint)mmoiCount('O', 'S', '/', '2');
        static readonly uint HTag_cmap = (uint)mmoiCount('c', 'm', 'a', 'p');
        static readonly uint HTag_gasp = (uint)mmoiCount('g', 'a', 's', 'p');
        static readonly uint HTag_head = (uint)mmoiCount('h', 'e', 'a', 'd');
        static readonly uint HTag_hhea = (uint)mmoiCount('h', 'h', 'e', 'a');
        static readonly uint HTag_hmtx = (uint)mmoiCount('h', 'm', 't', 'x');
        static readonly uint HTag_maxp = (uint)mmoiCount('m', 'a', 'x', 'p');
        static readonly uint HTag_name = (uint)mmoiCount('n', 'a', 'm', 'e');
        static readonly uint HTag_post = (uint)mmoiCount('p', 'o', 's', 't');
        static readonly uint HTag_webf = (uint)mmoiCount('w', 'e', 'b', 'f');
        private static Dictionary<string, ttf_tabledirectory> sm_dic;
        private static byte[] sm_data;
        private WOFFData woffData;








        #region "utility functions"
        internal static int mmoiCount(char ch0, char ch1, char ch2, char ch3)
        {
            return ((int)ch0 | ((int)ch1 << 8) | ((int)ch2 << 16) | ((int)ch3 << 24));
        }

        internal static string mmoiToString(int v)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((char)(v & 255));
            sb.Append((char)((v >> 8) & 255));
            sb.Append((char)((v >> 16) & 255));
            sb.Append((char)((v >> 24) & 255));
            return sb.ToString();
        }
        internal static int URead(int v)
        {
            var vj = ((v & 0x000000FF) << 24) +
                      ((v & 0x0000FF00) << 8) +
                      ((v & 0x00FF0000) >> 8) +
                      ((v & 0xFF000000) >> 24);

            return (int)vj;
        }
        internal static uint URead(uint v) {
            var vj = ((v & 0x000000FF) << 24) +
                      ((v & 0x0000FF00) << 8) +
                      ((v & 0x00FF0000) >> 8) +
                      ((v & 0xFF000000) >> 24);

            return vj;


            //StringBuilder sb = new StringBuilder();
            //sb.Append((v & 255));
            //sb.Append(((v >> 8) & 255));
            //sb.Append(((v >> 16) & 255));
            //sb.Append(((v >> 24) & 255));
            //var j = UInt32.Parse(sb.ToString());
            //return j;
        }
        internal static ushort URead(ushort i)
        {
            return
              (ushort)(  ((i & 0x00FF)<<8 )+
                ((i & 0xFF00)>>8));
        }
        internal static short URead(short i)
        {
            return(short)(
                ((i & 0x00FF) << 8) +
                ((i & 0xFF00) >> 8));
        }
        internal static UInt64  URead(UInt64 i)
        {
            var s =  (((ulong)(URead((uint)(i & 0xFFFFFFFF)))) << 32 )+
                (ulong)URead((uint)((i & 0xFFFFFFFF00000000) >> 32));

            return s;
        }
        internal static Int64 URead(Int64 i)
        {
            var s = (((long)(URead((uint)(i & 0xFFFFFFFF)))) << 32) +
                (long)URead((uint)((i >>32) & 0xFFFFFFF));

            return s;
        }
        //private class WOFFFileAddInfo<T> where T:struct
        //{
        //    public WOFFFileAddInfo()
        //    {
        //    }

        //    public WOFFFileInfo File { get; set; }
        //    public MemoryStream Memo { get; set; }
        //    public T Table { get; set; }
        //    public WOFFFileTableDirectory Dir { get; internal set; }
        //}
        #endregion

    }


  }
