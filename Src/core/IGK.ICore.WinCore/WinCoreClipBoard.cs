

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreClipBoard.cs
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
file:CoreClipBoard.cs
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
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
namespace IGK.ICore.WinCore
{
    using IGK.ICore.WinCore;
    using IGK.ICore.Codec;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    /// <summary>
    /// represent the clipboard wrapper
    /// </summary>
    public sealed class WinCoreClipBoard : ICoreClipboard 
    {
        private static WinCoreClipBoard sm_instance;
        private WinCoreClipBoard()
        {
        }

        public static WinCoreClipBoard Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WinCoreClipBoard()
        {
            sm_instance = new WinCoreClipBoard();
        }
        public static bool Contains(params string[] p)
        {

            foreach (string i in p)
            {
                if (Clipboard.ContainsData (i))
                    return true;
            }
            return false;
        }
        public static bool Copy(string key, object data)
        {
            if (data == null)
                return false;
            Clipboard.SetData(key, data);
            return true;
        }
        public static object GetData(params string[] Datas)
        {
            

            for (int i = 0; i < Datas.Length; i++)
            {
                if (Clipboard.ContainsData (Datas[i]))
                {
                    if ((Datas[i] == DataFormats.EnhancedMetafile )||
                        (Datas[i] == DataFormats .MetafilePict ))
                    {
                        return GetMetafileOnClipboard(CoreSystem.Instance.MainForm.Handle);
                    }
                    return Clipboard.GetData (Datas[i]);
                }
            }
            return null;
        }
        /// <summary>
        /// copy the bitmap to clip board
        /// </summary>
        /// <param name="bmp"></param>
        public static void CopyToClipBoard(System.Drawing.Image bmp)
        {
            Metafile mfile = WinCoreBitmapOperation.BuildMetaFileFromBitmap(bmp as Bitmap);
            if (mfile != null)
            {
                PutEnhMetafileOnClipboard(IntPtr.Zero, mfile);
                mfile.Dispose();
            }
        }

