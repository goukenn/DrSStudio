
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IGK.DrSStudio.Editor.FontEditor.WinUI
{
    using IGK.DrSStudio.WinUI;
    using IGK.ICore;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.Drawing2D.WorkingObjects.Standard;
    using IGK.ICore.IO.Font;
    using IGK.ICore.Web;
    using System.IO;

    [ComVisible(true)]
    public class FontEditorScriptObject : CoreWebScriptObjectBase
    {
        private FontEditorSurface fontEditorSurface;
        public ushort[] List;
        private CoreFontFile m_gfont;

        internal FontEditorScriptObject() {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fontEditorSurface"></param>
        public FontEditorScriptObject(FontEditorSurface fontEditorSurface)
        {
            this.fontEditorSurface = fontEditorSurface;
        }

        private CoreFontFile getFontParse() {
            string v_fname = fontEditorSurface.FontFileName;
            if (m_gfont == null)
                m_gfont = CoreFontFile.FontParser(v_fname, true);
            return m_gfont;

            
        }
        /// <summary>
        /// edit the selected font glyf code
        /// </summary>
        /// <param name="code">unicode value of font to edit</param>
        public void fe_edit(int code) {


            var ft = this.fontEditorSurface?.SelectedFont;
            if (ft == null) {
                return;
            }
            string v_fname = fontEditorSurface.FontFileName;

            object o = getFontParse().GetGlyph((char)code);
            if (o != null)
            {
                Vector2f[] v_pts;
                byte[] c;
                // int ii = ((CoreFontGlyphInfo)o).ExtractGlyphGraphics(out v_pts, out c);
                int i = ((CoreFontGlyphInfo)o).ExtractQuadraticPoint(out v_pts, out c);

                if (!((v_pts == null) || (c.Length == 0)))
                {
                    //extract as quadratic
                    v_pts = CoreMathOperation.FlipY(v_pts);
                    QuadraticElement p = QuadraticElement.CreateElement(v_pts, c);
                    // p.FlipY();

                    var v_rc = p.GetBound();
                    Core2DDrawingLayerDocument doc = new Core2DDrawingLayerDocument(v_rc.Width, v_rc.Height);
                    p.Translate(-v_rc.X, -v_rc.Y, enuMatrixOrder.Append);
                    doc.CurrentLayer.Elements.Add(p);


                    //   Core2DDrawingLayerDocument document = Core2DDrawingLayerDocument.CreateForm(p);
                    CoreDecoder.Instance.OpenDocument(
                        fontEditorSurface.Workbench,
                        doc);
                    return;
                }
            }
            //let gdi define glyf ... but for generating must be quadratic
            GraphicsPath pm = new GraphicsPath();
            try
            {
                using (var rft = new Font(ft, FontStyle.Regular))
                {
                    pm.AddString(((char)code).ToString(), rft.FontFamily, 0, 100, Point.Empty, new StringFormat());
                    List<Vector2f> PTS = new List<Vector2f>();
                    byte[] types = pm.PathTypes;
                    for (int i = 0; i < pm.PathPoints.Length; i++)
                    {
                        PTS.Add(new Vector2f(pm.PathPoints[i].X, pm.PathPoints[i].Y));

                    }
                    PathElement p = PathElement.CreateElement(PTS.ToArray(), types);
                    Core2DDrawingLayerDocument document = Core2DDrawingLayerDocument.CreateForm(p);
                    CoreDecoder.Instance.OpenDocument(
                        fontEditorSurface.Workbench,
                        document);
                }
            }
            catch (Exception ex) {
                CoreLog.WriteDebug(ex.Message);
            }
        }

        public string fe_getpage(int p) {
            var list = this.List;
            int page = list.Length / 1000;
            int start = ((p - 1) * 1000);
            int pp = Math.Min(list.Length, p * 1000);
            var data = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("div");
            data.setId("data");
            var rp = data.addDiv().setClass("igk-row");
            for (int i = start; i < pp; i++)
            {
                var t = ((int)list[i]).ToBase(16, 4);
                var span = rp.addDiv().setClass("floatl tblock").
                // setAttribute("style", "border:1px solid #eee; overflow:hidden; padding: 4px;").
                add("span");

                span.addA("#").setAttribute("onclick", "javascript: if (window.external)window.external.fe_edit(" + list[i] + "); return false; ").setContent("&#x" + t + ";");
                span.getParentNode().addDiv()
                .setAttribute("class", "v").setContent("&amp;#x" + t + "");//"&#x".IGKNumber::ToBase(i, 16).";";
            }
            var dv = data.addDiv("pager")
                .setClass("igk-row")
                .setAttribute("style", "font-size:14px");
            for (int i = 1; i <= page; i++)
            {
                dv.addDiv().setClass("floatl").addA("#")
                    .setAttribute("page", i.ToString())
                    .setClass("igk-btn")
                    .setAttribute("onclick", "javascript: alert('a clicked'); return false;")
                    .Content = i;
            }
            dv.addScript().Content = @"(function(){
var s = $igk('#pager'); 
var q = s.getItemAt(0); q.select('a')
var al = q.select('a');
al.each(function(){
this.reg_event('click', function(evt){
evt.preventDefault();
if (!window.external){
    return;
}
var s = window.external.fe_getpage(this.getAttribute('page'));
var m = $igk('#data');
m.getItemAt(0).replaceBy(s); 
m = $igk('#data').getItemAt(0);
if (m)
    igk.ajx.fn.initnode(m.o);
});  
//end each
return true; 
}); })();";

            return data.RenderXML(null);
        }

        public void fe_editall() {
            var ft = this.fontEditorSurface?.SelectedFont;
            if (ft == null)
            {
                return;
            }
            string v_fname = fontEditorSurface.FontFileName;

            Vector2f[] v_pts;
            byte[] c;
            // int ii = ((CoreFontGlyphInfo)o).ExtractGlyphGraphics(out v_pts, out c);
            //i = ((CoreFontGlyphInfo)o).ExtractQuadraticPoint(out v_pts, out c);

            CoreFontFile o = CoreFontFile.FontParser(v_fname, true);
            List<ICore2DDrawingDocument> docs = new List<ICore2DDrawingDocument>();

            var vs = CoreSystem.CreateWorkingObject(CoreConstant.DRAWING2D_SURFACE_TYPE) as ICore2DDrawingSurface;

            if (vs != null)

                for (int i = 0; i < o.NumberOfGlyphs; i++)
                {
                    try
                    {
                        var ii = ((CoreFontGlyphInfo)o.GetGlyph(i)).ExtractQuadraticPoint(out v_pts, out c);
                        v_pts = CoreMathOperation.FlipY(v_pts);


                        QuadraticElement p = QuadraticElement.CreateElement(v_pts, c);
                        // p.FlipY();
                        var v_rc = p.GetBound();
                        if (v_rc.IsEmpty)
                            continue;

                        Core2DDrawingLayerDocument doc = new Core2DDrawingLayerDocument(v_rc.Width, v_rc.Height);
                        p.Translate(-v_rc.X, -v_rc.Y, enuMatrixOrder.Append);
                        p.ResetTransform();
                        doc.CurrentLayer.Elements.Add(p);

                        vs.Documents.Add(doc);
                    }
                    catch (Exception ex){
                        CoreLog.WriteLine("Some error : "+ex.Message);
                    }
                }
            vs.Documents.Remove(vs.CurrentDocument);
             CoreSystem.Instance.Workbench.AddSurface (
                vs, true);
        }
    }

}

