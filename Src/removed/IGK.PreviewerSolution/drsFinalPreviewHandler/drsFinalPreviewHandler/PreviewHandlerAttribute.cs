

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PreviewHandlerAttribute.cs
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
file:PreviewHandlerAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.PreviewHandler
{
    [AttributeUsage (AttributeTargets.Class )]
    class PreviewHandlerAttribute : Attribute 
    {
        private string m_Extension;
        private string m_Guid;
        private string m_Name;
        private string m_AppId;
        public string AppId
        {
            get { return m_AppId; }
            set
            {
                if (m_AppId != value)
                {
                    m_AppId = value;
                }
            }
        }
        public string Name
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
        public string Guid
        {
            get { return m_Guid; }
            set
            {
                if (m_Guid != value)
                {
                    m_Guid = value;
                }
            }
        }
        public string Extension
        {
            get { return m_Extension; }
            set
            {
                if (m_Extension != value)
                {
                    m_Extension = value;
                }
            }
        }
        public PreviewHandlerAttribute()
        {
        }
    }
}

