

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidProjectEditorSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.Android.ContextMenu;

using IGK.ICore;
using IGK.ICore.ContextMenu;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.WinUI;

namespace IGK.DrSStudio.Android.WinUI
{
    [AndroidSurface("AndroidPropertySurface")]
    public class AndroidProjectEditorSurface : AndroidSurfaceBase 
    {
        private IGKXExpenderBox c_expenderBox_options;
        private IGKXPanel c_pan_content;
        public AndroidProjectEditorSurface()
        {
            this.InitializeComponent();
            this.Load += _Load;
            this.ProjectChanged += _ProjectChanged;
        }

        void _ProjectChanged(object sender, EventArgs e)
        {
            this.Title = this.Project.Name;
        }

        private void _Load(object sender, EventArgs e)
        {
            this.InitControl();
            this.Title = this.Project.Name;

            //ICoreLayoutManagerWorkbench bench = CoreSystem.GetWorkbench < ICoreLayoutManagerWorkbench>();
            //if (bench != null)
            //{ 
            //    //init layout
            //    var c = bench.CreateContextMenuItem();                
            //}
        }

        private void InitControl()
        {
            var g = this.c_expenderBox_options.AddGroup("general");
            g = this.c_expenderBox_options.AddGroup("application");            
            g = this.c_expenderBox_options.AddGroup("manifest");
            g = this.c_expenderBox_options.AddGroup("resources");
            g = this.c_expenderBox_options.AddGroup("layout");

            this.c_expenderBox_options.SelectedGroupChanged += _SelectedGroupChanged;
        }

        private void _SelectedGroupChanged(object sender, EventArgs e)
        {
            var g = this.c_expenderBox_options.SelectedGroup;
            ICoreWorkingConfigurableObject c = g.Tag as ICoreWorkingConfigurableObject;
            if (c != null)
            { 
                var b = CoreSystem.GetWorkbench ();
                if (b != null)
                    b.BuildWorkingProperty(this.c_pan_content, c);
            }
        }

        private void InitializeComponent()
        {
            this.c_expenderBox_options = new IGKXExpenderBox();
            this.c_pan_content = new IGKXPanel();
            this.SuspendLayout();
           
            // c_expenderBox_options
            // 
            this.c_expenderBox_options.CaptionKey = null;
            this.c_expenderBox_options.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_expenderBox_options.Location = new System.Drawing.Point(0, 0);
            this.c_expenderBox_options.Margin = new System.Windows.Forms.Padding(0);
            this.c_expenderBox_options.Name = "c_expenderBox_options";
            this.c_expenderBox_options.SelectedGroup = null;
            this.c_expenderBox_options.Size = new System.Drawing.Size(235, 303);
            this.c_expenderBox_options.TabIndex = 0;
            // 
            // c_pan_content
            // 
            this.c_pan_content.CaptionKey = null;
            this.c_pan_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_content.Location = new System.Drawing.Point(235, 0);
            this.c_pan_content.Name = "c_pan_content";
            this.c_pan_content.Size = new System.Drawing.Size(455, 303);
            this.c_pan_content.TabIndex = 1;
            // 
            // AndroidProjectEditor
            // 
            this.Controls.Add(this.c_pan_content);
            this.Controls.Add(this.c_expenderBox_options);
            this.Name = "AndroidProjectEditor";
            this.Size = new System.Drawing.Size(690, 303);
            this.ResumeLayout(false);

        }
     
    }
}
