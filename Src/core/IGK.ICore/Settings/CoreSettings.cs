

using IGK.ICore;
using IGK.ICore.ComponentModel;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSettings.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreSettings.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Settings
{
    /// <summary>
    /// Represent the settings Manager
    /// </summary>
    public sealed class CoreSettings
    {
        public static CoreSettingCollections Settings {
            get {
                return CoreSystem.GetSettings();
            }
        }

        public static T GetSettingValue<T>(string key, T defaultValue =default(T))
        {
            object t = GetSettingValue(key,(object) defaultValue);
            if (t is T)
            {
                return    (T)t;
            }
            else if ((typeof(T) == typeof(string)) && (t != null))
            {
                return (T)(object)(t.ToString());
            }
            return default(T);
        }
        /// <summary>
        /// retrieve a setting value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object GetSettingValue(string key, object defaultValue=null)
        {
            string[] r = key.Split('.');
            ICoreSetting v_setting = null;
            ICoreSettingValue v_appSetting = null;
            string v_key = string.Empty;
            for (int i = 0; i < r.Length; i++)
            {
                if (v_setting == null)
                {
                    if (i > 0)
                        v_key += ".";
                    v_setting = Settings[v_key+r[i]];                   
                    v_key += r[i];
                    continue;
                }
                if (v_appSetting == null)
                {
                    v_appSetting = v_setting[r[i]];
                    if (v_appSetting == null)
                        break;
                }
                else {
                    v_appSetting = v_appSetting[r[i]];
                }
            }
            if (v_appSetting != null)
            {
                if (defaultValue != null) 
                { 
                    Type t =  (v_appSetting.Value ??  typeof(string)).GetType();
                    if (defaultValue.GetType() != t)
                    {
                        TypeConverter conv = CoreTypeDescriptor.GetConverter(defaultValue.GetType());
                        return conv.ConvertFrom(v_appSetting.Value);
                    }
                }

                return v_appSetting.Value;
            }
            return defaultValue;
        }


        /// <summary>
        /// retrieve a setting
        /// </summary>
        /// <param name="key">path to setting</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ICoreSettingValue GetSetting(string key)
        {
            if (string.IsNullOrEmpty (key)) return null;
            string[] r = key.Split('.');
            string v_rkey = Path.GetFileNameWithoutExtension(key);
            if (Settings.Contains(v_rkey)) {
                return Settings[v_rkey][r[r.Length-1]];
            }
      
            ICoreSetting v_setting = null;
            ICoreSettingValue v_appSetting = null;
            for (int i = 0; i < r.Length; i++)
            {
                if (v_setting == null)
                {
                    v_setting = Settings[r[i]];
                    continue;
                }
                if (v_appSetting == null)
                {
                    v_appSetting = v_setting[r[i]];
                    if (v_appSetting == null)
                        break;
                }
                else
                {
                    v_appSetting = v_appSetting[r[i]];
                }
            }
            //if (v_appSetting != null)
            //{
            //    if (defaultValue != null)
            //    {
            //        Type t = (v_appSetting.Value ?? typeof(string)).GetType();
            //        if (defaultValue.GetType() != t)
            //        {
            //            TypeConverter conv = CoreTypeDescriptor.GetConverter(defaultValue);
            //            return conv.ConvertFrom(v_appSetting.Value);
            //        }
            //    }

                return v_appSetting;//.Value;
            //}
            //return defaultValue;
        }


        /// <summary>
        /// set value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetSettingValue(string name, object  value)
        {
            ICoreSettingValue s = GetSetting(name);
            if (s != null)
                s.Value = value;
            else
            {
                CoreSettingBase.DummySetting v_s = null;
                ICoreSetting v_setting = null;
                ICoreSettingValue v_appSetting = null;
                string[] r = name.Split('.');
                for (int i = 0; i < r.Length; i++)
                {
                    if (v_setting == null)
                    {
                        v_setting = Settings[r[i]];
                        if (v_setting == null)
                        {
                            v_s = new CoreSettingBase.DummySetting (r[i]);
                            Settings.Add(r[i], v_s);
                            v_setting = v_s;
                        }
                        continue;
                    }
                    if (v_appSetting == null)
                    {
                        v_appSetting = v_setting[r[i]];
                        if (v_appSetting == null)
                        {
                            v_s = new CoreSettingBase.DummySetting(r[i]);
                            (v_setting as CoreSettingBase.DummySetting).Add (v_s);
                            v_appSetting = v_s;
                        }
                    }
                    else
                    {
                        v_appSetting = v_appSetting[r[i]];
                    }
                }
                if (v_appSetting != null)
                {
                    v_appSetting.Value = value;
                }
            }
        }
    }
}

