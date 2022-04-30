

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLPrintingManager.cs
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
file:GLPrintingManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
namespace IGK.DrSStudio.GLPictureEditorAddIn
{
    
using IGK.ICore;using IGK.DrSStudio.GLPictureEditorAddIn.WinUI;
    /// <summary>
    /// represent a printing manager surface
    /// </summary>
    public class GLPrintingManager
    {
        PrintDocument m_printDocument;
        GLEditorSurface m_surface;
        bool m_preview;
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="surface"></param>
        public GLPrintingManager(GLEditorSurface surface)
        {
            this.m_surface = surface;
            this.m_printDocument = new PrintDocument();
            this.m_printDocument.BeginPrint += new PrintEventHandler(_BeginPrint);
            this.m_printDocument.PrintPage += new PrintPageEventHandler(_PrintPage);
            this.m_printDocument.QueryPageSettings += new QueryPageSettingsEventHandler(_QueryPageSettings);
        }
        void _QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
        }
        void _PrintPage(object sender, PrintPageEventArgs e)
        {
            if (this.m_preview)
            { 
            }
            //CoreUnit v_x = CoreUnit.FromHundredOfInch(e.MarginBounds.X, e.Graphics.DpiX);
            //CoreUnit v_y = CoreUnit.FromHundredOfInch(e.MarginBounds.Y, e.Graphics.DpiY);
            //CoreUnit v_w = CoreUnit.FromHundredOfInch(e.MarginBounds.Width, e.Graphics.DpiX);
            //CoreUnit v_h = CoreUnit.FromHundredOfInch(e.MarginBounds.Height, e.Graphics.DpiY);
            //Rectangle v_rc = new Rectangle((int)v_x.Value, (int)v_y.Value,
            //       (int)v_w.Value, (int)v_h.Value);
            using (Bitmap bmp = this.m_surface.SaveToBitmap())
            {
                bmp.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                e.Graphics.DrawImage(bmp, new Rectangle(e.MarginBounds.X, e.MarginBounds.Y, e.MarginBounds.Width,
                    e.MarginBounds.Height ));
            }
        }
        void _BeginPrint(object sender, PrintEventArgs e)
        {
        }
        internal void Print()
        {
            m_preview = false;
            using (PrintDialog ptd = new PrintDialog())
            {
                ptd.Document = this.m_printDocument;
                ptd.UseEXDialog = true;
                if (ptd.ShowDialog() == DialogResult.OK)
                {
                    ptd.Document.Print();
                }
            }
        }
        internal void PrintPreview()
        {
            m_preview = true;
            using (PrintPreviewDialog prv = new PrintPreviewDialog())
            {
                prv.Document = this.m_printDocument;               
                if (prv.ShowDialog() == DialogResult.OK)
                {
                    prv.Document.Print();
                }
            }
        }
    }
}

