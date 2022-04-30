using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.ScriptBuilderAddIn;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.WinUI.Common;

namespace  IGK.DrSStudio.ScriptBuilderAddIn.Menu
{
    [CoreMenu ("Tools.BuildScript", -10)]
    class BuildScriptFileInSurfaceDocumentMenu : 
        Core2DDrawingMenuBase,
        IScriptResponseListener 
    {
        protected override bool PerformAction()
        {
             using (var ofd = this.Workbench.CreateOpenFileDialog())
            {
                if (ofd == null)
                    return false;
                ofd.Filter = "script file | *.igkcs";
                if (ofd.ShowDialog() == enuDialogResult.OK)
                {
                    this.MainForm.Cursor = CoreCursors.Wait;
                    if (!ScriptBuilder.BuildScriptFile(this, this.CurrentSurface.CurrentDocument, ofd.FileName))
                    {
                        string msg = string.Empty;
                        if (this.CompilationErrors !=null)
                            msg = string.Join("\n", this.CompilationErrors);
                        CoreMessageBox.Show("err.buildscript.compilationfailed_1".R(msg), "title.error".R());
                    }
                    else {
                        CoreMessageBox.Show("msg.buildscript.compilationsucess".R());
                    }
                    this.MainForm.Cursor = CoreCursors.Normal;
                }
            }
            return false;
        }

        public bool CompilationSuccess
        {
            get;
            set;
        }

        public string[] CompilationErrors
        {
            get;
            set;
        }
    }
}
