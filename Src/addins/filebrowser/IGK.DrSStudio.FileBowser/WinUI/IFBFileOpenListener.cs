using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.FileBrowser.WinUI
{
    public interface IFBFileOpenListener
    {
        void Open(string filename);
    }
}
