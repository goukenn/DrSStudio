

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreWorkbenchBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WinCoreWorkbenchBase.cs
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
using IGK.ICore.WinCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    using ICore.Drawing2D;
    using IGK.DrSStudio.Tools;
    using IGK.ICore;
    using IGK.ICore.Codec;
    using IGK.ICore.IO;
    using IGK.ICore.Menu;
    using IGK.ICore.Resources;
    using IGK.ICore.Tools;
    using IGK.ICore.Tools.ActionRegister;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    [Serializable()]
    /// <summary>
    /// Represent the workbench
    /// </summary>
    public abstract  class WinCoreWorkbenchBase : 
        CoreWorkbenchBase, 
        ICoreWorkbench, 
        ICoreApplicationWorkbench ,
        ICoreActionRegisterWorkbench,
        ICoreLayoutManagerWorkbench,
        ICoreWorbenchMultisurface
    {     
        //PRIVATE MEMBERS 
        private ICoreMenuMessageShortcutContainer m_FilteredAction;
        private Dictionary<ICoreMessageFilter, WinCoreMessageFilter> m_hostFilterMessage;

        public WinCoreWorkbenchBase(ICoreMainForm mainForm):base(mainForm)
        {
        }
        public override ICoreMenuMessageShortcutContainer FilteredAction
        {
            get
            {
                return m_FilteredAction;
            }
            set
            {
                this.m_FilteredAction = value;
            }
        }
        public  bool IsFiltering {
            get {
                if (this.m_FilteredAction == null)
                    return false;
                return this.m_FilteredAction.IsFiltering;
            }
        }
        #region ICoreWorkbench Members
        /// <summary>
        /// create a layout manager
        /// </summary>
        /// <returns></returns>
        protected override  ICoreWorkbenchLayoutManager CreateLayoutManager()
        {
            return new WinCoreLayoutManagerBase(this);
        }
        public override  void BuildWorkingProperty(
                                        ICoreControl target,
                                        ICoreWorkingConfigurableObject obj
                                )
        {
           ICoreDialogToolRenderer c =  CreateToolRenderer(target, obj);
            if ((c==null) ||  !c.BuildWorkingProperty())
            {
                this.ConfigureWorkingObject (obj);
            }
        }
        protected abstract ICoreDialogToolRenderer CreateToolRenderer(ICoreControl target, ICoreWorkingConfigurableObject obj);
       
        
        public override  void OpenFile(params string[] files)
        {
            
            for (int i = 0; i < files.Length; i++)
            {
                if (string.IsNullOrEmpty(files[i]))
                    continue;
                ICoreCodec[] deco = CoreSystem.GetDecodersByExtension(
                    System.IO.Path.GetExtension(files[i]).Replace(".", ""));
                try
                {
                    switch (deco.Length)
                    {
                        case 1:
                            if ((deco[0] as ICoreDecoder).Open(this, files[i] , files.Length == 1))
                            {
                                OnFileOpened(new CoreWorkingFileOpenEventArgs(files[i], files[i]));
                            }
                            else {
                                CoreMessageBox.Show(CoreSystem.GetString ("ERROR.FILECANTBEOPEN", files[i]));
                            }
                            break;
                        case 0:
                            continue;
                        default:
                            CoreChooseACodecTool c = new CoreChooseACodecTool(deco);
                            if (ConfigureWorkingObject(c) == enuDialogResult.OK)
                            {
                                    if ( (c.Codecs == null) || !(c.Codecs as ICoreDecoder).Open(
                                        this, files[i], files.Length == 1))
                                    {
                                        CoreMessageBox.NotifyMessage("title.Error".R(),
                                            "e.fileNotOpen".R());
                                    }                             
                            }
                            break;
                    }
                }
                catch
#if DEBUG
                    (Exception ex)
#endif
                {
#if DEBUG 
                    CoreLog.WriteDebug("Can't open " + files[i] +"\n"+ex.Message );
#endif
                }
            }
        }
        #endregion
       
        #region ICoreWorkbench Members
        public override void ShowAbout()
        {
            using (ICoreDialogForm  dialog = this.CreateNewDialog ())
            {
                
                dialog.Title = "title.About".R();
                dialog.BackgroundDocument =
#if DEBUG
                    CoreResources.LoadDocument<ICore2DDrawingDocument>(CoreConstant.DRS_SRC+ @"\main\IGK.DrSStudio.Resources\Resources\igkdev_about.gkds")
                    ?? CoreResources.GetDocument("igkdev_about");
#else
                CoreResources.GetDocument("igkdev_about");
#endif
                if (dialog.BackgroundDocument != null)
                {
                    dialog.BackgroundDocument.BackgroundTransparent = false;
                }
                dialog.Owner = this.MainForm;
                dialog.StartPosition = enuFormStartPosition.CenterParent;
                if (dialog.BackgroundDocument != null)
                {
                    ICoreTextElement txt = dialog.BackgroundDocument.GetElementById<ICoreTextElement>("version");//as ICoreTextElement;
                    if (txt != null)
                    {
                        txt.Text = CoreConstant.VERSION;
                    }
                    dialog.Size = new Size2i(
                          dialog.BackgroundDocument.Width,
                          dialog.BackgroundDocument.Height);
                }
                dialog.ShowDialog();
            }
        }
        public override   ICoreDialogForm CreateNewDialog()
        {
            XFormDialog c = new XFormDialog();
            c.Owner = this.MainForm as Form ;
            c.StartPosition = FormStartPosition.CenterParent;
            c.AutoSize = true;
            c.AutoSizeMode = AutoSizeMode.GrowOnly;
            return c;
        }
        public override ICoreDialogForm CreateNewDialog(ICoreControl baseControl)
        {
            if (baseControl == null)
                return null;
            ICoreDialogForm form = CreateNewDialog();
            form.SuspendLayout();          
            Control ctr = baseControl as Control;
            form.Controls.Add(ctr);
            form.Icon = this.MainForm.Icon;            
            form.ResumeLayout();

           form.Size = new Size2i(baseControl.Size.Width, baseControl.Size.Height);
            Control c = baseControl as Control;
            if (c!=null)
                c.Anchor =  (AnchorStyles)15;
            form.AutoSize = false;
            return form;
        }
#endregion
     
        public override  void Open()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                StringBuilder sb = new StringBuilder ();                
                sb.Append(CoreDecoderBase.GetAllFilters());
                ofd.Filter = sb.ToString();
                ICoreWorkingFilemanagerSurface c = this.CurrentSurface as
                ICoreWorkingFilemanagerSurface;
                if (c != null)
                    ofd.InitialDirectory = PathUtils.GetDirectoryName(c.FileName);
                else
                {
                    //let system determine the last opened folder
                    
                 //   ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                }
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    MainForm.SetCursor(Cursors.WaitCursor);
                    OpenFile(ofd.FileNames);
                    MainForm.SetCursor(Cursors.Default);
                }
            }
        }
        /// <summary>
        /// create an empty manager project
        /// </summary>
        public override  bool CreateNewProject()
        {
            UIXProjectSelectorGUI v_selectProject = new UIXProjectSelectorGUI();
            using (ICoreDialogForm dial = CreateNewDialog(v_selectProject))
            {
                dial.Title = "title.NewProject".R();
                dial.Owner = MainForm;
                dial.AutoSize = false;
                dial.Size = new Size2i(v_selectProject.Width, v_selectProject.Height);
                v_selectProject.Anchor = WinCoreStyles.AnchorAll;
                if (dial.ShowDialog() == enuDialogResult.OK )
                {
                    return true;
                }
            }
            return false;
        }
        public override bool CreateNewFile()
        {
            ICoreWorkingSurface surface = CoreSystem.CreateWorkingObject(CoreConstant.DRAWING2D_SURFACE_TYPE) as ICoreWorkingSurface;
            if (surface != null)
            {
                this.Surfaces.Add(surface);
                this.CurrentSurface = surface;
                return true;
            }
            else
            {
                CoreMessageBox.Show(R.ngets("ERR.SURFACE_NOTFOUND", CoreConstant.DRAWING2D_SURFACE_TYPE));
            }
            return false;
        }
