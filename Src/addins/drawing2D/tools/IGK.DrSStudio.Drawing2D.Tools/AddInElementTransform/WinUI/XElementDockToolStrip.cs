

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XElementDockToolStrip.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XElementDockToolStrip.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.ElementTransform.WinUI
{
    using IGK.ICore.Tools;
    using IGK.DrSStudio.Drawing2D;
    using Tools;
    using IGK.ICore.Resources;
    using IGK.ICore.WinCore.WinUI.Controls;
    class XElementDockToolStrip :  IGKXToolStripCoreToolHost 
    {
        internal new _ElementDockTool Tool {
            get {
                return base.Tool as _ElementDockTool;
            }
        }
        public XElementDockToolStrip(_ElementDockTool tool)
            : base(tool)
        {
            this.InitControl();
        }
        private void InitControl()
        {
            IGKXToolStripButton v_btn = null;
            EventHandler btnEvent = new EventHandler(btn_Click);
            foreach (enuCore2DDockElement i in Enum.GetValues(typeof(enuCore2DDockElement)))
            {
                v_btn = new IGKXToolStripButton();
                v_btn.Text =string.Format(CoreConstant.ENUMVALUE, i.ToString()).R();
                v_btn.Click += btnEvent;
                v_btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
                v_btn.Action = CoreSystem.GetAction(string.Format("Tools.Dock.{0}", i.ToString()));
                v_btn.ImageDocument = CoreResources.GetDocument(
                    string.Format("btn_2DDock_{0}_gkds", i.ToString()));
                this.Items.Add(v_btn);
            }
            this.AddRemoveButton(null);
        }
        void btn_Click(object sender, EventArgs e)
        {
            IGKXToolStripButton btn = sender as IGKXToolStripButton;
            if (btn.Action != null)
            {
                btn.Action.DoAction();
            }
        }
    }
}

