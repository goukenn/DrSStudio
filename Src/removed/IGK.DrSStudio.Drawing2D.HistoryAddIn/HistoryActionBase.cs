

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistoryActionBase.cs
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
file:HistoryActionBase.cs
*/
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
//    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
//    public abstract class HistoryActionBase:
//        IHistory2DUndoAndRedo ,
//        IGK.DrSStudio.History.IHistoryAction
//    {
//        private int m_Index;
//        private HistoryActionsList m_owner;
//        public ICore2DDrawingSurface CurrentSurface {
//            get {
//                return this.m_owner.CurrentSurface;
//            }
//        }
//        protected void InvalidateSurface()
//        {
//            this.CurrentSurface.Invalidate();
//        }
//        /// <summary>
//        /// get the index of this history a ction
//        /// </summary>
//        public int Index
//        {
//            get { return m_Index; }
//            internal set { m_Index = value; }
//        }
//        public virtual string Info {
//            get {
//                return "HistoryAction";
//            }
//        }
//        public override string ToString()
//        {
//            return this.Info ;
//        }
//        private HistoryActionBase m_Next;
//        /// <summary>
//        /// get the image keys
//        /// </summary>
//        public virtual string ImgKey {
//            get {
//                return string.Empty;
//            }
//        }
//        public HistoryActionsList Owner { get { return this.m_owner; } internal set { m_owner = value; } }
//        public HistoryActionBase Next
//        {
//            get { return m_Next; }
//            internal set { m_Next = value; }
//        }
//        private HistoryActionBase m_Previous;
//        public HistoryActionBase Previous
//        {
//            get { return m_Previous; }
//            internal set { m_Previous = value; }
//        }
//        #region IUndoAndRedo Members
//        public virtual void Undo() { }
//        public virtual void Redo() { }
//        #endregion
//        #region IHistoryAction Members
//        IGK.DrSStudio.History.IHistoryList IGK.DrSStudio.History.IHistoryAction.Owner
//        {
//            get
//            {
//                return this.m_owner;
//            }
//            set
//            {
//                this.m_owner = value as HistoryActionsList;
//            }
//        }
//        IGK.DrSStudio.History.IHistoryAction IGK.DrSStudio.History.IHistoryAction.Previous
//        {
//            get
//            {
//                return this.Previous;
//            }
//            set
//            {
//                this.Previous = value as HistoryActionBase;
//            }
//        }
//        IGK.DrSStudio.History.IHistoryAction IGK.DrSStudio.History.IHistoryAction.Next
//        {
//            get
//            {
//                return this.Next;
//            }
//            set
//            {
//                this.Next = value as HistoryActionBase;
//            }
//        }
//        #endregion
//    }
//}

