

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _BitmapHistoryActions.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing2D.HistoryActions
{
    using IGK.ICore.Drawing2D.Tools;
    using IGK.DrSStudio.Drawing2D.History;
    using IGK.ICore.Resources;
    /// <summary>
    /// represent a bitmap history action
    /// </summary>
    public class _BitmapHistoryActions :
        HistoryActionBase ,
        IImageHistory 
    {
        SingleImageHistoryManager m_simage;
        int m_currentImageIndex;
        public int CurrentImageIndex {
            get {
                return this.m_currentImageIndex;
            }
        }
        public override string Info
        {
            get
            {
                return CoreResources.GetString("History.BitmapChanged");
            }
        }
        public override string ImgKey
        {
            get
            {
                return "image";
            }
        }
        internal _BitmapHistoryActions( 
            SingleImageHistoryManager simage,
            int index)
        {
            m_simage = simage;
            this.m_currentImageIndex = index;
        }
        public override void Undo()
        {
            m_simage.Undo(this);
        }
        public override void Redo()
        {
            m_simage.Redo(this);
        }
    }
}
