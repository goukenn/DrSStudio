

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IPreviewHandlerControl.cs
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
file:IPreviewHandlerControl.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.PreviewHandler
{
    public interface  IPreviewHandlerControl
    {
        IntPtr Handle { get; }
        Control.ControlCollection Controls { get; }
        Color BackColor { get; set; }
        Color ForeColor { get; set; }
        Font Font { get; set; }
        Rectangle Bounds { get; set; }
        bool Visible { get; set; }
        void CreateControl();
        object  Invoke(Delegate del);
        bool Focus();
        void Unload();
    }
}

