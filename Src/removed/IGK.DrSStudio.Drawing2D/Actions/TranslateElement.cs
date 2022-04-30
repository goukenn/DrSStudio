

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TranslateElement.cs
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
file:TranslateElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Actions
{
    public sealed class TranslateElement : Core2DDrawingMecanismAction
    {
        bool m_accelerate = false;
        bool m_accelerating = false;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer wtimer;
        private int m_MinStep;
        private int m_MaxStep;
        public int MaxStep
        {
            get { return m_MaxStep; }
            set
            {
                if (m_MaxStep != value)
                {
                    m_MaxStep = value;
                }
            }
        }
        public int MinStep
        {
            get { return m_MinStep; }
            set
            {
                if (m_MinStep != value)
                {
                    m_MinStep = value;
                }
            }
        }
        public TranslateElement()
        {
            this.m_MinStep = 1;
            this.m_MaxStep = 10;
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Interval = 2000;
            this.timer.Enabled = false;
            this.timer.Tick += new EventHandler(timer_Tick);
            this.wtimer = new System.Windows.Forms.Timer();
            this.wtimer.Interval = 500;
            this.wtimer.Enabled = false;
            this.wtimer.Tick += new EventHandler(endwtimer);
        }
        void endwtimer(object sender, EventArgs e)
        {
            if (this.m_accelerate)
            {
                this.wtimer.Enabled = false;
                this.m_accelerate = false;
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
                this.m_accelerate = true;
                this.m_accelerating = true;
                this.timer.Enabled = false;
                this.wtimer.Enabled = true;
        }
        protected override bool PerformAction()
        {
            float x = 0.0f;
            float y = 0.0f;
            switch (this.ShortCut)
            {
                case System.Windows.Forms.Keys.Left:
                    x -= (m_accelerate) ? m_MaxStep : m_MinStep;
                    break;
                case System.Windows.Forms.Keys .Right:
                    x += (m_accelerate) ? m_MaxStep : m_MinStep;
                    break;
                case System.Windows.Forms.Keys.Up :
                    y -=  (m_accelerate) ? m_MaxStep : m_MinStep;
                    break;
                case System.Windows.Forms.Keys.Down :
                    y += (m_accelerate) ? m_MaxStep : m_MinStep;
                    break;
            }
            foreach (ICore2DDrawingLayeredElement  l in this.Mecanism .CurrentSurface.CurrentLayer.SelectedElements.ToArray())
            {
                l.Translate(x, y, enuMatrixOrder.Append);
            }
            this.Mecanism.CurrentSurface.Invalidate();
            if (!this.m_accelerate && !this.timer.Enabled)
            {
                this.m_accelerating = false;
                this.timer.Enabled = true;
                this.wtimer.Enabled = true;
            }
            else if (this.wtimer.Enabled)
            {
                this.wtimer.Stop();
                if (this.m_accelerating)
                    this.m_accelerate = true;
                this.wtimer.Enabled = true;//reset the waiting timer
            }
            return false;
        }
    }
}

