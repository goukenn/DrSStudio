

using IGK.DrSStudio.WinUI;
using IGK.ICore.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing3D.FileBrowser.Menu.Window
{
    using IGK.DrSStudio;
    using IGK.DrSStudio.Drawing3D.FileBrowser.AnimationModel;
    using IGK.ICore;
    using IGK.ICore.Menu;
    using IGK.ICore.Web;
    using IGK.ICore.Web.WinUI;
    using IGK.ICore.WinUI.Common;
    using IGK.OGLGame.Graphics;
    using System.Runtime.InteropServices;

    [CoreMenu("FB.ChooseBlending",0x1)]
    public sealed class FBAnimationBlendingModeMenu : FBSurfaceMenu
    {
        [ComVisible(true)]
        public class objectForScripting :CoreWebScriptObjectBase, ICoreWebDialogProvider
        {
            private FBAnimationBlendingModeMenu fBAnimationBlendingModeMenu;
            internal enuTextureEnvironmentMode EnvMode;
            public objectForScripting(FBAnimationBlendingModeMenu owner)
            {
                this.fBAnimationBlendingModeMenu = owner;
            }
            public void updateValue(string name)
            {
                CubeAnimationModel anim = 
                    this.fBAnimationBlendingModeMenu.CurrentSurface.Scene.CurrentAnimModel
                    as CubeAnimationModel ;

                EnvMode = (enuTextureEnvironmentMode)
                    Enum.Parse(typeof(enuTextureEnvironmentMode), name);
                anim.EnvMode = EnvMode;
            }


            public ICoreWebScriptObject OjectForScripting
            {
                get { return this; }
            }

           
        }
        protected override bool PerformAction()
        {
            CubeAnimationModel anim = 
                this.CurrentSurface.Scene.CurrentAnimModel as CubeAnimationModel;
            if (anim ==null)
                return false;
            objectForScripting c = new objectForScripting (this);
            CoreXmlWebDocument doc = CoreXmlWebDocument.CreateICoreDocument();
            var frm = doc.Body.addForm();
            var lb = frm.Add("label");
            lb.Content = "lb.blending.mode".R();
            lb["for"] = "blending";
            
            var select = frm.Add("select");
            select["id"] = "blending";
            select["onchange"] = "window.external.updateValue(this.value); return false;";
            foreach (var item in Enum.GetNames(typeof(enuTextureEnvironmentMode)))
            {
                select.Add("option", new Dictionary<string, string>() {
                    {"value", item}
                }).Content = item;
            }


            c.Document = doc;
            using (var dialog = Workbench.CreateWebBrowserDialog(             
                c))
            {
                dialog.Title = "title.chooseBlendingMode".R();
                if (dialog.ShowDialog() == enuDialogResult.OK)
                {
                    anim.EnvMode = c.EnvMode;
                }
            }
            return base.PerformAction();
        }
    }
}
