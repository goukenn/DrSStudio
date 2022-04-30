

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreThreadingManager.cs
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
file:CoreThreadingManager.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading ;
using System.Globalization;
namespace IGK.ICore.Threading
{
    public static class CoreThreadManager
    {
        static CultureInfo sm_cultureInfo;
        public static CultureInfo CultureInfo
        {
            get { return sm_cultureInfo; }
            set { sm_cultureInfo = value; }
        }
        static CoreThreadManager()
        {
            sm_cultureInfo = CultureInfo.CreateSpecificCulture("fr");
            sm_cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            sm_cultureInfo.NumberFormat.PercentDecimalSeparator = ".";

            
        }
        /// <summary>
        /// create a new thread
        /// </summary>
        /// <param name="start"></param>
        /// <param name="ThreadName"></param>
        /// <returns></returns>
        public static Thread CreateThread(ThreadStart start, string ThreadName)
        {
            Thread th = new Thread(start);
            th.CurrentCulture = CultureInfo;
            th.CurrentUICulture = CultureInfo;
            th.Name = (ThreadName == null)? "IGKThread" : ThreadName ;
            return th;
        }
        public  static void InitCurrentThread()
        {
            SetUpThread(Thread.CurrentThread);
        }
        private static void SetUpThread(Thread thread)
        {
            thread.CurrentCulture = CultureInfo;
            thread.CurrentUICulture = CultureInfo;
        }
        /// <summary>
        /// init current thread
        /// </summary>
        public static void Init()
        {
            SetUpThread(Thread.CurrentThread);
        }

        internal static Thread CreateThread(Action p, object tHREAD_STARTFORM)
        {
            throw new NotImplementedException();
        }
    }
}

