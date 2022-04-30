using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Entities
{
    class AndroidEntityColor : AndroidEntities
    {
        private string m_Name;
        private string m_Value;

        public string Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
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
        public AndroidEntityColor(string name, string value)
        {
            this.m_Name = name;
            this.m_Value = value;
        }
        public override string ToString()
        {
            return this.m_Name + " : " + this.m_Value;
        }

        public Colorf EvalData() {
            return Colorf.Empty;
        }
    }
}
