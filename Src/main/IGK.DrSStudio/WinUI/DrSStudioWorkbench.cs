

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioWorkbench.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.IO;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:DrSStudioWorkbench.cs
*/
using IGK.ICore.Resources;
using IGK.ICore.Tools;
using IGK.ICore.Web;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinUI.Registrable;
using System.Runtime.InteropServices;

namespace IGK.DrSStudio.WinUI
    
{
    [Serializable()]
    /// <summary>
    /// rerpresent the drs stutio workbench
    /// </summary>
    public class DrSStudioWorkbench : 
        WinCoreWorkbenchBase , 
        ICoreWorkbenchDocumentInitializer,
        ICoreWorkbenchMessageFilter
    {
        public override ICoreActionRegisterTool ActionRegister
        {
            get
            {
                return CoreActionRegisterTool.Instance;
            }
        }

     
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="form"></param>
        public DrSStudioWorkbench(DrSStudioMainForm form):base(form)
        {
            this.FileOpened += DrSStudioWorkbench_FileOpened;
            this.CurrentSurfaceChanged += DrSStudioWorkbench_CurrentSurfaceChanged;
        }

        private void DrSStudioWorkbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            if (e.OldElement != null)

                if (e.OldElement is ICoreWorkingFilemanagerSurface)
                (e.OldElement as ICoreWorkingFilemanagerSurface).FileNameChanged -= ChangeSurfaceFile;


            if (e.NewElement !=null) 
            if (e.NewElement is ICoreWorkingFilemanagerSurface)
                    (e.NewElement as ICoreWorkingFilemanagerSurface).FileNameChanged += ChangeSurfaceFile;
           _setupTitle();

        }

        private void ChangeSurfaceFile(object sender, EventArgs e)
        {
            _setupTitle();
        }

        private void _setupTitle()
        {
            if (this.CurrentSurface != null)
            {
                string v_g = string.Empty;
                v_g = this.CurrentSurface.Title;
                if (this.CurrentSurface is ICoreWorkingRecordableSurface)
                {
                    ICoreWorkingRecordableSurface s = (this.CurrentSurface as ICoreWorkingRecordableSurface);
                    if (s.NeedToSave)
                        v_g += CoreConstant.SURFACE_CHANGED_CHAR;
                }
                this.MainForm.Title = DrSSConstants.APP_MAINFORM_SURFACE_TITLE_2.RFormat(DrSSConstants.VERSION_F, v_g);

            }
            else
            {
                this.MainForm.Title = DrSSConstants.APP_MAINFORM_TITLE_1.RFormat(DrSSConstants.VERSION_F);

            }
        }

        /*    void AddFileToRecentFilesList(string fileName)
            {
                var c = new System.Windows.Shell.JumpList();
                System.Windows.Shell.JumpList.AddToRecentCategory(fileName);
               // SHAddToRecentDocs((uint)ShellAddRecentDocs.SHARD_PATHW, fileName);
            }

            /// <summary>
            /// Native call to add the file to windows' recent file list
            /// </summary>
            /// <param name="uFlags">Always use (uint)ShellAddRecentDocs.SHARD_PATHW</param>
            /// <param name="pv">path to file</param>
            [DllImport("shell32.dll")]
            public static extern void SHAddToRecentDocs(UInt32 uFlags,
                [MarshalAs(UnmanagedType.LPWStr)] String pv);

            enum ShellAddRecentDocs
            {
                SHARD_PIDL = 0x00000001,
                SHARD_PATHA = 0x00000002,
                SHARD_PATHW = 0x00000003
            }
            */
        private void DrSStudioWorkbench_FileOpened(object sender, CoreWorkingFileOpenEventArgs e)
        {
            //System.Windows.Shell.
            System.Windows.Shell.JumpList.AddToRecentCategory(e.Path);
          //  AddFileToRecentFilesList(e.Path);
        }

