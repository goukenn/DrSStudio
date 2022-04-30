

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingContextMenuBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DDrawingContextMenuBase.cs
*/
using IGK.ICore.ContextMenu;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    public abstract class IGKD2DDrawingContextMenuBase : CoreContextMenuBase
    {

        protected bool CheckOverSingleElement()
        {
            Vector2f vf = new Vector2f();
            //get the screen location
            if (this.CurrentSurface == null)
            {
                return false;
            }
            vf = this.CurrentSurface.PointToClient(CoreApplicationManager.Application.ControlManager.MouseLocation);
            bool c = (((this.OwnerContext.SourceControl) == this.CurrentSurface)
                && (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1)
                && (this.CurrentSurface.CurrentLayer.SelectedElements[0].Contains(
                this.CurrentSurface.GetFactorLocation(vf))));
            return c;
        }

        /// <summary>
        /// check ove a element
        /// </summary>
        /// <param name="vector2f"></param>
        /// <returns></returns>
        protected bool CheckOverElements(Vector2f location)
        {
            ICore2DDrawingSurface c = this.CurrentSurface;
            if (c == null || (c.CurrentLayer.SelectedElements.Count == 0))
                return false;
            foreach (ICore2DDrawingLayeredElement l in c.CurrentLayer.SelectedElements)
            {
                if (l.Contains(location))
                    return true;
            }
            return false;
        }
        protected bool CheckIsMulti()
        {
            Vector2f vf = new Vector2f();
            ICore2DDrawingSurface v_2dsurface = this.CurrentSurface;
            //get the screen location
            if ((this.CurrentSurface == null) || (!this.AllowContextMenu))
            {
                return false;
            }
            vf = this.CurrentSurface.PointToClient(
                Vector2f.Round(this.OwnerContext.MouseOpeningLocation));


            bool c = (((this.OwnerContext.SourceControl) == this.CurrentSurface)
                && (v_2dsurface.CurrentLayer.SelectedElements.Count > 0)
                && (CheckPosOverMultiItem(v_2dsurface.GetFactorLocation(vf))));
            return c;
        }
        /// <summary>
        /// check if over one or more element
        /// </summary>
        /// <param name="vector2f"></param>
        /// <returns></returns>
        protected bool CheckPosOverMultiItem(Vector2f vector2f)
        {
            ICore2DDrawingSurface v_2dsurface = this.CurrentSurface;
            foreach (ICore2DDrawingLayeredElement l in v_2dsurface.CurrentLayer.SelectedElements)
            {
                if (l.Contains(vector2f)) return true;
            }
            return false;
        }
        public new ICore2DDrawingSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICore2DDrawingSurface;
            }
        }
        protected override void OnWorkbenchChanged(EventArgs eventArgs)
        {
            
            base.OnWorkbenchChanged(eventArgs);
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            ICore2DDrawingSurface c = this.CurrentSurface;
            if (c != null)
            {
                this.UnRegisterLayerEvent(c.CurrentLayer);
                this.UnRegisterDocumentEvent(c.CurrentDocument);
                this.UnRegisterSurfaceEvent(c);
            }

            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
        }
        void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            if (e.OldElement is ICore2DDrawingSurface)
            {
                ICore2DDrawingSurface c = e.OldElement as ICore2DDrawingSurface;
                
                //this.UnRegisterLayerEvent(c.CurrentLayer);
                this.UnRegisterDocumentEvent(c.CurrentDocument);
                this.UnRegisterSurfaceEvent(c);
            }
            if (e.NewElement is ICore2DDrawingSurface)
            {
                ICore2DDrawingSurface c = e.NewElement as ICore2DDrawingSurface;
                this.RegisterSurfaceEvent(c);
                this.RegisterDocumentEvent(c.CurrentDocument);
                //this.RegisterLayerEvent(c.CurrentLayer);
            }
            this.SetupEnableAndVisibility();
        }
        protected virtual void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            document.CurrentLayerChanged += document_CurrentLayerChanged;
            this.RegisterLayerEvent(document.CurrentLayer);
        }

        void document_CurrentLayerChanged(object sender, Core2DDrawingLayerChangedEventArgs e)
        {
            if (e.OldElement != null)
                this.UnRegisterLayerEvent(e.OldElement);
            if (e.NewElement != null)
                this.RegisterLayerEvent(e.NewElement);
        }
        protected virtual void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            this.UnRegisterLayerEvent(document.CurrentLayer);
            document.CurrentLayerChanged -= document_CurrentLayerChanged;
       
        }
        protected virtual void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
        }
        protected virtual void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
        }
        protected override void InitContextMenu()
        {
            base.InitContextMenu();
        }
        protected override bool IsEnabled()
        {
            return this.CurrentSurface != null;
        }
        protected override bool IsVisible()
        {
            return base.IsVisible () && (this.CurrentSurface != null) && this.AllowContextMenu ;
        }
        protected virtual void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.CurrentDocumentChanged -= surface_CurrentDocumentChanged;
        }

        void surface_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
        {
            if (e.OldElement !=null)
                this.UnRegisterDocumentEvent(e.OldElement as ICore2DDrawingDocument );
            if (e.NewElement != null)
                this.RegisterDocumentEvent(e.NewElement as ICore2DDrawingDocument);
        }
        protected virtual void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.CurrentDocumentChanged += surface_CurrentDocumentChanged;
        }
    }
}

