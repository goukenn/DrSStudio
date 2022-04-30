/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NetGLFileGenerator
{
    class ConstantCollection : System.Collections.IEnumerable 
    {
        List<GLConstant> m_constants;

        public ConstantCollection()
        {
            this.m_constants = new List<GLConstant>();
        }
        public int Count { get { return this.m_constants.Count; } }
        public void Add(GLConstant cons)
        {
            this.m_constants.Add(cons);
        }



        #region IEnumerable Members

        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_constants.GetEnumerator();
        }

        #endregion
    }
}