#region ICoreWorkbench Members
        /// <summary>
        /// project file
        /// </summary>
        /// <param name="filename">project filename</param>
        public override void OpenProject(string filename)
        {
            this.OnProjectOpened(new CoreProjectOpenedEventArgs(
                System.IO.Path.GetFileName(filename),
                System.IO.Path.GetFullPath(filename)));
        }
        public override bool IsFileOpened(string filename)
        {
            foreach (ICoreWorkingSurface  s in this.Surfaces)
            {
                ICoreWorkingFilemanagerSurface h = s as ICoreWorkingFilemanagerSurface;
                if (h == null)
                    continue;
                if (h.FileName == filename)
                    return true;
            }
            return false;
        }

        public override bool IsSolutionOpened(string filename)
        {
            foreach (ICoreWorkingSurface s in this.Surfaces)
            {
                ICoreWorkingProjectSolutionSurface h = s as ICoreWorkingProjectSolutionSurface;
                if (h == null)
                    continue;
                if (h.Solution .FileName == filename)
                    return true;
            }
            return false;
        }
        public virtual enuDialogResult  ShowDialog(string title, UserControl control)
        {
            if (control == null)
                return enuDialogResult.Ignore ;
            using (ICoreDialogForm frm = this.CreateNewDialog())
            {
                frm.Title = title;
                frm.Controls.Add(control);
                frm.Size = control.Size.CoreConvertFrom<Size2i>();
                return frm.ShowDialog();
            }
        }
        public virtual void Show(string title, UserControl control)
        {
            if (control == null)
                return;
            Form frm = new Form();
            frm.Text = title;
            frm.Controls.Add(control);
            frm.Size = control.Size;
            frm.Owner   = this.MainForm as Form ;
            frm.Show();
        }
