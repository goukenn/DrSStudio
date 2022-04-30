using IGK.ICore.Web;
using IGK.ICore.Web.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.Actions
{
    [ComVisible(true)]
    public class GSTaskScriptObject : CoreWebScriptObjectBase
    {
        private ICoreWebDialogProvider m_provider;

        public GSTaskScriptObject(ICoreWebDialogProvider taskAction)
        {
            this.m_provider = taskAction;
        }
        public override void Submit(object data)
        {
            if (data != null)
            {
                if (this.m_provider.UpdateData(data.ToString()))
                    this.m_provider.Dialog.DialogResult = enuDialogResult.OK;
            }
            else
                this.m_provider.Dialog.DialogResult = enuDialogResult.OK;
        }
    }
}
