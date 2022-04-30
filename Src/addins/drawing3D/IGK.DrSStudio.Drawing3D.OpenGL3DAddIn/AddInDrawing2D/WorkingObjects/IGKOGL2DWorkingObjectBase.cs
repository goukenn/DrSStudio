

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DWorkingObjectBase.cs
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
﻿using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.OpenGL
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class IGKOGL2DWorkingObjectBase : 
        RectangleElement , 
        ICore2DDrawingVisitable,
        IIGKOGL2DWorkingObject
    {


        private IIGKOGL2DGraphicsSetting m_setting;

        public IIGKOGL2DGraphicsSetting Setting
        {
            get { return this.m_setting; }
        }

        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_setting = CreateSetting();
        }

        private IIGKOGL2DGraphicsSetting CreateSetting()
        {
            return new GraphicSetting(this);
        }

        //public IIGKOGL2DGraphicsSetting GraphicSetting
        //{
        //    get
        //    {
        //        return this.m_setting;
        //    }
        //}

        public virtual bool Accept(ICore2DDrawingVisitor visitor)
        {//support drawing3D visitor
            return (visitor is IIGKOGL2DDrawingDeviceVisitor);
        }

        public virtual void Visit(ICore2DDrawingVisitor visitor)
        {
            this.Visit (visitor as IIGKOGL2DDrawingDeviceVisitor);    
        }

        protected virtual void Visit(IIGKOGL2DDrawingDeviceVisitor visitor)
        {
            object obj = visitor.Save();
            this.m_setting.BindSetting(visitor);


            this.m_setting.UnbindSetting(visitor);
            visitor.Restore(obj);
        }

        


        public class GraphicSetting : IIGKOGL2DGraphicsSetting
        {

            private IGKOGL2DWorkingObjectBase m_Owner;

            public IGKOGL2DWorkingObjectBase Owner
            {
                get { return m_Owner; }
            }
            public GraphicSetting(IGKOGL2DWorkingObjectBase owner)
            {
                
                this.m_Owner = owner;
            }
            public virtual void BindSetting(IIGKOGL2DDrawingDeviceVisitor visitor)
            {
                visitor.SetupGraphicsDevice(this.m_Owner);
                visitor.Device.Capabilities.CullFace = false  ;
                //visitor.Device.Capabilities.Blend = true;
                visitor.Device.Capabilities.DepthTest = true  ;

                visitor.MultiplyTransform(this.Owner.GetMatrix(), enuMatrixOrder.Prepend);
                //enabled scissor for scissor testing
                visitor.Device.Capabilities.ScissorTest = true;
                

                //transform bouwn to scissor test compatible
                //to scissor rectangle. because scissor rectangle is a rectangle according to the viewport
                //where viewport is BottomRight oriented but gdi is ToRight oriented. we have to make some 
                //adjustement
                Rectanglei rc = Rectanglei.Empty;

                Matrix m = visitor.GetCurrentTransform();
                rc = Rectanglei.Round(CoreMathOperation.ApplyMatrix(this.Owner.Bounds, m));
                //visitor.ToViewPortBound();
                //the adjustment
                rc = visitor.TransformToViewportBound(rc);//.Device.Viewport.Height - rc.Height - rc.Y ;
                visitor.Device.Viewport = rc;
                //set scissor test
                visitor.Device.SetScissorBox(rc);
                visitor.Device.RenderState.DepthBufferWriteMask = true ;
                visitor.Device.Clear(enuBufferBit.Depth);
                visitor.Device.Projection.PushMatrix();
                visitor.Device.Projection.MatrixMode = MatrixMode.Projection;
                visitor.Device.Projection.PushMatrix();
                visitor.Device.Projection.LoadIdentity();
                visitor.Device.Projection.SetFrustum(-1, 1, -1, 1, 1.0f, 10.0f);
            //    visitor.Device.Projection.Translate(0, 0, -3f);
                //visitor.Device.Projection.Translate(0, 0, -5.5f);
                //setup the graphics view
                visitor.Device.Projection.MatrixMode = MatrixMode.ModelView;
                visitor.Device.Projection.LoadIdentity();
                //setup de model-view
               visitor.Device.Projection.LookAt (new Vector3f (1.5f,1.5f, 1.5f), Vector3f.Zero, new Vector3f (0,1, 0));


               visitor.Device.Capabilities.Lighting = false;
               visitor.Device.Lights[0].Enabled = true;
               visitor.Device.Lights[0].Diffuse = Colorf.Red;
               visitor.Device.Lights[0].Position = new Vector4f(2, 2, -2, 0.0f);


                //visitor.Device.Capabilities.Enable(IGK.GLLib.GL.GL_COLOR_MATERIAL);
                //visitor.Device.Capabilities.CullFace = true;
                //visitor.Device.RenderState.CullFrontFace = PolygonFrontFace.ClockWise;
                //visitor.Device.RenderState.CullFaceMode = PolygonCullFace.Front;



                visitor.Device.Capabilities.PolygonSmooth = true;
                visitor.Device.Capabilities.LineSmooth = true;
                visitor.Device.Capabilities.PointSmooth = true;
                m.Dispose();
            }

            public virtual void UnbindSetting(IIGKOGL2DDrawingDeviceVisitor visitor)
            {
                visitor.Device.Projection.MatrixMode = MatrixMode.Projection;
                visitor.Device.Projection.PopMatrix();
                visitor.Device.Projection.MatrixMode = MatrixMode.ModelView;
                visitor.Device.Projection.PopMatrix();
                visitor.Device.Capabilities.ScissorTest = false;
                visitor.Device.Capabilities.CullFace = false;
                visitor.Device.Capabilities.Lighting = false;
                visitor.Device.Capabilities.DepthTest = false;
                //visitor.Device.Viewport = Rectanglei.Empty;
                visitor.Device.CheckError();
            }
        }
    }
}
