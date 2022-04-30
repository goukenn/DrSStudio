

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMenuAttribute.cs
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
file:CoreMenuAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Menu
{
    using IGK.ICore;using IGK.ICore.Actions;
    using IGK.ICore.WinUI;
    [AttributeUsage (AttributeTargets.Class,
        AllowMultiple = false, Inherited = false)]
    public class CoreMenuAttribute : CoreActionAttribute 
    {
        private int m_index;
        private enuKeys m_Keys;
        private bool m_SeparatorBefore;
        private bool m_SeperatorAfter;
        private string m_ShortcutText;
        private bool m_IsVisible;
        private string m_CaptionKey;
        private bool m_useShortcut;
        private bool m_IsShortcutMenuChild;
        private Type m_ShortCutMenuContainerTypeTool;
        /// <summary>
        /// get or set the shortcut menu container type tool
        /// </summary>
        public Type ShortCutMenuContainerTypeTool
        {
            get { return m_ShortCutMenuContainerTypeTool; }
            set
            {
                if (m_ShortCutMenuContainerTypeTool != value)
                {
                    m_ShortCutMenuContainerTypeTool = value;
                }
            }
        }
        /// <summary>
        /// get or set if this menu is shorcut menu child
        /// </summary>
        public bool IsShortcutMenuChild
        {
            get { return m_IsShortcutMenuChild; }
            set
            {
                if (m_IsShortcutMenuChild != value)
                {
                    m_IsShortcutMenuChild = value;
                }
            }
        }
        /// <summary>
        /// get of set if the system need  to use the shortcut
        /// </summary>
        public bool UseShortcut {
            get { return this.m_useShortcut; }
            set { this.m_useShortcut = value; }
        }
        /// <summary>
        /// Get or set the caption keys
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
        /// <summary>
        /// get or set if this menu is visible to user
        /// </summary>
        public bool IsVisible
        {
            get { return m_IsVisible; }
            set
            {
                    m_IsVisible = value;
            }
        }
        /// <summary>
        /// get or set the shortcut text
        /// </summary>
        public string ShortcutText
        {
            get { return m_ShortcutText; }
            set
            {
                    m_ShortcutText = value;
            }
        }
        /// <summary>
        /// get or set if a separator must be added after this menu
        /// </summary>
        public bool SeparatorAfter
        {
            get { return m_SeperatorAfter; }
            set
            {     m_SeperatorAfter = value;
            }
        }
        /// <summary>
        /// get or set if a seperator must be added before this menu
        /// </summary>
        public bool SeparatorBefore
        {
            get { return m_SeparatorBefore; }
            set
            {
                    m_SeparatorBefore = value;
            }
        }
        /// <summary>
        /// get or set the shortcut
        /// </summary>
        public enuKeys Shortcut
        {
            get { return m_Keys; }
            set
            {
                    m_Keys = value;
            }
        }
        /// <summary>
        /// get or set the index of the menu in the hierarchi
        /// </summary>
        public int Index { get { return this.m_index; } set { this.m_index = value; } }

        /// <summary>
        /// get or set if this property will be registered in default action. default is false
        /// </summary>
        public bool IsNoRegistered { get; set; }

        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="name">unique identifier name of the menu</param>
        /// <param name="index">default index of the menu in the hierarchie</param>
        public CoreMenuAttribute(string name, int index):base(name)
        {            
            this.m_index = index;
            this.m_IsVisible = true;
            this.m_useShortcut = true;
            this.m_CaptionKey = string.Format(CoreConstant.MENU_FORMAT, name);
        }
        public void RegisterShortCutMenu(ICoreMenuAction menu)
        { 
            Type t = this.ShortCutMenuContainerTypeTool ;
            if ((t==null) || (menu == null ))return ;
            ICoreMenuMessageShortcutContainer c  = t.GetProperty ("Instance").GetValue (null)
                as ICoreMenuMessageShortcutContainer;
            if (c != null)
                c.Register(menu);
        }
    }
}

