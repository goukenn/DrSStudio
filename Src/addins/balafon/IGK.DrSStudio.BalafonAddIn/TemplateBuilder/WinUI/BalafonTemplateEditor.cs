using IGK.ICore;
using IGK.ICore.Resources;
using IGK.ICore.WinCore.WinUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.TemplateBuilder.WinUI
{

    [System.Drawing.ToolboxBitmap (typeof(BalafonTemplateEditor), "balafon.templateEditor.png")]
    [CoreSurface("{0865121B-24CA-478B-BFE7-905255FDCBD2}")]
    public class BalafonTemplateEditor : IGKXWinCoreWorkingFileManagerSurface
    {
        
        /// <summary>
        /// .ctr
        /// </summary>
        public BalafonTemplateEditor()
        {
            this.InitializeComponent();
            this.FileName = "Template.btemplates";
            this.Title = "Template.btemplates";
            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
        }


        private void InitializeComponent()
        {
            
        }
    }
}
