

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DButtonSurfaceInitializer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;

    class IGKD2DButtonSurfaceInitializer : 
        ICoreWorkingProjectConfiguration,
        ICoreWorkingProjectWizard,
        ICoreInitializatorParam 
    {
        private float m_Width;
        private float m_Height;
        private ICoreWorkingSurface m_surface;

        public IGKD2DButtonSurfaceInitializer()
        {
            this.Width = 32;
            this.Height = 32;
               
        }
        public float Height
        {
            get { return m_Height; }
            set
            {
                if (m_Height != value)
                {
                    m_Height = value;
                }
            }
        }
        public float Width
        {
            get { return m_Width; }
            set
            {
                if (m_Width != value)
                {
                    m_Width = value;
                }
            }
        }
        public ICoreControl GetConfigControl()
        {
            return null;
        }

        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }

        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var t = parameters.AddGroup("buttondefinition");
            t.AddItem(GetType().GetProperty("Width"));
            t.AddItem(GetType().GetProperty("Height"));
            return parameters;
        }

        public string Id
        {
            get { return "init.buttondocumentdrawing"; }
        }

        public bool IsWellConfigured
        {
            get {
                return ((this.Width > 0) && (this.Height > 0));
            }
        }

        public enuDialogResult RunConfigurationWizzard(ICoreSystemWorkbench bench)
        {
            IGKXUserControl ctr = new IGKXUserControl();
            ctr.Size = new System.Drawing.Size(400, 200);
            //using (var b = bench.CreateNewDialog(ctr))
            //{
            //    b.Title = "title.buttonwizard".R();
            //    b.Size = new Size2i(ctr.Size.Width, ctr.Size.Height);
            //    ctr.Anchor = (System.Windows.Forms.AnchorStyles)15;
            if (bench.ConfigureWorkingObject(this, "title.buttoninitializer".R(), false, Size2i.Empty) == enuDialogResult.OK)
                {
                    this.m_surface = new IGKD2DButtonSurface();
                    this.m_surface.SetParam(this);
                    return enuDialogResult.OK;
                }
           // }
            return enuDialogResult.No;
        }

        public ICoreWorkingSurface Surface
        {
            get {
                return this.m_surface;
            }
        }

        public bool Contains(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;
            switch (key.ToLower())
            {
                case "width":
                case "height":
                    return true;
            }
            return false;
        }

        public int Count
        {
            get { return 2; }
        }

        public string this[string key]
        {
            get {
                switch (key.ToLower())
                {
                    case "width": return this.Width.ToString();
                    case "height": return this.Height.ToString();
                }
                return string.Empty;
            }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            Dictionary<string, string> m = new Dictionary<string, string>();
            m.Add("width", this.Width.ToString());
            m.Add("height", this.Width.ToString());
            return m.GetEnumerator();
        }
    }
}
