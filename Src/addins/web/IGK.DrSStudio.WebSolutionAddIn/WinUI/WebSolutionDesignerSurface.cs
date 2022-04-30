

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionDesignerSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace IGK.DrSStudio.Web.WinUI
{
    using IGK.ICore.GraphicModels;
    using IGK.DrSStudio.Web.WorkingObjects;    
    using IGK.ICore.Drawing2D;
    /// <summary>
    /// used to design the document
    /// </summary>
    [CoreSurface("WebSolutionDesignerSurface", EnvironmentName="WebDesignerSurface")]
    public class WebSolutionDesignerSurface : 
        IGKD2DDrawingSurface,
        ICoreWorkingProjectSolutionSurface ,
        ICoreWorkingEditableSurface
    {
        public WebSolutionDesignerSurface()
        {
            this.Dock = DockStyle.Fill;
            this.InitializeComponent();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
        }
        private void InitializeComponent()
        {            
        }

        
        public new WebSolutionDocument CurrentDocument
        {
            get { return base.CurrentDocument as WebSolutionDocument ; }
            set
            {
                base.CurrentDocument = value;
            }
        }


        public override bool IsToolValid(Type t)
        {
            return base.IsToolValid(t);
        }
        
        public abstract class WebSolutionDesignerSubClassBase
        {
            private WebSolutionDesignerSurface m_owner;
            public WebSolutionDesignerSurface Owner { get { return this.m_owner; } }
            public WebSolutionDesignerSubClassBase(WebSolutionDesignerSurface owner)
            {
                this.m_owner = owner;
            }
        }

        protected override Core2DDrawingDocumentBase CreateNewDocument()
        {
            return null;
        }
        protected override IGKD2DDrawingSurfaceBase.IGKD2DDrawingScene CreateScene()
        {
            return new WebSolutionScene(this);
        }

        protected override IGKD2DDrawingSurfaceBase.IGKD2DDrawingDocumentCollections CreateDocumentCollections()
        {
            return new DocumentCollection(this);
        }

        public class DocumentCollection : IGKD2DDrawingSurfaceBase.IGKD2DDrawingDocumentCollections
        {            
            public new WebSolutionDesignerSurface Owner
            {
                get { return base.Owner as WebSolutionDesignerSurface; }
            }
            public DocumentCollection(WebSolutionDesignerSurface surface):base(surface)
            {
                surface.SolutionChanged += surface_SolutionChanged;
            }

            void surface_SolutionChanged(object sender, EventArgs e)
            {
                this.Container.Clear();
                var s = this.Owner;
                if (s != null)
                {
                    this.Container.Add(s.Solution.Document);
                    this.Owner.CurrentDocument = s.Solution.Document;
                }
                else {
                    this.Owner.CurrentDocument = null;
                }
            }
            protected override void InitDocumentCollection()
            {                
            }
            public override void Replace(Core2DDrawingDocumentBase[] documents)
            {
                
            }
            public override void Add(Core2DDrawingDocumentBase document)
            {
                base.Add(document);
            }
            public override void Remove(Core2DDrawingDocumentBase document)
            {
                base.Remove(document);
            }
 
        }
        public override bool CanAddDocument
        {
            get
            {
                return false;
            }
        }

        class WebSolutionScene : IGKD2DDrawingSurfaceBase.IGKD2DDrawingScene
        {
            private GridBoxElement m_Grid;
            private CoreWorkingDisposableObjectContainer m_container;
            public GridBoxElement Grid
            {
                get { return m_Grid; }
              
            }
            public new WebSolutionDocument CurrentDocument {
                get {
                    return base.CurrentDocument as WebSolutionDocument;
                }
            }
            public WebSolutionScene(WebSolutionDesignerSurface surface):base(surface)
            {
                this.m_container = new CoreWorkingDisposableObjectContainer();
                this.m_Grid = new GridBoxElement();
                this.m_Grid.SuspendLayout();
                this.m_Grid.Small = "10px";
                this.m_Grid.Large = "100px";
                this.m_Grid.FillBrush.SetSolidColor(Colorf.LightGray);
                this.m_Grid.StrokeBrush.SetSolidColor(Colorf.LightGray);
                this.SizeChanged += WebSolutionScene_SizeChanged;
                this.m_Grid.ResumeLayout();

                this.m_container.Add(m_Grid);
            }

            void WebSolutionScene_SizeChanged(object sender, EventArgs e)
            {
                this.m_Grid.SuspendLayout();
                this.m_Grid.Bounds = new Rectanglef(0, 0, this.Width, this.Height);
                this.m_Grid.FillBrush.SetSolidColor(Colorf.LightGray);
                this.m_Grid.StrokeBrush.SetSolidColor(Colorf.LightGray);
                this.m_Grid.ResumeLayout();

                
            }


            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this.m_container.Dispose();
                }
                base.Dispose(disposing);
            }
            protected override void RenderFrames(ICoreGraphics g)
            {
                base.RenderFrames(g);
                g.Clear(Colorf.WhiteSmoke);
                this.m_Grid.Draw(g);
               // g.SetClip(new Rectanglef(0, 0, 300, 300));
                //render document
                foreach (ICore2DDrawingFrameRenderer item in this.Frames)
                {
                    item.Render(g);
                }
                //render overlay document
                foreach (ICore2DDrawingFrameRenderer item in this.OverlayFrames)
                {
                    item.Render(g);
                }
            }

            protected override void OnCorePaint(CorePaintEventArgs e)
            {
                base.OnCorePaint(e);
            }
        }

        private WebSolutionSolution  m_Solution;

        public WebSolutionSolution  Solution
        {
            get { return m_Solution; }
            set
            {
                if (m_Solution != value)
                {
                    m_Solution = value;
                    OnSolutionChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler SolutionChanged;
        ///<summary>
        ///raise the SolutionChanged 
        ///</summary>
        protected virtual void OnSolutionChanged(EventArgs e)
        {
            if (SolutionChanged != null)
                SolutionChanged(this, e);
        }

        ICoreWorkingProjectSolution ICoreWorkingProjectSolutionSurface.Solution
        {
            get {
                return this.m_Solution;
            }
        }
    }
}
    

