using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools
{
    public static class CenterLayerAsDocumentSize
    {
        public static void Resize(this ICore2DDrawingLayer layer, float inflate) {
            if (layer == null) return;

            var v_document = layer.ParentDocument as Core2DDrawingDocumentBase ;
            if (v_document == null) return;


            var l = layer;
            var r = Rectanglef.Empty;
            float minx = 0.0f;
            float miny = 0.0f;
            float maxx = 0.0f;
            float maxy = 0.0f;
            int i = 0;
            foreach (ICore2DDrawingLayeredElement item in l.Elements)
            {
                r = item.GetBound();
                if (i == 0)
                {
                    minx = r.X;
                    miny = r.Y;
                    maxx = r.Right;
                    maxy = r.Bottom;

                }
                else
                {
                    minx = Math.Min(minx, r.X);
                    miny = Math.Min(miny, r.Y);
                    maxx = Math.Max(maxx, r.Right);
                    maxy = Math.Max(maxy, r.Bottom);
                }
                i++;
            }
            var v_rc = new Rectanglef(
            minx,
            miny,
            maxx - minx,
            maxy - miny);
            //CoreMathOperation.GetBounds(rc.ToArray());
           //var m_defaultBound =new  Rectanglef (0,0, v_rc.Width, v_document.Height);
           v_rc.Inflate(inflate, inflate);
           v_document.SetSize(v_rc.Width, v_rc.Height);

            v_document.Translate(-v_rc.X,
                -v_rc.Y); 




           // var r = Rectanglef.Empty;
           // float minx = 0.0f;
           // float miny = 0.0f;
           // float maxx = 0.0f;
           // float maxy = 0.0f;
           // int i = 0;
           // foreach (ICore2DDrawingLayeredElement item in layer.Elements)
           // {
           //     r = item.GetBound();
           //     if (i == 0)
           //     {
           //         minx = r.X;
           //         miny = r.Y;
           //         maxx = r.Width;
           //         maxy = r.Height;
           //     }
           //     else
           //     {
           //         minx = Math.Min(minx, r.X);
           //         miny = Math.Min(miny, r.Y);
           //         maxx = Math.Max(maxx, r.X + r.Width);
           //         maxy = Math.Max(maxy, r.Y + r.Height);
           //     }
           //     // rc.Add(item.GetBound());
           //     i++;
           // }
           // var v_rc = new Rectanglef(
           // minx,
           // miny,
           // maxx - minx,
           // maxy - miny);
           // //CoreMathOperation.GetBounds(rc.ToArray());
           //var v_defaultBound = v_rc; //store
           // v_rc.Inflate(inflate , inflate );
           // //get translation of 


          
           // v_document.SetSize(v_rc.Width, v_rc.Height);
           // //v_document.Translate(
           // //  -v_rc.X + ((-v_defaultBound.Width + v_rc.Width) / 2.0f),
           // // - v_rc.Y + (-v_defaultBound.Height + v_rc.Height) / 2.0f);


           // v_document.Translate((v_rc.X / 2.0f) + (-v_defaultBound.Width + v_rc.Width) / 2.0f,
           //       (v_rc.X / 2.0f) + (-v_defaultBound.Height + v_rc.Height) / 2.0f); 

        }
    }
}
