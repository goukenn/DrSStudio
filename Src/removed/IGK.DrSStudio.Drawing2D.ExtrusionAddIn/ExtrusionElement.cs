

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ExtrusionElement.cs
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
file:ExtrusionElement.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Drawing3D;
    using IGK.DrSStudio.WinUI.Configuration;
    [Core2DDrawingSelectionItem("Extrusion", 
        typeof(Mecanism),
        ImageKey="DE_Cube")]
    public sealed class ExtrusionElement :
        Core2DDrawingLayeredDualBrushElement ,
        ICoreCameraOwner
    {
        private Core2DDrawingLayeredDualBrushElement m_Target;
        private Vector2f m_ExtrudeTranslate;
        private Vector2f m_ExtrudeScale;
        private enuExtrudeFace m_ExtrudeFace;
        private List<PolygonFace> m_faces;
        private ICoreBrush m_strokeBorder;
        private ICoreBrush m_fillBorder;
        /// <summary>
        /// get or set the extrude face model
        /// </summary>
        public enuExtrudeFace ExtrudeFace
        {
            get { return m_ExtrudeFace; }
            set
            {
                if (m_ExtrudeFace != value)
                {
                    m_ExtrudeFace = value;
                }
            }
        }
        public override bool CanEdit
        {
            get
            {
                return false;
            }
        }
       [IGK.DrSStudio.Codec.CoreXMLElement (true)]
        public Core2DDrawingLayeredDualBrushElement   Target
        {
            get { return m_Target; }
            set {
                if (this.m_Target != value)
                {
                    if (m_Target !=null)
                        m_Target.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(this.m_Target_PropertyChanged);
                    m_Target = value;
                    if (m_Target != null)
                        m_Target.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(this.m_Target_PropertyChanged);
                }
            }
        }
       void m_Target_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
       {
           switch ((enu2DPropertyChangedType ) e.ID )
           {
               case enu2DPropertyChangedType.BrushChanged :
                   //this.Invalidate(true);
                   WinUI.ICore2DDrawingSurface v_s = this.GetParentSurface();
                   if (v_s != null) v_s.Invalidate();
                   break;
           }
           this.OnPropertyChanged(e);
       }
        [CoreXMLAttribute (true)]
        [CoreXMLDefaultAttributeValue ("0;0")]
        public Vector2f ExtrudeTranslate {
            get {
                return m_ExtrudeTranslate;
            }
            set {
                this.m_ExtrudeTranslate = value;
            }
        }
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue("1.0f;1.0f")]        
        public Vector2f ExtrudeScale {
            get {
                return this.m_ExtrudeScale;
            }
            set {
                this.m_ExtrudeScale = value;
            }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            switch (this.ExtrudeFace)
            { 
                case enuExtrudeFace.Background :
                    return this.m_Target.GetBrush(mode);                    
                case enuExtrudeFace.Border:
                    switch (mode)
                    {
                        case enuBrushMode.Stroke:
                            return this.m_strokeBorder;                            
                        case enuBrushMode.Fill:
                        default :
                            return this.m_fillBorder;
                    }
            }
            return base.GetBrush(mode);
        }
        public override enuBrushSupport BrushSupport
        {
            get
            {
                switch (this.ExtrudeFace)
                {
                    case enuExtrudeFace.Front:
                        break;
                    case enuExtrudeFace.Background:
                        return this.m_Target.BrushSupport ;                       
                }                
                return base.BrushSupport;
            }
        }
         protected override void  ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            xreader.MoveToElement ();
            if (xreader.ReadToDescendant("Target"))
            {
                IXMLDeserializer x = xreader.ReadSubtree();
                    x.Read();                
                    while(x.Read())
                    {
                        if (x.NodeType == System.Xml.XmlNodeType.Element )
                        {
                            this.m_Target = CoreSystem.CreateWorkingObject(x.Name)
                                as Core2DDrawingLayeredDualBrushElement;
                        if (this.m_Target != null)
                            this.m_Target.Deserialize (xreader);
                        break;
                        }
                    }
            }
        }
        protected override void GeneratePath()
        {
            if (this.Target == null) return;
            lock (this)
            {
                GraphicsPath p = Target.GetPath().Clone() as GraphicsPath;
                GraphicsPath p2 = Target.GetPath().Clone() as GraphicsPath;
                //generate polygon face
                if (this.m_faces == null)
                    m_faces = new List<PolygonFace>();
                Matrix m = new Matrix();
                m.Scale(this.ExtrudeScale.X, this.ExtrudeScale.Y, enuMatrixOrder.Append);
                m.Translate(this.ExtrudeTranslate.X, this.ExtrudeTranslate.Y, enuMatrixOrder.Append);
                GraphicsPath v_outPath = new GraphicsPath();
                //Matrix m = new Matrix ();
                v_outPath.SetMarkers();
                v_outPath.AddPath(p, false);
                v_outPath.CloseFigure();
                v_outPath.SetMarkers();
                p2.Transform(m);
                v_outPath.AddPath(p2, false);
                v_outPath.CloseAllFigures();
                v_outPath.FillMode = this.FillMode;
                // p.Dispose();
                foreach (PolygonFace f in m_faces)
                {
                    f.Dispose();
                }
                m_faces.Clear();
                Rectangle rc = Rectangle .Round ( v_outPath.GetBounds());
                this.m_camera.Viewport = rc;// new Rectanglei(0, 0, rc.Width, rc.Height);
                 float v_w = rc.Width / 2.0f;
                float v_h = rc.Height / 2.0f;
                Extrude(this, m_faces, p, m);
                this.m_faces.Sort(new PolygonFace.PolygonFaceComparer());
                this.UpdateBorderBrush();
                this.SetPath(v_outPath);
            }
        }
        public static void Extrude(ICoreCameraOwner owner ,  List<PolygonFace> faces, 
            GraphicsPath path, 
            Matrix m)
        {
            GraphicsPath v_cp = path.Clone() as GraphicsPath;
            v_cp.Transform(m);
            Vector2f[] pt1 = path.PathPoints;
            Vector2f[] pt2 = v_cp.PathPoints;
            PolygonFace face = null;
            for (int i = 0; i < pt1.Length; i++)
            {
                if (i == (pt1.Length - 1))
                {
                    face = new PolygonFace(owner,
                        owner.MapWindowCordinate(new Vector3f[] {
                        new  Vector3f(pt1[i].X, pt1[i].Y , -1),
                        new  Vector3f(pt2[i].X, pt2[i].Y , 1),
                        new  Vector3f(pt2[0].X, pt2[0].Y , 1),
                        new  Vector3f(pt1[0].X, pt1[0].Y , -1)                        
                    }));
                }
                else
                {
                    face = new PolygonFace(owner,
                        owner.MapWindowCordinate(new Vector3f[] {
                        new  Vector3f(pt1[i].X, pt1[i].Y , -1),
                        new  Vector3f(pt2[i].X, pt2[i].Y , 1),
                        new  Vector3f(pt2[i+1].X, pt2[i+1].Y , 1),
                        new  Vector3f(pt1[i+1].X, pt1[i+1].Y , -1)                        
                    }));
                }
                faces.Add(face);
            }
        }
        public static Vector2f[][] Extrude( GraphicsPath path, Matrix m)
        {
            GraphicsPath v_cp = path.Clone() as GraphicsPath;
            v_cp.Transform(m);
            Vector2f[] pt1 = path.PathPoints;
            Vector2f[] pt2 = v_cp.PathPoints;
            float v_angle = CoreMathOperation.GetAngle(pt1[0], pt2[0])*CoreMathOperation.ConvRdToDEGREE ;
            List<Vector2f[]> polygon = new List<Vector2f[]>();
            for (int i = 0; i < pt1.Length ; i++)
            {
                if (i == (pt1.Length - 1))
                {
                    polygon.Add(new Vector2f[] { 
                        pt1[i],pt2[i],pt2[0],pt1[0]
                    });
                }
                else
                polygon.Add(new Vector2f[] { 
                pt1[i],pt2[i],pt2[i+1],pt1[i+1]
                });
            }
            v_cp.Dispose();
            polygon.Sort(new OrderPolygon());
            return polygon.ToArray();
        }
        public override bool Contains(Vector2f position)
        {
            GraphicsPath v_path = this.GetPath();
            if (v_path.IsVisible(position))
                return true;
            GraphicsPathIterator i = new GraphicsPathIterator(v_path);
            i.Rewind();
            GraphicsPath v_pathSection = new GraphicsPath();
            i.NextMarker(v_pathSection);
            for (int j = 1; j < i.SubpathCount - 1; j++)
            {
                i.NextMarker(v_pathSection);
                if (v_pathSection.IsVisible(position))
                    return true;
            }
            i.NextMarker(v_pathSection);
            if (v_pathSection.IsVisible(position))
                return true;
            i.Dispose();
            return base.Contains(position);
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            IGK.DrSStudio.WinUI.ICoreParameterConfigCollections p =  base.GetParameters(parameters);
            IGK.DrSStudio.WinUI.ICoreParameterGroup group =  p.AddGroup("ExtrusionProperties");
            group.AddItem("ScaleX", "lb.scaleX.caption", this.m_ExtrudeScale.X, enuParameterType.SingleNumber , ParamChanged);
            group.AddItem("ScaleY", "lb.scaleY.caption", this.m_ExtrudeScale.Y, enuParameterType.SingleNumber, ParamChanged);
            group.AddItem(GetType().GetProperty("ExtrudeFace"));
            return p;
        }
        private void ParamChanged(Object sender, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            if (e.Value == null)
                return;
            float f = 0.0f;
            if (float.TryParse (e.Value.ToString(), out f))
            {
                switch (e.Item.Name)
                { 
                    case "ScaleX":
                        this.m_ExtrudeScale = new Vector2f(f, this.m_ExtrudeScale.Y);
                        this.InitElement();
                        this.Invalidate(true);
                        break;
                    case "ScaleY":
                        this.m_ExtrudeScale = new Vector2f( this.m_ExtrudeScale.X, f);
                        this.InitElement();
                        this.Invalidate(true);
                        break;
                }
            }
        }
        public override void Draw(Graphics g)
        {     
            GraphicsPath v_path = this.GetPath();
            if (v_path == null)
                return;
            //for compositing quality
            GraphicsState s = g.Save();
            this.SetGraphicsProperty (g);
            Brush v_br = this.FillBrush.GetBrush();
            Pen v_pen = this.StrokeBrush.GetPen();
            //if (v_br!=null) g.FillPath(v_br, v_path);
            //if (v_pen!=null) g.DrawPath(v_pen, v_path);
            GraphicsPathIterator v_iterator = new GraphicsPathIterator(v_path );
            bool faces = true;
            using (GraphicsPath v_outpath = new GraphicsPath())
            {
                int v_index = 0;
                for (int i = 0; i < v_iterator.SubpathCount; i++)
                {
                    v_outpath.Reset();
                    v_index = v_iterator.NextMarker(v_outpath);
                    if (i == 0)
                    {
                        if (v_br != null) g.FillPath(v_br, v_outpath);
                        if (v_pen != null) g.DrawPath(v_pen, v_outpath);
                    }
                    else {
                        v_br = this.m_Target.FillBrush.GetBrush();
                        v_pen = this.m_Target.StrokeBrush.GetPen();
                        if (v_br != null) g.FillPath(v_br, v_outpath);
                        if (v_pen != null) g.DrawPath(v_pen, v_outpath);
                    }
                    if (faces)
                    {
                        if (this.m_faces != null)
                        {
                            GraphicsState s1 = g.Save();
                            Matrix m = this.GetMatrix();
                            g.MultiplyTransform(m, enuMatrixOrder.Prepend);
                            foreach (PolygonFace face in this.m_faces)
                            {
                                face.Draw(g);
                            }
                            g.Restore(s1);
                        }
                        faces = false;
                    }
                }
            }
            v_iterator .Dispose (); 
            g.Restore(s); 
        }
        //.ctr
        public ExtrusionElement()
        {
            this.m_ExtrudeScale = new Vector2f(1.0f, 1.0f);
            this.m_ExtrudeTranslate = new Vector2f(0.0f, 0.0f);
            this.m_fillBorder = new CoreBrush(this);
            this.m_strokeBorder = new CorePen(this);
            this.m_camera = new CoreCamera();
            this.m_strokeBorder.BrushDefinitionChanged += new EventHandler(m_strokeBorder_BrushDefinitionChanged);
            this.m_fillBorder.BrushDefinitionChanged += new EventHandler(m_fillBorder_BrushDefinitionChanged);
        }
        void m_fillBorder_BrushDefinitionChanged(object sender, EventArgs e)
        {
            UpdateBorderBrush();
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BrushChanged));       
        }
        private void UpdateBorderBrush()
        {
            if (this.m_faces !=null)
            foreach (PolygonFace face in this.m_faces)
            {
                face.FillBrush.Copy(this.m_fillBorder);
                face.StrokeBrush.Copy(this.m_strokeBorder);             
            }
        }
        void m_strokeBorder_BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.UpdateBorderBrush();
            OnPropertyChanged (new Core2DDrawingElementPropertyChangeEventArgs (enu2DPropertyChangedType.BrushChanged ));
        }
        #region ICoreFillModeElement Members
        FillMode m_fillMode;
        public FillMode FillMode
        {
            get
            {
                return this.m_fillMode;
            }
            set
            {
                if (this.m_fillMode != value)
                {
                    this.m_fillMode = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        #endregion
        #region ICoreEnabledTensionElement Members
        private bool m_enableTension;
        public bool EnableTension
        {
            get
            {
                return this.m_enableTension;
            }
            set
            {
                if (this.m_enableTension != value)
                {
                    this.m_enableTension = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs .Definition);
                }
            }
        }
        #endregion
        new class Mecanism : Core2DDrawingLayeredDualBrushElement .Mecanism
        {
            Core2DDrawingLayeredDualBrushElement m_Target;
            new ExtrusionElement Element
            {
                get
                {
                    return base.Element as ExtrusionElement;
                }
                set
                {
                    base.Element = value;
                }
            }
            protected override void OnKeyUp(KeyEventArgs e)
            {
                if (!e.Handled)
                {
                    switch (e.KeyCode)
                    { 
                        case Keys.E :
                            if (this.Element != null)
                            {
                                this.CurrentSurface.ElementToConfigure = this.m_Target;
                                e.Handled = true;
                            }
                            break;
                        case Keys.X:
                            if (this.Element != null)
                            {
                                this.CurrentSurface.ElementToConfigure = this.Element;
                                e.Handled = true;
                            }
                            break;
                    }
                }
                base.OnKeyUp(e);
            }
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                base.OnKeyPress(e);
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                this.StartPoint = e.FactorPoint;
                this.EndPoint = this.StartPoint;
                //1. Select element and extude it
                ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
                ExtrusionElement v_element = null;
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_NONE:
                            case ST_CREATING:
                                m_Target = null;
                                for (int i = l.Elements.Count - 1; i >= 0; i--)
                                {
                                    if (l.Elements[i].Contains(e.FactorPoint) &&
                                        (l.Elements[i] is ICore2DDrawingLayeredElement ) &&
                                        !(l.Elements[i] is ExtrusionElement))
                                    {
                                        m_Target = l.Elements[i] as Core2DDrawingLayeredDualBrushElement ;
                                        break;
                                    }
                                }
                                if (m_Target != null)
                                {
                                    v_element = new ExtrusionElement();
                                    v_element.Target = this.m_Target;
                                    v_element.InitElement();
                                    l.Elements.Remove(m_Target);
                                    this.Element = v_element;
                                    //add element to layer
                                    l.Elements .Add(v_element);
                                    //select that element for edition
                                    l.Select(v_element);
                                    //set the state
                                    this.State = ST_EDITING;
                                }
                                else
                                    this.State = ST_NONE;
                                break;
                            case ST_EDITING:
                                v_element = this.Element;
                                if (v_element != null)
                                {
                                    if (v_element.Contains(e.FactorPoint))
                                    {
                                        this.StartPoint = e.FactorPoint;
                                        this.EndPoint = e.FactorPoint;
                                    }
                                }
                                else
                                {
                                    this.State = ST_NONE;
                                }
                                break;
                        }
                        break;
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_EDITING:
                                if (this.Element != null)
                                {
                                    lock (this.Element)
                                    {
                                        this.EndPoint = e.FactorPoint;
                                        this.Element.ExtrudeTranslate = CoreMathOperation.GetDistanceP(this.EndPoint, this.StartPoint);
                                        this.Element.InitElement();
                                        this.CurrentSurface.Invalidate();
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_EDITING:
                                break;
                        }
                        break;
                }
            }
        }
        class OrderPolygon : System.Collections.IComparer, IComparer<Vector2f[]>
        {
            #region IComparer Members
            public int Compare(object x, object y)
            {
                Vector2f[] pt1 = (Vector2f[])x;
                Vector2f[] pt2 = (Vector2f[])y;
                float an1 = CoreMathOperation.GetAngle(pt1[0], pt1[2]);
                float an2 = CoreMathOperation.GetAngle(pt2[0], pt2[2]);
                return an1.CompareTo(an2);                
            }
            #endregion
            #region IComparer<Vector2f[]> Members
            public int Compare(Vector2f[] x, Vector2f[] y)
            {
                //float an1 = CoreMathOperation.GetAngle(x[0], x[2]) * CoreMathOperation.ConvRdToDEGREE;
                //float an2 = CoreMathOperation.GetAngle(y[0], y[2]) * CoreMathOperation.ConvRdToDEGREE;
                //float tangle1 = (float)Math.Ceiling(CoreMathOperation.GetAngle(x[0], x[1]) * CoreMathOperation.ConvRdToDEGREE);
                //float tdistance = (float)Math.Ceiling(CoreMathOperation.GetDistance (x[0], x[1]));
                //float x1 = x[3].X - x[0].X;
                //float y1 = x[3].Y - x[0].Y;
                //float z1 = 0;
                //float x2 = x[1].X - x[0].X;
                //float y2 = x[1].Y - x[0].Y;
                //float z2 = 0;
                //z2 = (float)(tdistance * Math.Sin(tangle1* CoreMathOperation.ConvDgToRadian ));
                //float vx = (y1 * z2) - (z1 * y2);
                //float vy = (z1 * x2) -(x1 *z2);
                //float vz = (x1 * y2) - (y1*x2);
                //an1 = CoreMathOperation.GetAngle(Point.Empty, new Vector2f(vx, vy)) * CoreMathOperation.ConvRdToDEGREE;
                //x1 = y[3].X - y[0].X;
                //y1 = y[3].Y - y[0].Y;
                //z1 = 0;
                //x2 = y[1].X - y[0].X;
                //y2 = y[1].Y - y[0].Y;
                //vx = (y1 * z2) - (z1 * y2);
                //vy = (z1 * x2) - (x1 * z2);
                //vz = (x1 * y2) - (y1 * x2);
                //an2 = CoreMathOperation.GetAngle(Point.Empty, new Vector2f(vx, vy)) * CoreMathOperation.ConvRdToDEGREE;
                float an1 = CoreMathOperation.GetAngle(x[0], x[2]) * CoreMathOperation.ConvRdToDEGREE;
                float an2 = CoreMathOperation.GetAngle(y[0], y[2]) * CoreMathOperation.ConvRdToDEGREE;
                float tangle1 = (float)Math.Ceiling(CoreMathOperation.GetAngle(x[0], x[1]) * CoreMathOperation.ConvRdToDEGREE);
                // float tangle2 =(float)Math.Ceiling ( CoreMathOperation.GetAngle(y[0], y[1]) * CoreMathOperation.ConvRdToDEGREE);
                //if (tangle1 < 0)
                //    tangle1 = 360 + tangle1;
                an1 = an1 - tangle1;
                an2 = an2 - tangle1;
                if (an1 < 0)
                    an1 = 360 + an1;
                if (an2 < 0)
                    an2 = 360 + an2;
                int r =  an1.CompareTo(an2);
                return r;
            }
            #endregion
        }
        #region ICoreCameraOwner Members
        CoreCamera m_camera;
        public ICoreCamera Camera
        {
            get {
                return m_camera;
            }
        }
        public Vector3f GetWindowCoord(Vector3f point)
        {
            Vector3f v = Matrix4x4f.GluProject(
           point,
           this.m_camera.ModelView,
           this.m_camera.Projection,
           this.m_camera.Viewport);
            return v;
        }
        #endregion
        #region ICoreCameraOwner Members
        public Vector3f[] MapWindowCordinate(Vector3f[] winCoord)
        {
            return Matrix4x4f.GluUnProject(winCoord,
                this.m_camera.ModelView,
                this.m_camera.Projection,
                this.m_camera.Viewport);
        }
        #endregion
    }
}

