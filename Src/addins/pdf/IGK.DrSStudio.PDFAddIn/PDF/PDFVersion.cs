using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.PDF
{
    public class PDFVersion
    {
        private int minor;
        private int major;
        public static readonly PDFVersion VERSION_1_7;
        public static readonly PDFVersion VERSION_1_8;
        public static readonly PDFVersion VERSION_1_4;
        internal PDFVersion(int major, int minor)
        {
            this.major = major;
            this.minor = minor;
        }
        public override string ToString()
        {
            return string.Format("{0}.{1}", this.major, this.minor);
        }
        static PDFVersion() {
            VERSION_1_4 = new PDFVersion(1, 4);
            VERSION_1_7 = new PDFVersion(1, 7);
            VERSION_1_8 = new PDFVersion(1, 8);
        }
    }
}
