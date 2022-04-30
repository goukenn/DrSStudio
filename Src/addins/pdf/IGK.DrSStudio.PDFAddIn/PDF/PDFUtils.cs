using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    using IGK.ICore;
    using System.Threading;

    public class PDFUtils
    {
        internal static string GetValue(object item)
        {
            if (item == null)
                return null;

            if (item.GetType().IsPrimitive || (item.GetType() == typeof (string)))
                return item.ToString();
            var r =  MethodInfo.GetCurrentMethod().Visit(null, item)  as string;
            //if ((r == null) && (item != null))
            //    return item.ToString();
            return r;
        }
        public static string GetValue(PDFObject obj) {
            return obj.GetReferenceString();
        }
        public static string GetValue(PDFNameObject obj)
        {
            return obj.Render();
        }
        public static string GetValue(PDFArray obj)
        {
            if (obj.Count == 0)
                return null;
            return obj.Render();
        }
        public static string GetValue(IPDFItem  obj)
        {
            return obj.Render();
        }

        public static string GetValue(PDFRectangle obj)
        {
            if (obj.Equals(PDFRectangle.Empty))
                return null;
            return obj.Render();
        }

        internal static global::System.Globalization.CultureInfo InitCulture()
        {

            var uiCulture = Thread.CurrentThread.CurrentUICulture;
            var cCultur = Thread.CurrentThread.CurrentCulture;
            global::System.Globalization.CultureInfo c = new System.Globalization.CultureInfo(uiCulture.LCID);
            var f = Thread.CurrentThread.CurrentCulture.NumberFormat;
            c.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = c;
            Thread.CurrentThread.CurrentUICulture = c;
            return cCultur;
        }

        internal static void RestoreCulture( global::System.Globalization.CultureInfo c)
        {
            Thread.CurrentThread.CurrentCulture = c;
            Thread.CurrentThread.CurrentUICulture = c;
        }
    }
}
