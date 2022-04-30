

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PreviewHandlerControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:PreviewHandlerControl.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace IGK.PrevHandlerLib
{
    public abstract class PreviewHandlerControl : UserControl 
    {
        protected PreviewHandlerControl()
        {
        }
        /// <summary>
        /// override this to load the file
        /// </summary>
        /// <param name="file"></param>
        public new abstract void Load(FileInfo file);
        /// <summary>
        /// override this to load the stream
        /// </summary>
        /// <param name="stream"></param>
        public new abstract void Load(Stream stream);
        public virtual void Unload()
        {
            foreach (Control c in Controls) c.Dispose();
            Controls.Clear();
        }
        protected static string CreateTempPath(string extension)
        {
            return Path.GetTempPath() + Guid.NewGuid().ToString("N") + extension;
        }
    }
}

