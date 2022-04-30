using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WebGLEngine
{
    public delegate void WebGLObjectEventHandler<T>(object o, WebGLObjectEventArgs<T> e) where T : class;
}
