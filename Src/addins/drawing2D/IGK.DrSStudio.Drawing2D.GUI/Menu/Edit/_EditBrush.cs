

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _EditBrush.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Menu;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.Drawing2D.Menu.Edit
{
    [DrSStudioMenu("Edit.EditBrush", 0x5000)]
    class _EditBrushMenu : Core2DDrawingMenuBase, ICoreEditBrushAction
    {
        private ICoreBrush m_Brush;
        private enuBrushSupport m_Support;
        /// <summary>
        /// get or set the brush support
        /// </summary>
        public enuBrushSupport Support
        {
            get { return m_Support; }
            set
            {
                if (m_Support != value)
                {
                    m_Support = value;
                }
            }
        }
        public ICoreBrush Brush
        {
            get { return m_Brush; }
            set
            {
                if (m_Brush != value)
                {
                    m_Brush = value;
                }
            }
        }
        protected override bool PerformAction()
        {
            ICoreBrush br = null;
            var v_support = this.Support;
            if (this.Brush == null)
            {
                if (this.CurrentSurface.ElementToConfigure is ICoreBrushOwner)
                {
                    ICoreBrushOwner b = (this.CurrentSurface.ElementToConfigure as ICoreBrushOwner);
                    br = b.GetBrush(this.CurrentSurface.BrushMode);
                    v_support = b.BrushSupport;
                }
                else
                {
                    if (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1)
                    {
                        br = (this.CurrentSurface.CurrentLayer.SelectedElements[0] as ICoreBrushOwner).GetBrush(
                        this.CurrentSurface.BrushMode);
                    }
                }
            }
            else
            {
                br = this.Brush;
                v_support = this.Support;
            }
            if (br != null)
            {
                ICoreControl doc = CoreControlFactory.CreateControl("IGKXEditBrush", new object[] { br, v_support }) as ICoreControl;
                if (doc != null)
                {
                    using (ICoreDialogForm dial = Workbench.CreateNewDialog(doc))
                    {
                        dial.Title = CoreResources.GetString("title.EditBrush", br.Id);
                        dial.ShowDialog();
                    }
                    return true;
                }
#if DEBUG
                else {
                    CoreMessageBox.Show("No IGKXEditBrush control found");
                }
#endif 
            }
            return false;
        }
        public _EditBrushMenu()
        {
            this.IsRootMenu = true;
        }


     
    }
}
