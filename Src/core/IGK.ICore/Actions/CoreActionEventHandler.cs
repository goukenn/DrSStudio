

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreActionEventHandler.cs
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
file:CoreActionEventHandler.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Actions
{
    public delegate void ActionEventHandler(object sender, CoreActionEventArgs e);
    /// <summary>
    /// represent a core action event Handler
    /// </summary>
    public class CoreActionEventArgs : EventArgs 
    {
        private IGK.ICore.Actions.ICoreAction  m_Action;
        /// <summary>
        /// Get thet action
        /// </summary>
        public IGK.ICore.Actions.ICoreAction  Action
        {
            get { return m_Action; }
        }
        public CoreActionEventArgs(IGK.ICore.Actions.ICoreAction action)
        {
            this.m_Action = action;
        }
    }
}

