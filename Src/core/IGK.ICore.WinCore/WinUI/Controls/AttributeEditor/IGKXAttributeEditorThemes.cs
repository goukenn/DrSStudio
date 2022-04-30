using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    class IGKXAttributeEditorThemes
    {
        #pragma warning disable

        public static readonly string AttributeEditorItemValueFont;
        public static readonly string AttributeEditorItemFont;

        #pragma warning enabled

        static IGKXAttributeEditorThemes() 
        {
            Type t = MethodInfo.GetCurrentMethod().DeclaringType ;
            foreach (var item in t.GetFields(BindingFlags.Static | BindingFlags.Public ))
            {
                item.SetValue(null, item.Name);
            }
        }
    }
}
