

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FileNamePicker.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI.CommonDialog
{
    /// <summary>
    /// used to pick a file in
    /// </summary>
    public class FileNamePicker : IGK.ICore.WinUI.Common.FileNamePicker
    {
        private XForm m_frm;
        private WebBrowser m_browser;
        private ScriptingMode m_scriptObject;
        private string m_Filter;

        public override  string Filter
        {
            get { return m_Filter; }
            set
            {
                if (m_Filter != value)
                {
                    m_Filter = value;
                }
            }
        }

        [ComVisible(true)]
        public  class ScriptingMode : MarshalByRefObject 
        {
            private FileNamePicker fileNamePicker;

            public ScriptingMode(FileNamePicker fileNamePicker)
            {
                this.fileNamePicker = fileNamePicker;
            }
            public void setFileName(string filename, string outFolder)
            {
                if (!string.IsNullOrEmpty(filename))
                {
                    this.fileNamePicker.FileName = Path.Combine (outFolder ,  filename);
                    this.fileNamePicker.m_frm.DialogResult = enuDialogResult.OK;
                }
            }
            public string getOutputDir() {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        return fbd.SelectedPath;
                    }
                }
                return Application.StartupPath;
            }

        }

        public FileNamePicker()
        {
            this.m_scriptObject = new ScriptingMode(this);
            this.m_frm = new XForm();
            this.m_browser = new WebBrowser();
            this.m_browser.ObjectForScripting = this.m_scriptObject;
            this.m_browser.Dock = DockStyle.Fill;
            this.m_browser.AllowNavigation = false;
            this.m_browser.AllowWebBrowserDrop = false;
            this.m_browser.ScrollBarsEnabled = false;
            this.m_frm.SuspendLayout();
            this.m_frm.Controls.Add(this.m_browser);
            this.m_frm.ResumeLayout();
            this.m_frm.Load += _initOnLoad;
        }

        private void _initOnLoad(object sender, EventArgs e)
        {
            IGK.ICore.Xml.CoreXmlElement v_xe = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("html");
            var form = v_xe.Add("body").Add("div").Add("form");
            form["onsubmit"] = "window.external.setFileName(this.FileName.value, this.OutputDir.value); return false;";
            form.Add("label").Content = "lb.FileName".R();
            form.Add("input", new Dictionary<string,string>() {
                {"id", "FileName"},
                {"type", "text"},
                {"value", "filename"}
            });
            form.Add("label").Content = "lb.Outputdir".R();
            form.Add("input", new Dictionary<string, string>() {
                {"id", "OutputDir"},
                {"type", "text"},
                {"value", "c:\\output"}
            });
            form.Add("input", new Dictionary<string, string>() {
                {"id", "btn_getOuputDir"},
                {"type", "button"},
                {"value", "..."},
                {"onclick", "this.form.OutputDir.value = window.external.getOutputDir(); return false;"}
            });
            form.Add("input", new Dictionary<string, string>() {
                {"type", "submit"},
                {"value", "validate"}
            });
            string s = v_xe.RenderXML(null);
            this.m_browser.DocumentText = s;
        }
        public override string Title
        {
            get
            {
                return m_frm.Text;
            }
            set
            {
                this.m_frm.Text =value;
            }
        }

        public override enuDialogResult ShowDialog()
        {
            return this.m_frm.ShowDialog();
        }
        public override void Dispose()
        {
            base.Dispose();
            this.m_frm.Dispose();
        }
    }



}
