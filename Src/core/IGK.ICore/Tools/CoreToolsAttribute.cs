

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreToolsAttribute.cs
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
file:CoreToolsAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Tools
{
    /// <summary>
    /// represent attribute used to register a Tool .
    /// </summary>
    public class CoreToolsAttribute : CoreAttribute 
    {
        private string m_Name;
        private string m_ToolDocumentKey;
        private string m_Category;
        private string m_Description;
        /// <summary>
        /// get or set the description of the tools
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
        /// get or set the category name
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
        /// get tool document keys
        /// </summary>
        public string ImageKey
        {
            get { return m_ToolDocumentKey; }
            set
            {
                if (m_ToolDocumentKey != value)
                {
                    m_ToolDocumentKey = value;
                }
            }
        }
        /// <summary>
        /// get or the name 
        /// </summary>
        public string Name
        {
            get { return m_Name; }
        }
        public CoreToolsAttribute(string name)
        {
            //indentifier checked
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(@"(_)*\w+([0-9a-zA-Z]\.)*");
            if (!rg.IsMatch(name))
                throw new Exception("name don't match requirement");
            if (!name.StartsWith("Tool."))
            {
                name = "Tool."+name;
            }
            this.m_Name = name;
            this.m_ToolDocumentKey = string.Format("Tool.{0}", name);
            this.m_Category = "DEFAULT";
        }
    }
}

