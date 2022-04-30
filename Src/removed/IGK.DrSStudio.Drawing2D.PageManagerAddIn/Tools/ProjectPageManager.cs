

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ProjectPageManager.cs
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
file:ProjectPageManager.cs
*/
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//namespace IGK.DrSStudio.Drawing2D.PageManagerAddIn.Tools
//{
//    using IGK.ICore;using IGK.DrSStudio.WinUI;
//    using IGK.DrSStudio.Drawing2D.WinUI ;
//    [IGK.DrSStudio.Codec.CoreProjectInfoItem(PageManagerConstant .PAGENUMBER)]
//    public class ProjectPageManager : IGK.DrSStudio.Codec.CoreProjectItemBase 
//    {
//        XDrawing2DSurface m_surface;
//        Drawing2DProjectInfo m_projectInfo;
//        ICore2DRenderingContext m_renderingContext;
//        //for deserialisation
//        public ProjectPageManager()
//        {
//            this.OwnerChanged += new EventHandler(ProjectPageManager_OwnerChanged);
//        }
//        void ProjectPageManager_OwnerChanged(object sender, EventArgs e)
//        {
//            if ((this.Owner != null) && (this.Owner.Surface is XDrawing2DSurface ))
//            {
//                if (this.m_projectInfo != null)
//                    Unregister();
//                this.m_surface = this.Owner.Surface as XDrawing2DSurface;
//                this.m_projectInfo = this.m_surface .ProjectInfo;    
//                Register();
//            }
//        }
//        internal ProjectPageManager(XDrawing2DSurface surface):base()
//        {
//            if (surface == null)
//                throw new CoreException(enuExceptionType.ArgumentIsNull, "surface");
//            this.m_surface = surface;
//            this.m_projectInfo = surface.ProjectInfo;
//            this.Add ("Pattern", "Page - %N%");
//            Register();
//        }
//        /// <summary>
//        /// get or set the pattern string
//        /// </summary>
//        public string Pattern
//        {
//            get { return (string)GetSingleValue("Pattern"); }
//            set
//            {
//                SetSingleValue("Pattern", value);
//            }
//        }
//        protected override void WriteAttributes(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
//        {
//            base.WriteAttributes(xwriter);
//        }
//        protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
//        {
//            base.WriteElements(xwriter);
//        }
//        void Unregister()
//        {
//            m_projectInfo.RenderingContextChanged -= new EventHandler(projectInfo_RenderingContextChanged);
//        }
//        void Register()
//        {
//            m_projectInfo.RenderingContextChanged += new EventHandler(projectInfo_RenderingContextChanged);
//            if (this.m_projectInfo.RenderingContext != null)
//                RegisterRenderginContext();
//            m_surface.Invalidate();
//        }
//        void projectInfo_RenderingContextChanged(object sender, EventArgs e)
//        {
//            if (m_renderingContext != m_projectInfo.RenderingContext)
//            {
//                UnregisetRenderingContext();
//                m_renderingContext = m_projectInfo.RenderingContext;
//                if (m_renderingContext !=null)            
//                RegisterRenderginContext();
//            }
//        }
//        private void UnregisetRenderingContext()
//        {
//            this.m_projectInfo.RenderingContext.AfterRenderDocument -= new CoreRenderingContextEventHandler(RenderingContext_AfterRenderDocument);
//        }
//        private void RegisterRenderginContext()
//        {
//            this.m_projectInfo.RenderingContext.AfterRenderDocument += new CoreRenderingContextEventHandler(RenderingContext_AfterRenderDocument);
//        }
//        void RenderingContext_AfterRenderDocument(object sender, CoreRenderingContextEventArgs e)
//        {
//            Core2DRenderingContextEventArgs args = e as Core2DRenderingContextEventArgs;
//            ICore2DRenderingContext context = sender as ICore2DRenderingContext;
//            int i = this.m_surface.Documents.IndexOf(context.Document as ICore2DDrawingDocument);
//            Dictionary<string, string> v_values = new Dictionary<string, string>();
//            v_values.Add("PageNumber", i.ToString());
//            v_values.Add("TotalNumberOfPage", this.m_surface.Documents.Count.ToString());
//            string txt = PageManagerUtils.Evaluate(this.Pattern, v_values);
//            System.Drawing.Font ft =this.m_surface.Font;
//            System.Drawing.SizeF s = context.Graphics.MeasureString (txt,  ft);
//            float x = args.DocumentRectangle.Width - s.Width;// this.m_surface.CurrentDocument.Width - s.Width;
//            float y = args.DocumentRectangle.Height - s.Height;// this.m_surface.CurrentDocument.Height - s.Height;
//            switch (context.ContextName)
//            {
//                case "Print2DContext":
//                    break;
//                default:               
//                    break;
//            }
//            context.Graphics.DrawString(txt,
//               this.m_surface.Font, System.Drawing.Brushes.Black,
//               new System.Drawing.Vector2f(x,
//                   y));
//           }
//        public override bool CanModify
//        {
//            get
//            {
//                return false;
//            }
//        }
//        public override bool IsVisible
//        {
//            get
//            {
//                return true;
//            }
//        }
//        public override void LoadNode(System.Xml.XmlNode v_cnode, Resources.ICoreDeserializerResources resources)
//        {
//        }
//    }
//}

