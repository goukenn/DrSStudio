

/*
IGKDEV @ 2008-2016
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
using IGK.ICore;using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:HistoryActionBase.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.History
{
    /// <summary>
    /// represent the base history actions
    /// </summary>
    public abstract class HistoryActionBase : IHistoryAction , ICoreIdentifier  
    {
        IHistoryList m_OwnerList;
        #region IHistoryAction Members

        /// <summary>
        /// get or set the history owner list
        /// </summary>
        public IHistoryList Owner
        {
            get
            {
                return m_OwnerList;
            }
            set
            {
                if (this.m_OwnerList != value)
                {
                    this.m_OwnerList = value;
                }
            }
        }
        protected void InvalidateSurface()
        {
            ICoreWorkingRenderingSurface v_s = null;
            if (this.Owner != null)
            {
                v_s = this.Owner.CurrentSurface as ICoreWorkingRenderingSurface;
                if (v_s != null)
                    v_s.RefreshScene();
            }
        }

        /// <summary>
        /// get the index of this history on the owner list
        /// </summary>
        public int Index
        {
            get { 
                if (this.m_OwnerList == null)
                    return -1;
                int h =  this.m_OwnerList.IndexOf(this);
                return h;
            }
        }
        private IHistoryAction m_Previous;
        private IHistoryAction m_Next;
        public IHistoryAction Next
        {
            get { return m_Next; }
            set
            {
                if (m_Next != value)
                {
                    m_Next = value;
                }
            }
        }
        public IHistoryAction Previous
        {
            get { return m_Previous; }
            set
            {
                if (m_Previous != value)
                {
                    m_Previous = value;
                }
            }
        }
        #endregion
        #region ICoreUndoAndRedoAction Members
        public abstract  void Undo();
        public abstract  void Redo();
        #endregion
        public abstract string Info { get; }
        public abstract string ImgKey { get; }

        string ICoreIdentifier.Id
        {
            get { return string.Empty; }
        }
    }
}

