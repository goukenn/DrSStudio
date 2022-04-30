

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _DropDownShadowEffectMenu.cs
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
file:_DropDownShadowEffectMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Effects;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Menu.Image
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    [WPFMenu("Image.DropDownShadowEffect", 2)]
    public sealed class _DropDownShadowEffectMenu : WPFImageMenuBase, ICoreWorkingConfigurableObject 
    {
        private DropShadowEffect m_DropShadowEffect;
        public DropShadowEffect DropShadowEffect
        {
            get { return m_DropShadowEffect; }
        }
        public double BlurRadius
        {
            get { return m_DropShadowEffect.BlurRadius; }
            set
            {
                    m_DropShadowEffect.BlurRadius = value;
            }
        }
        public double Direction
        {
            get { return m_DropShadowEffect.Direction; }
            set
            {
                m_DropShadowEffect.Direction = value;
            }
        }
        public double Opacity
        {
            get { return m_DropShadowEffect.Opacity; }
            set
            {
                m_DropShadowEffect.Opacity = value;
            }
        }
        public double ShadowDepth
        {
            get { return m_DropShadowEffect.ShadowDepth; }
            set
            {
                m_DropShadowEffect.ShadowDepth = value;
            }
        }
        public System.Windows.Media.Color Color
        {
            get { return m_DropShadowEffect.Color; }
            set
            {
                    m_DropShadowEffect.Color = value;
            }
        }
        public RenderingBias RenderingBias
        {
            get { return this.m_DropShadowEffect.RenderingBias; }
            set
            {
                this.m_DropShadowEffect.RenderingBias = value;
            }
        }
        protected override bool PerformAction()
        {
            m_DropShadowEffect = new DropShadowEffect();
            m_DropShadowEffect.BlurRadius = 5;
            m_DropShadowEffect.Color = System.Windows.Media.Colors.Black;
            m_DropShadowEffect.Direction = 45;
            m_DropShadowEffect.Opacity = 1.0;
            m_DropShadowEffect.RenderingBias = RenderingBias.Performance;
            m_DropShadowEffect.ShadowDepth = 1.0;
            this.ImageShape.Effect = m_DropShadowEffect;
            if (this.Workbench.ConfigureWorkingObject(this).Equals (enuDialogResult.OK))
            { 
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
            g.AddItem(t.GetProperty("RenderingBias"));
            g.AddItem (t.GetProperty ("BlurRadius"));
            g.AddItem(t.GetProperty("Color"));
            g.AddItem(t.GetProperty("Direction"));
            g.AddItem(t.GetProperty("Opacity"));
            g.AddItem(t.GetProperty("ShadowDepth"));
            return parameters;
        }
        #endregion
    }
}

