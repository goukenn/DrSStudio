

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FBToolBar.cs
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
file:FBToolBar.cs
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
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing3D.FileBrowser.WinUI
{
    
using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.Resources;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent a file bar addresss
    /// </summary>
    public class FBToolBar : IGKXControl,
        IFBDirectoryBrowser, 
        IFBAddressBar
    {
        IGKXButton c_browsedir;
        IGKXButton c_edit;
        IGKXButton c_refresh;
        IGKXButton c_fullScreen;
        IGKXButton c_setting;
        protected override System.Drawing.Size DefaultMinimumSize
        {
            get
            {
                return new System.Drawing.Size(100, 32);
            }
        }
        private string m_SelectedFolder;
        public string SelectedFolder
        {
            get { return m_SelectedFolder; }
            set
            {
                if (m_SelectedFolder != value)
                {
                    m_SelectedFolder = value;
                    OnSelectedFolderChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler SelectedFolderChanged;
        protected virtual void OnSelectedFolderChanged(System.EventArgs e)
        {
            if (SelectedFolderChanged != null)
                SelectedFolderChanged(this, e);
        }
        FBControlSurface m_surface;
        public FBToolBar(FBControlSurface surface)
        {
            m_surface = surface;
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw , true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.Paint += _Paint;
            this.c_browsedir = new IGKXButton();
            this.c_edit = new IGKXButton();
            this.c_setting = new IGKXButton();
            this.c_refresh = new IGKXButton();
            this.c_fullScreen = new IGKXButton();
            this.c_browsedir.Click += new EventHandler(c_browsedir_Click);
            this.c_edit.Click += new EventHandler(c_edit_Click);
            this.c_setting.Click += new EventHandler(c_setting_Click);
            this.c_refresh.Click += c_refresh_Click;
            this.c_fullScreen.Click += c_fullScreen_Click;
            this.c_browsedir.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(FBConstant.BTN_FOLDER));
            this.c_edit.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(FBConstant.BTN_EDIT ));
            this.c_refresh.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(FBConstant.BTN_REFRESH ));
            this.c_setting.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(FBConstant.BTN_SETTING ));
            this.c_fullScreen.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(FBConstant.BTN_FULLSCREEN));
            this.Controls.AddRange(new Control[] { 
                this.c_browsedir ,
                this.c_edit ,
                this.c_refresh , 
              //  this.c_fullScreen ,
                this.c_setting 
            });
            InitControlBound();
        }

      
        void c_fullScreen_Click(object sender, EventArgs e)
        {
          //  this.m_surface.ToogleFullScreen();
        }
        void c_refresh_Click(object sender, EventArgs e)
        {
            //reload file list
            this.m_surface.ReloadFileList();
        }
        void c_setting_Click(object sender, EventArgs e)
        {
            this.m_surface.Mecanism.Actions[enuKeys.Control | enuKeys.Shift | enuKeys.P].DoAction();
        }
        private void InitControlBound()
        {
            int x = 2;
            int y = 2;
            int h = this.Height - 4;
            int w = h;
            foreach (Control item in this.Controls)
            {
                IGKXButton c = item as IGKXButton;
                if (c.Visible)
                {
                    c.Bounds = new Rectangle(x, y, w, h);
                    x += w + 1;
                }
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            this.InitControlBound();
            base.OnSizeChanged(e);
        }
        void c_browsedir_Click(object sender, EventArgs e)
        {
            this.m_surface.Mecanism.Actions[enuKeys.Control | enuKeys.D].DoAction();
        }
        void c_edit_Click(object sender, EventArgs e)
        {
            this.m_surface.Mecanism.Actions[enuKeys.Control | enuKeys.O].DoAction();
        }
        private void _Paint(object sender, CorePaintEventArgs e)
        {
         
        }
        void _Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
        }
    }
}

