

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingMecanismAction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:Core2DDrawingMecanismAction.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.Actions
{
    using IGK.ICore;using IGK.DrSStudio.Actions ;
    /// <summary>
    /// represent the base class of mecanism action
    /// </summary>
    public abstract class Core2DDrawingMecanismAction : 
        CoreActionBase,
        ICore2DDrawingMecanismAction,
        ICoreAction 
    {
        Core2DDrawingMecanismBase m_mecanism;
        private Keys m_Keys;
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
        public Keys ShortCutDemand
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
        public Core2DDrawingMecanismBase Mecanism
        {
            get
            {
                return m_mecanism;
            }
            set {
                this.m_mecanism = value;
            }
        }
        public override string Id
        {
            get { return GetType().Name; }
        }
        #region ICoreMecanismAction Members
        ICoreWorkingMecanism ICoreMecanismAction.Mecanism
        {
            get
            {
                return this.Mecanism;
            }
            set
            {
                this.Mecanism = value as Core2DDrawingMecanismBase ;
            }
        }
        #endregion
    }
}

