

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PathCreationModeTool.cs
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
file:PathCreationModeTool.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
    [CoreTools ("PathCreationModeTool", Category="Drawing2D" , ImageKey="PathCreationTool")]
    /// <summary>
    /// represent a path creation mode tool
    /// </summary>
    sealed class PathCreationModeTool : Core2DDrawingToolBase 
    {
        private static PathCreationModeTool sm_instance;
        private PathCreationModeTool()
        {
        }
        public static PathCreationModeTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static PathCreationModeTool()
        {
            sm_instance = new PathCreationModeTool();
        }
        protected override void RegisterSurfaceEvent(WinUI.ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.CurrentToolChanged += new EventHandler(surface_CurrentToolChanged);
        }
        protected override void UnRegisterSurfaceEvent(WinUI.ICore2DDrawingSurface surface)
        {
            surface.CurrentToolChanged -= new EventHandler(surface_CurrentToolChanged);
            base.UnRegisterSurfaceEvent(surface);
        }
        void surface_CurrentToolChanged(object sender, EventArgs e)
        {
            if (this.CurrentSurface.CurrentTool == typeof(PathElement))
            {
                ((XToolCreationModeToolStrip)this.HostedControl).Mecanism = this.CurrentSurface.Mecanism;
            }
            else {
                ((XToolCreationModeToolStrip)this.HostedControl).Mecanism = null;
            }
        }
        protected override void GenerateHostedControl()
        {            
            this.HostedControl = new XToolCreationModeToolStrip(this);
        }
    }
}

