using IGK.ICore.Web;
using IGK.ICore.WinCore.WinUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI
{
    /*
    /// <summary>
    /// used to refresh web control on key press on F5
    /// </summary>
    public sealed class CorePrefilterKeyHandler: IMessageFilter
        {
            public CorePrefilterKeyHandler()
            {
            }

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == 0x100)
                {
                    if (m.WParam.ToInt32() == 0x74)
                    {
                        if (this.m_listener != null)
                        {
                            this.m_listener.Reload();
                            return true;
                        }
                    }
                }
                return false;
            }

          
        }
     * */
}
