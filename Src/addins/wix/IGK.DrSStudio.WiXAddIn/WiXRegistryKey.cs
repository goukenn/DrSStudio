

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXRegistryKey.cs
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
file:WiXRegistryKey.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn
{
   [WiXDisplayNameAttribte("RegistryKey")]
    public class WiXRegistryKey : WiXEntry
    {
        private string m_Root;
        private string m_Key;
        private enuWiXRegistryAction m_Action;
       [WiXAttribute ()]
        public enuWiXRegistryAction Action
        {
            get { return m_Action; }
            set {
                m_Action = value;
            }
        }
       [WiXAttribute()]
        public string Key
        {
            get { return m_Key; }
            set
            {
                if (m_Key != value)
                {
                    m_Key = value;
                }
            }
        }
       [WiXAttribute()]
        public string Root
        {
            get { return m_Root; }
            set
            {
                if (m_Root != value)
                {
                    m_Root = value;
                }
            }
        }
        public WiXRegistryKey()
        {
            this.m_Action = enuWiXRegistryAction.createAndRemoveOnUninstall;
        }
        protected  override bool Support(WiXEntry e)
        {
            if (e == null) return false;
            switch (WiXDisplayNameAttribte.GetName(e.GetType ()))
            { 
                case "Permission":
                case "PermissionEx":
                case "RegistryKey":
                case "RegistryValue":
                    return true;
            }
            return false;
        }
    }
}

