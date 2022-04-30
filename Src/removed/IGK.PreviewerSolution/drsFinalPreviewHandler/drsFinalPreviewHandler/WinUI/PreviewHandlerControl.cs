

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PreviewHandlerControl.cs
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
file:PreviewHandlerControl.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.PreviewHandler.WinUI
{
    /// <summary>
    /// represent the base Preview Handler Control
    /// </summary>
    class PreviewHandlerControl : UserControl, IPreviewHandlerControl
    {
        public PreviewHandlerControl()
        {
            this.BackColor = Color.Black;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        public void Unload()
        {
        }
    }
}

