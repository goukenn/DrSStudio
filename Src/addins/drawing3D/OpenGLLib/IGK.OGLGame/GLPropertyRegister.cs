

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLPropertyRegister.cs
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
file:GLPropertyRegister.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
namespace IGK.OGLGame
{
    /// <summary>
    /// represnent a gl property regition 
    /// </summary>
    public sealed class GLPropertyRegister
    {
        static Dictionary<Type, GLPropertyCollection> sm_property;
        static GLPropertyRegister() {
            sm_property = new Dictionary<Type, GLPropertyCollection>();
        }
        public static GLProperty Register(string name, object defaultV)
        {
            GLProperty p = new GLProperty();
            p.DefaultValue = defaultV;
            StackTrace trace = new StackTrace();
            StackFrame[] frames = trace.GetFrames();
            if (frames.Length > 1)
            {
                System.Reflection.MethodBase minfo =
                    System.Reflection.MethodInfo.GetCurrentMethod();
                Type b = frames[1].GetMethod().DeclaringType;
                Type c = minfo.ReflectedType;
                GLPropertyCollection cl;
                if (!sm_property.ContainsKey(b))
                {
                    cl = new GLPropertyCollection();
                    sm_property.Add(b, cl);
                    cl.Add(p);
                }
                else
                {
                    sm_property[b].Add(p);
                }
            }
            return p;
        }
        public static GLProperty Register(string name) {
            GLProperty p = new GLProperty();
            p.DefaultValue = null;
            StackTrace trace = new StackTrace();
            StackFrame[] frames=  trace.GetFrames();
            if (frames.Length > 1)
            {
                System.Reflection.MethodBase minfo =
                    System.Reflection.MethodInfo.GetCurrentMethod();
                Type b = frames[1].GetMethod().DeclaringType;
                Type c = minfo.ReflectedType;
                GLPropertyCollection cl;
                if (!sm_property.ContainsKey(b))
                {
                    cl = new GLPropertyCollection();
                    sm_property.Add(b, cl);
                    cl.Add(p);
                }
                else
                {
                    sm_property[b].Add(p);
                }
            }
            return p;
        }
        public static System.Collections.IEnumerator  GetProperties(Type type)
        {
            if (sm_property.ContainsKey(type))
            {
                return sm_property[type].GetEnumerator();
            }
            return null;
        }
    }
}

