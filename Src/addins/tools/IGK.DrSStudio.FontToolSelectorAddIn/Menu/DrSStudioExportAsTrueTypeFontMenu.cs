using IGK.DrSStudio.Editor.FontEditor;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Drawing2D.WorkingObjects.Standard;
using IGK.ICore.IO;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinCore.Menu
{
    [CoreMenu("File.ExportTo.TrueTypeFont", 0x30)]
    public class DrSStudioExportAsTrueTypeFontMenu : CoreApplicationMenu
    {

        class Visitor
        {
            public ICore2DDrawingDocument Document { get; internal set; }

            internal void Visit(ICore2DDrawingElement g, CoreGraphicsPath p)
            {
                MethodInfo.GetCurrentMethod().Visit(this, g, p);
            }
            public void Visit(QuadraticElement element, CoreGraphicsPath p) {
                this.Visit(element as ICoreQuadraticPath, p);
            }
            public void Visit(ICoreQuadraticPath g, CoreGraphicsPath p) {


                CoreGraphicsPath v_p = new CoreGraphicsPath();
                v_p.AddDefinition(g.Points, g.PointTypes);

                //  v_p.FlipY(g.GetBound());
                var H = this.Document.Height;
                var b = g.GetBound();
                Vector2f endLoc = Vector2f.Zero;
                endLoc = new Vector2f(b.X , b.Y);
                Matrix m = new Matrix();
                m.Translate(-endLoc.X, -endLoc.Y, enuMatrixOrder.Append);
                m.Scale(1, -1, enuMatrixOrder.Append );
                m.Translate(endLoc.X,H -endLoc.Y, enuMatrixOrder.Append);


                //var pt = CoreMathOperation.MultMatrixTransformVector(m, g.Points);
                v_p.Transform(m);

                //this.Scale(-1, 1, endLoc, enuMatrixOrder.Append, false);


                p.AddDefinition(v_p.PathPoints, v_p.PathTypes);
            }

            public void Visit(ICore2DDrawingLayeredElement g, CoreGraphicsPath p)
            {
                var H = this.Document.Height;
                using (ICore2DDrawingLayeredElement a = g.Clone() as ICore2DDrawingLayeredElement)
                {

                    //Invert AXES
                    Rectanglef b = a.GetBound();
                    Vector2f endLoc = Vector2f.Zero;
                    endLoc = new Vector2f(b.X, b.Y);
                    a.Translate(-endLoc.X, -endLoc.Y, enuMatrixOrder.Append);
                    a.Scale(1, -1,  enuMatrixOrder.Append);
                    a.Translate(endLoc.X, H - endLoc.Y, enuMatrixOrder.Append);

                 //   a.FlipY();
                    var v_p = a.GetPath();
                    p.AddDefinition(v_p.PathPoints, v_p.PathTypes);
                }
            }

            public void Visit(ImageElement img, CoreGraphicsPath g) {
            }
            public void Visit(RectangleElement rc, CoreGraphicsPath p) {
                var cp = rc.GetPath().Clone() as CoreGraphicsPath ;
                cp.Invert();
                p.AddDefinition(cp.PathPoints, cp.PathTypes);
            }

            
}
        protected override bool PerformAction()
        {

            if (this.CurrentSurface is ICore2DDrawingSurface s) {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "true type font | *.ttf";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        GenerateFont(sfd.FileName, s);

                    }
                }

            }


            return base.PerformAction();
        }

        private void GenerateFont(string fileName, ICore2DDrawingSurface s)
        {
            var fname = Path.GetFileName(fileName);
            var dir = Path.GetDirectoryName(fileName);

            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch
                {
                    //PrivateFontCollection
                    MessageBox.Show("msg.failedtogeneratefont".R(), "title.error".R());
                    return;
                }
            }
            int i = 0;
            //to preserv the first glyf
            Vector2f[] points = null;
            byte[] types = null;
            CoreGraphicsPath p = new CoreGraphicsPath();
            Visitor visitor = new Visitor();
            int glyf = 0;
            WOFFFileInfo winf = new WOFFFileInfo();
            int width = 100;
            int height = 100;
            foreach (ICore2DDrawingDocument doc in s.Documents)
            {
                visitor.Document = doc;
                p.Reset();
                width = Math.Max(width, doc.Width);
                height = Math.Max(height, doc.Height);
                bool quadric = false;

                //visit every layer and get path element
                foreach (ICore2DDrawingLayer l in doc.Layers)
                {
                    foreach (ICore2DDrawingElement g in l.Elements)
                    {
                        if (g is ICoreQuadraticPath )
                        {
                            var m = g as ICoreQuadraticPath;

                            CoreGraphicsPath v_p = new CoreGraphicsPath();
                            v_p.AddDefinition(m.Points, m.PointTypes);

                            //  v_p.FlipY(g.GetBound());
                            var H = doc.Height;
                            var b = m.GetBound();
                            Vector2f endLoc = Vector2f.Zero;
                            endLoc = new Vector2f(b.X, b.Y);
                            Matrix mmm = new Matrix();
                            mmm.Translate(-endLoc.X, -endLoc.Y, enuMatrixOrder.Append);
                            mmm.Scale(1, -1, enuMatrixOrder.Append);
                            mmm.Translate(endLoc.X, H - endLoc.Y, enuMatrixOrder.Append);


                            //var pt = CoreMathOperation.MultMatrixTransformVector(m, g.Points);
                            v_p.Transform(mmm);

                            //this.Scale(-1, 1, endLoc, enuMatrixOrder.Append, false);


                            //p.AddDefinition(v_p.PathPoints, v_p.PathPoints);

                            winf.Glyfs.Add(i, v_p.PathPoints, m.PointTypes, false);
                            i++;
                            glyf++;
                            quadric = true;
                            break;
                        }
                        else
                        {
                            visitor.Visit(g, p);
                        }
                    }
                    if (quadric)
                        break;
                }
                if (quadric) continue;

                if (p.PathPoints.Length > 0)
                {
                    winf.Glyfs.Add(i, p.PathPoints, p.PathTypes, false);
                    if (i == 0)
                    {
                        points = p.PathPoints;
                        types = p.PathTypes;
                    }
                    i++;
                    glyf++;
                }
            }
            if (glyf == 0)
                return;


         


            WOFFFile f = WOFFFile.CreateNew(enuWOFFFIleType.TTF);
           

            winf.Names.Add(enuWOFFFileLanguage.En, enuWOFFFileNameID.FontFamily, "IGKDEVFont");
            winf.Names.Add(enuWOFFFileLanguage.En, enuWOFFFileNameID.FontSubFamily, "Regular");
            winf.Names.Add(enuWOFFFileLanguage.En, enuWOFFFileNameID.FullFontName, "IGKDEV Font Regular");//title
            winf.Names.Add(enuWOFFFileLanguage.En, enuWOFFFileNameID.FontIdentifierName, "kgi.font");
            
            winf.Names.Add(enuWOFFFileLanguage.En, enuWOFFFileNameID.Copyright, "@ IGKDEV Font 2017");
            winf.Names.Add(enuWOFFFileLanguage.En, enuWOFFFileNameID.Designer, "C.A.D. BONDJE DOUE");
            winf.Names.Add(enuWOFFFileLanguage.En, enuWOFFFileNameID.UrlDesigner, "http://bondje.igkdev.com/");
            winf.Names.Add(enuWOFFFileLanguage.En, enuWOFFFileNameID.Version,
                "Version 1.0; DrSStudioFont; IGKDEV; Balafon" //version correct format
                                                              //"1.0;Drsstudio Font; sample; test"
                );
            winf.Names.Add(enuWOFFFileLanguage.En, enuWOFFFileNameID.Manufacturer, "IGKDEV");
            winf.Names.Add(enuWOFFFileLanguage.En, enuWOFFFileNameID.SampleText, "HEllo From space");
            winf.Names.Add(enuWOFFFileLanguage.En, enuWOFFFileNameID.LicenseURi, "http://igkdev.com/fonts/license");

            winf.BoxSize = new WOFFBoxSize(width, height);
            winf.HGlobalAdvanceWidth = width;
            winf.Ascender = (short)height;
            winf.UnitPerEm = (short)height;
                 

         
            //if (!(glyf > 262))
            //{

            //    for (; i < 262; i++)
            //    {
            //        winf.Glyfs.Add(i, points, types, false);
            //    }
            //}
            winf.CharRanges.Clear();
            //winf.CharRanges.Add(0xf000, 0xf000 + 260, 0xFFFF - 0xf000 + 2);
            winf.CharRanges.Add(0x0, 0x0, 0);
            winf.CharRanges.Add(0xf000, (ushort) (0xf000+ winf.Glyfs.Count-2), 0, 1);// 0xFFFF - 0xf000 + 2);

            winf.NumberOfHMetrics = (ushort)winf.Glyfs.Count;
         
            ushort[] v_dindexes = new ushort[winf.Glyfs.Count-1];// winf.Glyfs.Count];
            for (ushort _is = 0; _is < v_dindexes.Length; _is++)
            {
                v_dindexes[_is] = (ushort)(1+_is);
            }

            winf.CharRanges.Indexes = v_dindexes;
            winf.CharRanges.AutoIndex = false;
            //winf.SubSize = new Vector2i(50, 50);
            //winf.SuperSize = new Vector2i(55, 54);
            //winf.StrikeOutPosition = 90;

            f.Save(fileName, winf);
