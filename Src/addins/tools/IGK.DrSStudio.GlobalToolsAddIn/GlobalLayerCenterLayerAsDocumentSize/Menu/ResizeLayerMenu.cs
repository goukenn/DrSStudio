using IGK.ICore.WinCore;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools.Menu
{
    using IGK.ICore.Web;
    using IGK.DrSStudio.WinUI;
    using IGK.ICore.Xml;
    using IGK.ICore;
    using System.Runtime.InteropServices;
    using IGK.ICore.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Drawing2D.Menu;
    using IGK.ICore.Web.WinUI;
    using IGK.ICore.WinUI.Common;

    [CoreMenu(IGKD2DrawingConstant.MENU_LAYER+".Resize", 0x301)]
    public sealed class ResizeLayerMenu : Core2DDrawingMenuBase
    {
        private CoreUnit m_Unit;
        [ComVisible(true)]
        public class ScriptObject : CoreWebScriptObjectBase, ICoreWebDialogProvider
        {
            private ResizeLayerMenu resizeLayerMenu;
            public ICoreWebScriptObject OjectForScripting
            {
                get { return this; }
            }

          
            public ScriptObject(ResizeLayerMenu resizeLayerMenu)
            {
                this.resizeLayerMenu = resizeLayerMenu;
            }
            public void resize_size(string size)
            {
                this.resizeLayerMenu .m_Unit = size;
                this.DialogResult = enuDialogResult.OK;
            }
        }
        public ResizeLayerMenu()
        {
            this.m_Unit = CoreUnit.EmptyPixel;
        }
        protected override bool PerformAction()
        {
            using (var b = Workbench.CreateWebBrowserDialog(new ScriptObject(this) { Document = GetDocument() } ))
            {
                b.Title = "title.resizeToLayer".R();
                if (b.ShowDialog() == enuDialogResult.OK)
                {
                    this.CurrentSurface.CurrentLayer.Resize(this.m_Unit.GetPixel());
                    this.CurrentSurface.RefreshScene();
                }
            }
            return false;
        }

        private CoreXmlWebDocument GetDocument()
        {
            CoreXmlWebDocument document = CoreXmlWebDocument.CreateICoreDocument();
             
            ICoreWorkbenchDocumentInitializer v_n = this.Workbench as ICoreWorkbenchDocumentInitializer ;
            if (v_n !=null)
            {
                v_n.InitDocument(document);
            }            
           // IGK.ICore.Web.CoreWebExtensions.Idocument.Ini
            document.Body.LoadString(Encoding.UTF8 .GetString (Properties.Resources.layer_resize_layout).CoreEvalWebStringExpression());
            document.Body.getElementById("clsize")["value"] = this.m_Unit.ToString();
            return document;
        }
    }
}
