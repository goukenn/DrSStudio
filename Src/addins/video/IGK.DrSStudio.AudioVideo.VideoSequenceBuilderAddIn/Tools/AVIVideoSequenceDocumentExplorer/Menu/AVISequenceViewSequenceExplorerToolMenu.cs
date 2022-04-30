using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.Tools.AVIVideoSequenceDocumentExplorer.Menu
{
    [CoreViewMenu("VideoSequenceExplorer",0x300)]
    class AVISequenceViewSequenceExplorerToolMenu : CoreViewToolMenuBase
    {
        public AVISequenceViewSequenceExplorerToolMenu()
            : base(AVIVideoSequenceDocumentExplorerTool.Instance)
        {

        }
    }
}
