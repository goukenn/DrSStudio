

using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePathDecoder.cs
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
file:CorePathDecoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Codec
{
    /// <summary>
    /// using for extra files
    /// </summary>
    internal class CorePathDecoder : ICoreDecoder
    {
        public override string ToString()
        {
            return string.Format("PathDecode : [{0}]", GetFilter());
        }
        #region ICoreDecoder Members
        ICoreCodecExtensionCollections m_extensions;
        public ICoreCodecExtensionCollections Extensions
        {
            get { return m_extensions; }
        }
        public bool Open(ICoreWorkbench bench, string filename,  bool selectCreatedSurface)
        {
            if (System.IO.File.Exists(this.Path))
            {
                System.Diagnostics.Process.Start(this.Path, filename);
                return true;
            }
            return false;
        }
        #endregion
        #region ICoreCodec Members
        public string Category
        {
            get { return "Special"; }
        }
        public string MimeType
        {
            get { return "User/" + this.Extension; }
        }
        public string GetFilter()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0}|{1}", this.MimeType, this.Extension));
            return sb.ToString();
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return this.Extension; }
        }
        #endregion
        private string m_Extension;
        private string m_Path;
        public string Path
        {
            get { return m_Path; }
            set
            {
                if (m_Path != value)
                {
                    m_Path = value;
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
        public CorePathDecoder(string extension, string path)
        {
            if (string.IsNullOrEmpty(extension))
                throw new CoreException(enuExceptionType.ArgumentIsNull, "extension");
            if (!System.IO.File.Exists (path ))
                throw new CoreException(enuExceptionType.ArgumentNotValid, "path");
            CodecExtensionCollections c = new CodecExtensionCollections();
            c.Add(extension);
            m_extensions = c;
            this.Extension = extension;
            this.Path = path;
        }
    }
}

