

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreApplicationAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreApplicationAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    /// <summary>
    /// represent a core application attribute. to register your application to core system.
    /// only one CoreApplication is require per application
    /// </summary>
    [AttributeUsage ( AttributeTargets.Class, 
        AllowMultiple = false ,
        Inherited = false )]
    public class CoreApplicationAttribute : Attribute 
    {        
       
        public string Title
        {
            get;set;
        }

        public string CopyRight { get;  set;}
        public string Author { get; set; }
        public string Name { get; set; }

        public CoreApplicationAttribute()
        {
        }
        public CoreApplicationAttribute(string title):this(title, title, CoreConstant.COPYRIGHT, CoreConstant.AUTHOR )
        {
            
        }
        public CoreApplicationAttribute(string title, string name, string copyright, string author)
        {
            this.Title = title;
            this.Name = name;
            this.CopyRight = copyright;
            this.Author = author;

        }
    }
}

