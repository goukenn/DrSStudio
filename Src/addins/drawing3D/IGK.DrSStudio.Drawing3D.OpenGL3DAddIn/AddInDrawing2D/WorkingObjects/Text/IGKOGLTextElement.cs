

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGLTextElement.cs
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
﻿using IGK.ICore.Codec;
using IGK.ICore.WinUI.Configuration;
using IGK.OGLGame.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.OpenGL
{
    [IGKOGL2DItemAttribute("OpenGLText3D", typeof (Mecanism ))]
    class IGKOGLTextElement : 
        TextElement , 
        ICore2DDrawingVisitable,
        IIGKOGL2DWorkingObject,
        IIGKOGL3DTextElement
    {
        private IIGKOGL2DGraphicsSetting m_setting;
        private enuGL3DFontFormat m_FontFormat;

        [CoreXMLAttribute()]
        [CoreConfigurableProperty()]
        public enuGL3DFontFormat FontFormat
        {
            get { return m_FontFormat; }
            set
            {
                if (m_FontFormat != value)
                {
                    m_FontFormat = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        public IIGKOGL2DGraphicsSetting Setting
        {
            get { return this.m_setting; }
        }

        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_setting = CreateSetting();
            this.Text = "OpenGL Text";
        }
        private IIGKOGL2DGraphicsSetting CreateSetting()
        {
            return new IGKOGLGraphicSetting(this);
        }

        public virtual bool Accept(ICore2DDrawingVisitor visitor)
        {//support drawing3D visitor
            return (visitor is IIGKOGL2DDrawingDeviceVisitor);
        }

        public virtual void Visit(ICore2DDrawingVisitor visitor)
        {
            this.Visit(visitor as IIGKOGL2DDrawingDeviceVisitor);
        }

        
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();
            p.AddRectangle(this.Bounds);
        }

        public override Rectanglef GetSelectionBound()
        {
            return this.GetPath().GetBounds();
        }
        protected virtual void Visit(IIGKOGL2DDrawingDeviceVisitor visitor)
        {
            object obj = visitor.Save();
            this.m_setting.BindSetting(visitor);

            var v_ft = IGKOGL2DDrawingResourcesManager.Get3DFont(
                visitor.Device, this.Font, this);
            //if (this.m_ft == null)
            //    this.m_ft = IGK.OGLGame.Text.GL3DFont.Load(device, this.Font.FontName, IGK.OGLGame.enuGLFontStyle.Regular, IGK.OGLGame.Text.enuGL3DFontFormat.FillPolygon, 100.0f, 100.0f);
            var device = visitor.Device;
            if (v_ft !=null)
            {
                device.Projection.PushMatrix();
                //device.Projection.Translate(Position.X, Position.Y, 0.0f);
               // device.Projection.Scale(1, -1, 1);
                device.Draw3DString(v_ft, this.Text, 
                    Vector3f.Zero, 
                    this.FillBrush.Colors[0]);
                device.Projection.PopMatrix();
            }
            //v_ft.Dispose();
            this.m_setting.UnbindSetting(visitor);
            visitor.Restore(obj);
        }


        public new class Mecanism : RectangleElement.Mecanism
        { 

        }

    }
}
