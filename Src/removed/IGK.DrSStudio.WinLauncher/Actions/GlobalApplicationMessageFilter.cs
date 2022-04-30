

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GlobalApplicationMessageFilter.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:GlobalApplicationMessageFilter.cs
*/
using IGK.ICore;using IGK.DrSStudio.Menu;
using IGK.DrSStudio.Actions;
using IGK.DrSStudio.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinLauncher.Actions
{
    public sealed class GlobalApplicationMessageFilter : IGK.DrSStudio.Actions.ICoreFilterMessageAction 
    {
        Dictionary<Keys, IGlobalLayoutAction> m_actions;
        WinLauncherLayoutManager m_lmanager;
        public GlobalApplicationMessageFilter (WinLauncherLayoutManager lmanager)
	    {
            this.m_actions = new Dictionary<Keys, IGlobalLayoutAction>();
            this.m_lmanager = lmanager;
	    }
        public bool Contains(Keys k)
        {
            if (k == Keys.None) return false;
            return m_actions.ContainsKey(k);
        }
        public void Add(Keys key, IGlobalLayoutAction action)
        {
            if ((key != Keys.None) && !Contains(key) && (action != null))
            {
                this.m_actions.Add (key, action);
            }
        }
        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (this.m_actions.Count == 0)
                return false;
            try
            {
                Keys c = Keys.None;
                if (Environment.Is64BitOperatingSystem)
                { 
                    long l = m.WParam.ToInt64 ();
                    c = (Keys)l | Control.ModifierKeys;
                }
                else 
                    c = (Keys)m.WParam.ToInt32() | Control.ModifierKeys;
                switch (m.Msg)
                {
                    case User32.WM_KEYDOWN:
                        if (this.Contains(c))
                        {//only do action                        
                            return this.m_actions[c].DoAction(enuKeyState.KeyDown);
                        }
                        return false;
                    case User32.WM_KEYUP:
                        if (this.Contains(c))
                        {
                            return this.m_actions[c].DoAction(enuKeyState.KeyUp);
                        }
                        break;
                }
            }
            catch 
            {
                CoreLog.WriteDebug("Exception on : " + m.Msg);
            }
            return false;
        }
        internal IGlobalLayoutAction GetAction(Keys keys)
        {
            if (this.Contains(keys))
                return this.m_actions[keys];
            return null;
        }
    }
}

