

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XWebNavigationBar.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XWebNavigationBar.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WebBrowserAddIn.WinUI
{
    /// <summary>
    /// represent the navigation bar control
    /// </summary>
    class XWebNavigationBar : IGKXPanel
    {
        private IWebBrowserSurface m_WebBrowser;
        private XWebBrowserNavigationTextbox c_txb;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        [Browsable (false )]
        /// <summary>
        /// get or set the web browser
        /// </summary>
        public IWebBrowserSurface WebBrowser
        {
            get { return m_WebBrowser; }
            set
            {
                if (m_WebBrowser != value)
                {
                    m_WebBrowser = value;
                }
            }
        }
        public XWebNavigationBar()
        {
            this.InitializeComponent();
            this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.Paint += _Paint;
        }

      
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
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
        private void _Paint(object sender, CorePaintEventArgs e)
        {
        }
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(100, 36);
            }
        }
        private void InitializeComponent()
        {
            this.c_txb = new XWebBrowserNavigationTextbox();
            this.Controls.Add(this.c_txb);
        }
    }
}

