using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent a CoreFont Register class
    /// </summary>
    public class CoreFontRegister
    {
        static Dictionary<string, CoreFont> sm_fonts;
        static CoreFontRegister() {
            sm_fonts = new Dictionary<string, CoreFont>();
            CoreApplicationManager.ApplicationExit += _ApplicationExit;
        }

        static void _ApplicationExit(object sender, EventArgs e)
        {
            //free all font
            foreach (var item in sm_fonts.Values)
            {
                item.Dispose();
            }
            sm_fonts.Clear();
        }
        /// <summary>
        /// get font from definition
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public static CoreFont GetFont(string definition)
        {
            if (sm_fonts.ContainsKey(definition))
            {
                var ftt = sm_fonts[definition];
                if (!ftt.IsDisposed )
                    return ftt;
                sm_fonts.Remove (definition);
            }

            var ft = CoreFont.CreateFrom(definition, null);
            if (ft != null)
            {
                sm_fonts.Add(definition, ft);
                return ft;
            }
            return null;
        }
        public static bool UnRegister(string definition)
        {
            if (sm_fonts.ContainsKey(definition))
            {
                var ftt = sm_fonts[definition];
                if (!ftt.IsDisposed)                    
                    ftt.Dispose();
                sm_fonts.Remove(definition);
                return true;
            }
            return false;
        }

        public static CoreFont GetFontById(string name, string defaultDefinition=CoreConstant.DEFAULT_FONT_DEFINITION)
        {
            CoreFont ft = null;//GetFont(CoreSettings[""].);
            ft = GetFont( CoreSettings.GetSettingValue("Theme." + name, defaultDefinition));
            
            return ft;
        }
    }
}
