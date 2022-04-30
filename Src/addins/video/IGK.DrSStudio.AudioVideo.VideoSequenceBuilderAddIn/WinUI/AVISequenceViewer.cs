
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.WinUI
{
    using IGK.ICore;
    using IGK.ICore.WinCore;
    using IGK.DrSStudio.AudioVideo.Xml;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.Dispatch;
    /// <summary>
    /// represent a avi sequece viewer
    /// </summary>
    public class AVISequenceViewer : IGKXPanel, IAVISequenceViewer
    {
        private AVISequenceTitlePane c_title;

        public AVISequenceViewer()
        {
            InitializeComponent();
            this.m_dispatcher = new WinCoreControlDispatcher(this);
            this.c_title = new AVISequenceTitlePane(this);
            this.Paint += _Paint;
        }
        void _Paint(object sender, CorePaintEventArgs e)
        {
            c_title.Draw(e.Graphics);
        }

        private void InitializeComponent()
        {
            
        }

        private IAVISequenceProject  m_Project;

        public IAVISequenceProject  Project
        {
            get { return m_Project; }
            set
            {
                if (m_Project != value)
                {
                    m_Project = value;
                    OnProjectChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ProjectChanged;
        private ICore.WinUI.Dispatch.ICoreDispatcher m_dispatcher;

        protected virtual void OnProjectChanged(EventArgs e)
        {
            if (ProjectChanged != null)
            {
                ProjectChanged(this, e);
            }
        }




        public void LoadProjectFile(string filename)
        {
            AVISequenceProject p = AVISequenceProject.CreateFromFile(filename);
            if (p != null)
                this.Project = p;
        }



        public ICore.WinUI.Dispatch.ICoreDispatcher Dispatcher
        {
            get {
                return this.m_dispatcher;
            }
        }
    }
}
