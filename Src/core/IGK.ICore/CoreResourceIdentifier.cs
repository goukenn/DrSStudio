

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreResourceIdentifier.cs
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
file:CoreResourceIdentifier.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.Resources ;
    /// <summary>
    /// repreesent a core resource identifier
    /// </summary>
    public sealed class CoreResourceIdentifier
    {
        ICoreResourceItem m_resouce;
        /// <summary>
        /// get the id of the resources
        /// </summary>
        public string Id
        {
            get { return this.m_resouce .Id ; }
        }
        public CoreResourceIdentifier(ICoreResourceItem res)
        {
            if (res == null)
                throw new CoreException (enuExceptionType.ArgumentIsNull , "res");
            this.m_resouce = res;
        }
        public override string ToString()
        {
            return string.Format("#res/{0}", this.Id);
        }
        public static bool MatchString(string value)
        {
            return value.StartsWith("#res/");
        }
        public static CoreResourceIdentifier GetResource()
        {
            return null;
        }
        public static string GetResourceId(string ct)
        {
            return ct.Replace("#res/", "");
        }
    }
}

