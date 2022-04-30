

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MainForm_DockToRight.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:MainForm_DockToRight.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI.MainFormDisposition
{
    class MainForm_DockToRight : MainForm_Docking
    {
        public override string Id
        {
            get { throw new NotImplementedException(); }
        }
        protected override Rectanglei GetRectangle(Screen c)
        {
            int w = c.WorkingArea.Width / 2;
            int h = c.WorkingArea.Height;
            return new Rectanglei(c.Bounds.X + w, c.Bounds.Y, w, h);
        }
    }
}

