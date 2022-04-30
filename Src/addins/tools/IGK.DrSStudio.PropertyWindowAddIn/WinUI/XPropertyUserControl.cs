

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XPropertyUserControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XPropertyUserControl.cs
*/

using IGK.ICore.Resources;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.Controls;


namespace IGK.DrSStudio.PropertyWindowAddIn.WinUI
{
    class XPropertyUserControl : IGKXToolHostedControl
    {
        PropertyGrid c_grid_property;
      
        /// <summary>
        /// get or set the object to configure
        /// </summary>
        public object ObjectToConfigure
        {
            get { return this.c_grid_property.SelectedObject;  }
            set
            {
                this.c_grid_property.SelectedObject = value;
            }
        }

        public XPropertyUserControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.c_grid_property = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // c_grid_property
            // 
            this.c_grid_property.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_grid_property.Location = new System.Drawing.Point(0, 0);
            this.c_grid_property.Name = "c_grid_property";
            this.c_grid_property.Size = new System.Drawing.Size(322, 420);
            this.c_grid_property.TabIndex = 0;
            // 
            // XPropertyUserControl
            // 
            this.Controls.Add(this.c_grid_property);
            this.Name = "XPropertyUserControl";
            this.Size = new System.Drawing.Size(322, 420);
            this.ResumeLayout(false);
        }       
     
        public string Id
        {
            get { return this.Name; }
        }

        public override  void LoadDisplayText()
        {
            foreach (var item in this.Controls)
            {
                ICoreCaptionItem c = item as ICoreCaptionItem;
                if (c != null)
                    c.LoadDisplayText();
            }
        }
    }
}

