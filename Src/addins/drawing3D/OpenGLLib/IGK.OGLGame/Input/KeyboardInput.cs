

using IGK.ICore;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: KeyboardInput.cs
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
file:KeyboardInput.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace IGK.OGLGame.Input
{
    public sealed class KeyboardInput : ICoreMessageFilter , IMessageFilter 
    {
        private KeyboardState m_currentState;        //represent the current state
        private KeyBoardStateCollections m_keyBoardState;
        
        /// <summary>
        /// get or set keyboardInput capture all keys
        /// </summary>
        public static bool Capture
        {
            get { return sm_instance .m_capture ; }
            set
            {
                if (sm_instance.m_capture != value)
                {
                    sm_instance.m_capture = value;
                }
            }
        }
        public static KeyboardInput Instance {
            get { return sm_instance; }
        }
        public static KeyBoardStateCollections GetState()
        {
            KeyBoardStateCollections st = Instance.m_keyBoardState;           
            return st;
        }
        private static KeyboardInput sm_instance;
        private KeyboardInput()
        {
            //GLGame.GameTick += new EventHandler(GLGame_GameTick);
            this.m_keyBoardState = new KeyBoardStateCollections(this);
            this.m_capture = true;
        }
        static KeyboardInput()
        {
            sm_instance = new KeyboardInput();
            try
            {
                (CoreApplicationManager.Application as ICoreMessageFilterApplication)
                    ?.AddMessageFilter(sm_instance);
            }
            catch
            {

            }
           // Application.AddMessageFilter(sm_instance);
        }
        #region IMessageFilter Members
        public bool PreFilterMessage(ref ICoreMessage m)
        {
            return  _HandledKeyboard(m.Msg, m.WParam);           
         
        }

        private bool _HandledKeyboard(int msg, IntPtr WParam)
        {
            if (!m_capture)
                return false;
            switch (msg)
            {
                case WM_KEYDOWN:
                    {
                        bool m_isControl = ((Control.ModifierKeys & Keys.Control) == Keys.Control);
                        bool m_isShift = ((Control.ModifierKeys & Keys.Shift) == Keys.Shift);
                        bool m_isAlt = ((Control.ModifierKeys & Keys.Alt) == Keys.Alt);
                        int ik = WParam.ToInt32();
                        Keys k = (Keys)char.ToUpper((char)(byte)ik);
                        if (m_isControl)
                            k |= Keys.Control;
                        if (m_isShift)
                            k |= Keys.Shift;
                        if (m_isAlt)
                            k |= Keys.Alt;
                        m_currentState = new KeyboardState();
                        //m_currentState.m_key = k;
                        m_currentState.m_skey = (enuKeyboardButton)k;
                        m_currentState.m_state = enuKeyState.Pressed;
                        this.m_keyBoardState.Add(m_currentState);
                    }
                    return this.m_capture;
                case WM_KEYUP:
                    {
                        bool m_isControl = ((Control.ModifierKeys & Keys.Control) == Keys.Control);
                        bool m_isShift = ((Control.ModifierKeys & Keys.Shift) == Keys.Shift);
                        bool m_isAlt = ((Control.ModifierKeys & Keys.Alt) == Keys.Alt);
                        int ik = WParam.ToInt32();
                        Keys k = (Keys)char.ToUpper((char)(byte)ik);
                        if (m_isControl)
                            k |= Keys.Control;
                        if (m_isShift)
                            k |= Keys.Shift;
                        if (m_isAlt)
                            k |= Keys.Alt;
                        //create a keys stated
                        m_currentState = new KeyboardState();
                        //m_currentState.m_key = k;
                        m_currentState.m_skey = (enuKeyboardButton)k;
                        m_currentState.m_state = enuKeyState.Up;
                        this.m_keyBoardState.Add(m_currentState);
                    }
                    return this.m_capture;
            }
            m_currentState = KeyboardState.Empty;
            return false;
        }
        #endregion
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        private bool m_capture;
        private static ICoreMessageFilterApplication sm_filter;
        /// <summary>
        /// register a filter message for key board
        /// </summary>
        /// <param name="appFilter"></param>
        public static void Register(ICoreMessageFilterApplication appFilter)
        {
            if (sm_filter != null)
                sm_filter.RemoveMessageFilter(sm_instance);
            sm_filter = appFilter ;
            if (sm_filter !=null){
                sm_filter.AddMessageFilter(sm_instance);
            }
        }
        /// <summary>
        /// IMessageFilter interface
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            return _HandledKeyboard(m.Msg, m.WParam);
        }
    }
}

