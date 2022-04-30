using IGK.GS.DataTable;
using IGK.ICore.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS
{
    /// <summary>
    /// represent global system database configuration setting manager
    /// </summary>
    public class GSSystemConfigSettings
    {
        public static int DECIMAL_ROUND {
            get
            {
                return GetConfigValue<int>("GSDECIMAL_ROUND", 2);
            }
        }

        /// <summary>
        /// get config value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetConfigValue<T>(string name, T defaultValue = default(T))
        {
            ITbGSConfigs c = GSDB.SelectFirstRowInstance<ITbGSConfigs>(name.ToIdentifier(GSConstant.CL_NAME));
            var r = CoreTypeDescriptor.GetConverter(typeof(T));
            if (c != null)
                return (T)r.ConvertFrom(c.clValue);
            return defaultValue;
        }
        public static string GetConfigValue(string name, object @default)
        {
            ITbGSConfigs c = GSDB.SelectFirstRowInstance<ITbGSConfigs>(name.ToIdentifier(GSConstant.CL_NAME));
            if (c != null)
                return c.clValue;
            return @default == null ? string.Empty : @default.ToString();
        }

        public static void SetConfigValue(string name, string i)
        {
            ITbGSConfigs c = GSDB.SelectFirstRowInstance<ITbGSConfigs>(name.ToIdentifier(GSConstant.CL_NAME));
            if (c != null)
            {
                c.clValue = i;
                c.Update();
            }
            else
            {

                c = GSDB.CreateInterfaceInstance<ITbGSConfigs>();
                c.clName = name;
                c.clValue = i;
                c.Insert();
            }
        }

    }
}
