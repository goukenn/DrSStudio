

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CaptureWindow.cs
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
file:CaptureWindow.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

using IGK.AVICaptureApi.Native;


namespace IGK.AVICaptureApi.WinUI
{


    /// <summary>
    /// represent a capture windows
    /// </summary>
    public class CaptureWindow //: System.Windows.Forms.NativeWindow 
    {
        internal IntPtr  m_ParentHandle;
        private IntPtr  m_Handle;
        private uint m_driverId;
        private WindowManager m_Window;
        public IntPtr  Handle
        {
            get { return m_Handle; }
            internal set {
                this.m_Handle = value;
                m_Window = new WindowManager();
                m_Window.AssignHandle(value);
                System.Windows.Forms.Application.AddMessageFilter (m_Window);
            }
        }
        public IntPtr  ParentHandle
        {
            get { return m_ParentHandle; }
        }
        public CaptureWindow():base()
        {
            this.m_driverId = unchecked((uint)-1);
        }
        public override string ToString()
        {
            return string.Format("CaptureWindow [{0}]", this.Handle);
        }
        public bool ChooseCamSource()
        {
            int i = Native.User32.SendMessage(this.Handle,(uint)AviCap32.WM_CAP_DLG_VIDEOSOURCE, 0, 0);
            return i == 1;
        }
        /// <summary>
        /// connect to driver
        /// </summary>
        public bool Connect(uint driverid)
        {
            if (AviCap32.capDriverConnect(this.Handle, driverid))
            {
                this.m_driverId = driverid;
                return true;
            }
            return false;
        }
        public bool Disconnect()
        {
            return AviCap32.capDriverDisconnect (this.Handle);
        }
        class WindowManager : System.Windows.Forms.NativeWindow, System.Windows.Forms.IMessageFilter 
        {
            protected override void WndProc(ref System.Windows.Forms.Message m)            
            {
#if DEBUG
                //Console.WriteLine(m);
#endif
                switch (m.Msg)
                { 
                    case AviCap32.WM_CAP_DLG_VIDEOSOURCE :
                        break;
                    case AviCap32.WM_CAP_DRIVER_CONNECT :
                        m.LParam = new IntPtr(0);
                        //m.Result = new IntPtr(1);
                       // m.Msg = AviCap32.WM_CAP_DRIVER_DISCONNECT;
                        System.Threading.Thread th = new System.Threading.Thread(() => {
                            IntPtr v_f = IntPtr.Zero;
                            IntPtr v_n = Marshal.StringToCoTaskMemAnsi ("Video Source");
                            while ((v_f = User32.FindWindow(IntPtr.Zero,v_n )) == IntPtr.Zero)
                            { 
                              Thread .Sleep (5);
                            }
                            if (v_f != IntPtr.Zero)
                            {
                                NativeVideoSource vid = new NativeVideoSource();
                                vid.AssignHandle(v_f);                               
                                vid.DestroyHandle();
                            }
                        });
                        th.Start();
                        this.DefWndProc (ref m);
                        try
                        {
                            th.Abort();
                            th.Join();
                        }
                        catch { 
                        }
                        return;
                    case AviCap32.WM_CAP_DRIVER_DISCONNECT :
                        this.DefWndProc(ref m);
                        m.Result = new IntPtr(1);// IntPtr.Zero;
                        return;
                }
                base.WndProc(ref m);
            }
            #region IMessageFilter Members
            public bool PreFilterMessage(ref System.Windows.Forms.Message m)
            {
                switch (m.Msg)
                {
                    case AviCap32.WM_CAP_DLG_VIDEOSOURCE:
                        break;
                }
                return false;
            }
            #endregion
        }
        public bool SetMCIDevice(string deviceName)
        {
            IntPtr l = System.Runtime.InteropServices .Marshal.StringToCoTaskMemAnsi (deviceName );
            bool c = User32.BSendMessage(this.Handle, AviCap32.WM_CAP_SET_MCI_DEVICE, IntPtr.Zero,
                l);
            return c;
        }
        class NativeVideoSource : NativeWindow 
        {          
        }
    }
}

