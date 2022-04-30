using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Property | 
    AttributeTargets .Interface 
     , AllowMultiple =false , Inherited =true  )]
    public class CoreDataGuiAttribute : Attribute 
    {
        private enuGuiType m_GuiType;
        /// <summary>
        /// represent a graphic user interface type
        /// </summary>
public enuGuiType GuiType{
get{ return m_GuiType;}
set{ 
if (m_GuiType !=value)
{
m_GuiType =value;
}
}
}
        
            
        public CoreDataGuiAttribute()
        {

        }
        public CoreDataGuiAttribute(enuGuiType t) :this()
        {
            this.m_GuiType = t;
        }
        public static CoreDataGuiAttribute GetAttribute(Type interfaceType)
        {
            return Attribute.GetCustomAttribute(interfaceType, typeof(CoreDataGuiAttribute)) as CoreDataGuiAttribute;
        }
    }
}
