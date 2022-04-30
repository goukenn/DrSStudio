

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreApplication.cs
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
file:ICoreApplication.cs
*/
using IGK.ICore;using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    /// <summary>
    /// represent the base application
    /// </summary>
    public interface  ICoreApplication 
    {
        void Close();
        bool RegisterServerSystem(Func<CoreSystem> __initInstance);
        bool RegisterClientSystem(Func<CoreSystem> __initInstance);
        ICoreSystem GetSystem();
        event EventHandler ApplicationExit;
        #region "Properties"
        ICoreD2DPathUtils GraphicsPathUtils { get; }
        ICoreControlManager ControlManager { get; }
        ICoreResourceManager ResourcesManager { get; }
        ICoreBrushRegister BrushRegister { get; }        

        string StartupPath { get;  }
        string CurrentWorkingPath { get;  }
        string UserAppDataPath { get; }
        string AddInFolderPath { get; }
        #endregion
        /// <summary>
        /// check if the object is proxy transparent
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool IsTransparentProxy(object obj);
        ICoreScreenInfo GetScreenInfo();
        string Title { get;  }
        /// <summary>
        /// register the coresystem intance to add additional functionnality
        /// </summary>
        /// <param name="instance"></param>
        void Register(CoreSystem instance);
        /// <summary>
        /// get the private directory
        /// </summary>
        string PrivateFontsDirectory { get; }
        /// <summary>
        /// application name
        /// </summary>
        string AppName { get; }
        /// <summary>
        /// application copy right string
        /// </summary>
        string Copyright { get; }
        /// <summary>
        /// application author string name
        /// </summary>
        string AppAuthor { get; }

        ICoreSystemWorkbench CreateNewWorkbench();

        /// <summary>
        /// prefilter assembly to load. 
        /// </summary>
        /// <param name="m"></param>
        void OnPrefilterAssemblyList(List<string> m);

        void Initialize();
    }
}

