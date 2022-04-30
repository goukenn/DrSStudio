using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    /// <summary>
    /// mark assembly to load 
    /// </summary>
    [AttributeUsage (AttributeTargets.Assembly, AllowMultiple =false  )]
    public class CoreDataBaseInitializerAttribute : Attribute 
    {

        public Type InitializerType { get; set; }
        ///<summary>
        ///public .ctr
        ///</summary>
        public CoreDataBaseInitializerAttribute()
        {

        }
    }
}
