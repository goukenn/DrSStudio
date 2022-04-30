using IGK.DrSStudio.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Imaging.Menu
{
    using IGK.ICore;
    using IGK.ICore.Dependency;
    using IGK.ICore.Web;
    using IGK.ICore.WinUI.Common;
    using IGK.ICore.Xml;

    [DrSStudioMenu("Image.Info", int.MaxValue, SeparatorBefore = true)]
    class ImageInfoMenu : ImagingMenuBase 
    {
        private Dictionary<string, object> _expression;
        protected override bool PerformAction()
        {
            if (this.ImageElement == null) {
                return false;
            }
            var v_inf =  this.ImageElement.GetParam ("Image:Info") as Dictionary<string, object >;;
            if (v_inf !=null){
                this._expression = v_inf as Dictionary<string, object >;
               var s =  CoreCommonDialogUtility.BuildWebDialog (this.Workbench, 
                    "title.ImageInfo".R(), 
                    CoreWebUtils.EvalWebStringExpression (@"<body><div class=""igk-title-4"" >Infos: </div><div>[imeth:getExpressionList]</div></body>", this));
          
            }
            return false;
        }

        public string getExpressionList() {
            var t = CoreXmlElement.CreateXmlNode("table");
            t["class"] = "igk-table";
            foreach (var item in this._expression)
            {
                var tr = t.add("tr");
                tr.add("td").setContent(item.Key);
                tr.add("td").setContent(item.Value);
            }
            return t.RenderToXml ();//.Render();
        }
    }
}
