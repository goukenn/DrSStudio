

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreLayoutManagerBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;using IGK.ICore.ContextMenu;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreLayoutManagerBase.cs
*/
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    [Serializable()]
    /// <summary>
    /// represent the layout manager
    /// </summary>
    public abstract class CoreLayoutManagerBase : ICoreWorkbenchLayoutManager
    {
        private ICoreApplicationWorkbench m_wbench;
        private IXCoreStatus  m_StatusControl;
        public IXCoreStatus  StatusControl
        {
            get {
                if (m_StatusControl == null)
                {
                    m_StatusControl = CreateStatusContainer();
                }
                return m_StatusControl; 
            }
        }
        private string m_Environment;
        public string Environment
        {
            get { return m_Environment; }
            set
            {
                if (m_Environment != value)
                {
                    m_Environment = value;
                    OnEnvironmentChanged(EventArgs.Empty);
                }
            }
        }
        private void OnEnvironmentChanged(EventArgs eventArgs)
        {
            if (this.EnvironmentChanged != null)
                this.EnvironmentChanged(this, eventArgs);
        }
        /// <summary>
        /// .create the layout manager
        /// </summary>
        /// <param name="m_wbench"></param>
        protected CoreLayoutManagerBase(ICoreApplicationWorkbench m_wbench)
        {
            this.m_wbench = m_wbench;
        }

        protected virtual void GenerateMenu() { 
        }
        protected virtual void GenerateContextMenu() { 
        }
        /// <summary>
        /// create a menu
        /// </summary>
        /// <returns></returns>
        public abstract ICoreMenu CreateMenu();
        /// <summary>
        /// create a context menu
        /// </summary>
        /// <returns></returns>
        public  abstract ICoreContextMenu CreateContextMenu();

        public void InitContextMenu(ICoreContextMenu contex, params ICoreContextMenuAction[] menus)
        {
            if ((menus == null) || (menus.Length == 0))
                return;
            CoreContextMenuBase v_menu = null;
            //init context menu
            foreach (ICoreContextMenuAction a in menus)
            {
                v_menu = a as CoreContextMenuBase;
                if (v_menu != null)
                {
                    v_menu.Workbench = this.Workbench;
                    if (v_menu.IsSepearatorBefore && (v_menu.SeparatorBefore != null))
                        contex.Add(v_menu.SeparatorBefore as IXCoreMenuItemSeparator);
                    contex.Add(v_menu);//.MenuItem);
                    if (v_menu.IsSepearatorAfter && (v_menu.SeparatorAfter != null))
                        contex.Add(v_menu.SeparatorAfter as IXCoreMenuItemSeparator);
                }
            }
        }
       
        /// <summary>
        /// get the workbench
        /// </summary>
        public ICoreApplicationWorkbench Workbench
        {
            get { return this.m_wbench; }
        }
        /// <summary>
        /// get the main form
        /// </summary>
        public ICoreMainForm MainForm
        {
            get {
                if (this.m_wbench != null)
                    return this.m_wbench.MainForm;
                return null;
            }
        }
        /// <summary>
        /// init the layout manager
        /// </summary>
        public abstract void InitLayout();
        public abstract void ShowTool(ICoreTool tool);
        public abstract void HideTool(ICoreTool tool);

        public event EventHandler<CoreItemEventArgs<CoreToolBase >> ToolAdded;
        public event EventHandler<CoreItemEventArgs<CoreToolBase>> ToolRemoved;
        public event EventHandler EnvironmentChanged;

        protected virtual  void OnToolAdded(CoreItemEventArgs<CoreToolBase> e)
        {
            if (ToolAdded != null)
                ToolAdded(this, e);
        }
        protected virtual void OnToolRemoved(CoreItemEventArgs<CoreToolBase> e)
        {
            if (ToolRemoved != null)
                ToolRemoved(this, e);
        }
        public void Refresh()
        {
            MainForm.Refresh();
        }
        public abstract ICoreControl CreateControl(Type interfaceType);
        
        /// <summary>
        /// release resources own by this layout
        /// </summary>
        public virtual  void Dispose()
        {
        }
        /// <summary>
        /// create a surface container
        /// </summary>
        /// <returns></returns>
        protected abstract IXCoreSurfaceContainer CreateSurfaceContainer();

        public abstract IXCoreMenuItemContainer CreateMenuItem();        
        public abstract IXCoreContextMenuItemContainer CreateContextMenuItem();
        public abstract IXCoreStatus CreateStatusContainer();


        public abstract void RegisterToContextMenu(ICoreControl ctr);

        ICoreWorkbench ICoreWorkbenchLayoutManager.Workbench
        {
            get { return this.m_wbench; }
        }
    }
}

