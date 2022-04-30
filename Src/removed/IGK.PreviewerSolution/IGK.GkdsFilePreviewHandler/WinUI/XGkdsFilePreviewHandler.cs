

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XGkdsFilePreviewHandler.cs
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
file:XGkdsFilePreviewHandler.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ServiceProcess;
using System.IO.Pipes;
using System.IO;
namespace IGK.GkdsFilePreviewHandler.WinUI
{
    using IGK.ICore;using IGK.PreviewHandlerLib;
    using IGK.PreviewHandlerLib.WinUI;
    public sealed class XGkdsFilePreviewHandler : IGK.PreviewHandlerLib.WinUI.FileBasePreviewHandlerControl
    {
        Label c_lb_fname;        
        ServiceController m_service;
        PictureBox c_picbox;
        private string m_filename;
        class IGKXLabel : Label
        {
            public IGKXLabel()
            {
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);                
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                this.SetStyle(ControlStyles.ResizeRedraw, true);
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            }
        }
        class XPictureBox : PictureBox
        {
            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                //base.OnPaintBackground(pevent);
            }
            protected override void OnPaint(PaintEventArgs pe)
            {
                base.OnPaint(pe);
            }
            public XPictureBox()
            {
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                this.SetStyle(ControlStyles.ResizeRedraw, true);
            }
        }
        public XGkdsFilePreviewHandler()
            : base()
        {
            this.InitializeComponent();
            this.BackColor = Color.DarkGray;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, false);
            this.m_service = GkdsFileConstant.GetService(GkdsFileConstant.SERVICE_NAME);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }
        private void InitializeComponent()
        {
            SuspendLayout();
            c_lb_fname = new IGKXLabel();
            c_picbox = new XPictureBox();
            this.Controls.Add(c_lb_fname);
            this.Controls.Add(c_picbox);
            ResumeLayout();
        }
        public override void Unload()
        {
            //base.Unload();
        }
        public override void Load(System.IO.FileInfo file)
        {
            c_lb_fname.AutoSize = true;
            if (m_service == null)
            {
                c_lb_fname.Dock = DockStyle.Fill;
                c_lb_fname.TextAlign = ContentAlignment.MiddleCenter;
                c_lb_fname.Text = string.Format("No Service {0} found ", GkdsFileConstant.SERVICE_NAME);
                c_lb_fname.Visible = true;
                c_picbox.Visible = false;
                return;
            }
            c_lb_fname.Visible = true;
            c_picbox.Visible = true ;
            c_picbox.SizeMode = PictureBoxSizeMode.Zoom;
            c_picbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            c_picbox.Dock = DockStyle.Fill;
            c_lb_fname.Text = file.FullName;
            this.m_filename = file.FullName;
            SendCommandToPipe();
        }
        public void SendCommandToPipe(string filename)
        {
            this.m_filename = filename;
            this.SendCommandToPipe();
        }
        public void SendCommandToPipe()
        {
            if (m_service == null)
                return;
            NamedPipeClientStream v_clientPipe = new NamedPipeClientStream(".", GkdsFileConstant.PIPE_NAME, PipeDirection.InOut, PipeOptions.None,
                 System.Security.Principal.TokenImpersonationLevel.Impersonation);
            try
            {
                if (m_service.Status == ServiceControllerStatus.Stopped)
                {
                    this.m_service.Start();
                    this.m_service.WaitForStatus(ServiceControllerStatus.Running);
                }
                //
                m_service.ExecuteCommand((int)enuServiceCommand.GetBitmap);
                v_clientPipe.Connect();
                StreamReader sr = new StreamReader(v_clientPipe);
                if (sr.ReadLine() == GkdsFileConstant.GOOD_PIPE_NAME)
                {
                    StreamWriter sw = new StreamWriter(v_clientPipe);
                    sw.WriteLine(string.Join(";", new object[] { this.m_filename, this.Width, this.Height }));
                    sw.Flush();
                    byte[] sb = new byte[4096];
                    MemoryStream mem = new MemoryStream();
                    int v_count = 0;
                    //string v_st = sr.ReadToEnd();
                    //StreamWriter smw = new StreamWriter(mem);
                    //smw.Write(v_st);
                    //smw.Flush();
                    while ((v_count = sr.BaseStream.Read(sb, 0, sb.Length)) > 0)
                    {
                        mem.Write(sb, 0, v_count);
                    }
                    mem.Flush();
                    mem.Seek(0, SeekOrigin.Begin);
                    //StreamReader v_sr = new StreamReader(mem);
                    //string txt = v_sr.ReadToEnd();
                    //mem.Seek(0, SeekOrigin.Begin);
                    if (mem.Length == 0)
                    {
                        this.SetBitmap(null);
                    }
                    else
                    {
                        Bitmap bmp = null;
                        try
                        {
                            bmp = new Bitmap(mem);
                            this.SetBitmap(bmp);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //connection failed
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
            finally
            {
                //close connection
                if (v_clientPipe != null)
                    v_clientPipe.Close();
            }
        }
        private void SetBitmap(Bitmap bmp)
        {
            if (bmp == null)
            {
                this.c_picbox.Image = null;
            }
            else
            {
                if (c_picbox != null)
                {
                    this.c_picbox.Image = bmp;
                }
                else
                {
                    Form frm = new Form();
                    frm.BackgroundImage = bmp;
                    frm.BackgroundImageLayout = ImageLayout.None;
                    frm.ShowDialog();
                }
            }
        }
 }
}

