using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// Indicate that a control is registrable
    /// </summary>
    [AttributeUsage (AttributeTargets.Class , Inherited = false , AllowMultiple = false )]
    public class CoreRegistrableControlAttribute : Attribute
    {
        private bool m_IsRegistrable;
        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                    OnNameChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler NameChanged;

        protected virtual void OnNameChanged(EventArgs e)
        {
            if (NameChanged != null)
            {
                NameChanged(this, e);
            }
        }


        public bool IsRegistrable
        {
            get { return m_IsRegistrable; }
            set
            {
                if (m_IsRegistrable != value)
                {
                    m_IsRegistrable = value;
                }
            }
        }
        public CoreRegistrableControlAttribute()
        {
            this.m_IsRegistrable = true;
        }
        public CoreRegistrableControlAttribute(string name):this()
        {
            this.m_Name = name;
        }
        public CoreRegistrableControlAttribute(Type t):this()
        {
            this.Name = t.Name;
        }
    }
}
