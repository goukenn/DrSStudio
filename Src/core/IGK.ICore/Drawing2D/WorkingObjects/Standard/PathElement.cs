

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PathElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:PathElement.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.ComponentModel;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent a path element.
    /// </summary>
    [Core2DDrawingStandardElement ("Path", typeof(Mecanism), IsVisible = false )]
    public class PathElement :
        Core2DDrawingDualBrushElement,
        ICorePathElement,
        ICore2DFillModeElement,
        ICore2DClosableElement,
        ICorePathStringDefinition 
        
    {
        private Vector2f[]  m_Points;
        private byte[] m_PointTypes;
        private enuFillMode m_FillMode;
        private bool m_Closed;

   
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.Matrix;
            if (m.IsIdentity)
                return;
            this.m_Points = CoreMathOperation.TransformVector2fPoint(m, m_Points);
            base.BuildBeforeResetTransform();
        }
        public override bool Contains(Vector2f point)
        {
            return base.Contains(point);
        }
        public override bool IsOutiliseVisible(Vector2f point)
        {
            return base.IsOutiliseVisible(point);
        }
        [CoreXMLDefaultAttributeValue(false)]
        [CoreXMLAttribute ()]
        public bool Closed
        {
            get { return m_Closed; }
            set
            {
                if (m_Closed != value)
                {
                    m_Closed = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLDefaultAttributeValue(enuFillMode.Alternate )]
        public enuFillMode FillMode
        {
            get { return m_FillMode; }
            set
            {
                if (m_FillMode != value)
                {
                    m_FillMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        [CoreXMLElement ()]
        public byte[] PointTypes
        {
            get { return m_PointTypes; }
            set
            {
                if (m_PointTypes != value)
                {
                    m_PointTypes = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLElement ()]
        public Vector2f[]  Points
        {
            get { return m_Points; }
            set
            {
                if (m_Points != value)
                {
                    m_Points = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public PathElement():base()
        {
            
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_FillMode = enuFillMode.Alternate;
            this.m_Closed = false;
        }

        public void SetDefinition(CoreGraphicsPath path)
        {
            Vector2f[] t = null;
            Byte[] r = null;
            path.GetAllDefinition(out t, out r);
            this.SetDefinition(t, r);
        }

        public void SetDefinition(Vector2f[] points, Byte[] pointTypes)
        {

            if ((points!=null) && (pointTypes!=null) && (points.Length == pointTypes.Length))
            {
                this.m_Points = points;
                this.m_PointTypes = pointTypes;
                this.Matrix.Reset();
                OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        public static PathElement CreateElement(Vector2f[] points, Byte[] definition)
        {
            if ((points==null) || (definition == null) || (points.Length != definition .Length))
                return null;
            PathElement p = new PathElement();
            p.m_Points = points;
            p.m_PointTypes = definition;
            p.InitElement();
            return p;
        }
        public static PathElement CreateElement(ICoreGraphicsPath vpath)
        {
            if (vpath == null) return null;
            Vector2f[] v_p = null;
            byte[] v_b = null;
            vpath.GetAllDefinition(out v_p, out v_b);
            PathElement p = new PathElement();
            p.m_Points = v_p;
            p.m_PointTypes = v_b;
            p.InitElement();
            return p;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            if ((this.m_Points == null) || (this.m_PointTypes == null))
                return;


            path.Reset();
            if((this.m_Points?.Length> 0 )
            && (this.m_PointTypes?.Length == this.m_Points .Length)){
                //disable closings
                this.m_PointTypes[this.m_PointTypes.Length - 1] &= 0x7F;
                path.AddPath(this.Points, this.PointTypes);
                path.FillMode = this.FillMode;
            }
            if (this.Closed)
            {
                path.CloseFigure();
            }
           
        }

        public void ReversePath() {
            CorePathUtils.ReversePath(this.GetPath(), out Vector2f[] pts, out byte[] types);
            this.SetDefinition(pts, types);
        }


        protected override void WriteElements(IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
        }
        protected override void ReadElements(IGK.ICore.Codec.IXMLDeserializer xreader)
        {
            if (xreader.NodeType == XmlNodeType.Attribute)
                xreader.MoveToElement();
            IXMLDeserializer xr = xreader.ReadSubtree();
            TypeConverter v_conv = CoreTypeDescriptor.GetConverter(typeof(Vector2f));
            try
            {
                //move to childs
                xr.Read();
                while (xr.Read())
                {
                    switch (xr.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (xr.Name)
                            {
                                case "Points":
                                    {
                                        string sm = xr.ReadString(); //xr.ReadElementContentAsString();
                                       if (!string.IsNullOrEmpty(sm))
                                        {
                                            object obj = v_conv.ConvertFromString(sm);
                                            if ((obj!=null) && obj.GetType().IsArray)
                                            {
                                                if (obj is Vector2f[])
                                                {
                                                    this.m_Points = (Vector2f[])obj;
                                                }
                                                else if (obj is Vector2f[])
                                                {
                                                    Vector2f[] t = (Vector2f[])obj;
                                                    m_Points  = new Vector2f[t.Length];
                                                    for (int i = 0; i < t.Length; i++)
                                                    {
                                                        //implicit copy
                                                        m_Points[i] = t[i];
                                                    }
                                                }
                                            }
                                            else
                                                this.m_Points = new Vector2f[] { (Vector2f)obj };
                                        }
                                    }
                                    break;
                                case "PointTypes":
                                    {
                                        string v_str = xr.ReadString();
                                        if (!string.IsNullOrEmpty(v_str))
                                        {
                                            string[] sm = v_str.Split(';');
                                            byte[] t = new byte[sm.Length];
                                            for (int i = 0; i < sm.Length; i++)
                                            {
                                                t[i] = byte.Parse(sm[i]);
                                            }
                                            this.m_PointTypes = t;
                                        }
                                    }
                                    break;
                                default:
                                    System.Reflection.PropertyInfo v_pr = GetType().GetProperty(xreader.Name);
                                    if ((v_pr != null) &&
                                     IGK.ICore.Codec.CoreXMLElementAttribute.IsCoreXMLElementAttribute(v_pr))
                                    {
                                        IGK.ICore.Codec.CoreXMLSerializerUtility.SetElementProperty(xreader, v_pr, this, null);
                                    }
                                    break;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug(ex.Message);
            }
            //this.GetMatrix().Invert();//.Reset();
            this.InitElement();
        }
        /// <summary>
        /// add path definition to this element
        /// </summary>
        /// <param name="v_points"></param>
        /// <param name="v_defs"></param>
        public void AddDefinition(Vector2f[] v_points, byte[] v_defs)
        {
            if (!((v_points?.Length>0) && (v_defs.Length == v_points.Length)))
                return;

            var t  = new Vector2f[this.m_Points.Length + v_points.Length];
            var c = new byte[this.m_PointTypes.Length + v_defs.Length];

            Array.Copy(this.m_Points, t, this.m_Points.Length);
            Array.Copy(v_points, 0, t, this.m_Points.Length, v_points.Length);

            Array.Copy(this.m_PointTypes, c, this.m_PointTypes.Length);
            Array.Copy(v_defs, 0, c, this.m_PointTypes.Length, v_defs.Length);


            this.m_Points = t;
            this.m_PointTypes = c;


            this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            
        }

        /// <summary>
        /// mecanism used to update path element properties
        /// </summary>
        public new sealed class Mecanism : IGK.ICore.Drawing2D.Mecanism.Core2DDrawingSurfaceMecanismBase<PathElement>
        {
            private bool m_AllowSnippetView;
            public const int PATHMODE_LINE = 1;//insert line
            public const int PATHMODE_BEZIER = 2;//inser bezier
            public const int PATHMODE_ARC = 3; //insert arc
            public const int PATHMODE_POINT = 0;//insert point

            
            private int m_Mode;
            /// <summary>
            /// get or set the mecanism mode
            /// </summary>
            public int Mode
            {
                get { return m_Mode; }
                set
                {
                    if (m_Mode != value)
                    {
                        m_Mode = value;
                        OnModeChanged(EventArgs.Empty);
                    }
                }
            }
            public event EventHandler ModeChanged;

            private void OnModeChanged(EventArgs eventArgs)
            {
                if (this.ModeChanged != null)
                {
                    this.ModeChanged(this, eventArgs);
                }
            }
            public override void Render(ICoreGraphics device)
            {
                base.Render(device);
                //render hight light segment
                var l = this.Element;
                if (l == null)
                    return;
                bool seg = false;
                Vector2f sp = Vector2f.Zero;
                Vector2f ep = Vector2f.Zero;
                Colorf c = CoreRenderer.BezierCurveSelectionColor;
                bool start = true;
                for (int i = 0; i < l.m_PointTypes.Length; i++) {

                    if ((l.m_PointTypes[i] & 0x3) == 0x3) {

                        if (!seg)
                        {
                            if ((i == 1)||(start))
                            {
                                sp = l.m_Points[i - 1];
                                ep = l.m_Points[i];
                                seg = true;
                                start = false;
                            }
                            else if (i > 1)
                            {
                                sp = l.m_Points[i];
                                seg = true;
                                continue;
                            }
                        }
                        else {
                            ep = l.m_Points[i];
                            start = true;
                        }
                    }
                    if (seg) {
                        device.DrawLine(c, 
                            CurrentSurface.GetScreenLocation( sp),
                            CurrentSurface.GetScreenLocation(ep));
                        seg = false;
                    }
                }




            }
            void ChangeVectorType(int index)
            {
                //change control point type
                byte[] s = this.Element.m_PointTypes;
                int v_state = s[index];
                switch (v_state)
                {
                    case 1:
                        s[index] = 3;
                        break;
                    case 3:
                        s[index] = 1;
                        break;
                    default:
                        //this is a start point don't change the vector point
                        break;
                }
                this.Element.InitElement();
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.Invalidate();
            }
            void MarkSegmentType(int index) { 
                //mark segment index and create a create a close curve with that inde

            }
            

            private int m_PathMode;
            /// <summary>
            /// get or set the path mode
            /// </summary>
            public int PathMode
            {
                get { return m_PathMode; }
                set
                {
                    if (m_PathMode != value)
                    {
                        m_PathMode = value;
                    }
                }
            }
            public bool AllowSnippetView
            {
                get { return m_AllowSnippetView; }
                set
                {
                    if (m_AllowSnippetView != value)
                    {
                        m_AllowSnippetView = value;
                    }
                }
            }
            protected internal override void GenerateSnippets()
            {
                base.GenerateSnippets();
                var p = this.Element;
                if (p == null)
                {
                    return;
                }
                Vector2f[] t = p.Points;
                byte[] v_types = p.PointTypes;
                if (t != null)
                {
                    for (int i = 0; i < t.Length; i++)
                    {
                        var e = this.CurrentSurface.CreateSnippet(this, 0, i);
                        this.AddSnippet(e);
                        if ((v_types[i] & 3) == 3)
                        {
                            e.Shape = ICore.WinUI.enuSnippetShape.Circle;
                        }

                    }
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();              
                
            }
            protected internal override void InitSnippetsLocation()
            {
                if (this.Element == null) return;
                Vector2f[] t = this.Element.Points;
                var s = this.RegSnippets;

                if (s.Count >= t.Length)
                {
                    for (int i = 0; i < t.Length; i++)
                    {
                        this.RegSnippets[i].Location = CurrentSurface.GetScreenLocation(t[i]);
                    }
                }
            }
      
            
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (this.Element != null)
                        {
                            if ((this.Snippet != null))
                            { //only allow

                                
                                if (this.IsShiftKey)
                                {
                                    //insert new point
                                    if (this.IsControlKey)
                                    {
                                        this.State = ST_CREATING + 12;
                                    }
                                    else
                                    {
                                        this.State = ST_CREATING + 10;
                                        this.UpdateSnippetEdit(e);
                                    }
                                }
                                else if (this.IsControlKey ){
                                    ChangeVectorType(this.Snippet.Index);
                                }
                                else
                                {
                                    this.State = ST_EDITING;
                                    this.UpdateSnippetEdit(e);
                                }

                            }
                            else if (this.Element.Contains(e.FactorPoint))
                            {
                                this.Element.SuspendLayout();
                                this.BeginMove(e);
                            }

                        }
                        break;
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_MOVING:
                                this.EndMove(e);
                                break;
                            case ST_EDITING:
                            case ST_CREATING + 11:
                                if (this.State == ST_CREATING + 11)
                                {
                                    this.GenerateSnippets();
                                }
                                this.InitSnippetsLocation();
                                if (this.Snippet != null)
                                { //only allow

                                    this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                }
                                break;
                            case ST_CREATING +12: //changing snippet state index
                                if (this.Snippet != null) {
                                    int index = this.Snippet.Index;
                                    int i = this.Element.PointTypes[index] ;
                                    if ((i & 3) == 3)
                                    {
                                        i += -3+1;
                                    }
                                    else {
                                        if ((i & 1) == 1)
                                        {
                                            i += +3 - 1;
                                        }
                                        else
                                        {
                                            if (i == 0)
                                            {
                                                //start point
                                            }
                                            else
                                                i = 3;
                                        }
                                    }
                                    //this.Element.PointTypes[index] = (byte)i;
                                    this.Element.m_PointTypes[index] = (byte)i;
                                    this.Element.InitElement();
                                    this.GenerateSnippets();
                                    this.InitSnippetsLocation();
                                    this.Invalidate();
                                }
                                this.State = ST_NONE;
                                break;
                        }
                        break;
                    case enuMouseButtons.Right:
                        //remove point, end edition or go to default tool
                        if (this.Element != null)
                        {
                            if (this.Snippet != null)
                                this.RemovePoint();
                            else
                                EndEdition();
                        }
                        else
                        {
                            GotoDefaultTool();
                        }

                        break;
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_EDITING:
                            case ST_CREATING + 11:
                                if (this.Snippet != null)
                                {
                                    this.UpdateSnippetEdit(e);
                                }
                                break;
                            case ST_MOVING:
                                this.UpdateMove(e);
                                break;

                        }
                        break;
                }
            }

            private void RemovePoint()
            {
                List<Vector2f> tab = new List<Vector2f>();
                List<Byte> ctab = new List<byte>();
                tab.AddRange(this.Element.Points);
                if ((tab.Count > 0) && (this.Snippet.Index < tab.Count))
                {
                    ctab.AddRange(this.Element.m_PointTypes);
                    tab.RemoveAt(this.Snippet.Index);
                    ctab.RemoveAt(this.Snippet.Index);
                    this.Element.m_Points = tab.ToArray();
                    this.Element.m_PointTypes = ctab.ToArray();
                    this.Element.InitElement();
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                    this.Invalidate();
                }
            }
         
            protected override void UpdateSnippetEdit(ICore.WinUI.CoreMouseEventArgs e)
            {          
                if (this.Snippet == null)
                    return;
                PathElement v_element = this.Element;
                switch (this.State)
                {
                    case ST_CREATING + 10:
                        byte[] s = this.Element.m_PointTypes;
                         Vector2f[] t = this.Element.m_Points;
                        List<Vector2f> tpts = new List<Vector2f>();
                        tpts.AddRange(t);
                        tpts.Insert(this.Snippet.Index, e.FactorPoint);
                        List<Byte> tbytes = new List<byte>();
                        tbytes.AddRange(s);
                        tbytes.Insert(this.Snippet.Index+1, 1);

                        v_element.m_Points = tpts.ToArray();
                        v_element.m_PointTypes = tbytes.ToArray();
                        v_element.InitElement();
                        this.DisableSnippet();
                        
                        //snippet index to edit
                        this.State = ST_CREATING + 11;
                        break;
                    case ST_CREATING + 11:
                        this.Element.m_Points[this.Snippet.Index+1] = e.FactorPoint;
                        this.Element.InitElement();
                        this.Snippet.Location = e.Location;
                        break;
                    case ST_EDITING:
                        this.Element.m_Points[this.Snippet.Index] = e.FactorPoint;
                        this.Element.InitElement();
                        this.Snippet.Location = e.Location;
                        
                        break;
                }
                this.Invalidate();
            }


           
        }
    }

}

