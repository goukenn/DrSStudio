

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XWindowsPictureEditor.cs
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
file:XWindowsPictureEditor.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    [IGK.DrSStudio.CoreSurface ("GDIPictureEditorSurface")]
    /// <summary>
    /// pictures . 
    /// </summary>
    public class XWindowsPictureEditor : 
        IGK.DrSStudio.WinUI.XControl ,
        IGK.DrSStudio.WinUI.ICoreWorkingSurface ,
        IGK.DrSStudio.WinUI.ICoreWorkingConfigElementSurface ,
        IGK.DrSStudio.WinUI.ICoreWorkingUndoableSurface, 
        IGK.DrSStudio.WinUI.ICoreWorkingPrintingSurface ,
        IGK.DrSStudio.WinUI.ICoreWorkingFilemanagerSurface 
    {
        ViewImageSurface c_previewSurface;
        ViewImageSurface c_nextSurface;
        IGK.DrSStudio.History.IHistoryList m_historyList;
        IGK.DrSStudio.Drawing2D.History.SingleImageHistoryManager m_SImageManager;
        public XWindowsPictureEditor()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            c_nextSurface = new ViewImageSurface();
            c_previewSurface = new ViewImageSurface();
            c_previewSurface.Dock = System.Windows.Forms.DockStyle.None;
            c_previewSurface.RenderSelection = false;
            c_nextSurface.Dock = System.Windows.Forms.DockStyle.None;
            c_nextSurface.RenderSelection = false;
            c_nextSurface.CurrentTool = typeof(SelectMecanismElement);
            c_nextSurface.ShowScroll = false;
            c_nextSurface.CurrentDocument.BackgroundTransparent = true;
            c_previewSurface.ShowScroll = false;
            c_previewSurface.CurrentTool = typeof(SelectMecanismElement);
            c_previewSurface.CurrentDocument.BackgroundTransparent = true;
            this.SizeChanged += new EventHandler(_SizeChanged);
            this.FileNameChanged += new EventHandler(_FileNameChanged);
            this.Controls.Add(c_previewSurface);
            this.Controls.Add(c_nextSurface);
            this.m_historyList = IGK.DrSStudio.History.CoreHistoryManager.Register(this);
            this.InitBound();
        }
        void _FileNameChanged(object sender, EventArgs e)
        {
            System.Drawing.Bitmap bmp = null;
            try
            {
                if (this.m_SImageManager != null)
                {
                    this.m_SImageManager.Dispose();
                }
                bmp = new Bitmap(this.FileName);
                ImageElement img1 = ImageElement.FromImage(bmp);
                ImageElement img2 = ImageElement.FromImage(bmp);
                bmp.Dispose();
                this.c_previewSurface.CurrentLayer.Elements.Add(img1);
                this.c_previewSurface.CurrentDocument.SetSize(img1.Width, img1.Height);
                this.c_nextSurface .CurrentLayer.Elements.Add(img2);
                this.c_nextSurface.CurrentDocument.SetSize(img1.Width, img1.Height);
                this.c_nextSurface.ElementToConfigure = img2;
                this.m_SImageManager = new IGK.DrSStudio.Drawing2D.History.SingleImageHistoryManager(
                this.m_historyList,
                img2 ,
                this);
            }
            catch { 
            }
        }
        void _SizeChanged(object sender, EventArgs e)
        {
            this.InitBound();   
        }
        void InitBound()
        { 
            int w = (int)Math.Ceiling (this.Width / 2.0);
            this.c_previewSurface .Bounds = new Rectangle(0,0, w, this.Height);
            this.c_nextSurface. Bounds = new Rectangle(w, 0, w, this.Height);
        }
        #region ICoreWorkingSurface Members
        public string DisplayName
        {
            get {
                return PicEditorConstant.PICEDITOR_TITLE;
            }
        }
        public string SurfaceEnvironment
        {
            get {
                return PicEditorConstant.GDIENVIRONMENT;
            }
        }
        public ICoreWorkbench Workbench
        {
            get {
                if (CoreSystem.Instance != null)
                    return CoreSystem.Instance.Workbench;
                return null;
            }
        }
        #endregion
        #region ICoreWorkingObjectPropertyEvent Members
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        #endregion
        void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, e);
        }
        private string m_FileName;
        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                    OnFileNameChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler FileNameChanged;
        private void OnFileNameChanged(EventArgs eventArgs)
        {
            if (this.FileNameChanged != null)
            {
                this.FileNameChanged(this, eventArgs);
            }
        }
        #region ICoreWorkingConfigElementSurface Members
        public ICoreWorkingObject ElementToConfigure
        {
            get
            {
                return this.c_nextSurface.ElementToConfigure;
            }
            set
            {
                this.c_nextSurface.ElementToConfigure = value;
            }
        }
        public event EventHandler ElementToConfigureChanged {
            add { this.c_nextSurface.ElementToConfigureChanged += value; }
            remove { this.c_nextSurface.ElementToConfigureChanged -= value; }
        }
        #endregion
        #region ICoreWorkingUndoableSurface Members
        public bool CanRedo
        {
            get { return this.m_historyList.CanRedo; }
        }
        public bool CanUndo
        {
            get { return this.m_historyList.CanUndo; }
        }
        public void Redo()
        {
            this.m_historyList.Redo();
        }
        public void Undo()
        {
            this.m_historyList.Undo();
        }
        #endregion
        #region ICoreWorkingPrintingSurface Members
        public void Print()
        {
            this.c_nextSurface.Print();
        }
        public void PrintPreview()
        {
            this.c_nextSurface.PrintPreview();
        }
        #endregion
        #region ICoreWorkingFilemanagerSurface Members
        public void RenameTo(string p)
        {
            this.c_nextSurface.RenameTo(p);
        }
        #endregion
        #region ICoreWorkingRecordableSurface Members
        public bool NeedToSave
        {
            get { return this.c_nextSurface .NeedToSave ; }
        }
        public event EventHandler NeedToSaveChanged {
            add { this.c_nextSurface.NeedToSaveChanged += value; }
            remove { this.c_nextSurface.NeedToSaveChanged -= value; }
        }
        public void Save()
        {
            this.c_nextSurface.Save();
        }
        public void SaveAs()
        {
            this.c_nextSurface.SaveAs();
        }
        #endregion
        public string GetDefaultFilter()
        {
            return "pictures | *.bmp; *.jpg";
        }
    }
}

