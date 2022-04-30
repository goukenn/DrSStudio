

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingLayer.cs
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
file:ICore2DDrawingLayer.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    using IGK.ICore;
    using IGK.ICore.Codec;    
    using IGK.ICore.WinUI.Configuration;


    public interface ICore2DDrawingLayer : 
        ICoreWorkingObject,
        ICore2DDrawingObject,
        ICoreLayer ,        
        ICoreSerializerService ,
        ICoreWorkingPositionableObject,
        ICoreWorkingConfigurableObject ,
        ICoreWorkingObjectPropertyEvent ,
        ICoreWorkingObjectIdElementContainer,
        ICloneable ,
        ICore2DDrawingClippedObject ,
        ICore2DDrawingSelectionContainer,
        ICoreWorkingElementContainer
    {
        enuCoreLayerOperation LayerOption { get; set; }
        event EventHandler<CoreItemEventArgs<ICore2DDrawingLayeredElement>> ElementAdded;
        event EventHandler<CoreItemEventArgs<ICore2DDrawingLayeredElement>> ElementRemoved;
        event EventHandler SelectedElementChanged;
        /// <summary>
        /// set element to select in this layer. note item must contained in this layer or his parent
        /// </summary>
        /// <param name="items"></param>
        void Select(params ICore2DDrawingLayeredElement[] items);
        bool Select(Vector2f point, bool deepSearch);
        /// <summary>
        /// get or set if this layer if visible
        /// </summary>
        bool View { get; set; }
        [IGK.ICore.Codec.CoreXMLElement()]
        /// <summary>
        /// get the elements in the current selection
        /// </summary>
        new ICore2DDrawingLayeredElementCollections Elements { get; }
        /// <summary>
        /// collection of selected element
        /// </summary>
        ICore2DDrawingSelectedElementCollections SelectedElements { get; }
        
        new ICore2DDrawingObject GetElementById(string p);
        void Clear();
        /// <summary>
        /// get the parent document 
        /// </summary>
        new ICore2DDrawingDocument Parent { get; set; }
        /// <summary>
        /// element raised when element zindex changed
        /// </summary>
        event CoreWorkingObjectZIndexChangedHandler ElementZIndexChanged;
        /// <summary>
        /// event raised when clipped element changed
        /// </summary>
        event EventHandler ClippedChanged;
        bool ChangeIdOf(ICore2DDrawingLayeredElement iCore2DDrawingLayeredElement, string p);

        float Opacity { get; set; }
        /// <summary>
        /// translate all the contain of the current layer
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        void Translate(float dx, float dy);
        /// <summary>
        /// Get the matrix
        /// </summary>
        Matrix Matrix { get; }

        void SetClip(ICore2DDrawingLayeredElement v_element);

        ICore2DDrawingLayeredElement ClippedElement { get; set; }
    }
}

