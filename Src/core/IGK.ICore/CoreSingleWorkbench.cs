using IGK.ICore.Menu;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    [Serializable()]
    public class CoreSingleWorkbench :  
        ICoreWorkbench 
    {
        private ICoreWorkingSurface m_CurrentSurface;

        /// <summary>
        /// .ctr
        /// </summary>
        public CoreSingleWorkbench()
        {

        }
        public ICoreWorkingSurface CurrentSurface
        {
            get { return m_CurrentSurface; }
            set
            {
                if (m_CurrentSurface != value)
                {
                    CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e = new CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> (
                        m_CurrentSurface,  value );
                    m_CurrentSurface = value;
                    OnCurrentSurfaceChanged(e);
                }
            }
        }
       
        public event EventHandler<CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>> CurrentSurfaceChanged;
        public event EventHandler<CoreWorkingElementChangedEventArgs<ICoreWorkbenchEnvironment>> EnvironmentChanged;

        protected virtual void OnEnvironmentChanged(CoreWorkingElementChangedEventArgs<ICoreWorkbenchEnvironment> e) => this.EnvironmentChanged?.Invoke(this, e);

        protected virtual void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            CurrentSurfaceChanged?.Invoke(this, e);
        }

        public ICoreActionRegisterTool ActionRegister
        {
            get { throw new NotImplementedException(); }
        }

        public ICoreWorkbenchLayoutManager LayoutManager
        {
            get { throw new NotImplementedException(); }
        }


        public virtual bool MenuActionMessageFiltering
        {
            get { return false; }
        }

        public ICoreMenuMessageShortcutContainer FilteredAction
        {
            get;
            set;
        }

        public ICoreWorkbenchEnvironment Environment
        {
            get;
            set;
        }

        public virtual ICoreDialogForm CreateNewDialog()
        {
            return null;
        }

        public virtual ICoreDialogForm CreateNewDialog(ICoreControl baseControl)
        {
            return null;
        }

        public virtual IXCoreSaveDialog CreateNewSaveDialog()
        {
            return null;
        }

        public virtual IXCoreOpenDialog CreateOpenFileDialog()
        {
            return null;
        }

        public IXCoreFontDialog CreateFontDialog()
        {
            return null;
        }

        public virtual  IXCoreColorDialog CreateColorDialog()
        {
            return null;
        }

        public virtual IXCoreWaitDialog CreateWaitDialog()
        {
            return null;
        }

        public virtual IXCoreJobDialog CreateJobDialog()
        {
            return null;
        }

        public virtual void ShowAbout()
        {
        }

        public virtual void Open()
        {
            
        }

        public virtual bool CreateNewFile()
        {
            return false;
        }

        public virtual bool CreateNewProject()
        {
            return false;
        }

        public virtual void ShowOptionsAddSetting()
        {
        }

        public enuDialogResult ConfigureWorkingObject(ICoreWorkingConfigurableObject item, string title, bool allowCanel, Size2i defaultSize)
        {
            CoreMessageBox.Show("ConfigureWorkingObject Not Implemenent");
            return enuDialogResult.None;
        }

        public virtual void BuildWorkingProperty(ICoreControl targetControl, WinUI.Configuration.ICoreWorkingConfigurableObject objToConfigure)
        {
          
        }

        public void CallAction(string actionName)
        {
            CoreMessageBox.Show("Not Implemenent");
        }

     

        public enuDialogResult ShowDialog(string title, ICoreControl control)
        {
            CoreMessageBox.Show("Not Implemenent");
            return enuDialogResult.None;
        }

        public void Show(string title, ICoreControl control)
        {
            CoreMessageBox.Show("Not Implemenent");
        }
        /// <summary>
        /// inititialize this worbench with the coreSystem instance
        /// </summary>
        /// <param name="coreSystem"></param>
        public virtual void Init(CoreSystem coreSystem)
        {
            if (coreSystem.Workbench is ICoreWorbenchToolListener m)
            {
                CoreWorkbenchUtility.InitTools(m);// coreSystem.Workbench);
            }
        }

     

        public virtual void SetHelpMessageListener(ICoreWorkbenchHelpMessageListener helpMessageListener)
        {
            
        }

        public virtual T CreateCommonDialog<T>(string name) where T : WinUI.Common.IXCommonDialog
        {
            return default(T);
        }
        public T CreateCommonDialog<T>() where T : IXCommonDialog
        {
            return CreateCommonDialog<T>(typeof(T).Name);
        }


        public virtual void OpenFile(params string[] f)
        {
            throw new NotImplementedException();
        }

        public T FindSurface<T>()
        {
            if (typeof(T) == this.m_CurrentSurface.GetType())
                return (T)this.m_CurrentSurface;
            return default(T);
        }
    }
}
