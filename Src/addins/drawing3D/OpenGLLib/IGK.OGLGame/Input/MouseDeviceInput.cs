


using IGK.ICore;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MouseDevice.cs
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
file:MouseDevice.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.OGLGame.Input
{
    public delegate void MouseDeviceEventHandler(MouseDeviceEventArgs e);
    public class MouseDeviceEventArgs : EventArgs
    {
        MouseDeviceInput m_Device;
        private enuGLGameMouseButton m_Button;
        private int m_X;
        private int m_Y;
        public int Y
        {
            get { return m_Y; }
        }
        public int X
        {
            get { return m_X; }
        }
        public enuGLGameMouseButton Button
        {
            get { return m_Button; }
        }
        public MouseDeviceEventArgs(MouseDeviceInput device,enuGLGameMouseButton button, int x, int y )
        {
            this.m_Device = device;
            this.m_Button = button;
            this.m_X = x;
            this.m_Y = y;
        }
        public override string ToString()
        {
            return string.Format("{0}, x:{1} y:{2}", m_Button ,m_X , m_Y );
        }
        public MouseDeviceInput MouseDevice {
            get {
                return this.m_Device;
            }
        }
    }
    /// <summary>
    /// represent a mouse device
    /// </summary>
    public class MouseDeviceInput : ICoreMessageFilter 
    {
        private static MouseDeviceInput sm_instance;
        private Dictionary<enuGLGameMouseButton, MouseState> m_Buttons;
        private enuGLGameMouseButton m_Button;
        private enuMouseState  m_State;
        private Vector2i  m_location;        
        private bool m_ismouseWheel;
        private int m_mouseWheelDelta;
        private int m_key;
        private MouseDeviceEventHandler m_MouseDown;
        private MouseDeviceEventHandler m_MouseMotion;
        /// <summary>
        /// get the mouse well
        /// </summary>
        public static bool MouseWheel {
            get { return sm_instance.m_ismouseWheel; }
        }
        /// <summary>
        /// get the mouse device location
        /// </summary>
        public static Vector2i  Location
        {
            get { return sm_instance .m_location ; }
        }
        /// <summary>
        /// get the mouse wheel delta
        /// </summary>
        public static int MouseWheelDelta{
            get{
                return sm_instance.m_mouseWheelDelta;
            }
        }
        /// <summary>
        /// get the button state
        /// </summary>
        public static enuMouseState  State
        {
            get { return sm_instance.m_State; }
            private set
            {
                if (sm_instance.m_State != value)
                {
                    sm_instance.m_State = value;
                }
            }
        }
        ///// <summary>
        ///// get the mouse device button
        ///// </summary>
        //public static enuMouseButton Button
        //{
        //    get { return sm_instance.m_Button; }
        //    private set
        //    {
        //        if (sm_instance.m_Button != value)
        //        {
        //            sm_instance.m_Button = value;
        //        }
        //    }
        //}
        private MouseDeviceInput()
        {
            m_Buttons = new Dictionary<enuGLGameMouseButton, MouseState>();
        }
        public static void RegisterMouseDownCallBack(MouseDeviceEventHandler _delegate)
        {
            sm_instance.m_MouseDown += _delegate;
        }
        public static void RegisterMouseMotionCallBack(MouseDeviceEventHandler _delegate)
        {
            sm_instance.m_MouseMotion  += _delegate;
        }
        public static void UnRegisterMouseDownCallBack(MouseDeviceEventHandler _delegate)
        {
            sm_instance.m_MouseDown -= _delegate;
        }
        public static void UnRegisterMouseMotionCallBack(MouseDeviceEventHandler _delegate)
        {
            sm_instance.m_MouseMotion -= _delegate;
        }
        static MouseDeviceInput()
        {
            sm_instance = new MouseDeviceInput();
           // (CoreApplicationManager.Application as ICoreMessageFilterApplication ).AddMessageFilter(sm_instance);
			IGK.OGLGame.GLGame.AddUpdateComplete(sm_instance .UpdateComplete);
        }
        /// <summary>
        /// register
        /// </summary>
        /// <param name="filter"></param>
        public static void Register(ICoreMessageFilterApplication filter) {
            
            if (  sm_instance.m_filter  != null)
            {
                  sm_instance.m_filter.RemoveMessageFilter(sm_instance);
            }
            sm_instance.m_filter =  filter;
            if (sm_instance.m_filter != null)
            {
                sm_instance.m_filter.AddMessageFilter(sm_instance);
            }
        }
		void UpdateComplete()
		{
			if (this.m_State == enuMouseState.Released )
			{
			 	sm_instance.m_Button = enuGLGameMouseButton.None ;
            	sm_instance.m_State = enuMouseState.Released;           
			}
            this.m_ismouseWheel = false;
            this.m_mouseWheelDelta = 0;
		}
        #region IMessageFilter Members
        public bool PreFilterMessage(ref ICoreMessage m)
        {
            switch (m.Msg)
            {
                case WM_LBUTTONDOWN :
                case WM_LBUTTONDBLCLK:
                    sm_instance.m_Button = enuGLGameMouseButton.Left;
                    sm_instance.m_State = enuMouseState.Pressed;
                    sm_instance.SetButton();
                    sm_instance.m_location = GetLocation(m.LParam.ToInt64());
                    sm_instance.OnMouseDown();
                    return false ;                    
                case WM_LBUTTONUP:
                case WM_NCLBUTTONUP:
                    sm_instance.m_Button = enuGLGameMouseButton.Left;
                    sm_instance.m_State = enuMouseState.Up ;
                    sm_instance.SetButton();
                    sm_instance.m_location = GetLocation(m.LParam.ToInt64());
                    return false ;
                case WM_MOUSEMOVE :
                case WM_NCMOUSEMOVE:
                    sm_instance.m_location = GetLocation(m.LParam.ToInt64());
                    sm_instance.OnMouseMotion();
                    break;
                case WM_RBUTTONDOWN :
                case WM_RBUTTONDBLCLK :
                    sm_instance.m_Button = enuGLGameMouseButton.Right ;
                    sm_instance.m_State = enuMouseState.Pressed;
                    sm_instance.SetButton();
                    sm_instance.m_location = GetLocation(m.LParam.ToInt64());
                    sm_instance.OnMouseDown();
                    break;
                case WM_RBUTTONUP :
                    sm_instance.m_Button = enuGLGameMouseButton.Right;
                    sm_instance.m_State = enuMouseState.Up;
                    sm_instance.SetButton();
                    sm_instance.m_location = GetLocation(m.LParam.ToInt64());
                    break;
                case WM_MOUSEWHEEL:
                    sm_instance.m_ismouseWheel = true;
                    Vector2i i = GetLocation (m.WParam.ToInt64 () );
                    sm_instance.m_key = i.X;
                    sm_instance.m_mouseWheelDelta = i.Y;
                    sm_instance.m_location = GetLocation(m.LParam.ToInt64());
                    break;
            }
            return false ;
        }

        private void SetButton()
        {
            if (this.m_Buttons.ContainsKey(this.m_Button))
            {
                var t = this.m_Buttons[this.m_Button];
                t.m_state = this.m_State;
                this.m_Buttons[this.m_Button] = t;
            }
            else {
                var t = new MouseState(this.m_Button, this.m_State);
                this.m_Buttons[this.m_Button] = t;
            }
        }
        private void OnMouseMotion()
        {
            if (this.m_MouseMotion != null)
                this.m_MouseMotion(new MouseDeviceEventArgs(this, this.m_Button, this.m_location.X, this.m_location.Y));
        }
        private void OnMouseDown()
        {
            if (this.m_MouseDown != null)
                this.m_MouseDown(new MouseDeviceEventArgs(this, this.m_Button, this.m_location.X, this.m_location.Y));
        }
        private Vector2i GetLocation(long p)
        {
            Vector2i l = new Vector2i((short )(p & 0xFFFF), (short)((p >> 16) & 0xFFFF));
            return l;
        }
        #endregion
        internal const int WM_MOUSEMOVE = 0x200;
        internal const int WM_MOUSELEAVE = 0x0;
        internal const int WM_LBUTTONDOWN = 0x201;
        internal const int WM_LBUTTONUP = 0x202;
        internal const int WM_LBUTTONDBLCLK = 0x203;
        internal const int WM_RBUTTONDOWN = 0x204;
        internal const int WM_RBUTTONUP = 0x205;
        internal const int WM_RBUTTONDBLCLK = 0x206;
        internal const int WM_NCMOUSEMOVE = 0xa0;
        internal const int WM_NCLBUTTONUP = 0xa2;
        internal const int WM_MOUSEWHEEL = 0x20a;
        private ICoreMessageFilterApplication m_filter;

        public static MouseState GetState(enuGLGameMouseButton item)
        {
            return sm_instance.m_Buttons.ContainsKey(item) ?  sm_instance.m_Buttons[item] : MouseState.Empty;
        }
    }
}

