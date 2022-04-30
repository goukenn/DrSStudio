

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XWebBrowserNavigationTextbox.cs
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
file:XWebBrowserNavigationTextbox.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WebBrowserAddIn.WinUI
{
    using IGK.ICore.WinCore;
    /// <summary>
    /// get or set the navigation bar textbox
    /// </summary>
    class XWebBrowserNavigationTextbox : TextBox
    {
        public override System.Drawing.Size MinimumSize
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        [Browsable(false )]
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
        public string Uri {
            get {
                return this.Text;
            }
            set {
                this.Text = value;
            }
        }
        public XWebBrowserNavigationTextbox()
        {
            this.MinimumSize = new System.Drawing.Size(100, 18);
            this.Paint += _Paint;
            this.TextChanged += _TextChanged;
        }
        void _TextChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
        private void _Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Colorf.Red);
        }
    }
}

