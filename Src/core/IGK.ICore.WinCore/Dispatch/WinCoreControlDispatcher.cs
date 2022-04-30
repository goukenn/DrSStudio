using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Dispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.Dispatch
{
    /// <summary>
    /// represent a wincore dipathce info 
    /// </summary>
    public class WinCoreControlDispatcher : ICoreDispatcher, ICoreMouseClickDispatcher, ICoreMouseMoveDispatcher
    {
        private Dictionary<string, ICoreDispatcherInfo> m_dictionary;
        public static readonly string MouseClickDispatching = "MouseClick";
        public static readonly string MouseHoverDispatching = "MouseHover";
        public static readonly string MouseMoveDispatching = "MouseMove";


        private ICoreControl m_control;

        public WinCoreControlDispatcher(ICoreControl control)
        {
            if (control == null)
                throw new ArgumentNullException("control");
            this.m_dictionary = new Dictionary<string, ICoreDispatcherInfo>();
            this.m_control = control;
            this.m_control.MouseClick += m_control_MouseClick;
        }

        void m_control_MouseClick(object sender, CoreMouseEventArgs e)
        {
            this.MouseClick(e);
        }
        
        public bool Register(ICoreWorkingObject wObject, string eventName, MulticastDelegate @delegate)
        {
            if ((wObject == null) || (string.IsNullOrEmpty(eventName) || @delegate == null))
                return false ;
            ICoreDispatcherInfo v_info = null;
            if (!this.m_dictionary.ContainsKey(eventName))
            {
                v_info = WinCoreDispatcherInfo.CreateDispatcher(eventName);
                if (v_info == null)
                {
                    return false;
                }
                this.m_dictionary.Add(eventName, v_info);
            }
            else
            {
                v_info = this.m_dictionary[eventName];
            }
            v_info.AttachHandle(wObject, @delegate);
            return true;
        }
        public void UnRegister(ICoreWorkingObject wObject, string eventName, MulticastDelegate @delegate)
        {
            ICoreDispatcherInfo v_info = null;
            if (this.m_dictionary.ContainsKey(eventName))
            {
                v_info = this.m_dictionary[eventName];
                v_info.RemoveHandle(wObject, @delegate);
            }
        }
        public void UnRegister(ICoreWorkingObject wObject)
        {
           foreach(string s in GetRegistrableEventNames ())
           { 
               this.m_dictionary[s].RemoveHandle(wObject, null);
           }
        }
        /// <summary>
        /// get a lists of registrated dispatcher event methods
        /// </summary>
        /// <returns></returns>
        public virtual string[] GetRegistrableEventNames()
        {
            return this.m_dictionary.Keys.ToArray<string>();
        }

        public bool MouseClick(CoreMouseEventArgs e)
        {
            ICoreDispatcherInfo v_info = GetDispatcherInfo(MethodInfo.GetCurrentMethod().Name);
            if (v_info!=null) 
            {
                object obj = v_info.Invoke(e) ;
                if (obj is bool) {
                    return (bool)obj;
                }
            }
            return false;
        }
        
        public bool MouseHover(CoreMouseEventArgs e)
        {
            ICoreDispatcherInfo v_info = GetDispatcherInfo(MethodInfo.GetCurrentMethod().Name);
            if (v_info != null)
            {
                object obj = v_info.Invoke(e);
                if (obj is bool)
                {
                    return (bool)obj;
                }
            }
            return false;
        }

        public bool MouseMove(CoreMouseEventArgs e)
        {
            ICoreDispatcherInfo v_info = GetDispatcherInfo(MethodInfo.GetCurrentMethod().Name);
            if (v_info != null)
            {
                object obj = v_info.Invoke(e);
                if (obj is bool)
                {
                    return (bool)obj;
                }
            }
            return false;
        }
        /// <summary>
        /// get the dispatcher info
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ICoreDispatcherInfo GetDispatcherInfo(string name)
        {
            if (this.m_dictionary.ContainsKey(name))
                return this.m_dictionary[name];
            return null;
        }


        ICoreDispatcherInfo ICoreDispatcher.GetDispatcherInfo(string name)
        {
            return this.GetDispatcherInfo(name) as ICoreDispatcherInfo;
        }

        public int Count
        {
            get { return this.m_dictionary.Count; }
        }

        public void Clear()
        {
            this.m_dictionary.Clear();
        }


        public void Invoke(string eventName, EventArgs e)
        {
            var t = this.GetDispatcherInfo(eventName);
            if (t != null) {
                t.Invoke(e);
            }
        }

        void ICoreMouseClickDispatcher.Invoke(CoreMouseEventArgs e)
        {
            this.MouseClick(e);
        }

        void ICoreMouseEventDispatcher.Invoke(CoreMouseEventArgs e)
        {
            
        }

        void ICoreMouseMoveDispatcher.Invoke(CoreMouseEventArgs e)
        {
            this.MouseMove(e);
        }


       
    }
}
