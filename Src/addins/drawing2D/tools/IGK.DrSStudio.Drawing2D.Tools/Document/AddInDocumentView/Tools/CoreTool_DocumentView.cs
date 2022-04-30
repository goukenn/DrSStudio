

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreTool_DocumentView.cs
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

  
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing2D.Tools
{    
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Tools;
    using IGK.ICore.Drawing2D.Tools;
    [CoreTools("Tool.Drawing2DDocumentView", ImageKey=CoreImageKeys.MENU_DOCUMENT_GKDS)]
    class CoreTool_DocumentView : Core2DDrawingToolBase 
    {
        private static CoreTool_DocumentView sm_instance;

        public static CoreTool_DocumentView Instance
        {
            get
            {

                if (sm_instance == null)
                    sm_instance = new CoreTool_DocumentView();
                return sm_instance;
            }
        }

        private CoreTool_DocumentView()
        {

        }

        public new UIXDocumentViewControl HostedControl {
            get {
                return base.HostedControl as UIXDocumentViewControl;
            }
            set {
                base.HostedControl = value;
            }
        }
      

        protected override void GenerateHostedControl()
        {
            this.HostedControl = new UIXDocumentViewControl();
            
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            if (Workbench is ICoreLayoutManagerWorkbench s)                
            (s.LayoutManager as CoreLayoutManagerBase ).RegisterToContextMenu(this.HostedControl);
        }
        
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);

            surface.DocumentAdded += surface_DocumentAdded;
            surface.DocumentRemoved += surface_DocumentRemoved;            
            surface.CurrentDocumentChanged += surface_CurrentDocumentChanged;

            ICore2DDrawingPositionnableDocumentSurface c = surface as ICore2DDrawingPositionnableDocumentSurface;
            if (c != null)
            {
                c.DocumentZIndexChanged += c_DocumentZIndexChanged;
            }


            this.HostedControl.SetUpSurface();
        }

        void c_DocumentZIndexChanged(object o, CoreWorkingObjectZIndexChangedEventArgs e)
        {
            this.HostedControl.SetUpSurface();
        }

     
      
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {

            surface.DocumentAdded -= surface_DocumentAdded;
            surface.DocumentRemoved -= surface_DocumentRemoved;
            surface.CurrentDocumentChanged -= surface_CurrentDocumentChanged;        
            base.UnRegisterSurfaceEvent(surface);
            this.HostedControl.SetUpSurface();
        }
        private void surface_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
        {
            this.HostedControl.SelectDocument(e.NewElement as ICore2DDrawingDocument );
            //this.HostedControl.Invalidate(true);
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
            this.HostedControl.SetupDocumentProperty();
        }


        private void surface_DocumentAdded(object sender, CoreItemEventArgs<ICore2DDrawingDocument> e)
        {

            this.HostedControl.AddNewDocument(e.Item );         
            
        }
        void surface_DocumentIndexChanged(object o, CoreWorkingObjectZIndexChangedEventArgs e)
        {
            this.HostedControl.SetDocumentNewIndex(e.Item as ICore2DDrawingDocument  );         
        }
        void surface_DocumentRemoved(object sender, CoreItemEventArgs<ICore2DDrawingDocument> e)
        {
         
            this.HostedControl.RemoveDocument(e.Item );                       
        }
        
    }
}
