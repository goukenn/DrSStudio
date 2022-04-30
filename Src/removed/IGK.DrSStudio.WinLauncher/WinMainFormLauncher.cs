

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinMainFormLauncher.cs
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
file:WinMainFormLauncher.cs
*/
using IGK.ICore;using IGK.DrSStudio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinLauncher
{
    class WinMainFormLauncher
    {
        private WinLaucherStartForm winLaucherStartForm;
        public WinMainFormLauncher(WinLaucherStartForm winLaucherStartForm)
        {            
            this.winLaucherStartForm = winLaucherStartForm;
        }
        public void Run()
        {
            try
            {
                CoreSystem.Init();
                WinLaucherMainForm c = new WinLaucherMainForm();
                c.Load += c_Load;
                CoreMainFormRunner.Run(CoreSystem.Instance, c);
            }
            catch (Exception ex){
                CoreLog.WriteLine("exception on running "+ex.Message );
                //close main form
                if (winLaucherStartForm != null)
                    winLaucherStartForm.Invoke((MethodInvoker)winLaucherStartForm.Close);
            }
        }
        void c_Load(object sender, EventArgs e)
        {
            if (winLaucherStartForm !=null)
            winLaucherStartForm.Invoke((MethodInvoker)winLaucherStartForm.Close);
        }
    }
}

