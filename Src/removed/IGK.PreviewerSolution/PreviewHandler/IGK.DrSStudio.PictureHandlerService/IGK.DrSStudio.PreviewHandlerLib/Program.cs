

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
using Microsoft.Win32;
using System.Windows.Forms;
namespace IGK.PrevHandlerLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Form frm = new Form();
            frm.Text = "Args : " + Environment.CommandLine ;
            frm.ShowDialog();
            //Register();
        }
        private static void Register()
        {
            //string guid = "EC79C6CD-0034-42A6-B68E-A3FDB3F86502";
            //Guid gid = new Guid(guid);
            //Console.WriteLine("Previewer : " + PreviewHandler.HKEYLM_RegisterPreviewer);
            //PreviewHandler hprev = new PreviewHandler(".gkds", gid, "gkdsfile");
            //hprev.DisplayName = "IGKDEV DrSStudio Preview Handler";
            //Console.WriteLine("Previewer: Register");
            //hprev.Register();
            //Console.ReadLine();
            //Console.WriteLine("Previewer: Unregister");
            //hprev.UnRegister();
            //Console.ReadLine();
        }
    }
}

