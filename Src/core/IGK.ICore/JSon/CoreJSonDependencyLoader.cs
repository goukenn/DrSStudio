using IGK.ICore.ComponentModel;
using IGK.ICore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.JSon
{
    public class CoreJSonDependencyLoader : IJSonObjectListener
    {
        private CoreDependencyObject m_host;
        private CoreDependencyObject m_property;

        public CoreJSonDependencyLoader(CoreDependencyObject host, CoreDependencyObject property)
        {
            if (host == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "host");
            if (property == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "property");
            this.m_host = host;
            this.m_property = property;
        }
        void IJSonObjectListener.SetValue(string name, string value)
        {
            CoreDependencyProperty p = CoreDependencyObject.GetRegisterProperty(m_property.GetType().FullName + "::" + name);
            if (p != null)
                m_host.SetValue(p, value);
        }

        object IJSonObjectListener.GetValue(string name)
        {
            return m_host.GetValue(name);
        }

        T IJSonObjectListener.GetValue<T>(string name)
        {
            var conv = CoreTypeDescriptor.GetConverter(typeof(T));
            var obj = ((IJSonObjectListener)this).GetValue(name);
            if ((obj != null) && conv.CanConvertFrom(obj.GetType()))
                return (T)conv.ConvertFrom(obj);
            return default(T);
        }

        string IJSonObjectListener.ToJSonString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            bool r = false;
            foreach (CoreDependencyProperty item in CoreDependencyProperty.GetProperties(this.GetType()))
            {
                if (r)
                    sb.Append(",");
                sb.Append(string.Format("{0}={1}", item.Name, m_host.GetValue(item)));
                r = true;
            }
            sb.Append("}");

            return sb.ToString();
        }
    }
}
