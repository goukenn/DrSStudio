

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SymbolManager.cs
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
file:SymbolManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore;using IGK.DrSStudio.Drawing2D.SymbolManagementAddIn.WinUI;
using IGK.DrSStudio.WinUI;
namespace IGK.DrSStudio.Drawing2D.SymbolManagementAddIn.Tools
{
    [CoreToolsAttribute("Tool.SymbolManager")]
    public sealed class SymbolManager :  IGK.DrSStudio.Tools.CoreToolBase 
    {
        private static SymbolManager sm_instance;
        private string m_SymbolDirectory;
        private List<ICore2DDrawingLayeredElement> m_symbols;
        public string SymbolDirectory
        {
            get { return m_SymbolDirectory; }
            set
            {
                if (m_SymbolDirectory != value)
                {
                    m_SymbolDirectory = value;
                    OnSymbolDirectoryChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler SymbolDirectoryChanged;
        ///<summary>
        ///raise the SymbolDirectoryChanged 
        ///</summary>
        void OnSymbolDirectoryChanged(EventArgs e)
        {
            if (SymbolDirectoryChanged != null)
                SymbolDirectoryChanged(this, e);
        }
        private SymbolManager()
        {
            this.m_symbols = new List<ICore2DDrawingLayeredElement>();
            this.m_SymbolDirectory = SymbolConstant.DEFAULT_DIR;
        }
        public static SymbolManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static SymbolManager()
        {
            sm_instance = new SymbolManager();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }
        internal void ChooseSymbol()
        {
            using (SymbolSelectorForm frm = new SymbolSelectorForm())
            {
                using (ICoreDialogForm dialog = Workbench.CreateNewDialog())
                {
                    dialog.Controls.Add(frm);
                }
            }
        }
        internal static void Register(ICore2DDrawingLayeredElement element)
        {
            Instance.RegisterSymbol(element);
        }
        /// <summary>
        /// register symbol
        /// </summary>
        /// <param name="element"></param>
        private void RegisterSymbol(ICore2DDrawingLayeredElement element)
        {
            this.m_symbols.Add(element);
        }
        /// <summary>
        /// save symbol to current directory
        /// </summary>
        private void Save()
        {
        }
        /// <summary>
        /// load symbol from current directory
        /// </summary>
        private void LoadSysbol()
        { 
        }
    }
}

