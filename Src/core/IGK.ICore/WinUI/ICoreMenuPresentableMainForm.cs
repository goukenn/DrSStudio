using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    public interface ICoreMenuPresentableMainForm : ICoreMainForm
    {
        bool ShowMenu { get; set; }
        event EventHandler ShowMenuChanged;
    }
}
