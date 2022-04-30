

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IVideoProject.cs
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
file:IVideoProject.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn
{
    public interface IVideoProject
    {
        string ProjectName { get; set; }
        /// <summary>
        /// save project to
        /// </summary>
        /// <param name="filename"></param>
        void SaveProject(string filename);
        /// <summary>
        /// get the imported files
        /// </summary>
        IVideoImportedFileCollections ImportedFiles { get; }
        event ImportedFileEventHandler FileImported;
        event ImportedFileEventHandler FileRemove;
    }
}

