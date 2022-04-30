using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    public  interface ICoreWorkingModifiableElementContainer : ICoreWorkingElementContainer
    {
        void Add(ICoreWorkingObject obj);
        void Remove(ICoreWorkingObject obj);
    }
}
