

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXCustomAction.cs
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
file:WiXCustomAction.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn
{
    [WiXDisplayNameAttribte("CustomAction")]
    public class WiXCustomAction  : WiXFeature
    {
        private string m_BinaryKey;
        private string m_DllEntry;
        private string m_Impersonate;
        [WiXAttribute ()]
public string Impersonate{
get{ return m_Impersonate;}
set{ 
if (m_Impersonate !=value)
{
m_Impersonate =value;
}
}
}
        [WiXAttribute()]
public string DllEntry{
get{ return m_DllEntry;}
set{ 
if (m_DllEntry !=value)
{
m_DllEntry =value;
}
}
}
        [WiXAttribute()]
public string BinaryKey{
get{ return m_BinaryKey;}
set{ 
if (m_BinaryKey !=value)
{
m_BinaryKey =value;
}
}
}
        public WiXCustomAction():base(){ 
                BinaryKey="WixCA" ;
				DllEntry="WixShellExec";
                Impersonate = "yes";
        }
    }
}

