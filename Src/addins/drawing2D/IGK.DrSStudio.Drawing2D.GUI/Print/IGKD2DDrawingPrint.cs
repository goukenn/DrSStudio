

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingPrint.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing2D.Print
{

    using IGK.ICore;
    using IGK.ICore.Drawing2D.Print;
    using System.Windows.Forms;

    /// <summary>
    /// used for printing 2D document
    /// </summary>
    class IGKD2DDrawingPrint : ICorePrinter, IDisposable 
    {
        private IGKD2DDrawingSurface m_suface;
        private PrintDocument prt;
        private int m_Index;

        public int Index
        {
            get { return m_Index; }
            set
            {
                if (m_Index != value)
                {
                    m_Index = value;
                }
            }
        }

        public IGKD2DDrawingPrint(IGKD2DDrawingSurface surface)
        {
            this.m_suface = surface;
            prt = new PrintDocument();
            prt.PrintPage += prt_PrintPage; 
            this.m_Index = 0;
            
        } 

        void prt_PrintPage(object sender, PrintPageEventArgs e)
        {
            PrintPage(e);
        }

        protected virtual void PrintPage(PrintPageEventArgs e)
        {
            int c = this.m_suface.Documents.Count;
            if ((this.Index < 0) || (this.Index >= c))
                return;

            ICore2DDrawingDocument doc = this.m_suface.Documents[this.m_Index];
            var s = e.Graphics.Save();
            doc.Draw(e.Graphics);
            e.Graphics.Restore(s);

            this.m_Index++;
            e.HasMorePages = (this.Index < c);
        }
        public void Print()
        {
            CoreLog.WriteDebug("Print");
            this.prt.Print();
        }
        public void PrintPreview()
        {
            CoreLog.WriteDebug("Print Preview");
            using (PrintPreviewDialog ptrDial = new PrintPreviewDialog()) {
                ptrDial.Document = this.prt;
                if (ptrDial.ShowDialog() == DialogResult.OK) {
                    ptrDial.Document.Print();
                }
            }
        }


        public void Dispose()
        {
            this.prt.Dispose();
        }
    }
}
