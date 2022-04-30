

/*
IGKDEV @ 2008-2016
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
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GlobalApplicationMessageFilter.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
using IGK.ICore.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore;
using IGK.ICore.Windows.Native;
namespace IGK.DrSStudio.Actions
{
    [Serializable()]
    public sealed class GlobalApplicationMessageFilter : ICoreFilterMessageAction 
    {
        Dictionary<enuKeys, IGlobalLayoutAction> m_actions;
        ICoreWorkbenchLayoutManager m_lmanager;

        public int Priority { get { return -10; } }
        
        public GlobalApplicationMessageFilter(ICoreWorkbenchLayoutManager lmanager)
	    {
            this.m_actions = new Dictionary<enuKeys, IGlobalLayoutAction>();
            this.m_lmanager = lmanager;
	    }
        public bool Contains(enuKeys k)
        {
            if (k == enuKeys.None) return false;
            return m_actions.ContainsKey(k);
        }
        public void Add(enuKeys key, IGlobalLayoutAction action)
        {
            if ((key != enuKeys.None) && !Contains(key) && (action != null))
            {
                this.m_actions.Add (key, action);
            }
        }
        public bool PreFilterMessage(ref ICoreMessage m)
        {
            if (this.m_actions.Count == 0)
                return false;
            try
            {
                enuKeys c = enuKeys.None;
                enuKeys modkey = CoreApplicationManager.Application.ControlManager.ModifierKeys;
                if (Environment.Is64BitOperatingSystem)
                {
                    long l = m.WParam.ToInt64();
                    c = (enuKeys)l | modkey;
                }
                else
                    c = (enuKeys)m.WParam.ToInt32() | modkey;
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
            catch (Exception ex)
            {
                CoreLog.WriteDebug("Exception on : " + m.Msg + " "+ex.Message);
            }
            return false;
        }
        internal IGlobalLayoutAction GetAction(enuKeys keys)
        {
            if (this.Contains(keys))
                return this.m_actions[keys];
            return null;
        }
    }
}

