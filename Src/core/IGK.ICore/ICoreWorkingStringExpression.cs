using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    public interface ICoreWorkingStringExpression : ICoreWorkingObject
    {
        /// <summary>
        /// render this object and return a string
        /// </summary>
        /// <returns></returns>
        string Render();
    }
}
