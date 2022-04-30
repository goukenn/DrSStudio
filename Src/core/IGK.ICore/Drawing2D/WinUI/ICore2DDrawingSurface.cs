

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingSurface.cs
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
file:ICore2DDrawingSurface.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.ICore.WinUI;    
    using IGK.ICore.Codec ;
    /// <summary>
    /// represent base 2DD drawing surface
    /// </summary>
    public interface ICore2DDrawingSurface: 
        ICoreWorkingSurface ,
        ICoreWorkingDocumentManagerSurface,
        ICoreBrushOwner ,
        ICoreWorkingRenderingSurface ,
        ICoreWorkingSelectionSurface,
        ICoreWorkingConfigElementSurface ,
        ICoreWorkingBrushSupportSurface,
        ICoreWorkingDualBrushSelectorSurface,
        ICore2DScrollableWorkingSurface ,
        ICoreWorkingDependentAttribute
    {

        bool CanAddDocument { get; }

        event EventHandler<CoreItemEventArgs<ICore2DDrawingDocument>> DocumentAdded;
        
        event EventHandler<CoreItemEventArgs<ICore2DDrawingDocument>> DocumentRemoved;

        



        /// <summary>
        /// add new document to surface
        /// </summary>
        /// <returns></returns>
        bool AddNewDocument();
        /// <summary>
        /// get the document collections
        /// </summary>
        ICore2DDrawingDocumentCollections Documents { get; }
        /// <summary>
        /// get or set the font
        /// </summary>
        //ICoreFont Font { get; set; }
        /// <summary>
        /// get caret presentation
        /// </summary>
        /// <returns></returns>
        ICoreCaret CreateCaret();
        /// <summary>
        /// get or set the new current document
        /// </summary>
        new ICore2DDrawingDocument CurrentDocument { get; set; }
        /// <summary>
        /// get or set the current on the current document surface
        /// </summary>
        ICore2DDrawingLayer CurrentLayer { get; set; }
 
        void RefreshScene(ICore2DDrawingLayeredElement obj);
        /// <summary>
        /// create a snippet on surface
        /// </summary>
        /// <param name="mecanism">mecanism that will own the surface</param>
        /// <param name="demand">demand of this snippet</param>
        /// <param name="index">index of the snippet</param>
        /// <returns></returns>
        ICoreSnippet CreateSnippet(ICoreWorkingMecanism  mecanism, int demand, int index,  CoreSnippetRenderProc proc=null);
      
        /// <summary>
        /// create an empty document for this surface. the document created will not be added.
        /// </summary>
        /// <returns></returns>
        ICore2DDrawingDocument CreateNewDocument();
    }
}

