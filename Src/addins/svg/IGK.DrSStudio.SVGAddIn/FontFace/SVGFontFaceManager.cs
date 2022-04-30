using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Imaging;
using IGK.ICore.IO.Font;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.SVGAddIn.FontFace
{
    [CoreService(nameof(SVGFontFaceManager))]
    public class SVGFontFaceManager : ICoreServiceManager, ICoreFontService
    {
        public string Description => SVGConstant.SERVICE_MANAGER_DESC;

        public static SVGFontFaceCollection OpenFile(string fontfaceFile)
        {
            SVGFontFaceCollection c = null;
            if (File.Exists(fontfaceFile))
            {
                c = new SVGFontFaceCollection();
                c.Load(fontfaceFile);
            }
            return c;
        }

        public bool ExtractGlyfFont(string filename, string outfile, bool bitmap, ICoreFontServiceCallback callback)
        { 
            var col = SVGFontFaceManager.OpenFile(filename);
            if (col == null)
                return false ;

            
            if (col.Count == 0)
            {

                var font = CoreFontFile.FontParser(filename, false );
                ushort[] list = font.UnicodeList();

                Core2DDrawingLayerDocument doc = new Core2DDrawingLayerDocument();
                int w = 20 * 32;// col.HorizontalAdvX;
                float fx = 32 / (float)100;
                doc.SetSize(w, (int)(32 * (Math.Ceiling(list.Length / 20.0f) + 1)));
                float dx = 0;
                float dy = 0;
                int c = 0;

                //using graphics to get point

                System.Drawing.Drawing2D.GraphicsPath pm = new System.Drawing.Drawing2D.GraphicsPath();
                var pfont = new System.Drawing.Text.PrivateFontCollection ();
                byte[] tb = File.ReadAllBytes(filename);
                IntPtr alloc = tb.CopyToCoTaskMemory();
                //alloc.ToStringCore
                //pfont.AddFontFile (filename);
                pfont.AddMemoryFont(alloc, tb.Length);
                Marshal.FreeCoTaskMem(alloc);


                System.Drawing.FontFamily  ft = pfont.Families[0];
                List<Vector2f> PTS = new List<Vector2f>();
                byte[] types;
                int ii = 0;
                foreach (ushort code  in list)
                {
                    //CoreLog.WriteLine("extract : " + ii);
                    ii++;
                    pm.Reset();
                    pm.AddString(((char)code).ToString(), ft, 0, 100, Point.Empty, new StringFormat());
                    if (pm.PointCount == 0)
                        continue;
                    PTS.Clear();
                    types = pm.PathTypes;
                    for (int i = 0; i < pm.PathPoints.Length; i++)
                    {
                        PTS.Add(new Vector2f(pm.PathPoints[i].X, pm.PathPoints[i].Y));

                    }
                    PathElement p = PathElement.CreateElement(PTS.ToArray(), types);

                    if (p != null)
                    {
                        p.Id = "char_" + code;
                        p.FillBrush.SetSolidColor(Colorf.FromFloat(0.2f));
                        p.Translate(dx, dy, enuMatrixOrder.Append);
                        p.Scale(fx, fx, enuMatrixOrder.Append);
                        //p.FlipY();
                        doc.CurrentLayer.Elements.Add(p);

                        c++;
                        if ((c % 20) == 0)
                        {
                            dx = 0;
                            dy += 100;
                        }
                        else
                        {
                            dx += 100;
                        }
                    }
                }
                pfont.Families[0].Dispose();
                pfont.Dispose();
                
                doc.SaveToFile(outfile);

                return true;
            }
            else
            {

                Core2DDrawingLayerDocument doc = new Core2DDrawingLayerDocument();
                int w = 20 * 32;// col.HorizontalAdvX;
                float fx = 32 / (float)col.HorizontalAdvX;
                doc.SetSize(w, (int)(32 * (Math.Ceiling(col.Count / 20.0f) + 1)));
                float dx = 0;
                float dy = 0;
                int c = 0;
                foreach (SVGFontGlyph i in col)
                {
                    PathElement p = PathElement.CreateElement(
                        i.Path);
                    if (p != null)
                    {
                        p.FillBrush.SetSolidColor(Colorf.FromFloat(0.2f));
                        p.Translate(dx, dy, enuMatrixOrder.Append);
                        p.Scale(fx, fx, enuMatrixOrder.Append);
                        p.FlipY();
                        doc.CurrentLayer.Elements.Add(p);

                        c++;
                        if ((c % 20) == 0)
                        {
                            dx = 0;
                            dy += col.HorizontalAdvX;
                        }
                        else
                        {
                            dx += col.HorizontalAdvX;
                        }
                    }
                }

                doc.SaveToFile(outfile);
                if (bitmap)
                {
                    var bmp = CoreApplicationManager.Application.ResourcesManager.CreateBitmap((int)doc.Width, (int)doc.Height);
                    var dev = CoreApplicationManager.Application.ResourcesManager.CreateDevice(bmp);
                    //Graphics g = Graphics.FromImage(bmp);
                    if (dev != null)
                    {
                        doc.Draw(dev);
                        dev.Flush();
                    }
                    bmp.Save(Path.Combine(Path.GetDirectoryName(outfile), Path.GetFileNameWithoutExtension(outfile) + ".png"),
                         CoreBitmapFormat.Png);
                }
            }
            return true;
        }
    }
}
