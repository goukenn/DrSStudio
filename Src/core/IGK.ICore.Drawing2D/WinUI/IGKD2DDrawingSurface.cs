

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using IGK.ICore.Settings;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DDrawingSurface.cs
*/
using IGK.ICore.WorkingObjects;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Drawing2D.Print;

namespace IGK.ICore.Drawing2D.WinUI
{
    [CoreSurface (
        CoreConstant.DRAWING2D_SURFACE_TYPE ,
        EnvironmentName=CoreConstant.DRAWING2D_ENVIRONMENT )
    ]
    public class IGKD2DDrawingSurface :
        IGKD2DDrawingSurfaceBase ,
        ICoreWorkingEditableSurface,
        ICoreWorkingPasteAtSurface,
        ICoreWorkingResourcesContainerSurface,
        ICoreWorkingProjectManagerSurface,
        ICoreWorkingInitializableObject
    {

        private ResourceElement m_resourceElement; // get the resouces element
        private GKDSElement m_gkdsElement;

        /// <summary>
        /// get the source project
        /// </summary>
        public GKDSElement GkdsElement
        {
            get { return m_gkdsElement; }
        }
        public override bool IsToolValid(Type t)
        {
            Core2DDrawingGroupAttribute v_attr = Core2DDrawingObjectAttribute.GetCustomAttribute
               (t, typeof(Core2DDrawingGroupAttribute)) as Core2DDrawingGroupAttribute;
            if (v_attr == null)
                return false;
            if ((v_attr.MecanismType == null) || (v_attr.Environment != this.SurfaceEnvironment))
                return false;
            return true;
        }
        protected override void CreateElementToConfigureManager()
        {
            new IGK2DDrawingElementConfigurationManager(this);
        }

        public override void Print()
        {
            Type t = CoreAssemblyUtility.FindType("IGK.DrSStudio.Drawing2D.Print.IGKD2DDrawingPrint");
            if (t != null) { 
            ICorePrinter printer = Activator.CreateInstance(t, new object[] {
            this}) as ICorePrinter;
            // CoreSystem.CreateWorkingObject()
            //new IGKD2DDrawingPrint(this).Print();
            if (printer != null)
                printer.Print();
            }
        }
        public override void PrintPreview()
        {
            Type t = CoreAssemblyUtility.FindType("IGK.DrSStudio.Drawing2D.Print.IGKD2DDrawingPrint");
            if (t != null)
            {
                ICorePrinter printer = Activator.CreateInstance(t, new object[] {
            this}) as ICorePrinter;
                // CoreSystem.CreateWorkingObject()
                //new IGKD2DDrawingPrint(this).Print();
                if (printer != null)
                    printer.PrintPreview();
            }
            // new IGKD2DDrawingPrint(this).PrintPreview();
        }

        /// <summary>
        /// the overrided method support width and height as surface initializatorparam
        /// </summary>
        /// <param name="p"></param>
        public override void SetParam(ICoreInitializatorParam p)
        {
            if (p == null) return;

            CoreUnit width = p["width"];
            CoreUnit height = p["height"];

            this.CurrentDocument.SetSize(
                (int)Math.Ceiling (width.GetPixel ()),
                (int)Math.Ceiling (height.GetPixel()));
        }
      
        public IGKD2DDrawingSurface()
        {
            this.CurrentDocumentChanged += _CurrentDocumentChanged;
            this.OnRefreshServiceListener(new IGKD2DDrawingSurfaceZoomListener());
        }
        /// <summary>
        /// class to initialize the project surface
        /// </summary>
        void ICoreWorkingInitializableObject.Initialize()
        {
            this.m_gkdsElement = GKDSElement.Create(this);
            ProjectElement p =  this.GetProjectElement();
            p["Author"].Value = CoreSettings.GetSettingValue(CoreConstant.APP_ROOT_SETTING+".UserInfo.Author", string.Empty) as string;
            p["Creation"].Value = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            p["Version"].Value = "1.0";
        }
        void _CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
        {
            this.RefreshScene();
        }
        /// <summary>
        /// create a surface according to the gkds document
        /// </summary>
        /// <param name="gkds"></param>
        /// <returns></returns>
        public static IGKD2DDrawingSurface CreateSurface(GKDSElement gkds)
        {
            if (gkds == null)
                return null;
            //extract document element
            DocumentElement v_documentElement = gkds.GetDocument();
            if (v_documentElement == null)
                return null;
            ICoreWorkingDocument[] v_documents = v_documentElement.Documents.ToArray();
            v_documentElement.Documents.Clear();

            List<Core2DDrawingDocumentBase> m_doc = new List<Core2DDrawingDocumentBase>();
            //get all docement
            foreach (Core2DDrawingDocumentBase c in v_documents)
            {
                if (c == null) continue;
                m_doc.Add(c);
            }
            if (m_doc.Count > 0)
            {
                IGKD2DDrawingSurface v_surface = new IGKD2DDrawingSurface();
                v_surface.Documents.Replace(m_doc.ToArray());
                v_surface.m_gkdsElement  = gkds;
                gkds.Surface = v_surface;

                ProjectElement v_projectInfo = gkds.getElementTagObjectByTagName(CoreConstant.TAG_PROJECT) as ProjectElement;
                ResourceElement v_resource = gkds.GetResourceElement(true);
                if (v_projectInfo != null)
                {
                    v_surface.FileName = v_projectInfo.GetValue("FileName");
                }

                if (v_resource != null)
                {
                    v_surface.m_resourceElement = v_resource ;
                }
                v_surface.NeedToSave = false;
                return v_surface;
            }
            return null;
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // IGK2DDrawingSurface
            // 
            this.Name = "IGK2DDrawingSurface";
            this.Size = new System.Drawing.Size(464, 249);
            this.ResumeLayout(false);
        }

        #region "EditableSurface Properties"
        private readonly string[] m_CopyData = new string[] { 
            "Drawing2DElement",
            "Image",
            System.Windows.Forms.DataFormats.Bitmap,
            System.Windows.Forms.DataFormats.EnhancedMetafile ,
            System.Windows.Forms.DataFormats.MetafilePict ,
            System.Windows.Forms.DataFormats.FileDrop
        };

        public virtual  bool CanCopy
        {
            get {  return (this.CurrentLayer.SelectedElements.Count > 0); }
        }

        public virtual bool CanCut
        {
            get { return CanCopy; }
        }

        public bool CanPaste
        {
            get { return WinCoreClipBoard.Contains(m_CopyData); }
        }

        public void Copy()
        {
            if (this.CanCopy)
            {
                ICore2DDrawingLayeredElement[] v_items = this.CurrentLayer.SelectedElements.ToArray();
                IGK2DDClipBoard.CopyToClipBoard(IGK2DDClipBoard.TAG_COPY_CONTEXT, this, v_items);
            }
        }

        public virtual void Cut()
        {
            if (this.CanCut)
            {
                ICore2DDrawingLayeredElement[] v_items = this.CurrentLayer.SelectedElements.ToArray();
                IGK2DDClipBoard.CopyToClipBoard(
                    IGK2DDClipBoard.TAG_CUT_CONTEXT, 
                    this,
                    v_items);
                this.CurrentLayer.Select(null);
                this.CurrentLayer.Elements.RemoveAll(v_items);
                this.RefreshScene();
            }
        }

        /// <summary>
        /// retrieve all pastabe items
        /// </summary>
        /// <returns></returns>
        public ICore2DDrawingLayeredElement[] GetPastableItems()
        {
            return WinCoreClipBoard.GetPastableItems(m_CopyData);
           
        }
        /// <summary>
        /// paste items at the location
        /// </summary>
        /// <param name="point">location where to paste</param>
        public void PasteAt(Vector2f point)
        {
            if (this.CanPaste)
            {
                System.Drawing.Point pt =
                    this.PointToClient(new System.Drawing.Point((int)point.X,(int) point.Y));
                point = GetFactorLocation(new Vector2f (pt.X, pt.Y));
                ICore2DDrawingLayeredElement[] t = GetPastableItems();
                if (t.Length > 0)
                {
                    Rectanglef rc = Rectanglef.Empty;
                    bool v_firstOffset = false;
                    Vector2f v_offset = Vector2f.Zero;
                    foreach (ICore2DDrawingLayeredElement item in t)
                    {
                        if (v_firstOffset == false)
                        {
                            rc = item.GetBound();
                            v_offset = CoreMathOperation.GetDistanceP(rc.Location,
                                point);
                            v_firstOffset = true;
                        }
                        item.Translate(-v_offset.X, -v_offset.Y, enuMatrixOrder.Append);
                        this.CurrentLayer.Elements.Add(item);
                    }
                    this.Invalidate();
                }
            }
        }
        public void Paste()
        {
            if (this.CanPaste)
            {
                ICore2DDrawingLayeredElement[] t = GetPastableItems();
                if ((t==null) || (t.Length == 0))
                {
                    CoreLog.WriteLine ("No Element found to paste or not valid");
                }
                foreach (ICore2DDrawingLayeredElement item in t)
                {
                    this.CurrentLayer.Elements.Add(item);
                }
                this.RefreshScene();
            }
        }

        public static IGKD2DDrawingSurface CreateSurface(CoreUnit width, CoreUnit height)
        {
            
           var s = new  IGKD2DDrawingSurface ();
            s.CurrentDocument.SetSize (width.GetPixel(), height.GetPixel());
            s.CurrentDocument.BackgroundTransparent = true;
            return s;
        }

        #endregion

        public ICoreResourceContainer Resources
        {
            get {
                return (this.m_resourceElement!=null)? this.m_resourceElement.Resources : null;
            }
        }



        class IGK2DDrawingElementConfigurationManager
        {
            private IGKD2DDrawingSurface m_surface;

            public IGK2DDrawingElementConfigurationManager(IGKD2DDrawingSurface iGKD2DDrawingSurface)
            {

                this.m_surface = iGKD2DDrawingSurface;
                this.m_surface.CurrentDocumentChanged += m_surface_CurrentDocumentChanged;
                if (this.m_surface.CurrentDocument != null)
                    RegisterDocumentEvent(this.m_surface.CurrentDocument);
                this.Setup();
            }

            private void RegisterLayerEvent(ICore2DDrawingLayer layer)
            {
                layer.SelectedElementChanged += layer_SelectedElementChanged;
            }
            private void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
            {
                layer.SelectedElementChanged -= layer_SelectedElementChanged;
            }

            void layer_SelectedElementChanged(object sender, EventArgs e)
            {
                this.Setup();
            }

            void m_surface_CurrentLayerChanged(object sender, Core2DDrawingLayerChangedEventArgs e)
            {
                this.Setup();
            }

            void m_surface_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
            {
                if (e.OldElement is Core2DDrawingDocumentBase) UnRegisterDocumentEvent(e.OldElement as Core2DDrawingDocumentBase);
                if (e.NewElement is Core2DDrawingDocumentBase) RegisterDocumentEvent(e.NewElement as Core2DDrawingDocumentBase);
                this.Setup();

            }

            private void RegisterDocumentEvent(Core2DDrawingDocumentBase document)
            {
                document.CurrentLayerChanged += document_CurrentLayerChanged;
                RegisterLayerEvent(document.CurrentLayer);
            }
            private void UnRegisterDocumentEvent(Core2DDrawingDocumentBase document)
            {
                UnRegisterLayerEvent(document.CurrentLayer);
                document.CurrentLayerChanged -= document_CurrentLayerChanged;
            }

            void document_CurrentLayerChanged(object sender, Core2DDrawingLayerChangedEventArgs e)
            {
                if (e.OldElement is ICore2DDrawingLayer) this.UnRegisterLayerEvent(e.OldElement as ICore2DDrawingLayer);
                if (e.NewElement is ICore2DDrawingLayer) this.RegisterLayerEvent(e.NewElement as ICore2DDrawingLayer);
            }


            void Setup()
            {
                if ((this.m_surface.CurrentLayer != null) && (this.m_surface.CurrentLayer.SelectedElements.Count >= 1))
                    this.m_surface.ElementToConfigure = this.m_surface.CurrentLayer.SelectedElements[0];
                else
                    this.m_surface.ElementToConfigure = null;
            }
        }



        /// <summary>
        /// register a zoom surface listener. as demonstration.
        /// </summary>
        /// <note>
        /// register a system listener event of surface
        /// </note>
        public class IGKD2DDrawingSurfaceZoomListener : IIGKD2DrawingSurfaceListener
        {
            private IGKD2DDrawingSurfaceBase m_surface;
            ///<summary>
            ///public .ctr
            ///</summary>
            public IGKD2DDrawingSurfaceZoomListener():base()
            {
            }
            public void UnregisterService(IGKD2DDrawingSurfaceBase surface)
            {
                surface.ZoomModeChanged -= _ZoomChanged;
                surface.ZoomChanged -= _ZoomChanged;
                m_surface = null;
            }

            public void RegisterService(IGKD2DDrawingSurfaceBase surface)
            {
                surface.ZoomModeChanged += _ZoomChanged;
                surface.ZoomChanged += _ZoomChanged;
                this.m_surface = surface;
            }

            void _ZoomChanged(object sender, EventArgs e)
            {
                this.m_surface.RefreshScene();
            }
        }

      
    }
}

