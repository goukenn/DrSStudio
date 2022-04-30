

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMenuActionEventArgs.cs
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
file:CoreMenuActionEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Menu
{
    public delegate void CoreMenuActionEventHandler(object o , CoreMenuActionEventArgs e);
    /// <summary>
    /// represent the menu action event args 
    /// </summary>
    public class CoreMenuActionEventArgs : EventArgs  
    {
        private CoreMenuActionBase  m_Action;
        public CoreMenuActionBase  Action
        {
            get { return m_Action; }
        }
        public CoreMenuActionEventArgs(CoreMenuActionBase action)
        {
            this.m_Action = action;
        }
    }
}

