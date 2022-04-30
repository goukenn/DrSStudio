

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLScaleEffect.cs
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
file:GLScaleEffect.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Effect
{
    
using IGK.ICore;using IGK.DrSStudio.WinUI.Configuration;
    class GLScaleEffect : GLEffectBase 
    {
        IGK.OGLGame.Graphics.GraphicsPixelTransfer    m_PixelTransfer;
        public GLScaleEffect()
        {
            m_PixelTransfer = new IGK.OGLGame.Graphics.GraphicsPixelTransfer();
            m_PixelTransfer.RedScale = 1.0f;
            m_PixelTransfer.BlueScale = 1.0f;
            m_PixelTransfer.GreenScale = 1.0f;
        }
        public event IGK.OGLGame.GLPropertyChangedHandler  PropertyChanged{
            add { m_PixelTransfer.PropertyChanged += value; }
            remove { m_PixelTransfer.PropertyChanged -= value; }
    }
        public override void Bind(IGK.OGLGame.Graphics.OGLGraphicsDevice graphicsDevice)
        {
            m_PixelTransfer.Bind(graphicsDevice);
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var group = parameters.AddGroup("ColorScale");
            group.AddTrackbar("RedScale", "lb.RedScale", 0, 200, 100, ScaleChanged);
            group.AddTrackbar("GreenScale", "lb.GreenScale", 0, 200, 100, ScaleChanged);
            group.AddTrackbar("BlueScale", "lb.BlueScale", 0, 200, 100, ScaleChanged);
            group.AddTrackbar("AlphaScale", "lb.AlphaScale", 0, 200, 100, ScaleChanged);
            group = parameters.AddGroup("ColorBIAS");
            group.AddTrackbar("RedBias", "lb.RedScale", -100, 100, 0, BiasChanged);
            group.AddTrackbar("GreenBias", "lb.GreenScale", -100, 100, 0, BiasChanged);
            group.AddTrackbar("BlueBias", "lb.BlueScale", -100, 100, 0, BiasChanged);
            group.AddTrackbar("AlphaBias", "lb.AlphaScale", -100, 100, 0, BiasChanged);
            return parameters;
        }
        void BiasChanged(Object obj, CoreParameterChangedEventArgs e)
        {
            System.Reflection.PropertyInfo pr = m_PixelTransfer.GetType().GetProperty(e.Item.Name);
            pr.SetValue(this.m_PixelTransfer, Convert.ToSingle(e.Value) / 100.0f, null);
        }
        void ScaleChanged(Object obj, CoreParameterChangedEventArgs e)
        {
            switch (e.Item.Name.ToLower ())
            {
                case "redscale":
                    m_PixelTransfer.RedScale = Convert .ToSingle (e.Value)/100.0f;
                    break;
                case "greenscale":
                    m_PixelTransfer.GreenScale = Convert.ToSingle (e.Value)/100.0f;
                    break;
                case "bluescale":
                    m_PixelTransfer.BlueScale = Convert .ToSingle (e.Value)/100.0f;
                    break;
                case "alphascale":
                    m_PixelTransfer.AlphaScale = Convert.ToSingle(e.Value) / 100.0f;
                    break;
            }
        }
        public override void UnBind(OGLGame.Graphics.OGLGraphicsDevice graphicsDevice)
        {
        }
    }
}

