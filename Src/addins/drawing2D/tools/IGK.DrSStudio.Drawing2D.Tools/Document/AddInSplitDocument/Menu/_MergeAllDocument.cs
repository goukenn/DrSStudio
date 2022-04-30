

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _MergeAllDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_MergeAllDocument.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore.WinUI.Configuration;
    [DrSStudioMenu("Tools.MergeAllDocument", 12)]
    class _MergeAllDocument : 
        Core2DDrawingMenuBase ,
        ICoreWorkingConfigurableObject 
    {
        private enuMergeDocumentType m_Style;
        private float m_DpiX;
        private float m_DpiY;
        private enuMergeSaveFormat m_SaveFormat;
        public _MergeAllDocument()
        {
            m_DpiX = CoreScreen.DpiX;
            m_DpiY = CoreScreen.DpiY;
        }
        public enuMergeSaveFormat SaveFormat
        {
            get { return m_SaveFormat; }
            set
            {
                if (m_SaveFormat != value)
                {
                    m_SaveFormat = value;
                }
            }
        }
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
        public enuMergeDocumentType Style
        {
            get { return m_Style; }
            set
            {
                if (m_Style != value)
                {
                    m_Style = value;
                }
            }
        }
        protected override bool PerformAction()
        {
            if (Workbench.ConfigureWorkingObject(this,
                "title.mergeAlldocument".R(), false, Size2i.Empty).Equals(enuDialogResult.OK))
            {
                //merging all document
                //--------------------
                using (Bitmap bmp = MergeDocument())
                {
                    string v_str = "Pictures Files |";
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        System.Drawing.Imaging.ImageFormat v_imgFormat =
                            System.Drawing.Imaging.ImageFormat.Bmp;
                        v_str += "*." + this.SaveFormat.ToString()+";";
                        sfd.Title = "title.dlg.savemerge_all".R();
                        switch (this.SaveFormat)
                        {
                            case enuMergeSaveFormat.BMP:
                                v_imgFormat =
                            System.Drawing.Imaging.ImageFormat.Bmp;
                                break;
                            case enuMergeSaveFormat.PNG:
                                v_imgFormat =
                            System.Drawing.Imaging.ImageFormat.Png;
                                break;
                            case enuMergeSaveFormat.JPEG:
                                v_imgFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                                break;
                            case enuMergeSaveFormat.GIF:
                                v_imgFormat = System.Drawing.Imaging.ImageFormat.Gif;
                                break;
                            default:
                                break;
                        }
                        sfd.Filter = v_str;
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                bmp.Save(sfd.FileName, v_imgFormat);
                            }
                            catch {
                                CoreMessageBox.Show(
                                    "Impossible d'enregistrer le fichier", "error when saving", enuCoreMessageBoxButtons.Ok );
                            }
                        }
                    }
                }
            }
            return false;
        }
        #region ICoreWorkingConfigurableObject Members
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var group = parameters.AddGroup("Definition");
            group.AddItem(GetType().GetProperty("Style"),"lb.MergeAllDocStyle");
            group.AddItem(GetType().GetProperty("SaveFormat"), "lb.MergeAllDocCodec");
            return parameters;
        }
        public ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        #endregion
        public Bitmap  MergeDocument()
        {
           ICore2DDrawingDocument[] v_docs = (ICore2DDrawingDocument[]) this.CurrentSurface.Documents.ToArray();
           int w = 0;
           int h = 0;
            int posx = 0;
            int posy = 0;
           Bitmap v_outBmp = null;
           Bitmap v_docBmp = null;
           Graphics g = null;
           switch (this.Style)
           {
               case enuMergeDocumentType.Horizontal:
                   for (int i = 0; i < v_docs.Length; i++)
                   {
                       v_docBmp = v_outBmp ;
                       w += v_docs[i].Width;
                       h = Math.Max (h, v_docs[i].Height );
                       v_outBmp = new Bitmap(w, h);
                       using (g = Graphics.FromImage(v_outBmp))
                       {
                           if (v_docBmp != null)
                           {
                               g.DrawImage(v_docBmp, Point.Empty);
                               v_docBmp.Dispose();
                           }
                           g.TranslateTransform(posx, posy);
                           v_docs[i].Draw(g);
                       }
                       posx = w;
                   }
                   break;
               case enuMergeDocumentType.Vertical:
                   for (int i = 0; i < v_docs.Length; i++)
                   {
                       v_docBmp = v_outBmp;
                       h += v_docs[i].Height ;
                       w = Math.Max(w, v_docs[i].Width );
                       v_outBmp = new Bitmap(w, h);
                       using (g = Graphics.FromImage(v_outBmp))
                       {
                           if (v_docBmp != null)
                           {
                               g.DrawImage(v_docBmp, Point.Empty);
                               v_docBmp.Dispose();
                           }
                           g.TranslateTransform(posx, posy);
                           v_docs[i].Draw(g);
                       }
                       posy = h;
                   }
                   break;
               default:
                   break;
           }
         return   v_outBmp;
        }
    }
}

