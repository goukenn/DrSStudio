using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IGK.PDF
{
    [StructLayout(LayoutKind.Sequential )]
    public struct  PDFRectangle : IPDFItem 
    {
        public static readonly PDFRectangle Empty;

        static PDFRectangle() {
            Empty = new PDFRectangle();
        }
        float x;
        float y;
        float w;
        float h;
        public float Width { get { return w; } }
        public float Height{ get { return h; } }
        public PDFRectangle(
            float x,
        float y,
        float w,
        float h
            )
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public string Render()
        {
            return string.Format("[{0} {1} {2} {3}]", x, y, w, h);
        }
    }
}
