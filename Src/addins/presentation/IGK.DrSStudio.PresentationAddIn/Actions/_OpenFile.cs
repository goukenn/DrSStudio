

using IGK.ICore;
using IGK.ICore.Codec;
/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _OpenFile.cs
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
file:_OpenFile.cs
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
namespace IGK.DrSStudio.Presentation.Actions
{
    class _OpenFile : PresentationActionBase, IPresentationActions
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "GKDS Files | *"+CoreConstant.DEFAULTFILEEXTENTION ;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.PresentationForm.PresentationDocument = PresentationDocument.Open(ofd.FileName);
                    return true;
                }
            }
            return false;
        }
    }
}

