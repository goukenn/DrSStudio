using IGK.DrSStudio.Balafon.Xml;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.WinUI
{
    using IGK.ICore;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;

    [CoreSurface("{D75243E9-7CC3-434F-852E-7FF8C7BBE8EA}")]
    public class BalafonEditorSurface : IGKXWinCoreWorkingFileManagerSurface, ICoreWorkingFilemanagerSurface 
    {
        private BalafonProject m_Project;

        public BalafonProject Project
        {
            get { return m_Project; }
        }
        /// <summary>
        /// load a balafon project
        /// </summary>
        /// <param name="p"></param>
        internal void LoadProject(BalafonProject p)
        {
            this.m_Project = p;
        }
        public BalafonEditorSurface()
        {
            this.Load += _Load;
        }

        private void _Load(object sender, EventArgs e)
        {
            if (this.Project !=null)
            this.Title = "title.balfoneditorsurface_1".R(this.Project.Name);
        }
        public override void LoadDisplayText()
        {
            if (this.Project != null)
                this.Title = "title.balfoneditorsurface_1".R(this.Project.Name);
            base.LoadDisplayText();
        }

    }
}
