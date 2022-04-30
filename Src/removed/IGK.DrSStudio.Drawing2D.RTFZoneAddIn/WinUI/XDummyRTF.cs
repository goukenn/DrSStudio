

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XDummyRTF.cs
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
file:XDummyRTF.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    internal class XDummyRTF : RichTextBox 
    {
        [DllImport("USER32.dll")]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        private const int WM_USER = 0x400;
        private const int EM_FORMATRANGE = WM_USER + 57;
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct CHARRANGE
        {
            public int cpMin;
            public int cpMax;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE
        {
            public IntPtr hdc;
            public IntPtr hdcTarget;
            public RECT rc;
            public RECT rcPage;
            public CHARRANGE chrg;
        }
        public static Bitmap RtbToBitmap(Control rtb)
        {
            return RtbToBitmap(rtb,
                               rtb.ClientRectangle.Width,
                               rtb.ClientRectangle.Height);
        }
        private const double inch = 14.4;
        // Capture
        public static void RtbDrawToGraphics(
            Control rtb,
            Graphics gr,Rectangle rc)
        {
            IntPtr hDC = gr.GetHdc();
            FORMATRANGE fmtRange;
            RECT rect;
            int fromAPI;
            int h = rc.Height;
            int w = rc.Width;
            rect.Top = rc.X ; 
            rect.Left = rc.Y ;
            rect.Bottom = (int)(h
                               + (h * (96.0 / 100))
                               * inch);
            rect.Right = (int)(w
                              + (w * (96.0 / 100))
                              * inch);
            fmtRange.chrg.cpMin = 0;
            fmtRange.chrg.cpMax = -1;
            fmtRange.hdc = hDC;
            fmtRange.hdcTarget = hDC;
            fmtRange.rc = rect;
            fmtRange.rcPage = rect;
            int wParam = 1;
            IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, lParam, false);
            fromAPI = SendMessage(rtb.Handle, EM_FORMATRANGE, wParam, lParam);
            Marshal.FreeCoTaskMem(lParam);
            fromAPI = SendMessage(rtb.Handle, EM_FORMATRANGE,
                                  wParam, new IntPtr(0));
            gr.ReleaseHdc(hDC);
        }
        public static Bitmap RtbToBitmap(Control rtb, int width, int height)
        {
            if ((width <= 0) || (height <= 0) || rtb.IsDisposed )
                return null;
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor ;
                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None ;
                gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit ;
                IntPtr hDC = gr.GetHdc();
                FORMATRANGE fmtRange;
                RECT rect;
                int fromAPI;
                rect.Top = 0; rect.Left = 0;
                rect.Bottom = (int)(bmp.Height
                                   + (bmp.Height * (bmp.HorizontalResolution / 100))
                                   * inch);
                rect.Right = (int)(bmp.Width
                                  + (bmp.Width * (bmp.VerticalResolution / 100))
                                  * inch);
                fmtRange.chrg.cpMin = 0;
                fmtRange.chrg.cpMax = -1;
                fmtRange.hdc = hDC;
                fmtRange.hdcTarget = hDC;
                fmtRange.rc = rect;
                fmtRange.rcPage = rect;
                int wParam = 1;
                IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
                Marshal.StructureToPtr(fmtRange, lParam, false);
                fromAPI = SendMessage(rtb.Handle, EM_FORMATRANGE, wParam, lParam);
                Marshal.FreeCoTaskMem(lParam);
                fromAPI = SendMessage(rtb.Handle, EM_FORMATRANGE,
                                      wParam, new IntPtr(0));
                gr.ReleaseHdc(hDC);
                if (rtb.BackColor == Color.Transparent)
                {
                    ImageAttributes attr = new ImageAttributes();
                    ColorMap[] tab = new ColorMap[]{
                        new ColorMap(){
                             OldColor =   Color.White  ,
                             NewColor =   Color.Transparent 
                    }};
                    attr.SetRemapTable(tab);
                    gr.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    gr.DrawImage(bmp, new Rectangle(Point.Empty, bmp.Size), 0, 0, bmp.Width, bmp.Height,
                        GraphicsUnit.Pixel,
                        attr);
                }
            }
            return bmp;
        }
        public XDummyRTF()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.HideSelection = false;
        }
    }
}

