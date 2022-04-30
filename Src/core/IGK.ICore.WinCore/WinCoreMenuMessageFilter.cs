

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMenuMessageFilter.cs
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
file:CoreMenuMessageFilter.cs
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

namespace IGK.ICore.WinCore
{
    using IGK.ICore.WinCore;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.Actions;
    using IGK.ICore.Menu;
    using IGK.ICore.Resources;
    using IGK.ICore.Tools;

    /// <summary>
    /// used to operate in a system menu
    /// </summary>
    public sealed class WinCoreMenuMessageFilter:
        ICoreMessageFilter ,
        ICoreFilterMessageAction ,
        ICoreFilterToolMessage
    {
        private ICoreMenuMessageShortcutContainer m_owner;
        private bool m_waitforkey;
        private bool m_isControl;
        private bool m_isShift;
        private bool m_isAlt;
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_CHAR = 0x0102;
        internal const int VK_CONTROL = 0x11;
        internal const int VK_SHIFT = 0x10;
        internal const int VK_ALT = 0x12;
        ICoreWorkbenchMessageFilter m_workbench;
        char m_startLetter;
        int m_primarykey;

        public int Priority { get { return 0; } }
        public ICoreWorkbenchMessageFilter Workbench
        {
            get
            {
                return m_workbench;
            }
        }
        /// <summary>
        /// get or set if the message will wait for key
        /// </summary>
        public bool WaitForKey
        {
            get { return this.m_waitforkey; }
        }
        public char Letter
        {
            get
            {
                return this.m_startLetter;
            }
        }
        public WinCoreMenuMessageFilter(ICoreWorkbenchMessageFilter workbench,
            ICoreMenuMessageShortcutContainer owner, char letter)
        {
            this.m_owner = owner;
            this.m_workbench = workbench ?? throw new ArgumentNullException(nameof(workbench));
            this.m_startLetter = letter;
            this.m_waitforkey = false;
            if (char.IsLetter(this.m_startLetter))
            {
                m_primarykey = (int)this.m_startLetter - 64;
            }
        }
        public bool PreFilterMessage(ref ICoreMessage m)
        {
            bool v_result = false;
            switch (m.Msg)
            {
                case WM_KEYDOWN:
                    v_result = KeyDown(ref m);
                    break;
                case WM_KEYUP:
                    v_result = KeyUp(ref m);
                    break;
                case WM_CHAR:
                    //key char equivalent of key pres
                    v_result = KeyChar(ref m);
                    break;
            }
            return v_result;
        }
        private bool KeyUp(ref ICoreMessage m)
        {
            enuKeys c =  CoreApplicationManager.Application.ControlManager.ModifierKeys;
            this.m_isControl = ((c & enuKeys.Control) == enuKeys.Control);
            this.m_isShift = ((c & enuKeys.Shift) == enuKeys.Shift);
            this.m_isAlt = ((c & enuKeys.Alt) == enuKeys.Alt);
            int ik = m.WParam.ToInt32();
            switch (ik)
            {
                case VK_CONTROL:
                    return true;
                case VK_SHIFT:
                    return false;
                case VK_ALT:
                    return false;
            }
            enuKeys k = (enuKeys)char.ToUpper((char)(byte)ik);
            if (m_isControl)
                k |= enuKeys.Control;
            if (m_isShift)
                k |= enuKeys.Shift;
            if (m_isAlt)
                k |= enuKeys.Alt;
            if (!this.WaitForKey)
            {//wait for first key up event to wait for another key
                this.m_waitforkey = true;
                return true;
            }
            else
            {
                FilteringKey(k);
                return true;
            }
        }
        ICoreHelpWorkbench HelpBench {
            get {
                return Workbench as ICoreHelpWorkbench;
            }
        }
        private void FilteringKey(enuKeys k)
        {
            var h = HelpBench;
            m_waitforkey = false;
            m_isControl = false;
            m_isShift = false;
            if (this.Contains(k))
            {
                h.OnHelpMessage (string.Empty);
                this.EndFilter();
                this.Call(k);
            }
            else
            {
                h.OnHelpMessage(string.Format(CoreSystem.GetString("Help.DemandForTool.NotDefine",
                      " (Ctrl+" + this.m_startLetter.ToString().ToUpper() + "),(" + CoreResources.GetShortcutText(k)+ ")")
                    ));
            }
            this.m_owner.EndFilter();
        }
        private bool KeyDown(ref ICoreMessage m)
        {
            return true;
        }
        private bool KeyChar(ref ICoreMessage m)
        {
            enuKeys modKey = CoreApplicationManager.Application.ControlManager.ModifierKeys;
            this.m_isControl = ((modKey & enuKeys.Control) == enuKeys.Control);
            this.m_isShift = ((modKey & enuKeys.Shift) == enuKeys.Shift);
            this.m_isAlt = ((modKey & enuKeys.Alt) == enuKeys.Alt);
            int ik = m.WParam.ToInt32();
            enuKeys k = (enuKeys)char.ToUpper((char)(byte)ik);
            if (this.WaitForKey)
            {
                FilteringKey(k);
                return true;
            }
            return false;
        }
        private bool Contains(enuKeys k)
        {
            return this.m_owner.Contains(k);
        }
        private void Call(enuKeys k)
        {
            //stop filting before call keys actions
            this.m_owner.EndFilter();
            //call the action
            this.m_owner.Call(k);
        }
        public void EndFilter()
        {///end filtering
            this.m_waitforkey = false;
            //this.MainForm.AppMenu.Enabled = true;            
            CoreActionRegisterTool.Instance.RemoveFilterMessage(this);
        }
        public void StartFiltering()
        {//start filteing message
            this.HelpBench.OnHelpMessage(CoreSystem.GetString("Help.DemandForTool", "Ctrl+" + this.m_startLetter.ToString().ToUpper()));            
            CoreActionRegisterTool.Instance.AddFilterMessage(this);
        }

        public static void RemoveFilter(ICoreMessageFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}

