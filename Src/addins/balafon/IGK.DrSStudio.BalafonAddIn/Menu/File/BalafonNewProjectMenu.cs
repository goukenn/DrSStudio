
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.Menu.File
{
    using IGK.ICore.Menu;
    using IGK.ICore.Web;
    using IGK.ICore;
    using IGK.ICore.Xml ;
    using IGK.ICore.WinUI;
    using IGK.DrSStudio.Balafon.WinUI;
    using IGK.DrSStudio.Balafon.Xml;
    using IGK.ICore.Resources;
    using IGK.ICore.WinUI.Common;
    using IGK.ICore.IO;

    [CoreMenu ("File.New.BalafonProject", 0x600)]
    class BalafonNewProjectMenu : CoreApplicationMenu, ICoreWebReloadDocumentListener
    {
        private BalafonScriptRow m_scriptRow;        
        protected override bool PerformAction()
        {
            m_scriptRow = new BalafonScriptRow();
            m_scriptRow.SetReloadListener(this);
            m_scriptRow.DocumentChanged += (o, e) =>
            {
                m_scriptRow.Document.InitDocument();
                this.ReloadDocument(m_scriptRow.Document, true );

            };
            using (var d = Workbench.BuildWebDialog(m_scriptRow, 
               "title.balafon.NewProject".R(),
               string.Empty, 
               m_scriptRow))
            {

                d.Size = new Size2i(900, 540);
                if (d.ShowDialog() == enuDialogResult.OK)
                {
                    BalafonProject p = m_scriptRow.Project;
                    var v_s = new BalafonEditorSurface();
                    v_s.LoadProject(p);
                    this.Workbench.AddSurface(v_s, true);
                }
            }
            return false;
        }
        private string GetDocumentString() {
            var s =
#if !DEBUG
                CoreResources.GetResource("balafon_new_project");
#else
 CoreFileUtils.ReadAllBytes(CoreConstant.DRS_SRC + @"\addins\web\IGK.DrSStudio.BalafonAddIn\Resources\balafon_new_project.html") ??
CoreResources.GetResource("balafon_new_project");
#endif
            if (s!=null)
            return CoreWebUtils.EvalWebStringExpression(Encoding.UTF8.GetString(s), this.m_scriptRow);
            return string.Empty;

        }
        public void ReloadDocument(CoreXmlWebDocument document, bool attach)
        {
            document.Body.ClearChilds();

            document.Body.LoadString(GetDocumentString());
            document.Body.AppendScriptSource(
                CoreResources.GetResourceString("databasebuilder/resources/balafon.js", null, GetType().Assembly));

        }
    }
}
