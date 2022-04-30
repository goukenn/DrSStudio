

/*
IGKDEV @ 2008 - 2014
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
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:CoreTool_DocumentView.cs
*/
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Tools
{    
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Core;
    [CoreTools("Tool.Drawing2DDocumentView", ImageKey="Menu_Document")]
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
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.DocumentAdded += new Core2DDrawingDocumentEventHandler(surface_DocumentAdded);
            surface.DocumentRemoved += new Core2DDrawingDocumentEventHandler(surface_DocumentRemove);
            surface.DocumentZIndexChanged += new CoreWorkingObjectZIndexChangedHandler(surface_DocumentIndexChanged);
            surface.CurrentDocumentChanged += surface_CurrentDocumentChanged;
            this.HostedControl.SetUpSurface();
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {            
            surface.DocumentAdded -= new Core2DDrawingDocumentEventHandler(surface_DocumentAdded);
            surface.DocumentRemoved -= new Core2DDrawingDocumentEventHandler(surface_DocumentRemove);
            surface.DocumentZIndexChanged -= new CoreWorkingObjectZIndexChangedHandler(surface_DocumentIndexChanged);
            surface.CurrentDocumentChanged -= surface_CurrentDocumentChanged;            
            base.UnRegisterSurfaceEvent(surface);
        }
        void surface_DocumentIndexChanged(object o, CoreWorkingObjectZIndexChangedArgs e)
        {
            this.HostedControl.SetDocumentNewIndex(e.Item as ICore2DDrawingDocument  );         
        }
        void surface_DocumentRemove(object o, Core2DDrawingDocumentEventArgs e)
        {           
            this.HostedControl.RemoveDocument(e.Document);                       
        }
        void surface_DocumentAdded(object o, Core2DDrawingDocumentEventArgs e)
        {
            this.HostedControl.AddNewDocument(e.Document);         
        }
        void surface_CurrentDocumentChanged(object o, CoreElementChangedEventArgs<ICore2DDrawingDocument>  e)
        {
            this.HostedControl.SelectDocument(e.NewElement as ICore2DDrawingDocument );
        }
    }
}

