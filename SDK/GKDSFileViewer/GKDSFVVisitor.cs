using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.FileViewer
{
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinCore;
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using IGK.ICore.Xml;
    using IGK.ICore.ComponentModel;
    using System.Windows.Forms;
    /// <summary>
    /// used to visit element and render content
    /// </summary>
    class GKDSFVVisitor
    {
        ICoreBrush fillBrush;
        ICoreBrush strokeBrush;
        private bool m_read;
        private CoreGraphicsPath m_p; 
        private Control m_owner;
        private Matrix m_matrix;
        private Rectanglef m_display;

        public GKDSFVVisitor(Control owner){
            this.m_owner = owner;
            this.m_matrix = new Matrix();
            this.m_p = new CoreGraphicsPath();
            this.fillBrush = new CoreBrush(null);
            this.strokeBrush = new CorePen(null);
        }
        public void Render(Graphics g, string filename) {
            
            ICoreGraphics gmodel = CreateGraphics(g);
            gmodel.Clear(Colorf.FromFloat(0.99f));


            if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
                return;
            XmlReader xreader = XmlReader.Create(filename);
            if (xreader == null)
                return;
            
            ///render the first element document
            this.m_read = true;
            var rc = this.m_owner.Bounds;
            rc.Inflate(-10, -10);
            gmodel.DrawRectangle(Colorf.Black, rc.X, rc.Y,rc.Width, rc.Height );
            this.m_display = new Rectanglef(rc.X, rc.Y, rc.Width, rc.Height);

            var v_state = gmodel.Save();
            gmodel.SetClip(this.m_display);
            gmodel.TranslateTransform(rc.X, rc.Y, enuMatrixOrder.Append );
           
            this.Visit(xreader,gmodel, filename );
            gmodel.Restore(v_state);
          
        }

        private void Visit(XmlReader xreader,ICoreGraphics gmodel, string filename)
        {
            try
            {
                while (this.m_read && xreader.Read())
                {
                    switch (xreader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (xreader.Name.ToLower())
	{                                
                                
                                case "project":
                                    xreader.Skip();
                                    continue;
                            case "gkds":
                                case "documents":
                                case "filename":
                                    continue;
        default:
            break;
	}
                            string func = "Visit" + xreader.Name;
                            MethodInfo m = GetType().GetMethod(func);
                            if (m != null)
                            {

                                m.Invoke(this, new object[]{
                                xreader.ReadSubtree(),    
                                gmodel ,
                                filename 
                            });
                            }
                            else
                            {
                                var d = CoreSystem.GetWorkingObjectType(xreader.Name);
                                if (d != null) {
                                    using (var obj = CoreSystem.CreateWorkingObject(xreader.Name) as IDisposable )
                                    {
                                        if (obj is ICore2DDrawingElement)
                                        {
                                            (obj as Core2DDrawingObjectBase) .LoadXml(xreader.ReadOuterXml());
                                            (obj as ICore2DDrawingElement).Draw(gmodel);                                           
                                        }
                                    }
                                
                                }
                                else 
                                CoreLog.WriteDebug("Element not found visited [" + xreader.Name + "]");
                            }
                            break;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                xreader.Close();
            }
        }
        public void VisitLayerDocument(XmlReader xreader, ICoreGraphics g, string filename) {

            xreader.MoveToElement ();
            if (xreader.NodeType == XmlNodeType.None)
                xreader.Read();
            var attribs = xreader.GetAttributesDictionary();
            float w = attribs.ContainsKey("width") ? float.Parse(attribs["width"]) : 400;
            float h = attribs.ContainsKey("height") ? float.Parse(attribs["height"]) : 400;
            var v_state = g.Save();

            this.SetupGraphicsDevice(g);
            Matrix m = new Matrix();
            float ex = this.m_display.Width / w;
            float ey = this.m_display.Height / h;
            ex = ey = Math.Min(ex, ey);
            m.Scale(ex, ey);
            m.Translate((this.m_display.Width - (ex * w)) / 2.0f, (this.m_display.Height - (ey * h)) / 2.0f);
            g.MultiplyTransform(m, enuMatrixOrder.Prepend );

            g.FillRectangle(Colorf.FromFloat(0.3f), new Rectanglef(0, 0, w, h));
            while (xreader.Read())
            {
                switch (xreader.NodeType )
                {
                    case XmlNodeType.Element:
                        this.Visit(xreader.ReadSubtree(), g, filename);
                        break;
                    default:
                        break;
                }
            }
            g.Restore(v_state);
            

            //stop reading
            this.m_read = false;
        }
        public void VisitLayer(XmlReader xreader, ICoreGraphics g, string filename)
        {

            xreader.MoveToElement();
            if (xreader.NodeType == XmlNodeType.None)
                xreader.Read();
            var attribs = xreader.GetAttributesDictionary();
           while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case XmlNodeType.Element:
                        this.Visit(xreader.ReadSubtree(), g, filename);
                        break;
                    default:
                        break;
                }
            }
        }
      
        
        public void VisitCircle(XmlReader xreader, ICoreGraphics g, string filename)
        {
            xreader.MoveToElement();
            if (xreader.NodeType == XmlNodeType.None)
                xreader.Read();
            var attribss =  xreader.GetAttributesDictionary();
            this.initDef(attribss);
            this.m_p.Reset();

            var m = attribss.ContainsKey("model") ? (enuCircleModel)Enum.Parse(typeof(enuCircleModel), attribss["model"]) : enuCircleModel.Circle;
            var r = (float[]) (new CoreFloatArrayTypeConverter()).ConvertFromString(attribss["radius"]);
            var c = (Vector2f)(new CoreVector2fTypeConverter()).ConvertFromString(attribss["center"]);
            if (m == enuCircleModel.Circle)
            {
                foreach (float radius in r)
                {
                    this.m_p.AddEllipse(c, new Vector2f(radius, radius));
                }
            }
            else
            {
                foreach (float radius in r)
                {
                    float v_radius = radius;
                    // (float)(radius * Math.Sqrt(2.0f) / 2.0f);
                    this.m_p.AddRectangle(
                        new Rectanglef(
                            c.X - v_radius,
                            c.Y - v_radius,
                            2 * v_radius,
                            2 * v_radius));
                }
            }
            this.Render(g);

        }

        public void VisitEllipse(XmlReader xreader, ICoreGraphics g, string filename)
        {
            xreader.MoveToElement();
            if (xreader.NodeType == XmlNodeType.None)
                xreader.Read();
            var attribss = xreader.GetAttributesDictionary();
            this.initDef(attribss);
            this.m_p.Reset();

            var m = attribss.ContainsKey("model") ? (enuCircleModel)Enum.Parse(typeof(enuCircleModel), attribss["model"]) : enuCircleModel.Circle;
            var r = (Vector2f[])(new CoreVector2fArrayTypeConverter()).ConvertFromString(attribss["radius"]);
            var c = (Vector2f)(new CoreVector2fTypeConverter()).ConvertFromString(attribss["center"]);
            if (m == enuCircleModel.Circle)
            {
                foreach (Vector2f radius in r)
                {
                    this.m_p.AddEllipse(c,radius);
                }
            }
            else
            {
                foreach (Vector2f radius in r)
                {
                    // (float)(radius * Math.Sqrt(2.0f) / 2.0f);
                    this.m_p.AddRectangle(
                        new Rectanglef(
                            c.X - radius.X,
                            c.Y - radius.Y,
                            2 * radius.X,
                            2 * radius.Y));
                }
            }
            this.Render(g);

        }
        public void VisitRectangle(XmlReader xreader, ICoreGraphics g, string filename)
        {
            var attribss = xreader.GetAttributesDictionary();
            this.initDef(attribss);
            this.m_p.Reset();
            var m = attribss.ContainsKey("Bounds") ? Rectanglef.ConvertFromString(attribss["Bounds"]) : Rectanglef.Empty;           
            this.m_p.AddRectangle(
              m);        
            this.Render(g);
        }
        public void VisitRoundRect(XmlReader xreader, ICoreGraphics g, string filename)
        {
            //var attribss = xreader.GetAttributesDictionary();
            //this.initDef(attribss);
            //this.m_p.Reset();
            //var m = attribss.ContainsKey("Bounds") ? Rectanglef.ConvertFromString(attribss["Bounds"]) : Rectanglef.Empty;

            xreader.MoveToElement();
            if (xreader.NodeType == XmlNodeType.None)
                xreader.Read();
            RoundRectangleElement rc = new RoundRectangleElement();
            rc.LoadXml(xreader.ReadOuterXml());
            //this.m_p.Add (rc.GetPath());
            rc.Draw(g);
            rc.Dispose();

            //this.Render(g);
        }
        public void VisitText(XmlReader xreader, ICoreGraphics g, string filename)
        {
            //var attribss = xreader.GetAttributesDictionary();
            //this.initDef(attribss);
            //this.m_p.Reset();
            //var m = attribss.ContainsKey("Bounds") ? Rectanglef.ConvertFromString(attribss["Bounds"]) : Rectanglef.Empty;

            xreader.MoveToElement();
            if (xreader.NodeType == XmlNodeType.None)
                xreader.Read();
            TextElement rc = new TextElement();
            rc.LoadXml(xreader.ReadOuterXml());
            //this.m_p.Add (rc.GetPath());
            rc.Draw(g);
            rc.Dispose();

            //this.Render(g);
        }
        
        public void VisitGroup(XmlReader xreader, ICoreGraphics g, string filename)
        {
            var attribss = xreader.GetAttributesDictionary();
            this.initDef(attribss);
            this.m_p.Reset();
          
            this.Render(g);
        }
        public void VisitArc(XmlReader xreader, ICoreGraphics g, string filename)
        {
            var attribss = xreader.GetAttributesDictionary();
            this.initDef(attribss);
            this.m_p.Reset();

            this.Render(g);
        }
       
        public void VisitTubeLine(XmlReader xreader, ICoreGraphics g, string filename)
        {
            var attribss = xreader.GetAttributesDictionary();
            this.initDef(attribss);
            this.m_p.Reset();

            this.Render(g);
        }
        public void VisitSpline(XmlReader xreader, ICoreGraphics g, string filename)
        {
            var attribss = xreader.GetAttributesDictionary();
            this.initDef(attribss);
            this.m_p.Reset();

            this.Render(g);
        }
        public void VisitBezier(XmlReader xreader, ICoreGraphics g, string filename)
        {
            var attribss = xreader.GetAttributesDictionary();
            this.initDef(attribss);
            this.m_p.Reset();

            this.Render(g);
        }
        public void VisitPath(XmlReader xreader, ICoreGraphics g, string filename)
        {
            var attribss = xreader.GetAttributesDictionary();
            this.initDef(attribss);
            this.m_p.Reset();

            this.Render(g);
        }
       
        
        private void Render(ICoreGraphics g)
        {
            var s = g.Save();
            this.SetupGraphicsDevice(g);
            g.FillPath(this.fillBrush, this.m_p);
            g.DrawPath(this.strokeBrush as ICorePen, this.m_p);
            g.Restore(s);
        }

        private void SetupGraphicsDevice(ICoreGraphics g)
        {
            g.SmoothingMode = enuSmoothingMode.AntiAliazed;
            g.CompositingMode = enuCompositingMode.Over;
        }

        private void initDef(Dictionary<string, string> attribs)
        {
            this.fillBrush.SetSolidColor(Colorf.White);
            this.strokeBrush.SetSolidColor(Colorf.Black);

            if (attribs.ContainsKey("FillBrush"))
                this.fillBrush.CopyDefinition(attribs["FillBrush"]);
            if (attribs.ContainsKey("StrokeBrush"))
                this.strokeBrush .CopyDefinition(attribs["StrokeBrush"]);
            if (attribs.ContainsKey("FillMode"))
                this.m_p.FillMode = (enuFillMode)Enum.Parse(typeof(enuFillMode), attribs["FillMode"]);
            else
                this.m_p.FillMode = enuFillMode.Alternate;

            if (attribs.ContainsKey("Matrix"))
                this.m_matrix.LoadString(attribs["Matrix"]);
        }
        public static  ICoreGraphics CreateGraphics(Graphics g)
        {
            return WinCoreBitmapDeviceVisitor.Create (g,null);
        }
    }
}
