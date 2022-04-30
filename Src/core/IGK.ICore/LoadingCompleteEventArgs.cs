

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LoadingCompleteEventArgs.cs
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
file:LoadingCompleteEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    public delegate void CoreLoadingCompleteEventHandler (object sender , CoreLoadingCompleteEventArgs e);
    /// <summary>
    /// represent a loading events args
    /// </summary>
    public class CoreLoadingCompleteEventArgs : EventArgs 
    {
        ICoreLoadingContext m_context;
        public ICoreLoadingContext Context {
            get { return this.m_context; }
        }
        public CoreLoadingCompleteEventArgs(ICoreLoadingContext e)
        { 
            this.m_context = e;
        }
        public override string  ToString()
        {
 	         return base.ToString();
        }
    }
}

