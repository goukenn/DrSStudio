

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLFont.cs
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
file:GLFont.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.OGLGame.Text
{
    public abstract class GLFont : IDisposable 
    {
        private string m_name;
        /// <summary>
        /// get the name of this Font
        /// </summary>
        public string Name
        {
            get
            {
                return this.m_name;
            }
            protected set {
                this.m_name = value ;
            }
        }
        public GLFont()
        {
        }
        #region IDisposable Members
        public virtual void Dispose()
        {
        }
        #endregion
    }
}

