using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.SVGAddIn
{
    public interface ISVGElementContainer
    {
        void Add(ISVGElement svgElement);
        void Remove(ISVGElement svgElement);
    }
}
