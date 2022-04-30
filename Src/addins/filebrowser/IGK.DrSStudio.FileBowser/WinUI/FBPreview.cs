using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using IGK.FileViewer;
using System.Drawing;

namespace IGK.DrSStudio.FileBrowser.WinUI
{
    /// <summary>
    /// file preview 
    /// </summary>
    class FBPreview : UserControl {
        private string m_FileName;

        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                    OnFileNameChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler FileNameChanged;
        ///<summary>
        ///raise the FileNameChanged 
        ///</summary>
        protected virtual void OnFileNameChanged(EventArgs e)
        {
            if (FileNameChanged != null)
                FileNameChanged(this, e);
        }

        public FBPreview()   {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer , true);
            this.SetStyle(ControlStyles.ResizeRedraw , true);
            this.FileNameChanged +=_FileChanged;
            this.Paint += _Paint;
        }

        void _Paint(object sender, PaintEventArgs e)
        {
            if (File.Exists (this.FileName)) { 
                //detect and render file name
                switch (Path.GetExtension(this.FileName).ToLower())
                {
                    case ".gkds":
                        var visitor = new GKDSFVVisitor(this);
                        visitor.Render(e.Graphics, this.FileName);
                        break;
                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                    case ".bmp":
                        using (var img = Image.FromFile(this.FileName))
                        {
                            if (img != null)
                            {
                                var v_st =  e.Graphics.Save();
                                IGK.ICore.Rectanglef v_rc = new ICore.Rectanglef(0, 0, this.Width, this.Height);
                                v_rc.Inflate(-10, -10);
                                float w = img.Width;
                                float h = img.Height;
                               var m = new System.Drawing.Drawing2D.Matrix();
                                float ex = v_rc.Width / w;
                                float ey = v_rc.Height / h;
                                ex = ey = Math.Min(ex, ey);

                                e.Graphics.TranslateTransform (v_rc.X, v_rc.Y, System.Drawing.Drawing2D.MatrixOrder.Append  );

                                m.Scale(ex, ey, System.Drawing.Drawing2D.MatrixOrder.Append);
                                m.Translate((v_rc.Width - (ex * w)) / 2.0f, (v_rc.Height - (ey * h)) / 2.0f, System.Drawing.Drawing2D.MatrixOrder.Append );
                                
                                e.Graphics.MultiplyTransform(m, System.Drawing.Drawing2D.MatrixOrder.Prepend );
                                using (var sk = new Bitmap(img))
                                {
                                    sk.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                                    //e.Graphics.DpiX = img.HorizontalResolution;
                                    //e.Graphics.DpiY = img.HorizontalResolution;
                                    e.Graphics.DrawImage(sk, Point.Empty);
                                }
                                e.Graphics.Restore(v_st);
                            }
                        }
                        break;
                    default :
                        //render unknow res
                        break;
                }
            }
        }

        private void _FileChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
