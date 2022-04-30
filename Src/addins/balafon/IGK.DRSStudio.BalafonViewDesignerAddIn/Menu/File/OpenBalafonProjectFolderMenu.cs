using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DRSStudio.BalafonDesigner.Menu.File
{
    [CoreMenu("File.Open.BalafonProjectFolder", 0x1)]
    internal class OpenBalafonProjectFolderMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            ICoreSolutionManagerWorkbench benchSolution =  Workbench as ICoreSolutionManagerWorkbench;
            using (IGK.ICore.WinUI.Common.FolderNamePicker t =
                   Workbench.CreateCommonDialog<FolderNamePicker>(
                       ICore.WinUI.Common.CoreCommonDialogs.FolderNamePicker))
            {
                if (t != null)
                {
                    t.Title = "title.selectfoldertoload".R();
                    t.SelectedFolder = Environment.CurrentDirectory;
                    if (t.ShowDialog() == enuDialogResult.OK)
                    {
                        benchSolution.Solution = BalafonViewDesignerSolution.CreateSolution(t.SelectedFolder);


                       
                    }
                }
                else
                {

                    using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                    {
                        fbd.SelectedPath = Environment.CurrentDirectory;
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            //this.CurrentSurface.Project.LoadFolderFeature());
                            benchSolution.Solution = BalafonViewDesignerSolution.CreateSolution(fbd.SelectedPath);
                        }
                    }
                }
            }
            //using (FolderBrowserDialog ofd = new FolderBrowserDialog())
            //{
            //    if (ofd.ShowDialog() == DialogResult.OK)
            //    {
            //        benchSolution.Solution = BalafonViewDesignerSolution.CreateSolution(ofd.SelectedPath);
            //    }
            //}
            return base.PerformAction();
        }
    }
}
