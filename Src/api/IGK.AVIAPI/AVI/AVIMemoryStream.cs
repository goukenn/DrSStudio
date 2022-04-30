

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIMemoryStream.cs
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
file:AVIMemoryStream.cs
*/
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//namespace IGK.AVIApi.AVI
//{
//    public class AVIMemoryStream : AVIStream 
//    {
//        private AVIMemoryStream():base()
//        {
//        }
//        /// <summary>
//        /// create a memory stream
//        /// </summary>
//        /// <returns></returns>
//        public static AVIMemoryStream CreateStream()
//        {
//            IntPtr h = IntPtr.Zero;
//            IntPtr f = IntPtr.Zero;
//             enuAviOpenError j = (enuAviError)AVIApi.AVIStreamCreate(ref h, 0, 0,0);
//             if (h !=  enuAviOpenError .NoError )
//             {
//                 AVIMemoryStream sm = new AVIMemoryStream();
//                 sm.Handle = h;
//                 return sm;
//             }
//             return null;
//        }
//        public override void Dispose()
//        {
//            enuAviOpenError i = AVIApi.AVIStreamRelease(this.Handle);
//            if (i==0)
//            {
//                this.Handle = IntPtr.Zero;
//            }
//        }
//    }
//}

