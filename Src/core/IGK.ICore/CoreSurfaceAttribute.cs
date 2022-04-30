

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSurfaceAttribute.cs
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
file:CoreSurfaceAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// represent a managed surface
    /// </summary>
    [AttributeUsage (AttributeTargets.Class  , AllowMultiple=false, Inherited =false )]
    public class CoreSurfaceAttribute : CoreWorkingObjectAttribute
    {
        private string m_DisplayName;
        private string m_EnvironmentName;
        private string m_GUID;
        private string m_Description;

        public string Description
        {
            get { return m_Description; }
            set
            {
                if (m_Description != value)
                {
                    m_Description = value;
                    OnDescriptionChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler DescriptionChanged;

        protected virtual void OnDescriptionChanged(EventArgs e)
        {
            if (DescriptionChanged != null)
            {
                DescriptionChanged(this, e);
            }
        }


        public string GUID
        {
            get { return m_GUID; }
            set
            {
                if (m_GUID != value)
                {
                    m_GUID = value;
                }
            }
        }
        /// <summary>
        /// get or set the environment name
        /// </summary>
        public string EnvironmentName
        {
            get { return m_EnvironmentName; }
            set
            {
                if (m_EnvironmentName != value)
                {
                    m_EnvironmentName = value;
                }
            }
        }
        /// <summary>
        /// Get or set the default display name
        /// </summary>
        public string DisplayName
        {
            get { return m_DisplayName; }
            set
            {
                if (m_DisplayName != value)
                {
                    m_DisplayName = value;
                }
            }
        }
        public CoreSurfaceAttribute(string name):base(name)
        {
            this.m_DisplayName = name;
            this.m_EnvironmentName = name;
        }
        public CoreSurfaceAttribute (string name, string guid):base(name )
        {
            this.m_DisplayName = name;
            this.m_EnvironmentName = name;
            this.m_GUID = Guid.Parse(guid).ToString();
        }
    }
}

