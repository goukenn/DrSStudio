using IGK.ICore;
using IGK.ICore.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.Android.AndroidLayoutDesigner.WinUI
{

    [CoreSurface("{6DD56013-74BD-4152-9DDC-0DD198CC42F1}", EnvironmentName ="AndroidLayoutDesign")]
    /// <summary>
    /// represent a drawing 2D Layout surface 
    /// </summary>
    class AndroidLayoutEditorSurface : IGKD2DDrawingSurface
    {
        private CoreUnit m_defaultWidth;
        private CoreUnit m_defaultHeight;
        public override bool AllowMultiDocument => false;
        ///<summary>
        ///public .ctr
        ///</summary>
        public AndroidLayoutEditorSurface()
        {

        }
        public override bool AddNewDocument()
        {
            return base.AddNewDocument();
        }

        protected override Core2DDrawingDocumentBase CreateNewDocument()
        {
            var r = base.CreateNewDocument();
            r.SetSize(480, 790);
            return r;
        }
        public override void SetParam(ICoreInitializatorParam p)
        {
            base.SetParam(p);
            this . m_defaultWidth = p["width"];
            this . m_defaultHeight = p["height"];
            this.FileName = p["FileName"];
        }
    }
}
