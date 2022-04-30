

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMecanismActionBase.cs
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
file:CoreMecanismActionBase.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.MecanismActions
{
    using IGK.ICore;
    using IGK.ICore.Actions;
    using IGK.ICore.Mecanism;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent the base class of mecanism action
    /// </summary>
    public abstract class CoreMecanismActionBase : 
        CoreActionBase,
        ICoreAction ,
        ICoreMecanismAction 
    {
        CoreMecanismBase m_mecanism;
        private enuKeys m_Keys;
        private object m_Param;
        public object Param
        {
            get { return m_Param; }
            set
            {
                if (m_Param != value)
                {
                    m_Param = value;
                }
            }
        }
        public enuKeys ShortCutDemand
        {
            get { return m_Keys; }
            set
            {
                if (m_Keys != value)
                {
                    m_Keys = value;
                }
            }
        }
        public CoreMecanismBase Mecanism
        {
            get
            {
                return m_mecanism;
            }
            set {
                this.m_mecanism = value;
            }
        }
        ICoreWorkingMecanismAction ICoreMecanismAction.Mecanism
        {
            get
            {
                return this.Mecanism as ICoreWorkingMecanism;
            }
            set
            {
                this.Mecanism = value as CoreMecanismBase;
            }
        }
    }
}

