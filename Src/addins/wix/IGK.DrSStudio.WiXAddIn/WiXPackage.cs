

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXPackage.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXPackage.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WiXAddIn
{
    [WiXDisplayNameAttribte("Package")]
    public class WiXPackage : WiXFeature 
    {
        private string m_Description;
        private string m_Comments;
        private string m_Manufacturer;
        private int m_InstallerVersion;
        private string m_Compressed;
        [WiXGuidAttribute ()]
        [WiXAttribute ()]
        public override string Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
            }
        }
        [WiXAttributeAttribute()]
        public string Compressed
        {
            get { return m_Compressed; }
            set
            {
                if (m_Compressed != value)
                {
                    m_Compressed = value;
                }
            }
        }
        [WiXAttributeAttribute()]
        public int InstallerVersion
        {
            get { return m_InstallerVersion; }
            set
            {
                if (m_InstallerVersion != value)
                {
                    m_InstallerVersion = value;
                }
            }
        }
        [WiXAttributeAttribute()]
        public string Manufacturer
        {
            get { return m_Manufacturer; }
            set
            {
                if (m_Manufacturer != value)
                {
                    m_Manufacturer = value;
                }
            }
        }
        [WiXAttributeAttribute()]
        public string Comments
        {
            get { return m_Comments; }
            set
            {
                if (m_Comments != value)
                {
                    m_Comments = value;
                }
            }
        }
        [WiXAttributeAttribute()]
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
        public WiXPackage()
        {
            this.Description = "Default Description";
            this.Comments = "no comment";
            this.Manufacturer = "IGKDEV";
            this.Compressed = "yes";
            this.InstallerVersion = 305;//minimum is 200
            this.Id = null;
        }
    }
}

