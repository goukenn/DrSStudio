

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IVideoEditorSurface.cs
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
file:IVideoEditorSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.WinUI
{
    /// <summary>
    /// represent a video editor surface
    /// </summary>
    public interface IVideoEditorSurface : IGK.DrSStudio.WinUI.ICoreWorkingSurface, IVideoEditor
    {
        /// <summary>
        /// get the imported files
        /// </summary>
        IVideoImportedFileCollections ImportedFiles { get; }
        /// <summary>
        /// get or set the video project
        /// </summary>
        IVideoProject VideoProject { get; set; }
        /// <summary>
        /// save the video project
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool SaveProject(string filename);
        /// <summary>
        /// build video project to avi or wav according to extendsion
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool BuildVideoProject(string filename);
    }
}

