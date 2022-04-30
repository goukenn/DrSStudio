

/*
IGKDEV @ 2008 - 2014
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
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:PathElement.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Drawing.Imaging;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Rendering;
    [Core2DDrawingStandardItem ("Path",
        typeof (Mecanism),
        Keys = Keys.Shift | Keys.P,
        IsVisible=true)]
    public sealed class PathElement : 
        Core2DDrawingLayeredDualBrushElement ,
        ICore2DClosableElement ,
        ICore2DFillModeElement ,
        ICore2DPathBrushStyleElement
    {
        private bool m_closed;
        private Vector2f [] m_points;
        private Byte[] m_types;
        private enuFillMode m_FillMode;
        private CorePathBrushStyleBase m_PathBrushStyle;
        [IGK.DrSStudio.Codec.CoreXMLElement ()]
        public CorePathBrushStyleBase PathBrushStyle
        {
            get { return m_PathBrushStyle; }
            set {
                if (this.m_PathBrushStyle != value)
                {
                    if (this.m_PathBrushStyle != null) this.m_PathBrushStyle.PropertyChanged -= PathBrushChanged;
                    this.m_PathBrushStyle = value;
                    if (this.m_PathBrushStyle != null) this.m_PathBrushStyle.PropertyChanged += PathBrushChanged;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        //raise the property changed
        void PathBrushChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        public Vector2f [] Points
        {
            get
            {
                return this.m_points;
            }
            set
            {
                this.m_points = value;
                OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        /// <summary>
        /// get the type
        /// </summary>
        public byte[] Types
        {
            get
            {
                return this.m_types;
            }
            set
            {
                this.m_types = value;
                OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        [CoreXMLAttribute (true)]
        [CoreXMLDefaultAttributeValue (true)]
        public bool Closed
        {
            get
            {
                return m_closed;
            }
            set
            {
                if (this.m_closed != value)
                {
                    m_closed = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuFillMode.Alternate)]
        public enuFillMode enuFillMode
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
        protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            string sm = string.Empty;
            Vector2f.Vector2fArrayTypeConverter v_conv = new Vector2f.Vector2fArrayTypeConverter();
            xwriter.WriteStartElement("Points");
            sm = v_conv.ConvertToString(Points);
            xwriter.WriteValue(sm);
            xwriter.WriteEndElement();
            sm = string.Empty;
            if (this.Types != null)
            {
                for (int i = 0; i < this.Types.Length; i++)
                {
                    if (i != 0)
                    {
                        sm += ";";
                    }
                    sm += this.Types[i].ToString();
                }
                xwriter.WriteStartElement("Types");
                xwriter.WriteValue(sm);
                xwriter.WriteEndElement();
            }
            base.WriteElements(xwriter);
        }
        protected override void ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            if (xreader.NodeType == XmlNodeType.Attribute)
                xreader.MoveToElement();
            IGK.DrSStudio.Codec.IXMLDeserializer xr = xreader.ReadSubtree();
            Vector2f.Vector2fArrayTypeConverter v_conv = new Vector2f.Vector2fArrayTypeConverter();
            try
            {
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
                                            if (obj.GetType().IsArray)
                                            {
                                                if (obj is Vector2f[])
                                                {
                                                    this.m_points = (Vector2f[])obj;
                                                }
                                                else if (obj is Vector2f[])
                                                {
                                                    Vector2f[] t = (Vector2f[])obj;
                                                    m_points = new Vector2f[t.Length];
                                                    for (int i = 0; i < t.Length; i++)
                                                    {
                                                        //implicit copy
                                                        m_points[i] = t[i];
                                                    }
                                                }
                                            }
                                            else
                                                this.m_points = new Vector2f[] { (Vector2f)obj };
                                        }
                                    }
                                    break;
                                case "Types":
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
                                            this.m_types = t;
                                        }
                                    }
                                    break;
                                default:
                                    System.Reflection.PropertyInfo v_pr = GetType().GetProperty(xreader.Name);
                                    if ((v_pr != null) &&
                                     IGK.DrSStudio.Codec.CoreXMLElementAttribute.IsCoreXMLElementAttribute(v_pr))
                                    {
                                        IGK.DrSStudio.Codec.CoreXMLSerializerUtility.SetElementProperty(xreader, v_pr, this, null);
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
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity) return;
            this.m_points = m.TransformPoint(this.m_points);// CoreMathOperation.MultMatrixTransformPoint(m, this.m_points);
        }
        protected override void GeneratePath()
        {
            if ((this.m_points == null) || (this.m_points.Length == 0)||
                (this.m_points .Length != this.m_types.Length ))
            {
                this.SetPath(null);
                return;
            }
            CoreGraphicsPath v_path = new CoreGraphicsPath();
            v_path.AddDefinition(this.m_points, this.m_types );
            if (this.Closed)
                v_path.CloseAllFigures();
            if (this.PathBrushStyle != null)
            {
                this.PathBrushStyle.Generate(v_path);
            }
            v_path.enuFillMode = this.enuFillMode;         
            this.SetPath(v_path);
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem(GetType().GetProperty("enuFillMode"));
            g.AddItem(GetType().GetProperty("Closed"));
            return parameters;
        }
        /// <summary>
        /// represent a path selection mecanism
        /// </summary>
        public new sealed class Mecanism : Core2DDrawingLayeredDualBrushElement.Mecanism
        {
            public new PathElement Element { get { return base.Element as PathElement ; } }
            private bool m_AllowSnippetView;
            public const int PATHMODE_LINE = 1;
            public const int PATHMODE_BEZIER = 2;
            public const int PATHMODE_ARC = 3;
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
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if (this.Element != null)
                {
                    this.CurrentSurface.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                    PointF[] tab = this.Element.m_points;
                    byte[] types = this.Element.m_types;
                    IGK.DrSStudio.WinUI.ICoreSnippets s = null;
                    for (int i = 0; i < tab.Length
                        ; i++)
                    {
                         s = CurrentSurface.CreateSnippet(this, i, i);
                        this.AddSnippet(s);
                        switch (types[i])
                        { 
                            case 3:
                                s.Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Circle;
                                break;
                        }
                    }
                    this.CurrentSurface.Cursor = System.Windows.Forms.Cursors.Default;
                }
            }
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                { 
                    case MouseButtons.Left :
                        if (this.Element != null)
                        {
                            if ((this.Snippet != null))
                            { //only allow
                                if (this.IsShiftKey)
                                {
                                    //insert new point
                                    this.State = ST_CREATING + 10;
                                    this.UpdateSnippetElement(e);
                                }
                                else
                                {
                                    this.State = ST_EDITING;
                                    this.UpdateSnippetElement(e);
                                }
                            }
                            else if (this.Element.Contains(e.FactorPoint))
                            {
                                this.BeginMove(e);
                            }
                        }
                        break;
                }
            }
            protected override void OnMouseUp(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
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
                        }
                        break;
                    case MouseButtons.Right:
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
                            GoToDefaultTool();
                        }
                        break;
                }
            }
            private void RemovePoint()
            {
                List<PointF> tab = new List<PointF>();
                List<Byte> ctab = new List<byte>();
                tab.AddRange (this.Element.m_points);
                if ((tab.Count > 0) && (this.Snippet.Index < tab.Count))
                {
                    ctab.AddRange(this.Element.m_types);
                    tab.RemoveAt(this.Snippet.Index);
                    ctab.RemoveAt(this.Snippet.Index);
                    this.Element.m_points = tab.ToArray();
                    this.Element.m_types = ctab.ToArray();
                    this.Element.InitElement();
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                    this.CurrentSurface.Invalidate();
                }
            }
            protected override void OnMouseMove(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (this.State )
                            {
                            case ST_EDITING :
                            case ST_CREATING + 11:
                                if (this.Snippet != null)
                                {
                                    this.UpdateSnippetElement(e);
                                }
                                    break;
                            case ST_MOVING :
                                    this.UpdateMove(e);
                                    break;
                        }
                        break;
                }
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.Element != null)
                {
                    PointF[] tab = this.Element.m_points;
                    if (tab.Length == this.RegSnippets.Count)
                    {
                        for (int i = 0; i < tab.Length
                            ; i++)
                        {
                            this.RegSnippets[i].Location = CurrentSurface.GetScreenLocation(tab[i]);
                        }
                    }
                }
            }
            protected override void InitNewCreateElement(ICore2DDrawingElement element)
            {
                base.InitNewCreateElement(element);
            }
            protected override void UpdateSnippetElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                if (this.Snippet == null)
                    return;
                PathElement v_element = this.Element;
                switch (this.State)
                {
                    case ST_CREATING + 10:
                        byte[]  s = this.Element.m_types;
                        PointF[] t = this.Element.m_points;
                        List<PointF> tpts = new List<PointF>();
                        tpts.AddRange(t);
                        tpts.Insert(this.Snippet.Index, e.FactorPoint);
                        List<Byte> tbytes = new List<byte>();
                        tbytes.AddRange(s);
                        tbytes.Insert(this.Snippet.Index, 1);
                        v_element.m_points = tpts.ToArray();
                        v_element.m_types = tbytes.ToArray();
                        v_element.InitElement();
                        this.DisableSnippet();
                        int index = this.Snippet.Index;
                        this.State = ST_CREATING + 11;
                        break;
                    case ST_CREATING + 11:
                        this.Element.m_points[this.Snippet.Index] = e.FactorPoint;
                        this.Element.InitElement();
                        this.Snippet.Location = e.Location;
                        this.CurrentSurface.Invalidate();
                        break;
                    case ST_EDITING:
                        this.Element.m_points[this.Snippet.Index] = e.FactorPoint;
                        this.Element.InitElement();
                        this.Snippet.Location = e.Location;
                        this.CurrentSurface.Invalidate();
                        break;
                }
            }
        }
        public static PathElement Create(CoreGraphicsPath vp)
        {
            if (vp == null)
                return null;
            PathElement c = new PathElement();
            Vector2f[] pointfs = null;
            byte[] type = null;
            vp.GetAllDefinition(ref pointfs, ref type);
            c.m_points = pointfs;
            c.m_types = type;
            c.m_FillMode = vp.enuFillMode;
            c.InitElement();
            return c;
        }
        public void SetPathDefinition(PointF[] pointF, byte[] pathType)
        {
            if ((pointF == null) || (pathType == null))
                return;
            if (pathType.Length != pointF.Length)
                return;
            this.m_points = pointF;
            this.m_types = pathType;
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
    }
}

