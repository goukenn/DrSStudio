using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Window.VS.WinUI
{
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.Xml;
    using System.Windows.Forms;

    /// <summary>
    /// xml property viewser surface
    /// </summary>
    public class SLNSolutionFileExplorerSurface : IGKXWinCoreWorkingSurface, IAttributeEditorLoader 
    {
        private IGKXAttributeEditor c_viewer;
        public SLNSolutionFileExplorerSurface()
        {
            c_viewer = new IGKXAttributeEditor();
            c_viewer.Dock = DockStyle.Fill;
            c_viewer.SetAttributeLoaderListener(this);
            this.Controls.Add(c_viewer);
        }
        public void LoadFile(string filename)
        {
            var d = SLNFileCsProjFile.Open(filename);
            if (d != null)
            {
                c_viewer.ClearNode();
                c_viewer.LoadNode(d);                
            }
        }
        public void LoadAttribute(IGKXAttributeEditor editor, CoreXmlElement NodeName)
        {
            var c = editor.Attributes;
            c.Clear();

            c.Add("<Content>",
                "string",
                (string)NodeName.Content);
            foreach (KeyValuePair<string, CoreXmlAttributeValue> s in NodeName.Attributes)
            {
                c.Add(s.Key,
                "string",
                s.Value.GetValue());
            }
        }
    }
}
