using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI.Common
{
    public interface IXCommonSaveFileAsDialog : IXCommonDialog
    {
        string FileName { get; set; }
        string Filter { get; set; }
    }
}
