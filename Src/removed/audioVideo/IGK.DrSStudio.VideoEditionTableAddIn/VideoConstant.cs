

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoConstant.cs
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
file:VideoConstant.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn
{
    /// <summary>
    /// represent a video constant
    /// </summary>
    public static class VideoConstant
    {
        public const string CODEC_NAME = "VideoProject";
        public const string CODEC_MIMETYPE ="DRS/VideoProject";
        public const string CODEC_EXTENSIONS = "*"+ EXTENSIONS ;
        public const string EXTENSIONS = ".gkvidproj";
        public const string VIDEOEDITOR_SURFACENAME = "VideoEditorSurface";
        public const string EMPTY_PRJ_NAME = "EmptyVideoProject" + EXTENSIONS;
        public const string VIDEO_ENVIRONMENT = "VideoEditorEnvironment";
        public const string MENU_NEWVID_PROJECT ="File.New.VideoProject";
        public const string MENU_EDIT_IMPORT_VID = "Edit.ImportVideoFiles";
        public const string MENU_EDIT_BUILD = "Edit.Build";
        public const string MENU_VIDEOACTION = "VideoAction";
        public const string MENU_CLEAR_SOUNDS = MENU_VIDEOACTION+".ClearSound";
        public const string MENU_CLEAR_VIDEOS = MENU_VIDEOACTION + ".ClearVideo";
        public const string MENU_BUILD = MENU_VIDEOACTION + ".BuildVideo";
        public const string MENU_OPTIONS = MENU_VIDEOACTION + ".Options";
        public const string MENU_SAVE_DISPLAYED_PLAYERPIC = MENU_VIDEOACTION + ".SaveDisplayedPicture";
        public const string DLGT_IMPORT_FILE = "DLG.VideoEditor.ImportFile";
        public const string VIDEOPROJECTBUILDER_ID = "VideoBuilder";
        internal static string GetFilters()
        {
            return "Audio and Video|*.avi; *.wav";
        }
    }
}

