

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImportedFileEventHandler.cs
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
file:ImportedFileEventHandler.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn
{
    public delegate void ImportedFileEventHandler(object sender, ImportedFileEventArgs e);
    public class ImportedFileEventArgs : EventArgs 
    {
        private IVideoImportedFile  m_File;
        public IVideoImportedFile  File
        {
            get { return m_File; }
        }
        public ImportedFileEventArgs(IVideoImportedFile file)
        {
            this.m_File = file;
        }
        public override string ToString()
        {
            return string.Format ("Imported["+this.File.ToString()+"]");
        }
    }
}

