

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXToolStripCoreToolHost.cs
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
file:IGKXToolStripCoreToolHost.cs
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
namespace IGK.ICore.WinCore.WinUI.Controls
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI;
    public class
        IGKXToolStripCoreToolHost : 
        IGKXToolStrip ,
        ICoreToolHostedControl      
    {
         private CoreToolBase m_tool;
         private ICore2DDrawingDocument m_ToolDocument;

         public ICore2DDrawingDocument ToolDocument
         {
             get { return m_ToolDocument; }
             set
             {
                 if (m_ToolDocument != value)
                 {
                     m_ToolDocument = value;
                 }
             }
         }
      
         public CoreToolBase Tool
        {
            get { return m_tool; }
            set {
                this.m_tool = value;
            }
        }
     
      
        #region ICoreIdentifier Members
        string ICoreIdentifier.Id
        {
            get { return this.Name ; }
        }
        #endregion
          /// <summary>
        /// tool strip with attached tool
        /// </summary>
        /// <param name="tool"></param>
        public IGKXToolStripCoreToolHost(CoreToolBase tool)
            : base()
        {
            this.m_tool = tool;
        }

        public IGKXToolStripCoreToolHost()
        {
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


        ICoreTool ICoreToolHostedControl.Tool
        {
            get { return this.Tool; }
        }
    }
}

