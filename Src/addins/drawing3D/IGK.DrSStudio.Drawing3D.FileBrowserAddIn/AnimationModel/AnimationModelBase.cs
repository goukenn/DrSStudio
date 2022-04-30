

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AnimationModelBase.cs
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
file:AnimationModelBase.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing3D.FileBrowser.AnimationModel
{

    using IGK.DrSStudio.Drawing3D.FileBrowser.WinUI;
    using IGK.ICore;
    using IGK.OGLGame.Graphics;
    public abstract class AnimationModelBase : IAnimationModel , IDisposable , I3DModel 
    {
        private string m_currentFile;
        private string m_nextFile;
        private string m_upFile;
        private string m_downFile;
        private string m_previousFile;
        private enuFileViewState m_State;
        private int m_speed;
        private IFBSurface m_Surface;

        private int m_Columns;
        /// <summary>
        /// Get or set the columns of this animation struct
        /// </summary>
        public int Columns
        {
            get { return m_Columns; }
            set
            {
                if (m_Columns != value)
                {
                    m_Columns = value;
                }
            }
        }

        public event EventHandler PropertyChanged;
        ///<summary>
        ///raise the PropertyChanged 
        ///</summary>
        protected virtual void OnPropertyChanged(EventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        /// <summary>
        /// get the attached model surface
        /// </summary>
        public IFBSurface Surface {
            get { return this.m_Surface; }
            internal set {
                this.m_Surface = value;
            }
        }
        /// <summary>
        /// get the state
        /// </summary>
        public enuFileViewState State {
            get {
                return this.m_State;
            }
            protected set {
                this.m_State =value ;
            }
        }
        public virtual bool CanGoUp {
            get {
                int v_i = this.Surface.SelectedFileIndex;
                if (v_i == 0)
                    return false;
                return (v_i - this.Columns) >= 0;
            }
        }
        public virtual bool CanGoDown
        {
            get
            {
                int v_i = this.Surface.SelectedFileIndex;
                return (v_i + this.Columns) <
                    this.Surface.Files.Count - 1;
            }
        }
        public virtual bool CanGoRight
        {
            get
            {
                return this.Surface.SelectedFileIndex > 0;
            }
        }
        public virtual bool CanGoLeft
        {
            get {
                int v_i = this.Surface.SelectedFileIndex;
                return (((v_i) >= 0) && (v_i < this.Surface.Files.Count - 1));
            }
        }
        #region IAnimationModel Members
        public string CurrentFile
        {
            get { return this.m_currentFile; }
            protected set { this.m_currentFile = value; }
        }
        public string NextFile
        {
            get { return this.m_nextFile; }
            protected set { this.m_nextFile = value; }
        }
        public string PreviousFile
        {
            get { return this.m_previousFile; }
            protected set { this.m_previousFile = value ; }
        }
        public string UpFile
        {
            get { return this.m_upFile; }
            protected set { this.m_upFile = value ; }
        }
        public string DownFile
        {
            get { return this.m_downFile ; }
            protected set { this.m_downFile =value ; }
        }
        public int Speed
        {
            get
            {
                return this.m_speed;
            }
            set
            {
                this.m_speed = value;
            }
        }
        public abstract void Render(OGLGraphicsDevice Device);
        public abstract void GoUp();
        public abstract void GoDown();
        public abstract void GoLeft();
        public abstract void GoRight();
        #endregion
        internal virtual void Refresh()
        {
        }
        public virtual void OrganiseTextures(OGLGraphicsDevice Device)
        {            
        }
        #region IDisposable Members
        public virtual void Dispose()
        {}
        #endregion
        #region I3DModel Members
        public abstract void Initialize(OGLGraphicsDevice device, Rectanglei view);
        #endregion
        static Type[] sm_models;
        static AnimationModelBase() {
            Type t = typeof (AnimationModelBase ) ;
            List<Type > v_t = new List<Type>();
            foreach (Type item in System.Reflection.Assembly .GetExecutingAssembly ().GetTypes ())
            {
                if (item.IsSubclassOf(t))
                {
                    v_t.Add(item);
                }
            }
            sm_models = v_t.ToArray();
        }
        public static Type[] GetModels() 
        {
            return sm_models;
        }
        internal protected virtual void InitView(OGLGraphicsDevice Device, Rectanglei view)
        {            
        }
    }
}

