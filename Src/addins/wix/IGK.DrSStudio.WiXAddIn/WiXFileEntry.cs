

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXFileEntry.cs
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
file:WiXFileEntry.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WiXAddIn
{
    [WiXDisplayNameAttribte ("File")]
    public class WiXFileEntry : WiXDirectory 
    {
        private string m_Source;
        protected override bool Support(WiXEntry e)
        {
            return false;
        }
        private string m_DiskId;
        [WiXAttribute()]
        public string DiskId
        {
            get { return m_DiskId; }
            set
            {
                if (m_DiskId != value)
                {
                    m_DiskId = value;
                }
            }
        }
        [WiXAttribute()]
        public string Source
        {
            get { return m_Source; }
            set
            {
                if (m_Source != value)
                {
                    m_Source = value;
                }
            }
        }
        public WiXFileEntry()
        {
            this.m_DiskId = "1";
            this.Id = "File_" + this.GetHashCode();
        }
        public WiXFileEntry(string id, string name, string source):this()
        {
            this.Id = id;
            this.Name = name;
            this.Source = source;            
        }
    }
}

