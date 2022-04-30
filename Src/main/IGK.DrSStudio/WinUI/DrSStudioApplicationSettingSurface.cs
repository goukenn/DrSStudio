

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioApplicationSettingSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using IGK.ICore.Settings;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.WinUI;

namespace IGK.DrSStudio.WinUI
{
    
    /// <summary>
    /// represent application setting control used to setup the DrSStudio system service core
    /// </summary>
    [CoreWorkingObject (CoreConstant.GUI_APP_SETTINGCONTROL)]
    sealed class DrSStudioApplicationSettingSurface : IGKXUserControl 
    {
        private IGKXPanel xPanel2;
        private IGKXSplitter xSplitter1;
        private IGKXExpenderBox c_settingExpenderBox;
        CoreWorkingApplicationSetting item;

        public DrSStudioApplicationSettingSurface()
        {
            this.InitializeComponent();
        }
        public DrSStudioApplicationSettingSurface(ICoreWorkingConfigurableObject obj)
            : this()
        {
            item = obj as CoreWorkingApplicationSetting;
            if (item == null)
                throw new CoreException("item is null");

            this.Dock = DockStyle.Fill;
            this.Load += new EventHandler(_Load);
            this.c_settingExpenderBox.MinimumSize = new Size(200, 100);
        }

        void c_settingExpenderBox_SelectedItemChanged(object sender, EventArgs e)
        {
      
        }

        private void Configure(ICoreSetting s)
        {
            switch (s.GetConfigType())
            {
                case enuParamConfigType.CustomControl :
                    Control ctr = s.GetConfigControl() as Control;
                    if (ctr != null)
                    {
                        ctr.Dock = DockStyle.Fill;
                        this.xPanel2.Controls.Clear();
                        this.xPanel2.Controls.Add(ctr);
                    }
                    break;
                case enuParamConfigType.ParameterConfig :
                    this.xPanel2.Controls.Clear();
                    CoreSystem.GetWorkbench().BuildWorkingProperty(
                           this.xPanel2, 
                        s);
                    break;
            }
            this.c_settingExpenderBox.SelectedGroup = this.c_settingExpenderBox.Groups[s.GroupName];

        }

        void _Load(object sender, EventArgs e)
        {
            ICoreSetting v_setting = null;
           // this.c_settingExpenderBox.Renderer = new XApplicationSettingRenderer(this.c_settingExpenderBox);
            Dictionary<string, IGKXExpenderBoxGroup> v_traited = new Dictionary<string, IGKXExpenderBoxGroup>();
            
           CoreSettingCollections v_settings =   CoreSettings.Settings;
           string[] keys = v_settings.SortKeys();
             IGKXExpenderBoxGroup group = null;
            IGKXExpenderBoxGroupItem v_xitem = null;
            string v_namegroup = "";
           foreach (string item in keys)
           {

               v_setting = v_settings[item];
               if (v_setting.GetConfigType() == enuParamConfigType.NoConfig)
               {
                   CoreLog.WriteLine("No config for : " + v_setting.Id);
                   continue;
               }
               v_namegroup = v_setting.GroupName;
               
                if (!v_traited.ContainsKey (v_namegroup))
                {
                    group = this.c_settingExpenderBox.AddGroup(v_setting.Id);
                    group.Name = v_namegroup;
                    group.ImageKey = v_setting.ImageKey;
                    group.Index = v_setting.GroupIndex;
                    group.CaptionKey = "title."+v_namegroup;
                    v_traited.Add(v_namegroup, group);
                }
                else {
                    group =  v_traited [v_namegroup];
                }
             
               v_xitem = new IGKXExpenderBoxGroupItem()
               {
                   Tag = v_setting,
                   Index = v_setting.Index,
                   CaptionKey = v_setting.Id 
               };
               group.Items.Add(v_xitem);
               v_xitem.Click +=v_xitem_Click;
            }
           this.c_settingExpenderBox.Sort(new DrSStudioApplicationSettingComparer());
            
        }

        void v_xitem_Click(object sender, EventArgs e)
        {
            ICoreSetting s =((Control)sender).Tag as ICoreSetting;
            if (s != null)
            {
                this.Configure(s);
            }
        }

     


        private void InitializeComponent()
        {
            this.c_settingExpenderBox = new IGKXExpenderBox();
            this.xPanel2 = new IGKXPanel();
            this.xSplitter1 = new IGKXSplitter();
            this.SuspendLayout();
            // 
            // c_settingExpenderBox
            // 
            this.c_settingExpenderBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.c_settingExpenderBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_settingExpenderBox.Location = new System.Drawing.Point(0, 0);
            this.c_settingExpenderBox.MinimumSize = new System.Drawing.Size(200, 32);
            this.c_settingExpenderBox.Name = "c_settingExpenderBox";
            this.c_settingExpenderBox.Padding = new System.Windows.Forms.Padding(4);                        
            this.c_settingExpenderBox.Size = new System.Drawing.Size(200, 408);
            this.c_settingExpenderBox.TabIndex = 0;
            // 
            // xPanel2
            // 
            this.xPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.xPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanel2.Location = new System.Drawing.Point(205, 0);
            this.xPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.xPanel2.Name = "xPanel2";
            this.xPanel2.Size = new System.Drawing.Size(457, 408);
            this.xPanel2.TabIndex = 1;
            // 
            // xSplitter1
            // 
            this.xSplitter1.Location = new System.Drawing.Point(200, 0);
            this.xSplitter1.Name = "xSplitter1";
            this.xSplitter1.Size = new System.Drawing.Size(5, 408);
            this.xSplitter1.TabIndex = 2;
            this.xSplitter1.TabStop = false;
            // 
            // UIXApplicationSettingSurface
            // 
            this.Controls.Add(this.xPanel2);
            this.Controls.Add(this.xSplitter1);
            this.Controls.Add(this.c_settingExpenderBox);
            this.Name = "UIXApplicationSettingSurface";
            this.Size = new System.Drawing.Size(662, 408);
            this.ResumeLayout(false);

        }

        class DrSStudioApplicationSettingComparer : 
            IComparer, 
            IComparer<IGKXExpenderBoxItem>,
            IComparer<IGKXExpenderBoxGroup>
        {

            public int Compare(object x, object y)
            {
                return -1;
            }

            public int Compare(IGKXExpenderBoxGroup x, IGKXExpenderBoxGroup y)
            {
                return x.Index.CompareTo(y.Index);
            }

            public int Compare(IGKXExpenderBoxItem x, IGKXExpenderBoxItem y)
            {
                return x.Index.CompareTo(y.Index );
            }
        }

    
    }
}
