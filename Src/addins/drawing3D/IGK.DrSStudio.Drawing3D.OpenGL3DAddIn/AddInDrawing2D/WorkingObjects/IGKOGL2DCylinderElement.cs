

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DCylinderElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.ICore.Codec;
using IGK.ICore.WinUI.Configuration;
using IGK.GLLib;
using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Drawing3D;

namespace IGK.DrSStudio.Drawing2D.OpenGL
{
    [IGKOGL2DItemAttribute(
        "Cylinder", 
        typeof(Mecanism),
        ImageKey= OGLImageKeys.DE_CYLINDER_GKDS)]
    /// <summary>
    /// represent a cube element in a working object
    /// </summary>
    public class IGKOGL2DCylinderElement : IGKOGL2DWorkingObjectBase 
    {

        private double m_Height;
        private int m_Slices;
        private int m_Stack;
        private double m_BaseRadius;
        private double m_TopRadius;
        [CoreConfigurableProperty()]
        [CoreXMLAttribute()]
        public double TopRadius
        {
            get { return m_TopRadius; }
            set
            {
                if (m_TopRadius != value)
                {
                    m_TopRadius = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreConfigurableProperty()]
        [CoreXMLAttribute()]
        public double BaseRadius
        {
            get { return m_BaseRadius; }
            set
            {
                if (m_BaseRadius != value)
                {
                    m_BaseRadius = value;

                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        [CoreConfigurableProperty()]
        [CoreXMLAttribute()]
        public int Stack
        {
            get { return m_Stack; }
            set
            {
                if ((m_Stack != value) && (value > 1))
                {
                    m_Stack = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreConfigurableProperty()]
        [CoreXMLAttribute()]
        public int Slices
        {
            get { return m_Slices; }
            set
            {
                if ((m_Slices != value) && (value > 1))
                {
                    m_Slices = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreConfigurableProperty()]
        [CoreXMLAttribute()]
        public double Height
        {
            get { return m_Height; }
            set
            {
                if (m_Height != value)
                {
                    m_Height = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Slices = 16;
            this.m_Stack = 16;
            this.m_Height = 1.0f;
            this.m_BaseRadius = 0.5f;
            this.m_TopRadius = 0.5f;
        }
        protected override void Visit(IIGKOGL2DDrawingDeviceVisitor visitor)
        {
            Rectanglei rc = Rectanglei.Round(this.Bounds);
            if (rc.IsEmpty)
                return;
            object obj = visitor.Save();            
            this.Setting.BindSetting(visitor);
            var e = IGKOGL2DDrawingResourcesManager.GetQuadric(this, visitor.Device);
            
            visitor.Device.SetColor(FillBrush.Colors[0]);
            GLU.gluCylinder(e.Quadric, this.BaseRadius, this.TopRadius, this.Height, this.Slices, this.Stack);
            this.Setting.UnbindSetting(visitor);
            visitor.Restore(obj);
        }

        public new class Mecanism : RectangleElement.Mecanism 
        { 

        }
      
    }
}
