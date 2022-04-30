

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: InkElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:InkElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Ink;
using Microsoft.StylusInput;
using System.Drawing;
using System.IO;
using IGK.ICore;using IGK.DrSStudio.Drawing2D.Codec;
using IGK.DrSStudio.Codec;
namespace IGK.DrSStudio.Drawing2D.Ink
{
    [IGK.DrSStudio.Drawing2D.Core2DDrawingStandardItem("Stylus", typeof(Mecanism),
        ImageKey = "DE_Stylus")]
    class InkElement : IGK.DrSStudio.Drawing2D.Core2DDrawingLayeredElement ,
        ICoreSerializerAdditionalPropertyService
    {
        ICorePen m_strokeBrush;
        Microsoft.Ink.Ink m_ink;
        /// <summary>
        /// get the strokebrush of the ink object
        /// </summary>
        [CoreXMLAttribute]
        [CoreXMLDefaultStroke]
        public ICorePen StrokeBrush { get { return this.m_strokeBrush; } }
        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        public InkElement()
        {
            m_strokeBrush = new CorePen(this);
            this.m_strokeBrush.SetSolidColor(Colorf.Black);
            this.StrokeBrush.BrushDefinitionChanged += new EventHandler(StrokeBrush_BrushDefinitionChanged);
            this.m_ink = new Microsoft.Ink.Ink();
        }
        private void StrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new CoreWorkingObjectPropertyChangedEventArgs(
            (enuPropertyChanged)enu2DPropertyChangedType.BrushChanged));
        }
        //Serialization capability
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xwriter"></param>
        protected override void WriteAttributes(DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteAttributes(xwriter);
        }
        protected override void ReadAttributes(DrSStudio.Codec.IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
        }
        protected override void WriteElements(DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            xwriter.WriteElementString("Data", InkCommon.SaveAsISFToString (this.m_ink ) );
        }
        protected override void ReadElements(DrSStudio.Codec.IXMLDeserializer xreader)
        {
            base.ReadElements(xreader);
        }
        protected override bool ReadNode(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            switch (xreader.Name)
            { 
                case "Data":
                    this.m_ink.DeleteStrokes();
                    this.m_ink.Load ( InkCommon.GetInkDataFromISF(xreader .ReadContentAsString ()));
                    return true;
            }
            return base.ReadNode(xreader);
        }
        public override DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
 	         parameters = base.GetParameters(parameters);
            //Microsoft.Ink.Tablets v_alltablets  = new Tablets ();
            //System.Windows.Forms.MessageBox.Show("Tablet Count  "+v_alltablets.Count);
            return parameters ;
        }
        protected override void GeneratePath()
        {
            this.SetPath(null);
        }
        public override enuBrushSupport BrushSupport
        {
            get { return  enuBrushSupport.StrokeOnly ; }
        }
        public override void Draw(System.Drawing.Graphics g)
        {
            if ((this.m_ink == null) || (this.m_ink.Strokes.Count == 0))
                return;
            System.Drawing.Drawing2D.GraphicsState v_st  = g.Save ();
            this.SetGraphicsProperty(g);
            using (InkOverlay ov = new InkOverlay())
            {
                ov.Ink = this.m_ink;
                DrawingAttributes attr = new DrawingAttributes();
                attr.Color = Color.Aqua;
                Point[] v_tab = null;
                foreach (var stroke in ov.Ink.Strokes)
                {
                    if (stroke.BezierPoints.Length > 1)
                    {
                        v_tab = stroke.GetPoints();
                        ov.Renderer.InkSpaceToPixel(g, ref v_tab);
                            g.DrawLines(this.StrokeBrush.GetPen(),
                                v_tab);
                    }
                }
            }
            g.Restore(v_st);
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (mode == enuBrushMode.Stroke)
                return this.StrokeBrush;
            return null;
        }
        class Mecanism : IGK.DrSStudio.Drawing2D.Core2DDrawingMecanismBase
        {
            InkCollector m_inkCollector;
            InkMecanismPlugin m_mecanismPlugIn;
            public new InkElement Element
            {
                get { return base.Element as InkElement; }
                set
                {
                    base.Element = value;
                }
            }
            public Mecanism(){
                m_mecanismPlugIn = new InkMecanismPlugin(this);
            }
            protected override void RegisterSurface(WinUI.ICore2DDrawingSurface surface)
            {
                base.RegisterSurface(surface);
                m_inkCollector = new InkCollector(surface.Handle);
                m_inkCollector.DefaultDrawingAttributes.Transparency = 205;
                m_inkCollector.DefaultDrawingAttributes.Color = Color.Black;
                //enabled capture
                m_inkCollector.Enabled = true;
                m_inkCollector.Stroke += m_inkCollector_Stroke;
                m_inkCollector.NewPackets += m_inkCollector_NewPackets;
                m_inkCollector.MouseDown += m_inkCollector_MouseDown;
                m_inkCollector.MouseUp += m_inkCollector_MouseUp;
            }
            void m_inkCollector_MouseUp(object sender, CancelMouseEventArgs e)
            {
               // ClearStrokes();
            }
            void m_inkCollector_MouseDown(object sender, CancelMouseEventArgs e)
            {
               // ClearStrokes();
            }
            void m_inkCollector_NewPackets(object sender, InkCollectorNewPacketsEventArgs e)
            {
                //    if (e.Stroke.BezierPoints.Length  > 0)
                //    {
                //        InkCollector c = sender as InkCollector ;
                //        List<Point> mpts = new List<Point>();
                //        TabletPropertyDescription prop = new TabletPropertyDescription(
                //            Guid.NewGuid(),
                //            new TabletPropertyMetrics());
                //        foreach (Point item in e.Stroke.BezierPoints)
                //        {
                //            mpts.Add(Vector2i.Round(CurrentSurface.GetFactorLocation(item)));
                //        }
                //        Strokes v = l.m_ink.CreateStrokes();
                //        v.
                //        v.Add(e.Stroke);
                //        //Stroke s = l.m_ink.CreateStroke(e.PacketData,c.Tnull);//mpts.ToArray());
                //    }
                //}
            }
            void m_inkCollector_Stroke(object sender, InkCollectorStrokeEventArgs e)
            {
                InkElement l = this.Element;
                if (l == null)
                    return;
                using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    foreach (Stroke stroke in this.m_inkCollector.Ink.Strokes)
                    {
                        Point[] tab = stroke.GetPoints();
                        this.m_inkCollector.Renderer.InkSpaceToPixel(g, ref tab);
                        for (int i = 0; i < tab.Length; i++)
                        {
                            tab[i] = Vector2i.Round(this.CurrentSurface.GetFactorLocation(new Vector2f(tab[i].X, tab[i].Y)));
                        }
                        this.m_inkCollector.Renderer.PixelToInkSpace(g, ref tab);
                        //e.Stroke.SetPoints(tab);
                        l.m_ink.CreateStroke(tab);
                        this.CurrentSurface.Invalidate();
                    }
                }
                ClearStrokes();
                UpdateInk();
            }
            private void UpdateInk()
            {
                this.CurrentSurface.Invalidate();
                //InkElement l = this.Element;
                //if (l != null)
                //{
                //    byte[] t = this.m_inkCollector.Ink.Save();
                //    if (t.Length > 0)
                //    {
                //        l.m_ink = this.m_inkCollector.Ink.Clone() as Microsoft.Ink.Ink;
                //        this.Element.InitElement();
                //        this.CurrentSurface.Invalidate();
                //    }
                //}
            }
            protected override void UnRegisterSurface(WinUI.ICore2DDrawingSurface surface)
            {
                m_inkCollector.Enabled = false;
                m_inkCollector.Dispose();
                base.UnRegisterSurface(surface);
            }
            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.DrawString("Render with Ink", this.CurrentSurface.Font, CoreBrushRegister.GetBrush(Colorf.Black), Point.Empty);
            }
            private void ClearStrokes()
            {
                if (!this.m_inkCollector.CollectingInk)
                {
                    this.m_inkCollector.Enabled = false;
                    //this.m_inkCollector.Ink.Strokes.Clear();
                    this.m_inkCollector.Ink.DeleteStrokes(this.m_inkCollector.Ink.Strokes);
                    this.m_inkCollector.Enabled = true;
                }
            }
            protected override void OnElementChanged(CoreElementChangedEventArgs<ICoreWorkingObject> e)
            {
                base.OnElementChanged(e);           
                InkElement l = this.Element;
                if (l != null)
                {
                    //init element 
                    this.ClearStrokes();
                    l.m_ink = this.m_inkCollector.Ink.Clone() as Microsoft.Ink.Ink;
                    this.Element.InitElement();
                }
            }
            protected override void InitNewCreateElement(ICore2DDrawingElement element)
            {
                InkElement l = element as InkElement;
                if (l != null)
                {
                    l.m_strokeBrush.Copy(this.CurrentSurface.StrokeBrush);
                }
                base.InitNewCreateElement(element);
            }
            protected override void UpdateDrawing(WinUI.CoreMouseEventArgs e)
            {
                base.UpdateDrawing(e);
            }
            protected override void OnMouseDown(WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseDown(e);
            }
            protected override void OnMouseMove(WinUI.CoreMouseEventArgs e)
            {
                //base.OnMouseMove(e);
            }
            protected override void OnMouseUp(WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseUp(e);
            }
            public override void EndEdition()
            {
                base.EndEdition();
            }
        }
        class InkMecanismPlugin : InkSinkPlugin 
        {
            Mecanism m_mecanism;
            public InkMecanismPlugin(Mecanism mecanims)
            {
                this.m_mecanism = mecanims;
            }
            public override void Packets(RealTimeStylus sender, Microsoft.StylusInput.PluginData.PacketsData data)
            {
            }
        }
    }
}

