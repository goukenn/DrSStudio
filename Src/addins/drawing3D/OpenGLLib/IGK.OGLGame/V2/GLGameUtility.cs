using IGK.GLLib;
using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame.V2
{
    public class GLGameUtility
    {
        public static OGLGraphicsDevice InitDevice(IntPtr hwnd, bool extendContent, int major , int minor )
        {
            var g = OGLGraphicsDevice.CreateDeviceFromHWND(hwnd);
            OGLGraphicsDevice v_device = null;
            if ((extendContent) && (g != null))
            {
                int[] list = new int[]{
                    (int)WGL.WGL_CONTEXT_MAJOR_VERSION_ARB , major,
                    (int)WGL.WGL_CONTEXT_MINOR_VERSION_ARB , minor,
                    (int)WGL.WGL_CONTEXT_FLAGS_ARB , 0,
                    0
                };
                WGL.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
                g.Dispose();
                var hdc = OGLGraphicsDevice.CreateHDCFromHwnd(hwnd);
                var shared = IntPtr.Zero;
                var c = WGL.wglCreateContextAttribsARB(hdc, shared, list);
                if (c != IntPtr.Zero)
                {
                    v_device = OGLGraphicsDevice.CreateFromGLDC(hwnd, hdc, c);
                }

            }
            else {
                v_device = g;
            }
            return v_device;
        }
        
    }
}
