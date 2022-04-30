

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSXBrushSelector.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.WinUI;
using IGK.ICore.WinCore.WinUI.Controls;
using System.Drawing;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent a brushes selector
    /// </summary>
    public class GCSXBrushSelector : IGKXControl
    {
        private GCSXBrushSelectorBrush m_brush; //brush selector
        private bool m_Selected;

        public bool Selected
        {
            get { return m_Selected; }
            set
            {
                if (m_Selected != value)
                {
                    m_Selected = value;
                    this.Invalidate();
                }
            }
        }
        
        /// <summary>
        /// represent the brush selector brush
        /// </summary>
        public sealed class GCSXBrushSelectorBrush : CoreBrush 
        {
            private GCSXBrushSelector m_selector;            
            public GCSXBrushSelectorBrush(GCSXBrushSelector c):base(null)
            {
                this.m_selector = c;
                this.m_selector.SizeChanged += m_selector_SizeChanged;
                this.SetupBound();
            }

            private void SetupBound()
            {
                lock (this)
                {
                    this.Bounds = this.m_selector.ClientRectangle.CoreConvertFrom<Rectanglef>();
                    this.OnBrushDefinitionChanged(EventArgs.Empty);
                }   
            }

            void m_selector_SizeChanged(object sender, EventArgs e)
            {
                this.SetupBound();
            }
            protected override void OnBrushDefinitionChanged(EventArgs e)
            {
                base.OnBrushDefinitionChanged(e);
            }
            public override void CopyDefinition(string value)
            {
                this.SuspendBrush();
                base.CopyDefinition(value);
                this.AutoSize = false;                
                this.ResumeBrush();
            }
            public override void Copy(ICoreBrush iCoreBrush)
            {
                this.SuspendBrush();
                base.Copy(iCoreBrush);
                this.AutoSize = false;
                this.SetupBound();
                this.ResumeBrush();
             //   string def = this.GetDefinition();
            }

            
        }
        public GCSXBrushSelectorBrush Brush
        {
            get {
                return this.m_brush;
            }
          
        }

        protected override void Dispose(bool disposing)
        {
            if (this.m_brush != null)
            {
                this.m_brush.Dispose();
                this.m_brush = null;
            }
            base.Dispose(disposing);
        }
      
     
        void m_brush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        public GCSXBrushSelector()
        {
            this.Paint += _Paint;
            this.m_brush= new GCSXBrushSelectorBrush(this);
            this.m_brush.BrushDefinitionChanged +=m_brush_BrushDefinitionChanged;
        }
        static Brush sm_doc;
        void _Paint(object sender, CorePaintEventArgs e)
        {
            if (sm_doc == null)
                sm_doc = WinCoreBrushes.GetBrushes(WinCoreBrushesNames.DASH);
            if (sm_doc != null) {
                e.Graphics.FillRectangle (sm_doc,
                    0, 0, this.Width - 1, this.Height - 1);
            }

            if (this.Enabled)
                e.Graphics.FillRectangle(this.Brush, 0, 0, this.Width - 1, this.Height - 1);
            else
            {
                e.Graphics.FillRectangle(Colorf.DarkGrey, 0, 0, this.Width - 1, this.Height - 1);
            }
            e.Graphics.DrawRectangle(this.Selected ? Colorf.White : Colorf.DarkGray, 1, 1, this.Width - 3, this.Height - 3);
            e.Graphics.DrawRectangle(Colorf.Black, 0, 0, this.Width - 1, this.Height - 1);
        }

    }
}
