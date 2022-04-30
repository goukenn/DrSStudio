

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IWPFLayer.cs
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
file:IWPFLayer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Core.Layers;
    public interface IWPFLayer  : 
        IWPFElement , 
        ICoreLayer ,
        ICoreWorkingPositionableObject 
    {
        bool Visible { get; set; }
        event EventHandler VisibleChanged;
        System.Windows.Controls.Canvas Canvas { get; }
        new IWPFElementCollections Elements{get;}
        new IWPFSelectedElementCollections SelectedElements{get;}
        event WPFElementEventHandler<IWPFLayeredElement> ElementAdded;
        event WPFElementEventHandler<IWPFLayeredElement> ElementRemoved;
        event EventHandler SelectedElementChanged;
    }
}

