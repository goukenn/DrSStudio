using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.DB
{
    /// <summary>
    /// internal dummy property info
    /// </summary>
    class CoreDummyPropertyInfo : ICoreDataPropertyInfo
    {
        private Type InterfaceType;
        CoreDataGuiAttribute m_guiAttr = null;
        CoreDataTableDisplayInfoAttribute m_displayAttr = null;

        internal CoreDummyPropertyInfo(Type InterfaceType,
            CoreDataGuiAttribute attr,
           CoreDataTableDisplayInfoAttribute m_displayAttr)
        {


            this.InterfaceType = InterfaceType;
            this.m_guiAttr = attr;
            this.m_displayAttr = m_displayAttr;
        }
        public Type PropertyType
        {
            get { return InterfaceType; }
        }


        public CoreDataGuiAttribute GetGuiAttribute()
        {
            return this.m_guiAttr;
        }


        public CoreDataTableDisplayInfoAttribute GetDisplayInfoAttribute()
        {
            return this.m_displayAttr;
        }
    }
}
