using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    /// <summary>
    /// represent les informations à appliquer au type de colomne 
    /// </summary>
    public class GSDataFieldAttribute : Attribute
    {
        private enuGSDataField m_Biding;
        private int m_MemberIndex;
        private int m_Length;
        private string m_TypeName;
        private string m_ColumnName;
        private enuGSDataRelation m_Relation;
        private string m_InsertFunction;
        private string m_UpdateFunction;

        public string UpdateFunction
        {
            get { return m_UpdateFunction; }
            set
            {
                if (m_UpdateFunction != value)
                {
                    m_UpdateFunction = value;
                }
            }
        }
        public string InsertFunction
        {
            get { return m_InsertFunction; }
            set
            {
                if (m_InsertFunction != value)
                {
                    m_InsertFunction = value;
                }
            }
        }

        /// <summary>
        /// get or set the data relation. used in future. to mark a relation between column data
        /// </summary>
        public enuGSDataRelation Relation
        {
            get { return m_Relation; }
            set
            {
                if (m_Relation != value)
                {
                    m_Relation = value;
                }
            }
        }

        /// <summary>
        /// get or set the column name. if not specified the name of the properties will be used.
        /// </summary>
        public string ColumnName
        {
            get { return m_ColumnName; }
            set
            {
                if (m_ColumnName != value)
                {
                    m_ColumnName = value;
                    OnColumnNameChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ColumnNameChanged;

        protected virtual void OnColumnNameChanged(EventArgs e)
        {
            if (ColumnNameChanged != null)
            {
                ColumnNameChanged(this, e);
            }
        }


        /// <summary>
        /// get or set the name of data type
        /// </summary>
        public string TypeName
        {
            get { return m_TypeName; }
            set
            {
                if (m_TypeName != value)
                {
                    m_TypeName = value;
                }
            }
        }
        public int Length
        {
            get { return m_Length; }
            set
            {
                if (m_Length != value)
                {
                    m_Length = value;
                }
            }
        }

        /// <summary>
        /// used to store registry on a single column info
        /// </summary>
        public int MemberIndex
        {
            get { return m_MemberIndex; }
            set
            {
                if (m_MemberIndex != value)
                {
                    m_MemberIndex = value;
                }
            }
        }

        public enuGSDataField Binding
        {
            get { return m_Biding; }
            set
            {
                if (m_Biding != value)
                {
                    m_Biding = value;
                }
            }
        }
        public GSDataFieldAttribute(enuGSDataField biding)
            : this()
        {
            this.m_Biding = biding;
            this.m_Length = 11;

        }
        public GSDataFieldAttribute()
        {
            this.m_Biding = enuGSDataField.None;
            this.m_Length = 11;

        }
        public string Description { get; set; }
        public string Default { get; set; }
    }
}
