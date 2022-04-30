
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmap8BppSurfaceAddin.WinUI
{
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinCore;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;

    [CoreSurface("Bitmp8BppSurface", EnvironmentName = CoreConstant.DRAWING2D_ENVIRONMENT,
    DisplayName = "8Bpp Surface")]
    class Bitmp8BppSurface : IGKD2DDrawingSurface
    {
        public override bool CanAddDocument => false;

        //Bitmap m_cbitmap;

        public Bitmp8BppSurface()
        {
            //this.m_cbitmap = new Bitmap(32,32, PixelFormat.Format8bppIndexed);
        }
        protected override IGKD2DDrawingScene CreateScene()
        {
            return new Bitmap8BppSurfaceScene(this);
        }
        
        protected override Core2DDrawingDocumentBase CreateNewDocument()
        {
            return new Bitmap8BppDocument();//.CreateNewDocument();
        }

        /// <summary>
        /// represent a Bitmap 8 Document
        /// </summary>
        public class Bitmap8BppDocument : Core2DDrawingLayerDocument
        {
            public Bitmap8BppDocument():base(256,256)
            {

            }

            protected override ICore2DDrawingLayer CreateNewLayer(){
             
                return new Bitmap8BppDocumentLayer();
            }
           
        }

        class Bitmap8BppDocumentLayer : Core2DDrawingLayer
        {
            public Bitmap8BppDocumentLayer()
            {

            }
        }

        class Bitmap8BppSurfaceDocumentRenderer : IGKD2DDrawingScene.IGKD2DDrawingSceneDocumentRender
        {
            private Bitmap8BppSurfaceScene m_scene;
            public Bitmap8BppSurfaceDocumentRenderer(Bitmap8BppSurfaceScene scene) : base(scene)
            {
                this.m_scene = scene;
            }
            public override void Render(ICoreGraphics device)
            {
                base.Render(device);
                //var obj = device.Save();
                //this.SetupDevice(device);
                ////draw bitmpa

                //var v = this.m_scene.Owner as Bitmp8BppSurface;
                ////Graphics vg = Graphics.FromImage(v.m_cbitmap);
                ////vg.Clear(Color.Yellow);
                ////vg.Flush();
                ////vg.Dispose();
                //int w =(int) v.CurrentDocument.Width;
                //int h = (int)v.CurrentDocument.Height;
                //using (ICoreBitmap c = WinCoreExtensions.ToCoreBitmap(v.m_cbitmap))
                //{//?.toCoreBitmap();
                //    var v_rc = new Rectanglei(0, 0, w, h);
                //    device.DrawRectangle(Colorf.Red, v_rc);
                //    device.Draw(c, v_rc);
                //    //device.Draw(v.m_cbitmap);



                //}

                //device.Restore(obj);
            }
        }
        class Bitmap8BppSurfaceScene : IGKD2DDrawingScene{

            public Bitmap8BppSurfaceScene(Bitmp8BppSurface surface):base(surface)
            {

            }

            protected override IGKD2DDrawingSceneDocumentRender CreateDocumentSceneRenderer()
            {
                return new Bitmap8BppSurfaceDocumentRenderer(this);
            }
            protected override void InitFrames()
            {
                base.InitFrames();
               
            }

            protected override void RenderFrames(ICoreGraphics g)
            {
                base.RenderFrames(g);
               
            }

         
        }
    }
}
