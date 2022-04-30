

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistorySurfaceManager.cs
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
file:HistorySurfaceManager.cs
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
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.ICore;using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.History;
    [CoreTools("Tool.HistorySurfaceManager", ImageKey = "Tool_2DHistory")]
    public class HistorySurfaceManager : 
        Core2DDrawingToolBase ,                
        IHistorySurfaceManager
    {
        HistoryActionsList m_currentActionList;
        /// <summary>
        /// Get the current action list
        /// </summary>
        public HistoryActionsList ActionsList {
            get { return this.m_currentActionList; }
        }
        public bool CanAdd {
            get {
                if (m_currentActionList==null)return false ;
                return m_currentActionList.CanAdd;
            }
        }
        public new UIXHistoryControlHost HostedControl
        {
            get {
                return base.HostedControl as UIXHistoryControlHost;
            }
            set {
                base.HostedControl = value;
            }
        }
        protected override void GenerateHostedControl()
        {
            //this.HostedControl = new IGK.DrSStudio.Drawing2D.WinUI.UIXHistoryControl(this);
            this.HostedControl = new UIXHistoryControlHost(this);
            this.HostedControl.CaptionKey = "Tool.HistorySurfaceManager";
        }
        private Dictionary<ICore2DDrawingSurface, HistoryActionsList> m_HistoryActionsList;
        private static HistorySurfaceManager sm_instance;
        static HistorySurfaceManager(){            
               sm_instance = new HistorySurfaceManager();
        }
        private HistorySurfaceManager()
        {                      
            m_HistoryActionsList = new Dictionary<ICore2DDrawingSurface, HistoryActionsList>();
            InitManager();
        }
        public static HistorySurfaceManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        #region IUndoAndRedo Members
        public void Undo()
        {
            HistoryActionsList d = m_HistoryActionsList[this.CurrentSurface];
            d.Undo();
        }
        public void Redo()
        {
            HistoryActionsList d = m_HistoryActionsList[this.CurrentSurface];
            d.Redo();
        }
        #endregion
        bool Contains(ICore2DDrawingSurface surface)
        {
            if (surface == null)
                return false;
            return this.m_HistoryActionsList .ContainsKey (surface );
        }
        void InitManager()
        {
            new ElementAddedManager(this);
            new ImageManager(this);
            new LayerManager(this);
            new DocumentManager(this);
            HistoryActionSetting.Instance.Bind(this);
        }
        internal void ClearHistory()
        {
            HistoryActionsList d = m_HistoryActionsList[this.CurrentSurface];
            d.Clear();
        }
        internal void SaveHistory()
        {
            HistoryActionsList d = m_HistoryActionsList[this.CurrentSurface];
            d.SaveHistory();
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            RegisterSurfaceHistory(surface);
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.UnRegisterSurfaceEvent(surface);
        }
        private void RegisterSurfaceHistory(ICore2DDrawingSurface surface)
        {
            if (surface ==null)return ;
            if (m_HistoryActionsList.ContainsKey(surface))
            {
                this.m_currentActionList = this.m_HistoryActionsList[surface];
                Edit(this.m_currentActionList);
                return;
            }
            try
            {
                HistoryActionsList action = new HistoryActionsList(
                    surface,
                    this,
                   IGK.DrSStudio.IO.PathUtils.GetPath(
                   IGK.DrSStudio.Settings .CoreApplicationSetting .Instance.TempFolder+"/"+ Guid.NewGuid ())
                    );
                this.m_HistoryActionsList.Add(surface, action);
                this.m_currentActionList = action;
                Edit(action);
            }
            catch (Exception ex) {
                CoreServices.ShowError(ex.Message);
            }
        }
        /// <summary>
        /// edit this current history action
        /// </summary>
        /// <param name="hList"></param>
        private void Edit(HistoryActionsList hList)
        {
            this.HostedControl.Edit(hList);
        }
        internal void Add(HistoryActionBase historyAction)
        {
            this.m_currentActionList.Add(historyAction);
        }
        #region IHistorySurfaceManager Members
        public void Remove(IHistoryActionList actionList)
        {
            if (this.HostedControl.HistoryList == actionList)
                this.HostedControl.Edit(null);
            this.m_HistoryActionsList.Remove(actionList.CurrentSurface );
        }
        #endregion
    }
}

