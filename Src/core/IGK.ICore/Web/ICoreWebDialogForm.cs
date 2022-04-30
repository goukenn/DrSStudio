using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Web
{
    public interface ICoreWebDialogForm : ICoreDialogForm
    {
        ICoreWebControl WebControl { get; }
    }
}
