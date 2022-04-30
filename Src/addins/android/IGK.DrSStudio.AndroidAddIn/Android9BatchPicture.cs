

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Android9BatchPicture.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android
{
    public class Android9BatchPicture : ImageElement
    {

        private Vector2f m_Top;
        private Vector2f m_Bottom;
        private Vector2f m_Left;
        private Vector2f m_Right;
        private Rectanglef m_Bounds;

        public Android9BatchPicture():base()
        {
            //
        }
        /// <summary>
        /// bounds where to draw
        /// </summary>
        public Rectanglef Bounds
        {
            get { return m_Bounds; }
            set
            {
                if (m_Bounds != value)
                {
                    m_Bounds = value;
                    OnPropertyChanged (Core2DDrawingChangement.Definition);
                }
            }
        }
        /// <summary>
        /// get or set right dimension
        /// </summary>
        public Vector2f Right
        {
            get { return m_Right; }
            set
            {
                if (m_Right != value)
                {
                    m_Right = value;
                }
            }
        }
        /// <summary>
        /// get or set the left dimension
        /// </summary>
        public Vector2f Left
        {
            get { return m_Left; }
            set
            {
                if (m_Left != value)
                {
                    m_Left = value;
                }
            }
        }
        /// <summary>
        /// get or set the bottom dimension
        /// </summary>
        public Vector2f Bottom
        {
            get { return m_Bottom; }
            set
            {
                if (m_Bottom != value)
                {
                    m_Bottom = value;
                }
            }
        }
        /// <summary>
        /// get or set the top dimension
        /// </summary>
        public Vector2f Top
        {
            get { return m_Top; }
            set
            {
                if (m_Top != value)
                {
                    m_Top = value;
                }
            }
        }

        public override void Visit(ICore2DDrawingVisitor visitor)
        {
            if (visitor == null)
            {
                return;
            }
            object obj = visitor.Save();
            visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend);
            visitor.SetupGraphicsDevice(this);
            visitor.InterpolationMode = this.InterpolationMode;
            //visitor.Visit(this.Bitmap, new Rectanglef (0,0, Top.X, Right.X));
            //visitor.Visit(this.Bitmap, new Rectanglef(0, 0, Top.X, Right.X));
            //visitor.Visit(this.Bitmap, new Rectanglef(0, 0, Top.X, Right.X));
            //visitor.Visit(this.Bitmap, new Rectanglef(0, 0, Top.X, Right.X));


            //visitor.Visit(this.Bitmap, new Rectanglef(Top.X, Right.X, this.Bounds.Width - Top.X, Right.X));
            visitor.Restore(obj);
        }
    }
}
