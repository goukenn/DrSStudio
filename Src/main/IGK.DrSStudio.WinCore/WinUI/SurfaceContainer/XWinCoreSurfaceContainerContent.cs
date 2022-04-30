

using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XWinCoreSurfaceContainerContent.cs
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
file:XWinCoreSurfaceContainerContent.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent the surface host
    /// </summary>
    public class XWinCoreSurfaceContainerContent : IGKXUserControl 
    {
        private XWinCoreSurfaceContainer c_surfaceContainer;
        /// <summary>
        /// get the surface container
        /// </summary>
        public XWinCoreSurfaceContainer SurfaceContainer {
            get {
                return this.c_surfaceContainer;
            }
        }
        /// <summary>
        /// design mode
        /// </summary>
        public XWinCoreSurfaceContainerContent():base()
        {
        }

        protected override void OnControlAdded(System.Windows.Forms.ControlEventArgs e)
        {
            base.OnControlAdded(e);
        }
        protected override void OnControlRemoved(System.Windows.Forms.ControlEventArgs e)
        {
            base.OnControlRemoved(e);
        }
        /// <summary>
        /// surface container 
        /// </summary>
        /// <param name="surfaceContainer"></param>
        internal XWinCoreSurfaceContainerContent(XWinCoreSurfaceContainer  surfaceContainer):this()
        {
            this.c_surfaceContainer = surfaceContainer;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
        }
        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new ControlCollection(this);
        }
        new class ControlCollection : Control.ControlCollection
        {
            
            public ControlCollection(XWinCoreSurfaceContainerContent owner):base(owner )
            {
            }
            public override void Add(Control value)
            {
                if (value is ICoreWorkingSurface)
                {
                    base.Add(value);
                }
            }
            public override void Remove(Control value)
            {
                base.Remove(value);
            }

        }

    }
}

