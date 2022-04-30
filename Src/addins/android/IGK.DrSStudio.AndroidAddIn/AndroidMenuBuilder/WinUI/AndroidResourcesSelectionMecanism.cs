
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.Mecanism;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.WinUI
{
    /// <summary>
    /// android mecanism selection
    /// </summary>
    sealed class AndroidResourcesSelectionMecanism : SelectionElement.Mecanism
    {

        protected override void OnMouseClick(ICore.WinUI.CoreMouseEventArgs e)
        {
           // base.OnMouseClick(e);
        }
        protected override void OnMouseMove(CoreMouseEventArgs e)
        {
           // base.OnMouseMove(e);
        }
        protected override void OnMouseUp(CoreMouseEventArgs e)
        {
            //base.OnMouseUp(e);
        }
        protected override void OnMouseDown(CoreMouseEventArgs e)
        {
            base.OnMouseDown(e);
        }
        protected override void GenerateActions()
        {
            base.GenerateActions();
        }
        protected override void GenerateSnippets()
        {
           // base.GenerateSnippets();
        }
        public override void Render(ICoreGraphics device)
        {
            base.Render(device);
        }
    }
}
