using IGK.ICore;
using IGK.ICore.Web;
using IGK.ICore.Web.WinUI;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Registrable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    /*
    class DrSStudioWebBrowserShortcutKeysHandler : IMessageFilter 
    {
        private ICoreDialogForm dialog;
        private ICoreWebScriptObject objectForScripting;

        
        public DrSStudioWebBrowserShortcutKeysHandler(ICoreDialogForm dialog,
            ICoreWebScriptObject objectForScripting, IWebBrowserControl control)
        {
            this.dialog = dialog;
            this.objectForScripting = objectForScripting;
            this.control = control;
            this.dialog.Disposed += dialog_Disposed;
            this.registerHandler();
        }

        private void registerHandler()
        {
            Application.AddMessageFilter(this);
        }

        void dialog_Disposed(object sender, EventArgs e)
        {
            unregisterHandler();
        }

        private void unregisterHandler()
        {
            Application.RemoveMessageFilter (this);   
        }

        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_CHAR = 0x0102;
        private IWebBrowserControl control;
        public bool PreFilterMessage(ref Message m)
        {
            enuKeys c = (enuKeys)m.WParam | CoreApplicationManager.ModifierKeys;
            switch (m.Msg)
            {
                case WM_KEYDOWN:
                    if (c == enuKeys.F5)
                    {
                        //handle F5 hit
                        this.objectForScripting.Reload();
                        var t = (this.objectForScripting as ICoreWebDialogProvider);
                        if (t!=null)
                            this.control.HtmlDocument = t.Document.Render(null);
                        return true;
                    }
                    break;
                case WM_KEYUP:                   
                    break;
            }
            return false;
        }
    }
     * */
}
