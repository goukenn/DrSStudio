using IGK.ICore;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.Menu
{
    [CoreMenu("Tools.Debug.Script.GenCoreImageKeyClass", 0x0001)]
    class GenCoreImagesKeyMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            var v_obj = new StoreObject();
           CoreResourcesManager.StoreObjectCollection(v_obj );
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "(csharp)*.cs|*.cs";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    v_obj.StoreToFile(sfd.FileName);
                }
            }
            
            return base.PerformAction();
        }
        class StoreObject : ICoreResourceObjectListener
        {
            List<string > _list ;
            public StoreObject()
            {
                _list = new List<string> ();
            }
            public void Store(string key, object value)
            {
                _list.Add (key);
            }

            internal void StoreToFile(string fileName)
            {
                StringBuilder sb = new StringBuilder( );
                sb.AppendLine ("namepsace IGK.ICore{");
                sb.AppendLine ("public static class CoreImageKeys{");
                string n = string.Empty;
                string g = string.Empty ;
                _list.Sort();
                foreach (var item in _list)
                {
                    n = item;
                    g = item;
                    if (!item.ToLower().EndsWith ("gkds"))
                        continue;
                    if (n.StartsWith("igk.drsstudio.resources:/")) {
                        g = n.Split('/').Last();
                        n = g.ToUpper();
                        sb.AppendLine($"public const string {n}=\"{g}\";");
                    }
                    
                }
                sb.AppendLine("}");
                sb.AppendLine ("}");
                File.WriteAllText (fileName,  sb.ToString ())
;            }
        }
    }
}
