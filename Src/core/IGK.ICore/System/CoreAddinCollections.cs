

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAddinCollections.cs
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
file:CoreAddinCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection ;
namespace IGK.ICore
{
    [Serializable ()]
    /// <summary>
    /// represent thte base addin collection
    /// </summary>
    public sealed class CoreAddinCollections : MarshalByRefObject , ICoreAddInCollections 
    {
        List<ICoreAddIn> m_addins;
        Dictionary<string, Assembly> m_assemblies;
        #region ICoreAddInCollections Members
        /// <summary>
        /// get the number of addings
        /// </summary>
        public int Count
        {
            get { return this.m_addins.Count; }
        }
        public ICoreAddIn this[int index]
        {
            get {
                return this.m_addins[index];
            }
        }
        #endregion
        #region IEnumerable Members
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_addins.GetEnumerator();
        }
        #endregion
        internal void Add(ICoreAddIn addin)
        {
            this.m_addins.Add(addin);
            m_assemblies.Add (addin.Assembly.FullName, addin.Assembly );
        }
        /// <summary>
        /// return a assembly location from name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetAssemblyLocation(string name)
        {
            Assembly asm = GetAssembly(name);
            if (asm != null)
                return asm.Location;
            return null;
        }
        public System.Reflection.Assembly GetAssembly(string name) 
        {
            if (string.IsNullOrEmpty(name))
                return null;
            name = name.ToLower();
            foreach (ICoreAddIn item in this.m_addins)
            {
                if ((!string.IsNullOrEmpty (item.Attribute.FriendlyName) && (item.Attribute.FriendlyName.ToLower() == name) )||
                    (item.Assembly.FullName.Split (',')[0].ToLower() == name))

                {
                    return item.Assembly;
                }
            }
            return null;
        }

        public Assembly GetAssemblyFromFullName(string fullName) {
            if (this.m_assemblies.ContainsKey(fullName)) {
            return this.m_assemblies[fullName ];
            }
            return null; 

        }
        
        //.ctr
        public CoreAddinCollections()
        {
            this.m_addins = new List<ICoreAddIn>();
            this.m_assemblies = new Dictionary<string, Assembly> ();
        }
    }
}

