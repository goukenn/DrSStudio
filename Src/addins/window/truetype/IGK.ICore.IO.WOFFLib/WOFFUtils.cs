using IGK.ICore.ComponentModel;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static IGK.ICore.IO.WOFFFile;

namespace IGK.ICore.IO
{
    /// <summary>
    /// represent a woff's utility functions class
    /// </summary>
    static class WOFFUtils
    {
        public static T ToLSB<T>(this T i) where T : struct {

            Type t = typeof(T);
            if (!t.IsValueType && !t.IsPrimitive)
                throw new ArgumentException($"not a value type {nameof(i)}");
            var c = t.Assembly.CreateInstance(t.FullName);
            foreach (var mi in t.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {

                if (mi.FieldType.IsArray) {

                    if (mi.FieldType.Name.Replace("[]","")=="Byte") {
                        mi.SetValue(c, mi.GetValue(i));
                    }
                    continue;
                }

                var m = typeof(WOFFFile).GetMethod("URead",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public,
                    null,
                     new Type[]{
                         mi.FieldType
                     }, null);
                if (m != null)
                {
                    var g = m.Invoke(null, new object[] { mi.GetValue(i) });
                    mi.SetValue(c, g);
                }
                else
                    throw new Exception("no method found for " + mi.Name);// Console.WriteLine(i.Name + " : " + i.GetValue(vss));
            }

            return (T)c;
        }

        internal static byte[] ParseBytesFromString(string v)
        {
            if (!string.IsNullOrEmpty(v))
            {
                string[] sm = v.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                byte[] t = new byte[sm.Length];
                for (int i = 0; i < sm.Length; i++)
                {
                    t[i] = byte.Parse(sm[i]);
                }
                return t;
            }
            return null;
        }
        internal static Vector2f[] ParseVector2fFromString(string v) {
            if (!string.IsNullOrEmpty(v))
            {
                TypeConverter v_conv = CoreTypeDescriptor.GetConverter(typeof(Vector2f));
                object obj = v_conv.ConvertFromString(v);
                if ((obj != null) && obj.GetType().IsArray)
                {
                    if (obj is Vector2f[])
                    {
                        return (Vector2f[])obj;
                    }
                    else if (obj is Vector2f[])
                    {
                        Vector2f[] t = (Vector2f[])obj;
                        var m_Points = new Vector2f[t.Length];
                        for (int i = 0; i < t.Length; i++)
                        {
                            //implicit copy
                            m_Points[i] = t[i];
                        }
                        return m_Points;
                    }
                }
                else
                    return new Vector2f[] { (Vector2f)obj };
            }
            return null;
        }

        /// <summary>
        /// get vector orientation
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        internal static enuOrientation GetOrientation(Vector2f[] g)
        {

            float sum = 0;
            Vector2f pts = Vector2f.Zero;
            Vector2f opts = Vector2f.Zero;
            for (int i = 0; i < g.Length; i++)
            {
                pts = g[i];
                if ((i + 1) >= g.Length)
                {
                    //last point
                    opts = g[0];
                }
                else
                {
                    opts = g[i + 1];
                }

                sum += (pts.X * opts.Y) - (opts.X * pts.Y);
            }


            return sum > 0 ? enuOrientation.Clockwize : enuOrientation.AntiClockwize;
        }


        /// <summary>
        /// remove ttf header
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="header"></param>
        /// <param name="output"></param>
        public static void RemoveTable(string filename, string header, string output) {

            if (header == "head")//can't remove the header table
                return;
            using (MemoryStream mem = new MemoryStream()) {
                BinaryReader binR = new BinaryReader(File.Open(filename, FileMode.Open));
                BinaryWriter binw = new BinaryWriter(mem);


                ttf_fileheader h = CoreByteArrayExtension.Read<ttf_fileheader>(binR);
                Dictionary<string, ttf_tabledirectory> dics = new Dictionary<string, ttf_tabledirectory>();

                bool remove = false;

                //StringBuilder sb = new StringBuilder();
                for (int i = 0; i < h.NumTables; i++)
                {
                    
                    var g = CoreByteArrayExtension.Read<ttf_tabledirectory>(binR);
                    var pos = binR.BaseStream.Position;
                    //Console.WriteLine(g.TagName);
                    //WOFFLog.Log(g);
             

                    if (g.TagName == header)
                    {
                        remove = true;
                    }
                    else {
                        dics[g.TagName] = g;                       
                    }
                }
                if (remove) {
                    int n = h.NumTables - 1;
                    int s = Marshal.SizeOf(typeof(ttf_tabledirectory));
                    h.numTables = URead((ushort)(n));// h.NumTables - 1));


                    var exp = Math.Floor(Math.Log((int)n) / Math.Log(2));
                    var sR = Math.Pow(2, exp) * 16;
                    h.entrySelector = (ushort)URead((ushort)exp);
                    h.searchRange = (ushort)URead((ushort)sR);//(Math.Pow(2, exp)*16);//.Floor(Math.Log((int)cT) / Math.Log(2));
                    h.rangeShift = (ushort)URead((ushort)((n * 16) - sR));


                    h.WriteTo(binw);
                    var pos = binw.BaseStream.Position;
                    binw.Write(new byte[n * s], 0, n * s);
                    var s_datapos = binw.BaseStream.Position;
                    binw.BaseStream.Position = pos;

                    uint header_offset = 0;
                    uint ln = 0;
                    byte[] data = null;

                    WOFFFile cf = WOFFFile.CreateNew(enuWOFFFIleType.TTF);
                    foreach (var item in dics)
                    {
                        if (item.Value.TagName == "header") {
                            header_offset = URead(item.Value.offset);
                        }
                        var c =  item.Value;
                        var offread = URead(c.offset);
                        binR.BaseStream.Position = offread;// URead(c.offset); 

                        ln = URead(c.length);
                        data = new byte[ln];
                        binR.Read(data, 0, data.Length);


                        GetInfo(c.TagName, data);

                        c.offset = URead((uint) s_datapos);
                        //calcchecksum
                        c.checksum = URead((uint)WOFFFile.CalculateChecksum(data));
                        c.WriteTo(binw);


                        pos = binw.BaseStream.Position;
                        

                        binw.BaseStream.Position = s_datapos;
                        binw.Write(data, 0, data.Length);

                        uint paddData = ln % 4;
                        if (paddData > 0) {
                            paddData = 4 - paddData;
                            ln += paddData;
                            binw.Write(new byte[paddData], 0, (int)paddData);
                        }
                        s_datapos += ln;
                        binw.BaseStream.Position = pos;
                    }

                }
                binR.Close();
                mem.Seek(0, SeekOrigin.Begin);
                using (var fs = File.Open(output, FileMode.Create))
                {
                    mem.WriteTo(fs);
                }

            }
        }

        private static void GetInfo(string tagName, byte[] data)
        {
            object obj = null;
            Type t = typeof(font_cmap);

            Console.WriteLine("Log : " + tagName);
            switch (tagName.ToLower())
            {
                case "cmap":
                    {
                        var i = data.ToStructure<font_cmap>().ToLSB();
                    }
                    break;
                case "head":
                    {
                        var i = data.ToStructure<font_head>().ToLSB();
                    }
                    break;
                case "glyf":
                    {
                        var i = data.ToStructure<font_glyf>().ToLSB();
                    }
                    break;
                case "hhea":
                    {
                        var i = data.ToStructure<font_hhea>().ToLSB();

                        WOFFLog.Log(data.ToStructure<font_hhea>());
                    }

                    break;
                case "hmtx":
                    {
                        var i = data.ToStructure<font_hmtx>().ToLSB();
                    }

                    break;
                case "loca":
                    {
                        var i = data.ToStructure<font_loca>().ToLSB();
                    }

                    break;
                case "maxp":
                    {
                        var i = data.ToStructure<font_maxp>().ToLSB();
                    }

                    break;
                case "name":
                    {
                        var i = data.ToStructure<font_name>().ToLSB();

                        using (var m = new MemoryStream(data))
                        {
                            BinaryReader binr = new BinaryReader(m);
                            var s = WOFFFile.CreateNew(enuWOFFFIleType.TTF);

                            WOFFFile.VisitName(s, binr, new WOFFFile.WOFFFileTableDirectory() {
                                origLength = URead((uint)data.Length),
                                compLength = URead((uint)data.Length)
                            });
                        }



                    }

                    break;
                case "post":
                    {
                        var i = data.ToStructure<font_post>().ToLSB();
                    }

                    break;
                case "fftm":
                    {
                        var i = data.ToStructure<font_FFTM>().ToLSB();
                    }

                    break;
                case "gdef":
                    {
                        var i = data.ToStructure<font_GDEF1>().ToLSB();
                    }

                    break;
                case "os/2":
                    {
                        var i = data.ToStructure<font_OS2B>().ToLSB();

                        WOFFLog.Log(i);
                    }

                    break;
                case "":
                default:
                    Console.WriteLine("not used " + tagName);
                    
                    break;
            }
            if (obj != null)
            {

            }
        }
    }
}
