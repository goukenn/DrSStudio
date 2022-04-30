

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePathBrushStyleBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:CorePathBrushStyleBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D
{
    [TypeConverter(typeof(PathBrushStyleConverter))]
    /// <summary>
    /// represent the base
    /// </summary>
    public abstract class CorePathBrushStyleBase :
        IGK.DrSStudio.Drawing2D.ICore2DPathBrushStyle,
        IGK.DrSStudio.WinUI.Configuration.ICoreWorkingConfigurableObject
    {
        static Dictionary<string, Type> sm_register;
        static CorePathBrushStyleBase() {
            sm_register = new Dictionary<string, Type>();
        }
        public static bool Register(string name, Type type)
        {
            if (sm_register.ContainsKey(name) || (type == null) || !type.IsSubclassOf (typeof(CorePathBrushStyleBase )))
                return false;
            sm_register.Add(name, type);
            return true;
        }
        public event EventHandler PropertyChanged;
        ///<summary>
        ///raise the PropertyChanged 
        ///</summary>
        protected virtual void OnPropertyChanged(EventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        #region ICore2DPathBrushStyle Members
        public virtual void Generate(CoreGraphicsPath v_path)
        {
            v_path.Flatten();
        }
        public abstract string Id
        {
            get;
        }
        #endregion
        #region ICoreWorkingConfigurableObject Members
        public virtual  ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        public virtual IGK.DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
        {
            return IGK.DrSStudio.WinUI.Configuration.enuParamConfigType.ParameterConfig;
        }
        public virtual IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            return parameters;
        }
        #endregion
        #region ICoreWorkingDefinitionObject Members
        public virtual void CopyDefinition(string str)
        {
            string[] t = str.Split(';');
            System.ComponentModel.TypeConverter v_converter = null;
            for (int i = 1; i < t.Length; i++)
            {
                string[] d = t[i].Split(':');
                if (d.Length == 2)
                {
                    System.Reflection.PropertyInfo pr = GetType().GetProperty(d[0]);
                    if ((pr != null) && (pr.CanWrite))
                    {
                        v_converter = System.ComponentModel.TypeDescriptor.GetConverter(pr.PropertyType);
                        pr.SetValue(this, v_converter.ConvertFromString(d[1]), null);
                    }
                }
            }
        }
        public virtual string GetDefinition()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#" + this.Id + ";");
            return sb.ToString();
        }
        public string GetDefinition(IGK.DrSStudio.Codec.IXMLSerializer seri)
        {
            return this.GetDefinition();
        }
        #endregion
        #region ICloneable Members
        public virtual object Clone()
        {
            CorePathBrushStyleBase c = MemberwiseClone() as CorePathBrushStyleBase;
            c.CopyDefinition(this.GetDefinition());
            return c;
        }
        #endregion
        class PathBrushStyleConverter : TypeConverter 
        {
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                return base.CanConvertFrom(context, sourceType);
            }
            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (string.IsNullOrEmpty(value as string))
                    return null;
                string[] t = value.ToString().Split(';');
                string n = t[0];
                Type v_t = CorePathBrushStyleBase.GetTypeFromName(n);
                if (v_t != null)
                {
                    CorePathBrushStyleBase v_c = v_t.Assembly.CreateInstance(v_t.FullName)
                        as CorePathBrushStyleBase;
                    if (v_c != null)
                    {
                        v_c.CopyDefinition(value.ToString());
                        return v_c;
                    }
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
        internal static Type  GetTypeFromName(string n)
        {
            n = n.Replace("#", "");
            if (sm_register.ContainsKey(n))
            {
                return sm_register[n];
            }
            return null;
        }
    }
}

