

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXToolStrip.cs
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
file:IGKXToolStrip.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using IGK.ICore;
using IGK.ICore.Settings;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    /// <summary>
    /// represent the base tools strip
    /// </summary>
    public class IGKXToolStrip : ToolStrip
    {
        private string m_captionkey;
        protected ToolStripItem m_addorRemoveButton;
        public event EventHandler BackgroundDocumentChanged;
     
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        /// <summary>
        /// get the ToolStrip Add Or Remove Button
        /// </summary>
        protected ToolStripItem AddOrRemoveButton
        {
            get
            {
                return m_addorRemoveButton;
            }
        }
        public string CaptionKey
        {
            get
            {
                return this.m_captionkey;
            }
            set
            {
                m_captionkey = value;
                OnCaptionKeyChanged(EventArgs.Empty);
            }
        }
        public event EventHandler CaptionKeyChanged;
        protected virtual void OnCaptionKeyChanged(EventArgs e)
        {
            this.LoadDisplayText();
            if (CaptionKeyChanged != null)
                this.CaptionKeyChanged(this, e);
        }
        private ICore2DDrawingDocument _backgroundDocument;
        [DesignOnly(false)]
        [Browsable (false)]
        public ICore2DDrawingDocument BackgroundDocument
        {
            get
            {
                return _backgroundDocument;
            }
            set
            {
                if (_backgroundDocument != null)
                {
                    _backgroundDocument = value;
                    OnBackgroundDocumentChanged(EventArgs.Empty);
                }
            }
        }
        protected virtual void OnBackgroundDocumentChanged(EventArgs e)
        {
            this.Invalidate();
            if (this.BackgroundDocumentChanged != null)
                this.BackgroundDocumentChanged(this, e);
        }
        public virtual void LoadDisplayText()
        {
            this.Text = CoreResources.GetString(CaptionKey);
            if (this.m_addorRemoveButton != null)
            {
                this.m_addorRemoveButton.Text = CoreResources.GetString(CoreConstant.TSBTN_ADDORREMOVE_CAPTION);
            }
            foreach (var item in this.Items)
            {
                if (item is IGKXToolStripButton)
                {
                    (item as IGKXToolStripButton).LoadDisplayText();
                }
            }
        }
        //ctr
        public IGKXToolStrip()
        {
            this.Renderer = new XToolStripRenderer();
            CoreApplicationSetting.Instance.LangReloaded += Instance_LangReloaded;
        }

        void Instance_LangReloaded(object sender, EventArgs e)
        {
            this.LoadDisplayText();
        }
        public void AddRemoveButton(EventHandler e)
        {
            CreateAddOrRemoveButton(e);
        }
        protected void CreateAddOrRemoveButton(EventHandler clickhandler)
        {
            if (this.AddOrRemoveButton != null)
                return;
            ToolStripDropDownButton btn = null;
            btn = new ToolStripDropDownButton(CoreResources.GetString(CoreConstant.TSBTN_ADDORREMOVE_CAPTION));
            btn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btn.Overflow = ToolStripItemOverflow.Always;
            if (clickhandler != null)
                btn.Click += clickhandler;
            m_addorRemoveButton = btn;
            this.Items.Add(btn);
        }
        /// <summary>
        /// used this to enable or activate element
        /// </summary>
        /// <param name="g"></param>
        public virtual void Enable(bool v)
        {
            this.Enabled = true;
            foreach (ToolStripItem item in this.Items)
            {
                if (item == this.AddOrRemoveButton)
                    continue;
                item.Enabled = v;
            }
        }
        [Browsable(false)]
        /// <summary>
        /// get the application mainform
        /// </summary>
        public ICoreMainForm MainForm
        {
            get
            {
                return CoreSystem.GetMainForm();
            }
        }
        [Browsable(false)]
        public ICoreWorkbench WorkBench
        {
            get { return CoreSystem.GetWorkbench(); }
        }
    }
}

