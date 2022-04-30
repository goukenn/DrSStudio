using IGK.DrSStudio.Drawing2D.Menu;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Menu;
using IGK.ICore.Web;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInPathOperator.Menu.Path
{
    [CoreMenu("Path.FlattenPath", 0xa2)]
    class FlattenPathMenu: PathElementMenuBase
    {

        private float flatness;

        ///<summary>
        ///public .ctr
        ///</summary>
        public FlattenPathMenu():base()
        {
            flatness = 1.0f;
        }
        public string getFlatness() {
            return flatness.ToString();
        }
        protected override bool PerformAction()
        {
            var l = this.PathElement;
            var g = l.GetPath().Clone() as CoreGraphicsPath;

            var d = CoreCommonDialogUtility.BuildWebDialog(
                 CoreSystem.Instance.Workbench,
                 "title.path.flatness".R(),CoreWebUtils.EvalWebStringExpression(@"
<div style=""font-size:2em"">Select Flatness</div>
<form>
<div class=""igk-row"">
<div class=""igk-col igk-col-3-3"" style=""padding:40px 30px"">
<div><input value=""[imeth:getFlatness]"" class=""igk-form-control"" id='flatness' /></div>
<div><input type=""submit"" value=""[lang:btn.send]"" class=""igk-btn igk-btn-default igk-dialog-btn"" onclick=""javascript:ns_igk.ext.call('UpdateData', {flatness:this.form.flatness.value}); return false;"" /></div>
</div>
</div>
</form>
", this));
            if (d != null)
            {
                IGK.ICore.JSon.CoreJSon c = new ICore.JSon.CoreJSon();
                var trd = c.ToDictionary(d.ToStringCore());
                float cc = 0.0f;
                if (float.TryParse(trd["flatness"].ToString(), out cc))
                {
                    this.flatness = cc;
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }
            
                g.Flatten(this.flatness);
            
                Vector2f[] p = null;
                byte[] t = null;
                g.GetAllDefinition(out p, out t);


                l.SetDefinition(p, t);
                g.Dispose();
            
            return false;
        }
    }
}
