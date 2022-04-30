using IGK.ICore.Web;
using IGK.ICore.Web.WinUI;
using IGK.ICore.WinUI.Registrable;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Common
{
    public static class CoreCommonDialogUtility
    {
        
        public static IXCoreOpenDialog CreateOpenFileDialog(this ICoreSystemWorkbench bench)
        {
            if (bench is ICoreWorkbenchDialogFactory m)
                return m.CreateOpenFileDialog();
            return null;
        }
        public static IXCoreColorDialog CreateColorDialog(this ICoreSystemWorkbench bench) {
            if (bench is ICoreWorkbenchDialogFactory m)
                return m.CreateColorDialog();
            return null;
        }
        public static IXCoreSaveDialog CreateNewSaveDialog(this ICoreSystemWorkbench bench) {
            if (bench is ICoreWorkbenchDialogFactory m)
                return m.CreateNewSaveDialog();
            return null;
        }
        public static ICoreDialogForm CreateNewDialog(this ICoreSystemWorkbench bench) {
            if (bench is ICoreWorkbenchDialogFactory m)
            {
                return m.CreateNewDialog();
            }
            return null;
        }
        public static ICoreDialogForm CreateNewDialog(this ICoreSystemWorkbench bench, ICoreControl baseControl) {
            if (bench is ICoreWorkbenchDialogFactory m)
            {
                return m.CreateNewDialog(baseControl);
            }
            return null;
        }

        /// <summary>
        /// create a new commond dialog
        /// </summary>
        /// <param name="name">common dialog name</param>
        /// <returns>return the common dialog</returns>
        public static T CreateCommonDialog<T>(this ICoreSystemWorkbench bench, string name) where T : IXCommonDialog {
            if (bench is ICoreWorkbenchDialogFactory m) {
                return m.CreateCommonDialog<T>(name);
            }
            return default(T);
        }
        /// <summary>
        /// create a common dialog from the type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateCommonDialog<T>(this ICoreSystemWorkbench bench) where T : IXCommonDialog {
            if (bench is ICoreWorkbenchDialogFactory m)
            {
                return m.CreateCommonDialog<T>();
            }
            return default(T);
        }


        public static string PickFolder(this ICoreSystemWorkbench coreWorkbench, string title=null, string selectFolder=null)
        {
            if (coreWorkbench==null)
                return null;
            FolderNamePicker fd = coreWorkbench.CreateCommonDialog<FolderNamePicker>(
       CoreCommonDialogs.FolderNamePicker);
            if (fd != null)
            {
                fd.Title = title;
                fd.SelectedFolder = selectFolder ?? Environment.CurrentDirectory;
                if (fd.ShowDialog() == enuDialogResult.OK)
                {
                    return fd.SelectedFolder;
                }
            }
            return selectFolder ?? Environment.CurrentDirectory;
        }

        public static string PickFileName(this ICoreSystemWorkbench coreWorkbench, string title = null, string filename = null)
        {
            if (coreWorkbench == null)
                return null;
            FileNamePicker fd = coreWorkbench.CreateCommonDialog<FileNamePicker>(
       CoreCommonDialogs.FileNamePicker)
      ;
            if (fd != null)
            {
                fd.Title = title;
                fd.FileName = filename;
                if (fd.ShowDialog() == enuDialogResult.OK)
                {
                    return fd.FileName;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// return a json dictionary if ok or null
        /// </summary>
        /// <param name="coreWorkbench">workbench source</param>
        /// <param name="stringResource">string document</param>
        /// <returns>return null or a provider result</returns>
        public static object BuildWebDialog(this ICoreSystemWorkbench coreWorkbench, string title, string data)
        {
            if (!(coreWorkbench is ICoreWebWorkbench) || string.IsNullOrEmpty(data))
                return null;
            
            var provider = new CoreCommonWebDialogProvider();
            using (var d = coreWorkbench.CreateWebBrowserDialog(
            provider.OjectForScripting,
            data))
            {
                if ((d != null) && d is ICoreWebDialogForm h)
                {
                    provider.Dialog = d;
                    h.WebControl.SetReloadDocumentListener (provider);
                    d.Title = title;
                    if (d.ShowDialog() == enuDialogResult.OK)
                    {
                        return provider.Result;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// create a web browser dialog
        /// </summary>
        /// <param name="Workbench">must past the workbench to use</param>
        /// <param name="dialogProvider">dialog provider setting class</param>
        /// <returns></returns>
        public static ICoreWebDialogForm CreateWebBrowserDialog(
            this ICoreSystemWorkbench Workbench,
            ICoreWebDialogProvider dialogProvider)
        {
            if (Workbench == null) return null;
            if (Workbench is ICoreWebWorkbench wb)
            {
                var s = wb.CreateWebBrowserDialog(dialogProvider.OjectForScripting);
                dialogProvider.Dialog = s;
                return s;
            }

            if (CoreControlFactory.CreateControl(
                typeof(IWebBrowserControl).Name) is IWebBrowserControl control)
            {
                if (Workbench.CreateNewDialog(control) is ICoreWebDialogForm dialog)
                {
                    control.ObjectForScripting = dialogProvider.OjectForScripting;
                    // control.HtmlDocument = dialogProvider.Document.Render(null);

                    dialogProvider.Dialog = dialog;
                    return dialog;
                }

            }
            return null;
        }

        public static ICoreDialogForm CreateWebBrowserDialog(
            this ICoreSystemWorkbench Workbench,
            object objectForScripting,
            string document)
        {
            if (Workbench is ICoreWebWorkbench wb && (objectForScripting is ICoreWebScriptObject))
            {
                return wb.CreateWebBrowserDialog(objectForScripting as ICoreWebScriptObject, document);
            }
            CoreXmlWebDocument doc = CoreXmlWebDocument.CreateICoreDocument();
            doc.Body.LoadString(document);
            if (CoreControlFactory.CreateControl(
                typeof(IWebBrowserControl).Name) is IWebBrowserControl control)
            {
                var dialog = Workbench.CreateNewDialog(control);
                control.ObjectForScripting = objectForScripting;
                //control.HtmlDocument = doc.Render(null);
                return dialog;

            }
            return null;
        }

        //public static ICoreWebDialogForm CreateWebBrowserDialog(this ICoreSystemWorkbench bench, ICoreWebScriptObject objectForScripting) {
        //    if (bench is ICoreWebWorkbench m) {
        //        return m.CreateWebBrowserDialog(objectForScripting);
        //    }
        //    return null;
        //}

        //public static ICoreWebDialogForm CreateWebBrowserDialog(this ICoreSystemWorkbench bench, ICoreWebScriptObject objectForScripting, string document) {
        //    if (bench is ICoreWebWorkbench m)
        //    {
        //        return m.CreateWebBrowserDialog(objectForScripting, document);
        //    }
        //    return null;
        //}
        public static ICoreWebDialogForm ShowDialog(this ICoreSystemWorkbench bench, ICoreWebScriptObject objectForScripting, string document)
        {
            if (bench is ICoreWorkbenchDialogFactory m)
            {
                return m.ShowDialog(objectForScripting, document);
            }
            return null;
        }
        public static enuDialogResult ShowDialog(this ICoreSystemWorkbench bench, string title , ICoreControl control)
        {
            if (bench is ICoreWorkbenchDialogFactory m)
            {
                return m.ShowDialog(title, control);
            }
            return enuDialogResult.None;
        }

    }
}
