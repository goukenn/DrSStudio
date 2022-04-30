

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXPermissionUtilEx.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.WiXAddIn
{
    [WiXDisplayNameAttribte("PermissionEx",
        URI= WiXConstant.UTIL_EXTENSION_URI)]
    /// <summary>
    /// represent a wix permission util ex
    /// </summary>
    public class WiXPermissionUtilEx : WiXEntry 
    {
        private string m_User;
        private WiXYesOrNoValue m_GenericAll;
        [WiXAttribute(IsAttribute = false)]
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
       
        [WiXAttribute()]
        public WiXYesOrNoValue GenericAll
        {
            get { return m_GenericAll; }
            set
            {
                if (m_GenericAll != value)
                {
                    m_GenericAll = value;
                }
            }
        }
        [WiXAttribute()]
        public string User
        {
            get { return m_User; }
            set
            {
                if (m_User != value)
                {
                    m_User = value;
                }
            }
        }
    }
}
