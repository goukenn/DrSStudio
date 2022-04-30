

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ToolPinceauManager.cs
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
file:ToolPinceauManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    using IGK.DrSStudio.Settings;
    [IGK.DrSStudio.CoreTools ("Tool.PinceauToolManager", ImageKey="btn_edit")]
    sealed class ToolPinceauManager : IGK.DrSStudio.Drawing2D .Tools.Core2DDrawingToolBase 
    {
        private ICore2DSymbolElement  m_Element;
        private bool m_MecanismRegister;
        public bool MecanismRegister
        {
            get { return m_MecanismRegister; }
            set
            {
                if (m_MecanismRegister != value)
                {
                    m_MecanismRegister = value;
                }
            }
        }
        public ICore2DSymbolElement  Element
        {
            get { return m_Element; }
            set
            {
                if (m_Element != value)
                {
                    m_Element = value;
                }
            }
        }
        private static ToolPinceauManager sm_instance;
        private ToolPinceauManager()
        {
        }
        public static ToolPinceauManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static ToolPinceauManager()
        {
            sm_instance = new ToolPinceauManager();
        }
        internal new XPinceauToolManager HostedControl {
            get { return base.HostedControl as XPinceauToolManager ; }
            set {
                base.HostedControl = value;
            }
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new XPinceauToolManager ();
            this.HostedControl.CurrentDirectory = PinceauSetting.Instance.CurrentDirectory;
            this.HostedControl.EditPinceauClick += new EventHandler(HostedControl_EditPinceauClick);
            this.HostedControl.SelectedSymbolClick += new EventHandler(HostedControl_SelectedSymbolClick);
            this.HostedControl.ChooseDirClick += new EventHandler(HostedControl_ChooseDirClick);
            this.HostedControl.DirectoryChanged += new EventHandler(HostedControl_DirectoryChanged);
        }
        void HostedControl_DirectoryChanged(object sender, EventArgs e)
        {
            PinceauSetting.Instance.CurrentDirectory = HostedControl.CurrentDirectory;
        }
        void HostedControl_ChooseDirClick(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.HostedControl.CurrentDirectory = fbd.SelectedPath;
                }
            }
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            layer.SelectedElementChanged += new EventHandler(layer_SelectedElementChanged);
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.SelectedElementChanged -= new EventHandler(layer_SelectedElementChanged);
            base.UnRegisterLayerEvent(layer);
        }
        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            //register a symbol item
            if (MecanismRegister  ==false )
            {
                ICore2DDrawingLayer l = sender as ICore2DDrawingLayer;
                if (l.SelectedElements.Count == 1)
                {
                    this.Element = l.SelectedElements[0] as ICore2DSymbolElement;
                }
                else
                {
                    this.Element = null;
                }
            }
        }
        void HostedControl_EditPinceauClick(object sender, EventArgs e)
        {
            if ((this.Element != null)&&(this.Element.SymbolItem !=null))
            {
                ICoreWorkingConfigurableObject c = this.Element.SymbolItem as ICoreWorkingConfigurableObject;
                if (c!=null)
                Workbench.ConfigureWorkingObject(c);
            }
        }
        void HostedControl_SelectedSymbolClick(object sender, EventArgs e)
        {
            if (this.Element != null)
            {
                this.Element.SymbolItem = this.HostedControl.SelectedSymbol;
            }
        }
    }
}

