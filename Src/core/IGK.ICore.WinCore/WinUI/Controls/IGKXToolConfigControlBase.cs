

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXToolConfigControlBase.cs
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
file:UIXToolConfigControlBase.cs
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
using System.Text;
using System.ComponentModel;
namespace IGK.ICore.WinCore.WinUI.Controls
{
    using IGK.ICore.WinCore;
    using IGK.ICore.Resources;
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Actions;
    using IGK.ICore;
    /// <summary>
    /// Represent the base control that edit tool
    /// </summary>
    public class IGKXToolConfigControlBase : 
        UIXConfigControlBase ,
        ICoreToolHostedControl 
    {
        ICoreTool m_Tool;
        ICore2DDrawingDocument m_document;
        #region ICoreToolHostedControl Members
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        /// <summary>
        /// Get the current hosted tool
        /// </summary>
        public ICoreTool Tool
        {
            get { return this.m_Tool; }
            set { this.m_Tool = value; }
        }
        public virtual ICore2DDrawingDocument ToolDocument
        {
            get { return this.m_document; }
        }
        #endregion
        protected IGKXToolConfigControlBase()
        {
        }

        /// <summary>
        /// call system action
        /// </summary>
        /// <param name="actionname"></param>
        protected void CallAction(string actionname)        
        {
            
            CallAction(actionname, false);
        }
        /// <summary>
        /// call system action
        /// </summary>
        /// <param name="actionname"></param>
        protected void CallAction(string actionname, bool viewMessageBoxOnNotFound)
        {
            if ((m_Tool != null) && (m_Tool.Workbench != null))
            {
                if (viewMessageBoxOnNotFound)
                {
                    ICoreAction ack = CoreSystem.GetAction(actionname);
                    if (ack == null)
                    {
                        CoreMessageBox.Show("MGS.NOACTIONFOUND.WITHNAME_1".R(actionname));
                    }
                    else
                        ack.DoAction();
                }
                else 
                m_Tool.Workbench.CallAction(actionname);
            }            
        }
        /// <summary>
        /// get the current surface
        /// </summary>
        protected ICoreWorkingSurface CurrentSurface {
            get {
                if ((m_Tool != null) && (m_Tool.Workbench != null))
                    return m_Tool.Workbench.CurrentSurface;
                return null;
            }
        }
        public IGKXToolConfigControlBase(ICoreTool tool):base()
        {
            if (tool == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull , "tool");
            this.m_Tool = tool;
            this.m_CaptionKey = this.m_Tool.Id;
            this.m_document = CoreResources.GetDocument(this.m_Tool.ToolImageKey);
            this.AutoScroll = true;
        }
        Size2i ICoreToolHostedControl.Size
        {
            get
            {
                return new Size2i(base.Size.Width, base.Size.Height);
            }
            set
            {
                base.Size = new System.Drawing.Size(value.Width, value.Height);
            }
        }

        Vector2i ICoreToolHostedControl.Location
        {
            get
            {
                return new Vector2i(base.Location.X,
                    base.Location.Y);
            }
            set
            {
                base.Location = new System.Drawing.Point(value.X, value.Y);
            }
        }
    }
}

