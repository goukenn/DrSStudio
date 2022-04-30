using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Entities
{
    /// <summary>
    /// android attribute enties
    /// </summary>
    public class AndroidAttrs : AndroidEntities 
    {
        private string m_Name;
        private AndroidAttrsFormat m_Format;

        public AndroidAttrsFormat Format
        {
            get { return m_Format; }
            set
            {
                if (m_Format != value)
                {
                    m_Format = value;
                }
            }
        }
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
        public override string EntityTypeName
        {
            get
            {
                return AndroidConstant.ANDROID_ATTRIBUTES;
            }
        }
        public AndroidAttrs()
        {
            
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
