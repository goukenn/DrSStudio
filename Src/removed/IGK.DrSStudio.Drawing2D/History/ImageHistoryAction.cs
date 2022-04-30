

/*
IGKDEV @ 2008 - 2014
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
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:ImageHistoryAction.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.History
{
    using IGK.ICore;using IGK.DrSStudio.History ;
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
            get { return CoreSystem.GetString ("PictureChanged"); }
        }
    }
}

