

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistoryChangedEventArgs.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

  
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;

using System.Text;

namespace IGK.ICore.History
{
    public delegate void  HistoryChangedEventHandler(object o, HistoryChangedEventArgs e);
    
    public class HistoryChangedEventArgs
    {
        private enuHistoryState m_state;
        private IHistoryAction m_Previous;
        private IHistoryAction m_Current;
        /// <summary>
        /// get the previous state
        /// </summary>
        public enuHistoryState State { get { return this.m_state; } }
        /// <summary>
        /// get the previous action
        /// </summary>
        public IHistoryAction Previous { get { return this.m_Previous; } }
        /// <summary>
        /// get the current action
        /// </summary>
        public IHistoryAction Current { get { return this.m_Current; } }
        public HistoryChangedEventArgs(enuHistoryState HistoryState , IHistoryAction previous, IHistoryAction current)
        {
            this.m_state = HistoryState;
            this.m_Previous = previous;
            this.m_Current = current;
        }
    }
}
