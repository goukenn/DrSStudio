

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreApplicationSetting.cs
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
file:CoreApplicationSetting.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO; 
namespace IGK.ICore.Settings
{
    using IGK.ICore;using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Resources;
    /// <summary>
    /// Represent the base application setting
    /// </summary>
    [CoreAppSetting (Name =CoreConstant.APP_SETTING)]
    public class CoreApplicationSetting : CoreSettingBase 
    {
        private static CoreApplicationSetting sm_instance;
        public event EventHandler LangReloaded;
        /// <summary>
        /// raise the lang reloaded event
        /// </summary>
        internal void OnLangReloaded()
        {
            if (this.LangReloaded != null)
                this.LangReloaded(this, EventArgs.Empty);
        }
        private CoreApplicationSetting()
        {
            //init default 
            this.Add("TempFolder", "%startup%/Temp", null);
            this.Add("Lang", "Fr", new EventHandler(LangChanged));
            this.Add("LangFolder", "%startup%/Lang", null);
            this.Add("AddInFolder", "%startup%/AddIn", null);
            this.Add("SkinsFolder", "%startup%/Skins", null);
            this.Add("AllowSaveToRegistry", false, null);
            this.Add("SaveBothGkdsFileAndOther", true, null);
#if DEBUG
            this.Add("ShowMenuIndex", true, (o,e) => { 
                OnSettingChanged(new CoreSettingChangedEventArgs(this["ShowMenuIndex"]));
            });
#endif

            this.Initialize = false;
            CoreApplicationManager.ApplicationExit += _ApplicationExit;
        }

        public bool AllowSaveToRegistry
        {
            get { return Convert.ToBoolean ( this["AllowSaveToRegistry"].Value) ; }
            set
            {
                this["AllowSaveToRegistry"].Value = value;
            }
        }
        public bool SaveBothGkdsFileAndOther
        {
            get { return Convert.ToBoolean(this["SaveBothGkdsFileAndOther"].Value); }
            set
            {
                this["SaveBothGkdsFileAndOther"].Value = value;
            }
        }
        private void LangChanged(object o, EventArgs e)
        {
            if (SettingManager.CoreSystem != null)
            {
                CoreLog.WriteDebug ($"Lang changed : {this.Lang}" );
				CoreResources.ReloadString();
                OnLangReloaded();
            }
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterGroup group =  parameters.AddGroup("Folder");
            group.AddItem("TempFolder", "TempFolder", this.TempFolder, enuParameterType.Text, settingChanged);
            group.AddItem("Lang", "Lang", this.Lang , enuParameterType.Text, LangChanging);
            group.AddItem("AddInForder", "AddInFolder", this.AddInFolder, enuParameterType.Text, settingChanged);
            group.AddItem("SkinsFolder", "SkinsFolder", this.SkinsFolder, enuParameterType.Text, settingChanged);

            group = parameters.AddGroup("Options");
            group.AddItem("AllowSaveToRegistry", "AllowSaveToRegistry", this.AllowSaveToRegistry , enuParameterType.Bool, settingChanged);
            group.AddItem("SaveBothGkdsFileAndOther", "SaveBothGkdsFileAndOther", this.SaveBothGkdsFileAndOther, enuParameterType.Bool, settingChanged);
            #if DEBUG
            group.AddItem("ShowMenuIndex", "ShowMenuIndex", this.ShowMenuIndex, enuParameterType.Bool, settingChanged);
            #endif
            return parameters;
        }
        void LangChanging(object sender,CoreParameterChangedEventArgs e)
        {
            this["Lang"].Value = e.Value;
        }
        void settingChanged(object sender, CoreParameterChangedEventArgs e)
        {
            OnSettingChanged(e);            
        }
        public static CoreApplicationSetting Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreApplicationSetting()
        {
            sm_instance = new CoreApplicationSetting();
        }
        void _ApplicationExit(object sender, EventArgs e)
        {
            ClearTempFolder();
        }
        private static void ClearTempFolder()
        {
            string path = IO.PathUtils.GetStartupFullPath(Instance.TempFolder);
            try
            {
                if (Directory.Exists(path))
                {
                    IO.PathUtils.EmptyDirectory(path);
                }
            }
            catch { 
            }
        }
        /// <summary>
        /// get the startup folder
        /// </summary>
        public string StartUpFolder {
            get { return CoreApplicationManager.StartupPath; }
        }
        /// <summary>
        /// get the tempory folder
        /// </summary>
        public bool Initialize 
        {
            get
            {
                return (bool)this["Initialize"].Value;
            }
            internal set {
                this["Initialize"].Value = value;
            }
        }
        /// <summary>
        /// get the tempory folder
        /// </summary>
        public string TempFolder {
            get {
                return (string)this["TempFolder"].Value;
            }
        }
        /// <summary>
        /// get the tempory folder
        /// </summary>
        public string AddInFolder
        {
            get
            {
                return (string)this["AddInFolder"].Value;
            }
        }
        /// <summary>
        /// get the language folder
        /// </summary>
        public string SkinsFolder
        {
            get
            {
                return (string)this["SkinsFolder"].Value;
            }
        }
        /// <summary>
        /// get the language folder
        /// </summary>
        public string LangFolder
        {
            get
            {
                return (string)this["LangFolder"].Value;
            }
        }
        public string Lang {
            get
            {
                return (string)this["Lang"].Value;
            }
        }
        protected internal override void InitDefaultProperty(System.Reflection.PropertyInfo prInfo, CoreSettingDefaultValueAttribute attrib)
        {
        }
        #if DEBUG
        public bool ShowMenuIndex
        {
            get {
                return (bool)this["ShowMenuIndex"].Value;
            }
            set { this["ShowMenuIndex"].Value =value; } 
        }
        #endif
    }
}

