

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioDialogToolRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    class DrSStudioDialogToolRenderer : ICoreDialogToolRenderer 
    {
        private ICoreControl m_target;
        private ICoreWorkingConfigurableObject m_object;
        private DrSStudioWorkbench m_workbench;
        Size2i m_preferredSize;

        public Size2i PreferredSize { get { return m_preferredSize; } }
        public DrSStudioDialogToolRenderer(ICoreControl target, 
            ICoreWorkingConfigurableObject obj, DrSStudioWorkbench workbench)
        {
            this.m_workbench = workbench;
            this.m_target = target;
            this.m_object = obj;
        }

        public  bool BuildWorkingProperty()
        {
            return (new DrSStudioWorkbenchGUIRenderer(this)).BuildConfigurableItem(this.m_target, this.m_object, ref m_preferredSize );            
        }

        public  void Reset()
        {
            this.m_target.Controls.Clear();
        }
        public  void Reload()
        {
            Control ctrl = (this.m_target as Control);
            ctrl.Cursor = Cursors.WaitCursor;
            Form frm = this.m_target.FindForm() as Form ;
            this.m_target.Visible = false;
            Application.DoEvents();

            frm.SuspendLayout();
            this.m_target.Controls.Clear();
            this.m_target.SuspendLayout();


            ctrl.Size = ctrl.MinimumSize;// new Size(this.m_target.FindForm().Width, 100);

            this.BuildWorkingProperty();
            this.m_target.ResumeLayout();
            //this.m_target.Visible = true;
            (this.m_target as Control).Cursor = Cursors.Default;
            frm.ResumeLayout();
            this.m_target.Visible = true;
            Application.DoEvents();
        }

        public  ICoreWorkingConfigurableObject Object
        {
            get { return this.m_object; }
        }



        public ICoreControl Target
        {
            get { return this.m_target; }
        }
    }

}