        public override IXCommonDialog CreateCommonDialog(string name)
        {
            Type t = Type.GetType(string.Format("IGK.DrSStudio.WinUI.CommonDialog.{0}", name), false, true );
            if (t!=null)
            {
                return t.Assembly.CreateInstance(t.FullName, true) as IXCommonDialog;
            }
            return null;
        }
        protected override ICoreDialogToolRenderer CreateToolRenderer(ICoreControl target, ICoreWorkingConfigurableObject obj)
        {
            return new DrSStudioDialogToolRenderer(target, obj, this); 
        }
        protected override ICoreWorkbenchLayoutManager CreateLayoutManager()
        {
            return new DrSStudioLayoutManager(this);
        }
        public override void ShowOptionsAddSetting()
        {
            if (ConfigureWorkingObject(CoreWorkingApplicationSetting.Instance,
                "title.OptionsAndSetting".R(),false,
                new Size2i(790,560)) == enuDialogResult.OK)
            {
                CoreWorkingApplicationSetting.Instance.SaveSetting();
            }
        }
        public override enuDialogResult ConfigureWorkingObject(
            ICoreWorkingConfigurableObject item,
            string title, bool allowCancel,
            Size2i defaultSize )
        {
            if ((item != null) && (item.GetConfigType() != enuParamConfigType.NoConfig))
            {
                XDrSStudioConfigureItemPropertyControl v_propControl = new XDrSStudioConfigureItemPropertyControl
                {
                    Name = "Property",
                    AllowOkButton = true,
                    Element = item
                };

                DrSStudioWorkbenchToolRenderer v_render = new DrSStudioWorkbenchToolRenderer(
                    v_propControl.ContentPanel as ICoreControl,
                    item,
                    this);
                v_render.BuildWorkingProperty();
                

                using (ICoreDialogForm frm = CreateNewDialog(v_propControl))
                {
                    frm.Title = title;
                    if (!defaultSize.IsEmpty)
                    {
                        frm.Size = defaultSize;
                    }
                    else if (!v_render.PreferredSize .IsEmpty){
                      frm.Size = v_render.PreferredSize;
                    }

                    if (frm is Form)
                    {
                        Form v_frm = (frm as Form);                        
                        v_frm.Owner = this.MainForm  as Form ;
                        v_frm.SuspendLayout();
                        v_frm.StartPosition = FormStartPosition.CenterParent;                        
                        v_frm.ResumeLayout();
                    }
                    return frm.ShowDialog();
                }
            }
            return enuDialogResult.None;
        }
        public override enuDialogResult ConfigureWorkingObject(ICoreWorkingConfigurableObject item)
        {
            return ConfigureWorkingObject(item,"title.option".R(), false, Size2i.Empty );
        }

        public override IXCoreSaveDialog CreateNewSaveDialog()
        {
            return new DrsStudioSaveDialog();
        }

        public override IXCoreOpenDialog CreateOpenFileDialog()
        {
            return new DrSStudioOpenFileDialog();
        }

        public override IXCoreFontDialog CreateFontDialog()
        {
            return new DrSStudioFontDialog();
        }

        public override IXCoreColorDialog CreateColorDialog()
        {
            return new DrSStudioColorDialog();
        }
        public override IXCoreJobDialog CreateJobDialog()
        {
            return new DrSStudioJobDialog();
        }

        public override IXCoreWaitDialog CreateWaitDialog()
        {
            return new DrSStudioWaitDialog();
        }

        public void InitDocument(CoreXmlWebDocument document)
        {
            document.AddScript(PathUtils.GetPath("%startup%/sdk/Scripts/drs.js"));
            document.AddLink(PathUtils.GetPath("%startup%/sdk/bootstrap/css/bootstrap.min.css"));
            document.AddLink(PathUtils.GetPath("%startup%/sdk/Styles/igk.css"));

            document.Body.AppendScript(PathUtils.GetPath("%startup%/sdk/jquery/jquery.min.js"));
            document.Body.AppendScript(PathUtils.GetPath("%startup%/sdk/bootstrap/js/bootstrap.min.js"));
        }

        public override ICoreWebDialogForm CreateWebBrowserDialog(ICoreWebScriptObject objectForScripting)
        {
            //IWebBrowserControl control = null ; 
            //CoreControlFactory.CreateControl(
            //    typeof(IWebBrowserControl).Name)
            //    as IWebBrowserControl;
            if (CoreControlFactory.CreateControl(typeof(IWebBrowserControl).Name)
                as IWebBrowserControl is IWebBrowserControl control)
            {
                var dialog = new DrSStudioWebBrowserDialog(control);               
                    control.ObjectForScripting = objectForScripting;
                    dialog.Owner = CoreSystem.Instance.MainForm as Form;
                    if (objectForScripting.Document != null) {
                            
                        control.AttachDocument(objectForScripting.Document);
                    }
                return dialog;

            }
            return null;
        }

        public override ICoreWebDialogForm CreateWebBrowserDialog(ICoreWebScriptObject objectForScripting, string document)
        {
            if (objectForScripting == null)
                return null;

            CoreXmlWebDocument doc = CoreXmlWebDocument.CreateICoreDocument();
            doc.InitDocument();
            doc.Body.LoadString(document);

            objectForScripting.Document = doc;

            //IWebBrowserControl control = CoreControlFactory.CreateControl(
            //    typeof(IWebBrowserControl).Name)
            //    as IWebBrowserControl;

            if (CoreControlFactory.CreateControl(
                typeof(IWebBrowserControl).Name)
                as IWebBrowserControl is IWebBrowserControl control)
            {
                var dialog = new DrSStudioWebBrowserDialog(control);                
                control.ObjectForScripting = objectForScripting;
                control.AttachDocument (doc);
                return dialog;

            }
            return null;    
        }
    }
}

