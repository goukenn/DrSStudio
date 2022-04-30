

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreBrush.cs
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
file:ICoreBrush.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    public interface ICoreBrush : 
        ICoreDisposableObject ,
        ICloneable ,
        ICoreWorkingDefinitionObject
    {
        void ReplaceColor(Colorf oldColor, Colorf newColor);
        /// <summary>
        /// for 2D drawing object
        /// </summary>
        ICoreBrushOwner Owner { get; }
        /// <summary>
        /// get the current brush type
        /// </summary>
        enuBrushType BrushType { get; }
        enuLinearMode LinearMode { get; }
        Colorf[] Colors { get; }
        float[] Factors { get; }
        float[] Positions { get; }
        float Angle { get; }
        float Scale { get; }
        float Focus { get; }
        Vector2f PathFocusScale{get;}
        /// <summary>
        /// Get or set the path center
        /// </summary>
        Vector2f PathCenter { get; set; }
        enuHatchStyle HatchStyle { get; }
        enuWrapMode WrapMode { get; }
        Rectanglef Bounds { get; }
        enuPathBrushMode PathBrushMode { get; }
        enuLinearOperator LinearOperator { get; }
        bool GammaCorrection { get; }
        bool AutoCenter { get; }
        bool AutoSize { get; }
        bool OneColorPerVertex { get; }
        bool TextureAutoTranform { get; }
        Matrix Transform { get; }
        ICoreBitmap Bitmap { get; } // windows bitmap object
        void SetSolidColor(Colorf color);
        void Copy(ICoreBrush iCoreBrush);
        event EventHandler BrushDefinitionChanged;
        event EventHandler Disposed;

        void SetHatchBrush(Colorf frontColor,
            Colorf backColor,
            enuHatchStyle hatchStyle);
        void SetLinearBrush(Colorf startColor, Colorf endColor, float angle);
        void SetLinearBrush(
            Colorf[] colors,
            float[] factors,
            float[] positions,
            float Angle,
            enuLinearMode linMode,
            enuWrapMode wrapMode,
            bool gammaCorrection,
            enuLinearOperator linOperator,
            float focus,
            float scale,
            bool AutoSize,
            Rectanglei rectangle);
        //create a new path brush
        void SetPathBrush(
            Colorf[] colors,
            float[] factors,
            float[] positions,
            enuLinearMode linMode,
            enuWrapMode wrapMode,
            Vector2f pathFocusScale,
            enuPathBrushMode pathMode,
            enuLinearOperator linOperator,
            float focus,
            float scale,
            bool OnColorPerVertex,
            bool Autosize,
            Rectanglei rectangle,
            bool AutoCenter, //alow auto center
            Vector2f Center  //define the center Vector2i according to AutoCenter field
            );
        void SetTextureBrush(
            ICoreBitmap bitmap,
            enuWrapMode wrapMode,
            bool AutoSize,
            Rectanglei rectangle,
            bool AutoTransform);
        /// <summary>
        /// get texture brush
        /// </summary>
        /// <param name="bitmapfile"></param>
        /// <param name="wrapMode"></param>
        /// <param name="AutoSize"></param>
        /// <param name="rectangle"></param>
        /// <param name="AutoTransform"></param>
        void SetTextureBrush(
          string bitmapfile,
          enuWrapMode wrapMode,
          bool AutoSize,
          Rectanglei rectangle,
          bool AutoTransform);
        /// <summary>
        /// set brush model
        /// </summary>
        /// <param name="brush"></param>
        void SetBrush(ICoreBrush brush);
        /// <summary>
        /// set the texture resource
        /// </summary>
        /// <param name="texture"></param>
        void SetTextureResource(ICoreTextureResource texture, enuWrapMode wrapMode,
          bool AutoSize,
          Rectanglei rectangle,
          bool AutoTransform);
    }
}

