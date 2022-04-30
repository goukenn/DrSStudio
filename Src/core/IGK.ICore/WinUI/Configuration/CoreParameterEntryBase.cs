

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreParameterEntryBase.cs
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
file:CoreParameterEntryBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI.Configuration
{
    /// <summary>
    /// represent the base corer parameter entry base
    /// </summary>
    public abstract class CoreParameterEntryBase : ICoreParameterEntry 
    {
        string m_name;
        string m_captionKey;
        private ICoreParameterEntry m_Parent;
        /// <summary>
        /// get the parent host
        /// </summary>
        public virtual ICoreDialogToolRenderer Host
        {
            get { 
                if (this.m_Parent!=null)
                return m_Parent.Host;
                return null;
            }            
        }
        public CoreParameterEntryBase  Parent
        {
            get { return m_Parent as CoreParameterEntryBase; }
            set
            {
                if (m_Parent != value)
                {
                    m_Parent = value;
                }
            }
        }
        public string Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }
        public string CaptionKey
        {
            get { return this.m_captionKey; }
            set { this.m_captionKey = value; }
        }
        protected CoreParameterEntryBase()
        {
        }
        protected CoreParameterEntryBase(string name, string captionKey)
        {
            this.m_name = name;
            this.m_captionKey = captionKey;
        }
        ICoreParameterEntry ICoreParameterEntry.Parent
        {
            get { return this.Parent; }
            set { this.m_Parent = value; }
        }
    }
}

