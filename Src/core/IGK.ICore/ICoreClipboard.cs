using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    public interface ICoreClipboard : ICoreApplicationService
    {
        void CopyToClipboard(string dataType, object obj);
        /// <summary>
        /// get string data
        /// </summary>
        /// <returns></returns>
        string GetTextData();
    }
}
