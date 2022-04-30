

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ToolGdiBrushManager.cs
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
file:ToolGdiBrushManager.cs
*/
using IGK.ICore;using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    [CoreTools ("Drawing2D.GdiBlendingBrushManager")]
    class ToolGdiBrushManager : Core2DDrawingToolBase 
    {
        ICoreDialogForm dial;
        XUserControl m_ctrl;
        private static ToolGdiBrushManager sm_instance;
            private ToolGdiBrushManager(){
            }
            public static ToolGdiBrushManager Instance{
            get{
            return sm_instance;
            }
            }
            static ToolGdiBrushManager(){ 
            sm_instance = new ToolGdiBrushManager();
            }
                    protected override void GenerateHostedControl()
            {
 	            this.HostedControl = null;
                dial = Workbench.CreateNewDialog();
                m_ctrl = new XUserControl();
                m_ctrl.Dock = DockStyle.Fill;
                dial.Controls.Add(m_ctrl);
            }
            public void ShowTool(GdiBlendingBrushElement element){
                this.m_ctrl.Controls.Clear();
                Workbench.BuildWorkingProperty(this.m_ctrl, element);
                this.dial.FormClosing += dial_FormClosing;
                this.dial.Show();
            }
            void dial_FormClosing(object sender, FormClosingEventArgs e)
            {
                switch (e.CloseReason)
                {
                    case CloseReason.ApplicationExitCall:
                        break;
                    case CloseReason.FormOwnerClosing:
                        break;
                    case CloseReason.MdiFormClosing:
                        break;
                    case CloseReason.None:
                        break;
                    case CloseReason.TaskManagerClosing:
                        break;
                    case CloseReason.UserClosing:
                         e.Cancel = true;
                         this.dial.Hide();
                        break;
                    case CloseReason.WindowsShutDown:
                        break;
                    default:
                        break;
                }
            }
            public void HideTool(GdiBlendingBrushElement element){
                this.dial.Hide();
            }
            protected override void UnregisterBenchEvent(ICoreWorkbench workbench)
            {
                this.dial.Dispose();
                base.UnregisterBenchEvent(workbench);
            }
    }
}

