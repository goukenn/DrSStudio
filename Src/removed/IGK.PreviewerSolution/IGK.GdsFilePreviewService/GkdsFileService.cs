

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GkdsFileService.cs
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
file:GkdsFileService.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Threading;
using System.IO;
using System.IO.Pipes;
using System.Drawing;
namespace IGK.GdsFilePreviewService
{
    using IGK.ICore;using IGK.GkdsFilePreviewHandler;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Drawing2D;
    /// <summary>
    /// represent a gkds file service
    /// </summary>
    public class GkdsFileService : ServiceBase
    {
        List<ServerPipeService> m_runninPipe;
        NamedPipeServerStream m_serverPipe;
        public GkdsFileService()
        {
            this.ServiceName = GkdsFileConstant.SERVICE_NAME;
            m_runninPipe = new List<ServerPipeService>();
            m_serverPipe = new NamedPipeServerStream(GkdsFileConstant.PIPE_NAME, PipeDirection.InOut);
        }
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
        }
        protected override void OnStop()
        {
            base.OnStop();
        }
        protected override void OnCustomCommand(int command)
        {
            switch ((enuServiceCommand)command)
            {
                case enuServiceCommand.GetBitmap:
                    {
                        ServerPipeService srv = new ServerPipeService(this);
                        m_runninPipe.Add(srv);
                        srv.Run();
                    }
                    break;
                case enuServiceCommand.GetSystemInfo:
                    break;
            }
        }
        class ServerPipeService
        {
            Thread m_thread;
            GkdsFileService m_service;
            public NamedPipeServerStream ServerPipe {
                get { return m_service.m_serverPipe; }
            }
            public ServerPipeService(GkdsFileService service)
            {
                this.m_service = service;
            }
            public void Abort()
            {
                if (m_thread != null)
                {
                    m_thread.Abort();
                    m_thread.Join();
                }
            }
            public void Run()
            {
                if (m_thread == null)
                {
                    m_thread = new Thread(RunServer);
                    // m_thread.SetApartmentState (ApartmentState.STA );
                    // m_thread.IsBackground = false;
                    m_thread.CurrentCulture = IGK.DrSStudio.Threading.ThreadManager.CultureInfo;
                    m_thread.CurrentUICulture = IGK.DrSStudio.Threading.ThreadManager.CultureInfo;
                    m_thread.Start();
                }
            }
            void RunServer()
            {
                if (ServerPipe == null)
                    return;
                this.ServerPipe.WaitForConnection();
                StreamWriter sw = new StreamWriter(ServerPipe);
                StreamReader sr = new StreamReader(ServerPipe);
                sw.WriteLine(GkdsFileConstant.GOOD_PIPE_NAME);
                sw.Flush();
                string[] v_fileInfo = sr.ReadLine().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                string v_file = string.Empty;
                int v_w = 0;
                int v_h = 0;
                bool v_good = false;
                try
                {
                    ServerPipe .RunAsClient ( new PipeStreamImpersonationWorker (
                        delegate(){
                    if (v_fileInfo.Length == 3)
                    {
                        v_file = v_fileInfo[0];
                        v_w = Convert.ToInt32(v_fileInfo[1]);
                        v_h = Convert.ToInt32(v_fileInfo[2]);
                        v_good = true;
                    }
                    if (v_good && File.Exists(v_file) && (v_w > 0) && (v_h > 0))
                    {
                        Bitmap bmp = new Bitmap(v_w, v_h, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                        ICoreWorkingDocument[] docs = null;
                        try
                        {
                            docs = CoreSystem.OpenDocumentFile(v_file);
                            //sw.WriteLine("File : " + v_file );
                            //sw.WriteLine("Document : " + docs );
                            //sw.WriteLine("Document.Length : " + docs.Length );
                            //sw.WriteLine("WorkingObjects : " + CoreSystem.Instance.WorkingObjects.Count);
                            //sw.WriteLine(Thread.CurrentThread.CurrentCulture);
                            //sw.WriteLine(Thread.CurrentThread.CurrentUICulture);
                            //sw.WriteLine(System.Windows.Forms.Application.StartupPath);
                            //sw.WriteLine(GetType().Assembly.Location);
                            //sw.Flush();
                            //return;
                            ICore2DDrawingDocument doc = docs[0] as ICore2DDrawingDocument;
                            using (Graphics g = Graphics.FromImage(bmp))
                            {
                                g.Clear(Color.White);
                                doc.Draw(g,
                                     true, new Rectanglei(0, 0, v_w, v_h), IGK.DrSStudio.Drawing2D.enuFlipMode.None);
                                g.Flush();
                            }
                            bmp.Save(sw.BaseStream, System.Drawing.Imaging.ImageFormat.Png);
                            sw.Flush();
                        }
                        catch (Exception ex)
                        {
                            sw.WriteLine(ex.Message);
                            sw.Flush();
                        }
                    }
                    }));
                }
                catch
                {
                }
                finally
                {
                    ServerPipe.Disconnect();
                }
                this.m_service.m_runninPipe.Remove(this);
            }
        }
    }
}

