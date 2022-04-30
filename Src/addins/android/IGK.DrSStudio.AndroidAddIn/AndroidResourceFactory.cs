

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidResourceFactory.cs
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
file:AndroidResourceFactory.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace IGK.DrSStudio.Android
{
    class AndroidResourceFactory
    {
        public IAndroidResourceItem CreateStringResources(string name, string value)
        {
            return null;
        }
        /// <summary>
        /// create a resource container
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AndroidResourceContainerBase CreateResourceContainer(string name)
        {
            Type t = Type.GetType(string.Format("{0}.Android{1}ResourceContainer",
                MethodBase.GetCurrentMethod().DeclaringType.Namespace,
                name), false, true);
            if (t == null)
                return null;
            AndroidResourceContainerBase v_Res =
                t.Assembly.CreateInstance(t.FullName) as AndroidResourceContainerBase;
            return v_Res;
        }
    }
}

