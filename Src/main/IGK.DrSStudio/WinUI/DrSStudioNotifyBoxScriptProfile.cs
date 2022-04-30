using IGK.ICore.Web;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IGK.DrSStudio.WinUI
{
    [ComVisible(true)]
    public class DrSStudioNotifyBoxScriptProfile : CoreWebScriptObjectBase
    {
        public void close()
        {
            this.DialogResult = enuDialogResult.OK;
        }
    }
}
