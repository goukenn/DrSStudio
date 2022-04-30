

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DPictureEncoderBase.cs
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
file:IGKD2DPictureEncoderBase.cs
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
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Reflection;
namespace IGK.DrSStudio.Drawing2D.Codec
{
    using IGK.ICore.WinCore;
using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore;
    using IGK.ICore.Drawing2D.Codec;
    using IGK.ICore.Drawing2D;
    /// <summary>
    /// reprsent a base picture encoder
    /// </summary>
    public abstract class IGKD2DPictureEncoderBase : 
        CoreEncoderBase,
        ICoreBitmapEncoder
    {
        //jpeg data
        internal const int JPEG_DATA_UNICHAR = 0x01;
        internal const int JPEG_DATA_CHAR = 0x02;
        //jpeg value
        internal const int JPEG_COPYRIGHT = 33432;
        internal const int JPEG_COMMENT = 37510;
        //jpeg additionale value
        internal const int JPEG_TITLE = 40091;
        internal const int JPEG_AUTHORCOMMENT = 40092;
        internal const int JPEG_AUTHOR = 40093;
        internal const int JPEG_SUBJECT = 40095;
        //jpeg other value
        internal const int JPEG_CAMERANAME = 0x10F;
        internal const int JPEG_CAMERAMODEL = 0x110;
        //to write program name
        internal const int JPEG_PROGNAME = 0x131;
        private bool m_AdditionalInfo;
        public bool AdditionalInfo
        {
            get { return m_AdditionalInfo; }
            set
            {
                if (m_AdditionalInfo != value)
                {
                    m_AdditionalInfo = value;
                }
            }
        }
        internal static PropertyItem GetUniCharPropertyItem(int code, string value)
        {
            PropertyItem item = null;
            ConstructorInfo ctr = typeof(PropertyItem).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                null, new Type[0], null);
            if (ctr != null)
            {
                item = ctr.Invoke(null) as PropertyItem;
                item.Id = code;
                item.Type = JPEG_DATA_UNICHAR;
                item.Len = value.Length * 2;
                byte[] v_t = new byte[value.Length * 2];
                IntPtr v_alloc = Marshal.StringToCoTaskMemUni(value);
                Marshal.Copy(v_alloc, v_t, 0, v_t.Length);
                item.Value = v_t;
                Marshal.FreeCoTaskMem(v_alloc);
            }
            return item;
        }
        internal static PropertyItem GetCharPropertyItem(int code, string value)
        {
            PropertyItem item = null;
            ConstructorInfo ctr = typeof(PropertyItem).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                null, new Type[0], null);
            if (ctr != null)
            {
                item = ctr.Invoke(null) as PropertyItem;
                item.Id = code;
                item.Type = JPEG_DATA_CHAR;
                item.Len = value.Length + 1;
                byte[] v_t = new byte[value.Length + 1];
                IntPtr v_alloc = Marshal.StringToCoTaskMemAnsi(value);
                Marshal.Copy(v_alloc, v_t, 0, v_t.Length);
                item.Value = v_t;
                Marshal.FreeCoTaskMem(v_alloc);
            }
            return item;
        }
        public static void SetPropertyItem(Image bmp, string filename)
        {
            if (bmp is Metafile)
            {
                return;
            }            
            bmp.SetPropertyItem(GetCharPropertyItem(JPEG_PROGNAME, CoreApplicationManager.Application.AppName));
            bmp.SetPropertyItem(GetCharPropertyItem(JPEG_COPYRIGHT, CoreApplicationManager.Application.Copyright));
            bmp.SetPropertyItem(GetUniCharPropertyItem(JPEG_TITLE, System.IO.Path.GetFileNameWithoutExtension(filename)));
            bmp.SetPropertyItem(GetUniCharPropertyItem(JPEG_AUTHOR, Environment.UserName));
        }
        private float m_DpiX;
        private float m_DpiY;
        public float DpiY
        {
            get { return m_DpiY; }
            set
            {
                if (m_DpiY != value)
                {
                    m_DpiY = value;
                }
            }
        }
        public float DpiX
        {
            get { return m_DpiX; }
            set
            {
                if (m_DpiX != value)
                {
                    m_DpiX = value;
                }
            }
        }
        protected IGKD2DPictureEncoderBase()
        {
            this.m_DpiX = CoreScreen.DpiX;
            this.m_DpiY = CoreScreen.DpiY;
        }
        public override bool Save(ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            List<ICore2DDrawingDocument> m_doc = new List<ICore2DDrawingDocument>();
            for (int i = 0; i < documents.Length; i++)
            {
                if (documents[i] is ICore2DDrawingDocument)
                    m_doc.Add(documents[i] as ICore2DDrawingDocument);
            }
            if (m_doc.Count > 0)
            {
                return SaveDocument(filename, m_doc.ToArray());
            }
            return false;
        }
        protected  abstract bool SaveDocument(string filename, ICore2DDrawingDocument[] documents);
        public virtual bool SavePicture(string filename, ICoreBitmap bitmap)
        {
            return false;
        }
        public static ImageCodecInfo GetImageEncodersInfo(string name)
        {
            foreach (ImageCodecInfo info in ImageCodecInfo.GetImageEncoders())
            {
                if (info.FormatDescription == name)
                {
                    return info;
                }
            }
            return null;
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.NoConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var group = parameters.AddGroup("Resolution");
            Type t = GetType();
            group.AddItem(t.GetProperty("DpiX"));
            group.AddItem(t.GetProperty("DpiY"));
            group.AddItem(t.GetProperty("Width"));
            group.AddItem(t.GetProperty("Height"));
            return parameters;
        }
        public override  ICoreControl GetConfigControl()
        {
            return null;
        }
    }
}

