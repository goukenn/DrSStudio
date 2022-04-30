
using IGK.DrSStudio.Editor.FontEditor.WinUI;
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Imaging;
using IGK.ICore.IO;
using IGK.ICore.IO.Font;
using IGK.ICore.Xml;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Editor.FontEditor
{
    using IGK.ICore.Xml;
    using IGK.ICore.Web;
    using System.Drawing.Text;
    using System.Runtime.InteropServices;

    /// <summary>
    /// represent editor utility
    /// </summary>
    public static class FontEditorUtility
    {
        static string[] sm_fontList;
        

        internal static string GetFontInfoFile(string fontname)
        {
            fontname = fontname.ToLower();
            string dir = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Windows ),"fonts");
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                if (sm_fontList == null)
                {
                    List<string> m = new List<string>();        
                    string[] t = System.IO.Directory.GetFiles(dir);
                    m.AddRange(t);
                    sm_fontList = m.ToArray();
                }

                RegistryView rv = Environment.Is64BitProcess ? RegistryView.Registry64 : RegistryView.Registry32;
                RegistryKey rg = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, rv);
                RegistryKey fontkey = rg.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", false);
                string v_m = string.Empty;
                string v_p = string.Empty;
                if (fontkey != null)
                {
                    string[] e = fontkey.GetValueNames();
                    Array.Sort(e);
                    foreach (var item in e)
                    {
                        string h = fontkey.GetValue(item) as string;
                        if (string.IsNullOrEmpty(h))
                            continue;
                        v_m = getName(item).ToLower ();
                        if (v_m == fontname)
                        {
                            v_p = getPath(dir, h);
                            if ((Path.GetExtension(v_p).ToLower() == ".ttf") &&
                                (sm_fontList.Contains(v_p)))
                            {
                                fontkey.Close();
                                return v_p;                                
                            }
                        }
                    }
                }
                fontkey.Close();
            }
            return string.Empty;
        }

        public static void GenerateHtmlDocument(string fontfile, string outHtmlFile)
        {
            FontEditorScriptObject m_scriptObject = new FontEditorScriptObject();
            var doc = CoreXmlWebDocument.CreateICoreDocument();
            doc.AddLink(PathUtils.GetPath("%startup%/sdk/lib/bootstrap/css/bootstrap.min.css"));
            doc.AddLink(PathUtils.GetPath("%startup%/sdk/Styles/igk.css"));
            bool v = false;
            bool v_build = true;
            string fname = Path.GetFileName(fontfile);
            byte[] tb = File.ReadAllBytes(fontfile);
            using (var piCollection = new PrivateFontCollection())
            {
              
                IntPtr alloc = tb.CopyToCoTaskMemory();
                piCollection.AddMemoryFont(alloc, tb.Length);
                //piCollection.AddFontFile(fontfile);
                v = (piCollection.Families.Length > 0);
                piCollection.Dispose();
                Marshal.FreeCoTaskMem(alloc);
            }

            if (!v)
            {
                var dv = doc.Body.addDiv();
                dv.Content = "e.fontfilenotfound_1".R(fontfile);
                v_build = false;
            }


            if (v_build ){

            string s = $"data:application/font-ttf;charset=utf-8;base64,{Convert.ToBase64String(tb, Base64FormattingOptions.None)}";

            doc.Head.add("style").setAttribute("type", "text/css")
            .setContent(string.Format(@"
@font-face{{font-family: ""testing""; src: url('{0}') format(""truetype"");}}
.testing-font{{
	font-family: 'testing';
	font-size: 4em;
}}
.testing-font a:visited{{
	color:#444;
}}
.testing-font a:hover{{
	color:#ccc;
}}
.testing-font a:focus{{
	color:#ccc;
}}
.v{{
font-family: sans-serif; font-size: 12pt;
}}
.tblock{{

}}
", s));

            var data = doc.Body.addDiv().setClass("fith fitw overflow-y-a").addDiv().setClass("igk-container");

                //bool copy =
                //PathUtils.CopyFile(f, rf, true);
                //m_files.Add(rf);

                var font = CoreFontFile.FontParser(fontfile, false);
                    if (font == null)
                    {
                        data.addDiv().setClass("igk-danger").Content = "/!\\ Failed to open file";
                    }
                    else
                    {
                        ushort[] list = font.UnicodeList();
                        string t = string.Empty;
                        var header = data.addDiv();

                        var rp = data.addDiv();
                        rp.setClass("testing-font");
                        bool unicode = list.Length > 0;
                        if (unicode)
                        {
                            var ul = header.add("ul");
                            ul.add("li").Content = "lb.Name_1".R(fname);
                            ul.add("li").Content = "lb.numberofunicodeglyf_1".R(list.Length);
                            ul.add("li").Content = "lb.numberofInnerGlyf_1".R(font.NumberOfGlyphs);
                            m_scriptObject.List = list;
                            rp.addDiv().Content = m_scriptObject.fe_getpage(1);
                        }
                        else
                        {
                            var ul = header.add("ul");
                            ul.add("li").Content = "lb.Name_1".R(fname);
                            ul.add("li").Content = "lb.numberofInnerGlyf_1".R(font.NumberOfGlyphs);
                            //1000 per page
                            int page = font.NumberOfGlyphs / 1000;
                            int p = font.NumberOfGlyphs % 1000;
                            for (int i = 0; i < p; i++)
                            {
                                var span = rp.addDiv().setClass("floatl tblock").
                                //setAttribute("style", "border:1px solid #eee; overflow:hidden; padding: 4px;").
                                add("span");

                                span.addA("#").setAttribute("onclick", "javascript: if (window.external)window.external.fe_edit(" + i + "); return false; ")
                                    .setContent("&#x" + i.ToBase(16, 4) + ";");
                                span.getParentNode().addDiv()
                                .setAttribute("class", "v").setContent("&amp; #x" + i.ToBase(16, 4));
                            }
                        }
                    }
        }


            File.WriteAllText(outHtmlFile, doc.RenderXML(null));
        }

        private static string getPath(string dir, string v_m)
        {
            if (File.Exists(v_m))
                return v_m;
            return dir + "\\" + v_m;
        }
        private static string getName(string name)
        {
            //System.Diagnostics.Debug.WriteLine("getName::"+name);
            name = name.Replace("(TrueType)", "");
            return name.Trim();
        }

        public static bool ExtractGlyphToGkds(string ttf, string gkds, bool bitmap =false )
        {
            if (!PathUtils.CreateDir(Path.GetDirectoryName(gkds)))
                return false;

           var c = CoreSystemServices.GetServiceByName("SVGFontFaceManager") as ICoreFontService;

            return c?.ExtractGlyfFont(ttf, gkds, bitmap, (v,e)=> {


            }) ?? false;

            
        }
    }
}
