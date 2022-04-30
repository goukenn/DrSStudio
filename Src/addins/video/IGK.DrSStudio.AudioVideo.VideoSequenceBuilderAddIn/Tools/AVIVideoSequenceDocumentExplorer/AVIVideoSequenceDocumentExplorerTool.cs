using IGK.DrSStudio.AudioVideo.WinUI;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Tools;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo.Tools
{
    [CoreTools("AVIVideoSequenceExplorerTool")]
    class AVIVideoSequenceDocumentExplorerTool : Core2DDrawingToolBase
    {
        private static AVIVideoSequenceDocumentExplorerTool sm_instance;
        private AVIVideoSequenceDocumentExplorerTool()
        {
        }

        public static AVIVideoSequenceDocumentExplorerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AVIVideoSequenceDocumentExplorerTool()
        {
            sm_instance = new AVIVideoSequenceDocumentExplorerTool();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new UIDocumentVideoSequencePropertyExplorer();
        }
        protected override void OnCurrentSurfaceChanged(ICore.CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
            
        }
        protected override void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            this.loadProperties(null);
            base.UnRegisterDocumentEvent(document);
        }
        protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            base.RegisterDocumentEvent(document);
            this.loadProperties(document);
        }

        private void loadProperties(ICore2DDrawingDocument document)
        {
            UIDocumentVideoSequencePropertyExplorer s = this.HostedControl as
                UIDocumentVideoSequencePropertyExplorer;
            //s.loadProperties(document as ICoreWorkingAttachedPropertyObject);
        }
    }
}
