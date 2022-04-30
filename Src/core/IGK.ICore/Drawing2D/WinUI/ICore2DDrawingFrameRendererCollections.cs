using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.WinUI
{
    public interface ICore2DDrawingFrameRendererCollections : IEnumerable 
    {
        int Count { get; }
        void Add(ICore2DDrawingFrameRenderer frame);
        void Remove(ICore2DDrawingFrameRenderer frame);
        /// <summary>
        /// determinie if frame already contains an instance of type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Contains<T>();
    }
}
