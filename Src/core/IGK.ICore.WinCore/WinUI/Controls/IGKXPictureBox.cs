using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    [CoreRegistrableControl("PictureBox")]
    class IGKXPictureBox : IGKXUserControl , ICorePictureBox
    {
        private System.Windows.Forms.PictureBox c_pcb;
        private enuZoomMode m_zoomMode;

        event EventHandler ZoomModeChanged;

        public IGKXPictureBox()
        {
            this.InitializeComponent();

            this.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;

        }

      

        private void InitializeComponent()
        {
            this.c_pcb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.c_pcb)).BeginInit();
            this.SuspendLayout();
            // 
            // c_pcb
            // 
            this.c_pcb.Location = new System.Drawing.Point(3, 3);
            this.c_pcb.Name = "c_pcb";
            this.c_pcb.Size = new System.Drawing.Size(280, 228);
            this.c_pcb.TabIndex = 0;
            this.c_pcb.TabStop = false;
            // 
            // IGKXPictureBox
            // 
            this.Controls.Add(this.c_pcb);
            this.Name = "IGKXPictureBox";
            this.Size = new System.Drawing.Size(286, 234);
            ((System.ComponentModel.ISupportInitialize)(this.c_pcb)).EndInit();
            this.ResumeLayout(false);

        }

        public ICoreBitmap Bitmap
        {
            get
            {
                return null;
            }
            set
            {
                if (value != null)
                {
                    this.c_pcb.Image = value.ToGdiBitmap();
                }
                else
                    this.c_pcb.Image = null;
            }
        }

        public enuZoomMode ZoomMode { get => m_zoomMode; set {
                if (this.m_zoomMode != value) {
                    this.m_zoomMode = value;
                    OnZoomModeChanged(EventArgs.Empty);
                }
            } }
        protected virtual  void OnZoomModeChanged(EventArgs 
            e)
        {
            switch (this.m_zoomMode)
            {
                case enuZoomMode.Normal:
                    this.c_pcb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
                    break;
                case enuZoomMode.NormalCenter:
                    this.c_pcb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
                    break;
                case enuZoomMode.ZoomNormal:
                    break;
                case enuZoomMode.ZoomCenter:
                    this.c_pcb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                    break;
                case enuZoomMode.Stretch:
                    this.c_pcb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                    break;
                default:
                    break;
            }
            ZoomModeChanged?.Invoke(this, e);
        }
    }
}
