using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    public class CoreBrushRegister : ICoreBrushRegister
    {
        public virtual void Unregister(ICoreBrush coreBrush)
        {
            
        }

        public virtual void Register(ICoreBrush coreBrush)
        {
            
        }

        public virtual T GetPen<T>(Colorf color) where T : class, IDisposable
        {
            return null;
        }

        public virtual  T GetBrush<T>(Colorf color) where T : class, IDisposable
        {
            return null;
        }

        public virtual  T GetBrush<T>(enuHatchStyle style, Colorf cl1, Colorf cl2) where T : class, IDisposable
        {
            return null;
        }

        public T GetPen<T>(ICorePen corePen) where T : class, IDisposable
        {
            return null;
        }

        public T GetBrush<T>(ICoreBrush brush) where T : class, IDisposable
        {
            return null;
        }

        public T GetBrush<T>(ICoreBitmap bitmap) where T : class, IDisposable
        {
            return null;
        }

        public T GetBrush<T>(ICore2DDrawingDocument document) where T : class, IDisposable
        {
            return null;
        }
        /// <summary>
        /// override this to reload brush
        /// </summary>
        /// <param name="coreBrush"></param>
        public virtual void Reload(ICoreBrush coreBrush)
        {
            
        }
        public void Dispose()
        {
        }
    }
}
