using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Balafon.Tools.BrushManagement.Menu
{
    [CoreMenu("Tools.Balafon.GetCssStyleDefinition", 1)]
    class BrushToCssMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            var c = (this.CurrentSurface.ElementToConfigure as ICoreBrushOwner)?.GetBrush(ICore.enuBrushMode.Fill);

            if (c != null) {
              string s =  BalafonManageUtils.ConvertToCss(c);
                if (!string.IsNullOrEmpty(s))
                {
                    
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "css definition | *.css";
                        sfd.FileName = "out.css";
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            File.WriteAllText(sfd.FileName, s);
                        }
                    }
                }
            }
            return base.PerformAction();
        }
    }
}
