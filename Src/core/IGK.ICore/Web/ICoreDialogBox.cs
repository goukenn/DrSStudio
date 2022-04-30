using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Web
{
    public interface ICoreDialogBox
    {
        IGK.ICore.WinUI.enuDialogResult DialogResult { get; set; }
    }
}
