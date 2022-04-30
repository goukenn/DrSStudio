

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidGroupAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.WinUI
{
    [AttributeUsage (AttributeTargets.Class , Inherited =false, AllowMultiple 
        =false )]
    public class AndroidGroupAttribute : CoreWorkingObjectAttribute, ICoreWorkingGroupObjectAttribute
    {
        private Type m_Mecanism;
        private enuKeys m_keys;
        private bool m_isVisibled;
        

        /// <summary>
        /// Get or set the name space of this group
        /// </summary>
        public override string NameSpace
        {
            get { return AndroidConstant.NAME_SPACE ; }
           
        }
        /// <summary>
        /// get or set the mencanism type
        /// </summary>
        public Type MecanismType
        {
            get { return m_Mecanism; }
            set
            {
                if (m_Mecanism != value)
                {
                    m_Mecanism = value;
                }
            }
        }
        public AndroidGroupAttribute(string name, Type mecanism):base(name )
        {
            this.m_Mecanism = mecanism;
        }


        public virtual string Environment
        {
            get { return AndroidConstant.ENVIRONMENT; }
        }

        public virtual string GroupImageKey
        {
            get { return string.Empty; }
        }

        public virtual string GroupName
        {
            get { return "androidBasics"; }
        }

        public bool IsVisible
        {
            get
            {
                return this.m_isVisibled;
            }
            set
            {
                this.m_isVisibled = value;
            }
        }

        public enuKeys Keys
        {
            get
            {
                return m_keys;
            }
            set
            {
                this.m_keys = value;
            }
        }
    }
}
