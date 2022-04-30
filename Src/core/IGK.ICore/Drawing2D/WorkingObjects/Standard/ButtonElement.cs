

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ButtonElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent a button element host shape
    /// </summary>
    public class ButtonElement : RectangleElement , ICore2DDrawingVisitable 
    {
        private CoreButtonDocument m_ButtonDocument;
        private bool m_Enabled;
        private enuButtonState m_ButtonState;
        /// <summary>
        /// get or set the button state
        /// </summary>
        public enuButtonState ButtonState
        {
            get { return m_ButtonState; }
            set
            {
                if (m_ButtonState != value)
                {
                    m_ButtonState = value;
                }
            }
        }
        public CoreButtonDocument ButtonDocument
        {
            get { return m_ButtonDocument; }
            set
            {
                if (m_ButtonDocument != value)
                {
                    m_ButtonDocument = value;
                }
            }
        }
        /// <summary>
        /// get or set if this button Element is on the Enabled state
        /// </summary>
        public bool Enabled
        {
            get { return m_Enabled; }
            set
            {
                if (m_Enabled != value)
                {
                    m_Enabled = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public ButtonElement()
        {
            this.Enabled = true;
        }
        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            if (visitor == null) 
                return;
            if (this.m_ButtonDocument ==null)
            {
                return ;
            }
            object obj = visitor.Save();
            visitor.SetupGraphicsDevice(this);

           // visitor.TranslateTransform(Bounds.X, Bounds.Y, enuMatrixOrder.Prepend);

            if (this.Enabled )
            {
                this.ButtonDocument.Draw(this.ButtonState, visitor, true, Rectanglei.Round(this.Bounds), enuFlipMode.None);
            }
            else{
                this.ButtonDocument.Disabled.Draw(visitor,true,Rectanglei.Round ( this.Bounds), enuFlipMode.None);
            }
            visitor.Restore(obj);
        }
    }
}
