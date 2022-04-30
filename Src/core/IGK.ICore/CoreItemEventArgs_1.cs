

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreItemEventArgs_1.cs
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
file:CoreItemEventArgs_1.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{

    public delegate void CoreItemEventHandler(object sender, CoreItemEventArgs e);

    /// <summary>
    /// represent a class item event args
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CoreItemEventArgs<T> : CoreItemEventArgs 
    {
        public new T Item
        {
            get { return (T)base.Item; }
        }
        public CoreItemEventArgs(T item):base(item)
        {
        }
    }
}

