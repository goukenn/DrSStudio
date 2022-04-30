using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Dispatch
{
    /// <summary>
    /// represent a dispatcher event args
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CoreDispatcherEventArgs<T> : EventArgs, ICoreDispatcherEventArgs
    {
        private T m_Event;
        private ICoreDispatcherInfo m_dispatcherInfo;
        public T Event {
            get {
                return this.m_Event;
            }
        }
        private bool m_StopPropagation;

        public bool StopPropagation
        {
            get { return m_StopPropagation; }
            set
            {
                if (m_StopPropagation != value)
                {
                    m_StopPropagation = value;
                }
            }
        }
        
        public CoreDispatcherEventArgs(ICoreDispatcherInfo dispatcher, T eventa)
        {
            this.m_dispatcherInfo = dispatcher;
            this.m_Event = eventa;
        }
    }
}
