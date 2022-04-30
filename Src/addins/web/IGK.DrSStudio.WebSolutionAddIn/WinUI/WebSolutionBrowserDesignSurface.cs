

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionBrowserDesignSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Web.WinUI
{
    [CoreSurface ("WebSolutionWebDesignSurface")]
    public class WebSolutionBrowserDesignSurface :
         WebSolutionSurfaceBase
    {
        private WebBrowser c_browser;
      
        public WebSolutionBrowserDesignSurface():base()
        {
            
            this.Load += _load;
            this.InitializeComponent();
            this.c_browser = new WebBrowser();
            this.Dock = DockStyle.Fill;
            this.c_browser.Dock = DockStyle.Fill;
            this.c_browser.ObjectForScripting = new WebSolutionScriptingObject(this);
            this.Controls.Add(this.c_browser);
        }

        public WebSolutionBrowserDesignSurface(WebSolutionSolution solution):this()
        {
            this.Solution = solution;
            this.Title = solution.Name;            
            this._load(this, EventArgs.Empty );
        }

        private void _load(object sender, EventArgs e)
        {
            if (this.Solution != null)
            {
                this.c_browser.Url = this.Solution.Uri;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WebSolutionDesignSurface
            // 
            this.Name = "WebSolutionDesignSurface";
            this.Size = new System.Drawing.Size(594, 295);
            this.Dock = DockStyle.Fill;
            this.ResumeLayout(false);

        }
    }
}
