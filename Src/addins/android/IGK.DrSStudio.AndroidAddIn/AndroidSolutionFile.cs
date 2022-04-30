

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSolutionFile.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// represent a android file
    /// </summary>
    public class AndroidSolutionFile : AndroidSolutionItem 
    {
        private string m_FileName;

        public override void Deserialize(IXMLDeserializer xreader)
        {
            base.Deserialize(xreader);
        }
        public override void Serialize(IXMLSerializer xwriter)
        {
            xwriter.WriteStartElement ("File");
                xwriter.WriteStartElement("Path");
                    xwriter.WriteValue(this.FileName);
                xwriter.WriteEndElement();

            xwriter.WriteEndElement();
        }
        /// <summary>
        /// get or set the filename
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                    this.Name = Path.GetFileName(this.FileName);
                }
            }
        }
     
        public AndroidSolutionFile(AndroidProject project, string filename)
        {
            this.Project = project;
            this.FileName = filename;            
        }

        public override void Open(ICoreSystemWorkbench bench)
        {
            if (this.Project != null)
            {
                this.Project.Open(bench, this);
            }
            else
            {
                Process.Start(this.FileName);
            }
        }

        public override string ImageKey
        {
            get {
                return string.Format ("android_file{0}",Path.GetExtension(this.FileName).Replace(".", "_"));
            }
        }
    }
}
