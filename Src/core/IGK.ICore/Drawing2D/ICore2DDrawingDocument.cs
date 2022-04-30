

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingDocument.cs
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
file:ICore2DDrawingDocument.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    using IGK.ICore;
    using IGK.ICore.Codec;
    
    
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.Resources;
    using IGK.ICore.WinUI.Configuration;
    /// <summary>
    /// represent an elemnt conteinat
    /// </summary>
      public interface ICore2DDrawingDocument :
        ICoreWorkingDocument ,
        ICoreLayeredDocument,
        ICore2DDrawingObject ,
        ICoreWorkingPositionableObject,
        ICloneable,
        ICoreDisposableObject,
        ICoreWorkingObjectPropertyEvent ,
        ICoreWorkingConfigurableObject ,
        ICoreWorkingObjectExtensionContainer,
        ICoreWorkingObjectIdElementContainer,
        ICoreSerializerLoadingService ,
        ICore2DDrawingGraphicsProperty,
        ICore2DDrawingTextGraphicsProperty,
        ICore2DDrawingClippedObject ,
        ICore2DDrawingDimensionDocument,
        ICoreResourceItem,
        ICoreWorkingStringExpression,
        ICoreWorkingElementContainer ,
        ICore2DDrawingTransform
    {
          ICoreBrush FillBrush { get; }
          /// <summary>
          /// get if this document allow multi layer
          /// </summary>
          bool AllowMultiLayer { get; }
        
        [CoreXMLAttribute()]  
        Colorf BackgroundColor { get; set; }
        
        [CoreXMLAttribute ()]
        [CoreXMLDefaultAttributeValue(false)]
        /// <summary>
        /// boolground transparent
        /// </summary>
        bool BackgroundTransparent { get; set; }       
        /// <summary>
        /// get or set the current layer
        /// </summary>
        new ICore2DDrawingLayer CurrentLayer { get; set; }
        /// <summary>
        /// event raised when current layer changed
        /// </summary>
        event Core2DDrawingLayerChangedEventHandler CurrentLayerChanged;
        /// <summary>
        /// get the layers collections
        /// </summary>
        new ICore2DDrawingLayerCollections Layers { get; }
        ///// <summary>
        ///// get the clipped region
        ///// </summary>
        ///// <returns></returns>
        //System.Drawing.Region GetClippedRegion();
      /// <summary>
      /// get the rectangle i element 
      /// </summary>
        Rectanglei Bounds { get; }
        enuRenderingMode RenderingMode { get; set; }
        event Core2DDrawingLayerEventHandler LayerAdded;
        event Core2DDrawingLayerEventHandler LayerRemoved;
        event CoreWorkingObjectZIndexChangedHandler LayerZIndexChanged;
        
        [ CoreXMLAttribute()]  
        enuPixelOffset PixelOffset { get; set; }       
        //void SetGraphicsProperty(System.Drawing.Graphics g);
          /// <summary>
          /// add new layer to document
          /// </summary>
          /// <returns></returns>
        ICore2DDrawingLayer AddNewLayer();
        /// <summary>
        /// set element to clip
        /// </summary>
        /// <param name="element"></param>
        void SetClip(ICore2DDrawingLayeredElement element);        
          /// <summary>
          /// remove all extra layer and clear the current layer
          /// </summary>
        void Clear();
        /// <summary>
        /// change id of the layer
        /// </summary>
        /// <param name="layer">layer to change</param>
        /// <param name="id">new id of the layer</param>
        /// <returns></returns>
        bool ChangeIdOf(ICoreWorkingObject element, string newId);
        //translate the current document
        void Translate(float dx, float dy);
        void SetSize(float  width, float height);
        enuInterpolationMode InterpolationMode { get; set; }
        bool ClipView { get; set; }
        ICoreGraphicsPath  GetClippedRegion();
    }
}

