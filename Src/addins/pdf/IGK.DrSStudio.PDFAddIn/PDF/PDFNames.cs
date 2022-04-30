using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    public class PDFNames
    {
        public static readonly PDFNameObject Type;
        public static readonly PDFNameObject Type1;
        public static readonly PDFNameObject Type2;
        public static readonly PDFNameObject TrueType;
        public static readonly PDFNameObject Font;
        public static readonly PDFNameObject Length;
        public static readonly PDFNameObject DecodeParms;
        public static readonly PDFNameObject F;
        public static readonly PDFNameObject Filter;
        public static readonly PDFNameObject FFilter;
        public static readonly PDFNameObject FDecodeParms;
        public static readonly PDFNameObject DL;
        public static readonly PDFNameObject Page;
        public static readonly PDFNameObject Parent;
        public static readonly PDFNameObject Resources;
        public static readonly PDFNameObject Kids;
        public static readonly PDFNameObject Count;
        public static readonly PDFNameObject MediaBox;
        public static readonly PDFNameObject BaseType;
        public static readonly PDFNameObject BBox;
        public static readonly PDFNameObject Subtype;

        public static readonly PDFNameObject ASCII85Decode;
        public static readonly PDFNameObject LZWDecode;
        public static readonly PDFNameObject EndOfLine;

        //trailer
        public static readonly PDFNameObject Size;
        public static readonly PDFNameObject Root;
        public static readonly PDFNameObject Catalog;
        public static readonly PDFNameObject Pages;
        public static readonly PDFNameObject Contents;
        public static readonly PDFNameObject BaseFont;
        public static readonly PDFNameObject F1;
        public static readonly PDFNameObject F2;
        public static readonly PDFNameObject F3;
        public static readonly PDFNameObject F4;
        public static readonly PDFNameObject F5;
        public static readonly PDFNameObject F13;
        public static readonly PDFNameObject WinAnsiEncoding;
        public static readonly PDFNameObject Encoding;
        public static readonly PDFNameObject XObject;
        public static readonly PDFNameObject Image;
        public static readonly PDFNameObject ImageB;
        public static readonly PDFNameObject PDF;
        public static readonly PDFNameObject ProcSet;
        public static readonly PDFNameObject Width;
        public static readonly PDFNameObject Height;
        public static readonly PDFNameObject ColorSpace;
        public static readonly PDFNameObject DeviceRGB;
        public static readonly PDFNameObject DeviceGray;
        public static readonly PDFNameObject BitsPerComponent;
        public static readonly PDFNameObject Colors;
        public static readonly PDFNameObject Predictor;
        public static readonly PDFNameObject Columns;
        public static readonly PDFNameObject DCTDecode;
        

        static PDFNames() {
            foreach (var item in MethodInfo.GetCurrentMethod().DeclaringType.GetFields(
                 BindingFlags.Static | BindingFlags.Public 
                ))
            {
                item.SetValue(null, new PDFNameObject(item.Name));
            }
        }
    }
}
