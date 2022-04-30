/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: KeyBoardStateCollections.cs
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
file:KeyBoardStateCollections.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace IGK.OGLGame.Input
{
    /// <summary>
    /// represent a keys board state collection
    /// </summary>
    public class KeyBoardStateCollections : IEnumerable 
    {
        Dictionary<enuKeyboardButton, KeyboardState> m_states;
        KeyboardInput m_input;
        enuKeyboardButton m_lastKeyboardButton;
        internal void Clear()
        {
            m_states .Clear();
        }
        internal KeyBoardStateCollections(KeyboardInput input)
        {

            m_states = new Dictionary<enuKeyboardButton,KeyboardState> ();
            foreach (enuKeyboardButton  key in Enum.GetValues (typeof (enuKeyboardButton )))
            {
                m_states.Add(key, new KeyboardState(key, enuKeyState.Up));
            }
            this.m_input = input;
        }
        /// <summary>
        /// get if a current key is down
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKeyDown(enuKeyboardButton key)
        {
            if (m_states.ContainsKey(key))
            {
                return m_states[key].IsKeyDown(key);
            }
            return false;
        }
        public bool IsKeyRelease(enuKeyboardButton key)
        {
            if (this.m_lastKeyboardButton == key)
            {
                if (m_states.ContainsKey(key))
                {
                    if (m_states[key].m_state == enuKeyState.Up) {
                        var s = m_states[key];
                        s.m_state = enuKeyState.Released;
                        m_states[key] = s;
                        return true;
                    }
                }
            }
            if (m_states.ContainsKey(key))
            {
                return m_states[key].IsKeyReleased(key);
            }
            return false;
        }
        public bool IsKeyUp(enuKeyboardButton key)
        {
            if (m_states.ContainsKey(key))
            {
                return m_states[key].IsKeyUp (key);
            }
            return false;
        }
        internal void Add(KeyboardState keyState)
        {
            this.m_states[keyState.m_skey]= keyState  ;
            if (keyState.m_state == enuKeyState.Pressed)
                this.m_lastKeyboardButton = keyState.m_skey;
        }
        public bool LastKeyDown(enuKeyboardButton keyButton)
        {
            return (keyButton == m_lastKeyboardButton );
        }
        internal void Update()
        {
              this.m_lastKeyboardButton = enuKeyboardButton.None;
        }
        public IEnumerator GetEnumerator()
        {
            return m_states.GetEnumerator();
        }
    }
}

