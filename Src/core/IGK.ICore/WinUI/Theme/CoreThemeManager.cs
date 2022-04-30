using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Theme
{
    /// <summary>
    /// represent a CoreRenderer font utility  fonction 
    /// </summary>
    public static class CoreThemeManager
    {

        /// <summary>
        /// get he value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ICoreFont GetFont(string name) {
            return GetValue<ICoreFont>(name);
        }

        /// <summary>
        /// get the value of theme
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetValue<T>(string name)
        {
            return CoreRenderer.GetValue<T>(name);
        }
        /// <summary>
        /// get the value of theme
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetValue<T>(string name, T defaultValue=default(T))
        {
            ICoreRendererSetting m = CoreRenderer.GetSetting<T>(name, defaultValue);
            return (T)m.Value;
        }
        /// <summary>
        /// get the global setting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ICoreRendererSetting GetSetting<T>(string name, T defaultValue=default(T))
        {
            return CoreRenderer.GetSetting<T>(name, defaultValue);
        }
    }
}
