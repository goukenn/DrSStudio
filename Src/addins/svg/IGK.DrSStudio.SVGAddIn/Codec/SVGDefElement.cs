using System;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using System.Collections.Generic;

namespace IGK.DrSStudio.SVGAddIn
{
    internal class SVGDefElement : Core2DDrawingLayeredElement, ICoreWorkingObject, ISVGElement
    {
        List<ICoreWorkingObject> c = new List<ICoreWorkingObject>();
        public ICoreWorkingObject GetElementById(string id) {
            foreach (var item in c)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            
        }

        internal void Add(ICoreWorkingObject l)
        {
            this.c.Add(l);
        }
    }
}