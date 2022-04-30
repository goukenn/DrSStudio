

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCV.cs
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
file:CoreCV.cs
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
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Printing;
namespace IGK.DrSStudio
{
    
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.WinCore;
    using IGK.ICore.Drawing2D;
    using IGK.DrSStudio.Drawing2D;
    static class CoreCV
    {
        public const string CV_WIDTH = "86.5 mm";
        public const string CV_HEIGHT = "55.5 mm";
        public const string CV_TOP = "12 mm";
        public const string CV_MIDMARGIN = "15 mm";
        public const string CV_RIGHT = "12 mm";
        //2 mm to add for no reason
        //
        //public static readonly CoreUnit Width = "87 mm";//"85 mm";
        //public static readonly CoreUnit Height = "56 mm";//"54 mm";
        private static CoreUnit sm_marginTop = "12 mm";
        private static CoreUnit sm_marginMiddle = "15 mm";
        private static CoreUnit sm_marginRight = "12 mm";
        private static int m_hNumber = 2;
        private static int m_vNumber = 5;
        private static bool m_allowVerticalSpace;
        private static bool m_drawGridLine;
        static CVDocument sm_docToPrint;
        static bool sm_busy;
        public static int NumberVertical
        {
            get
            {
                return m_vNumber;
            }
            set
            {
                m_vNumber = value;
            }
        }
        public static int NumberHorizontal
        {
            get
            {
                return m_hNumber;
            }
            set
            {
                m_hNumber = value;
            }
        }
        public static bool AllowVerticalSpace
        {
            get { return m_allowVerticalSpace; }
            set { m_allowVerticalSpace = value; }
        }
        public static bool DrawGridLine
        {
            get
            {
                return m_drawGridLine;
            }
            set
            {
                m_drawGridLine = value;
            }
        }
        public static void PrintDocument(PrintDocument printDocument, CVDocument document)
        {
            PrintDocument(printDocument, document, false);
        }
        public static void PrintDocument(PrintDocument printDocument, CVDocument document, bool preview)
        {
            if ((printDocument == null) || (document == null) || sm_busy)
                return;
            sm_docToPrint = document;
            printDocument.PrintPage += new PrintPageEventHandler(document_PrintPage);
            printDocument.BeginPrint += new PrintEventHandler(printDocument_BeginPrint);
            printDocument.PrintController = new StandardPrintController();
            if (!preview)
            {
                try
                {
                    printDocument.Print();
                }
                catch (Exception ex)
                {
                    CoreMessageBox.Show("Error When Printring \n" + ex.Message);
                }
                return;
            }
        }
        static void printDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            //begin printing
        }
        private static void document_PrintPage(object sender, PrintPageEventArgs e)
        {
            sm_busy = true;
            e.Graphics.PageUnit = GraphicsUnit.Pixel;
            PrintDocument doc = (PrintDocument)sender;
            int dpix = (int)e.Graphics.DpiX;
            int dpiy = (int)e.Graphics.DpiY;
            //int v_bkdpix = (int)sm_docToPrint.DpiX;
            //int v_bkdpiy = (int)sm_docToPrint.DpiY;
            sm_marginTop = new CoreUnit(CV_TOP, dpiy);
            sm_marginMiddle = new CoreUnit(CV_MIDMARGIN, dpix);
            sm_marginRight = new CoreUnit(CV_RIGHT, dpix);
            //get cv mode size
            CoreUnit v_cvWidth = "0";
            CoreUnit v_cvHeight = "0";
            switch (sm_docToPrint.CVModel)
            { 
                case enuCVModel.Horizontal :
                    v_cvWidth = new CoreUnit(CV_WIDTH, dpix);
                    v_cvHeight = new CoreUnit(CV_HEIGHT, dpiy);
                    break;
                case enuCVModel.Vertical :
                    v_cvHeight = new CoreUnit(CV_WIDTH, dpiy);
                    v_cvWidth = new CoreUnit(CV_HEIGHT, dpix);
                    break;
            }
            CoreUnit v_paperWidth = new CoreUnit((e.PageBounds.Width * dpix / CoreScreen.DpiX ), dpix);
            CoreUnit v_paperHeight = new CoreUnit((e.PageBounds.Height * dpiy) /
                CoreScreen.DpiY 
                 , dpiy);
            float tMargin = ((ICoreUnitPixel)sm_marginTop).Value;
            float rMargin = ((ICoreUnitPixel)sm_marginRight).Value;
            float mid = AllowVerticalSpace ? ((ICoreUnitPixel)sm_marginMiddle).Value : 0;
            //get cv value
            float v_w = ((ICoreUnitPixel)v_cvWidth).Value;
            float v_h = ((ICoreUnitPixel)v_cvHeight).Value;
            //get paper value
            float v_H = ((ICoreUnitPixel)v_paperHeight).Value;
            float v_W = ((ICoreUnitPixel)v_paperWidth).Value;
            int w = (int)Math.Floor(v_w);
            int h = (int)Math.Floor(v_h);
            float v_mtop = tMargin;
            GraphicsState state = null;
            Rectanglef rc = new Rectanglef(0, 0, v_w, v_h);
            Rectanglef v_pageRc = new Rectanglef(0, 0, v_W, v_H);
            v_pageRc.Width -= rMargin;
            v_pageRc.Height -= tMargin;
           // sm_docToPrint.SetDpi(e.Graphics.DpiX, e.Graphics.DpiY);
            Graphics g = e.Graphics;
            enuRenderingMode  v_gMode = sm_docToPrint.RenderingMode;
            sm_docToPrint.RenderingMode = enuRenderingMode.Vector;
            Colorf v_lineColor = Colorf.FromFloat(1.0f, 0.7f);
            Pen v_lpen = CoreBrushRegisterManager.GetPen<Pen>(v_lineColor);
            v_lpen.DashStyle = DashStyle.Dash;
            int numInLine = (int)Math.Floor(v_pageRc.Width / (rc.Width + mid));
            int numInColumn = (int)Math.Floor(v_pageRc.Height / rc.Height);
            float cas = tMargin + ((numInColumn) * rc.Height);
            if (cas > v_pageRc.Height)
            {
                numInColumn--;
            }
            float v_posx = 0.0f;
            float v_posy = tMargin;
            if (DrawGridLine)
            {
                v_posx = rMargin;
                int vline = (mid > 0) ? numInLine * 2 : numInLine + 1;
                for (int i = 0; i < vline; i++)
                {
                    e.Graphics.DrawLine(v_lpen,
                    v_posx, 0,
                    v_posx,
                    v_H);
                    if ((mid > 0) && (i > 0) && (i % 2 != 0))
                    {
                        v_posx += mid;
                    }
                    else
                        v_posx += v_w;
                }
                for (int i = 0; i < numInColumn + 1; i++)
                {
                    e.Graphics.DrawLine(v_lpen,
                          0, tMargin + (i * v_h), v_W,
                          tMargin + (i * v_h));
                }
            }
            for (int i = 0; i < numInColumn; i++)
            {
                state = e.Graphics.Save();
                v_posx = rMargin;
                for (int j = 0; j < numInLine; j++)
                {
                    e.Graphics.ResetTransform();
                    //e.Graphics.TranslateTransform(v_posx, v_posy, MatrixOrder.Append);
                    e.Graphics.Clip = new Region(new RectangleF(v_posx, v_posy, rc.Width, rc.Height));
                    sm_docToPrint.Draw(g, false, Rectanglef.Round(
                        new Rectanglef(v_posx, v_posy, rc.Width, rc.Height)),
                        enuFlipMode.None);
                    //sm_docToPrint.Draw(g);
                    v_posx += v_w + mid;
                }
                e.Graphics.Restore(state);
                v_posy += v_h;
            }
            sm_busy = false;
            v_lpen.DashStyle = DashStyle.Solid;
        }
    }
}

