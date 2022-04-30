

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ClipBoard.cs
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
file:ClipBoard.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D.WinUI
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent a marshaling surface 
    /// </summary>
    public static class IGK2DDClipBoard
    {
        public const string TAG_DRAWING2DELEMENT = "Drawing2DElement";
        public const string TAG_COPY_CONTEXT = "Copy";
        public const string TAG_CUT_CONTEXT = "Cut";
        static ICoreWorkingSurface sm_Source;
        /// <summary>
        /// get source from the copy cut operation
        /// </summary>
        public static ICoreWorkingSurface Source {
            get {
                return sm_Source;
            }
        }
        /// <summary>
        /// copy layered element to clipboard
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="source"></param>
        /// <param name="v_items"></param>
        public static void CopyToClipBoard(string tagName, ICoreWorkingSurface source,
            ICore2DDrawingLayeredElement[] v_items)
        {
            if (string.IsNullOrEmpty(tagName))
                throw new CoreException(enuExceptionType.ArgumentIsNull, "tagName");
            if (source == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "source");
            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            string v_out = string.Empty ;
            System.Xml.XmlWriterSettings v_setting = new System.Xml.XmlWriterSettings();
            v_setting.OmitXmlDeclaration = true;
            CoreXMLSerializer v_seri = CoreXMLSerializer.Create(System.Xml.XmlWriter.Create(mem, v_setting)) as CoreXMLSerializer;
            v_seri.EmbedBitmap = true;
            //Create a start element
            v_seri.WriteStartElement(tagName);
            for (int i = 0; i < v_items.Length; i++)
            {
                v_items[i].Serialize(v_seri);               
            }
            v_seri.WriteEndElement();
            v_seri.Close();
            if (!v_seri.Error)
            {
                mem.Seek(0, System.IO.SeekOrigin.Begin);
                v_out = new System.IO.StreamReader(mem).ReadToEnd();
             
                if (WinCoreClipBoard.Copy(TAG_DRAWING2DELEMENT, v_out))
                {
                    sm_Source = source;
                }
            }
            mem.Dispose();
        }
        public static void CopyToClipBoard(System.Drawing.Bitmap bmp)
        {
            if (bmp != null)
            {
                System.Windows.Forms.Clipboard.SetImage(bmp);
            }
        }
    }
}

