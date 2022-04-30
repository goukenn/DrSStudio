using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.FileViewer.WinUI
{
    public class GKDSFVView : UserControl
    {
        private string m_FileName;
        GKDSFVVisitor m_visitor;
        [System.ComponentModel.DesignerSerializationVisibility (System.ComponentModel.DesignerSerializationVisibility.Hidden )]
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

        private void OnFileNameChanged(EventArgs eventArgs)
        {
            if (this.FileNameChanged != null)
                this.FileNameChanged(this, eventArgs);
        }

        public event EventHandler FileNameChanged;

        public GKDSFVView()
        {
            this.m_visitor = new GKDSFVVisitor(this);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw , true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer , true);

            this.Paint += _Paint;
            this.FileNameChanged += GKDSFVView_FileNameChanged;
            this.FileName = @"D:\MyWorks\MyGKDSFiles\androidPresentation.gkds";
        }

        void GKDSFVView_FileNameChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        void _Paint(object sender, PaintEventArgs e)
        {
            this.m_visitor.Render(e.Graphics, this.FileName);
        }
    }
}
