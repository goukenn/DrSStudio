

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MergerProgression.cs
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
file:MergerProgression.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// merging progression
    /// </summary>
    sealed class MergerProgression
    {
        private IMergeForm m_Form;
        private string m_FileName;
        private System.Threading .Thread  m_Tread;
        public System.Threading .Thread  Tread
        {
            get { return m_Tread; }
        }
        public string FileName
        {
            get { return m_FileName; }
        }
        public IMergeForm Form
        {
            get { return m_Form; }
        }
        private VideoFileProject m_VideoProject;
        public VideoFileProject VideoProject
        {
            get { return m_VideoProject; }
        }
        public MergerProgression(string filename, 
            IMergeForm form, 
            VideoFileProject project)
        {
            this.m_VideoProject = project;
            this.m_Form = form;
            this.m_FileName = filename;
        }
        public void Generate()
        {
            this.m_VideoProject.GenerateFile(this.FileName,
                updateProgression);
            //restotre defaul file text
            this.m_Form.BeginInvoke ((MethodInvoker )
                delegate (){
                    this.m_Form .LoadTitle();
                });
        }
        public long updateProgression(int i)
        {
            this.m_Form.BeginInvoke((MethodInvoker)delegate() {
                this.m_Form.Text = string.Format("Progression: {0}",
                    i);
            });
            return 0;
        }
        internal void BeginGenerate()
        {
            this.m_Tread = new System.Threading.Thread(this.Generate);
            this.m_Tread.IsBackground = false;
            this.m_Tread.Start();
        }
        internal void CancelGenerate()
        {
            if (System.Threading.Thread.CurrentThread ==
                this.m_Tread)
            {
            }
            this.m_Tread.Abort();
            this.m_Tread.Join();
        }
    }
}

