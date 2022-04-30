

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: KeyboardState.cs
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
file:KeyboardState.cs
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
using System.Windows.Forms ;
namespace IGK.OGLGame.Input
{
    /// <summary>
    /// represent a key board state
    /// </summary>
    public struct KeyboardState
    {
        internal enuKeyboardButton m_skey; //Game input keys
        //internal Keys m_key; //System.Windows.Forms.Keys
        internal enuKeyState m_state;
        internal static readonly KeyboardState Empty;
        static KeyboardState() {
            Empty = new KeyboardState();
        }
        public KeyboardState(enuKeyboardButton button, enuKeyState state)
        {
            this.m_skey = button;
            this.m_state = state;
        }
        public override string ToString()
        {
            return string.Format("{0},{1}", this.m_skey.ToString(), this.m_state);
        }
        public bool IsKeyDown(enuKeyboardButton keys)
        {
            return (keys == this.m_skey) && (m_state == enuKeyState.Pressed);
        }
        public bool IsKeyUp(enuKeyboardButton keys)
        {
            return (keys == this.m_skey) && (m_state == enuKeyState.Up);
        }
        public bool IsKeyReleased(enuKeyboardButton key)
        {
            return (key == this.m_skey) && (m_state == enuKeyState.Released);
        }
        ///// <summary>
        ///// get if the current key is down
        ///// </summary>
        ///// <param name="keys"></param>
        ///// <returns></returns>
        //public bool IsKeyDown(System.Windows.Forms.Keys keys)
        //{
        //    return (keys == this.m_key) && (m_state == enuKeyState.Pressed);
        //}
        //public bool IsKeyUp(System.Windows.Forms.Keys keys)
        //{
        //    return (keys == this.m_key) && (m_state == enuKeyState.Release);
        //}
    }
}

