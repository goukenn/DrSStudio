

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XIconSurface.cs
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
file:XIconSurface.cs
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
/* This file is part of iGK-DrawingSoft.
*    iGK-DrawingSoft is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    iGK-DrawingSoft is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU Lesser General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
namespace IGK.DrSStudio.XIcon
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Codec;    
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI;
    [CoreSurface ("IconSurface")]
    sealed class XIconSurface : IGKD2DDrawingSurface  
    {
        XIconProject m_project;
        private bool m_vistaIcon;
        public override bool AllowMultiDocument
        {
            get
            {
                return (!this.VistaIcon);
            }
        }
        public class IconDocumentCollection : IGKD2DDrawingSurface.DocumentCollection
        {
            public IconDocumentCollection(XIconSurface surface):base( surface )
            {
            }          
        }
        public bool VistaIcon { get { return this.m_vistaIcon; } }
        public new XIconProject Project { get { return this.m_project;  } }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_project != null)
                {
                    m_project.Dispose();
                    m_project = null;
                }
            }
            base.Dispose(disposing);
        }
        public XIconSurface(ICore2DDrawingDocument [] document)
        {
            if (document.Length > 0)
            {
                ICore2DDrawingDocument d = this.CurrentDocument;
                for (int i = 0; i < document.Length; i++)
                {
                    this.Documents.Add(document[i] as Core2DDrawingDocumentBase );
                }
                this.Documents.Remove(d as Core2DDrawingDocumentBase );
                this.CurrentDocument = this.Documents[0];
            }
            this.m_vistaIcon = false;
            this.NeedToSave = false;
        }
        protected override Core2DDrawingDocumentBase CreateNewDocument()
        {
            return base.CreateNewDocument();
        }
        protected override IGKD2DDrawingSurfaceBase.DocumentCollection CreateDocumentCollections()
        {
            return new IconDocumentCollection(this);
        }
        //vista icon  constructor
        public XIconSurface()
            : base()
        {            
            this.CurrentDocument.Clear();
            this.CurrentDocument.SetSize(256, 256);
            this.m_vistaIcon = true;
            this.FileName = "Windows.ico";
        }
        public new static XIconSurface CreateSurface(GKDSElement project)
        {
            List<ICore2DDrawingDocument> m = new List<ICore2DDrawingDocument>();
            //foreach (ICoreWorkingDocument c in project.)
            //{
            //    if (c is ICore2DDrawingDocument)
            //        m.Add(c as ICore2DDrawingDocument);
            //}
            XIconSurface v_surface = new XIconSurface();//m.ToArray());
            //v_surface.FileName = project["FileName"].Value;
            return v_surface;
        }
        public override  void Save()
        {
            if ((this.m_project == null) || string.IsNullOrEmpty (this.FileName ) || (!System.IO.File .Exists (this.FileName) ))
            {
                SaveAs (this.FileName );
            }
            else {
            Bitmap bmp = null;
            Graphics g = null;
            if (this.m_project == null) 
                return;
            for (int i = 0; i < this.m_project.Icons.Count; i++)
            {
                bmp = this.m_project.Icons.GetImage(i);
                g = Graphics.FromImage(bmp);
                g.CompositingMode = CompositingMode.SourceCopy;
                g.Clear(Color.Transparent);
                g.CompositingMode = CompositingMode.SourceOver;
                //this.CurrentDocument.SetGraphicsProperty(g);
                this.CurrentDocument.Draw(g, new Rectangle(0, 0, bmp.Width, bmp.Height));
                g.Flush();
                g.Dispose();
            }
                this.m_project.Icons.Save(FileName);
                this.NeedToSave = false ;
            }            
        }
        public  void InsertFile(string filename)
        {
            /*   if (!File.Exists(filename)) return;
            string ext = System.IO.Path.GetExtension(filename);
            //aucune extension trouver
            if (string.IsNullOrEmpty(ext)) return;
            ICoreDecoder decoder = CoreSystem.GetDecoders (ext)[0] as ICoreDecoder ;
            ICoreWorkingDocument[] document = null;
            if (decoder !=null)
            {
                //choose the first decoder and open document
                document = decoder.Open(filename);
                if (decoder.MimeType == "image/layered-gkds")
                {
                    //insert new document
                    foreach (Core2DDrawingDocument doc in document)
                    {
                        if (doc == null) continue;
                        foreach (Core2DDrawingLayer l in doc.Layers)
                        {
                            this.CurrentDocument.Layers.Add(l);
                        }
                    }
                }
            }        */
        }
        public override void SaveAs(string filename)
        {
            Bitmap bmp = null;
            Graphics g = null;
            if (this.m_project == null)
            {
                base.SaveAs(filename);
            }
            else
            {
                    //sfd.Title = "Save As Vista Icon";
                    //sfd.Filter = "Vista Icons Files|*.ico|gkds. file |*.gkds";
                        switch (System.IO.Path.GetExtension(filename.ToLower()))
                        {
                            case ".ico":
                                //save to ico's file 
                                for (int i = 0; i < this.m_project.Icons.Count; i++)
                                {
                                    bmp = this.m_project.Icons.GetImage(i);
                                    if (bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined)
                                    {
                                        System.Windows.Forms.MessageBox.Show("Bitmap is undefined");
                                    }
                                    g = Graphics.FromImage(bmp);
                                    g.Clip = new Region(new RectangleF(Point.Empty, bmp.Size));
                                    g.CompositingMode = CompositingMode.SourceCopy;
                                    g.Clear(Color.Transparent);
                                    g.CompositingMode = CompositingMode.SourceOver;
                                    //this.CurrentDocument.Design = false;
                                    this.CurrentDocument.Draw(g, new Rectangle(0, 0, bmp.Width, bmp.Height));
                                    //this.CurrentDocument.Design = d;
                                    g.Flush();
                                    g.Dispose();
                                }
                                this.m_project.Icons.Save(filename );
                                this.m_project.Icons.ResetImage();                                
                                IGK.DrSStudio.Codec.CoreEncoder.Instance.SaveWithEncoder(
                            null, this, this.FileName, this.Documents.ToArray());
                                this.FileName = filename;
                                this.NeedToSave = false;
                                break;
                            default :
                        if (File.Exists(this.FileName) == false)
                            File.Create(this.FileName).Close();
                        base.Save();
                        break;
                        }
                }
            }

        /// <summary>
        /// set icon project
        /// </summary>
        /// <param name="pr"></param>
        internal void SetIconProject(XIconProject  pr)
        {
            this.m_vistaIcon = true;            
            m_project = pr;
            //var obj = this.Project["IconProject"];
            //if (obj != null)
            //{
            //    obj.Value = "true";
            //}
        }
    }        
    }

