using System;
using IGK.OGLGame;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using IGK.GLLib;

namespace IGK.OGLGame
{
    /// <summary>
    /// represent a game device procedure to initialize  
    /// </summary>
    public sealed class OGLGameDeviceProcs
    {
        /// <summary>
        /// OpenGL 4.4 requirement proc for core profile. 
        /// </summary>
        /// <param name="hdc">hdc to create device</param>
        /// <returns>return an openGL device if created or null</returns>
        public static IntPtr OpenGL_4_4_Proc(IntPtr hdc)
        {
            return CreateDeviceProc(hdc, 4, 4, enuGLDeviceProcProfile.Core );
        }

        /// <summary>
        /// Create device global
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <returns></returns>
        public static IntPtr CreateDeviceProc(IntPtr hdc, int major, int minor, enuGLDeviceProcProfile profile)
        {
           IntPtr shared = IntPtr.Zero;
            List<int> prof = new List<int> ();
            //  int i =  glewInit();
            try
            {
                if (glewIsSupported("WGL_ARB_create_context_profile"))
                {//echeck if wgl
                    prof.Add(WGL_CONTEXT_PROFILE_MASK_ARB);
                    //   prof.Add(WGL_CONTEXT_CORE_PROFILE_BIT_ARB);

                    // prof.Add(1);
                    prof.Add((int)
                        profile);

                }
            }
            catch {
                //method not found to check if WGL_ARG_create_context_profile
                prof.Add(WGL_CONTEXT_PROFILE_MASK_ARB);
                //   prof.Add(WGL_CONTEXT_CORE_PROFILE_BIT_ARB);
                // prof.Add(1);
                prof.Add((int)
                    profile);
            }

            prof.AddRange(new int[] {
                    (int)IGK.GLLib.WGL.WGL_CONTEXT_MAJOR_VERSION_ARB, major ,
                    (int)IGK.GLLib.WGL.WGL_CONTEXT_MINOR_VERSION_ARB, minor ,
                    (int)IGK.GLLib.WGL.WGL_CONTEXT_FLAGS_ARB, 0 ,
                    0
                });
            IntPtr v_gldc = IGK.GLLib.WGL.wglCreateContextAttribsARB(hdc,
                shared,
                prof.ToArray ()
              );
            //int h = 0;
            //GL.glGetIntegerv( (int)IGK.GLLib.WGL.WGL_CONTEXT_MAJOR_VERSION_ARB, ref h);
            //GL.glGetIntegerv( (int)IGK.GLLib.GL.GL_MAJOR_VERSION, ref h);
            //int vProfileMask = 0;
            //GL.glGetIntegerv( WGL_CONTEXT_PROFILE_MASK_ARB, ref vProfileMask);
            int vProfileMask = GLUtils.GetIntegerv(WGL_CONTEXT_PROFILE_MASK_ARB);

            return v_gldc ;
        }

        const int WGL_CONTEXT_PROFILE_MASK_ARB = 0x9126;
        const int WGL_CONTEXT_CORE_PROFILE_BIT_ARB = 1;
        const int WGL_CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB = 2;

        const string glewlib="glew32.dll";
        [DllImport(glewlib)]
        private extern static bool glewIsSupported(string v);
        [DllImport(glewlib)]
        private extern static int glewInit();

        public static OGLGameCreateDeviceProc CreateProc(int v1, int v2, enuGLDeviceProcProfile profile)
        {
            return (hdc)=> {
                return CreateDeviceProc(hdc, v1, v2, profile);
            };
        }
    }
}