using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.ICore.WorkingObjects
{
    /// <summary>
    /// represent a utility class used internally to store object
    /// </summary>
    class GKDSElementSetting
    {
        internal void SetupDocument(ICoreWorkingDocument doc, ICoreWorkingObject obj)
        {
            ICoreDocumentSetup setup = doc as
                ICoreDocumentSetup;
            if (setup != null)
                setup.SetupDocument(obj);
            else
                MethodInfo.GetCurrentMethod ().Visit(this, doc, obj);
        }

        public void SetupDocument(ICore2DDrawingDocument document, ICore2DDrawingLayeredElement element)
        {
            var rc = element.GetBound ();
            element.SuspendLayout();
            if (element.CanTranslate)
            {
                element.Translate(-rc.X, -rc.Y, enuMatrixOrder.Append);
                element.ResetTransform();
            }
            element.ResumeLayout();

            document.SetSize(rc.Width, rc.Height);
            document.CurrentLayer.Elements.Add(element);
        }

        internal void AddElementTo(ICoreWorkingObject obj, ICoreWorkingDocument d)
        {
            MethodInfo.GetCurrentMethod().Visit(this, obj, d);
        }

        public void AddElementTo(ICore2DDrawingLayeredElement obj, ICore2DDrawingDocument d)
        {
            d.CurrentLayer.Elements.Add(obj);
        }
    }
}
