using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI.Dispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore.Dispatch
{
    class WinCoreDispatcherInfo : ICoreDispatcherInfo
    {
        private string m_eventName;
        private CoreDispatcherEvent m_dispatcherEvent;
        private Dictionary<ICoreWorkingObject, MulticastDelegate> m_handles;
        private List<ICoreWorkingObject> m_keys;
        public string EventName { get { return this.m_eventName; } }

        public override string ToString()
        {
            return "DispatcherInfo:" + this.m_eventName;
        }
        private WinCoreDispatcherInfo(string eventName, CoreDispatcherEvent dispacherEvent)
        {            
            this.m_eventName = eventName;
            this.m_dispatcherEvent = dispacherEvent;
            this.m_handles = new Dictionary<ICoreWorkingObject, MulticastDelegate>();
        }
        /// <summary>
        /// create a drsstudio dispather dispatcher
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static WinCoreDispatcherInfo CreateDispatcher(string name) {
            Type t = CoreAssemblyUtility.FindType(string.Format(CoreConstant.DISPATCHER_TYPE_RESOLV_1, name));
            if ((t != null) && (!t.IsAbstract ))
            {
                var v_dipacther =  t.Assembly.CreateInstance(t.FullName) as CoreDispatcherEvent;
                WinCoreDispatcherInfo r = new WinCoreDispatcherInfo(name, v_dipacther);
                return r;
            }
            return null;
        }

        public void AttachHandle(ICoreWorkingObject wObject, MulticastDelegate @delegate)
        {
            MulticastDelegate del = null;
            if (this.m_handles.ContainsKey(wObject))
            {
                del = this.m_handles[wObject];
                del =(MulticastDelegate) MulticastDelegate.Combine(del, @delegate);
                this.m_handles[wObject] = del;
            }
            else {
                this.m_handles.Add(wObject, @delegate);
                if (wObject is ICoreWorkingDisposableObject) 
                { 
                    (wObject as ICoreWorkingDisposableObject).Disposed += (o,e)=>
                    {
                        RemoveHandle (wObject, @delegate );
                    };
                }
                this.m_keys = this.m_handles.Keys.ToList();
                this.m_keys.Sort((a,b)=>{
                    ICore2DDrawingLayeredElement a1 = a as ICore2DDrawingLayeredElement;
                    ICore2DDrawingLayeredElement a2 = b as ICore2DDrawingLayeredElement;
                    return a1.ZIndex .CompareTo (a2.ZIndex );
                });
                //this.m_handles.OrderBy( i => ((ICore2DDrawingLayeredElement )i.Key).ZIndex);
            }            
        }

        public void RemoveHandle(ICoreWorkingObject wObject, MulticastDelegate @delegate)
        {
            if (this.m_handles.ContainsKey(wObject))
            {
                var d = this.m_handles[wObject];
                var e = MulticastDelegate.Remove(d, @delegate);
                m_handles.Remove(wObject);
                this.m_keys = this.m_handles.Keys.ToList();
            }
          
        }

        /// <summary>
        /// invoke the argument
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public virtual bool Invoke(EventArgs arguments)
        {
            bool process = false;
            var t = this.m_keys;// this.m_handles.Keys.ToArray<ICoreWorkingObject>();

            Type tt = typeof(CoreDispatcherEventArgs<>);
            Type cc = tt.MakeGenericType(arguments.GetType());
            var m1 = cc.GetConstructor(new Type[] { this.GetType(), arguments.GetType() });         
            
          var e = m1.Invoke( new object[]{
                this, arguments 
            }) ;
            ICoreDispatcherEventArgs prop = e as ICoreDispatcherEventArgs;
            foreach (ICoreWorkingObject obj in t)
            {
                if (this.m_handles.ContainsKey(obj) && this.m_dispatcherEvent.CanProcess(obj, arguments))
                {
                   this.m_handles[obj].DynamicInvoke(obj, e);                    

                   process = true;
                   if (prop.StopPropagation)
                   {
                       break;
                   }
                }
            }
            return process;
        }
    }
}

