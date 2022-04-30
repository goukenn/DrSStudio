

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreRftRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio
{
    class WinCoreRftRenderer : IDisposable 
    {
        WinCoreRichTextBox c_rtf;

        class WinCoreRichTextBox : RichTextBox
        {
           
            public WinCoreRichTextBox()
            {
                //this.SetStyle(ControlStyles.AllPaintingInWmPaint|
                //     ControlStyles.OptimizedDoubleBuffer |
                //      ControlStyles.ResizeRedraw  |
                //       ControlStyles.SupportsTransparentBackColor |
                //       ControlStyles.UserPaint 
                //    , true);
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                this.BackColor = Color.Transparent;
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                //base.OnPaint(e);
            }
            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                //base.OnPaintBackground(pevent);
            }
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                { 
                    case 0x14:
                        break;
                    case 0xf:
                        break;
                    case EM_FORMATRANGE:
                        break;
                }
                base.WndProc(ref m);

            }
        }

        public WinCoreRftRenderer()
        {
            this.c_rtf = new WinCoreRichTextBox();
            this.c_rtf.CreateControl();
        }


        internal void DrawString(string text, Rectanglef bounds, System.Drawing.Graphics graphics)
        {
            if (string.IsNullOrEmpty(text) || (graphics ==null))
            {
                return;
            }
            Rectanglei v_r = Rectanglei.Round(bounds);
            if ((v_r.IsEmpty ) || (v_r.Width == 0) || (v_r.Height == 0))
                return ;
            this.c_rtf.Rtf = text;

            using (Bitmap bmp = new Bitmap(v_r.Width, v_r.Height))
            {
                Graphics g = Graphics.FromImage(bmp);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                IntPtr r = g.GetHdc();
                //render content
                FORMATRANGE fmt = GetFormatRange(new Rectangle(Point.Empty , bmp.Size), r, 0);
                int nextChar = FormatRange(this.c_rtf, true, ref fmt);
                g.ReleaseHdc(r);
                bmp.MakeTransparent(Color.Transparent);
                graphics.DrawImage(bmp, new Rectangle(v_r.X , v_r.Y , bmp.Width , bmp.Height));
            }

        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
            public void SetRect(Rectangle rc)
            {
                Left = rc.Left;
                Top = rc.Top;
                Right = rc.Right;
                Bottom = rc.Bottom;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CHARRANGE
        {
            public int cpMin;         //First character of range (0 for start of doc)
            public int cpMax;           //Last character of range (-1 for end of doc)
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE
        {
            public IntPtr hdc;             //Actual DC to draw on
            public IntPtr hdcTarget;       //Target DC for determining text formatting
            public RECT rc;                //Region of the DC to draw to (in twips)
            public RECT rcPage;            //Region of the whole DC (page size) (in twips)
            public CHARRANGE chrg;         //Range of text to draw (see earlier declaration)
        }

        private const int WM_USER = 0x0400;
        private const int EM_FORMATRANGE = WM_USER + 57;

        [DllImport("USER32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        [DllImport("USER32.DLL", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        static extern int SendMessageFormatRange(IntPtr hWnd, uint wMsg, int wParam, ref FORMATRANGE lParam);


        // build a FORMATRANGE structure with the proper page size and hdc
        // (the hdc must be released after the FORMATRANGE is used)
        FORMATRANGE GetFormatRange(Rectangle bound, IntPtr hdc, int firstChar)
        {
            // get page rectangle in twips (RichEd20.dll uses twips)
            var rc = bound;
            rc.X = (int)(rc.X * 14.4 + .5);
            rc.Y = (int)(rc.Y * 14.4 + .5);
            rc.Width = (int)(rc.Width * 14.4 + .5);
            rc.Height = (int)(rc.Height * 14.40 + .5);

            // set up FORMATRANGE structure with the target device/rect
            var fmt = new FORMATRANGE();
            fmt.hdc = fmt.hdcTarget = hdc;
            fmt.rc.SetRect(rc);
            fmt.rcPage = fmt.rc;

            // specify the document range to render
            CHARRANGE rg = new CHARRANGE();
            rg.cpMin = firstChar;
            rg.cpMax = -1;
            fmt.chrg = rg;

            // done
            return fmt;
        }
        // send the EM_FORMATRANGE message to the RichTextBox to render or measure
        // a range of the document into a target specified by a FORMATRANGE structure.
        int FormatRange(RichTextBox rtb, bool render, ref FORMATRANGE fmt)
        {
            // render or measure part of the document
            int nextChar = SendMessageFormatRange(
                rtb.Handle,
                EM_FORMATRANGE,
               (render ? 1 : 0),
                ref fmt);

            // reset after rendering/measuring
            SendMessage(rtb.Handle, EM_FORMATRANGE, IntPtr.Zero, IntPtr.Zero);

            // return next character to print
            return nextChar;
        }


        public void Dispose()
        {
            if (this.c_rtf != null)
            {
                this.c_rtf.Dispose();
            }
        }
    }
}
