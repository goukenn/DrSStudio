using IGK.ICore.WinCore;
using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;

namespace IGK.ICore.WinCore
{
    static class AttributeValueRenderer 
    {
        static AttributeValueRenderer() {
            CoreRendererBase.InitRenderer(System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType);
        }

        public static Colorf AttributePanImageMarginColor { get { return CoreRenderer.GetColor("AttributePanImageMarginColor", Colorf.DarkCyan); } }
        public static Colorf AttributePanColor1 { get { return CoreRenderer.GetColor("AttributePanColor1", Colorf.FromString("#9d9de9")); } }
        public static Colorf AttributePanColor2 { get { return CoreRenderer.GetColor("AttributePanColor2", Colorf.FromString("#EFEFEF")); } }
        public static Colorf AttributePanForeColor { get { return CoreRenderer.GetColor("AttributePanForeColor", Colorf.White); } }
        public static Colorf AttributePanSelectedColor1 { get { return CoreRenderer.GetColor("AttributePanSelectedColor1", Colorf.SkyBlue); } }
        public static Colorf AttributePanSelectedColor2 { get { return CoreRenderer.GetColor("AttributePanSelectedColor2", Colorf.CornflowerBlue); } }
        public static Colorf AttributePanSelectedForeColor { get { return CoreRenderer.GetColor("AttributePanSelectedForeColor", Colorf.White); } }
        public static Colorf AttributeSplitter { get { return CoreRenderer.GetColor("AttributeSplitter", Colorf.White ); } }
        public static Colorf AttributePanValueForeColor { get { return CoreRenderer.GetColor("AttributePanValueForeColor", Colorf.Black); } }
        public static Colorf AttributePanSelectedValueForeColor { get { return CoreRenderer.GetColor("AttributePanSelectedValueForeColor", Colorf.White); } }
    }
}
