

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _InsertDocumentBlock.cs
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
file:_InsertDocumentBlock.cs
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
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.Menu.Insert
{
    using IGK.ICore.WinCore;
using IGK.ICore.Menu;
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.DrSStudio.Menu;
    using IGK.ICore.Drawing2D.Menu;
    [DrSStudioMenu(CoreConstant.MENU_INSERT+".DocumentBlock", 0)]
    class _InsertDocumentBlock :
               Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "GKDS Picture Documents |*.gkds;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                       DocumentBlockElement c =  DocumentBlockElement.FromFile(ofd.FileName);
                        if (c!=null)
                        {
                            this.CurrentSurface.CurrentLayer.Elements.Add (c);
                            this.CurrentSurface.RefreshScene();
                        }
                    }
                    catch
                    {
                        CoreMessageBox.Show(new CoreException("Can't Open File"));
                    }
                }
            }
            return false;
        }
    }
}

