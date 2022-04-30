using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.Resources;
using IGK.ICore.Web;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace IGK.CssPropertiesBuilder.WinUI
{


    /// <summary>
    /// class used for scripting
    /// </summary>
    [ComVisible(true)]
    public class CSSBuilderScriptViewerProfile : CoreWebScriptObjectBase
    {
        public CSSBuilderScriptViewerProfile()
        {
        }
        public string GetCss()
        {
           
            var x = CoreXmlElement.CreateXmlNode("script") as CoreXmlWebScriptElement;
            x.ForWebDocument = true;
            x["type"] = "text/javascript";
            x.Content =
#if DEBUG
 CoreFileUtils.ReadAllFile(CoreConstant.DRS_SRC+@"\addins\web\IGK.CssPropertiesBuilder\Resources\css_builder_main.js") ??
  CoreResources.GetString(String.Format(CSSBuilderConstant.RES_FORMAT_1, "css_builder_main.js")) ;      
#else
 CoreResources.GetString (String.Format (CSSBuilderConstant.RES_FORMAT_1 ,"css_builder_main.js" ));
#endif
            return x.RenderXML(null);
        }

        public void Save(string data) {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.FileName = "index.css";
                sfd.Filter = "css-file | *.css";
                if (sfd.ShowDialog() == global::System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, data);
                    if (this.ReloadListener is CssEditorSurface s){
                        s.FileName = sfd.FileName;
                    }
                }
            }
        }

        public void PickColor(string n ) {
            using (ColorDialog cldial = new ColorDialog())
            {
                cldial.AllowFullOpen = true;
                cldial.FullOpen = true;
                if (cldial.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                { 
                    Colorf gf = Colorf.FromIntArgb (cldial.Color.ToArgb());
                    this.InvokeScript("cssbuilder.setvalue('" + n + "', '" + gf.ToString(true) + "');");
                }
            }
        }
    }
}
