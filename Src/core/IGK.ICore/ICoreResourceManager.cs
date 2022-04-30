

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreResourceManager.cs
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
file:ICoreResourceManager.cs
*/
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.IO;
using IGK.ICore.IO.Files;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    public interface ICoreResourceManager
    {
        ICoreBitmap CreateBitmap(int width, int height);
        ICoreBitmap CreateBitmapFromFile(string value);
        ICoreBitmap CreateBitmap(System.IO.Stream stream);
        /// <summary>
        /// create bitmap from string data.
        /// </summary>
        /// <param name="stringData">string data that represent a core bitmap</param>
        /// <returns></returns>
        ICoreBitmap CreateBitmapFromStringData(string stringData);

        ICoreGraphics CreateDevice(object obj);
        ICoreFont CreateFont(string fontName, float size, enuFontStyle enuFontStyle, enuGraphicUnit enuGraphicUnit);
        ICorePathStringDefinition CreatePathStringDefinition(string text, Rectanglef bounds, CoreFont coreFont, enuStringAlignment halignment, enuStringAlignment valignment);
        /// <summary>
        /// create a cursor from bitmap
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        ICoreCursor GetCusor(ICoreBitmap bmp);
        /// <summary>
        /// create a core fonts
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        CoreFont CreateFont(string fontName, float size, enuFontStyle style, enuRenderingMode mode);
        /// <summary>
        /// create a new font info
        /// </summary>
        /// <param name="fontname"></param>
        /// <returns></returns>
        ICoreFontInfo CreateFontInfo(string fontname);

        /// <summary>
        /// register private font
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool RegisterPrivateFont(string filename);


        Rectanglef GetStringRangeBounds(string text, ICoreFont font, Rectanglef bounds, int from, int length);
        /// <summary>
        /// measure string 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        Size2f MeasureString(string text, ICoreFont font);
        /// <summary>
        /// Mesaure string range
        /// </summary>
        /// <param name="texte"></param>
        /// <param name="coreFont"></param>
        /// <param name="rc"></param>
        /// <param name="startIndex"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        Rectanglef  MeasureString(string text, ICoreFont coreFont,Rectanglef rc, int startIndex, int Length);

        /// <summary>
        /// overrided return a zip readear utility
        /// </summary>
        ICoreZipReader GetZipReader();

        XCursor CreateCursor(string key);

        /// <summary>
        /// Create bitmap from an object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        ICoreBitmap CreateBitmap(object obj);

        ICoreIcon GetIcon(string name);

        void Init();

        bool IsNotAGkdsDocument(string key);
    }
}

