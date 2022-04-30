

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ViewToggleFullScreen.cs
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
file:_ViewToggleFullScreen.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.Menu;
namespace IGK.DrSStudio.Menu.View
{
    /// <summary>
    /// display view mode
    /// </summary>
    enum enuViewMode
    { 
        Normal, 
        FullScreen
    }
    [DrSStudioMenu("View.ToggleFullscreen", -80, IsVisible=true , Shortcut= enuKeys.F11) ]
    class _ViewToggleFullScreen : CoreApplicationMenu 
    {
        enuViewMode m_viewMode;
        public _ViewToggleFullScreen():base()
        {
        }
        protected override bool PerformAction()
        {
            if (this.MainForm !=null)
            {
            Form  frm = (Form ) MainForm ;
            if (MainForm.FullScreen)
            {
                MainForm.FullScreen = false;
                frm.WindowState = FormWindowState.Normal;
                m_viewMode = enuViewMode.Normal;
            }
            else 
            {
                MainForm.FullScreen = true;
                if (m_viewMode == enuViewMode.Normal)
                {
                    frm.WindowState = FormWindowState.Maximized;
                }
            }         
            }
            return true;
        }
    }
}

