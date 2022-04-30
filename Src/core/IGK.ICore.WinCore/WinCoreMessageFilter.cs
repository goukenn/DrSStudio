

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreMessageFilter.cs
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
file:WinCoreMessageFilter.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.Windows.Native;

namespace IGK.ICore.WinCore
{
    public class WinCoreMessageFilter : IMessageFilter ,ICoreMessageFilter 
    {
        private static Dictionary<ICoreMessageFilter, WinCoreMessageFilter> sm_filter;
        private ICoreMessageFilter messageFilter;
        private WinCoreMessageHost m_messageHost;


        /// <summary>
        /// represent a core message host
        /// </summary>
        public class WinCoreMessageHost : ICoreMessage 
        {
            internal  Message Message;

            public int Msg
            {
                get { return Message.Msg;  }
            }
            public IntPtr HWnd
            {
                get { return Message.HWnd; }
            }
            public IntPtr LParam
            {
                get
                {
                    return Message.LParam;
                }
                set
                {
                    Message.LParam = value;
                }
            }
            public IntPtr WParam
            {
                get
                {
                    return Message.WParam;
                }
                set
                {
                    Message.WParam = value;
                }
            }
            public IntPtr Result
            {
                get
                {
                    return Message.Result;
                }
                set
                {
                    Message.Result = value;
                }
            }

            public bool IsKeyInputMessage()
            {
                switch (this.Msg)
                {
                    case User32.WM_KEYDOWN:
                    case User32.WM_KEYUP:
                        return true;
                    default:
                        break;
                }
                return false;
            }


            ///<summary>
            ///public .ctr
            ///</summary>
            public WinCoreMessageHost(Message message)
            {
                this.Message = message;
            }
            ///<summary>
            ///public .ctr
            ///</summary>
            public WinCoreMessageHost()
            {

            }

            public static WinCoreMessageHost CreateMessage(IntPtr wnd, KeyEventArgs e)
            {

                var m = new Message
                {
                    HWnd = wnd,
                    Msg = User32.WM_KEYDOWN,
                    WParam = IntPtr.Zero,
                    LParam = IntPtr.Zero
                };

                if (Environment.Is64BitOperatingSystem)
                {
                    m.WParam = new IntPtr((long)e.KeyCode);
                    
                }
                else
                    m.WParam = new IntPtr((int)e.KeyCode);

                return new WinCoreMessageHost() {
                    Message = m
                };


            }
        }



        static WinCoreMessageFilter() {
            sm_filter = new Dictionary<ICoreMessageFilter, WinCoreMessageFilter>();
        }
        public WinCoreMessageFilter(ICoreMessageFilter messageFilter)
        {            
            this.messageFilter = messageFilter;
            this.m_messageHost = new WinCoreMessageHost();
        }
        public bool PreFilterMessage(ref Message m)
        {
            this.m_messageHost.Message = m;
            ICoreMessage vdd = this.m_messageHost;
            return this.messageFilter.PreFilterMessage(ref vdd);
        }

        public bool PreFilterMessage(ref ICoreMessage m)
        {
            return this.messageFilter.PreFilterMessage(ref m);
        }

        internal static IMessageFilter CreateFilter(ICoreMessageFilter messageFilter)
        {
            if (messageFilter != null)
            {
                if (sm_filter.ContainsKey(messageFilter )){
                    return sm_filter[messageFilter ];
                }
                var d =     new WinCoreMessageFilter(messageFilter);
                sm_filter.Add(messageFilter, d);
                return d;
            }
            return null;
        }

        internal static IMessageFilter  GetFilter(ICoreMessageFilter messageFilter)
        {
            if ( (messageFilter != null) && (sm_filter.ContainsKey(messageFilter)))
            {
                return sm_filter[messageFilter];
            }
            return null;
        }

        internal static void Remove(ICoreMessageFilter messageFilter)
        {
            if ((messageFilter != null) && (sm_filter.ContainsKey(messageFilter)))
            {
                sm_filter.Remove(messageFilter);
            }
        }

        public static void RegisterFilter(ICoreMessageFilter filter)
        {
            if (filter is IMessageFilter)
            {
                Application.AddMessageFilter(filter as IMessageFilter);
            }
            else {
                var f = WinCoreMessageFilter.CreateFilter(filter);
                Application.AddMessageFilter(f);
            }
        }
        public static void UnregisterFilter(ICoreMessageFilter filter) {
            if (filter is IMessageFilter)
            {
                Application.RemoveMessageFilter(filter as IMessageFilter);
            }
            else {
                var f = WinCoreMessageFilter.GetFilter(filter);
                Application.RemoveMessageFilter(f);
                WinCoreMessageFilter.Remove(filter);
            }
        }
    }
}

