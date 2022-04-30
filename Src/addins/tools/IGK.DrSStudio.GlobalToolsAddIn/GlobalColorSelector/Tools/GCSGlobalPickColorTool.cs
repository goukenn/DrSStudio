

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSGlobalPickColorTool.cs
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
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools
{
    
    public class GCSGlobalPickColorTool : CoreToolBase
    {
        private static GCSGlobalPickColorTool sm_instance;
        private PickColorMecanism.Mecanism m_PickColorMecanism;

        private GCSGlobalPickColorTool()
        {
        }

        public static GCSGlobalPickColorTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GCSGlobalPickColorTool()
        {
            sm_instance = new GCSGlobalPickColorTool();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = GCSGlobalColorTool.Instance.HostedControl;
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
            s.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
            GCSGlobalColorTool.Instance.HostedControl.PickColorButton.Enabled = true ;
            GCSGlobalColorTool.Instance.HostedControl.PickColorButton.Click += 
                PickColorButton_Click;
        }

        private void PickColorButton_Click(object sender, EventArgs e)
        {
            if (this.m_PickColorMecanism == null)
            {
                ICoreWorkingToolManagerSurface v_surface = this.CurrentSurface as ICoreWorkingToolManagerSurface;
                if (!v_surface.Mecanism.IsFreezed)
                {//mecanism already freeze
                    this.m_PickColorMecanism = new PickColorMecanism.Mecanism(v_surface.Mecanism);
                    this.m_PickColorMecanism.SelectedColorChanged += _MecanismColorChanged;
                    this.m_PickColorMecanism.SelectionAbort += new EventHandler(m_PickColor_SelectionAbort);
                    //set capture to the surface
                    v_surface.Capture = true;
                    v_surface.Focus();
                }
            }
        }

        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.UnregisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
            GCSGlobalColorTool.Instance.HostedControl.PickColorButton.Enabled = false;
            GCSGlobalColorTool.Instance.HostedControl.PickColorButton.Click -=
                PickColorButton_Click;
        }

        void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            GCSGlobalColorTool.Instance.HostedControl.PickColorButton.Enabled = e.NewElement is ICoreWorkingColorSurface;
        }

        void m_PickColor_SelectionAbort(object sender, EventArgs e)
        {
            EndPickColor();
        }

        private void EndPickColor()
        {
            if (this.m_PickColorMecanism == null)
                return;
            this.m_PickColorMecanism.SelectedColorChanged -= _MecanismColorChanged;
            this.m_PickColorMecanism.SelectionAbort -= new EventHandler(m_PickColor_SelectionAbort);            
            this.m_PickColorMecanism = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        void _MecanismColorChanged(object o, EventArgs e)
        {
            //set the color property
            Colorf cl = this.m_PickColorMecanism.SelectedColor;
            GCSGlobalColorTool.Instance.HostedControl.SetColor(cl);
            if (GCSGlobalColorTool.Instance.HostedControl.CurrentSelector!=null)
            GCSGlobalColorTool.Instance.HostedControl.CurrentSelector.SetColor(cl);
            this.EndPickColor();
        }

        void SetColor(Colorf cl)
        {
            GCSGlobalColorTool .Instance.HostedControl.SetColor (cl);
        }

       

        internal void SetWorkbench(GCSGlobalColorTool gCSGlobalColorTool, ICoreSystemWorkbench coreWorkbench)
        {
            this.Workbench = coreWorkbench;
        }
    }

}
