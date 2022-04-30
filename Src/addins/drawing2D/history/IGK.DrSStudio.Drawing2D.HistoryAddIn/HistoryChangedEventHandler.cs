

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistoryChangedEventHandler.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.History;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
///* 
//-------------------------------------------------------------------
//Company: IGK-DEV
//Author : C.A.D. BONDJE DOUE
//SITE : http://www.igkdev.be
//Application : DrSStudio
//powered by IGK - DEV &copy; 2008-2012
//THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
//FOR MORE INFORMATION ABOUT THE LICENSE
//------------------------------------------------------------------- 
//*/

///* 
//-------------------------------------------------------------
//This file is part of iGK-DEV-DrawingStudio
//-------------------------------------------------------------
//-------------------------------------------------------------
//-------------------------------------------------------------
//view license file in Documentation folder to get more info
//Copyright (c) 2008-2010 
//Author  : Charles A.D. BONDJE DOUE 
//mail : bondje.doue@hotmail.com
//-------------------------------------------------------------
//-------------------------------------------------------------
//*/
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace IGK.DrSStudio.Drawing2D
//{
//    


//    public delegate void HistoryChangedEventHandler(object o, HistoryChangedEventArgs e);


//    public class HistoryChangedEventArgs
//    {
//        private enuHistoryState m_state;
//        private HistoryActionBase m_Previous;
//        private HistoryActionBase m_Current;
//        /// <summary>
//        /// get the previous state
//        /// </summary>
//        public enuHistoryState State { get { return this.m_state; } }
//        /// <summary>
//        /// get the previous action
//        /// </summary>
//        public HistoryActionBase Previous { get { return this.m_Previous; } }
//        /// <summary>
//        /// get the current action
//        /// </summary>
//        public HistoryActionBase Current { get { return this.m_Current; } }
//        public HistoryChangedEventArgs(enuHistoryState HistoryState, HistoryActionBase previous, HistoryActionBase current)
//        {
//            this.m_state = HistoryState;
//            this.m_Previous = previous;
//            this.m_Current = current;
//        }
//    }
//}
