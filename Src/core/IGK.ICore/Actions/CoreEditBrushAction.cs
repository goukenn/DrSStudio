

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreEditBrushAction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Actions
{
     /// <summary>
    /// represent a class that is call to edit a brush
    /// </summary>
    public class CoreEditBrushAction : CoreActionBase
    {
        private ICoreBrush m_Brush;
        private enuBrushSupport m_Support;

        public enuBrushSupport Support
        {
            get { return m_Support; }
        }
        public ICoreBrush Brush
        {
            get { return m_Brush; }
        }
        public override string Id
        {
            get { return "{BA6F1220-9D7C-4D4B-92AE-363923FB2AD4}"; }
        }
        public CoreEditBrushAction(ICoreBrush brush, enuBrushSupport support)
        {
            if (brush == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "brush");
            this.m_Brush = brush;
            this.m_Support = support;
        }

        protected override bool PerformAction()
        {
            
            IGK.ICore.WinUI.ICoreEditSingleBrushControl c = IGK.ICore.WinUI.CoreControlFactory.CreateControl(
                CoreControlConstant .SINGLECOLORSELECTOR )
                    as IGK.ICore.WinUI.ICoreEditSingleBrushControl;

            ICoreApplicationWorkbench bench = CoreSystem.GetWorkbench <ICoreApplicationWorkbench>();
            if ((c != null) && (bench != null))
            {

                c.BrushSupport = this.Support;
                c.Brush = this.Brush;

                using (ICoreDialogForm dial = bench.CreateNewDialog(c))
                {
                    dial.AutoSize = false;
                    dial.Size = new Size2i(c.Size.Width, c.Size.Height);
                    dial.Title = "title.EditBrush".R();
                    dial.Owner = bench.MainForm;
                    dial.AutoSize = false;
                    dial.ShowDialog();
                }
            }
            else {
                CoreLog.WriteLine("ERROR Controller or worbench not created ");
                CoreMessageBox.Show ("Error : n");
                CoreMessageBox.NotifyMessage("Exception", "Erreur : Controller cible Not Trouver");
            }

            return true;
        }
    }
}
