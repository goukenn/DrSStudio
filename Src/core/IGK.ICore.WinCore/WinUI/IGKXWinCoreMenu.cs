

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXWinCoreMenu.cs
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
file:XWinCoreMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.ICore.WinUI
{
    using IGK.ICore.Menu;
    using IGK.ICore.Settings;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinCore.WinUI;
    using IGK.ICore.Drawing2D;

    public class IGKXWinCoreMenu : MenuStrip, ICoreMenu
    {
        private CoreFont m_font;

        /// <summary>
        /// get or set the core font of the menu host
        /// </summary>
        public CoreFont CoreFont {
            get {
                return m_font;
            }
            set {
                if ((m_font != null) && (m_font != value )) {
                    m_font.FontDefinitionChanged -= FontDefChanged;
                }
                m_font = value;
                if (m_font != null)
                {
                    m_font.FontDefinitionChanged += FontDefChanged;
                    this.Font = m_font.ToGdiFont();

                }
            }
        }

        private void FontDefChanged(object sender, EventArgs e)
        {
            this.Font = this.CoreFont.ToGdiFont();
        }

        public IGKXWinCoreMenu()
        {
            this.Renderer = new XWinCoreMenuRenderer();
            this._initFont();
        }
       
        

        

        private void _initFont()
        {
            //initialize font
            this.CoreFont = WinCoreControlRenderer.MenuFont;           
        }

        private void IGKXWinCoreMenu_FontChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Menu Font Changed ");
        }

        public void Add(CoreMenuActionBase menu)
        {
            this.Items.Add(menu.MenuItem as ToolStripMenuItem);
        }
        public void Remove(CoreMenuActionBase menu)
        {
            this.Items.Remove(menu.MenuItem as ToolStripMenuItem);
        }
        public int Count
        {
            get { return this.Items.Count; }
        }
        
    }
}

