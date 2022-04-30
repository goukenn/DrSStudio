

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MainForm_Maximize.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:MainForm_Maximize.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI.MainFormDisposition
{
    class MainForm_Maximize : MainForm_Docking
    {
        public override string Id
        {
            get { throw new NotImplementedException(); }
        }
        protected override Rectanglei GetRectangle(System.Windows.Forms.Screen e)
        {
            return base.GetRectangle(e);
        }
        public override bool DoAction(enuKeyState keystate)
        {
            if (this.IsWindowKeyPressed && (keystate == enuKeyState.KeyUp))
            {
                (this.MainForm as Form).WindowState = FormWindowState.Maximized;
                return true;
            }
            return false;
        }
        protected override bool PerformAction()
        {
            return base.PerformAction();
        }
    }
}

