using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.DB
{
    public enum enuCoreDataRelation
    {
        /// <summary>
        /// mark a relation for column
        /// </summary>
        OnePerOne,
        /// <summary>
        /// mark to be multiple
        /// </summary>
        OnePerMulti
    }
}
