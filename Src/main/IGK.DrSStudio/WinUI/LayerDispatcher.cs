

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerDispatcher.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Dispatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.DrSStudio.WinUI
{

    /// <summary>
    /// represent a layer dispatcheable event on windows form surface
    /// </summary>
    public class LayerDispatcher : ICoreDispatcher, ICoreMouseClickDispatcher, ICoreMouseMoveDispatcher
    {
        private Dictionary<string, DispatcherInfo> m_dictionary;
        public static readonly string MouseClickDispatching = "MouseClick";
        public static readonly string MouseHoverDispatching = "MouseHover";
        public static readonly string MouseMoveDispatching = "MouseMove";

        public LayerDispatcher()
        {
            this.m_dictionary = new Dictionary<string, DispatcherInfo>();
        }
        
        public bool Register(ICoreWorkingObject wObject, string eventName, MulticastDelegate @delegate)
        {
            if ((wObject == null) || (string.IsNullOrEmpty(eventName) || @delegate == null))
                return false ;
            DispatcherInfo v_info = null;
            if (!this.m_dictionary.ContainsKey(eventName))
            {
                v_info = DispatcherInfo.CreateDispatcher(eventName);
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
            DispatcherInfo v_info = null;
            if (this.m_dictionary.ContainsKey(eventName))
            {
                v_info = this.m_dictionary[eventName];
                v_info.RemoveHandle(wObject, @delegate);
            }
        }
        public void UnRegister(ICoreWorkingObject wObject)
        {
            foreach (var item in GetRegistrableEventNames())
            {
                this.m_dictionary [item].RemoveHandle (wObject , null);
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
            DispatcherInfo v_info = GetDispatcherInfo(MethodInfo.GetCurrentMethod().Name);
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
            DispatcherInfo v_info = GetDispatcherInfo(MethodInfo.GetCurrentMethod().Name);
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
            DispatcherInfo v_info = GetDispatcherInfo(MethodInfo.GetCurrentMethod().Name);
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
        public DispatcherInfo GetDispatcherInfo(string name)
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
