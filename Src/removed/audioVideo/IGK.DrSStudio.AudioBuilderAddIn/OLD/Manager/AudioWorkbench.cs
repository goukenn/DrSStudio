

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioWorkbench.cs
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
file:AudioWorkbench.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.AudioBuilder
{
    using IGK.ICore;using IGK.DrSStudio.AudioBuilder.WinUI ;
    using IGK.DrSStudio.WinUI.Configuration;
    using IGK.DrSStudio.WinUI;
    /// <summary>
    /// represent the default workbench
    /// </summary>
    sealed class AudioWorkbench :CoreWorkbenchBase 
    {
        public new AudioForm MainForm {
            get {
                return base.MainForm as AudioForm;
            }
        }
        public override IGK.DrSStudio.WinUI.ICoreDialogForm CreateNewDialog()
        {
            return base.CreateNewDialog();
        }
        public override void CreateNewProject()
        {
        }
        protected override ICoreWorkingSurfaceCollections CreateSurfaceCollection()
        {
            return new SurfaceCollections(this);
        }
        sealed class SurfaceCollections : ICoreWorkingSurfaceCollections 
        {
            AudioWorkbench m_wb;
            public SurfaceCollections(AudioWorkbench t)
            {
                m_wb = t; 
            }
            public bool CanAdd { get { return false; } }
            public bool CanRemove { get { return false; } }
            #region ICoreWorkingSurfaceCollections Members
            public IGK.DrSStudio.WinUI.ICoreWorkingSurface this[int index]
            {
                get { return this.m_wb.MainForm.CurrentSurface; }
            }
            public int Count
            {
                get { return 1; }
            }
            public void Add(IGK.DrSStudio.WinUI.ICoreWorkingSurface surface)
            {
                throw new NotImplementedException();
            }
            public void Remove(IGK.DrSStudio.WinUI.ICoreWorkingSurface surface)
            {
                throw new NotImplementedException();
            }
            public IGK.DrSStudio.WinUI.ICoreWorkingSurface[] ToArray()
            {
                IGK.DrSStudio.WinUI.ICoreWorkingSurface[] t =
                   new IGK.DrSStudio.WinUI.ICoreWorkingSurface[]{
                        this.m_wb.MainForm.CurrentSurface
                    };
                return t;
            }
            public bool Contains(IGK.DrSStudio.WinUI.ICoreWorkingSurface surface)
            {
                return this.m_wb.MainForm.CurrentSurface == surface;
            }
            public int IndexOf(IGK.DrSStudio.WinUI.ICoreWorkingSurface surface)
            {
                if (this.m_wb.MainForm.CurrentSurface == surface)
                    return 0;
                return -1;
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                IGK.DrSStudio.WinUI.ICoreWorkingSurface[] t = 
                    new IGK.DrSStudio.WinUI.ICoreWorkingSurface[]{
                        this.m_wb.MainForm.CurrentSurface
                    };
                return t.GetEnumerator();
            }
            #endregion
            public DrSStudio.WinUI.ICoreWorkingSurface this[string key]
            {
                get { throw new NotImplementedException(); }
            }
        }
        public override void Open()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Audio Files | *.mp3;*.wav";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.MainForm.CurrentSurface.OpenFile (ofd.FileName);
                }
            }
        }
        public override void OpenFile(string[] files)
        {
            string v_ext = string.Empty;
            foreach (string item in files)
            {
                v_ext = System.IO.Path.GetExtension(item);
                this.MainForm.CurrentSurface.OpenFile (item);
            }
        }
        public override DialogResult ConfigureWorkingObject(ICoreWorkingConfigurableObject item, bool AllowCancel)
        {
            return base.ConfigureWorkingObject(item, AllowCancel);
        }
        public AudioWorkbench(AudioForm frm):base(frm)
        {
        }
        protected override IGK.DrSStudio.WinUI.ICoreLayoutManager CreateLayoutManager()
        {
            return new AudioLayoutManager(this);
        }
        /// <summary>
        /// represent the layout manager
        /// </summary>
        internal  class AudioLayoutManager :
            IGK.DrSStudio.WinUI.ICoreLayoutManager 
        {
            AudioWorkbench m_worbench;
            public AudioLayoutManager(AudioWorkbench bench)
            {
                this.m_worbench = bench;
            }
            #region ICoreLayoutManager Members
            public IGK.DrSStudio.WinUI.ICoreMainForm MainForm
            {
                get  {return  this.m_worbench.MainForm; }
            }
            public string Environment
            {
                get
                {
                    return AudioConstant.ENVIRONMENT;
                }
                set
                {
                }
            }
            public event EventHandler EnvironmentChanged;
            protected virtual void OnEnvironmentChanged(EventArgs e)
            {
                if (this.EnvironmentChanged != null)
                {
                    this.EnvironmentChanged(this, e);
                }
            }
            public IGK.DrSStudio.WinUI.ICoreWorkbench Workbench
            {
                get { return this.m_worbench; }
            }
            public void InitLayout()
            {
                //MenuStrip s = new MenuStrip ();
                //CoreSystem.Instance.Menus.GenerateMenu(s, this.Workbench);
                //this.MainForm.Controls.Add(s);
                //this.MainForm.MainMenuStrip = s;
            }
            public void ShowTool(IGK.DrSStudio.Tools.ICoreTool tool)
            {
            }
            public void HideTool(IGK.DrSStudio.Tools.ICoreTool tool)
            {
            }
            public System.Windows.Forms.StatusStrip StatusStrip
            {
                get { return null; }
            }
            public System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem()
            {
                return new System.Windows.Forms.ToolStripMenuItem();
            }
            public event IGK.DrSStudio.WinUI.CoreToolEventHandler ToolAdded;
            public event IGK.DrSStudio.WinUI.CoreToolEventHandler ToolRemoved;
            #endregion
            protected virtual void OnToolAdded(IGK.DrSStudio.WinUI.CoreToolEventArgs e)
            {
                if (this.ToolAdded != null)
                {
                    this.ToolAdded(this, e);
                }
            }
            protected virtual void OnToolRemoved(IGK.DrSStudio.WinUI.CoreToolEventArgs e)
            {
                if (this.ToolRemoved != null)
                {
                    this.ToolRemoved(this, e);
                }
            }
            #region IDisposable Members
            public void Dispose()
            {
                throw new NotImplementedException();
            }
            #endregion
            public void Refresh()
            {
            }
        }
        protected override ICoreDialogToolRenderer CreateToolRenderer(Control target, ICoreWorkingConfigurableObject obj)
        {
            return null;
        }
    }
}

