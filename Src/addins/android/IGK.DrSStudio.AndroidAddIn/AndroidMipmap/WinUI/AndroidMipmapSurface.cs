using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;

namespace IGK.DrSStudio.Android.AndroidMipmap.WinUI
{
    class AndroidMipmapSurface : IGKD2DDrawingSurface
    {
        ///<summary>
        ///public .ctr
        ///</summary>
        public AndroidMipmapSurface()
        {
            
        }
        protected override Core2DDrawingDocumentBase CreateNewDocument() => base.CreateNewDocument();
        
        public override bool AllowMultiDocument => false;
      

    }
}