        public static void CopyToClipBoard(string data)
        {
            Clipboard.SetData(DataFormats.Text, data);
        }
        #region "Native Functions"
        [DllImport("user32.dll")]
        static extern bool OpenClipboard(IntPtr hWndNewOwner);
        [DllImport("user32.dll")]
        static extern bool EmptyClipboard();
        [DllImport("user32.dll")]
        static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);
        [DllImport("user32.dll")]
        static extern IntPtr GetClipboardData(uint uFormat);
        [DllImport("user32.dll")]
        static extern bool CloseClipboard();
        [DllImport("gdi32.dll")]
        static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, IntPtr hNULL);
        [DllImport("gdi32.dll")]
        static extern bool DeleteEnhMetaFile(IntPtr hemf);
        [DllImport("user32.dll")]
        static extern uint EnumClipboardFormats(uint pformat);
        #endregion
        // Metafile mf is set to a state that is not valid inside this function.
        static public bool PutEnhMetafileOnClipboard(IntPtr hWnd, Metafile mf)
        {
            if (mf == null)
                return false;
            bool bResult = false;
            IntPtr hEMF = IntPtr.Zero;
            IntPtr hEMF2 = IntPtr.Zero;
            hEMF = mf.GetHenhmetafile(); // invalidates mf
            if (!hEMF.Equals(new IntPtr(0)))
            {
                hEMF2 = CopyEnhMetaFile(hEMF, IntPtr.Zero);
                if (!hEMF2.Equals(IntPtr.Zero))
                {
                    if (OpenClipboard(hWnd))
                    {
                        if (EmptyClipboard())
                        {
                            IntPtr hRes = SetClipboardData(14 /*CF_ENHMETAFILE*/, hEMF2);
                            bResult = hRes.Equals(hEMF2);
                            CloseClipboard();
                        }
                    }
                }
                DeleteEnhMetaFile(hEMF);
            }
            return bResult;
        }
        // Metafile mf is set to a state that is not valid inside this function.
        static public Metafile GetMetafileOnClipboard(IntPtr hWnd)
        {
            Metafile m = null;           
            if (OpenClipboard(hWnd))
            {
                            uint[] format = GetSystemClipBoardFormat();
                            if (format.Length > 0)
                            {
                                IntPtr hRes = GetClipboardData(14 /*CF_ENHMETAFILE*/);
                                m = new Metafile(hRes, false);
                            }
                        CloseClipboard();
            }            
            return m;
        }
        public static void Copy(IDataObject data)
        {
            //DataObject d = data as DataObject ;            
            //string[] formats = d.GetFormats();
            //Metafile mfile = data.GetData(formats[0]) as Metafile;// CoreBitmapOperation.BuildMetaFileFromBitmap(bmp as Bitmap);
            //PutEnhMetafileOnClipboard(IntPtr.Zero, mfile);
            //mfile.Dispose();
            Clipboard.SetDataObject(data);
        }
        public static uint[] GetSystemClipBoardFormat()
        {
            List<uint > m_formats = new List<uint> ();
            uint m_format = 0;
            do{
                m_format = EnumClipboardFormats (m_format );
                if (m_format !=0)
                m_formats.Add(m_format);
            }
            while (m_format!=0);
            m_formats.ToArray ();
            return m_formats.ToArray();
        }

        public void CopyToClipboard(string dataType, object obj)
        {
            Clipboard.SetData(dataType, obj);
        }

        public string GetTextData()
        {
            return Clipboard.GetText();
        }
        /// <summary>
        /// get pasteable items
        /// </summary>
        /// <param name="m_CopyData"></param>
        /// <returns></returns>
        public static ICore2DDrawingLayeredElement[] GetPastableItems(string[] m_CopyData)
        {
            List<ICore2DDrawingLayeredElement> m_list = new List<ICore2DDrawingLayeredElement>();

            object v_obj = WinCoreClipBoard.GetData(m_CopyData);
            if (v_obj is global::System.Drawing.Image)
            {//create a image element
                global::System.Drawing.Image v_img = v_obj as global::System.Drawing.Image;
                WinCoreBitmap c = WinCoreBitmap.Create(v_img as global::System.Drawing.Bitmap);
                if (c != null)
                {
                    ImageElement img = ImageElement.CreateFromBitmap(c);
                    m_list.Add(img);
                }
                else
                {
                    WinCoreBitmap rc = WinCoreBitmap.Create(v_img.Width, v_img.Height);
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(rc.Bitmap))
                    {
                        g.DrawImage(v_img, System.Drawing.Point.Empty);
                    }
                    ImageElement img = ImageElement.CreateFromBitmap(rc);
                    m_list.Add(img);
                    rc.Dispose();
                }
            }
            else if (v_obj is ICore2DDrawingLayeredElement[])
            {
                ICore2DDrawingLayeredElement[] v_items = (ICore2DDrawingLayeredElement[])v_obj;
                m_list.AddRange(v_items);
            }
            else if (v_obj is string)
            {
                string v_s = v_obj.ToString();
                System.IO.MemoryStream mem = new System.IO.MemoryStream();
                System.IO.StreamWriter writer = new System.IO.StreamWriter(mem);
                writer.Write(v_s);
                writer.Flush();
                mem.Seek(0, System.IO.SeekOrigin.Begin);
                System.Xml.XmlReader vreader = System.Xml.XmlReader.Create(mem);
                CoreXMLDeserializer v_deseri = CoreXMLDeserializer.Create(vreader);
                try
                {
                    v_deseri.MoveToContent();


                    while (v_deseri.Read())
                    {
                        switch (v_deseri.NodeType)
                        {
                            case System.Xml.XmlNodeType.Element:
                                ICoreSerializerService v_objitem = v_deseri.CreateWorkingObject(v_deseri.Name)
                                    as ICoreSerializerService;
                                if (v_objitem != null)
                                {
                                    IXMLDeserializer v_deseri2 = v_deseri.ReadSubtree();
                                    v_objitem.Deserialize(v_deseri2);
                                    if ((v_objitem is ICore2DDrawingLayeredElement) &&
                                         v_objitem.IsValid)
                                    {
                                        m_list.Add(v_objitem as ICore2DDrawingLayeredElement);
                                    }

                                }
                                else
                                    v_deseri.ReadToEndElement();
                                break;
                        }
                    }

                }
                catch
                {
                    CoreLog.WriteDebug("Error whend deserialize element");
                }
                finally
                {
                    v_deseri.Close();
                    vreader.Close();
                    writer.Close();
                    mem.Dispose();
                }

            }
            else if (v_obj is string[])
            {
                //array of string file 
                string[] tab = v_obj as string[];
                foreach (string v_f in tab)
                {
                    if (System.IO.File.Exists(v_f))
                    {
                        ImageElement v_img = ImageElement.CreateFromFile(v_f);
                        if (v_img != null)
                        {
                            m_list.Add(v_img);
                        }
                    }
                }
            }
            return m_list.ToArray();
        }
    }
}

