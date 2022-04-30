

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BrushResource.cs
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
file:BrushResource.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Resources
{
    using IGK.ICore;using IGK.ICore.Drawing2D;
    
    
    /// <summary>
    /// represent a brush resource
    /// </summary>
    public sealed class BrushResource : CoreResourceItemBase 
    {
        private ICoreBrush m_Brush;
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
        public BrushResource()
        {
        }
        public override enuCoreResourceType ResourceType
        {
            get { return enuCoreResourceType.Brush; }
        }
        public override object GetData()
        {
            throw new NotImplementedException();
        }
        public override string GetDefinition()
        {
            if (this.m_Brush != null)
                return this.m_Brush.GetDefinition();
            return null;
        }
      
    }
}

