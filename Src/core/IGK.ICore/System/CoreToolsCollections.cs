

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreToolsCollections.cs
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
file:CoreToolsCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.Tools ;
    using System.Reflection;
    /// <summary>
    /// represent the core tool generation
    /// </summary>
    internal sealed class CoreToolsCollections :
        CoreSystemCollections ,
        ICoreToolsCollections
    {
        Dictionary<string, ICoreTool> m_tools;
        public CoreToolsCollections(CoreSystem core):
            base(core )
        {
            this.m_tools = new Dictionary<string, ICoreTool>();            
            core.RegisterTypeLoader (RegisterType);
        }
        #region ICoreToolsCollections Members
        public int Count
        {
            get { return this.m_tools.Count; }
        }
        public IGK.ICore.Tools.ICoreTool this[string name]
        {
            get {
                if (this.m_tools.ContainsKey(name))
                {
                    return this.m_tools[name];
                }
                return null;
            }
        }
        #endregion
        #region IEnumerable Members
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_tools.GetEnumerator();
        }
        #endregion
        protected override void RegisterType(Type type)
        {
            CoreToolsAttribute v_attr = Attribute.GetCustomAttribute(type,
          typeof(CoreToolsAttribute)) as CoreToolsAttribute;
            if (v_attr != null)
            {
                if (!this.m_tools.ContainsKey(v_attr.Name))
                {
                    PropertyInfo prInfo = type.GetProperty(CoreConstant.INSTANCE_PROPERTY, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                    if (prInfo != null)
                    {
                        ICoreTool tool =
                            prInfo.GetValue(
                            null, null) as ICoreTool;
                        this.m_tools.Add(v_attr.Name, tool);
                    }
                }
            }
        }
    }
}

