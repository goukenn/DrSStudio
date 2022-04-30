using IGK.ICore.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Dependency
{
    /// <summary>
    /// class that store CoreDependencyProperty diffent value
    /// </summary>
    internal class CoreDependencyValue
    {
        private Dictionary<CoreDependencyProperty, CoreDependencyStorage> m_propertyStorages;
        // store depenpencyProperty and value storage

        /// <summary>
        /// represent a dependency value storage
        /// </summary>
        class CoreDependencyStorage{
            private Dictionary<enuDependencyValueType, object> mV; ///storage of the value

            /// <summary>
            /// .ctr
            /// </summary>
            public CoreDependencyStorage()
            {
                this.mV = new Dictionary<enuDependencyValueType, object>();
            }

            public CoreDependencyStorage(object value, enuDependencyValueType valueType): this()
            {
                if (value!=null)
                this.mV.Add(valueType, value);
            }

            internal object GetValue(enuDependencyValueType enuDependencyValueType)
            {
                //get an effective value from context 

                return this.mV[enuDependencyValueType];
            }

            internal void SetValue(object value, enuDependencyValueType valueType)
            {
                //store or remove value
                if (value == null)
                {
                    if (this.mV.ContainsKey(valueType))
                        this.mV.Remove(valueType);
                    return;
                }                    
                this.mV[valueType] = value;
            }
            public override string ToString()
            {
                return string.Format("CoreDependencyStorage[Count:{0}]", this.mV.Count);
            }
        }

        public override string ToString()
        {
            return string.Format("CoreDependencyValue[PropertyCount:"+this.m_propertyStorages.Count+"]");
        }
        public CoreDependencyValue()
        {
            this.m_propertyStorages = new Dictionary<CoreDependencyProperty, CoreDependencyStorage>();
        }
        /// <summary>
        /// get the dependent value property
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public object GetValue(CoreDependencyProperty prop)
        {
            if (this.m_propertyStorages.ContainsKey (prop))
                return this.m_propertyStorages[prop].GetValue(enuDependencyValueType.Default);
            return null;
        }
        /// <summary>
        /// set the dependent value
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <param name="valueType"></param>
        public void SetProperty(CoreDependencyProperty prop, object value, enuDependencyValueType valueType)
        {
            if (prop == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "prop");

            var s  = CoreTypeDescriptor.GetConverter(prop.PropertyType).ConvertFrom(value);

            if (this.m_propertyStorages.ContainsKey(prop))
                this.m_propertyStorages[prop].SetValue(s, valueType);
            else {
                if (s == null)
                    return;
                this.m_propertyStorages.Add(prop, new CoreDependencyStorage(s, valueType));
            }
        }

        internal System.Collections.IEnumerable GetValues(enuDependencyValueType type)
        {
            Dictionary<CoreDependencyProperty, object> v_value = new Dictionary<CoreDependencyProperty, object>();
            foreach (var item in this.m_propertyStorages)
            {
                var c = item.Value.GetValue(type);
                if (c != null) {
                    v_value.Add(item.Key, c);
                }
            }
            return v_value;
        }
    }
}
