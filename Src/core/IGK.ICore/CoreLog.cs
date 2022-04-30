

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreLog.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace IGK.ICore
{
#pragma warning disable 649
    /// <summary>
    /// log wrapper
    /// </summary>
    public static class CoreLog
    {
        private static ICoreLog sm_Log;
        [Conditional("DEBUG")]
        public static void WriteDebug(string message)
        {
#if DEBUG
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            var meth = stackTrace.GetFrame(1).GetMethod();
            string methName = meth.Name;
            
            System.Diagnostics.Debug.WriteLine (string.Format("[IGK.ICore] - {2}:{0}(): --> {1}", methName,
                message,
                meth.DeclaringType.FullName ));
#endif
        }

        [Conditional("DEBUG")]
        public static void WriteDebug(string tag, string message)
        {
            #if DEBUG
            System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
            string methName = stackTrace.GetFrame(1).GetMethod().Name;
            System.Diagnostics.Debug.WriteLine( string.Format("[{2}] - {0}(): --> {1}", methName,message,
                tag), tag);
#endif
        }

        [Conditional("DEBUG")]
        public static void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }
        [Conditional("DEBUG")]
        public static void WriteDebugLine(string message)
        {
            Debug.WriteLine(message);
        }
        [Conditional("DEBUG")]
        public static void WriteError(string p)
        {
            if (sm_Log != null)
            {
                sm_Log.WriteError(p);
            }
        }
    }
}

