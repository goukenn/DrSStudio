

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PresentationSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Presentation.WinUI
{
    /// <summary>
    /// represent the presentation working surface
    /// </summary>
    public class PresentationSurface : IGKXWinCoreWorkingSurface
    {
        private PresentationMecanism m_mecanism;
        private PresentationDocument m_PresentationDocument;
        public event EventHandler PresentationDocumentChanged;
        ///<summary>
        ///raise the PresentationDocumentChanged 
        ///</summary>
        protected virtual void OnPresentationDocumentChanged(EventArgs e)
        {
            PresentationDocumentChanged?.Invoke(this, e);
        }

        public PresentationDocument PresentationDocument
        {
            get { return m_PresentationDocument; }
            set
            {
                if (m_PresentationDocument != value)
                {
                    m_PresentationDocument = value;
                    OnPresentationDocumentChanged(EventArgs.Empty);
                }
            }
        }
        public PresentationSurface()
        {
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackColor = Colorf.Black.ToGdiColor();
            this.Paint += PresentationSurface_Paint;
            this.PresentationDocumentChanged += PresentationSurface_PresentationDocumentChanged;
            this.Focus();
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!this.Focused)
                Focus();
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
        }

        internal void EnableMecanism()
        {
            this.m_mecanism = new PresentationMecanism(this);            
        }

        void PresentationSurface_Paint(object sender, CorePaintEventArgs e)
        {
            if (this.PresentationDocument != null)
            {
                var r = this.ClientRectangle;
                this.PresentationDocument.Render(e.Graphics, new Rectanglei (r.X,r.Y,r.Width,r.Height ));
            }
        }

        void PresentationSurface_PresentationDocumentChanged(object sender, EventArgs e)
        {
            this.Update();
            this.Invalidate();
        }

      

 
        

    }
}
