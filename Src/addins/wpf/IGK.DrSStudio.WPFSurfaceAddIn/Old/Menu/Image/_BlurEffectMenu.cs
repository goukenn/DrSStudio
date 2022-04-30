

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _BlurEffectMenu.cs
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
file:_BlurEffectMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Menu.Image
{
    using System.Windows.Media.Effects;
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI.Configuration;
    using IGK.DrSStudio.WinUI;
    [WPFMenu ("Image.Blur", 1 )]
    sealed class _BlurEffectMenu : WPFImageMenuBase , ICoreWorkingConfigurableObject 
    {
        private BlurEffect m_BlurEffect;
        public BlurEffect BlurEffect
        {
            get { return m_BlurEffect; }
        }
        public double Radius
        {
            get { return this.m_BlurEffect.Radius ; }
            set
            {
                this.m_BlurEffect.Radius = value;
            }
        }
        public RenderingBias  RenderingBias
        {
            get { return this.m_BlurEffect.RenderingBias ; }
            set
            {
                this.BlurEffect.RenderingBias = value;
            }
        }
        public System.Windows.Media.Effects.KernelType  KernelType
        {
            get { return this.BlurEffect.KernelType; }
            set
            {
                this.BlurEffect.KernelType = value;
            }
        }
        protected override bool PerformAction()
        {
            BlurEffect bl = new BlurEffect();
            bl.Radius = 5;
            bl.RenderingBias = RenderingBias.Quality;
            bl.KernelType = global::System.Windows.Media.Effects.KernelType.Gaussian;
            this.m_BlurEffect = bl;
            this.ImageShape.Effect = bl; 
            if (Workbench.ConfigureWorkingObject(this).Equals (enuDialogResult.OK))
            {
                this.ImageShape.Effect = null;
            }
            return base.PerformAction();
        }
        #region ICoreWorkingConfigurableObject Members
        public ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        public IGK.DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            Type t = this.GetType();
            var g = parameters.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem(t.GetProperty("Radius"));
            g.AddItem(t.GetProperty("RenderingBias"));
            g.AddItem(t.GetProperty("KernelType"));
            return parameters;
        }
        #endregion
    }
}

