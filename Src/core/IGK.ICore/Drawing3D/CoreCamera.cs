

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCamera.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;using IGK.ICore.Resources;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreCamera.cs
*/
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing3D
{
    /// <summary>
    /// represent the camera 
    /// </summary>
    public class CoreCamera : 
        MarshalByRefObject, 
        ICoreCamera,
        ICoreWorkingDefinitionObject,
        IDisposable 
    {
        private Matrix m_modelviewMatrix;
        private Matrix m_projectMatrix;
        private Vector3f m_location;
        private Rectanglei m_Viewport;
        private string m_Id;
        public Rectanglei Viewport
        {
            get { return m_Viewport; }
            set
            {
                if (!m_Viewport.Equals(value))
                {
                    m_Viewport = value;
                }
            }
        }
        #region ICamera Members
        public Vector3f Location
        {
            get
            {
                return this.m_location;
            }
            set
            {
                this.m_location = value;
            }
        }
        public Matrix Projection
        {
            get { return this.m_projectMatrix; }
        }
        public Matrix ModelView
        {
            get { return this.m_modelviewMatrix; }
        }
        #endregion
        public CoreCamera()
        {
            this.m_modelviewMatrix = new Matrix();
            this.m_projectMatrix = new Matrix();
            this.m_location = Vector3f.Zero;
            this.m_projectMatrix.Reset();
            this.m_projectMatrix.Ortho(-1f, 1f, -1, 1, -1, 1);
        }
        public string  GetDefinition(IGK.ICore.Codec.IXMLSerializer xwriter)
        {
            return this.GetDefinition();
        }
        #region ICoreWorkingDefinitionObject Members
        public string GetDefinition()
        {
            StringBuilder sb = new StringBuilder();
            if (!this.ModelView .IsIdentity )
            sb.Append (string.Format ("ModelView:{0};",Matrix.ToString( this.ModelView )));
            if (!this.Projection .IsIdentity )
            sb.Append (string.Format ("ProjectionView:{0};",Matrix.ToString( this.ModelView )));
            sb.Append(string.Format("Viewport:{0} {1} {2} {3};", this.Viewport.X ,this.Viewport.Y , this.Viewport.Width , this.Viewport.Height ));
            return sb.ToString();
        }
        public void CopyDefinition(string str)
        {
            string[] t = str.Split(new char[] { ';', ';' });
            string[] v_t = null;
            for (int i = 0; i < t.Length; i += 2)
            {
                switch (t[i].ToLower())
                {
                    case "modelview":
                        v_t = t[i + 1].Trim().Split(' ');
                        this.m_modelviewMatrix = Matrix.ConvertFromString(v_t);
                        break;
                    case "projectionview":
                        this.m_projectMatrix = Matrix.ConvertFromString(v_t);
                        break;
                    case "viewport":
                        v_t = t[i + 1].Trim().Split(' ');
                        if (v_t.Length == 4)
                        {
                            try
                            {
                                this.m_Viewport = new Rectanglei(
                                    Convert.ToInt32(v_t[0]),
                                    Convert.ToInt32(v_t[1]),
                                    Convert.ToInt32(v_t[2]),
                                    Convert.ToInt32(v_t[0]));
                            }
                            catch (Exception ex)
                            {
#if DEBUG
                                CoreMessageBox.Show(ex, "Exception");
#else
                                CoreMessageBox.Show(ex.Message, CoreResources.GetString (CoreConstant.TITLE_EXCEPITON_KEY));
#endif
                            }
                        }
                        break;
                }
            }
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { 
                if (string.IsNullOrEmpty (this.m_Id ))
                {
                    this.m_Id = "CoreCamera_"+this.GetHashCode ();
                }
                return this.m_Id; 
            }
        }
        #endregion
        public void Dispose()
        {
            this.m_modelviewMatrix.Dispose();
            this.m_projectMatrix.Dispose();
        }
    }
}

