

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XTimeLineEditor.cs
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
file:XTimeLineEditor.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing ;
namespace IGK.DrSStudio.AudioBuilder.WinUI
{
    internal class XTimeLineEditor : Control 
    {
        XAudioBuilderSurface m_surface;
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(100, AudioConstant.TIMELINE_HEIGHT);
            }
        }
        public XTimeLineEditor(XAudioBuilderSurface surface)
        {
            this.m_surface = surface;
            this.m_surface.ScaleChanged += new EventHandler(m_surface_ScaleChanged);
            this.Paint += new PaintEventHandler(_Paint);
            this.SetStyle(ControlStyles.FixedHeight, true);
        }
        void m_surface_ScaleChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        void _Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(XAudioRenderer .AudioTimeLineBackgroundColor);
            int i = 0;
            while (i < this.Width)
            {
                i += 5;
                e.Graphics.DrawLine(Pens.Black, i, 0, i,
                    ((i%50)== 0)?10:5
                    );
            }
            int x = 0;
            int s = this.m_surface.TimeScale;
            TimeSpan v_span = new TimeSpan ();
            string v_txt = string.Empty;
            Font v_ft = null;
            v_ft = new Font("Courrier New", 8.0f, FontStyle.Regular, GraphicsUnit.Pixel);
            while (x < this.Width)
            {
                v_txt = string.Format("{0:00}:{1:00}:{2:00}", v_span.Minutes, v_span.Seconds, v_span.Milliseconds);
                e.Graphics.DrawString(v_txt, v_ft, Brushes.Black, x, 10);
                x += 150;
                v_span =  v_span .Add (new TimeSpan (0,0,0,0, s * 1));
            }
            v_ft.Dispose();
        }
    }
}

