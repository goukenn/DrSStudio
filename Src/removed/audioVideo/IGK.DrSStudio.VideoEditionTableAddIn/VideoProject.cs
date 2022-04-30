

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoProject.cs
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
file:VideoProject.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn
{
    public class VideoProject : IVideoProject 
    {
        private IVideoImportedFileCollections m_importedFiles;
        public event ImportedFileEventHandler FileImported;
        public event ImportedFileEventHandler FileRemove;
        internal protected virtual void OnFileImported(ImportedFileEventArgs e)
        {
            if (this.FileImported != null)
                this.FileImported(this, e);
        }
        internal protected virtual void OnFileRemove(ImportedFileEventArgs e)
        {
            if (this.FileRemove != null)
                this.FileRemove(this, e);
        }
        class VideoProjectImportedFileCollections : IVideoImportedFileCollections
        {
            private List<IVideoImportedFile> m_files;
            private VideoProject m_project;
            public VideoProjectImportedFileCollections(VideoProject project)
            {
                this.m_project = project;
                this.m_files = new List<IVideoImportedFile>();
            }
            #region IVideoImportedFileCollections Members
            public int Count
            {
                get { return this.m_files.Count ; }
            }
            public void Clear()
            {
                this.m_files.Clear();
            }
            public void Add(IVideoImportedFile file)
            {
                if ((file == null) || (m_files.Contains(file)))
                    return;
                this.m_files.Add(file);
                this.m_project .OnFileImported (new ImportedFileEventArgs (file ));
            }
            public void Remove(IVideoImportedFile file)
            {
                if (this.m_files.Contains(file))
                {
                    this.m_files.Remove(file);
                    this.m_project.OnFileRemove(new ImportedFileEventArgs(file));
                }
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_files.GetEnumerator();
            }
            #endregion
        }
        #region IVideoProject Members
        private string m_ProjectName;
        public string ProjectName
        {
            get { return m_ProjectName; }
            set
            {
                if (m_ProjectName != value)
                {
                    m_ProjectName = value;
                }
            }
        }
        public void SaveProject(string filename)
        {
        }
        public IVideoImportedFileCollections ImportedFiles
        {
            get { return m_importedFiles; }
        }
        #endregion
        public virtual IVideoImportedFileCollections CreateImportedFileCollections()
        {
            return new VideoProjectImportedFileCollections(this);
        }
        /// <summary>
        /// .ctr video project
        /// </summary>
        public VideoProject()
        {
            this.m_importedFiles = CreateImportedFileCollections();
            this.m_ProjectName = VideoConstant.EMPTY_PRJ_NAME;
        }
    }
}

