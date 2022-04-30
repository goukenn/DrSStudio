using IGK.ICore.WinUI.Dispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.WinUI
{
    public interface  IAVISequenceViewer
    {
        IAVISequenceProject Project { get; set; }
        ICoreDispatcher Dispatcher { get; }
        void LoadProjectFile(string filename);

        event EventHandler ProjectChanged;
    }
}
