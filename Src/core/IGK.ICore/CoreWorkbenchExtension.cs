using IGK.ICore.Web;
using IGK.ICore.Web.WinUI;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using IGK.ICore.WinUI.Registrable;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    /// <summary>
    /// represent a core workbench extension logic
    /// </summary>
    public static class CoreWorkbenchExtension
    {
        public static bool CreateNewFile(this ICoreSystemWorkbench workbench) {
            if (workbench is ICoreWorbenchFileCreator l)
                return l.CreateNewFile();
            return false;

        }
        
         public static bool CreateNewProject(this ICoreSystemWorkbench workbench)
        {
            if (workbench is ICoreWorbenchProjectCreator l)
                return l.CreateNewProject();
            return false;

        }
        /// <summary>
        /// Add Surface to workbench
        /// </summary>
        /// <param name="workbench">global workbench</param>
        /// <param name="surface">surface to add</param>
        /// <param name="selected">true to select the surface otherwise false</param>
        public static void AddSurface(this ICoreSystemWorkbench workbench, ICoreWorkingSurface surface, bool selected)
        {
            if (workbench is ICoreWorkingSurfaceContainer c)
            {
                c.AddSurface(surface);
                if (selected)
                {
                    c.CurrentSurface = surface;
                }
            }
            else
            {
                if (workbench is ICoreSurfaceManagerWorkbench r)
                {
                    r.Surfaces.Add(surface);
                    if (selected)
                    {
                        r.CurrentSurface = surface;
                    }
                }
            }
        }
        /// <summary>
        /// get a surface in workbench by name
        /// </summary>
        /// <param name="workbench"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ICoreWorkingSurface GetSurface(this ICoreSystemWorkbench workbench, string name)
        {
            ICoreSurfaceManagerWorkbench b = workbench as ICoreSurfaceManagerWorkbench;
            if (b != null)
            {
                return b.Surfaces[name];
            }
            return null;
        }

        public static void RemoveSurface(this ICoreSystemWorkbench Workbench, ICoreWorkingSurface surface)
        {
            ICoreWorkingSurfaceContainer c = Workbench as ICoreWorkingSurfaceContainer;
            if (c != null)
            {
                c.RemoveSurface(surface);
            }
        }

        ///// <summary>
        ///// create a web browser dialog
        ///// </summary>
        ///// <param name="Workbench">must past the workbench to use</param>
        ///// <param name="dialogProvider">dialog provider setting class</param>
        ///// <returns></returns>
        //public static ICoreWebDialogForm CreateWebBrowserDialog(
        //    this ICoreSystemWorkbench Workbench,
        //    ICoreWebDialogProvider dialogProvider)
        //{
        //    if (Workbench == null) return null;
        //    if (Workbench is ICoreWebWorkbench wb)
        //    {
        //        var s = wb.CreateWebBrowserDialog(dialogProvider.OjectForScripting);
        //        dialogProvider.Dialog = s;
        //        return s;
        //    }

        //    if (CoreControlFactory.CreateControl(
        //        typeof(IWebBrowserControl).Name) is IWebBrowserControl control)
        //    {
        //        if (Workbench.CreateNewDialog(control) is ICoreWebDialogForm dialog)
        //        {
        //            control.ObjectForScripting = dialogProvider.OjectForScripting;
        //            // control.HtmlDocument = dialogProvider.Document.Render(null);

        //            dialogProvider.Dialog = dialog;
        //            return dialog;
        //        }

        //    }
        //    return null;
        //}

        //public static ICoreDialogForm CreateWebBrowserDialog(
        //    this ICoreSystemWorkbench Workbench,
        //    object objectForScripting,
        //    string document)
        //{
        //    if (Workbench is ICoreWebWorkbench wb && (objectForScripting is ICoreWebScriptObject))
        //    {
        //        return wb.CreateWebBrowserDialog(objectForScripting as ICoreWebScriptObject, document);
        //    }
        //    CoreXmlWebDocument doc = CoreXmlWebDocument.CreateICoreDocument();
        //    doc.Body.LoadString(document);
        //    if (CoreControlFactory.CreateControl(
        //        typeof(IWebBrowserControl).Name) is IWebBrowserControl control)
        //    {
        //        var dialog = Workbench.CreateNewDialog(control);
        //        control.ObjectForScripting = objectForScripting;
        //        //control.HtmlDocument = doc.Render(null);
        //        return dialog;

        //    }
        //    return null;
        //}

        public static ICoreWorkbenchLayoutManager GetLayoutManager(this ICoreSystemWorkbench workbench) {
            if (workbench is ICoreLayoutManagerWorkbench s) {
                return s.LayoutManager;
            }
            return null;
        }
        public static bool SetCurrentSurface(this ICoreSystemWorkbench workbench, ICoreWorkingSurface surface) {

            if (workbench is ICoreWorkbench m) {
                m.CurrentSurface = surface;
                return true;
            }
            return false;

        }
    }
}
