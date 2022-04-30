

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XCursor.cs
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
file:XCursor.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.IO.Files
{
    /// <summary>
    /// represent a cursor class
    /// </summary>
    public class XCursor : IDisposable, ICoreCursor
    {
        private IntPtr m_Handle;
        /// <summary>
        /// get the cursor handle
        /// </summary>
        public IntPtr Handle
        {
            get { return m_Handle; }
        }
        public static XCursor  CreateFromHWND(IntPtr cursor)
        {
            if (cursor != IntPtr.Zero)
            {
                XCursor c = new XCursor();
                c.m_Handle = cursor;
                return c;
            }
            return null;
        }
        public readonly static XCursor Default;

        static XCursor() { 

        }
        public void Dispose()
        {
        }
    }
}

