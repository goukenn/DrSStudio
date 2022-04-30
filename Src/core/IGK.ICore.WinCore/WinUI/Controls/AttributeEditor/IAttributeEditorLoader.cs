
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.Xml;
    using IGK.ICore.WinCore.WinUI.Controls;

    public interface IAttributeEditorLoader
    {
        void LoadAttribute(IGKXAttributeEditor editor, CoreXmlElement NodeName);
    }
}
