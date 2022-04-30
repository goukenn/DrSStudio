

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImageHistoryAction.cs
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
using IGK.ICore.History;
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

namespace IGK.DrSStudio.Drawing2D.History
{
    
    using IGK.ICore.Resources;

    public class ImageHistoryAction :
        HistoryActionBase,
        IImageHistory 
    {

        #region IImageHistory Members
        private int m_currentIndex;
        private SingleImageHistoryManager m_owner;

        public int CurrentImageIndex
        {
            get { return m_currentIndex; }
        }

        #endregion

        public ImageHistoryAction(SingleImageHistoryManager owner, int index)
        {
            this.m_currentIndex = index;
            this.m_owner = owner;
        }

        public override void Undo()
        {
            this.m_owner.Undo(this);
        }

        public override void Redo()
        {
            this.m_owner.Redo(this);
        }

        public override string ImgKey
        {
            get { return "IMG_KEY"; }
        }

        public override string Info
        {
            get { return CoreResources.GetString ("PictureChanged"); }
        }
    }
}

