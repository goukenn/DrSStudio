using System;
using IGK.ICore.GraphicModels;

namespace IGK.ICore.Drawing2D.Mecanism
{
    internal class ObjectRenderer
    {
        private ICoreGraphics device;
        private CoreGraphicsPath e;

        public ObjectRenderer(ICoreGraphics device, CoreGraphicsPath e)
        {
            this.device = device;
            this.e = e;
        }

        internal void Render(enuDrawing2DEditionMode t)
        {


            switch (t)
            {
                case enuDrawing2DEditionMode.Document:
                case enuDrawing2DEditionMode.Group:
                    device.FillPath(Colorf.FromFloat(0.3f, 0.4f), e);
                    break;

            }
            //    )
            //{
            //    //if (this.EditionMode != enuDrawing2DEditionMode.Local)
            //    //{
            //        //var p = this.Element.GetPath() as CoreGraphicsPath;



            //    //this.m_flattenItem.Draw(e);
            //    //device.Restore(v_state);


            //    //}
            //}
        }
    }
}