

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreFontInfo.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WinCoreFontInfo.cs
*/
using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;


namespace IGK.ICore.WinCore
{
    public class WinCoreFontInfo : ICoreFontInfo, IDisposable 
    {
        private string fontName;
        private FontFamily m_fontFamily;
        public object  FontFamily { get { return this.m_fontFamily; } }
        private WinCoreFontInfo()
        { 
        }
        public static WinCoreFontInfo CreateFontFromFile(string file)
        {
            if (File.Exists(file) == false)
            {
                return null;
            }
            System.Drawing.Text.PrivateFontCollection pc = new System.Drawing.Text.PrivateFontCollection();
            pc.AddFontFile(file);
            if (pc.Families.Length > 0)
            {
                FontFamily r = pc.Families[0];
                WinCoreFontInfo rf = new WinCoreFontInfo
                {
                    m_fontFamily = r
                };
                pc.Dispose();
                return rf;
            }
            return null;

        }
        public WinCoreFontInfo(string fontName)
        {
            this.fontName = fontName;
            m_fontFamily = new FontFamily(fontName);
            bool v_c = m_fontFamily.Name.ToUpper() == fontName.ToUpper();         
            Debug.Assert(v_c, "Font definition not created : "+m_fontFamily.Name+" =? " + fontName + " : " + v_c);
        }

        

        public string FontFamilyName
        {
            get { return this.m_fontFamily.Name; }
        }
        public bool IsStyleAvailable(enuFontStyle style)
        {
            try
            {
                return this.m_fontFamily.IsStyleAvailable((FontStyle)style);
            }
            catch { 
            }
            finally { 
            }
            return false;
        }
        public void Dispose()
        {
            this.m_fontFamily.Dispose();
        }
        public int GetCellAscent(enuFontStyle style)
        {
            return this.m_fontFamily.GetCellAscent((FontStyle)style);
        }
        public int GetCellDescent(enuFontStyle style)
        {
            return this.m_fontFamily.GetCellAscent((FontStyle)style);
        }
        public int GetLineSpacing(enuFontStyle style)
        {
            return this.m_fontFamily.GetCellAscent((FontStyle)style);
        }
      

        public int GetCellAscent(ICoreFont font)
        {
            return this.m_fontFamily.GetCellAscent((FontStyle)font.FontStyle);
        }

        public int GetCellDescent(ICoreFont font)
        {
            return this.m_fontFamily.GetCellDescent((FontStyle)font.FontStyle);
        }

        public int GetLineSpacing(ICoreFont font)
        {
            return this.m_fontFamily.GetLineSpacing((FontStyle)font.FontStyle);
        }

        public int GetEmHeight(ICoreFont font)
        {
            return this.m_fontFamily.GetEmHeight((FontStyle)font.FontStyle);
        }
    }
}

