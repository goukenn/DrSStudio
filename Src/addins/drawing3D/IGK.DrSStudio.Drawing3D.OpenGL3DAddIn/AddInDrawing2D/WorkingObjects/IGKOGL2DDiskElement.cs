

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DDiskElement.cs
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
        "Disk", 
        typeof(Mecanism),
        ImageKey= OGLImageKeys.DE_DISK_GKDS)]
    /// <summary>
    /// represent a cube element in a working object
    /// </summary>
    public class IGKOGL2DDiskElement : IGKOGL2DWorkingObjectBase 
    {
        private float m_InnerRadius;
        private float m_OuterRadius;
        private int m_Slices;
        private int m_Rings;

        [CoreConfigurableProperty()]
        [CoreXMLAttribute()]
        public int Rings
        {
            get { return m_Rings; }
            set
            {
                if ((m_Rings != value) && (value > 1))
                {
                    m_Rings = value;

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
        public float OuterRadius
        {
            get { return m_OuterRadius; }
            set
            {
                if (m_OuterRadius != value)
                {
                    m_OuterRadius = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreConfigurableProperty()]
        [CoreXMLAttribute()]
        public float InnerRadius
        {
            get { return m_InnerRadius; }
            set
            {
                if (m_InnerRadius != value)
                {
                    m_InnerRadius = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Rings = 10;
            this.m_Slices = 16;
            this.m_InnerRadius = 0.7f;
            this.m_OuterRadius = 1.0f;

        }
        protected override void Visit(IIGKOGL2DDrawingDeviceVisitor visitor)
        {
            Rectanglei rc = Rectanglei.Round(this.Bounds);
            if (rc.IsEmpty)
                return;
            object obj = visitor.Save();            
            this.Setting.BindSetting(visitor);
            visitor.Device.SetColor(FillBrush.Colors[0]);
            var e = IGKOGL2DDrawingResourcesManager.GetQuadric(this, visitor.Device);
            GLU.gluDisk(e.Quadric, this.InnerRadius, this.OuterRadius, this.Slices, this.Rings);
            
            this.Setting.UnbindSetting(visitor);
            visitor.Restore(obj);
        }

        public new class Mecanism : RectangleElement.Mecanism 
        { 

        }
      
    }
}
