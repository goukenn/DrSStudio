

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoFileProject.cs
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
file:VideoFileProject.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore;using IGK.AudioVideo.AVI;
namespace IGK.DrSStudio
{
    public class VideoFileProject : IVideoFileProject 
    {
        int m_width = 0;
        int m_height = 0;
        int m_cType = 0;
        float m_fps = 0;
        VideoFileCollections m_vidFiles;
        private object m_AudioCodec;
        private object m_VideoCodec;
        public object VideoCodec
        {
            get { return m_VideoCodec; }
            set
            {
                if (m_VideoCodec != value)
                {
                    m_VideoCodec = value;
                }
            }
        }
        public object AudioCodec
        {
            get { return m_AudioCodec; }
            set
            {
                    m_AudioCodec = value;
            }
        }
        public VideoFileProject()
        {
            this.m_vidFiles = new VideoFileCollections(this);
        }
        #region IVideoFileProject Members
        public IVideoFileCollections<IVideoFile> VideoFiles
        {
            get {
                return m_vidFiles;
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public bool GenerateFile(string filename, AVISaveProgressionCallBack updateProgessCallBack)
        {            
            //AVIFile f = AVIFile.CreateFromFile(filename);
            AVIEditableStream v_evid = null;
            AVIEditableStream v_eaud = null;
            List<AVIStream> v_streams = new List<AVIStream> ();
            foreach (var item in this.VideoFiles)
	        {
                if (item.HasVideo)
                {
                    if (v_evid == null){
                        v_evid = AVIEditableStream.CreateFromHwnd(item.VidHandle);
                        v_streams .Add (v_evid );
                    }
                    else
                    {
                        v_evid.Paste(item.VidHandle, 0,
                            (int)AVIStream.GetStreamLength(item.VidHandle));
                    }
                }
                if (item.HasAudio)
                {
                    if (v_eaud == null){
                        v_eaud = AVIEditableStream.CreateFromHwnd(item.AudHandle);
                        v_streams .Add(v_eaud );
                    }
                    else
                        v_eaud.Paste(item.AudHandle, 0, (int)AVIStream.GetStreamLength(item.AudHandle));
                }
	        }
            bool v_result = false ;
            AVIFile file = AVIFile.Open (filename, enuAviAccess.Create | enuAviAccess.Write  );
            if (file != null)
            {
                for (int i = 0; i < v_streams.Count; i++)
                {
                    file.CopyStream(v_streams[i]);
                }
                file.Close();
            }
            //AVIFile v_f = AVIFile.CreateFileFromStreams (
            //    v_streams .ToArray ());
            //if (v_f != null)
            //{        
            //    v_result = v_f.SaveTo(filename, updateProgessCallBack);
            //}
            if (v_evid !=null)
                v_evid.Dispose();
            if (v_eaud != null)
                v_eaud.Dispose();
            //if (v_f != null) 
            //v_f.Dispose();
            return v_result;
        }
        #endregion
        class VideoFileCollections : IVideoFileCollections<IVideoFile>
        {
            List<IVideoFile> m_list;
            VideoFileProject m_project;
            public override string ToString()
            {
                return "VideoFileCollections : [" + this.Count + "] ";
            }
            public VideoFileCollections(VideoFileProject project)
            {
                m_project = project;
                m_list = new List<IVideoFile>();
            }
            #region IVideoFileCollections<IVideoFile> Members
            public int Count
            {
                get { return this.m_list .Count ; }
            }
            public void Add(IVideoFile item)
            {
                this.m_list.Add(item);
                this.m_project.OnItemAdded(new VideoItemEventArgs<IVideoFile >(item));
            }
            public void Remove(IVideoFile item)
            {
                this.m_list.Remove (item);
                this.m_project.OnItemRemoved(new VideoItemEventArgs<IVideoFile>(item));
            }
            #endregion
            #region IEnumerable<IVideoFile> Members
            public IEnumerator<IVideoFile> GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }
            #endregion
            #region IEnumerable Members
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }
            #endregion
            #region IVideoFileCollections<IVideoFile> Members
            public IVideoFile this[int index]
            {
                get { return this.m_list[index]; }
            }
            #endregion
            #region IVideoFileCollections<IVideoFile> Members
            public void Clear()
            {
                foreach (var item in this.m_list)
                {
                    item.Dispose();
                }
                this.m_list.Clear();
            }
            #endregion
        }
        public event VideoItemEventHandler<IVideoFile > ItemAdded;
        public event VideoItemEventHandler<IVideoFile> ItemRemoved;
        ///<summary>
        ///raise the ItemRemoved 
        ///</summary>
        protected virtual void OnItemRemoved(VideoItemEventArgs<IVideoFile > e)
        {
            if (ItemRemoved != null)
                ItemRemoved(this, e);
        }
        ///<summary>
        ///raise the ItemAdded 
        ///</summary>
        protected virtual void OnItemAdded(VideoItemEventArgs<IVideoFile> e)
        {
            if (this.VideoFiles.Count == 1)
            {
                this.m_width = this.VideoFiles[0].Width;
                this.m_height = this.VideoFiles[0].Height;
                this.m_cType = this.VideoFiles[0].Compression ;
                this.m_fps = this.VideoFiles[0].Fps ;
            }
            if (ItemAdded != null)
                ItemAdded(this, e);
        }
        #region IVideoFileProject Members
        public int Width
        {
            get { return this.m_width; }
        }
        public int Height
        {
            get { return this.m_height; }
        }
        #endregion
        internal bool CanAdd(VideoFile vid)
        {
            if (this.m_vidFiles .Count == 0)
                return true ;
            return (vid.Width == this.Width) &&
                (vid.Height == this.Height);
        }
    }
}

