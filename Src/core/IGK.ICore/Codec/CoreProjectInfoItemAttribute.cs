

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreProjectInfoItemAttribute.cs
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
file:CoreProjectInfoItemAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    [AttributeUsage (AttributeTargets.Class , AllowMultiple = false , Inherited = true )]
    public class CoreProjectInfoItemAttribute  : Attribute 
    {
        private string m_Name;
        public string Name
        {
            get { return m_Name; }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="name">Display name of the item project</param>
        /// <param name="Target">Target</param>
        public CoreProjectInfoItemAttribute(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new CoreException(enuExceptionType.ArgumentIsNull, "name");
            this.m_Name = name;
        }
        public override string ToString()
        {
            return string.Format("ProjectInfoItemAttribute : {0}", this.Name);
        }
    }
}

