

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DispatcherEvent.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.ICore.WinUI.Dispatch
{
    /// <summary>
    /// represent base dispatcher event. as a visitable process 
    /// </summary>
    public class CoreDispatcherEvent : ICoreDispatcherEvent
    {
        public virtual  bool CanProcess(ICoreWorkingObject obj, params object[] arguments)
        {

            if ((obj == null) || (arguments == null))
                return false;
            List<Object> t = new List<object>();
            t.Add(obj);
            t.AddRange(arguments);

            object response = MethodInfo.GetCurrentMethod().Visit(this, t.ToArray());
            if (response != null)
                return (bool)response;
            return false;
        }      
       
    }
}
