

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinLauncherMainFormSetting.cs
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
file:WinLauncherMainFormSetting.cs
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
using System.Windows.Forms ;
using System.Drawing ;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.Settings;
    using IGK.DrSStudio.WinLauncher;
    /// <summary>
    /// represent a mainform setting used to store application global setting on the file system.
    /// </summary>
    [CoreAppSetting(Name = "MainFormSetting")]
    sealed class WinLaucherMainFormSetting : CoreSettingBase 
    {
        private static WinLaucherMainFormSetting sm_instance;
        private WinLaucherMainFormSetting()
        {
            this.Location = Vector2i.Zero;
            this.Size = Size2i.Empty;
            this.WindowsState = FormWindowState.Normal;
        }
        public static WinLaucherMainFormSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WinLaucherMainFormSetting()
        {
            sm_instance = new WinLaucherMainFormSetting();   
        }
        protected override void InitDefaultProperty(System.Reflection.PropertyInfo prInfo, IGK.DrSStudio.Settings.CoreSettingDefaultValueAttribute attrib)
        {            
            base.InitDefaultProperty(prInfo, attrib);
        }
        public Vector2i Location {
            get {
                return (Vector2i)this["Location"].Value ;
            }
            set {
                this["Location"].Value = value;
            }
        }
        public Size2i Size
        {
            get
            {
                return (Size2i)this["Size"].Value;
            }
            set
            {
                this["Size"].Value = value;
            }
        }
        [CoreSettingDefaultValue (FormWindowState.Normal )]
        public FormWindowState WindowsState {
            get {
                return (FormWindowState)this["WindowsState"].Value;
            }
            set {
                this["WindowsState"].Value = value;
            }
        }
        internal void Bind(WinLaucherMainForm mainForm)
        {
            if (!this.Size.Equals(Size2i.Empty))
            {
                mainForm.Size = this.Size;
            }
            //set the start position
            mainForm.StartPosition = FormStartPosition.Manual;
            //-------------------------------------------------------
            //check for screen 
            //-------------------------------------------------------
            Screen v_screen = Screen.FromPoint(this.Location);
            if ((v_screen == null) ||(!v_screen.Bounds.Contains (this.Location )))
            {
                v_screen = Screen.PrimaryScreen;
                //center to screen
                mainForm.Location = 
                     new Vector2i( Math.Max ((v_screen.WorkingArea.Width -  mainForm .Size.Width )/ 2, 0), 
                Math.Max ( (v_screen.WorkingArea.Height -mainForm .Size.Height ) / 2, 0));
                mainForm.WindowState = this.WindowsState;
            }
            else {            
                mainForm.Location = this.Location;
                mainForm.WindowState = this.WindowsState;
            }
            mainForm.LocationChanged += new EventHandler(mainForm_LocationChanged);
            mainForm.SizeChanged += new EventHandler(mainForm_SizeChanged);
        }
        void mainForm_SizeChanged(object sender, EventArgs e)
        {
            WinLaucherMainForm m = (sender as WinLaucherMainForm);
            if (m.WindowState == FormWindowState.Normal )
                this.Size = m.Size;
            this.WindowsState = m.WindowState;
        }
        void mainForm_LocationChanged(object sender, EventArgs e)
        {
            this.Location = (Vector2i ) (sender as IGK.DrSStudio.WinLauncher .WinLaucherMainForm ).Location;
        }
    }
}

