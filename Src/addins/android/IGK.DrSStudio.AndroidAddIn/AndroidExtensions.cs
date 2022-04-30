
using IGK.ICore.IO;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IGK.DrSStudio.Android
{
    static class AndroidExtensions
    {
        /// <summary>
        /// get the wpf font familly from string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static FontFamily WpfFontFamily(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            return (System.Windows.Media.FontFamily)
            new System.Windows.Media.FontFamilyConverter().ConvertFromString(text);
        }

        public static void InitAndroidWebDocument(this CoreXmlWebDocument document)
        {
            if (document == null)
                return;

            document.AddLink(PathUtils.GetPath("%startup%/sdk/lib/bootstrap/css/bootstrap.min.css"));
            document.AddLink(PathUtils.GetPath("%startup%/sdk/Styles/igk.css"));
        }

    }
}
