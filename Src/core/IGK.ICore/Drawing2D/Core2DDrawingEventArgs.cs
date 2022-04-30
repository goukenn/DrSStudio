

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingEventArgs.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    using IGK.ICore;using IGK.ICore.WinUI;

    /// <summary>
    /// represent an event args to past drawing parameter to element before
    /// </summary>
    public class Core2DDrawingEventArgs: CoreEventArgs
    {
        private Vector2f m_StartPoint;
        private Vector2f m_EndPoint;
        private ICoreSnippet m_Snippet;
        private int m_State;
        /// <summary>
        /// get or set the state of this
        /// </summary>
        public int State
        {
            get { return m_State; }
            set
            {
                if (m_State != value)
                {
                    m_State = value;
                }
            }
        }
        
        public ICoreSnippet Snippet
        {
            get { return m_Snippet; }           
        }
        public Vector2f EndPoint
        {
            get { return m_EndPoint; }
            set
            {
                if (m_EndPoint != value)
                {
                    m_EndPoint = value;
                }
            }
        }
        public Vector2f StartPoint
        {
            get { return m_StartPoint; }
            set
            {
                if (m_StartPoint != value)
                {
                    m_StartPoint = value;
                }
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        public Core2DDrawingEventArgs(Vector2f startPoint):this(startPoint,startPoint, 0, null)
        {

        }
        /// <summary>
        /// .ctr to create a working object element
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="State"></param>
        /// <param name="snippet"></param>
        public Core2DDrawingEventArgs(Vector2f startPoint, Vector2f endPoint, 
            int State, 
            ICoreSnippet snippet)
        {

            this.m_StartPoint = startPoint;
            this.m_EndPoint = endPoint;
            this.m_State = State;
            this.m_Snippet = snippet;
        }
    }
}
