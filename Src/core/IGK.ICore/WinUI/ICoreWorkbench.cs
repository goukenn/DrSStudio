

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkbench.cs
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
file:ICoreWorkbench.cs
*/
using IGK.ICore;using IGK.ICore.Tools;
using IGK.ICore.WinUI.Common;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// manage data infrastratucture
    /// </summary>
    public interface ICoreWorkbench : 
        ICoreSystemWorkbench, 
        ICoreWorkbenchWorkingObjectConfigurator,
        ICoreWorkingSurfaceHandler,
        ICoreWorkbenchEnvironmentHandler,
        ICoreWorbenchToolListener,
        ICoreWorkbenchDialogFactory,
        ICoreWorkbenchMessageFilter,
        ICoreWorkbenchShowSetting,
        ICoreWorkbenchOpener,
        ICoreWorkbenchAboutHost,
        ICoreWorbenchFileCreator,
        ICoreWorbenchProjectCreator
    {                
        
       
        ICoreActionRegisterTool ActionRegister { get; } 
      
        /// <summary>
        /// build working object property action
        /// </summary>
        /// <param name="targetControl"></param>
        /// <param name="objToConfigure"></param>
        /// <param name="showOKButtong"></param>
        /// <param name="AllowCancel"></param>
        void BuildWorkingProperty(ICoreControl targetControl, ICoreWorkingConfigurableObject objToConfigure);       
        /// <summary>
        /// show control dialog in workbench
        /// </summary>
        /// <param name="control"></param>
        void Show(string title, ICoreControl control);
        /// <summary>
        /// open multiple files
        /// </summary>
        /// <param name="f"></param>
        void OpenFile(params string[] f);
        /// <summary>
        /// find surface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T FindSurface<T>();
    }
}

