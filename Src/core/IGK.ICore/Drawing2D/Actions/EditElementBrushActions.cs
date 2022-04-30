

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EditElementBrush.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.Actions
{
    using IGK.ICore;using IGK.ICore.Actions;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;

    class EditElementBrushActions : CoreParameterActionBase, 
        IGK.ICore.WinUI.Configuration.ICoreParameterAction
    {
        private ICoreBrushOwner m_owner;
        private ICoreBrush m_brush;
        private enuBrushSupport m_support;

        public EditElementBrushActions(string name, ICoreBrushOwner owner, ICoreBrush brush , 
            enuBrushSupport support):base()
        {
            this.Name = name;
            this.m_support = support;
            this.CaptionKey = string.Format("lb." + name);
            this.m_owner = owner;
            this.m_brush = brush;
            this.Action = new EditBrush(this);
        }
        class EditBrush : CoreActionBase
        {
            private EditElementBrushActions m_owner;

            public EditBrush(EditElementBrushActions editElementBrush)
            {                
                this.m_owner = editElementBrush;
            }
            protected override bool PerformAction()
            {
                ICoreApplicationWorkbench b = CoreSystem.GetWorkbench < ICoreApplicationWorkbench>();
                ICoreEditSingleBrushControl c = CoreControlFactory.CreateControl("GCSXSingleColorSelector")
                    as ICoreEditSingleBrushControl;
                if ((b != null )&& (c!=null)) {

                    c.Brush = this.m_owner.m_brush;
                    c.BrushSupport = this.m_owner.m_support;
                    using (ICoreDialogForm dial = b.CreateNewDialog(c))
                    {
                        dial.AutoSize = false;
                        dial.Size = new Size2i (c.Size.Width , c.Size.Height);
                        dial.Title = "title.EditBrush".R();
                        dial.Owner = b.MainForm;
                        dial.AutoSize = false;
                        
                        dial.ShowDialog();
                    }
                    
                }
                return false;
            }
        }

    }
}
