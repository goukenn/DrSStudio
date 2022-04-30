

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXProjectWriter.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.ICore.Xml;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXProjectWriter.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace IGK.DrSStudio.WiXAddIn
{
    /// <summary>
    /// represent a wix Project writer
    /// </summary>
    public class WiXProjectWriter : CoreXmlWriter
    {
        private StringBuilder sb;
        public WiXProjectWriter(StringBuilder sb, System.Xml.XmlWriterSettings setting):
            base(sb, setting )
        {            
            this.sb = sb;
        }
        public void Visit(WiXProject wiXProject)
        {
            this.WriteStartElement("IGKWixProject");
            foreach (PropertyInfo item in wiXProject.GetType().GetProperties ())
            {
                if (item.CanRead)
                {
                    object obj = item.GetValue(wiXProject);
                    if (obj != null)
                    {
                        this.WriteStartElement(item.Name);
                        this.Visit(obj);
                        this.WriteEndElement();
                    }
                }
            }
            this.WriteEndElement();
        }
        public void Visit(object obj)
        {
            if (obj != null)
            {             
                MethodBase.GetCurrentMethod().Visit(this, obj);
            }
        }

        public void Visit(bool target)
        {
            if (target)
            {
                this.WriteString("1");
            }
            else {
                this.WriteString("0");
            }
        }
        public virtual void Visit(string text)
        {
            this.WriteString(text);
        }
        public virtual void Visit(int text)
        {
            this.WriteString(text.ToString());
        }
        public virtual void Visit(float text)
        {
            this.WriteString(text.ToString());
        }
        public virtual void Visit(Version text)
        {
            this.WriteString(text.ToString());
        }
        public void Visit(WiXProjectVariables variable){
            if (variable == null)
                return;
            foreach(var b in variable.GetType().GetProperties())
            {
                var obj = b.GetValue(variable);
                if (obj != null) {
                    this.WriteElementString(b.Name, obj.ToString());
                }
            }
        }
        public virtual void Visit(WiXProject.WiXProjectExtensionCollection extensions)
        {
            foreach (IWiXProjectExtension item in extensions)
            {
                this.Visit(item);
            }
        }

        public virtual void Visit(WiXProject.FileCollections files)
        {
            foreach (var item in files)
            {
                this.Visit(item);
            }
        }

        public virtual void Visit(WiXDirectory directory)
        {
            this.WriteStartElement("Directory");
            this.WriteAttributeString("Name", directory.Id);
            this.WriteEndElement();
        }
        public virtual void Visit(WiXProjectFile file)
        {
            if (file.FileType == enuWiXFileType.Shortcut)
            {
                this.WriteStartElement("Shortcut");
                this.WriteAttributeString("Id", file.Id);
                this.WriteAttributeString("Name", file.FileName);
                this.WriteAttributeString("Description", file.Description);
                this.WriteAttributeString("Target", file.Target);
                this.WriteAttributeString("WorkingDirectory", file.WorkingDir);
                this.WriteEndElement();
            }
            else
            {
                if (file.IsDirectory)
                {
                    this.WriteStartElement("Directory");
                    this.WriteAttributeString("Name", file.Id);

                    if (file.ChildCount > 0)
                    {
                        foreach (var item in file)
                        {
                            this.Visit(item);
                        }
                    }
                    this.WriteEndElement();
                }
                else
                {
                    this.WriteStartElement("File");
                    this.WriteAttributeString("Name", file.Id);
                    this.WriteAttributeString("SourcePath", file.FileName);
                    this.WriteEndElement();
                }
            }
        }
        public virtual void Visit(Guid guid)
        {
            this.WriteString(guid.ToString());
        }
        public virtual void Visit(WiXRegistryKey key)
        { 

        }

    }
}

