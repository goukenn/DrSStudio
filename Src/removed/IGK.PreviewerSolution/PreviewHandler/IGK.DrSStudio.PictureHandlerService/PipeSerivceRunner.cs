

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PipeSerivceRunner.cs
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
file:PipeSerivceRunner.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Pipes;
using System.Threading ;
using System.ServiceProcess;
using System.Diagnostics;
namespace IGK.DrSStudio.PictureHandlerService
{
    class PipeServerRuner
    {
        NamedPipeServerStream serverPipe;
        Thread opThread;
        PictureHandlerSrv service;
        public PipeServerRuner(PictureHandlerSrv  srv)
        {
            serverPipe = new NamedPipeServerStream(PictureHandlerConstant .PIPE_NAME , PipeDirection.InOut);
            opThread = new Thread(DoJob);
            this.service  = srv;
        }
        void DoJob()
        {            
            serverPipe.WaitForConnection();
            StreamWriter sw = new StreamWriter(serverPipe);
            StreamReader sr = new StreamReader(serverPipe);
            sw.WriteLine(PictureHandlerConstant.PIPE_NAMESERVER);
            sw.Flush();
            //get the file name
            string file = sr.ReadLine();
            MemoryStream mem = new MemoryStream();
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(256, 256);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
            g.Clear(System.Drawing.Color.Yellow);
            g.Flush();
            g.Dispose();
            bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
            mem.Seek(0, SeekOrigin.Begin);
            mem.WriteTo(sw.BaseStream);
            mem.Dispose();
            serverPipe.Disconnect();
            serverPipe.Close();
        }
        internal void Run()
        {
            opThread.Start();
        }
        internal void Stop()
        {
            if (opThread != null)
            {
                opThread.Abort();
                opThread.Join();
            }
        }
    }
}

