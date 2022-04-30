

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CVSurface.cs
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
file:CVSurface.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
namespace IGK.DrSStudio.WinUI
{
    
using IGK.ICore.Drawing2D.WinUI ;
    using IGK.ICore.WinUI;
    using IGK.ICore; using IGK.ICore.Drawing2D; using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI.Common;

    [CoreSurface("CVSurface", EnvironmentName=CoreConstant.DRAWING2D_ENVIRONMENT)]
    class CVSurface : IGKD2DDrawingSurface 
    {
        public new CVDocument CurrentDocument
        {
            get { return base.CurrentDocument as CVDocument; }
            set { base.CurrentDocument = value; }
        }
        public CVSurface():base()
        {
        }
        public override void Print()
        {
            CVPrintManager printManager = new CVPrintManager(this.CurrentDocument as CVDocument);
            printManager.Print();   
        }
        public override void PrintPreview()
        {
            //PrintDocument m_printdocument = new PrintDocument();
            //PageSettings v_pagesetting = new PageSettings();
            using (CVPrintControl pct = new CVPrintControl(this.CurrentDocument))
            {
                using (ICoreDialogForm dialog = CoreSystem.Instance.Workbench.CreateNewDialog(pct))
                {
                    dialog.Title = R.ngets("title.printpreviewdialog");
                    dialog.StartPosition = enuFormStartPosition.CenterParent;
                    dialog.ShowInTaskbar = false;
                    dialog.CanMaximize = false;
                    dialog.ShowDialog();
                }
            }
        }
        public new static CVSurface CreateSurface(GKDSElement gkds)
        {
            return null;
        }
        public  static CVSurface CreateSurface(ICoreProject project, ICoreWorkingDocument[] documents)
        {            
            CVSurface v_surface = new CVSurface();
            v_surface.FileName = project["FileName"].Value;
            (v_surface.Documents as CVDocumentCollection).Replace(documents);
            v_surface.NeedToSave = false;
            return v_surface;
        }
        public override void SetParam(ICoreInitializatorParam p)
        {
            base.SetParam(p);
            string s = null;
            if ((p!=null)&&(p.Contains ("type")))
                s =p["type"].ToLower ();
            else 
                s = "horizontal";
            switch(s)
            {
                case "vertical":
                    this.CurrentDocument.CVModel = enuCVModel.Vertical;
                    break;
                case "horizontal":
                default :
                    this.CurrentDocument.CVModel = enuCVModel.Horizontal;
                    break;
            }
        }
        protected override IGKD2DDrawingSurfaceBase.IGKD2DDrawingDocumentCollections CreateDocumentCollections()
        {
            return new CVDocumentCollection(this);
        }
        protected override Core2DDrawingDocumentBase CreateNewDocument()
        {
            return new CVDocument();
        }
        public override bool AllowMultiDocument
        {
            get { return false; }
        }
        /// <summary>
        /// represent the default cd document collections
        /// </summary>
        class CVDocumentCollection : IGKD2DDrawingDocumentCollections 
        {
            CVSurface m_surface;
            CVDocument m_document;
            public CVDocumentCollection(CVSurface surface):base(surface )
            {
                this.m_surface = surface;
                this.m_document = surface.CreateNewDocument() as CVDocument ;
            }
            internal void Replace(ICoreWorkingDocument[] documents)
            {
                this.m_document = documents[0] as CVDocument;
                if (this.m_document != null)
                {
                    this.m_surface.CurrentDocument.Dispose();
                    this.m_surface.CurrentDocument = this.m_document;
                }
            }
            //protected override bool CanAdd(ICore2DDrawingDocument doc)
            //{
            //    return (doc is CVDocument);
            //}
        }
    }
}

