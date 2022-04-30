/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Drawing2DSurfaceTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore.WinCore;
using IGK.DrSStudio.Drawing2D.Settings;
using IGK.ICore;
using IGK.ICore.Drawing2D.Tools;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Menu.View
{
    [CoreTools("Tool.Drawing2DSurfaceTool")]
    class Drawing2DSurfaceTool : Core2DDrawingToolBase
    {
        private static Drawing2DSurfaceTool sm_instance;
        private Drawing2DSurfaceTool()
        {
        }

        public static Drawing2DSurfaceTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static Drawing2DSurfaceTool()
        {
            sm_instance = new Drawing2DSurfaceTool();

        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            var fb = workbench as ICoreSurfaceManagerWorkbench;
            if (fb!=null)
            fb.SurfaceAdded += workbench_SurfaceAdded;
        }

       
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            var fb = workbench as ICoreSurfaceManagerWorkbench;
            if (fb != null)
            fb.SurfaceAdded -= workbench_SurfaceAdded;
            base.UnregisterBenchEvent(workbench);

        }

        void workbench_SurfaceAdded(object sender, CoreItemEventArgs<ICoreWorkingSurface> e)
        {
            var v_s = e.Item as ICore2DDrawingSurface;
            if (v_s != null)
            {
                v_s.ZoomMode = Drawing2DZoomSetting.Instance.ZoomMode;
                v_s.ShowScroll = Drawing2DZoomSetting.Instance.ShowScroll;
                if (v_s is ICore2DDrawingRuleSurface)
                {
                    (v_s as ICore2DDrawingRuleSurface).ShowRules = Drawing2DZoomSetting.Instance.ShowRule;
                }
            }
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.ZoomModeChanged += surface_ZoomModeChanged;
            surface.ShowScrollChanged += surface_ShowScrollChanged;
            if (surface is ICore2DDrawingRuleSurface)
            (surface as ICore2DDrawingRuleSurface).ShowRuleChanged += Drawing2DSurfaceTool_ShowRuleChanged;
        }

     
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.ZoomModeChanged -= surface_ZoomModeChanged;
            surface.ShowScrollChanged -= surface_ShowScrollChanged;
            if (surface is ICore2DDrawingRuleSurface)
                (surface as ICore2DDrawingRuleSurface).ShowRuleChanged -= Drawing2DSurfaceTool_ShowRuleChanged;
            base.UnRegisterSurfaceEvent(surface);
        }
        void Drawing2DSurfaceTool_ShowRuleChanged(object sender, EventArgs e)
        {
            Drawing2DZoomSetting.Instance.ShowRule = (sender as ICore2DDrawingRuleSurface).ShowRules;
        }
        void surface_ShowScrollChanged(object sender, EventArgs e)
        {
            Drawing2DZoomSetting.Instance.ShowScroll= this.CurrentSurface.ShowScroll;
        }

        void surface_ZoomModeChanged(object sender, EventArgs e)
        {
            Drawing2DZoomSetting.Instance.ZoomMode = this.CurrentSurface.ZoomMode;
        }
    }
}
