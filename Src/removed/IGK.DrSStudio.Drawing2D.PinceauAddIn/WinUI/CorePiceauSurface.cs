

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePiceauSurface.cs
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
file:CorePiceauSurface.cs
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
﻿
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.WinUI;
    [CoreSurface ("PinceauSurface")]
    /// <summary>
    /// piceau document 
    /// </summary>
    sealed class CorePinceauSurface : XDrawing2DSurface 
    {
        public CorePinceauSurface()
            : base()
        {
            this.FileName = "pinceau.gkdps";
            this.NeedToSave = false;
        }
        public override IGK.DrSStudio.Drawing2D.ICore2DDrawingDocument CreateNewDocument()
        {
            return new PinceauDocument();
        }
        public override void Save()
        {
            SaveAs(this.FileName);
        }
        public override void SaveAs(string filename)
        {
            (this.CurrentDocument as PinceauDocument).SaveStyle(filename);
            NeedToSave = false;               
        }
        public override ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("Save Pencil",
                "Pencil | *.gkps",
                this.FileName);
        }
    }
}

