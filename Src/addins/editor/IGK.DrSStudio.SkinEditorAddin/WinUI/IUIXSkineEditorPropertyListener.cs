using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.SkinEditorAddin.WinUI
{
    public interface IUIXSkineEditorPropertyListener
    {
        ICore.Settings.ICoreRendererSetting[] GetRendererSetting();
    }
}
