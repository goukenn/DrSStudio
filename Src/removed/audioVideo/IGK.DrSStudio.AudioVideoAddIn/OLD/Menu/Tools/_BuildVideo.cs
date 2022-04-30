

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _BuildVideo.cs
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
file:_BuildVideo.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.AudioVideo.Menu.Tools
{
    using IGK.ICore;using IGK.AudioVideo.AVI;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio;
    [IGK.DrSStudio.Menu.CoreMenu("Tools.BuildVideo", 50, ImageKey="Menu_BuildVideo")]
    sealed class _BuildVideo : IGK.DrSStudio.Menu.CoreApplicationMenu 
    {
        private new ICore2DDrawingSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICore2DDrawingSurface;
            }
        }
        protected override bool PerformAction()
        {
            ICore2DDrawingSurface s = this.CurrentSurface as IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface ;
            if (s!=null)
            {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Avi Files | *.avi;";
                sfd.FileName = "NewAvi.avi";
                sfd.Title = "Build 1 min video - sample ";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (!BuildVideo(sfd.FileName, s.CurrentDocument, null))
                    {
                        MessageBox.Show("Some error happend");
                    }
                }
            }
            }
            return false;
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            this.Enabled  = false;
        }
        protected override void RegisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            workbench.CurrentSurfaceChanged += new CoreWorkingSurfaceChangedEventHandler(workbench_CurrentSurfaceChanged);
        }
        protected override void UnregisterBenchEvent(IGK.DrSStudio.WinUI.ICoreWorkbench workbench)
        {
            workbench.CurrentSurfaceChanged -= new CoreWorkingSurfaceChangedEventHandler(workbench_CurrentSurfaceChanged);
            base.UnregisterBenchEvent(workbench);
        }
        void workbench_CurrentSurfaceChanged(object o, CoreWorkingSurfaceChangedEventArgs e)
        {
            this.Enabled = (this.CurrentSurface != null);
        }
        private static bool  BuildVideo(string outfile, 
            IGK.DrSStudio.Drawing2D.ICore2DDrawingDocument doc,
            CoreJobProgressEventHandler proc)
        {
            AVIFile f = null;
            ICoreExtensionContext context =  doc.Extensions["AnimationContext"];
            try
            {
                f = AVIFile.CreateFile (outfile);
                if (f == null)
                    return false ;
                Bitmap bmp = CoreBitmapOperation.GetBitmap(doc, 96, 96);
                int fps = 24;
                AVIFile.VideoStream vid = f.AddNewVideoStream(fps, 1, 0,
                    bmp);
                //synchronize
                if (vid.BeginGetFrame())
                {
                    int nbr_frame = 1 * 60 * fps;
                    for (int i = 1; i < nbr_frame; i++)
                    {
                        vid.AddFrame(i, 1, bmp);
                        if (proc != null)
                            proc.BeginInvoke((i / nbr_frame)* 100.0f, null, null);
                   }
                    vid.EndGetFrame();
                    if (proc !=null)
                        proc.BeginInvoke(100.0f, null, null);
                }
                bmp.Dispose();
                //finish build video
              //  vid.ExportTo(IntPtr.Zero, "d:\\ppp.avi");
                vid.Dispose();
                //vid = f.GetVideoStream();
                //AVIFile.VideoStream tvid = AVIFile.VideoStream.Compress(vid, true);
                //AVIFile fs = AVIFile.Create(outfile + ".2.avi");
                //fs.AddVideoStream(tvid);
                //tvid.Release();
              // fs.Close();
            }
            catch (Exception Exception){
                MessageBox.Show("Exception: " + Exception.Message ,"Error");
            }
            finally
            {
                if (f != null)
                    f.Close();
            }
            return true;
        }
    }
}

