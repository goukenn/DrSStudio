

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: xDummyRTFEditForm.cs
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
file:xDummyRTFEditForm.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    internal sealed class xDummyRTFEditForm : Form 
    {
        XRtfToolStrip c_fontcontrol;
        XDummyRTF c_rtf;
        public xDummyRTFEditForm(XDummyRTF rtf)
        {
            if ((rtf == null) || rtf.IsDisposed)
                throw new ArgumentException("rtf");
            this.c_rtf = rtf;
            this.Controls.Add(c_rtf);
            c_rtf.Dock = DockStyle.Fill;
            c_fontcontrol = new XRtfToolStrip();
            c_fontcontrol.Dock = DockStyle.Top;
            c_fontcontrol.FontDefinitionChanged += new EventHandler(c_fontcontrol_FontDefinitionChanged);
            this.Controls.Add(c_fontcontrol);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
        }
        void c_fontcontrol_FontDefinitionChanged(object sender, EventArgs e)
        {
            if (c_rtf.SelectionLength > 0)
            {
                Font ft = new Font (c_fontcontrol.FontName ,
                    ((ICoreUnitPixel )c_fontcontrol.FontSize ).Value ,
                    c_fontcontrol.FontStyle ,
                     GraphicsUnit.Pixel );
                c_rtf.SelectionFont = ft;
            }
            else {
                Font ft = new Font(c_fontcontrol.FontName,
                    ((ICoreUnitPixel)c_fontcontrol.FontSize).Value,
                    c_fontcontrol.FontStyle,
                     GraphicsUnit.Pixel);
                c_rtf.SelectionFont = ft;
            }
        }
    }
}

