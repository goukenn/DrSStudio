

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IWPFLayeredElement.cs
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
file:IWPFLayeredElement.cs
*/
using System;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    /// <summary>
    /// represent a layered element
    /// </summary>
    public interface IWPFLayeredElement : 
        IWPFElement ,
        IDisposable ,
        ICoreWorkingPositionableObject 
    {
        System.Windows.DependencyObject  Shape { get; }
        /// <summary>
        /// get the parent layer of this element
        /// </summary>
        WPFLayer ParentLayer { get; }
    }
}

