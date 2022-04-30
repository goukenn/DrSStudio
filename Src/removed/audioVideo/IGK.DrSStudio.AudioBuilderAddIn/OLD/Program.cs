

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Program.cs
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
file:Program.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.AudioBuilder
{
    using IGK.ICore;using IGK.DrSStudio.AudioBuilder.WinUI;
    sealed class Program
    {
        [STAThread ()]
        public static void Main()
        {
            Application.EnableVisualStyles();
            CoreSystem.Init();
            try
            {
                CoreSystem.Instance.Run(
                    null, typeof(AudioForm));
                //Application.Run(new AudioForm());
            }
            catch (Exception Exception){
                System.Windows.Forms.MessageBox.Show(Exception.Message );
            }
        }
    }
}

