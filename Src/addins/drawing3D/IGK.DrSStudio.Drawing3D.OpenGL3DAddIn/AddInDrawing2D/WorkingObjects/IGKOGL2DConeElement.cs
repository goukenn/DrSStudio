

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DConeElement.cs
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
        "Cone", 
        typeof(Mecanism),
        ImageKey=OGLImageKeys.DE_CUBE_GKDS)]
    /// <summary>
    /// represent a cube element in a working object
    /// </summary>
    public class IGKOGL2DConeElement : IGKOGL2DWorkingObjectBase 
    {
        private double m_Radius;
        private double m_Height;
        private int m_Slices;
        private int m_Stack;
        [CoreConfigurableProperty()]
        [CoreXMLAttribute()]
        public int Stack
        {
            get { return m_Stack; }
            set
            {
                if ((m_Stack != value)&&(value > 1))
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
                if ((m_Slices != value)&&(value > 1))
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
        [CoreConfigurableProperty()]
        [CoreXMLAttribute()]
        public double Radius
        {
            get { return m_Radius; }
            set
            {
                if (m_Radius != value)
                {
                    m_Radius = value;

                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Slices = 10;
            this.m_Stack = 16;
            this.m_Radius = 0.4f;
            this.m_Height = 1.0f;
        }
        protected override void Visit(IIGKOGL2DDrawingDeviceVisitor visitor)
        {
            Rectanglei rc = Rectanglei.Round(this.Bounds);
            if (rc.IsEmpty)
                return;
            object obj = visitor.Save();            
            this.Setting.BindSetting(visitor);
            visitor.Device.SetColor(FillBrush.Colors[0]);
            GLUT.glutSolidCone(this.Radius, this.Height, this.Slices, this.Stack);
            visitor.Device.SetColor(StrokeBrush.Colors[0]);
            GLUT.glutWireCone(this.Radius, this.Height, this.Slices, this.Stack);
            this.Setting.UnbindSetting(visitor);
            visitor.Restore(obj);
        }

        public new class Mecanism : RectangleElement.Mecanism 
        { 

        }
      
    }
}
