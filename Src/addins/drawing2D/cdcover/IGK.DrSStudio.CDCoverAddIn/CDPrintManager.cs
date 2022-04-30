

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CDPrintManager.cs
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
file:CDPrintManager.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;

namespace IGK.DrSStudio.CDCoverAddin
{

    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;

    static class CDPrintManager
    {
        public const string CD_WIDTH = "116 mm";// "120 mm";//"116 mm";
        public const string CD_HEIGHT = "116 mm";//"120 mm";//"116 mm";
        public const string CD_MARGINTOP = "14.5 mm";//"15.5 mm"; // ordinary 16,5 mm
        public const string CD_MARGINLEFT = "44 mm";//"45 mm";//47
        public const string CD_VERTICALSPACE = "32 mm";//"34 mm";//32
        public static readonly CoreUnit Width = CD_WIDTH;
        public static readonly CoreUnit Height = CD_HEIGHT;
        private static readonly CoreUnit sm_marginTop = CD_MARGINTOP;
        private static readonly CoreUnit sm_marginLeft = CD_MARGINLEFT;
        private static readonly CoreUnit sm_verticalSpace = CD_VERTICALSPACE;
        static bool m_DrawRuleLine;
        static int m_numberPage = 2;
        private static CDCoverDocument sm_document;
        private static bool sm_busy;
        /// <summary>
        /// allow draw rule line
        /// </summary>
        public static bool DrawRuleLine {
            get {
                return m_DrawRuleLine;
            }
            set {
                m_DrawRuleLine = value;
            }
        }
        public static int NumberPerPage {
            get {
                return m_numberPage;
            }
            set {
                m_numberPage = value;
            }
        }
        /// <summary>
        /// get if  the item is bussy
        /// </summary>
        public static bool IsBusy {
            get {
                return sm_busy;
            }
        }
        public static bool Print(CDCoverDocument document)
        {
            if ((document == null) || (sm_busy))
                return false;
            sm_document = document;
            PrintDocument v_printDoc = new PrintDocument();
            InitPrintDocument(v_printDoc);
            StandardPrintController v_controller = new StandardPrintController();
            v_printDoc.PrintController = v_controller;
            v_printDoc.PrintPage += new PrintPageEventHandler(v_printDoc_PrintPage);
            v_printDoc.BeginPrint += new PrintEventHandler(v_printDoc_BeginPrint);
            v_printDoc.QueryPageSettings += new QueryPageSettingsEventHandler(v_printDoc_QueryPageSettings);
            using (PrintDialog prDial = new PrintDialog())
            {
                prDial.ShowHelp = true;
                prDial.ShowNetwork = true;
                prDial.UseEXDialog = true;
                prDial.Document = v_printDoc;
                if (prDial.ShowDialog() == DialogResult.OK)
                {
                    prDial.Document.Print();
                    return true;
                }
            }
            return false;
        }
        static void v_printDoc_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {
        }
        static void v_printDoc_BeginPrint(object sender, PrintEventArgs e)
        {
        }
        private static void InitPrintDocument(PrintDocument printDoc)
        {
            PageSettings v_pagesetting = new PageSettings();
            CoreUnit w = "210 mm";
            CoreUnit h = "297 mm";
            v_pagesetting.PaperSize = new PaperSize("A4 210 x 297 mm", (int)((ICoreUnitPixel)w).Value,
            (int)((ICoreUnitPixel)h).Value);
            v_pagesetting.Margins = new Margins(0, 0, 0, 0);
            v_pagesetting.Landscape = false;
            printDoc.DefaultPageSettings = v_pagesetting;
        }
        public static void PrintPreview(CDCoverDocument document)
        {
            if ((document == null)||(sm_busy))
                return;
            sm_document = document;
            PrintDocument v_printDoc = new PrintDocument();
            InitPrintDocument(v_printDoc);
            StandardPrintController v_controller = new StandardPrintController();
            v_printDoc.PrintController = v_controller;
            v_printDoc.PrintPage += new PrintPageEventHandler(v_printDoc_PrintPage);
            using (PrintPreviewDialog prDial = new PrintPreviewDialog())
            {
                prDial.ShowIcon = true;
                prDial.UseAntiAlias = true;
                prDial.Document = v_printDoc;
                prDial.ShowDialog();
            }
        }
        static void v_printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            sm_busy = true;
            GraphicsPath path = new GraphicsPath();
            GraphicsPath v_pcenter = new GraphicsPath();
            float v_dpix = e.Graphics.DpiX ;
            float v_dpiy = e.Graphics.DpiY ;
            //float v_bkdpix = sm_document.DpiX;
            //float v_bkdpiy = sm_document.DpiY;
            //sm_document.SetDpi(v_dpix, v_dpiy);
            e.Graphics.PageUnit = System.Drawing.GraphicsUnit.Pixel  ;
            CoreUnit Width = new CoreUnit(CD_WIDTH, v_dpix);
            CoreUnit Height = new CoreUnit(CD_HEIGHT, v_dpiy);
            CoreUnit sm_marginTop = new CoreUnit(CD_MARGINTOP, v_dpiy);
            CoreUnit sm_marginLeft = new CoreUnit(CD_MARGINLEFT, v_dpix);
            CoreUnit sm_verticalSpace = new CoreUnit(CD_VERTICALSPACE, v_dpiy);
            float v_w =(float) Math.Ceiling (((ICoreUnitPixel)Width).Value );
            float v_h =(float)Math.Ceiling ( ((ICoreUnitPixel)Height).Value) ;
            Rectanglef v_docrc = new Rectanglef(0, 0, (int)v_h, (int)v_h);
            Vector2f  center = CoreMathOperation.GetCenter(v_docrc);
            CoreUnit v_center = new CoreUnit("15 mm", v_dpix);
            Rectanglef rdim = CoreMathOperation.GetBounds(center, (int)((ICoreUnitPixel)v_center ).Value / 2);
            v_pcenter.AddEllipse(rdim.X, rdim.Y,rdim.Width,rdim.Height );
            v_pcenter.CloseFigure();
            path.SetMarkers();
            path.AddRectangle(new Rectanglef (v_docrc.X, v_docrc.Y, v_docrc.Width, v_docrc.Height ));
            path.AddEllipse(v_docrc.X, v_docrc.Y, v_docrc.Width, v_docrc.Height);
            path.CloseFigure();
            float tMargin = (float)Math.Ceiling (((ICoreUnitPixel)sm_marginTop).Value);
            float lMargin = (float)Math.Ceiling (((ICoreUnitPixel)sm_marginLeft).Value);
            float sMargin = (float)Math.Ceiling (((ICoreUnitPixel) sm_verticalSpace).Value);
            Graphics g = e.Graphics;
            //if (DrawRuleLine)
            //{
            Pen v_pen = CoreBrushRegisterManager.GetPen<Pen>(Colorf.Gray);
                g.DrawLine(v_pen, lMargin, 0, lMargin, e.PageBounds.Height * v_dpiy / CoreScreen.DpiY);
                g.DrawLine(v_pen, lMargin + v_w, 0, lMargin + v_w, e.PageBounds.Height * v_dpiy / CoreScreen.DpiY);
                g.DrawLine(v_pen, 0, tMargin, e.PageBounds.Width * v_dpix / CoreScreen.DpiX, tMargin);
                g.DrawLine(v_pen, 0, tMargin + v_h, e.PageBounds.Width * v_dpix / CoreScreen.DpiX, tMargin + v_h);
                g.DrawLine(v_pen, 0, tMargin + v_h + sMargin, e.PageBounds.Width * v_dpix / CoreScreen.DpiX, tMargin + v_h + sMargin);
                g.DrawLine(v_pen, 0, tMargin + 2 * v_h + sMargin, e.PageBounds.Width * v_dpix / CoreScreen.DpiX, tMargin + 2 * v_h + sMargin);
           // }
            RectangleF rc = new RectangleF(0, 0,
               v_w, v_h);
            Rectangle v_rc = new Rectangle(
                new Point((int)lMargin,(int) tMargin),
                new Size((int)v_w, (int)v_h));
            GraphicsState s = e.Graphics.Save();
            e.Graphics.ResetTransform();
            sm_document.Draw (e.Graphics, false, enuFlipMode.None,
                v_rc.X,
                v_rc.Y,
                v_rc.Width ,
                v_rc.Height 
                );
            if (NumberPerPage == 2)
            {
                e.Graphics.ResetTransform();
                tMargin = tMargin + v_h + sMargin;
                v_rc = new System.Drawing.Rectangle(
                new Point((int)lMargin, (int)tMargin),
                new Size((int)v_w, (int)v_h));
                sm_document.Draw(e.Graphics, false, 
                    enuFlipMode.None,
                         v_rc.X,
                v_rc.Y,
                v_rc.Width,
                v_rc.Height);    
            }
            //sm_document.SetDpi(v_bkdpix, v_bkdpiy);
            e.Graphics.Restore(s);
            sm_busy = false;
        }
    }
}

