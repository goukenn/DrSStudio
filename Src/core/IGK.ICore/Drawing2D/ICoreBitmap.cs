

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreBitmap.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;using IGK.ICore.Imaging;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ICoreBitmap.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent the 
    /// </summary>
    public interface ICoreBitmap : ICoreWorkingObject , IDisposable , ICloneable 
    {
        /// <summary>
        /// get the width
        /// </summary>
        int Width { get; }
        /// <summary>
        /// get the height
        /// </summary>
        int Height { get; }
        /// <summary>
        /// get the size of this image
        /// </summary>
        Size2i Size { get; }
        /// <summary>
        /// get the number of frame store in this ICoreBitmap
        /// </summary>
        int FrameCount { get; }
        /// <summary>
        /// get the pixel information
        /// </summary>
        enuPixelFormat PixelFormat { get;  }
        bool Save(string filename, object ImageCodecInfo, object EncoderParameters);
        /// <summary>
        /// save to file with core format
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        bool Save(string filename, CoreBitmapFormat format);
        /// <summary>
        /// invert color
        /// </summary>
        void InvertColor();
        /// <summary>
        /// rotate color
        /// </summary>
        /// <param name="rotationFlag"></param>
        void Rotate(int rotationFlag);
        /// <summary>
        /// if have multiple frame get frame at index i
        /// </summary>
        /// <param name="i">the index to get</param>
        /// <returns></returns>
        ICoreBitmap GetFrame(int i);
        void AddFrame(ICoreBitmap frame);
        /// <summary>
        /// set the bitmap resolution
        /// </summary>
        /// <param name="dpix"></param>
        /// <param name="dpiy"></param>
        void SetResolution(float dpix, float dpiy);
        /// <summary>
        /// to string with withSizeInfo or not
        /// </summary>
        /// <param name="withSizeInfo"></param>
        /// <returns></returns>
        string ToStringData(bool withSizeInfo=true);

        /// <summary>
        /// save to memory stream
        /// </summary>
        /// <param name="mem"></param>
        /// <param name="coreBitmapFormat"></param>
        void Save(System.IO.Stream mem, CoreBitmapFormat coreBitmapFormat);
        /// <summary>
        /// get bitmap data of this ICoreBitmap info table
        /// </summary>
        /// <returns></returns>
        byte[] ToData();
    }
}

