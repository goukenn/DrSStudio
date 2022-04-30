

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterActionBase.cs
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
file:CoreParameterAction.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IGK.ICore.WinUI.Configuration
{
    using IGK.ICore;using IGK.ICore.Actions;
using IGK.ICore.Settings;
using IGK.ICore.WinUI.Configuration;
    /// <summary>
    /// represent the base parameter action type
    /// </summary>
    public class CoreParameterActionBase :
        CoreParameterEntryBase ,
        ICoreParameterAction        
    {
        private ICoreAction m_action;
        private bool m_Reload;
        public ICoreAction Action
        { get { return this.m_action; } protected set { this.m_action = value; } }
        /// <summary>
        /// get or set if all this configuration must be reloaded . default is false;
        /// </summary>
        public bool Reload
        {
            get { return m_Reload; }
            set
            {
                if (m_Reload != value)
                {
                    m_Reload = value;
                }
            }
        }
        public CoreParameterActionBase(string name, string captionkey, IGK.ICore.Actions.ICoreAction action):base(name , captionkey )
        {
            if (action == null)
                throw new ArgumentNullException("action");
            this.m_Reload = false;
            this.m_action = action;            
        }
        protected CoreParameterActionBase()
        { 
        }
        public override string ToString()
        {
            return "CoreParameterActions : " + this.Name;
        }
    }
}

