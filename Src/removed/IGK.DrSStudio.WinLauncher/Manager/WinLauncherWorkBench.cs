

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinLauncherWorkBench.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:WinLauncherWorkBench.cs
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
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using IGK.ICore;using IGK.DrSStudio.WinLauncher.Manager;
namespace IGK.DrSStudio.WinLauncher
{
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudioTools;
    using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent a winlaucher workbench
    /// </summary>
    internal class WinLauncherWorkBench : CoreWorkbenchBase  
    {
        internal  WinLauncherWorkBench(WinLaucherMainForm mainForm):base(mainForm )
        {
            mainForm.FormClosed += mainForm_FormClosed;
        }
        /// <summary>
        /// overrides show options and setting
        /// </summary>
        public override void ShowOptionsAddSetting()
        {
            base.ShowOptionsAddSetting();
        }
        //protected override ICoreDialogToolRenderer CreateToolRenderer()
        //{
        //    return new WinLayoutWorkbenchToolRenderer(this);
        //}
        void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        public IGK.DrSStudio.Tools.ActionRegister.CoreActionRegisterTool ActionRegister {
            get {
                return IGK.DrSStudio.Tools.ActionRegister.CoreActionRegisterTool.Instance;
            }
        }
        public override ICoreDialogForm CreateNewDialog()
        {
            DialogForm dlg =  new DialogForm();
            dlg.Owner = this.MainForm as Form ;
            return dlg;
        }
        protected override IGK.DrSStudio.WinUI.ICoreLayoutManager CreateLayoutManager()
        {
            return new WinLauncherLayoutManager(this);
        }
        public override System.Windows.Forms.DialogResult ConfigureWorkingObject(ICoreWorkingConfigurableObject item, bool AllowCancel)
        {
            if ((item != null) && (item.GetConfigType() != enuParamConfigType.NoConfig))
            {
                XConfigureItemProperty v_propControl = new XConfigureItemProperty();
                v_propControl.Name = "Property";
                v_propControl.AllowOkButton = true;
                v_propControl.CanCancel = AllowCancel;
                v_propControl.Element = item;
                v_propControl.Dock = DockStyle.Fill;
                WinLayoutWorkbenchToolRenderer v_render = new WinLayoutWorkbenchToolRenderer(
                    v_propControl.TopPanel,
                    item,
                    this);
                v_render.BuildWorkingProperty();
                using (ICoreDialogForm frm = CreateNewDialog(v_propControl))
                {
                    frm.Caption = CoreSystem.GetString(CoreConstant.LB_CONFIGURE_KEY, CoreSystem.GetString(item.Id));
                    if (frm is Form)
                    {
                        Form v_frm = (frm as Form);
                        Button btn = new Button()
                        {
                            Location = new Point(-999, -999)
                        };
                        btn.DialogResult = DialogResult.Cancel;
                        v_frm.Controls.Add(btn);
                        v_frm.CancelButton = btn;
                        (frm as Form).Size = v_propControl.PreferredSize;
                    }
                    return frm.ShowDialog();
                }
            }
            return DialogResult.None;
        }
        /// <summary>
        /// represnet the default renderer
        /// </summary>
        public class WinLayoutWorkbenchToolRenderer : CoreWorkbenchBase.CoreWorkbenchToolRenderer 
        {
            Control m_target;//base target
            ICoreWorkingConfigurableObject m_object;//base object
            public WinLayoutWorkbenchToolRenderer(Control target, ICoreWorkingConfigurableObject obj, WinLauncherWorkBench workbench)
                : base(workbench)
            {
                this.m_target = target;
                this.m_object = obj;
            }
            public override bool BuildWorkingProperty()
            {
                new WinLauncherGUIToolRenderer(this).BuildConfigurableItem(m_target, m_object);
                return true;                
            }
            public override void Reset(){
                this.m_target.Controls.Clear();
            }
            public override void Reload()
            {
                this.m_target.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                Form frm = this.m_target.FindForm();
                this.m_target.Visible = false;
                Application.DoEvents();
                frm.SuspendLayout();                
                this.m_target.Controls.Clear();
                this.m_target.SuspendLayout();
                this.m_target.Size = this.m_target.MinimumSize;// new Size(this.m_target.FindForm().Width, 100);
                this.BuildWorkingProperty();
                this.m_target.ResumeLayout();
                //this.m_target.Visible = true;
                this.m_target.Cursor = System.Windows.Forms.Cursors.Default;
                frm.ResumeLayout();
                this.m_target.Visible = true;
                Application.DoEvents();
            }
            public override ICoreWorkingConfigurableObject Object
            {
                get { return this.m_object; }
            }
            public override Control Target
            {
                get { return this.m_target; }
            }
        }
        protected override ICoreDialogToolRenderer CreateToolRenderer(Control target, ICoreWorkingConfigurableObject obj)
        {
            return  new WinLayoutWorkbenchToolRenderer(
                    target,
                    obj ,
                    this);
        }
    }
}

