using IGK.ICore.Web;
using IGK.ICore.Web.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IGK.ICore.WinUI.Common
{
    [ComVisible(true)]
    public sealed class CoreCommonWebDialogProvider :
        CoreWebScriptObjectBase,
        ICoreWebDialogProvider, ICoreWebScriptObject, ICoreWebReloadDocumentListener
    {
        public ICoreWebScriptObject OjectForScripting
        {
            get { return this; }
        }

       

        public override bool UpdateData(string data)
        {
            this.Result = data;
            if (this.Dialog != null)
            {
                this.Dialog.DialogResult = enuDialogResult.OK;
            }
            return true;
        }

      


        

        public override void Submit(object data)
        {
            string g = (string)data;
            this.Result = data;
        }

        public object Result { get; set; }

        public void ReloadDocument(CoreXmlWebDocument document,bool attachToBrowser)
        {
            CoreLog.WriteDebug("Handle common web dialog reload"); 
        }
    }
}
