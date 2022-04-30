

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingMecanism.cs
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
file:ICoreWorkingMecanism.cs
*/
using IGK.ICore;using IGK.ICore.Actions;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    public interface ICoreWorkingMecanism :         
        ICoreWorkingSurfaceHost,
        ICoreWorkingMecanismAction, 
        IDisposable
    {
        /// <summary>
        /// get the state
        /// </summary>
        int State { get; }
        /// <summary>
        /// get a bool that indicate the mecanism allow Context
        /// </summary>
        bool AllowContextMenu { get; }
        /// <summary>
        /// state changed
        /// </summary>
        /// 
        event EventHandler StateChanged;
        ICoreSnippet Snippet { get; set; }
        event EventHandler SnippetChanged;
        /// <summary>
        /// get registrated snippet
        /// </summary>
        ICoreSnippetCollections RegSnippets { get; }
        /// <summary>
        /// get if this mecanism is on design mode
        /// </summary>
        bool DesignMode { get; }
        bool Register(ICoreWorkingSurface surface);
        bool UnRegister();
        bool IsFreezed { get; }
        /// <summary>
        /// used to freeze mecanism
        /// </summary>
        void Freeze();
        /// <summary>
        /// used to unfreeze mecanims
        /// </summary>
        void UnFreeze();
        /// <summary>
        /// edit a working object in this mecanism
        /// </summary>
        /// <param name="e"></param>
        void Edit(ICoreWorkingObject  e);
        /// <summary>
        /// get if this mecanism can process the message
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        bool CanProcessActionMessage(ICoreMessage m);
    }
}

