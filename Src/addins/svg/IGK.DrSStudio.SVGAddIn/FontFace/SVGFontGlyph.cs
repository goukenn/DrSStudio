using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.SVGAddIn.FontFace
{
    public class SVGFontGlyph
    {
        private int m_Unicode;
        private CoreGraphicsPath m_Path;
        
        public CoreGraphicsPath Path
        {
            get { return m_Path; }
            set
            {
                if (m_Path != value)
                {
                    m_Path = value;
                }
            }
        }
        public int Unicode
        {
            get { return m_Unicode; }
            set
            {
                if (m_Unicode != value)
                {
                    m_Unicode = value;
                }
            }
        }
        public SVGFontGlyph(int unicode, CoreGraphicsPath path)
        {
            this.m_Path = path;
            this.m_Unicode = unicode;
        }
    }
}
