

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMouseEventArgs.cs
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
file:CoreMouseEventArgs.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    using IGK.ICore;using IGK ;
    public delegate void CoreMouseEventHandler(object o, CoreMouseEventArgs e);
    /// <summary>
    /// represent the mouse event args
    /// </summary>
    public class CoreMouseEventArgs : EventArgs 
    {
        private Vector2f m_FactorPoint;
        private Vector2i m_Location;
        private int m_delta;
        private enuMouseButtons m_MouseButton;
        private int m_Clicks;
        public int X { get { return this.m_Location.X; } }
        public int Y { get { return this.m_Location.Y; } }
        public int Clicks
        {
            get { return m_Clicks; }
        }
        public enuMouseButtons Button
        {
            get { return m_MouseButton; }
        }
        public int Delta
        {
            get { return m_delta; }
        }
        /// <summary>
        /// get the factor Vector2i
        /// </summary>
        public Vector2i Location
        {
            get { return m_Location; }
        }
        /// <summary>
        /// get the factor Vector2i
        /// </summary>
        public Vector2f FactorPoint
        {
            get { return m_FactorPoint; }
        }
        public CoreMouseEventArgs(enuMouseButtons button,
            Vector2i location,
            Vector2f factorVector2i,
            int delta,
            int Clicks)
        {
            this.m_MouseButton = button;
            this.m_FactorPoint = factorVector2i;
            this.m_Location = location;
            this.m_delta = delta;
            this.m_Clicks = Clicks;
        }
    }
}

