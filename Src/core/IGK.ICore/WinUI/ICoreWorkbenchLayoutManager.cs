

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkbenchLayoutManager.cs
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
file:ICoreWorkbenchLayoutManager.cs
*/
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent the ICoreWorkbenchLayoutManager interface used for a valid layout manager
    /// </summary>
    public interface  ICoreWorkbenchLayoutManager : IDisposable 
    {
        /// <summary>
        /// get the workbench
        /// </summary>
        ICoreWorkbench Workbench { get; }
        /// <summary>
        /// get the status control
        /// </summary>
        IXCoreStatus StatusControl { get; }
        /// <summary>
        /// get the string environment name. 
        /// </summary>
        string Environment { get; set; }
        /// <summary>
        /// event raise when environment changed
        /// </summary>
        event EventHandler EnvironmentChanged;
        /// <summary>
        /// this function is call to init main form layout
        /// </summary>
        void InitLayout();
        /// <summary>
        /// show tool dialog
        /// </summary>
        /// <param name="tool"></param>
        void ShowTool(ICoreTool tool);
        /// <summary>
        /// hide tool dialog
        /// </summary>
        /// <param name="tool"></param>
        void HideTool(ICoreTool tool);
        /// <summary>
        /// event raised when tool is added to layout
        /// </summary>
        event EventHandler<CoreItemEventArgs<CoreToolBase>> ToolAdded;
        /// <summary>
        /// event raised when tool is removed from layout
        /// </summary>
        event EventHandler<CoreItemEventArgs<CoreToolBase>> ToolRemoved;
        /// <summary>
        /// refresh the bench surface
        /// </summary>
        void Refresh();
        /// <summary>
        /// register a control to context menu
        /// </summary>
        /// <param name="ctr"></param>
        void RegisterToContextMenu(ICoreControl ctr);
        /// <summary>
        /// create a control by specifying the interface
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        ICoreControl CreateControl(Type interfaceType);

        /// <summary>
        /// create a menu
        /// </summary>
        /// <returns></returns>
        ICoreMenu CreateMenu();
        /// <summary>
        /// create a context menu
        /// </summary>
        /// <returns></returns>
        ICoreContextMenu CreateContextMenu();
        /// <summary>
        /// create a menu item container
        /// </summary>
        /// <returns></returns>
        IXCoreMenuItemContainer CreateMenuItem();
        /// <summary>
        /// create a status control
        /// </summary>
        /// <returns></returns>
        IXCoreStatus CreateStatusContainer();
        /// <summary>
        /// create a context menu item
        /// </summary>
        /// <returns></returns>
        IXCoreContextMenuItemContainer CreateContextMenuItem();

        /// <summary>
        /// init context menu
        /// </summary>
        /// <param name="contex"></param>
        /// <param name="menus"></param>
        void InitContextMenu(ICoreContextMenu contex, params ICoreContextMenuAction[] menus);
    }
}

