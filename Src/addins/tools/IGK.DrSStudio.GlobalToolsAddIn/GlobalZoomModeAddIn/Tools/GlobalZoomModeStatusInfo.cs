

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GlobalZoomModeStatusInfo.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools
{
    using IGK.DrSStudio.WinUI;
    using IGK.ICore.Tools;

    [CoreTools("Tool.GlobalZoomModeAddIn")]
    sealed class GlobalZoomModeAddIn : CoreToolBase 
    {
        private static GlobalZoomModeAddIn sm_instance;
        private GlobalZoomModeAddIn()
        {
        }

        public static GlobalZoomModeAddIn Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GlobalZoomModeAddIn()
        {
            sm_instance = new GlobalZoomModeAddIn();
        }
        private IGKWinCoreStatusTextItem m_textLabel;

        protected override void GenerateHostedControl()
        {
            m_textLabel = new IGKWinCoreStatusTextItem();
            this.m_textLabel.Bounds = new Rectanglef(0, 0, 100, 0);
            this.m_textLabel.Index = 0x0400;
            this.Workbench.GetLayoutManager()?.StatusControl.Items.Add(this.m_textLabel);
        }
        private new ICore2DDrawingSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICore2DDrawingSurface;
            }
        }
        private void UpdateText()
        {
            var s = this.CurrentSurface;
            if (s != null)
            {
                this.m_textLabel.Text = s.ZoomMode.ToString();
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
            this.m_textLabel.Visible = false;
            if (e.OldElement is ICore2DDrawingSurface )
            this.UnRegiserSurface(e.OldElement  as ICore2DDrawingSurface);
            if (e.NewElement is ICore2DDrawingSurface)
            this.RegiserSurface(e.NewElement as ICore2DDrawingSurface);   
        }

        private void UnRegiserSurface(ICore2DDrawingSurface s)
        {
            s.ZoomModeChanged -= _ZoomModeChanged;
        }

        private void _ZoomModeChanged(object sender, EventArgs e)
        {
            this.UpdateText();
        }


        private void RegiserSurface(ICore2DDrawingSurface s)
        {
            s.ZoomModeChanged += _ZoomModeChanged;
            this.UpdateText();
            this.m_textLabel.Visible = true;
        }

    }
}
