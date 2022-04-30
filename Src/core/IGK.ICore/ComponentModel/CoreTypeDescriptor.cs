using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.ComponentModel
{
    public class CoreTypeDescriptor
    {
        static Dictionary<Type, CoreTypeDescriptorInfo> sm_infos;

        static CoreTypeDescriptor()
        {
            sm_infos = new Dictionary<Type, CoreTypeDescriptorInfo>();
        }
        public static TypeConverter GetConverter(Object target)
        {
            if (target != null)
                return GetConverter(target.GetType());
            return null;
        }
        public static TypeConverter GetConverter(Type target)
        {
            if (sm_infos.ContainsKey(target))
            {
                return sm_infos[target].Converter();
            }
            return TypeDescriptor.GetConverter(target);
        }
        public static void AddAttributes(Type target, Attribute attribute)        
        {
            if (sm_infos.ContainsKey(target))
            {
                sm_infos[target].SetAttribute(attribute);
            }
            else {
                CoreTypeDescriptorInfo info = new CoreTypeDescriptorInfo();
                sm_infos.Add(target, info);
                info.SetAttribute(attribute);
            }
        }

        class CoreTypeDescriptorInfo
        {
            private TypeConverterAttribute m_typeConverterAttribute;
            /// <summary>
            /// return a new instance converter
            /// </summary>
            /// <returns></returns>
            public TypeConverter Converter (){
                if (m_typeConverterAttribute !=null)
                {
                    Type t = Type.GetType (m_typeConverterAttribute.ConverterTypeName);
                        return t.Assembly.CreateInstance (t.FullName) as TypeConverter;
                }
                return null;
            }

            internal void SetAttribute(Attribute attribute)
            {
                MethodInfo.GetCurrentMethod().Visit(this, attribute);
            }
            private void SetAttribute(TypeConverterAttribute attribute)
            {
                this.m_typeConverterAttribute = attribute;
            }
        }

      
    }
}
