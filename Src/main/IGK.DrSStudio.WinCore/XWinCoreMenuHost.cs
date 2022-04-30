

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XWinCoreMenuHost.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XWinCoreMenuHost.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent the win core menu host
    /// </summary>
    class XWinCoreMenuHost : IGKXUserControl, ICoreMenuHostControl
    {
        ICoreMenu m_menu;
        public XWinCoreMenuHost()
        {
            this.Size = new System.Drawing.Size(100, 0);
            this.AutoSize = true;
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        public ICoreMenu Menu
        {
            get { return m_menu; }
        }
        public void Add(ICoreMenu menu)
        {
            m_menu = menu;
            this.Controls.Add(menu as Control);
        }
        public void Remove(ICoreMenu menu)
        {
            if (this.m_menu == menu)
            {
                this.Controls.Remove(menu as Control);
            }
        }
        public virtual ICoreMenu CreateMenu()
        {
            return new IGKXWinCoreMenu();
        }
    }
}

