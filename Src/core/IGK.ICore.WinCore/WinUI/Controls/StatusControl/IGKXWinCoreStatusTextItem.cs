

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXWinCoreStatusText.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;
using IGK.ICore.WinCore.WinUI;


namespace IGK.ICore.WinUI
{
    
    public class IGKWinCoreStatusTextItem : IGKWinCoreStatusItemBase , IXCoreStatusText
    {
        private string m_Text;
        private bool m_Autosize;

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            this.updateSize();
        }
        public IGKWinCoreStatusTextItem()
        {
            this.updateSize();
        }
        public bool Autosize
        {
            get { return m_Autosize; }
            set
            {
                if (m_Autosize != value)
                {
                    m_Autosize = value;
                    this.updateSize();
                }
            }
        }
        public string Text
        {
            get { return m_Text; }
            set
            {
                if (m_Text != value)
                {
                    m_Text = value;
                    updateSize();

                    if (this.Parent != null)
                    {
                      
                        this.Parent.Invalidate();
                        if (this.Parent.InvokeRequired)
                        {
                            try
                            {
                                this.Parent.Invoke(new CoreMethodInvoker(() =>
                                {
                                    this.Parent.Update();
                                }));
                            }
                            catch
                            {
                                //worker thread not exist: error
                            }

                        }else
                        this.Parent.Update();
                    }
                    OnTextChanged(EventArgs.Empty);
                }
            }
        }

        private void updateSize()
        {
            if (this.Autosize && (this.Parent != null))
            {

                Size2f s =
                    WinCoreUtility.MeasureString(this.Text, this.Parent.Font);
                this.Bounds = new Rectanglef(0, 0, 
                    (float)Math.Ceiling (s.Width), 
                    (float)Math.Ceiling(s.Height));
                this.Parent.SetupItemAndInvalidate();
            }
        }
        public event EventHandler TextChanged;
        ///<summary>
        ///raise the TextChanged 
        ///</summary>
        protected virtual void OnTextChanged(EventArgs e)
        {
        
            if (TextChanged != null)
                TextChanged(this, e);
        }

        public override void Render(ICoreGraphics graphics, bool active)
        {
            
            if (
                string.IsNullOrEmpty(this.Text) || this.Bounds.IsEmpty || !this.Visible)
                return;

            Colorf cl =
                active ? WinCoreControlRenderer.WinCoreStatusActiveForeColor :
                WinCoreControlRenderer.WinCoreStatusInactiveForeColor;
                
            graphics.DrawString(this.Text, 
                this.Parent.Font,
                WinCoreBrushRegister.GetBrush(cl),
                this.Bounds,
                WinCoreStringFormats.TrimmingHorizontalAlignment);
            
        }

    }
}
