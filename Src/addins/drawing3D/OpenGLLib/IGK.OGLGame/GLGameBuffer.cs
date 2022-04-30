using IGK.GLLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame
{
    /// <summary>
    /// represent GLGame Buffer data.
    /// </summary>
    public class GLGameBuffer : IDisposable
    {
        private uint m_id;

        delegate void GLGENBUFFERPROC(uint count, ref uint refId);
        delegate void GLDELBUFFERPROC(uint count, uint[] refId);
        delegate void GLBUFFERDATAPROC(uint target, int size, float[] data, uint usage);
        delegate void GLBUFFERDATAPROC2(uint target, int size, IntPtr data, uint usage);
        delegate void GLVERTEXBUFFERDATAPROC(int index​, int size​, uint type​, bool normalized​, int stride​, float[] pointer​);
        static bool sm_initProc;
        static GLGENBUFFERPROC glGenBuffer;
        static GLDELBUFFERPROC glDeleteBuffer;
        static GLGENBUFFERPROC glGenVertexArrays;
        static GLDELBUFFERPROC glDeleteVertexArrays;
        static GLBUFFERDATAPROC glBufferData;
        static GLBUFFERDATAPROC2 glBufferData2;

        /// <summary>
        /// call this function first . to initialize buffer method requirement
        /// </summary>
        public static bool Init() {
            if (sm_initProc)
                return true;

            GL.GetProcedureInit();
            glGenBuffer = GL.GetProcedure<GLGENBUFFERPROC>("glGenBuffers");
            glBufferData = GL.GetProcedure<GLBUFFERDATAPROC>("glBufferData");
            glGenVertexArrays = GL.GetProcedure<GLGENBUFFERPROC>("glGenVertexArrays");
            glDeleteBuffer = GL.GetProcedure<GLDELBUFFERPROC>("glDeleteBuffers");
            glDeleteVertexArrays = GL.GetProcedure<GLDELBUFFERPROC>("glDeleteVertexArrays");
            glBufferData2 = GL.GetProcedure<GLBUFFERDATAPROC2>("glBufferData");


            GL.glDeleteBuffers(1, new uint[0]);

            if (!GL.GetProcedureInitEnd())
            {
                throw new OGLGameException(enuGLError.NotImplement,
                    string.Join(";", GL.GetProcedureInitErrors()));
            }
            sm_initProc = true;
            return sm_initProc;
        }
        public static GLGameBuffer Create() {
            uint refId = 0;//new uint[1];
          //  IntPtr h = GLMarshal.CreateTaskMemoryPointer(typeof(uint), 1);
         // var d = (GENBUFFERPROC)GL.GetProcedure ("glGenBuffers", typeof(GENBUFFERPROC));

           glGenBuffer(1,ref refId );
           // GL.glGenBuffers(1, ref refId);
           //refId =  (uint)Marshal.ReadInt32(h);//.h.ReadInt();
           // GLMarshal.FreeTaskMemoryPointer (h);

            if (refId== 0)
                return null;

            GLGameBuffer buffer = new GLGameBuffer ();
            buffer.m_id = refId ;
            return buffer;
        }

        /// <summary>
        /// handle of this buffer data
        /// </summary>
        public uint Handle {
            get { return this.m_id; }
        }

        public void Dispose()
        {
            if (this.m_id > 0)
            {
                GL.glDeleteBuffers(1, new uint[]{
                (uint)this.m_id
            });
                m_id = 0;


            }
        }
        /// <summary>
        /// copy data to graphics card pipeline
        /// </summary>
        /// <param name="v"></param>
        public void SetData(float[] v)
        {
            this.Bind ();
            glBufferData(GL.GL_ARRAY_BUFFER, sizeof (float) *  v.Length, v, GL.GL_STATIC_DRAW );    
        }
        /// <summary>
        /// generate and bind a vertex array buffer
        /// </summary>
        /// <returns></returns>
        public static OGLVertexArrayBuffer GenVertexArray()
        {
            uint vaoId=0;
            glGenVertexArrays(1, ref vaoId);
            if (vaoId != 0xFFFF)
            {
                OGLVertexArrayBuffer c = new OGLVertexArrayBuffer(vaoId);
                GL.glBindVertexArray(vaoId);
                return c;// vaoId;
            }
            return null;
        }

        public void Bind()
        {
            GL.glBindBuffer(GL.GL_ARRAY_BUFFER, this.m_id);
        }

        /// <summary>
        /// bind to 
        /// </summary>
        /// <param name="vertexBufferId">a vertex buffer id created with GenVertexArray</param>
        /// <param name="attribid"></param>
        /// <param name="count"></param>
        /// <param name="v2"></param>
        public void BindVertextAttribData(uint attribid, int count, float[] v2, int stride=0, int offset=0)
        {
            //passing data to current array buffer. create from glGenVertexArrays
            this.SetData(v2);   
            //setting up data localisation
            GL.glVertexAttribPointer(
               attribid,                  // attribute 0. No particular reason for 0, but must match the layout in the shader.
               count,                  // size
               GL.GL_FLOAT,           // type
               false,           // normalized?
               stride,                  // stride
               IntPtr.Zero + offset           // array buffer offset
            );

            //activate the attrib shader pointer
            GL.glEnableVertexAttribArray(attribid);
        }

        //public void LinkData(uint attribid, int count, IntPtr ptr, int stride=0, int offset=0) {
        //    GL.glBindBuffer(GL.GL_ARRAY_BUFFER, this.m_id);

        //    glBufferData2(GL.GL_ARRAY_BUFFER, sizeof(float) * 18, ptr, GL.GL_STATIC_DRAW);
        //    GL.glEnableVertexAttribArray(attribid);
        //    //i = GL.glGetError();

        //    GL.glVertexAttribPointer(
        //       attribid,                  // attribute 0. No particular reason for 0, but must match the layout in the shader.
        //       count,                  // size
        //       GL.GL_FLOAT,           // type
        //       false,           // normalized?
        //       stride,                  // stride
        //      IntPtr.Zero  //ptr+ offset           // array buffer offset
        //    );

        //}


        /// <summary>
        /// free static current sm_bufferid;
        /// </summary>
        public static void FreeVertexArrays(uint vertexBufferId)
        {
            if (vertexBufferId != 0) {
                glDeleteVertexArrays (1, new uint[] {
                    vertexBufferId
                });
            }
        }
        public static void BindVertexArrays(uint id) {
            GL.glBindVertexArray(id);
        }
        /// <summary>
        /// unbind this 
        /// </summary>
        public void UnBind()
        {
            GL.glBindBuffer(GL.GL_ARRAY_BUFFER, 0);
        }
    }
}
