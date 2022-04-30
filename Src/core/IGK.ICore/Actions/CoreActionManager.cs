

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreActionManager.cs
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
using System.Threading.Tasks;

namespace IGK.ICore.Actions
{
    /*
     * manage all actions register performed action. to call last action
     * 
     * 
     */
    /// <summary>
    /// represent a core action manager.
    /// </summary>
    public class CoreActionManager
    {
        private static CoreActionManager sm_instance;
        private ICoreAction m_lastAction;
        private List<ICoreAction> m_actions;

        private CoreActionManager()
        {
            m_actions = new List<ICoreAction>();
        }

        public static CoreActionManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreActionManager()
        {
            sm_instance = new CoreActionManager();
        }
        /// <summary>
        /// register action 
        /// </summary>
        /// <param name="action"></param>
        public static void RegisterAction(ICoreAction action)
        {
            if ((action != null) && !Instance.m_actions.Contains(action))
            {
                action.ActionPerformed += Instance._ActionPerformed;
                Instance.m_actions.Add(action);
            }
        }
        /// <summary>
        /// unregister action
        /// </summary>
        /// <param name="action"></param>
        public static void UnRegisterAction(ICoreAction action)
        {
            if ((action != null) && (Instance.m_actions.Contains(action)))
            {
                action.ActionPerformed -= Instance._ActionPerformed;
                Instance.m_actions.Remove(action);
            }
        }

        void _ActionPerformed(object sender, EventArgs e)
        {
            this.m_lastAction = (ICoreAction)sender;
        }

        public static void PerformLastAction()
        {
            if (Instance.m_lastAction != null)
            {
                Instance.m_lastAction.DoAction();
            }
        }
        public static ICoreAction LastAction {
            get {
                return Instance.m_lastAction;
            }
        }
    }
}
