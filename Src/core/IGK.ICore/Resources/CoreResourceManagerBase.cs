

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreResourceManagerBase.cs
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
file:CoreResourceManagerBase.cs
*/
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.IO;
using IGK.ICore.IO.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Resources
{
    /// <summary>
    /// CoreResourceManagerBase represent resource manager class.
    /// </summary>
    public abstract class CoreResourceManagerBase : ICoreResourceManager
    {
        public abstract ICoreBitmap CreateBitmap(int width, int height);
        public abstract ICoreBitmap CreateBitmapFromFile(string filename);
        public abstract ICoreBitmap CreateBitmapFromStringData(string stringData);

        public abstract ICoreGraphics CreateDevice(object obj);
        public abstract ICoreBitmap CreateBitmap(System.IO.Stream stream);
        public abstract ICoreFont CreateFont(string fontName, float size, enuFontStyle enuFontStyle, enuGraphicUnit enuGraphicUnit);
        public abstract ICoreCursor GetCusor(ICoreBitmap bmp);
        public abstract CoreFont CreateFont(string fontName, float size, enuFontStyle style, enuRenderingMode mode);
        public abstract ICorePathStringDefinition CreatePathStringDefinition(string text, Rectanglef bounds, CoreFont coreFont, enuStringAlignment halignment, enuStringAlignment valignment);
        public abstract ICoreFontInfo CreateFontInfo(string fontName);
        public abstract Size2f MeasureString(string text, ICoreFont font);
        public abstract Rectanglef MeasureString(string text, ICoreFont font, Rectanglef rc, int index, int length);
        /// <summary>
        /// initialize requested resources
        /// </summary>
        public virtual void Init() {
        }

        public abstract bool RegisterPrivateFont(string filename);
        /// <summary>
        /// get a zip file reader 
        /// </summary>
        /// <returns></returns>
        public abstract ICoreZipReader GetZipReader();


        public abstract XCursor CreateCursor(string key);


        public abstract ICoreBitmap CreateBitmap(object obj);

        public abstract  Rectanglef GetStringRangeBounds(string text, ICoreFont font, Rectanglef bounds, int from, int length);


        public virtual  ICoreIcon GetIcon(string name)
        {
            return CoreResources.GetObject (name) as ICoreIcon ;
        }


        public virtual bool IsNotAGkdsDocument(string key)
        {
            string v_ext = System.IO.Path.GetExtension(key);
            if ((!string.IsNullOrEmpty(v_ext)&&(v_ext=="gkds")) || (!key.ToLower().EndsWith ("gkds")))
                return true;
            return false;
        }
    }
}

