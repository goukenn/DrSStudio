

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLTriangle.cs
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
file:GLTriangle.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.OGLGame.WinUI.GLControls
{
    
using IGK.ICore;
    using IGK.OGLGame.Graphics;
    public class GLTriangle : GLShape 
    {
        private Colorf m_FillColor;
        public Colorf FillColor
        {
            get { return m_FillColor; }
            set
            {
                if (!m_FillColor.Equals(value))
                {
                    m_FillColor = value;
                }
            }
        }
            public GLTriangle(){ 
                this.FillColor = Colorf.White ;
}
            int m_angle;
        protected internal override void Render(GLControlTime ControlTime)
        {
            var proj = this.GraphicsDevice.Projection;
            proj.MatrixMode = MatrixMode.Projection;
            proj.PushMatrix();
            proj.LoadIdentity();
            proj.SetOrtho (-1, 1, -1, 1, -1, 1);
            proj.MatrixMode = MatrixMode.ModelView;
            proj.PushMatrix();
            proj.LoadIdentity();
            if (m_angle > 360)
                m_angle = 360 - m_angle;
            else
                m_angle++;
            proj.Rotate(m_angle, new Vector3f(0, 0, 1));
            GraphicsDevice.SetColor(this.FillColor);
            GraphicsDevice.Begin(IGK.OGLGame.Graphics.enuGraphicsPrimitives .Triangles );
            GraphicsDevice.SetColor(Colorf.Lime);
            GraphicsDevice.SetVertex(0.0f, 0.8f);
            GraphicsDevice.SetColor(Colorf.Yellow);
            GraphicsDevice.SetVertex(0.4f, -0.4f);
            GraphicsDevice.SetColor(Colorf.Red );
            GraphicsDevice.SetVertex(-0.4f, -0.4f);            
            GraphicsDevice.End();
            GraphicsDevice.Projection.PopMatrix();
            Vector3f[] d = new Vector3f[]{
                    new Vector3f (-0.8f, -0.8f,0.0f),
                    new Vector3f (-0.4f, 0.8f,0.0f),
                    new Vector3f (0.4f, -0.8f,0.0f),
                    new Vector3f (0.8f, 0.8f,0.0f)
                };
            OGLGame.Graphics.GLDrawUtils.DrawBezier(
                GraphicsDevice,
                enuGraphicsPrimitives .Points ,
                Colorf.White,
                100,
               d);
            //for (int i = 0; i < d.Length; i++)
            //{
            //    GLDrawUtils.FillRect(GraphicsDevice, 
            //        Colorf.White,  new Rectanglei(
            //            (int)d[i].X,
            //        (int)d[i].Y,
            //        (int)1f,
            //        (int)1f));
            //}
            proj.MatrixMode = MatrixMode.Projection;
            proj.PopMatrix();
        }
    }
}