#endregion
        /// <summary>
        /// get the workbench tool renderer
        /// </summary>
        public  abstract class CoreWorkbenchToolRendererBase : ICoreDialogToolRenderer
        {
            WinCoreWorkbenchBase m_workbench;
            public abstract Control Target { get; }
            public abstract ICoreWorkingConfigurableObject Object { get; }
            public CoreWorkbenchToolRendererBase(WinCoreWorkbenchBase workbench)
            {
                this.m_workbench = workbench;
            }
            /// <summary>
            /// override this method to render target item
            /// </summary>
            /// <param name="target"></param>
            /// <param name="obj"></param>
            /// <returns></returns>
            public abstract  bool BuildWorkingProperty();
            public abstract void Reload();
            public abstract void Reset();
            ICoreControl ICoreDialogToolRenderer.Target
            {
                get { throw new NotImplementedException(); }
            }
        }
        public ICoreMessageBox CoreMessageBox
        {
            get { return IGK.ICore.WinUI.CoreMessageBox.GetInstance(); }
        }
        enuDialogResult ICoreWorkbenchWorkingObjectConfigurator.ConfigureWorkingObject(ICoreWorkingConfigurableObject item,
            string title, bool allowCancel, Size2i defaultsize)
        {
            return ConfigureWorkingObject(item, title, allowCancel, defaultsize);
        }
        public override enuDialogResult ShowDialog(string title, ICoreControl control)
        {
            using (var i = CreateNewDialog(control))
            {
                i.Title = title;
                i.Owner = this.MainForm;
                return i.ShowDialog();
            }
        }
        public override void Show(string title, ICoreControl control)
        {
            var i = CreateNewDialog(control);
            
                i.Title = title;
                i.Owner = this.MainForm;
                i.Show();
            
        }

        public override void RegisterMessageFilter(ICoreMessageFilter messageFilter)
        {
            if (messageFilter == null) return;
            if (m_hostFilterMessage == null)
                m_hostFilterMessage = new Dictionary<ICoreMessageFilter, WinCoreMessageFilter>();
            if (!this.m_hostFilterMessage.ContainsKey(messageFilter))
            {
                WinCoreMessageFilter c = new WinCoreMessageFilter(messageFilter);
                m_hostFilterMessage.Add(messageFilter, c);
                //add message filter to application
                if (CoreApplicationManager.Application is ICoreMessageFilterApplication filter)
                    filter.AddMessageFilter(c);
            }
        }
        public override void UnregisterMessageFilter(ICoreMessageFilter messageFilter)
        {
            if (this.m_hostFilterMessage.ContainsKey(messageFilter))
            {
                WinCoreMessageFilter c = this.m_hostFilterMessage[messageFilter];

                if (CoreApplicationManager.Application is ICoreMessageFilterApplication filter)
                    filter.RemoveMessageFilter(c);

                //Application.RemoveMessageFilter(c);
            }
        }
        /// <summary>
        /// override this to show application option and settings
        /// </summary>
        public override void ShowOptionsAddSetting()
        {           
        }
        /// <summary>
        /// override this property to get custom action registered
        /// </summary>
        public override ICoreActionRegisterTool ActionRegister
        {
            get { return null; }
        }

        public IXCoreMenuItemContainer CreateMenuItem()
        {
            if (this.LayoutManager !=null)
                return this.LayoutManager.CreateMenuItem();
            return null;
        }
        public IXCoreContextMenuItemContainer CreateContextMenuItem()
        {
            if (this.LayoutManager != null)
                return this.LayoutManager.CreateContextMenuItem();
            return null;
        }

        public void DispatchMessage(ICoreMessage message)
        {

            foreach (var item in this.m_hostFilterMessage)
            {
                if (item.Value.PreFilterMessage (ref message))
                    return;
            }
           
        }
    }
}

