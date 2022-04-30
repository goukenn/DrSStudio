

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXGroupBox.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Resources;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKXGroupBox.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinUI;
namespace IGK.DrSStudio.WinUI
{
    [DefaultProperty("CaptionKey")]
    public class IGKXGroupBox : GroupBox  
    {
        private string m_CaptionKey;
        /// <summary>
        /// get or set the caption key
        /// </summary>
        public string CaptionKey
        {
            get { return m_CaptionKey; }
            set
            {
                if (m_CaptionKey != value)
                {
                    m_CaptionKey = value;
                    this.LoadDisplayText();
                    OnCaptionKeyChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        protected virtual void LoadDisplayText()
        {
            this.Text = CoreResources.GetString(this.CaptionKey);            
        }

        public event EventHandler CaptionKeyChanged;
        ///<summary>
        ///raise the CaptionKeyChanged 
        ///</summary>
        protected virtual void OnCaptionKeyChanged(EventArgs e)
        {
            if (CaptionKeyChanged != null)
                CaptionKeyChanged(this, e);
        }


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
       
        public override System.Drawing.Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
       
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }
        public IGKXGroupBox()
        {
            this.SetStyle(ControlStyles.ResizeRedraw |  ControlStyles.OptimizedDoubleBuffer , true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Paint += _Paint;
        }

        private void _Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(CoreRenderer.BackgroundColor);

            e.Graphics.DrawRectangle(
                WinCoreBrushRegister.GetPen(Colorf.FromFloat(0.6f)),
                0, 0, this.Width-1, this.Height - 1);


        }
    }
}

