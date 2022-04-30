

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
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
namespace IGK.GetPictureSampleWithInitReg
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Drawing2D ;
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread.CurrentCulture);
            Console.WriteLine(Thread.CurrentThread.CurrentUICulture );
            CoreSystem.RegisterResources = false;
            CoreSystem.InitReg(null);
            Console.WriteLine(Thread.CurrentThread.CurrentCulture);
            Console.WriteLine(Thread.CurrentThread.CurrentUICulture);
            ICoreWorkingDocument[] d = CoreSystem.OpenDocumentFile (@"D:\MyPictureWorks\redcircle.gkds");
            if ((d != null) && (d.Length > 0))
            {
                ICore2DDrawingDocument doc = d[0] as ICore2DDrawingDocument ;
                Bitmap bmp = new Bitmap(doc.Width, doc.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    doc.Draw(g);
                    g.Flush();
                }
                using (Form frm = new Form())
                {
                    frm.BackgroundImage = bmp;
                    frm.ShowDialog();
                }
            }
            return;
        }
    }
}

