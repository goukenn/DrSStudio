using IGK.DRSStudio.BalafonDesigner.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.BalafonDesigner
{
    public class BalafonExposer
    {
        public static Control CreateSurface() {

            var _surface =  new BalafonViewDesignerSurface();

          

            return _surface;
        }

        private static void ApplicationKeyHandle(object sender, KeyEventArgs e)
        {
          
        }
    }
}
