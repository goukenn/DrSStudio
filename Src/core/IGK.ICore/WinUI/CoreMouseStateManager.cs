

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMouseStateManager.cs
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
file:CoreMouseStateManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a mouse state manager
    /// </summary>
    public static class CoreMouseStateManager
    {
        class CoreMouseStateItem 
        {
            ICoreMouseStateObject m_item;
            public CoreMouseStateItem(ICoreMouseStateObject item){ 
                this.m_item = item;
                this.m_item.Disposed += m_item_Disposed;
                this.m_item.MouseEnter += m_item_MouseEnter;
                this.m_item.MouseLeave += m_item_MouseLeave;
                this.m_item.MouseDown += m_item_MouseEnter;
            }
            void m_item_MouseLeave(object sender, EventArgs e)
            {
                this.m_item.MouseState = enuMouseState.None;
            }
            void m_item_MouseEnter(object sender, EventArgs e)
            {
                this.m_item.MouseState = enuMouseState.Hover;
            }
            void m_item_Disposed(object sender, EventArgs e)
            {
                this.m_item.Disposed -= m_item_Disposed;
                UnRegister(this.m_item);
            }
        }
     
        
        static Dictionary<ICoreMouseStateObject, CoreMouseStateItem> sm_register;
        static CoreMouseStateManager() {
            sm_register = new Dictionary<ICoreMouseStateObject, CoreMouseStateItem>();
        }
        public static void Register(ICoreMouseStateObject obj)
        {
            if ( (obj!=null) && !sm_register.ContainsKey(obj))
            {
                sm_register.Add(obj, new CoreMouseStateItem(obj));
                obj.Disposed += obj_Disposed;
            }
        }

        static void obj_Disposed(object sender, EventArgs e)
        {
            UnRegister(sender as ICoreMouseStateObject);
        }
        public static void UnRegister(ICoreMouseStateObject obj)
        {
            if (sm_register.ContainsKey(obj))
            {
                sm_register.Remove(obj);
            }
        }
    }
}

