

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ShortCutManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ShortCutManager.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms ;
namespace IGK.DrSStudio.XMLEditorAddIn.Tools
{
    using IGK.ICore.Tools;
    using Actions;
    [CoreTools("Tool.XMLEditorShortCutManager")]
    class ShortCutManager : CoreToolBase 
    {
        private Dictionary<Keys, IXMLEditorAction> m_actions;
        private static ShortCutManager sm_instance;
        private ShortCutManager()
        {
            m_actions = new Dictionary<Keys, IXMLEditorAction>();
        }
        public static ShortCutManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static ShortCutManager()
        {
            sm_instance = new ShortCutManager();
        }
        public bool CallAction(Keys key)
        {
            if (m_actions.ContainsKey(key))
            {
                this.m_actions[key].DoAction();
                return true;
            }
            return false;
        }
    }
}

