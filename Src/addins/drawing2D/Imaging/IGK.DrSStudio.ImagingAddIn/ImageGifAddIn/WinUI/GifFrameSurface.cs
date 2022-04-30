

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifFrameSurface.cs
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
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GifFrameSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.GifAddIn.WinUI
{
    using IGK.DrSStudio.GifAddIn;
    [CoreSurface("GifFrameSurface", EnvironmentName = CoreConstant.DRAWING2D_ENVIRONMENT,
        DisplayName="Gif")]
    /// <summary>
    /// represnet a gif frame surfacxe
    /// </summary>
    class GifFrameSurface : IGKD2DDrawingSurface, ICoreWorkingSurface 
    {
        private GifFileDocument   m_GifDocument;
        private int m_SelectedIndex;
        private GifFrameLayer  m_SelectedFrameLayer;
        /// <summary>
        /// get the frame layer
        /// </summary>
        public GifFrameLayer  SelectedFrameLayer
        {
            get { return m_SelectedFrameLayer; }
            set
            {
                if (m_SelectedFrameLayer != value)
                {
                    m_SelectedFrameLayer = value;
                }
            }
        }
        public int SelectedIndex
        {
            get { return m_SelectedIndex; }
            set
            {
                if (m_SelectedIndex != value)
                {
                    m_SelectedIndex = value;
                }
            }
        }
        /// <summary>
        /// represent a gif document
        /// </summary>
        public GifFileDocument   GifDocument
        {
            get { return m_GifDocument; }
            set
            {
                if (m_GifDocument != value)
                {
                    m_GifDocument = value;
                }
            }
        }
     
        public GifFrameSurface()
        {
            this.InitializeComponent();
        }
      
      
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GifFrameSurface
            // 
            this.Name = "GifFrameSurface";
            this.Size = new System.Drawing.Size(571, 512);
            this.ResumeLayout(false);
        }
    }
}

