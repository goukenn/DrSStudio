using IGK.ICore;
using IGK.ICore.Xml;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006
#pragma warning disable IDE0017

namespace IGK.DrSStudio.Android.AndroidThemeBuilder.WinUI
{
    [ComVisible(true)]
    public  class AndroidThemeFileEditorScripting
    {
        private AndroidThemeFileEditorSurface m_owner;

        internal AndroidThemeFileEditorScripting(AndroidThemeFileEditorSurface androidThemeFileEditorSurface)
        {
            this.m_owner = androidThemeFileEditorSurface;
        }
        public string update_value(string id, string value) {
            return m_owner.UpdateTheme(id, value);
        }

        public string search_for(string d) {
            m_owner.SearchKey = d;
            return m_owner.getThemePropertiesList();
        }
        public string EditProperties() {
            //edit Theme Info
            CoreSystem.GetAction("Android.EditThemeInfo")?.DoAction();
            return this.m_owner.getThemeInfo();
        }
    }
}