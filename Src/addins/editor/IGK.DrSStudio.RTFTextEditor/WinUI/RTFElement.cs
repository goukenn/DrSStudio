

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RTFElement.cs
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
file:RTFElement.cs
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
namespace IGK.DrSStudio.RTFTextEditor.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D ;

    sealed class RTFElement : 
        ICoreWorkingObject,
        ICoreTextElement,
        ICoreBrushOwner ,
        ICoreBrushContainer ,
        IDisposable
    {
        private IGK.DrSStudio.Drawing2D.CoreFont m_Font;
        private ICoreBrush m_FillBrush;
        private ICoreBrush m_StrokeBrush;
        private string m_text;
        public ICoreBrush[] GetBrushes()
        {
            return new ICoreBrush[] {
                m_FillBrush , m_StrokeBrush 
            };
        }
        public RTFElement()
        {
            m_Font = new CoreFont(this, CoreConstant.DEFAULT_FONT_NAME);
            m_Font.FontSize = 12.0f;
            m_Font.FontStyle = enuFontStyle.Regular;
            m_FillBrush = new CoreBrush(null);
            m_StrokeBrush = new CoreBrush(null);
        }
        /// <summary>
        /// get fill brush
        /// </summary>
        public ICoreBrush FillBrush {
            get { return this.m_FillBrush; }
        }
        /// <summary>
        /// get stroke brush
        /// </summary>
        public ICoreBrush StrokeBrush {
            get { return this.m_StrokeBrush; }
        }
        
        public ICoreFont Font
        {
            get { return this.m_Font; }
        }
        
        #region ICoreIdentifier Members
        public string Id
        {
            get {
                return string.Format ("RTFElement_{0}",GetHashCode());
            }
        }
        #endregion
        #region ICore2DDrawingBrushSupportElement Members
        public IGK.DrSStudio.Drawing2D.ICoreBrush GetBrush(enuBrushMode mode)
        {
            switch (mode)
            { 
                case enuBrushMode.Fill :
                    return m_FillBrush;
                case enuBrushMode.Stroke :
                    return m_StrokeBrush;
            }
            return null;
        }
        public IGK.DrSStudio.Drawing2D.enuBrushSupport BrushSupport
        {
            get {
                return enuBrushSupport.Fill | 
                    enuBrushSupport.Stroke | 
                    enuBrushSupport.Solid;
            }
        }
        #endregion
        #region IDisposable Members
        public void Dispose()
        {
            this.m_FillBrush.Dispose();
            this.m_StrokeBrush.Dispose();
            this.m_Font.Dispose();
            this.m_FillBrush = null;
            this.m_StrokeBrush = null;
            this.m_Font = null;
        }
        #endregion

        public string Text
        {
            get
            {
                return this.m_text;
            }
            set
            {
                this.m_text = value;
            }
        }


        public Matrix GetMatrix()
        {
            return Matrix.Identity;
        }

        public CoreGraphicsPath GetPath()
        {
            return null;
        }

        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;


        void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, e);
        }
    }
}

