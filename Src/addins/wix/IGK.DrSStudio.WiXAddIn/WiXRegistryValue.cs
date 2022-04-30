

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXRegistryValue.cs
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
file:WiXRegistryValue.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn
{
    [WiXDisplayNameAttribte ("RegistryValue")]
    public class WiXRegistryValue : WiXEntry 
    {
        private string m_type;
        private string m_Root;
        private string m_Key;
        private string m_Name;
        private string m_Value;
        private string m_KeyPath;
        [WiXAttribute ()]
        public string KeyPath
        {
            get { return m_KeyPath; }
            set
            {
                if (m_KeyPath != value)
                {
                    m_KeyPath = value;
                }
            }
        }
        [WiXAttribute()]
        public string Value
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                }
            }
        }
        [WiXAttribute()]
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
        [WiXAttribute()]
public string Key{
get{ return m_Key;}
set{ 
if (m_Key !=value)
{
m_Key =value;
}
}
}
        [WiXAttribute()]
public string Root{
get{ return m_Root;}
set{ 
if (m_Root !=value)
{
m_Root =value;
}
}
}
        [WiXAttribute()]
public string Type{
get{ return m_type;}
set{ 
if (m_type !=value)
{
m_type =value;
}
}
}
        public WiXRegistryValue()
        {
            Root="HKCU";
            Key="Software\\Microsoft\\TestWixApp";
            Name="installed";
            Type="integer" ;
            Value="1" ;
            KeyPath="yes";
        }
    }
}

