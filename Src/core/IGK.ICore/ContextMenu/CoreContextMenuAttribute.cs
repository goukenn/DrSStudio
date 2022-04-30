

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreContextMenuAttribute.cs
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
file:CoreContextMenuAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.ContextMenu
{
    using IGK.ICore;using IGK.ICore.Actions;
    using IGK.ICore.WinUI;
    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = false)]
    public class CoreContextMenuAttribute : CoreActionAttribute
    {
        private int m_index;
        private enuKeys  m_ShortCut;
        /// <summary>
        /// get or set the shortcut keys
        /// </summary>
        public enuKeys  ShortCut
        {
            get { return m_ShortCut; }
            set
            {
                if (m_ShortCut != value)
                {
                    m_ShortCut = value;
                }
            }
        }
        public int Index { get { return this.m_index; } }
        private string m_CaptionKey;
        /// <summary>
        /// get or set the caption key of this item
        /// </summary>
        public string CaptionKey
        {
            get {
                if (string.IsNullOrEmpty(this.m_CaptionKey))
                    return this.Name;
                return m_CaptionKey; }
            set
            {
                if (m_CaptionKey != value)
                {
                    m_CaptionKey = value;
                }
            }
        }
        private bool m_SeparatorBefore;
        private bool m_SeparatorAfter;
        public bool SeparatorAfter
        {
            get { return m_SeparatorAfter; }
            set
            {
                if (m_SeparatorAfter != value)
                {
                    m_SeparatorAfter = value;
                }
            }
        }
        public bool SeparatorBefore
        {
            get { return m_SeparatorBefore; }
            set
            {
                if (m_SeparatorBefore != value)
                {
                    m_SeparatorBefore = value;
                }
            }
        }
        public CoreContextMenuAttribute(string name, int index):base(name )
        {            
            this.m_index = index ;
            this.m_CaptionKey = name;
        }
    }
}

