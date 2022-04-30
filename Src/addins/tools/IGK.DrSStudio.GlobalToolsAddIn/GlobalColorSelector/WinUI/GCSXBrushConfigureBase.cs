

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSXBrushConfigureBase.cs
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
file:XBrushConfigureBase.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
using System; using IGK.ICore.WinCore;
using IGK.ICore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.Drawing2D;
    using System.ComponentModel;
    using System.Windows.Forms;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent the base brush to configure class
    /// </summary>
    public class GCSXBrushConfigureBase:
        IGKXNoteBookPage,               
        IBrushSelector
    {
       private ICoreBrush m_BrushToConfigure;
        [Browsable(false)]
        public virtual enuBrushType BrushType { get { return enuBrushType.Solid; } }
        [Browsable(false )]
        public ICoreWorkingSurface CurrentSurface {
            get {
                ICoreWorkbench bench = CoreSystem.GetWorkbench();
                if (bench !=null)
                    return bench.CurrentSurface;
                return null;
            }
        }
        public GCSXBrushConfigureBase()
        {
            
        }
        #region IBrushSelector Members
        public ICoreBrush BrushToConfigure
        {
            get { return this.m_BrushToConfigure; }
            set
            {
                if (this.m_BrushToConfigure != value)
                {
                    this.m_BrushToConfigure = value;
                    OnBrushToConfigureChanged(EventArgs.Empty);                   
                }
            }
        }
        public event EventHandler BrushToConfigureChanged;

        protected virtual void OnBrushToConfigureChanged(EventArgs eventArgs)
        {
            if (this.m_BrushToConfigure != null)
            {
                if (this.m_BrushToConfigure.BrushType == this.BrushType)
                {
                    ConfigFromBrush(this.m_BrushToConfigure);
                }
                else
                {
                    //setup the new brush
                    ConfigureBrush();
                    if (this.BrushToConfigureChanged != null)
                    {
                        this.BrushToConfigureChanged(this, EventArgs.Empty);
                    }
                }
            }          
        }
        protected virtual void ConfigureBrush()
        {
        }
        protected virtual void ConfigFromBrush(ICoreBrush value)
        {
        }
        private IColorSelector m_ColorOwner;
        public IColorSelector ColorOwner
        {
            get { return m_ColorOwner; }
            set
            {
                if (m_ColorOwner != value)
                {
                    m_ColorOwner = value;
                }
            }
        }
        #endregion
        #region IColorSelector Members
        public virtual void SetColor(Colorf color)
        {
            //override this to configure color
        }
        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GCSXBrushConfigureBase
            // 
            this.Name = "GCSXBrushConfigureBase";
            this.Size = new System.Drawing.Size(747, 359);
            this.ResumeLayout(false);

        }
    }
}

