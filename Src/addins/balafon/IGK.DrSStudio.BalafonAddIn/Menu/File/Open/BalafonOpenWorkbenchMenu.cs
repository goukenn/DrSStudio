
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.Menu.File.Open
{
    using IGK.ICore;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Common;
    using System.IO;
    using WinUI;
    using Xml;

    /// <summary>
    /// BALAFON Workbench
    /// </summary>
    [CoreMenu("File.Open.BalafonWorkbench", 0xB1)]
    class BalafonOpenWorkbenchMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            BalafonProject v_project = null;
            using (IGK.ICore.WinUI.Common.FolderNamePicker t =
                    Workbench.CreateCommonDialog<FolderNamePicker>(CoreCommonDialogs.FolderNamePicker))
            {

                if (t != null)
                {
                    t.Title = "title.selectFolderWorkbench".R();
                    t.SelectedFolder = Environment.CurrentDirectory;
                    if (t.ShowDialog() == enuDialogResult.OK)
                    {
                        string dirName = Path.GetFileName (t.SelectedFolder);
                        string f= IO.Utils.GetWorbenchFile(t.SelectedFolder); 

                        //if (File.Exists(f)) {

                        //   BalafonProject v_project =  Balafon.IO.Utils.OpenFile(f);
                        //    if (v_project != null) {
                        //        var v_s = new BalafonEditorSurface();
                        //        v_s.LoadProject(v_project);
                        //        this.Workbench.AddSurface(v_s, true);
                        //        return true;
                        //    }
                        //}


                        //build project from directory
                       v_project= Balafon.IO.Utils.LoadWorkbench(t.SelectedFolder);

                        if (v_project != null)
                        {
                            if (this.Workbench is ICoreWorkbenchEnvironmentHandler m)
                                m.Environment = BalafonEnvironment.Create(v_project,
                                 this.Workbench);
                        }
                        //this.Workbench.SelectedWorkbench = v_project;
                    }
                }
            }
            return  false;
        }
    }
}
