


using IGK.ICore;


/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidResourceEditorSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Android.WinUI
{
    [AndroidSurfaceAttribute("ResourceEditor")]
    /// <summary>
    /// represent a android editor surface
    /// </summary>
    public class AndroidResourceEditorSurface :
        AndroidResourceEditorSurfaceBase,
        IAttributeEditorLoader,
        IAttributeEditorStoreListener 
    {

        private IGKXAttributeEditor c_editor;

        public AndroidResourceEditorSurface()
        {
            this.Load += _Load;
            this.InitializeComponent();
        }

        private void _Load(object sender, EventArgs e)
        {
            this.c_editor.SetAttributeLoaderListener (this);
            this.c_editor.SetStoreAttributeListener (this);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            c_editor = new IGKXAttributeEditor();
            c_editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(c_editor);

            // 
            // AndroidResourceEditorSurface
            // 
            this.Name = "AndroidResourceEditorSurface";
            this.Size = new System.Drawing.Size(451, 288);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// open a filen mae
        /// </summary>
        /// <param name="filename"></param>
        public void OpenFile(string filename)
        {
            if (!File.Exists(filename))
                return;
            this.c_editor.LoadFile(filename);
        }
    
        public void LoadAttribute(IGKXAttributeEditor editor, ICore.Xml.CoreXmlElement NodeName)
        {

        }
        public void StoreAttribute(IGKXAttributeEditor editor, string filename)
        {
          
        }
    }

}
