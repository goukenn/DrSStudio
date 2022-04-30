using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IGK.DrSStudio.WiXAddIn.Menu.WiX
{

    /// <summary>
    /// represent a wix genarator object
    /// </summary>
    sealed class NewWixGenerator
    {
        Thread m_th;
        string m_filename;
        WiXProject m_project;
        public NewWixGenerator(WiXProject project, string filename)
        {
            this.m_th = new Thread(_run);
            this.m_filename = filename;
            this.m_project = project;
        }
        public void Generate()
        {
            this.m_th.Start();
        }
        void Abort()
        {
            this.m_th.Abort();
        }
        void _run()
        {
            this.m_project.GenerateTo(this.m_filename);
        }
        internal void GenerateWiXobject()
        {
            this.m_project.GenerateWixObject(this.m_filename);
        }
    }
}
