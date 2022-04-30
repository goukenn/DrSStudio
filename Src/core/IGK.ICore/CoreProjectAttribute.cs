

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreProjectAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreProjectAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    /// <summary>
    /// reprensent project attribute
    /// </summary>
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Assembly  , AllowMultiple = false , Inherited = false ) ]
    public class CoreProjectAttribute : Attribute 
    {
        private Guid m_guid;
        private string m_Description;
        private string m_Category;
        /// <summary>
        /// get or set the project category
        /// </summary>
        public string Category
        {
            get { return m_Category; }
            set
            {
                if (m_Category != value)
                {
                    m_Category = value;
                }
            }
        }
        /// <summary>
        /// get or set the description
        /// </summary>
        public string Description
        {
            get { return m_Description; }
            set
            {
                if (m_Description != value)
                {
                    m_Description = value;
                }
            }
        }
        /// <summary>
        /// get the project guid
        /// </summary>
        public Guid Guid
        {
            get { return m_guid; }           
        }
        public CoreProjectAttribute(string guid, string description, string category)
        {
            this.m_guid  = Guid.Parse (guid);
            this.m_Description = description;
            this.m_Category = category;
        }
    }
}

