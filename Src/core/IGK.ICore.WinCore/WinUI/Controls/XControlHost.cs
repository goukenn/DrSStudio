

using IGK.ICore.WinCore.WinUI.Controls;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XControlHost.cs
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
file:XControlHost.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.ICore.WinUI
{
    public class XControlHost<T> : IGKXControl where T: Control 
    {
        private T m_Host;
        public T Host
        {
            get { return m_Host; }
            set
            {
                if (m_Host != value)
                {
                    m_Host = value;
                    this.Controls.Clear();
                    if (m_Host !=null)
                    this.Controls.Add(m_Host);
                }
            }
        }
        public XControlHost()
        {
            this.Host = typeof(T).Assembly.CreateInstance (typeof(T).FullName ) as T;
        }
        protected override Control.ControlCollection CreateControlsInstance()
        {
            return null;// new ControlHost<T>(this);
        }
        class ControlHost<M> : Control.ControlCollection where M : Control 
        {
            private XControlHost<M> xControlHost;
            public ControlHost(XControlHost<M> xControlHost):base(xControlHost)
            {
                this.xControlHost = xControlHost;
            }
            public override void Add(Control value)
            {
                if (value == this.xControlHost.Host)
                {
                    base.Add(value);
                }
            }
            public override void Remove(Control value)
            {
                if (value == this.xControlHost.Host)
                {
                    base.Remove(value);
                }
            }
        }
    }
}

