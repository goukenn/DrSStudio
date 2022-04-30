

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _PrintDocument.cs
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
file:_PrintDocument.cs
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
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace IGK.DrSStudio.Presentation.Actions
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Drawing2D;
    class _PrintDocumentAction : PresentationActionBase, IPresentationActions
    {
        private int pageIndex;
        protected override bool  PerformAction()
        {
            //if (this.Mecanism.CurrentSurface.Documents.Length > 0)
            //{
                using (PrintDialog prDialog = new PrintDialog())
                {
                    PrintDocument document = new PrintDocument();
                    document = new PrintDocument();
                    document.PrintPage += new PrintPageEventHandler(Document_PrintPage);
                    document.BeginPrint += new PrintEventHandler(Document_BeginPrint);
                    pageIndex = 0;
                    prDialog.ShowNetwork = true;
                    prDialog.UseEXDialog = true;
                    prDialog.Document = document;
                    if (prDialog.ShowDialog() == DialogResult.OK)
                    {
                        pageIndex = 0;
                        prDialog.Document.Print();
                        return true;
                    }
                }
            //}
            return false;
        }
        void Document_BeginPrint(object sender, PrintEventArgs e)
        {
            this.pageIndex = 0;
            PrintDocument v_doc = sender as PrintDocument;
            v_doc.DocumentName = this.PresentationForm.FileName;
        }
        void Document_PrintPage(object sender, PrintPageEventArgs e)
        {
            ICore2DDrawingDocument doc = this.Surface.PresentationDocument.GetPage(pageIndex);
            e.Graphics.PageUnit = System.Drawing.GraphicsUnit.Pixel;
                 float dpix = e.Graphics.DpiX;
            float dpiy = e.Graphics.DpiY;
            CoreUnit v_x = CoreUnit.FromHundredOfInch(e.MarginBounds.X, dpix);
            CoreUnit v_y = CoreUnit.FromHundredOfInch(e.MarginBounds.Y, dpiy);
            CoreUnit v_w = CoreUnit.FromHundredOfInch(e.MarginBounds.Width, dpix);
            CoreUnit v_h = CoreUnit.FromHundredOfInch(e.MarginBounds.Height, dpix);
            doc.Draw(e.Graphics,
                Rectanglef.Round (new Rectanglef (v_x .Value , v_y .Value , v_w.Value , v_h.Value ))
                );
            pageIndex++;
            PrintDocument v_doc = sender as PrintDocument;
            if (v_doc.PrintController.IsPreview)
                e.HasMorePages = false;
            e.HasMorePages = this.Surface.PresentationDocument.HasMorePage(pageIndex);
        }
    }
}

