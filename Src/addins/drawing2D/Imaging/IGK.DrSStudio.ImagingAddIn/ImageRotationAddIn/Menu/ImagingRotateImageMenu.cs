

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImagingRotateImageMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Imaging.Menu
{
    using System.Drawing;
    using IGK.DrSStudio.Drawing2D.Menu;

    [DrSStudioMenu("Image.Rotation", 0x30)]
    class ImagingRotateImageMenu : ImageMenuBase
    {
        protected override bool PerformAction()
        {
            return false;
        }

        protected override void InitMenu()
        {
            base.InitMenu();
            
            CoreMenuAttribute v_attr = null;
            int v_index  = 0;
            ImageRotateSubmMenu v_subMenu = null;
            string[] vi = Enum.GetNames(typeof(RotateFlipType));
            string k = string.Empty;
            List<string> v_reg = new List<string>();
            foreach (RotateFlipType item in Enum.GetValues (typeof(RotateFlipType)))
	        {
                k = this.Id + "." + item;
                if (v_reg.Contains(k))
                    continue;

                v_attr  = new CoreMenuAttribute (k, v_index);
                v_subMenu = new ImageRotateSubmMenu(this, item);
                v_subMenu.SetAttribute(v_attr);
                if (this.Register(v_attr, v_subMenu))
                {
                    //add child to list
                    this.Childs.Add(v_subMenu);
                    v_reg.Add(k);
                    
                }
	        }
        }

        class ImageRotateSubmMenu : ImageMenuBase
        {
            private ImagingRotateImageMenu imagingRotateImageMenu;
            private RotateFlipType item;

            public ImageRotateSubmMenu(ImagingRotateImageMenu imagingRotateImageMenu, RotateFlipType item)
            {
                
                this.imagingRotateImageMenu = imagingRotateImageMenu;
                this.item = item;
            }
            protected override bool PerformAction()
            {
                this.ImageElement.RotateBitmap((int)item);
                this.Invalidate();
                return true;
            }
        }
    }
}
