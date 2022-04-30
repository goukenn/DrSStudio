

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSGlobalColorTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Resources;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools
{
    
    /// <summary>
    /// represent a tool that will be used to select brush color on intem or surface
    /// </summary>
    [CoreTools("Tool.GCGlobalColorTool", ImageKey=CoreImageKeys.MENU_COLOR_GKDS)]
    sealed class GCSGlobalColorTool : CoreToolBase{

        private static GCSGlobalColorTool sm_instance;
        private bool m_configuring;
        //public new CoreWorkingConfigElementSurface CurrentSurface {
        //    get {
        //        return base.CurrentSurface as ICoreWorkingConfigElementSurface;
        //    }
        //}
        private GCSGlobalColorTool()
        {
        }

        public static GCSGlobalColorTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GCSGlobalColorTool()
        {
            sm_instance = new GCSGlobalColorTool();

        }
        public new GCSXDualBrushColorSelector HostedControl {
            get {
                return base.HostedControl as GCSXDualBrushColorSelector;
            }
            set {
                base.HostedControl = value;
            }
        }
        protected override void GenerateHostedControl()
        {
            var c = new GCSXDualBrushColorSelector()
            {
                Tool = this,
                ToolDocument = CoreResources.GetDocument(this.ToolImageKey),
                CaptionKey = "title.Colors"
            };
            c.SaveBrushButton.Click += SaveBrushButton_Click;
            c.VisibleChanged += c_VisibleChanged;
            c.ConfigFillBrush.BrushDefinitionChanged += ConfigFillBrush_BrushDefinitionChanged;
            c.ConfigStrokeBrush.BrushDefinitionChanged += ConfigStrokeBrush_BrushDefinitionChanged;
            this.HostedControl = c;

            GCSGlobalPickColorTool.Instance.SetWorkbench(this, this.Workbench);
        }

        void ConfigStrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            this.m_configuring = true;
            ICore2DDualBrushObject br = this.CurrentSurface as ICore2DDualBrushObject;
            if (br!=null)
            {
                br.StrokeBrush.Copy(this.HostedControl.ConfigStrokeBrush );
            }
            else{
                ICoreWorkingColorSurface c = this.CurrentSurface as ICoreWorkingColorSurface;
                if (c != null)
                {
                    c.CurrentColor = this.HostedControl.ConfigStrokeBrush.Colors[0];
                }
            }
            this.m_configuring = false;
        }

        


        void ConfigFillBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            this.m_configuring = true;
            ICore2DDualBrushObject br = this.CurrentSurface as ICore2DDualBrushObject;
            if (br != null)
            {
                br.FillBrush.Copy(this.HostedControl.ConfigFillBrush);
            }
            else
            {
                ICoreWorkingColorSurface c = this.CurrentSurface as ICoreWorkingColorSurface;
                if (c != null)
                {
                    c.CurrentColor = this.HostedControl.ConfigFillBrush.Colors[0];
                }
            }
            this.m_configuring = false;
        }

        void c_VisibleChanged(object sender, EventArgs e)
        {
            this.UpdateElementConfig();
        }

        void SaveBrushButton_Click(object sender, EventArgs e)
        {
            ICoreBrush br = this.HostedControl.GetBrush(this.HostedControl.BrushMode);
            if (br != null)
            {
                using (IXCoreSaveDialog sfd = Workbench.CreateNewSaveDialog())
                {
                    sfd.Filter = "Brush definition | *.igkbrush";
                    if (sfd.ShowDialog() == enuDialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, br.GetDefinition());
                    }
                }
            }
        }


        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += _CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= _CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
        }

        void _CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            if (e.OldElement is ICoreWorkingConfigElementSurface)
            {
                UnregisterSurfaceEvent(e.OldElement as ICoreWorkingConfigElementSurface);
            }
            if (e.OldElement is ICoreWorkingColorSurface)
            {
                UnregisterSurfaceEvent(e.OldElement as ICoreWorkingColorSurface);
            }

            if (e.NewElement is ICoreWorkingColorSurface)
            {
                RegisterSurfaceEvent(e.NewElement as ICoreWorkingColorSurface);
            }
            if (e.NewElement is ICoreWorkingConfigElementSurface)
            {
                var s = e.NewElement as ICoreWorkingConfigElementSurface;
                RegisterSurfaceEvent(s);
                ICoreBrushOwner br = s.ElementToConfigure as ICoreBrushOwner;
                if (br != null)
                    this.HostedControl.ElementToConfigure = br;
                else {
                    this.HostedControl.ElementToConfigure = null;
                    //copy surface brush definition
                    this.HostedControl.CopyBrushDefinition(e.NewElement as ICoreBrushOwner);
                }
            }
            else
                this.HostedControl.ElementToConfigure = null;
        }

        private void RegisterSurfaceEvent(ICoreWorkingColorSurface surface)
        {
            surface.CurrentColorChanged += surface_CurrentColorChanged;
        }

        void surface_CurrentColorChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;
            ICoreWorkingColorSurface v_surface = sender as ICoreWorkingColorSurface;

            this.m_configuring = true;
            this.HostedControl.SetColor(v_surface.CurrentColor);
            this.m_configuring = false;
        }

        private void UnregisterSurfaceEvent(ICoreWorkingColorSurface surface)
        {
            surface.CurrentColorChanged -= surface_CurrentColorChanged;
        }

        private void RegisterSurfaceEvent(ICoreWorkingConfigElementSurface surface)
        {
            surface.ElementToConfigureChanged += surface_ElementToConfigureChanged;
        }
        private void UnregisterSurfaceEvent(ICoreWorkingConfigElementSurface surface)
        {
            surface.ElementToConfigureChanged -= surface_ElementToConfigureChanged;
        }
        void surface_ElementToConfigureChanged(object sender, EventArgs e)
        {
            this.UpdateElementConfig();
        }

        private void UpdateElementConfig()
        {
            var s = this.CurrentSurface as ICoreWorkingConfigElementSurface;
            if (this.HostedControl != null)
            {
                if ((s != null) && this.HostedControl.Visible)
                {

                    if (s.ElementToConfigure != null)
                    {
                        this.HostedControl.ElementToConfigure = s.ElementToConfigure as ICoreBrushOwner;
                    }
                    else {
                        this.HostedControl.ElementToConfigure = s as ICoreBrushOwner;
                    }
                }
                else
                    this.HostedControl.ElementToConfigure = null;
            }
        }

     

      }
}
