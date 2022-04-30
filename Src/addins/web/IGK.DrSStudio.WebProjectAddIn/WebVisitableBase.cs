

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebVisitableBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebVisitableBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WebProjectAddIn
{
    public class WebVisitableBase : IWebVisitable 
    {
        private ICoreWorkingObject m_WorkingObject;
        public ICoreWorkingObject WorkingObject
        {
            get { return m_WorkingObject; }
        }
        public WebVisitableBase(ICoreWorkingObject WorkingObject)
        {
            this.m_WorkingObject = WorkingObject;
        }
        protected void Visit(IWebVisitor visitor)
        {
            System.Reflection.MethodInfo c = visitor.GetType().GetMethod("Visit", new Type[] { this.m_WorkingObject.GetType() });// System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance ,
            if (c != null)
                c.Invoke(visitor, new object[] { this.m_WorkingObject });
            else
                visitor.Visit(this);
        }
        public virtual void Accept(IWebVisitor visitor)
        {
            this.Visit(visitor);        
        }
    }
}

