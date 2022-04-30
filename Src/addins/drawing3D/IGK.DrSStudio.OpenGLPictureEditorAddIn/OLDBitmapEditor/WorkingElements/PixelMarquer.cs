

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PixelMarquer.cs
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
file:PixelMarquer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.OGLBitmapEditor.WorkingElements
{
    
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Codec ;
    /// <summary>
    /// represent a pixel marquer
    /// </summary>
    [Core2DDrawingStandardItem ("PixelMarquer", typeof (Mecanism ))]
    class PixelMarquer : Core2DDrawingLayeredElement, 
        ICoreSerializerAdditionalPropertyService 
    {
        List<Vector2i> m_vectors;
        ICoreBrush m_FillBrush;
        public Vector2i[] Marks {
            get {
                return m_vectors.ToArray();
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public ICoreBrush FillBrush {
            get {
                return this.m_FillBrush;
            }
        }
        public override IGK.DrSStudio.Drawing2D.enuBrushSupport BrushSupport
        {
            get { return IGK.DrSStudio.Drawing2D.enuBrushSupport.FillOnly; }
        }
        protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            Vector2i[] tab = this.m_vectors.ToArray();
            Vector2i.Vector2iConverter  conv = new Vector2i.Vector2iConverter();
            string v_g = conv.ConvertToString(tab);
            xwriter.WriteElementString("Marks", v_g );
            //write value
        }
        protected override void ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {            
            base.ReadElements(xreader);
        }
        public override IGK.DrSStudio.Drawing2D.ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (mode == enuBrushMode.Fill)
                return m_FillBrush;
            return null;
        }
        protected override void GeneratePath()
        {
            this.SetPath(null);
        }
        public PixelMarquer()
        {
            this.m_vectors = new List<Vector2i>();
            this.m_FillBrush = new CoreBrush(this);
            this.m_FillBrush.SetSolidColor(Colorf.Black);
            this.m_FillBrush.BrushDefinitionChanged += new EventHandler(m_FillBrush_BrushDefinitionChanged);
        }
        void m_FillBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged (new Core2DDrawingElementPropertyChangeEventArgs (enu2DPropertyChangedType.BrushChanged ));
        }
        public override void Draw(Graphics g)
        {
            System.Drawing.Drawing2D.GraphicsState s = g.Save();
            this.SetGraphicsProperty(g);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            Rectanglei rc = Rectanglei.Empty;
            System.Drawing.Brush br = this.m_FillBrush.GetBrush();
            foreach (Vector2i  item in this.m_vectors )
            {
                rc = new Rectanglei(item, new Size2i (1,1));
                g.FillRectangle(
                    br,
                    rc);
            }
            g.Restore(s);
        }
        public override Graphics GetShadowPath()
        {
            return null;
        }
        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        class Mecanism : Core2DDrawingMecanismBase
        {
            new PixelMarquer Element{
                get {
                    return base.Element as PixelMarquer;
                }
            }
            protected override void UpdateDrawing(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                base.UpdateDrawing(e);
                PixelMarquer marquer = this.Element;
                Vector2i v = Vector2i.Round(e.FactorPoint);
                Rectanglei rc = new Rectanglei(0,0,this.CurrentSurface.CurrentDocument.Width-1,
                    this.CurrentSurface.CurrentDocument.Height-1);
                if (!marquer.m_vectors.Contains(v) && rc.Contains (v))
                {
                    marquer.m_vectors.Add(v);
                    this.CurrentSurface.Invalidate();
                }
            }
        }
        bool ReadAdditionalInfo(IXMLDeserializer deseri)
        {
            if (deseri.Name == "Marks")
            {
                Vector2f.Vector2fArrayTypeConverter conv = new Vector2f.Vector2fArrayTypeConverter();
                string st = deseri.ReadElementContentAsString();
                Vector2f[] c =  (Vector2f[])conv.ConvertFromString(st);
                Vector2i[] cTab = new Vector2i[c.Length];
                for (int i = 0; i < cTab.Length ; i++)
                {
                    cTab[i] = Vector2i.Round(c[i]);
                }
                this.m_vectors.Clear();
                this.m_vectors.AddRange(cTab);
                return true;
            }
            return false;
        }
        public override void Dispose()
        {
            base.Dispose();
            this.m_FillBrush.Dispose();
        }
        #region ICoreSerializerAdditionalPropertyService Members
        CoreReadAdditionalElementPROC ICoreSerializerAdditionalPropertyService.GetProc()
        {
            return new CoreReadAdditionalElementPROC(ReadAdditionalInfo);
        }
        #endregion
    }
}

