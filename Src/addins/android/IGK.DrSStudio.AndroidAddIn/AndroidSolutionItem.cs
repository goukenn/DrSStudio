

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSolutionItem.cs
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// represent a android solution item
    /// </summary>
    public abstract class AndroidSolutionItem : 
        ICoreWorkingProjectSolutionItem  ,
        ICoreSerializerService
    {
        private string m_Name;
        private AndroidProject m_Project;

        public virtual void Open(ICoreSystemWorkbench bench)
        { 
        }

        public AndroidProject Project
        {
            get { return m_Project; }
            set
            {
                if (m_Project != value)
                {
                    m_Project = value;
                }
            }
        }
        /// <summary>
        /// get or set the name of this items
        /// </summary>
        public virtual string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
        public abstract string ImageKey
        {
            get;
        }



        ICoreWorkingProjectSolution ICoreWorkingProjectSolutionItem.Solution
        {
            get {
                return this.m_Project as ICoreWorkingProjectSolution;
            }
        }

        public virtual void Deserialize(IXMLDeserializer xreader)
        {
            
        }
        protected virtual string getName()
        {
            string n = this.GetType().Name;
            string k = "AndroidSolution";
            int i = n.IndexOf(k);
            if ((i>=0) && ((i + k.Length < n.Length )))
                n = n.Substring(i+ k.Length);
            return n;
        }

        public virtual void Serialize(IXMLSerializer xwriter)
        {
            string n = this.getName();
            xwriter.WriteStartElement(n);
            xwriter.WriteAttributeString("Id", this.Id);
            CoreXMLSerializerUtility.WriteAttributes(this, xwriter);
            CoreXMLSerializerUtility.WriteElements(this, xwriter);
            xwriter.WriteEndElement();
        }

        public string Id
        {
            get {
                return this.getName()+ this.GetHashCode();
            }
        }

        string ICoreWorkingProjectSolutionItem.Name {
            get {
                return this.Name;
            }
            set { 
            }
        }

        public bool IsValid
        {
            get { return true; }
        }
    }
}
