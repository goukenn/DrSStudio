

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreTemplateRegisterCollections.cs
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
file:CoreTemplateRegisterCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
namespace IGK.ICore
{
    //internal sealed class CoreTemplateRegisterCollections :          
    //      ICoreTemplateCollections 
    //{
    //    Dictionary<string, CoreProjectTemplateAttribute> m_templates;
    //    CoreSystem m_core;
    //    internal CoreTemplateRegisterCollections(CoreSystem core)            
    //    {
    //        m_templates = new Dictionary<string, CoreProjectTemplateAttribute>();
    //        core.RegisterAssemblyLoader(InitTemplate);
    //        m_core = core;
    //    }
    //    void InitTemplate(Assembly asm)
    //    {
    //        Attribute[] v_tattr = Attribute.GetCustomAttributes(asm,
    //            typeof(CoreProjectTemplateAttribute));
    //        foreach (CoreProjectTemplateAttribute item in v_tattr)
    //        {
    //            if (!string.IsNullOrEmpty (item.Name) &&  !m_templates.ContainsKey(item.Name))
    //            {
    //                m_templates.Add(item.Name, item);
    //            }
    //            else
    //                throw new CoreException(string.Format ("template already registered or name is null : [{0}]", item.Name));
    //        }
    //    }
    //    #region ICoreTemplates Members
    //    public int Count
    //    {
    //        get { return this.m_templates.Count; }
    //    }
    //    public CoreProjectTemplateAttribute this[string name]
    //    {
    //        get { return this.m_templates[name ];}
    //    }
    //    #endregion
    //    #region IEnumerable Members
    //    public System.Collections.IEnumerator GetEnumerator()
    //    {
    //        return this.m_templates.Values.GetEnumerator();
    //    }
    //    #endregion
    //    #region ICoreTemplates Members
    //    public bool Contains(string templateName)
    //    {
    //        if (string.IsNullOrEmpty(templateName))
    //            return false;
    //        return this.m_templates.ContainsKey(templateName);
    //    }
    //    #endregion
    //}
}

