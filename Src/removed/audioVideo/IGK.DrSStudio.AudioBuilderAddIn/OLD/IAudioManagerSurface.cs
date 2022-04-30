

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IAudioManagerSurface.cs
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
file:IAudioManagerSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioBuilder
{
    public interface IAudioManagerSurface  :
        IGK.DrSStudio.WinUI.ICoreWorkingRecordableSurface
    {
        event EventHandler CurrentTrackChanged;
        event TrackEventHandler TrackAdded;
        event TrackEventHandler TrackRemoved;
        IAudioTrack CurrentTrack { get; set; }
        IAudioTrackCollections Tracks { get; }
        bool LoadAudioFile(string filename);
        void Play();
        void Pause();
        void Record();
        void MoveToNextTrack();
        void MoveToPreviousTrack();
        void AddNewTrack();     
    }
}

