

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EditBrushAction.cs
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
file:EditBrushAction.cs
*/
using IGK.ICore;using IGK.DrSStudio.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Actions
{
    /// <summary>
    /// edit fill brush action
    /// </summary>
    public class EditBrushAction : CoreActionBase
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
        public EditBrushAction(ICoreBrush brush, enuBrushSupport support)
        {
            if (brush == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "brush");
            this.m_Brush = brush;
            this.m_Support = support;
        }
        protected override bool PerformAction()
        {
            ICoreEditBrushAction brush_a = CoreSystem.Instance.Actions["Edit.EditBrush"] as
                ICoreEditBrushAction;
            if (brush_a != null)
            {
                brush_a.Support = this.Support;
                brush_a.Brush = this.Brush;
                brush_a.DoAction();
                brush_a.Brush = null;
            }
            return true;
        }
    }
}

