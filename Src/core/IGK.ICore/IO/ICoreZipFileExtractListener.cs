using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.IO
{
    public interface ICoreZipFileExtractListener
    {
        bool Extract(string filename);
    }
}
