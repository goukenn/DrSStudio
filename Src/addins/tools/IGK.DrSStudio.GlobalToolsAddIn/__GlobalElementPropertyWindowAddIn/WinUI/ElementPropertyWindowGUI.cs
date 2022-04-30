

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ElementPropertyWindowGUI.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    class ElementPropertyWindowGUI : IGKXToolConfigControlBase
    {
        private ICoreWorkingConfigurableObject m_ElementToConfigure;

        public ICoreWorkingConfigurableObject ElementToConfigure
        {
            get { return m_ElementToConfigure; }
            set
            {
                if (m_ElementToConfigure != value)
                {
                    m_ElementToConfigure = value;
                    this.SuspendLayout();
                    this.Controls.Clear();
                    if (value != null)
                    {
                        Workbench.BuildWorkingProperty(this,
                            value);
                    }
                    this.ResumeLayout();
                }
            }
        }

        

        public ElementPropertyWindowGUI()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {            
            this.SuspendLayout();
            // 
            // ElementPropertyWindowGUI
            // 
            this.Name = "ElementPropertyWindowGUI";
            this.Size = new System.Drawing.Size(295, 366);
            this.ResumeLayout(false);

        }
    }
}
