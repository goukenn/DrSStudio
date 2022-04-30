

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreActionAttribute.cs
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
file:CoreActionAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Actions
{
    [AttributeUsage (AttributeTargets.Class )]
    /// <summary>
    /// represent the core action(command) default attribute
    /// </summary>
    public class CoreActionAttribute : CoreAttribute 
    {
        private string m_Name;
        private string m_Category;
        private string m_ImageKey;
        /// <summary>
        /// get or set the image key of the actions
        /// </summary>
        public virtual string ImageKey
        {
            get { return m_ImageKey; }
            set
            {
                if ((value != null) && (value.EndsWith("gkds"))) {
                }


                if (m_ImageKey != value)
                {
                    m_ImageKey = value;
                }
            }
        }
        /// <summary>
        /// get or set the category list. Semi column separated
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
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private string m_Description;
        /// <summary>
        /// get the descrition ove the menu
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
        public CoreActionAttribute(string name)
        {
            this.m_Name = name;
            this.m_Category = CoreConstant.DEFAULT_ACTION_CATEGORY;
        }
        public string[] GetCategories()
        {
            if (string.IsNullOrEmpty(this.m_Category))
            {
        return null;
            }
            return this.m_Category.Split(';');
        }
    }
}

