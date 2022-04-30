

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCodecAttribute.cs
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
file:CoreCodecAttribute.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    /// <summary>
    /// represent the base class of drs codec attribute
    /// </summary>
    public class CoreCodecAttribute : CoreAttribute 
    {
        private string m_Name;
        private string m_Extension;
        private string m_MimeType;
        private string m_CaptionKey;
        private string m_Description;
        /// <summary>
        /// get or set the description of this codec
        /// </summary>
        public string Description
        {
            get { return m_Description; }
            set
            {
                if (m_Description != value)
                {
                    m_Description = value;
                }
            }
        }

        public string CaptionKey
        {
            get { return m_CaptionKey; }
            set
            {
                if (m_CaptionKey != value)
                {
                    m_CaptionKey = value;
                }
            }
        }
        /// <summary>
        /// get or set the mime type
        /// </summary>
        public string MimeType
        {
            get { return m_MimeType; }
            set { m_MimeType = value; }
        }
        /// <summary>
        /// get or set the Extensions. semi-column separated value for multi extension. exemple .gkds; .text
        /// </summary>
        public string Extensions
        {
            get { return m_Extension; }
            set { m_Extension = value; }
        }
        /// <summary>
        /// get or set the name of the codec
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private string m_Category;
        /// <summary>
        /// get or set the codec category
        /// </summary>
        public string Category
        {
            get { return m_Category; }
            set
            {
                if ((m_Category != value)&& !string.IsNullOrEmpty (value ))
                {
                    m_Category = value;
                }
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="name">name of this codec</param>
        /// <param name="mimetype">custom mimetype</param>
        /// <param name="extension">semi column extension separator exemple: gkds;ico;bmp</param>
        public CoreCodecAttribute(string name, string mimetype, string extension)
        {
            this.m_Name = name;
            this.m_MimeType = mimetype;
            this.m_Extension = extension;
            this.m_Category = string.Format("CAT_{0}", name);
            this.m_CaptionKey = string.Format("CODEC_", name);
        }
    }
}

