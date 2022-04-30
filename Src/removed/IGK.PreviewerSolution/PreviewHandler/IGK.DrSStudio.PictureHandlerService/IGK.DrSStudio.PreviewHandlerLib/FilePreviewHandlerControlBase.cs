

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FilePreviewHandlerControlBase.cs
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
file:FilePreviewHandlerControlBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace IGK.PrevHandlerLib
{
    public  abstract class FilePreviewHandlerControlBase : PreviewHandlerControl
    {
        public sealed override void Load(Stream stream)
        {
            string tempPath = Path.GetTempFileName();
            using (FileStream fs = File.OpenWrite(tempPath))
            {
                const int COPY_BUFFER_SIZE = 1024;
                byte[] buffer = new byte[COPY_BUFFER_SIZE];
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) != 0) fs.Write(buffer, 0, read);
            }
            Load(new FileInfo(tempPath));
        }
        protected static FileInfo MakeTemporaryCopy(FileInfo file)
        {
            string tempPath = CreateTempPath(Path.GetExtension(file.Name));
            using (FileStream to = File.OpenWrite(tempPath))
            using (FileStream from = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
            {
                byte[] buffer = new byte[4096];
                int read;
                while ((read = from.Read(buffer, 0, buffer.Length)) > 0) to.Write(buffer, 0, read);
            }
            return new FileInfo(tempPath);
        }
    }
}

