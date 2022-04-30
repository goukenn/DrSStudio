

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TimeLineCursor.cs
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
file:TimeLineCursor.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.AudioBuilder.WinUI
{
    public class TimeLineCursor : System.Windows.Forms .Form 
    {
        XAudioBuilderSurface m_surface;
        public override Size MinimumSize
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                base.MinimumSize = value;
            }
        }
        protected override Size DefaultSize
        {
            get
            {
                return new Size(AudioConstant.TIMELINE_CUR_WIDTH, 100);
            }
        }
        public TimeLineCursor(XAudioBuilderSurface surface)
        {
            this.SetStyle(ControlStyles.FixedWidth, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false );
            this.Paint += new PaintEventHandler(_Paint);
            this.FormBorderStyle = FormBorderStyle.None;
            this.m_surface = surface;
            this.TopLevel = false;
            this.Parent  = surface;
            this.Show();
            this.m_surface.SizeChanged += new EventHandler(m_surface_SizeChanged);
            this.m_surface.PositionChanged += new EventHandler(m_surface_PositionChanged);
            this.UpdateLocation();
            this.Opacity = 0.4f;
        }
        void m_surface_PositionChanged(object sender, EventArgs e)
        {
            UpdateLocation();
        }
        void m_surface_SizeChanged(object sender, EventArgs e)
        {
            UpdateLocation();    
        }
        private void UpdateLocation()
        {
            //upade the locatoin
            //by default set the location to center
            int x = 0;
            if (this.m_surface.Duration > 0)
                x = (this.m_surface.Position / this.m_surface.Duration) * this.m_surface.Width;
            Point cp = /* this.m_surface .PointToScreen (*/
                new Point (x, 0);//);
            this.Bounds = new Rectangle(cp.X, cp.Y, this.Width, this.m_surface.Height);
        }
        void _Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawRectangle(Pens.Black, new Rectangle(2, 0, 1, this.Height));
            e.Graphics.FillRectangle(Brushes.Black, this.ClientRectangle);
        }
    }
}

