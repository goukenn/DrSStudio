

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DocumentStatusInfoTools.cs
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
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.ICore.Drawing2D.Tools;
using IGK.ICore.Resources;
using IGK.ICore.Tools;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Tools
{
    [CoreTools("Tool.Drawing2D.DocumentStatusTool")]
    class DocumentStatusInfoTools : Core2DDrawingToolBase 
    {
        private static DocumentStatusInfoTools sm_instance;
        private DocumentStatusInfoTools()
        {
        }

        public static DocumentStatusInfoTools Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static DocumentStatusInfoTools()
        {
            sm_instance = new DocumentStatusInfoTools();
        }
        IGKWinCoreStatusTextItem m_info;
        private IGKWinCoreStatusTextItem m_sizeInfo;

        protected override void GenerateHostedControl()
        {
            m_info = new IGKWinCoreStatusTextItem();
            m_sizeInfo = new IGKWinCoreStatusTextItem();
            m_info.Bounds = new Rectanglef(0, 0, 120, 24);
            m_info.Index = 0x0500;

            m_sizeInfo.Index = 0x0501;
            m_sizeInfo.Bounds = new Rectanglef(0, 0, 70, 24);

            m_info.Visible = false;
            m_sizeInfo.Visible = false;
            var l = this.Workbench.GetLayoutManager();
            l.StatusControl.Items.Add(m_info);
            l.StatusControl.Items.Add(m_sizeInfo);
            base.GenerateHostedControl();
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
            UpdateTextInfo();
            this.m_sizeInfo.Visible = (this.CurrentSurface != null);
            this.updateSizeInfo();
            
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.CurrentDocumentChanged -= surface_CurrentDocumentChanged;
            surface.DocumentAdded -= surface_DocumentAdded;
            surface.DocumentRemoved -= surface_DocumentRemoved;
            base.UnRegisterSurfaceEvent(surface);
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.CurrentDocumentChanged += surface_CurrentDocumentChanged;
            surface.DocumentAdded += surface_DocumentAdded;
            surface.DocumentRemoved += surface_DocumentRemoved;
            
        }
        protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            base.RegisterDocumentEvent(document);
            document.PropertyChanged += document_PropertyChanged;
        }
        protected override void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            document.PropertyChanged -= document_PropertyChanged;
            base.UnRegisterDocumentEvent(document);
        }

        void document_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            this.UpdateTextInfo();
            this.updateSizeInfo();
        }

        void surface_DocumentRemoved(object sender, CoreItemEventArgs<ICore2DDrawingDocument> e)
        {
            this.UpdateTextInfo();
        }

        void surface_DocumentAdded(object sender, CoreItemEventArgs<ICore2DDrawingDocument> e)
        {
            this.UpdateTextInfo();
        }

        void surface_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
        {
            UpdateTextInfo();
            this.m_sizeInfo.Visible = (e.NewElement != null);
            this.updateSizeInfo();
        }
        private void updateSizeInfo()
        {
            if (this.m_sizeInfo.Visible) 
            {
                this.m_sizeInfo.Text = string.Format("{0}x{1}",
                    this.CurrentSurface.CurrentDocument.Width,
                    this.CurrentSurface.CurrentDocument.Height);
            }
        }
        private void UpdateTextInfo()
        {
            var e = this.CurrentSurface;
            if (e!=null)
            {
                this.m_info.Text = string.Format("Document:{0}/{1}",
                    e.CurrentDocument.ZIndex+1,
                    e.Documents.Count
                    );
                this.m_info.Visible = true;
            }
            else {
                this.m_info.Text = CoreResources.GetString ("tip.NoDocument");
                this.m_info.Visible = false;
            }
        }
    }
}
