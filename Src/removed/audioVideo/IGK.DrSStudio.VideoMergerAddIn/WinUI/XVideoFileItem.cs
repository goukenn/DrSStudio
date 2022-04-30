

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoFileItem.cs
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
file:XVideoFileItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.WinUI
{
    public class XVideoFileItem : System.Windows.Forms.Control 
    {
        IVideoFile m_VidFile;
        const int SMALLSIZE = 32;
        XVideoFileControl ParentControl;
        public XVideoFileItem(XVideoFileControl Parent, IVideoFile vidfile)
        {
            this.MinimumSize = new Size(0, SMALLSIZE);
            this.m_VidFile = vidfile;
            this.ParentControl = Parent;
            this.ParentControl.Controls.Add(this);
            this.ParentControl.SizeChanged += new EventHandler(ParentControl_SizeChanged);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.ResizeRedraw , true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint , true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer , true);
            this.BackColor = Color.Transparent;
            this.ContextMenuStrip = MergerContextMenuItem.Instance.ContextMenuStrip;   
        }
        void ParentControl_SizeChanged(object sender, EventArgs e)
        {
            UpdateSize();
        }
        internal void UpdateSize()
        {
           int i =  this.ParentControl.Items.IndexOf(this);
           this.Bounds = new Rectangle(
               0,
               i * SMALLSIZE,
               this.ParentControl.Width,
               SMALLSIZE);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_VidFile.Dispose();
                this.m_VidFile = null;
            }
            base.Dispose(disposing);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawString(this.m_VidFile.ToString(),
                this.Font, Brushes.Black, Point.Empty );
            ControlPaint.DrawBorder(e.Graphics,
                this.ClientRectangle,
                Color.Black,
                ButtonBorderStyle.Solid);
        }
        public override string ToString()
        {
            return base.ToString();
        }
        internal void ExportAudioAsWavFile()
        {
            if (!this.m_VidFile.HasAudio)
            {
                MessageBox.Show("No audio file to export");
                return;
            }
            using (System.Windows.Forms.SaveFileDialog  ofd = new SaveFileDialog ())
            {
                ofd.Filter  = "Audio files | *.wav;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Form frm = this.FindForm();
                        frm.Cursor = Cursors.WaitCursor;
                        IGK.AudioVideo.AVI.AVIFile.AudioStream aud =
                            IGK.AudioVideo.AVI.AVIStream.GetAudioStream(this.m_VidFile.AudHandle);
                        if (aud != null)
                        {
                            if (aud.ExportToWaveFile (ofd.FileName,
                                null, false , IntPtr.Zero ))
                              MessageBox.Show("Extraction audio Reussi");
                            else
                                MessageBox.Show("Extraction audio Impossible ");
                       }
                    frm.Cursor = Cursors.Default;
                }
            }
        }
        internal void ExportTrackFile()
        {
            if (!this.m_VidFile.HasAudio)
            {
                MessageBox.Show("No audio file to export");
                return;
            }
            using (System.Windows.Forms.SaveFileDialog ofd = new SaveFileDialog())
            {
                ofd.Filter = "Audio files | *.wav;| alls files | *.*;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Form frm = this.FindForm();
                    frm.Cursor = Cursors.WaitCursor;
                    IGK.AudioVideo.AVI.AVIFile.AudioStream aud =
                        IGK.AudioVideo.AVI.AVIStream.GetAudioStream(this.m_VidFile.AudHandle);
                    if (aud != null)
                    {
                        if (IGK.AudioVideo.AVI.AVIStream.ExtractStreamToDataFile(aud, ofd.FileName))
                        {
                            MessageBox.Show("Extraction audio Reussi");
                        }
                        else {
                            MessageBox.Show("Extraction audio Impossible ");
                        }
                    }
                    frm.Cursor = Cursors.Default;
                }
            }
        }
    }
}

