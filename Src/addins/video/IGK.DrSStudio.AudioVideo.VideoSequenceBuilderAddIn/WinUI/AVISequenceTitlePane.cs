using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinCore.Dispatch;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.AudioVideo.WinUI
{
    class AVISequenceTitlePane : RectangleElement, ICore2DDrawingVisitable 
    {
        private AVISequenceViewer c_viewer;
        private TextElement c_text;

        public override void Dispose()
        {
            this.c_viewer.SizeChanged -= _SizeChanged;
            base.Dispose();
        }
        public AVISequenceTitlePane(AVISequenceViewer viewer)
        {
            this.c_viewer = viewer;
            this.c_text = new TextElement();
            this.c_text.FillBrush.SetSolidColor(Colorf.White);
            this.c_text.StrokeBrush.SetSolidColor(Colorf.Transparent);
            this.c_text.Font.CopyDefinition("FontName:Tahoma; Size: 11pt");
            this.c_text.RenderMode = enuTextElementMode.Text;
            this.c_text.Font.HorizontalAlignment = enuStringAlignment.Center;
            this.c_text.Font.VerticalAlignment = enuStringAlignment.Center;

            this.c_viewer.SizeChanged += _SizeChanged;

            //this.c_viewer.Dispatcher.Register(this,
            //    WinCoreControlDispatcher.MouseClickDispatching,
            //    new EventHandler(
            //        (o, e) => {
            //            System.Windows.Forms.MessageBox.Show("you click on the bar");
            //        }
            //   )
            //);
        }

        void _SizeChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();
            var v_rc = new Rectanglef(0, 0, this.c_viewer.Width, "24px".ToPixel());
            this.Bounds = v_rc;
            this.c_text.Bounds = v_rc;
            this.ResumeLayout();
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            Rectanglef rc  = new Rectanglef ( 0, 0, this.c_viewer.Width, "24px".ToPixel());
            visitor.FillRectangle(Colorf.Black, 0, 0, this.c_viewer.Width, "24px".ToPixel());
            visitor.DrawRectangle(Colorf.Black, 0, 0, this.c_viewer.Width, "24px".ToPixel());
            c_text.Text = this.c_viewer.Project.Name;
            c_text.Draw(visitor);

        }
    }
}
