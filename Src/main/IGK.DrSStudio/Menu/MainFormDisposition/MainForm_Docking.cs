

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MainForm_Docking.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:MainForm_Docking.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WinUI.MainFormDisposition
{
    using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
    using IGK.ICore.Actions;
    using IGK.ICore;
    
    abstract class MainForm_Docking : CoreActionBase, IGlobalLayoutAction
    {
        public ICoreMainForm MainForm { get { return CoreSystem.GetMainForm(); } }
        public ICoreWorkbench Workbench { get { return CoreSystem.GetWorkbench (); } }
        public bool IsWindowKeyPressed {
            get {
                if (Workbench.GetLayoutManager() is DrSStudioLayoutManager l)
                    return ((MainForm_WindowKey)l.GlobalAction.GetAction(
                    enuKeys.LWin)).IsPressed;
                return false;
            }
        }
        public override enuActionType ActionType
        {
            get
            {
                return enuActionType.SystemAction;
            }
        }
        public override enuKeys ShortCut
        {
            get
            {
                return base.ShortCut;
            }
        }
        protected override bool PerformAction()
        {
 	         return false;
        }
        protected virtual Rectanglei GetRectangle(System.Windows.Forms.Screen e)
        {
            return Rectanglei.Empty;
        }
        public virtual bool DoAction(enuKeyState keystate)
        {

            if (this.IsWindowKeyPressed)
            {
                this.DoAction();
                //1. get the container screen
                System.Windows.Forms.Screen c = System.Windows.Forms.Screen.FromPoint(
                    new  System.Drawing.Point(this.MainForm.Location.X , 
                        this.MainForm.Location.Y));
                Rectanglei  rc = GetRectangle(c);
                //2. messure and flip to 
                this.MainForm.Location = rc.Location;                
                this.MainForm.Size = rc.Size;
                System.Windows.Forms.Form rf = (this.MainForm as System.Windows.Forms.Form);
                var gg = rf.MaximumSize;
                var g = (this.MainForm as System.Windows.Forms.Form).WindowState;
                return true;
            }
            return false;
        }
    }
}

