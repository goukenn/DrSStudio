

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DSurface.cs
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
file:ICore2DSurface.cs
*/
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.WinUI
{
    /// <summary>
    /// represent a base 2d surface implementation
    /// </summary>
    public interface  ICore2DSurface : ICoreWorkingSurface ,
        ICore2DDrawingObject ,
        ICoreWorkingDocumentManagerSurface,
        ICore2DScrollableWorkingSurface
    {
        /// <summary>
        /// get if surface allow multi document
        /// </summary>
        bool AllowMultipleDocument { get; }
        /// <summary>
        /// get autorization if current document allow multi layer
        /// </summary>
        bool AllowMultipleLayer { get; }
        /// <summary>
        /// create a new document
        /// </summary>
        /// <returns></returns>
        ICore2DDrawingDocument CreateNewDocument();
        /// <summary>
        /// get the document collection
        /// </summary>
        ICore2DDrawingDocumentCollections Documents { get; }
        event Core2DDrawingDocumentEventHandler DocumentAdded;
        event Core2DDrawingDocumentEventHandler DocumentRemoved;
        event CoreWorkingObjectZIndexChangedHandler DocumentZIndexChanged;
        ICoreSnippet CreateSnippet(ICoreWorkingMecanism mecanism, int Demand, int Index);
    }
}

