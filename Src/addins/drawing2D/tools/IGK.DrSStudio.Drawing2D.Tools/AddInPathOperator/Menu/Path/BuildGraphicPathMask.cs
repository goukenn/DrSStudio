using IGK.DrSStudio.Drawing2D.Menu;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInPathOperator.Menu.Path
{
    [CoreMenu("Path.MaskPath", 0xa2)]
    class BuildGraphicPathMask : PathElementMenuBase
    {
        protected override bool PerformAction()
        {
            var l = this.CurrentSurface.CurrentDocument.AddNewLayer();
            var doc = this.CurrentSurface.CurrentDocument;

            using (Bitmap bmp = new Bitmap(doc.Width, doc.Height)) {
                using (Graphics gr = Graphics.FromImage(bmp)) {
                    ICoreGraphics d = IGK.ICore.CoreApplicationManager.Application.ResourcesManager?.CreateDevice(gr);//.GraphicsPathUtils.
                    if (d != null) {
                        bool doc_t = doc.BackgroundTransparent;
                        doc.BackgroundTransparent = true;
                        d.Draw(doc);
                        d.Flush();
                        doc.BackgroundTransparent = doc_t;
                    }
                    
                }
                var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Byte[] v_data = new byte[data.Stride * data.Height];
                Marshal.Copy(data.Scan0, v_data, 0, v_data.Length);
                byte r, g, b, a;
                int v_offset;
                int h = bmp.Height;
                int w = bmp.Width; 

                PathElement p = new PathElement();
                CoreGraphicsPath path = new CoreGraphicsPath();
                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w ; j++)
                    {

                        v_offset = (i * data.Stride) + (j*4);
                        b = v_data[v_offset ];
                        g = v_data[v_offset + 1];
                        r = v_data[v_offset + 2];
                        a = v_data[v_offset + 3];

                        if (b != 0)
                        {
                            path.AddRectangle(new Rectanglef(j, i, 1, 1));
                        }
                        else
                        {
                        }
                    }
                }
          
                bmp.UnlockBits(data);
                p.SetDefinition(path);
                l.Elements.Add(p);
            }
            return base.PerformAction();
        }
    }
}
