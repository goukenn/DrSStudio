

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidLayoutDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android
{
    
using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.GraphicModels;
    using IGK.ICore;
    using IGK.DrSStudio.Android.WinUI;

    [CoreWorkingDocument("AndroiLayoutView")]
    /// <summary>
    /// represent a android layout document
    /// </summary>
    public class AndroidLayoutDocument  :
        CoreWorkingObjectBase,  
        ICoreWorkingDocument,
        ICore2DDrawingVisitable 
        
    {

        private Matrix m_Matrix;

        [CoreXMLAttribute()]
        public Matrix Matrix
        {
            get { return m_Matrix; }          
        }
        private int m_DeviceWidth;
        private int m_DeviceHeight;
        private string m_LayoutType;
        private Core2DDrawingLayeredElement m_view;
        /// <summary>
        /// get or set the layout type name
        /// </summary>
        public string LayoutType
        {
            get { return m_LayoutType; }
            set
            {
                if (m_LayoutType != value)
                {
                    m_LayoutType = value;
                }
            }
        }
        /// <summary>
        /// get or set device width
        /// </summary>
        public int DeviceHeight
        {
            get { return m_DeviceHeight; }
            set
            {
                if (m_DeviceHeight != value)
                {
                    m_DeviceHeight = value;
                }
            }
        }
        /// <summary>
        /// get or set the device height
        /// </summary>
        public int DeviceWidth
        {
            get { return m_DeviceWidth; }
            set
            {
                if (m_DeviceWidth != value)
                {
                    m_DeviceWidth = value;
                }
            }
        }
        public AndroidLayoutDocument()
        {
            this.m_view = new AndroidLinearLayout();
            this.m_Matrix = new Matrix();
            
        }
        public override void Dispose()
        {
            if (this.m_Matrix != null)
            {
                this.m_Matrix.Dispose();
                this.m_Matrix = null;
            }
            base.Dispose();
        }      
        /// <summary>
        /// the the adroid layout to file name
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            AndroidLayoutVisitor c = new AndroidLayoutVisitor();
            c.Visit(this.View);
            c.SaveTo(filename);
            //c.Dispose();
        }
        /// <summary>
        /// get the default surface type
        /// </summary>
        public virtual Type DefaultSurfaceType
        {
            get { return  typeof(AndroidLayoutDesignSurface); }
        }

        internal void LoadDocument(string nodeContent)
        {
            throw new NotImplementedException();
        }

        internal void LoadFile(string filename)
        {
            throw new NotImplementedException();
        }


        public Core2DDrawingLayeredElement View { get {
            return this.m_view;
        } }

        public void Rotate(float m_angle)
        {
            this.m_Matrix.Rotate(m_angle, new Vector2f(this.m_DeviceWidth / 2.0f, this.m_DeviceHeight / 2.0f));
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            if (this.View !=null)
            visitor.Visit(this.View);
        }
    }
}