#if DEBUG
            FontEditorUtility.GenerateHtmlDocument(fileName, Path.Combine(dir, "font.doc.html"));
#endif



            //            File.WriteAllText(Path.Combine(dir, "out.html"), @"<html>
            //<head>
            //<style>

            //@font-face{
            //	font-family:'test';
            //	src:url('./dash.ttf') format('truetype');
            //}
            //@font-face{
            //	font-family:'rtest';
            //	src:url('./___output.ttf') format('truetype');
            //}
            //.ftest{
            //     font-family:'rtest', arial, sans-serif;
            //	 font-size:0.5em;
            //}
            //body{
            //background-color:red;
            //}
            //</style>
            //</head>
            //<body style=""font-family:'test', arial, sans-serif; font-size:2em; "">
            //      <div>
            //      The font \xa that empty &#x18a;
            //a b c d e f g j i k
            //l m n o p q r s t u
            //v w x y z
            //&#xf100;
            //&#xf105;
            //</ div >


            //<div class=""ftest"">
            //The font \xa that empty &#x18a;
            //a b c d e f g j i k
            //l m n o p q r s t u
            //v w x y z
            //&#xf100;&#xf105;&#x0001;&#x0000;&#xf105;
            //</div>
            //</body>
            //</html>");


        }

        protected override void InitMenu()
        {
            base.InitMenu();
            

        }
    }
}
