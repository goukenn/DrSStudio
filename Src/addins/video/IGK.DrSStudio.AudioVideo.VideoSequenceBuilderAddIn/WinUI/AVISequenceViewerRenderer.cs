using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.WinUI
{
    class AVISequenceViewerRenderer
    {
        static AVISequenceViewerRenderer() {             
        }

        public static Colorf AVISequenceBackgroundColor { get { return CoreRenderer.GetColor("AVISequenceBackgroundColor", Colorf.Black); } }
    }
}
