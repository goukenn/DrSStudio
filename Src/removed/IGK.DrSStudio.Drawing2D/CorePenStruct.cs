

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePenStruct.cs
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
file:CorePenStruct.cs
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
using System.Drawing;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    [StructLayout(LayoutKind.Sequential)]
    public struct CorePenStruct : ICorePenStruct
    {
        public static readonly CorePenStruct Empty;
        static CorePenStruct()
        {
            Empty = new CorePenStruct();
            Empty.m_Width = 1.0f;
            Empty.m_StartCap = CorePenLineCap.GetLineCap(enuLineCap.Flat);
            Empty.m_EndCap = CorePenLineCap.GetLineCap(enuLineCap.Flat);
            Empty.m_DashStyle = CorePenDashStyle.GetLineStyle(enuDashStyle.Solid);
        }
        private float m_Width;
        private enuPenAlignment m_Alignment;
        public enuPenAlignment Alignment
        {
            get { return m_Alignment; }
            set { m_Alignment = value; }
        }
        public float Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }
        #region ICorePenStruct Members
        private enuDashCap m_DashCap;
        public enuDashCap DashCap
        {
            get { return m_DashCap; }
            set { m_DashCap = value; }
        }
        private ICoreLineStyle m_DashStyle;
        public ICoreLineStyle DashStyle
        {
            get { return m_DashStyle; }
            set { m_DashStyle = value; }
        }
        private ICoreLineCap m_EndCap;
        public ICoreLineCap EndCap
        {
            get { return m_EndCap; }
            set { m_EndCap = value; }
        }
        private enuLineJoin m_LineJoin;
        public enuLineJoin LineJoin
        {
            get { return m_LineJoin; }
            set { m_LineJoin = value; }
        }
        private float m_MiterLimit;
        public float MiterLimit
        {
            get { return m_MiterLimit; }
            set { m_MiterLimit = value; }
        }
        private ICoreLineCap m_StartCap;
        public ICoreLineCap StartCap
        {
            get { return m_StartCap; }
            set { m_StartCap = value; }
        }
        #endregion
        #region ICorePenStruct Members
        enuLineCap ICorePenStruct.EndCap
        {
            get
            {
                return this.m_EndCap.LineCap;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        enuLineCap ICorePenStruct.StartCap
        {
            get
            {
                return this.m_StartCap.LineCap;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        enuDashStyle ICorePenStruct.DashStyle
        {
            get
            {
                return this.m_DashStyle.Style;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}

