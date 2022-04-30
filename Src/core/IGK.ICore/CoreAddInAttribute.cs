

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAddInAttribute.cs
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
file:CoreAddInAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection ;
namespace IGK.ICore
{
    /// <summary>
    /// sign assembly with this attribute to insert assembly.
    /// </summary>
    [AttributeUsage (AttributeTargets.Assembly ,AllowMultiple=false)]
    public class CoreAddInAttribute :
        CoreAttribute,
        ICoreAddInAttribute
    {
        private string m_friendlyName;
        private string m_version;
        private string m_authorName;
        private string m_authorContact;
        private string m_Date;
        private string m_AddInTitle;
        private Type m_Initializer;
        private bool m_debugVersion;
        private enuCoreAddInStatus m_Status;
        public enuCoreAddInStatus Status
        {
            get { return m_Status; }
            set
            {
                if (m_Status != value)
                {
                    m_Status = value;
                }
            }
        }
        /// <summary>
        /// get or set the type used to initialize the AddIn. 
        /// This type must be marked with attribute CoreAddInInitializerAttribute(true) 
        /// and must implement ICoreInitilizer. you can inherit from CoreAddInInitializerBase
        /// </summary>
        public Type Initializer
        {
            get { return m_Initializer; }
            set
            {
                if (m_Initializer != value)
                {
                    m_Initializer = value;
                }
            }
        }
        public string AddInTitle
        {
            get { return m_AddInTitle; }
            set
            {
                if (m_AddInTitle != value)
                {
                    m_AddInTitle = value;
                }
            }
        }
        public string Date
        {
            get { return m_Date; }
            set
            {
                if (m_Date != value)
                {
                    m_Date = value;
                }
            }
        }
        public bool DebugVersion {
            get {
                return this.m_debugVersion;
            }
        }
        private string m_Description;
        private Type m_CheckingType;
        /// <summary>
        /// get or set the type used to check additional requirement for this adding. this type must have a static method 
        /// name Check that return a boolean indicating wheter an Addin is correct. and accept a boolean whether to 
        /// show reponse dialog if checking failed
        /// </summary>
        public Type CheckingType
        {
            get { return m_CheckingType; }
            set { this.m_CheckingType = value; }
        }
        /// <summary>
        /// get or set description of this addin
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
        #region ICoreAddInAttribute Members
        /// <summary>
        /// get the friendly name
        /// </summary>
        public string FriendlyName
        {
            get { return m_friendlyName; }
        }
        public string Version
        {
            get { return m_version ;  }
            set { m_version = value; }
        }
        public string AuthorName
        {
            get { return m_authorName ; }
            set { m_authorName = value; }
        }
        public string AuthorContact
        {
            get { return m_authorContact ; }
            set { m_authorContact = value; }
        }
        #endregion
        public CoreAddInAttribute() {
            this.m_authorName = CoreConstant.AUTHOR;
            this.m_version = CoreConstant.VERSION;
            this.m_authorContact = CoreConstant.CONTACT;
            this.m_Status = enuCoreAddInStatus.BetaVersion;
#if DEBUG 
            this.m_debugVersion = true;
#else
            this.m_debugVersion =false;
#endif
        }
        public CoreAddInAttribute(string Name)
            : this()
        {
            this.m_friendlyName = Name;
        }

#if !WINDOWS_UWP
        public static bool IsAddIn(System.Reflection.Assembly asm)
        {
            return Attribute.IsDefined(asm, typeof(CoreAddInAttribute));
        }
        public static CoreAddInAttribute GetAttribute(Assembly asm)
        {
            CoreAddInAttribute v_attr =
                Attribute.GetCustomAttribute(asm, typeof(CoreAddInAttribute)) as CoreAddInAttribute;
            return v_attr;
        }
#else
       public static bool IsAddIn(System.Reflection.Assembly asm){
            return asm.IsDefined(typeof(CoreAddInAttribute));            
        }
        public static CoreAddInAttribute GetAttribute(Assembly asm)
        {
            return asm.GetCustomAttribute(typeof(CoreAddInAttribute)) as CoreAddInAttribute;            
        }
#endif


    }
}

