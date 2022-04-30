

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreDesignResourceManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.IO;
using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.WinUI.Design
{
    public sealed class WinCoreDesignResourceManager : ICoreResourceManager
    {
        public ICoreBitmap CreateBitmapFromStringData(string stringData)
        {
            return null;
        }

        public ICoreBitmap CreateBitmap(int width, int height)
        {
            return null;
        }

        public ICoreBitmap CreateBitmapFromFile(string value)
        {
            return null;
        }

        public ICoreBitmap CreateBitmap(System.IO.Stream stream)
        {
            return null;
        }

        public ICoreGraphics CreateDevice(object obj)
        {
            return null;
        }

       

        public ICorePathStringDefinition CreatePathStringDefinition(string text, Rectanglef bounds, CoreFont coreFont, enuStringAlignment halignment, enuStringAlignment valignment)
        {
            return null;
        }

        public ICoreCursor GetCusor(ICoreBitmap bmp)
        {
            return null;
        }

        public CoreFont CreateFont(string fontName, float size, enuFontStyle style, enuRenderingMode mode)
        {
            CoreFont winFont = CoreFont.CreateFont(fontName, size, style, mode);
            return winFont;
        }
        public ICoreFont CreateFont(string fontName, float size, enuFontStyle enuFontStyle, enuGraphicUnit enuGraphicUnit)
        {
            CoreFont winFont = CoreFont.CreateFont(fontName, size, enuFontStyle, enuGraphicUnit == enuGraphicUnit.Pixel ? enuRenderingMode.Pixel : enuRenderingMode.Vector );
            return winFont;
        }

        public ICoreFontInfo CreateFontInfo(string fontname)
        {
            ICoreFontInfo c = new WinCoreDesignFontInfo(fontname);
            return c;
        }

        public bool RegisterPrivateFont(string filename)
        {
            return false;
        }

        public Size2f MeasureString(string text, ICoreFont font)
        {
            if (string.IsNullOrEmpty(text) || (font == null))
                return Size2f.Empty;

            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                var d = new Font (font.FontName, font.FontSize , (FontStyle)font.FontStyle );
                var s = g.MeasureString(text, d);
                d.Dispose();
                return new Size2f (s.Width, s.Height );
            }
            
        }
        public ICoreZipReader GetZipReader()
        {
            return null;
        }


        internal  class WinCoreDesignFontInfo : ICoreFontInfo, IDisposable 
    {
        private string fontName;
        private FontFamily m_fontFamily;
        public object  FontFamily { get { return this.m_fontFamily; } }
        private WinCoreDesignFontInfo()
        { 
        }
        public static WinCoreDesignFontInfo CreateFontFromFile(string file)
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
                WinCoreDesignFontInfo rf = new WinCoreDesignFontInfo();
                rf.m_fontFamily = r;
                pc.Dispose();
                return rf;
            }
            return null;

        }
        public WinCoreDesignFontInfo(string fontName)
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
        public int GetCellAscent(ICoreFont style)
        {
            return this.m_fontFamily.GetCellAscent((FontStyle)style.FontStyle );
        }
        public int GetCellDescent(ICoreFont style)
        {
            return this.m_fontFamily.GetCellAscent((FontStyle)style.FontStyle);
        }
        public int GetLineSpacing(ICoreFont style)
        {
            return this.m_fontFamily.GetCellAscent((FontStyle)style.FontStyle);
        }
        public int GetEmHeight(ICoreFont style)
        {
            return this.m_fontFamily.GetCellAscent((FontStyle)style.FontStyle);
        }
    }




        public ICore.IO.Files.XCursor CreateCursor(string key)
        {
            return null;
        }


        public ICoreBitmap CreateBitmap(object obj)
        {
            if (obj is Bitmap)
            {
                WinCoreBitmap bmp = WinCoreBitmap.Create(obj as Bitmap);
                return bmp;
            }
            return null;
        }


        public Rectanglef MeasureString(string text, ICoreFont coreFont, Rectanglef rc, int startIndex, int Length)
        {
            return Rectanglef.Empty;
        }


        public Rectanglef GetStringRangeBounds(string text, ICoreFont font, Rectanglef bounds, int from, int length)
        {
            throw new NotImplementedException();
        }


        public ICoreIcon GetIcon(string name)
        {
            return null;
        }
        public void Init()
        {
        }


        public bool IsNotAGkdsDocument(string key)
        {
            return false;
        }
    }
}
