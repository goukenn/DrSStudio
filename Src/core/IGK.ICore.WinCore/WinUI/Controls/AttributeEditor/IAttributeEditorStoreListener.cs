using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    public interface IAttributeEditorStoreListener
    {
        void StoreAttribute(IGKXAttributeEditor editor, string filename);
    }
}
