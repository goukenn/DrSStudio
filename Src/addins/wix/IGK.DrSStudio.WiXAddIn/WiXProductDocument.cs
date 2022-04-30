

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXProductDocument.cs
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
file:WiXProductDocument.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn
{
    [WiXDisplayNameAttribte("Product")]
    /// <summary>
    /// represent a base document
    /// </summary>
    public class WiXProductDocument : WiXDocument , ICoreWorkingDocument
    {
      
        private string m_Language;
        private Version m_Version;
        private string m_Name;
        private string m_Manufacturer;
        private WiXPackage m_Package;
        private string m_UpgradeCode;
        private WiXDirectory m_Directory;
        private WiXFeatureEntry m_FeatureEntry;
        [Browsable(false)]
        public WiXFeatureEntry FeatureEntry
        {
            get
            {
                return this.m_FeatureEntry;
            }
        }
        [Browsable(false)]
        public WiXDirectory Directory {
            get {
                return this.m_Directory;
            }
        }
        [Browsable(false)]
        /// <summary>
        /// get the wix pakage info
        /// </summary>
        public WiXPackage Package {
            get {
                return this.m_Package;
            }
        }
       [WiXAttributeAttribute()]
       [WiXGuidAttribute ()]
        public string UpgradeCode
        {
            get { return m_UpgradeCode; }
            set
            {
                if (m_UpgradeCode != value)
                {
                    m_UpgradeCode = value;
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
        [WiXAttributeAttribute()]
        public Version Version
        {
            get { return m_Version; }
            set
            {
                if (m_Version != value)
                {
                    m_Version = value;
                }
            }
        }
        [WiXAttributeAttribute()]
        public string Language
        {
            get { return m_Language; }
            set
            {
                if (m_Language != value)
                {
                    m_Language = value;
                }
            }
        }
        [WiXAttributeAttribute()]
        [WiXGuidAttribute ()]        
        public override string Id
        {
            get { return base.Id ; }
            set
            {
                base.Id = value ;
            }
        }
        public WiXProductDocument()
        {
            this.Id = Guid.NewGuid().ToString ();
            this.UpgradeCode = Guid.NewGuid().ToString();
            this.Name = "IGKDEV WIX Package";
            this.Language = "1033";
            this.Version = new Version("1.0.0.0");
            this.Manufacturer = "IGKDEV";
            this.m_Package = new WiXPackage();
            this.m_Directory = new WiXDirectory();
            this.m_FeatureEntry = new WiXFeatureEntry();
            this.Features.Add(this.m_Package);
            this.Features.Add(new WiXMedia());
            this.Features.Add(this.m_Directory);
            this.Features.Add(this.m_FeatureEntry);
        }
    }
}

