using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS
{
    /// <summary>
    /// represent a togo context interface
    /// </summary>
    public  interface IGSContext
    {
        IGSDbContext DbContext { get; }
        /// <summary>
        /// /select all 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="andCondition"></param>
        /// <returns></returns>
        IGSDataQueryResult SelectAll(Type t, Dictionary<string, object > andCondition);
    }
}
