

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingLayeredElement.cs
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
file:Core2DDrawingLayeredElement.cs
*/
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.ComponentModel;
using IGK.ICore.Dependency;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{

    
    /// <summary>
    /// represent a layered element
    /// </summary>
    public abstract class Core2DDrawingLayeredElement : 
        Core2DDrawingObjectBase,
        ICore2DDrawingLayeredElement ,
        ICore2DDrawingTransformableElement ,
        ICoreBrushOwner ,
        ICore2DDrawingShadowElement,
        ICoreWorkingPositionableObject
    {
        CoreGraphicsPath m_graphicPath;
        Matrix m_matrix;
        Matrix m_tempMatrix;
        private bool m_View;
        private enuSmoothingMode m_SmoothingMode;
        private enuCompositingMode m_CompositingMode;
        public bool Locked;
        private Core2DShadowProperty m_ShadowProperty;
        private bool m_AllowShadow;

        /// <summary>
        /// return the display bound of this element.
        /// </summary>
        /// <returns></returns>
        public virtual Rectanglef GetDisplayBounds()
        {
            //only get the bounds screen display
            Rectanglef v_rc = this.GetBound();
            
            return this.ApplyDocumentMatrix(v_rc);
        }
        /// <summary>
        /// get selection bound of this element
        /// </summary>
        /// <returns></returns>
        public virtual Rectanglef GetSelectionBound()
        {
            //only get the bounds screen display
            Rectanglef v_rc = this.GetBound();
            return this.ApplyDocumentMatrix(v_rc);
            //return this.GetBound(); 
        }
        /// <summary>
        /// get the matrix transform
        /// </summary>
        [CoreXMLAttribute ()]
        [CoreMatrixDefaultValue ()]
        [Browsable(false)]
        public Matrix Matrix {
            get {
                return this.m_matrix;
            }
        }
        /// <summary>
        /// get the set temporart matrix.
        /// </summary>
        protected Matrix TempMatrix { get { return this.m_tempMatrix; } }

        /// <summary>
        /// represent shadow property attribute
        /// </summary>
        [IGK.ICore.Codec.CoreXMLAttribute()]
        [Category("ShadowProperties")]
        public Core2DShadowProperty ShadowProperty
        {
            get { return m_ShadowProperty; }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(false)]
        [Category("ShadowProperties")]
        public bool AllowShadow
        {
            get {
                if (this.CanRenderShadow == false)
                    return false;
                return m_AllowShadow; 
            }
            set
            {
                if (this.CanRenderShadow && (m_AllowShadow != value))
                {
                    m_AllowShadow = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [System.ComponentModel.Browsable(false)]
        /// <summary>
        /// get if this element can render shadow
        /// </summary>
        public virtual bool CanRenderShadow {
            get { 
                return true;
            }
        }
        protected override void OnLoadingComplete(EventArgs eventArgs)
        {//init element before raise the loading element complete
            //this.InitElement();
            base.OnLoadingComplete(eventArgs);
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var p = base.GetParameters(parameters);
            if (this.CanRenderShadow)
            {
                var group = parameters["ShadowProperty"];
                if (group == null)
                    group = parameters.AddGroup("ShadowProperty");
                group.AddItem(GetType().GetProperty("AllowShadow"));
                group.AddConfigObject(this.ShadowProperty);
            }
            return p;
        }
        
        ///// <summary>
        ///// render the shawdow
        ///// </summary>
        ///// <param name="g"></param>
        //public void RenderShadow(ICoreGraphics g)
        //{
        //    if (!this.CanRenderShadow || !this.AllowShadow)
        //        return;
        //    ///get shadown path
        //    CoreGraphicsPath  v_p = this.GetShadowPath();
        //    ICoreBrush br = this.ShadowProperty.Brush;// CoreBrushRegister.GetBrush(this.ShadowProperty.ShadowColor);
        //    object  s = g.Save();
        //    //this.SetGraphicsProperty(g);
        //    if (this.ShadowProperty.IsClipped)
        //    {
        //        //GraphicsPath v_cp = this.GetPath();
        //        //if (v_cp != null)
        //        //{
        //        //    GraphicsPath c_p = v_cp.Clone() as GraphicsPath;
        //        //    Region rg = new Region(c_p);
        //        //    rg.Complement(v_p);
        //        //    g.Clip = rg;
        //        //}
        //    }
        //    g.FillPath(br, v_p);
        //    v_p.Dispose();
        //    g.Restore(s);
        //}

        public virtual  CoreGraphicsPath GetShadowPath()
        {
            CoreGraphicsPath c = new CoreGraphicsPath();

            return c;
        }



        [Browsable(false)]
        public virtual bool CanTranslate {
            get
            {
                return true;
            }
        }
        [Browsable(false)]
        public virtual bool CanRotate{
            get{
                return true;
            }
        }
        [Browsable(false)]
        public virtual bool CanScale {
            get {
                return true;
            }
        }
        [System.ComponentModel.Browsable(false)]
        public virtual bool CanReSize {
            get {
                return true;
            }
        }

        protected Vector2f ToDocumentPoint(Vector2f d) {
            using (Matrix m = GetDocumentMatrix())
            {
                if (!m.IsIdentity)
                {
                    m.Invert();
                    d = m.TransformPoints(new Vector2f[] { d })[0];
                }
            }
            return d;
        }
        /// <summary>
        /// detect if contains
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool Contains(Vector2f point)
        {
            point = ToDocumentPoint(point);

            if (this.GetPath().IsVisible(point))
                return true;
            else if (this.GetPath().IsOutilineVisible(point))
                return true;
            return false;
        }
        /// <summary>
        /// detect outilise visibility
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool IsOutiliseVisible(Vector2f point)
        {
            return this.GetPath ().IsOutilineVisible (point);
        }
        [CoreXMLDefaultAttributeValue (enuCompositingMode.Over )]
        [CoreConfigurablePropertyAttribute()]
        [Category("Definition")]
        public enuCompositingMode CompositingMode
        {
            get { return m_CompositingMode; }
            set
            {
                if (m_CompositingMode != value)
                {
                    m_CompositingMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLDefaultAttributeValue(enuSmoothingMode.AntiAliazed)]
        [CoreConfigurablePropertyAttribute()]
        [Category("Definition")]
        public enuSmoothingMode SmoothingMode
        {
            get { return m_SmoothingMode; }
            set
            {
                if (m_SmoothingMode != value)
                {
                    m_SmoothingMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public void Invalidate(bool t)
        {
            ICoreWorkingDesignerSurface c = this.ParentDesigner;
            if (c != null)
            {
                c.RefreshScene();
            }
        }
        [Browsable(false)]
        [ReadOnly(true)]
        public ICoreWorkingDesignerSurface ParentDesigner {
            get {
                return CoreSystem.GetWorkbench()?.CurrentSurface as ICoreWorkingDesignerSurface;
            }
        }
        public bool View
        {
            get { return m_View; }
            set
            {
                if (m_View != value)
                {
                    m_View = value;
                    OnPropertyChanged(Core2DDrawingChangement.ViewChanged);
                }
            }
        }
        /// <summary>
        /// maintain the init element when view required;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            switch ((enu2DPropertyChangedType)e.ID)
            {
                case enu2DPropertyChangedType.DefinitionChanged:
                case enu2DPropertyChangedType.MatrixChanged:
                    if (!this.IsLoading)
                    {
                        this.InitElement();
                    }
                    break;
            }
            base.OnPropertyChanged(e);
        }
        /// <summary>
        /// get the graphics path bounds
        /// </summary>
        /// <returns></returns>
        public virtual Rectanglef GetBound() 
        {
           return m_graphicPath.GetBounds();            
        }
        /// <summary>
        /// return the default bound of this element
        /// </summary>
        /// <returns></returns>
        public Rectanglef GetDefaultBound()
        {
            Rectanglef rc = this.GetBound();
            Matrix v_m = this.GetMatrix().Clone () as Matrix ;
            if (v_m.IsIdentity)
                return rc;
            if (v_m.IsInvertible)
                v_m.Invert();
            rc = CoreMathOperation.ApplyMatrix(rc, v_m);
            v_m.Dispose();
            return rc;
        }
        /// <summary>
        /// get the current in uses matrix.(Temporary or Basic)
        /// </summary>
        /// <returns></returns>
        public Matrix GetMatrix() {
            if (this.m_tempMatrix != null)
                return m_tempMatrix;
            return m_matrix;
        }
        /// <summary>
        /// rebuild working object before reset transform
        /// </summary>
        protected virtual void BuildBeforeResetTransform()
        { 
        }
        protected void InitElement()
        {
            this.InitGraphicPath (this.m_graphicPath );
            this.SetMatrix();
        }
        /// <summary>
        /// preferer used function to indicate that is not a property
        /// </summary>
        /// <returns></returns>
        public CoreGraphicsPath GetPath()
        {
            return this.m_graphicPath;
        }
        public Core2DDrawingLayeredElement()
        {
            this.m_graphicPath = CreateGraphicsPath();
            this.m_ShadowProperty = new Core2DShadowProperty(this);
            this.m_ShadowProperty.PropertyChanged += _ShadowProperty_PropertyChanged;

            this.m_matrix = new Matrix();
            this.m_tempMatrix = null;
            this.m_View = true;
            this.SuspendLayout();
            //initialize element 
            this.InitializeElement();

            this.ResumeLayout();
        }
        /// <summary>
        /// override this method to initialize your element in the constructor chain.
        /// </summary>
        protected virtual void InitializeElement()
        {
            this.m_SmoothingMode = enuSmoothingMode.AntiAliazed;
            this.m_CompositingMode = enuCompositingMode.Over;
        }
        void _ShadowProperty_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            OnPropertyChanged(Core2DDrawingChangement.Definition);
        }
        /// <summary>
        /// recursivily get the parent document
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ICore2DDrawingDocument GetParentDocument(ICore2DDrawingObject obj)
        {
            if (obj.Parent == null)
                return null;
            if (obj.Parent is ICore2DDrawingDocument)
                return obj.Parent as ICore2DDrawingDocument;
            if (obj.Parent is ICore2DDrawingLayer)
            {
                return GetParentDocument(obj.Parent);
            }
            return null;
        }
        protected virtual CoreGraphicsPath CreateGraphicsPath()
        {
            if (this is ICoreBrushOwner)
                return new CoreGraphicsPath(this as ICoreBrushOwner);
            return new CoreGraphicsPath ();
        }
        public override void Dispose()
        {
            if (this.m_matrix != null) this.m_matrix.Dispose();
            if (this.m_tempMatrix !=null) this.m_tempMatrix.Dispose();
            if (this.m_graphicPath != null) this.m_graphicPath.Dispose();
            if (this.m_ShadowProperty != null) { this.m_ShadowProperty.Dispose(); this.m_ShadowProperty = null; }
            base.Dispose();
        }
        protected abstract  void InitGraphicPath(CoreGraphicsPath path);
        
        [Browsable(false)]
        /// <summary>
        /// get the container
        /// </summary>
        public ICore2DDrawingElementContainer Container
        {
            get {
                return Parent as ICore2DDrawingElementContainer;
            }
        }
        ICoreWorkingElementContainer ICoreWorkingPositionableObject.Container
        {
            get { return this.Container; }
        }
        [Browsable(false)]
        public ICore2DDrawingLayer ParentLayer
        {
            get {
                var q = this.Parent;
                while ((q != null) && !(q is ICore2DDrawingLayer))
                {
                    q = q.Parent;
                }
                return q as ICore2DDrawingLayer;
            }
        }
        [Browsable(false)]
        public ICore2DDrawingDocument ParentDocument
        {
            get {
                var q = this.Parent;
                while ((q != null) && !(q is ICore2DDrawingDocument))
                {
                    q = q.Parent;
                }
                return q as ICore2DDrawingDocument;
            }
        }
        public  ICoreGraphicsRegion GetRegion()
        {
            return null;
        }
        [System.ComponentModel.Browsable(false)]
        public int ZIndex
        {
            get {
                ICoreWorkingObjectIdElementContainer v = (this.Parent as ICoreWorkingObjectIdElementContainer);
                if (v!=null)
                {
                    return v.IndexOf(this);
                }
                return -1;
            }
        }
        public void Rotate(float angle, Vector2f center, enuMatrixOrder enuMatrixOrder, bool p)
        {
            if ((this.Locked) || (!CanScale))
                return;
            if (p)
            {
                this.RestorePathPreviousMatrix();
                this.DisposeTempMatrix();
                this.m_tempMatrix = this.m_matrix.Clone() as Matrix;
                this.m_tempMatrix.Rotate(angle, center, enuMatrixOrder.Append);
                this.SetMatrix();
            }
            else
            {                
                this.RestorePathPreviousMatrix();
                this.m_matrix.Rotate (angle, center, enuMatrixOrder.Append);
                this.DisposeTempMatrix();
                this.SetMatrix();
                this.OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.MatrixChanged));
            }
        }
        public void Inflate(float x, float y, enuMatrixOrder enuMatrixOrder)
        {
        }
        protected override void WriteAttributes(IXMLSerializer xwriter)
        {
            base.WriteAttributes(xwriter);
        }
        protected override void ReadAttributes(IXMLDeserializer xreader)
        {
            string m = xreader.GetAttribute("Matrix");
            if (!string.IsNullOrEmpty(m))
            {
               this.m_matrix.LoadString(m);                             
            }
            base.ReadAttributes(xreader);
        }
        [System.ComponentModel.Browsable(false)]
        /// <summary>
        /// get if this elemenet cant be edited
        /// </summary>
        public virtual bool CanEdit { get { return true; } }
        public Rectanglef Intersect(Rectanglef e)
        {
            Rectanglef v_rc = this.GetBound();
            v_rc.Intersect(e);
            return v_rc;
        }
        /// <summary>
        /// get document matrix chain that is apply a a element
        /// </summary>
        /// <returns></returns>
        public  Matrix GetDocumentMatrix()
        {
            Matrix v_mat = new Matrix();
            ICore2DDrawingObject l = this.Parent;
            while (l != null)
            {
                if (l is ICore2DDrawingTransformableElement)
                {
                    v_mat.Multiply((l as ICore2DDrawingTransformableElement).GetMatrix(), enuMatrixOrder.Append );
                }
                if (l is ICore2DDrawingViewPort)
                {
                    ICore2DDrawingViewPort c = l as ICore2DDrawingViewPort;
                    v_mat.Translate(c.Bounds.X, c.Bounds.Y, enuMatrixOrder.Append);
                }
                l = l.Parent;
            }
            return v_mat;
        }
        /// <summary>
        /// get the first top parent view port
        /// </summary>
        /// <returns></returns>
        public ICore2DDrawingViewPort GetViewPort()
        {  ICore2DDrawingObject l = this.Parent;
            while ((l != null) && (l is ICore2DDrawingViewPort) == false )
            {
                l = l.Parent;
            }
            return l as ICore2DDrawingViewPort;

        }
        internal protected void Scale(float ex, float ey, Rectanglef rectanglef, Vector2f endLocation, enuMatrixOrder enuMatrixOrder, bool temp)
        {
            if (!CanScale || this.Locked)
                return;
            if ((ex.ToString() == float.NaN.ToString()) ||
                (ey.ToString() == float.NaN.ToString()))
                return;
            Rectanglef rf = rectanglef ;
            if (temp)
            {
                RestorePathPreviousMatrix();
                this.DisposeTempMatrix();
                this.m_tempMatrix = this.m_matrix.Clone() as Matrix ;
                this.m_tempMatrix.Translate(-rf.X, -rf.Y, enuMatrixOrder.Append);
                this.m_tempMatrix.Scale(ex, ey, enuMatrixOrder.Append);
                this.m_tempMatrix.Translate(endLocation.X, endLocation.Y, enuMatrixOrder.Append);
                this.SetMatrix();
            }
            else
            {
                this.RestorePathPreviousMatrix();
                this.DisposeTempMatrix();
                this.m_matrix.Translate(-rf.X, -rf.Y, enuMatrixOrder.Append);
                this.m_matrix.Scale(ex, ey, enuMatrixOrder.Append);
                this.m_matrix.Translate(endLocation.X, endLocation.Y, enuMatrixOrder.Append);                
                this.SetMatrix();                
                this.OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.MatrixChanged));
            }
        }
        internal protected virtual void Translate(float dx, float dy, enuMatrixOrder order, bool temp)
        {
            if (!CanTranslate) return;
            if (this.Locked) return;
            if (((dx == 0) && (dy == 0)))
            {
                //no translation 
                return;
            }
            if (temp)
            {
                this.RestorePathPreviousMatrix();
                this.DisposeTempMatrix();
                this.m_tempMatrix = this.m_matrix.Clone() as Matrix ;                
                this.m_tempMatrix.Translate(dx, dy, order);
                this.SetMatrix();
                
            }
            else
            {
                this.RestorePathPreviousMatrix();
                this.DisposeTempMatrix();
                this.m_matrix.Translate(dx, dy, order);
                this.SetMatrix();
                this.OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.MatrixChanged));
            }
        }
        /// <summary>
        /// override this to apply your custom matrix behaviour
        /// </summary>
        protected virtual void SetMatrix()
        {
            ICoreGraphicsPath vp = this.GetPath();
            if (vp!=null) 
                vp.Transform(this.GetMatrix());
        }
        protected  void DisposeTempMatrix()
        {
            if (this.m_tempMatrix != null)
            {
                this.m_tempMatrix.Dispose();
                this.m_tempMatrix = null;
            }
        }
        protected  virtual void RestorePathPreviousMatrix()
        {
            if (this.m_tempMatrix != null)
            {
                if (this.m_tempMatrix.IsInvertible)
                {
                    Matrix m = this.m_tempMatrix.Clone() as Matrix;
                    m.Invert();
                    this.GetPath().Transform(m);
                    m.Dispose();
                }
            }
            else
            {
                if (this.Matrix.IsInvertible )
                {
                    Matrix m = this.Matrix.Clone() as Matrix;
                    m.Invert();
                    GetPath().Transform(m);
                    m.Dispose();
                }
            }
        }
       
        protected Rectanglef ApplyDocumentMatrix(Rectanglef v_rc)
        {
            if (v_rc.IsEmpty && (v_rc.X == 0) && (v_rc.Y == 0))
                return Rectanglef.Empty;
            Matrix mm = GetDocumentMatrix();
            v_rc = CoreMathOperation.ApplyMatrix(v_rc, mm);
            mm.Dispose();
            return v_rc;
        }
        ICoreGraphicsPath ICore2DDrawingLayeredElement.GetPath()
        {
            return this.GetPath();
        }
        public void Transform(Matrix matrix)
        {
            if (this.m_matrix !=null)
                this.m_matrix = matrix.Clone() as Matrix;
            this.InitElement();
            OnPropertyChanged(Core2DDrawingChangement.Matrix);
        }
        /// <summary>
        /// multiplay the matrix with the current mattrix definiont
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="order"></param>
        public void MultTransform(Matrix matrix, enuMatrixOrder order)
        {
            this.m_matrix.Multiply(matrix, order);
            OnPropertyChanged(Core2DDrawingChangement.Matrix);
        }
        /// <summary>
        /// used to reset transformation by applying the current matrix to the path definition
        /// </summary>
        public void ResetTransform()
        {
            this.ResetTransform(true);
        }
        protected internal void ResetTransform(bool raiseTransformEvent)
        {
            if (!this.m_matrix.IsIdentity)
            {
                BuildBeforeResetTransform();
                this.m_matrix.Reset();
                if (!this.IsLoading)
                    this.InitElement();
                if (raiseTransformEvent)
                {
                    OnPropertyChanged(Core2DDrawingChangement.Matrix);
                }
            }
        }
        public void ClearTransform()
        {
            if (!this.m_matrix.IsIdentity)
            {
                this.m_matrix.Reset();
                this.InitElement();
                OnPropertyChanged(Core2DDrawingChangement.Matrix);
            }
        }
        /// <summary>
        /// translate the matrix transform
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="order"></param>
        public void Translate(float dx, float dy, enuMatrixOrder order)
        {
            //this.m_matrix.Translate(dx, dy, order);
            this.Translate(dx, dy, order, false);
            //this.OnPropertyChanged(Core2DDrawingChangement.Matrix);
        }
        /// <summary>
        /// scale the matrix transform
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ey"></param>
        /// <param name="order"></param>
        public void Scale(float ex, float ey, enuMatrixOrder order)
        {
            this.m_matrix.Scale(ex, ey, order);
            this.OnPropertyChanged(Core2DDrawingChangement.Matrix);
        }
        /// <summary>
        /// roate the matrix tranformation
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="center"></param>
        /// <param name="mat"></param>
        public void Rotate(float angle, Vector2f center, enuMatrixOrder mat)
        {
            this.m_matrix.Rotate(angle, center, mat);
            this.OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.MatrixChanged));
        }
        /// <summary>
        /// get the alignment bounds of this element. used in Align function
        /// </summary>
        /// <returns></returns>
        public virtual Rectanglef GetAlignmentBound() {
            return this.GetBound();
        }
        /// <summary>
        /// align to bound
        /// </summary>
        /// <param name="alignment">alignement</param>
        /// <param name="bounds">bounds to align</param>
        public virtual void Align(enuCore2DAlignElement alignment, Rectanglef bounds)
        {
            if (this.Locked)
                return;
            Matrix m = this.GetMatrix();
            Rectanglef b = this.GetAlignmentBound();
            Vector2f  c = Vector2f.Zero;

            if (this is ICore2DCircleElement)
            {
                c = (this as ICore2DCircleElement).Center;
            }
            else 
            c = CoreMathOperation.GetCenter(b);
            Vector2f bc = CoreMathOperation.GetCenter(bounds);
            Vector2f p = CoreMathOperation.GetDistanceP(bc, c);
            p = new Vector2f(Math.Ceiling(p.X), Math.Ceiling(p.Y));
            Rectanglef v_rc = bounds;
            float dx = 0.0f;
            float dy = 0.0f;
            float rx = b.X;
            float ry = b.Y;
            switch (alignment)
            {
                case enuCore2DAlignElement.TopLeft:
                    dx = 0;
                    dy = 0;
                    break;
                case enuCore2DAlignElement.CenterHorizontal:
                    ry = 0;
                    dx = p.X;// (v_rc.Width - b.Width) / 2.0f;
                    this.Translate(dx, dy, enuMatrixOrder.Append, false);
                    return;
                case enuCore2DAlignElement.CenterVertical:
                    rx = 0;
                    dy = p.Y;// (v_rc.Height - b.Height) / 2.0f;
                    this.Translate(dx, dy, enuMatrixOrder.Append, false);
                    return;
                case enuCore2DAlignElement.TopMiddle:
                    dx = (v_rc.Width - b.Width) / 2.0f;
                    break;
                case enuCore2DAlignElement.TopRight:
                    dx = v_rc.Width - b.Width;
                    break;
                case enuCore2DAlignElement.Center:
                    dx = p.X;// (v_rc.Width - b.Width) / 2.0f;
                    dy = p.Y;// (v_rc.Height - b.Height) / 2.0f;
                    this.Translate(dx, dy, enuMatrixOrder.Append, false);
                    return;
                case enuCore2DAlignElement.MiddleLeft:
                    dy = (v_rc.Height - b.Height) / 2.0f;
                    break;
                case enuCore2DAlignElement.MiddleRight:
                    dx = v_rc.Width - b.Width;
                    dy = (v_rc.Height - b.Height) / 2.0f;
                    break;
                case enuCore2DAlignElement.BottomLeft:
                    dy = v_rc.Height - b.Height;
                    break;
                case enuCore2DAlignElement.BottomRight:
                    dy = v_rc.Height - b.Height;
                    dx = v_rc.Width - b.Width;
                    break;
                case enuCore2DAlignElement.BottomMiddle:
                    dx = (v_rc.Width - b.Width) / 2.0f;
                    dy = v_rc.Height - b.Height;
                    break;
            }
#pragma warning disable IDE0054 // Use compound assignment
            dx = dx - rx;
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
            dy = dy - ry;
#pragma warning restore IDE0054 // Use compound assignment
            this.Translate(dx, dy, enuMatrixOrder.Append, false);
        }
        public void Align(enuCore2DAlignElement alignment)
        {
            if (this.Locked)
                return;
            var c = this.GetViewPort();
            if (c != null)
            {
                this.Align(alignment, c.Bounds);
            }
        }

        public void Dock(enuCore2DDockElement dock)
        {
            if (this.Locked)
                return;
            var c = this.GetViewPort();
            if (c!=null){
                this.Dock(dock, c.Bounds);
            }
        }
        public void Dock(enuCore2DDockElement dock, Rectanglef docBound)
        {
            if (this.Locked)
                return;
            float v_w = 0;
            float v_h = 0;
            Rectanglef v_rec = Rectanglef.Empty;
            v_rec = docBound;
            v_w = (float)v_rec.Width;
            v_h = (float)v_rec.Height;
            Rectanglef b = this.GetBound();
            float ex = 1.0f;
            float ey = 1.0f;
            Vector2f endLoc = Vector2f.Zero;
            switch (dock)
            {
                case enuCore2DDockElement.DockFill:
                    ex = v_w / b.Width;
                    ey = v_h / b.Height;
                    break;
                case enuCore2DDockElement.DockLeft:
                    ey = v_h / b.Height;
                    break;
                case enuCore2DDockElement.DockRight:
                    ey = v_h / b.Height;
                    endLoc = new Vector2f(v_w - b.Width, 0.0f);
                    break;
                case enuCore2DDockElement.DockUp:
                    ex = v_w / b.Width;
                    break;
                case enuCore2DDockElement.DockDown:
                    ex = v_w / b.Width;
                    endLoc = new Vector2f(0, v_h - b.Height);
                    break;
                case enuCore2DDockElement.DockLeftAndRight:
                    ex = v_w / b.Width;
                    ey = 1;
                    endLoc = new Vector2f(0, b.Y);
                    break;
                case enuCore2DDockElement.DockUpAndDown:
                    ey = v_h / b.Height;
                    ex = 1;
                    endLoc = new Vector2f(b.X, 0);
                    break;
            }
            Scale(ex, ey, endLoc, enuMatrixOrder.Append, false);
        }
        protected internal virtual void Scale(float ex, float ey, Vector2f endLocation, enuMatrixOrder order, bool temp)
        {
            if ((this.Locked) || (!CanScale))
                return;
            Rectanglef rf = Rectanglef.Empty;
            if (temp)
            {
                this.RestorePathPreviousMatrix();
                this.DisposeTempMatrix();
                rf = this.GetBound();
                this.m_tempMatrix = this.m_matrix.Clone() as Matrix;
                this.m_tempMatrix.Translate(-rf.X, -rf.Y, enuMatrixOrder.Append);
                this.m_tempMatrix.Scale(ex, ey, enuMatrixOrder.Append);
                this.m_tempMatrix.Translate(endLocation.X, endLocation.Y, enuMatrixOrder.Append);
                this.SetMatrix();
            }
            else
            {
                rf = this.GetBound();
                this.RestorePathPreviousMatrix();
                this.m_matrix.Translate(-rf.X, -rf.Y, enuMatrixOrder.Append);
                this.m_matrix.Scale(ex, ey, enuMatrixOrder.Append);
                this.m_matrix.Translate(endLocation.X, endLocation.Y, enuMatrixOrder.Append);
                this.DisposeTempMatrix();
                this.SetMatrix();
                this.OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.MatrixChanged));
            }
        }
        public void FlipX()
        {
            if ((this.Locked))
                return;
            Rectanglef b = this.GetBound();
            Vector2f endLoc = Vector2f.Zero;
            endLoc = new Vector2f(b.X + b.Width, b.Y);
            this.Scale(-1, 1, endLoc, enuMatrixOrder.Append, false);
        }
        public void FlipY()
        {
            if (this.Locked)
                return;
            Rectanglef b = this.GetBound();
            Vector2f endLoc = new Vector2f(b.X, b.Y + b.Height);
            Scale(1, -1, endLoc, enuMatrixOrder.Append, false);
        }


        public virtual  ICoreBrush GetBrush(enuBrushMode enuBrushMode)
        {
            return null;
        }

        [Category("Brush")]
        public virtual  enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.None; }
        }


        #region "for edition"
        protected internal virtual void BeginDrawing(Core2DDrawingEventArgs e){ 
        }
        protected internal virtual void UpdateDrawing(Core2DDrawingEventArgs e) { 
        }
        #endregion





        public T GetValue<T>(CoreDependencyProperty property)
        {
            var v_o = GetValue(property);
            if ((v_o != null)&&(v_o is T))
                return (T)v_o;
            return default(T);
        }
    }
}

