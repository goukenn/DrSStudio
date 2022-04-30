using IGK.DrSStudio.ColladaEngine.IO;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Menu;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ColladaEngine.Menu.Exports
{
    [CoreMenu("File.Exports.Collada", 10)]
    class ColladaExportDataMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            using (var sfd = Workbench.CreateNewDialog()) {

                if (sfd.ShowDialog() == ICore.WinUI.enuDialogResult.OK) {

                    ColladaFile file = ColladaFile.CreateNew ();

                    file.Save("outfile.dae");

                }
            }


                return false;

        }
    }
}
