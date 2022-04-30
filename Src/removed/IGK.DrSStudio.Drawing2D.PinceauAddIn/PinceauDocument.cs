

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PinceauDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:PinceauDocument.cs
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
﻿
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Tools;
    [CoreWorkingObject ("PinceauDocument")]
    class PinceauDocument : Core2DDrawingDocumentBase 
    {
        internal const string FILE_EXTENSION = "gkps";
        internal const string FILE_COMMENT = "THIS FILE CONTAIN PINCIL STYLE EXTENSION - AUTHOR : C.A.D BONDJE DOUE @ IGKDEV 2008-2012";
        public PinceauDocument()
            : base("32 px", "32 px")
        {
            this.BackgroundTransparent = true;
            this.PixelOffset = enuPixelOffset.HightQuality;
            this.InterpolationMode = enuInterpolationMode.Hight;
            this.SmoothingMode = enuSmoothingMode.AntiAliazed ;
            this.CurrentLayer.Clear();
        }
        /// <summary>
        /// save to file name
        /// </summary>
        /// <param name="filename"></param>
        public void SaveStyle(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return;
            FileStream f = File.Create(filename);
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.CloseOutput = true;
            XmlWriter xw = XmlWriter.Create(f, setting);
            IGK.DrSStudio.Codec.CoreXMLSerializer seri =
                IGK.DrSStudio.Codec.CoreXMLSerializer.Create(xw);
            seri.WriteStartElement(FILE_EXTENSION);
            seri.WriteComment(FILE_COMMENT);
            foreach (ICore2DDrawingElement l in this.CurrentLayer.Elements)
            {
                l.Serialize (seri );
            }
            seri.WriteEndElement();
            seri.Flush();
            seri.Close();
        }
        /// <summary>
        /// get the style
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        internal static Core2DDrawingLayeredElement[] GetStyle(string filename)
        {
            if (string.IsNullOrEmpty(filename) || (File.Exists(filename) == false)) return null;
            FileStream f = File.Open(filename, FileMode.Open);
            XmlReader xr = XmlReader.Create(f);
            IGK.DrSStudio.Codec.CoreXMLDeserializer deseri =
                IGK.DrSStudio.Codec.CoreXMLDeserializer.Create(xr);
            List<Core2DDrawingLayeredElement > lbm = new List<Core2DDrawingLayeredElement>();
            bool v_isgksp = false;
            while (deseri.Read())
            {
                switch (deseri.NodeType)
                {
                    case XmlNodeType.Element:
                        if (deseri.Name.ToLower() == FILE_EXTENSION)
                        {
                            v_isgksp = true;
                        }
                        else
                        {
                            if (v_isgksp)
                            {
                                Core2DDrawingLayeredElement o =
                                    CoreSystem.CreateWorkingObject (deseri.Name)
                                    as
                                    Core2DDrawingLayeredElement
                                    ;
                                if (o != null)
                                {
                                    o.Deserialize (deseri);
                                    lbm.Add(o);
                                }
                            }
                        }
                        break;
                }
            }
            deseri.Close();
            f.Close();
            return lbm.ToArray();
        }
        /// <summary>
        /// create Pinceau from a layer item
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static PinceauDocument CreateDocument(Core2DDrawingLayeredElement items)
        {
            if (items == null)
                return null;
            Core2DDrawingLayeredElement v_i = items.Clone() as Core2DDrawingLayeredElement;
            PinceauDocument doc = new PinceauDocument();
            doc.CurrentLayer.Elements.Add (v_i);
            v_i.Dock(enuCore2DDockElement.DockFill,doc.Bounds );
            v_i.ResetTransform();
            return doc;
        }
    }
}

