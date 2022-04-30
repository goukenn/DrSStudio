

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextEditorSurface.cs
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
file:TextEditorSurface.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.RTFTextEditor.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.WinUI;
    using System.Drawing;


    [IGK.DrSStudio.CoreSurface ("RTFTextEditor",
        EnvironmentName = "{C9550F00-ED05-454F-8A76-5716F3BBEC4F}")]
    public class TextEditorSurface :
        IGKXWinCoreWorkingSurface,
        ICoreWorkingFilemanagerSurface ,
        ICoreWorkingConfigElementSurface,
        ICoreWorkingColorSurface ,
        ICoreWorkingDualBrushSelectorSurface
    {
        private RichTextBox m_rtfBox;
        private string m_fileName;
        private bool m_needToSave;
        private RTFElement m_elementToconfigure;
        public ICoreBrush[] GetBrushes()
        {
            return null;
        }
        public virtual void RenameTo(string newfilename)
        {
            if (string.IsNullOrEmpty(newfilename))
                return;
            string p = this.FileName;
            System.IO.File.Delete(p);
            this.FileName = newfilename;
            this.Save();
        }
        public TextEditorSurface()
        {
            m_elementToconfigure = new RTFElement();
            m_rtfBox = new RichTextBox();
            m_rtfBox.Dock = DockStyle.Fill;
            m_rtfBox.ShowSelectionMargin= true;
            m_rtfBox.ShortcutsEnabled = true;
            m_rtfBox.HideSelection = false;
            this.Controls.Add(m_rtfBox);
            this.m_elementToconfigure.Font.FontDefinitionChanged += new EventHandler(Font_FontDefinitionChanged);
            this.m_elementToconfigure.FillBrush.SetSolidColor(Colorf.White);
            this.m_elementToconfigure.StrokeBrush .SetSolidColor(Colorf.Black );
            this.m_elementToconfigure.FillBrush.BrushDefinitionChanged += new EventHandler(FillBrush_BrushDefinitionChanged);
            this.m_elementToconfigure.StrokeBrush.BrushDefinitionChanged += new EventHandler(StrokeBrush_BrushDefinitionChanged);
            this.FileName = System.IO.Path.GetFullPath(TextEditorConstant.EMPTY_FILE);
            UpdateFont();
        }
      
        void StrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.m_rtfBox.SelectionColor = this.m_elementToconfigure.StrokeBrush.Colors[0].CoreConvertTo<Color>();
        }
        void FillBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.m_rtfBox.SelectionBackColor  = this.m_elementToconfigure.FillBrush.Colors[0].CoreConvertTo<Color>();
        }
        void Font_FontDefinitionChanged(object sender, EventArgs e)
        {
            UpdateFont();
        }
        /// <summary>
        /// update the surface font
        /// </summary>
        private void UpdateFont()
        {
            if (this.m_rtfBox.SelectionLength > 0)
            {
                this.m_rtfBox.SelectionFont = m_elementToconfigure.Font.ToGdiFont();
            }
            else {
                this.m_rtfBox.SelectionFont = m_elementToconfigure.Font.ToGdiFont ();
            }
        }
     
        #region ICoreWorkingFilemanagerSurface Members
        public string FileName
        {
            get {
                return this.m_fileName;
            }
             set {
                 if (this.m_fileName != value)
                 {
                     this.m_fileName = value;
                     OnFileNameChanged(EventArgs.Empty);
                 }
            }
        }
        public event EventHandler FileNameChanged;
        protected virtual void OnFileNameChanged(EventArgs e)
        {
            if (this.FileNameChanged != null)
            {
                this.FileNameChanged(this, e);
            }
        }
        #endregion
        #region ICoreWorkingRecordableSurface Members
        public bool NeedToSave
        {
            get { return this.m_needToSave ; }
            protected set {
                if (this.m_needToSave != value)
                {
                    this.m_needToSave = value;
                    OnNeedToSaveChanged(EventArgs.Empty);
                }
            }
        }
        private void OnNeedToSaveChanged(EventArgs eventArgs)
        {
            if (this.NeedToSaveChanged != null)
                this.NeedToSaveChanged(this, eventArgs);
        }
        public event EventHandler NeedToSaveChanged;
        public void Save()
        {
            if (System.IO.File.Exists(this.FileName))
            {
                try
                {
                    this.m_rtfBox.SaveFile(this.FileName);
                    this.NeedToSave = true;
                }
                catch (Exception ex){
                    CoreMessageBox.Show(ex);
                }
            }
            else {
                this.SaveAs(this.FileName );
            }
        }
        public void SaveAs(string filename)
        {
            this.m_rtfBox.SaveFile(filename);
            this.FileName = filename;
        }
        #endregion
        internal bool LoadFile(string filename)
        {
            this.m_rtfBox.LoadFile(filename);
            return true;
        }
        #region ICoreWorkingConfigElementSurface Members
        public ICoreWorkingObject ElementToConfigure
        {
            get
            {
                return this.m_elementToconfigure;
            }
            set
            {
                //not implement
            }
        }
        public event EventHandler ElementToConfigureChanged;
        protected virtual void OnElementToConfigureChanged(EventArgs e)
        {
            if (this.ElementToConfigureChanged != null)
            {
                this.ElementToConfigureChanged(this, e);
            }
        }
        #endregion
        #region ICoreWorkingColorSurface Members
        Colorf m_CurrentColor;
        public Colorf CurrentColor
        {
            get
            {
                return this.m_CurrentColor;
            }
            set
            {
                if (!this.m_CurrentColor.Equals(value))
                {
                    this.m_CurrentColor = value;
                    this.OnCurrentColorChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// current color changed
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual  void OnCurrentColorChanged(EventArgs eventArgs)
        {
            if (this.CurrentColorChanged != null)
            {
                this.CurrentColorChanged(this, eventArgs);
            }
        }
        public event EventHandler CurrentColorChanged;
        #endregion
        #region ICoreWorkingDualBrushSelectorSurface Members
        enuBrushMode m_BrushMode;
        public enuBrushMode BrushMode
        {
            get
            {
                return this.m_BrushMode ;
            }
            set
            {
                if (this.m_BrushMode != value)
                {
                    this.m_BrushMode = value;
                    OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
        private void OnPropertyChanged(EventArgs eventArgs)
        {
            if (this.BrushModeChanged != null)
                this.BrushModeChanged(this, eventArgs);
        }
        public event EventHandler BrushModeChanged;
        #endregion
        #region ICore2DDrawingDualBrushElement Members
        public IGK.DrSStudio.Drawing2D.ICoreBrush GetBrush(enuBrushMode mode)
        {
            return this.m_elementToconfigure.GetBrush(mode);
        }
        #endregion
        #region ICore2DDrawingObject Members
        public new IGK.DrSStudio.Drawing2D.ICore2DDrawingObject Parent
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region ICore2DDualBrushObject Members
        public IGK.DrSStudio.Drawing2D.ICoreBrush FillBrush
        {
            get {
                return this.m_elementToconfigure.FillBrush;
            }
        }
        public IGK.DrSStudio.Drawing2D.ICorePen StrokeBrush
        {
            get {
                return this.m_elementToconfigure.StrokeBrush as IGK.DrSStudio.Drawing2D.ICorePen ;
            }
        }
        #endregion
        #region ICore2DDrawingBrushSupportElement Members
        public IGK.DrSStudio.Drawing2D.enuBrushSupport BrushSupport
        {
            get {
                return IGK.DrSStudio.Drawing2D.enuBrushSupport.All;
            }
        }
        #endregion
        public string GetDefaultFilter()
        {
            return "rtf  | *.rtf";
        }
        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("Save RTF Text",
                GetDefaultFilter(),
                this.FileName);
        }


        public Matrix GetMatrix()
        {
            throw new NotImplementedException();
        }

        public CoreGraphicsPath GetPath()
        {
            throw new NotImplementedException();
        }
    }
}

