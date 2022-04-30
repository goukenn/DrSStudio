

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioWorkbenchToolRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:DrSStudioWorkbenchToolRenderer.cs
*/
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// Represent the default renderer
    /// </summary>
    public class DrSStudioWorkbenchToolRenderer : 
        CoreWorkbenchBase.CoreWorkbenchToolRenderer
    {
        private Size2i m_preferredSize;

        public Size2i PreferredSize {
            get {
                return m_preferredSize;
            }
        }
        public DrSStudioWorkbenchToolRenderer(
            ICoreControl target,
            ICoreWorkingConfigurableObject obj, CoreWorkbenchBase  workbench)
            : base(workbench,
            target, 
            obj )
        {
        }
        public override bool BuildWorkingProperty()
        {
            DrSStudioWorkbenchGUIRenderer p = new DrSStudioWorkbenchGUIRenderer(this);
            return p.BuildConfigurableItem(Target, this.Object, ref m_preferredSize  );
        }
        public override void Reset()
        {
            this.Target.Controls.Clear();
        }
        public override void Reload()
        {
            this.Target.SetCursor(System.Windows.Forms.Cursors.WaitCursor);
            IXForm frm = this.Target.FindForm();
            this.Target.Visible = false;
            Application.DoEvents();
            frm.SuspendLayout();
            this.Target.Controls.Clear();
            this.Target.SuspendLayout(); 
            this.Target.Size = this.Target.GetMinimumSize();// new Size(this.Target.FindForm().Width, 100);
            this.BuildWorkingProperty();
            this.Target.ResumeLayout();
            //this.Target.Visible = true;
            (this.Target as Control).Cursor = System.Windows.Forms.Cursors.Default;
            frm.ResumeLayout();
            this.Target.Visible = true;
            Application.DoEvents();
        }
    }
}

