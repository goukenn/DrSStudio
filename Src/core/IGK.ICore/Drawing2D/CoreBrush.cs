

using IGK.ICore;using IGK.ICore.Imaging;
using IGK.ICore.IO;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreBrush.cs
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
file:CoreBrush.cs
*/
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public class CoreBrush : CoreResourceItemBase, ICoreBrush 
    {
        private ICoreBrushOwner m_owner;
        protected ICoreBrush m_Brush;
        private enuBrushType m_BrushType;
        private Colorf[] m_Colors;
        private float[] m_factors;
        private float[] m_positions;
        private string m_filename;
        private ICoreBitmap m_Bitmap;
        private enuHatchStyle m_hatchStyle;
        private bool m_autosize;
        private Rectanglef m_bound;
        private enuWrapMode m_wrapMode;
        private float m_Scale;
        private float m_Focus;
        private float m_Angle;
        private enuLinearMode m_LinearMode;
        private enuPathBrushMode m_PathBrushMode;
        private Vector2f m_PathFocusScale;
        private Vector2f m_PathCenter;
        private enuLinearOperator m_linearOperator;
        private bool m_gammaCorrection;
        private bool m_autoCenter;
        private Matrix m_matrix;
        private bool m_textureAutoTransform;
        private ICoreTextureResource m_textureRes;
        public Vector2f PathFocusScale { get { return this.m_PathFocusScale; } }

        public override string ToString()
        {
            if (string.IsNullOrEmpty (this.Id ) == false )
            {
                return string.Format("Brush#{0}", this.Id);
            }
            return base.ToString();
        }
        /// <summary>
        /// get the transform of the current matrix
        /// </summary>
        public Matrix Transform { get { return this.m_matrix; } }
        public enuPathBrushMode PathBrushMode
        {
            get
            {
                return this.m_PathBrushMode;
            }
        }
        /// <summary>
        /// get the brush type
        /// </summary>
        public enuBrushType BrushType
        {
            get { return m_BrushType; }
            protected set {
                this.m_BrushType = value;
            }
        }
        /// <summary>
        /// get the linear mode
        /// </summary>
        public enuLinearMode LinearMode
        {
            get { return this.m_LinearMode; }
        }
        private bool m_OneColorPerVertex;
        /// <summary>
        /// get if the path brush support one color per vertex
        /// </summary>
        public bool OneColorPerVertex
        {
            get { return m_OneColorPerVertex; }
        }
        public Colorf[] Colors { get { return this.m_Colors; } protected set { this.m_Colors =value; } }

        /// <summary>
        /// get the hatch style, used SetHatchBrush to modify this property
        /// </summary>
        public enuHatchStyle HatchStyle { get { return this.m_hatchStyle; } }
        /// <summary>
        /// get or set the autosize of this value
        /// </summary>
        public bool AutoSize { get { return this.m_autosize; } set {
            this.m_autosize = value;
        } }
        public enuWrapMode WrapMode { get { return this.m_wrapMode; } }
        /// <summary>
        /// get or set the bounds the requested bound of this brush
        /// </summary>
        public Rectanglef Bounds { get { return this.m_bound; } set { this.m_bound = value; } }
        public ICoreBitmap Bitmap { get { return this.m_Bitmap; } }
        public float[] Factors { get { return this.m_factors; } protected set { this.m_factors = value; } }
        public float[] Positions { get { return this.m_positions; } protected set { this.m_positions = value; } }
        public float Scale { get { return this.m_Scale; } }
        public float Focus { get { return this.m_Focus; } }
        internal static readonly CoreBrush White;
        public float Angle { get { return this.m_Angle; } set { this.m_Angle = value; } }
        public enuLinearOperator LinearOperator { get { return this.m_linearOperator; } }
        public bool GammaCorrection { get { return this.m_gammaCorrection; } }
        public bool AutoCenter { get { return this.m_autoCenter; } }
        public bool TextureAutoTranform { get { return this.m_textureAutoTransform; } }
        static CoreBrush() {
            White = new CoreBrush();
            White.SetSolidColor(Colorf.White);
        }
        private CoreBrush()
        {
        }
        /// <summary>
        /// get or set the path center
        /// </summary>
        public Vector2f PathCenter
        {
            get { return this.m_PathCenter; }
            set
            {
                if (!this.m_PathCenter.Equals(value))
                {
                    this.m_PathCenter = value;
                    OnBrushDefinitionChanged(EventArgs.Empty);                    
                }
            }
        }
        protected void setPathCenter(Vector2f p) {
            this.m_PathCenter = p;
        }
        /// <summary>
        /// get the resources type
        /// </summary>
        public override enuCoreResourceType ResourceType
        {
            get { return enuCoreResourceType.Brush; }
        }
        #region Events
        public event EventHandler BrushDefinitionChanged;
        public new event EventHandler Disposed;
        #endregion

        private bool m_suspendBrush;
       
        /// <summary>
        /// raise the brush event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBrushDefinitionChanged(EventArgs e)
        {
            if ((!this.m_suspendBrush )&& 
                (this.BrushDefinitionChanged != null))
                this.BrushDefinitionChanged(this, e);
        }
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="owner"></param>
        public CoreBrush(ICoreBrushOwner owner)
        {
            this.m_owner = owner;
            this.m_matrix = new Matrix();
            InitBrush();
            if (this.m_owner !=null)
                this.m_owner.PropertyChanged += _OwnerPropertyChanged;
            if (this.m_owner is ICoreLoadableComponent)
            {
                (this.m_owner as ICoreLoadableComponent).LoadingComplete += this._LoadingComplete;
            }
            if (CoreApplicationManager.Application != null)
            {//brush registring
                var v_brushRegister = CoreApplicationManager.Application.BrushRegister;
                if (v_brushRegister != null)
                    v_brushRegister.Register(this);
            }
        }
        void _LoadingComplete(object sender, EventArgs e)
        {
            ICoreLoadableComponent k = sender as ICoreLoadableComponent;
            if (!k.IsLoading)
            {
                var v_brushRegister = CoreApplicationManager.Application.BrushRegister;
                if (v_brushRegister != null)
                    v_brushRegister.Reload(this);
            }
        }
        void _OwnerPropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
                switch ((enu2DPropertyChangedType)e.ID)
                {
                    case enu2DPropertyChangedType.DefinitionChanged:
                    case enu2DPropertyChangedType.MatrixChanged:
                        if (CoreApplicationManager.Application != null)
                        {
                            ICoreLoadableComponent k = o as ICoreLoadableComponent;
                            var i = CoreApplicationManager.Application.BrushRegister;
                            //reload brush
                            if ((i != null) && ((k != null) && !(k.IsLoading)) || ((k == null) && (m_owner != null)))
                            {
                                i.Reload(this);
                            }
                        }
                        break;
                }
        }
        /// <summary>
        /// override this method to initialize the brush before register
        /// </summary>
        protected virtual void InitBrush()
        {            
            this.SetSolidColor(Colorf.White);            
        }
        public void SetHatchBrush(Colorf frontColor,
       Colorf backColor,
       enuHatchStyle hatchStyle)
        {
            if (this.m_Colors.Length == 2)
            {
                this.m_Colors[0] = frontColor;
                this.m_Colors[1] = backColor;
            }
            else
                this.m_Colors = new Colorf[] { frontColor, backColor };
            this.m_hatchStyle = hatchStyle;
            this.m_BrushType = enuBrushType.Hatch;
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        public void SetLinearBrush(
            Colorf[] colors,
            float[] factors,
            float[] positions,
            float Angle,
            enuLinearMode linMode,
            enuWrapMode wrapMode,
            bool gammaCorrection,
            enuLinearOperator linOperator,
            float focus,
            float scale,
            bool AutoSize,
            Rectanglei rectangle)
        {
            if ((colors == null) ||
                (colors.Length < 2))
                return;
            this.m_Colors = colors;
            this.m_LinearMode = linMode;
            this.m_Angle = Angle;
            this.m_autosize = AutoSize;
            this.m_gammaCorrection = gammaCorrection;
            this.m_factors = factors;
            this.m_positions = positions;
            this.m_wrapMode = wrapMode;
            this.m_linearOperator = linOperator;
            this.m_Focus = focus;
            this.m_Scale = scale;
            this.m_Angle = Angle;
            this.m_bound = rectangle;
            this.m_BrushType = enuBrushType.LinearGradient;
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        public void SetLinearBrush(Colorf startColor, Colorf endColor, float angle)
        {
            this.SetLinearBrush(new Colorf[] { startColor, endColor },
                null, null, angle, enuLinearMode.Dual,
                 enuWrapMode.TileFlipXY ,
                  false, enuLinearOperator.None, 0.0f, 0.0f, true, Rectanglei.Empty);
        }


        /// <summary>
        /// texture brush from icore bitmap
        /// </summary>
        /// <param name="bitmap">bitmap to get</param>
        /// <param name="wrapMode">wrap mode</param>
        /// <param name="AutoSize"></param>
        /// <param name="rectangle"></param>
        /// <param name="AutoTransform"></param>
        public void SetTextureBrush(
            ICoreBitmap bitmap,
            enuWrapMode wrapMode,
            bool AutoSize,
            Rectanglei rectangle,
            bool AutoTransform)
        {
            if (bitmap == null)
                return;
            this.m_Bitmap = bitmap.Clone() as ICoreBitmap;
            this.m_autosize = AutoSize;
            this.m_bound = rectangle;
            this.m_BrushType = enuBrushType.Texture;
            this.m_wrapMode = wrapMode;
            this.m_textureAutoTransform = AutoTransform;
            this.m_filename = null;
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        public void SetTextureBrush(
          string bitmapfile,
          enuWrapMode wrapMode,
          bool AutoSize,
          Rectanglei rectangle,
          bool AutoTransform)
        {
            if (!File.Exists(bitmapfile))
                return;
            ICoreBitmap bmp = CoreApplicationManager.Application.ResourcesManager.CreateBitmapFromFile(bitmapfile);
            if (bmp == null)
                return;
            this.m_Bitmap = bmp;
            this.m_autosize = AutoSize;
            this.m_bound = rectangle;
            this.m_BrushType = enuBrushType.Texture;
            this.m_wrapMode = wrapMode;
            this.m_textureAutoTransform = AutoTransform;
            this.m_filename = bitmapfile;
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        /// <summary>
        /// set the texture resources
        /// </summary>
        /// <param name="texture"></param>
        public void SetTextureResource(
            ICoreTextureResource texture, enuWrapMode wrapMode,
            bool AutoSize,
            Rectanglei rectangle,
             bool AutoTransform)
        {
            if (texture == null)
                return;
            ICoreBitmap bmp = texture.GetBitmap();
            if (bmp == null)
                return;
            this.m_Bitmap = bmp.Clone() as ICoreBitmap;
            this.m_autosize = AutoSize;
            this.m_bound = rectangle;
            this.m_BrushType = enuBrushType.Texture;
            this.m_wrapMode = wrapMode;
            this.m_textureAutoTransform = AutoTransform;
            this.m_filename = texture.Id;
            this.m_textureRes = texture;
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colors">colors to use</param>
        /// <param name="factors">factors depending of the number of colors</param>
        /// <param name="positions">positions depending of the number of color</param>
        /// <param name="linMode">linear mode selection</param>
        /// <param name="wrapMode">wrap mode</param>
        /// <param name="pathFocusScale">path focus scale</param>
        /// <param name="pathMode">path mode</param>
        /// <param name="linOperator">linear operator</param>
        /// <param name="focus">linear focus</param>
        /// <param name="scale">scale requirement</param>
        /// <param name="OnColorPerVertex">one color per vertex mode</param>
        /// <param name="Autosize">autosize mode </param>
        /// <param name="rectangle">rectangle if autosize mode is false</param>
        /// <param name="AutoCenter">auto center</param>
        /// <param name="Center">auto center location if autocenter = true</param>
        public void SetPathBrush(
            Colorf[] colors,
            float[] factors,
            float[] positions,
    enuLinearMode linMode,
    enuWrapMode wrapMode,
            Vector2f pathFocusScale,
    enuPathBrushMode pathMode,
    enuLinearOperator linOperator,
    float focus,
    float scale,
            bool OnColorPerVertex,
    bool Autosize,
    Rectanglei rectangle,
    bool AutoCenter, //alow auto center
    Vector2f Center  //define the center point according to AutoCenter field
            )
        {
            if ((colors == null) ||
            (colors.Length < 2))
                return;
            this.m_Colors = colors;
            this.m_factors = factors;
            this.m_positions = positions;
            this.m_Focus = focus;
            this.m_Scale = scale;
            this.m_linearOperator = linOperator;
            this.m_LinearMode = linMode;
            this.m_wrapMode = wrapMode;
            this.m_autosize = Autosize;
            this.m_bound = rectangle;
            this.m_PathBrushMode = pathMode;
            this.m_BrushType = enuBrushType.PathGradient;
            this.m_autoCenter = AutoCenter;
            this.m_PathCenter = Center;
            this.m_PathFocusScale = pathFocusScale;
            this.m_OneColorPerVertex = OnColorPerVertex;            
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        public virtual void CopyDefinition(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;
            string[] tb = value.Trim().Split(new char[] { ';', ':' });

            if (tb.Length == 1)
            {
                if (this.SetShortCutValue(tb[0]))
                {
                    OnBrushDefinitionChanged(EventArgs.Empty);
                    return;
                }
            }
            //default property
            this.m_autosize = true;
            this.m_autoCenter = true;

            for (int i = 0; i < tb.Length; i += 2)
            {
                if ((i + 1) >= tb.Length)
                    break;
                switch (tb[i].Trim().ToLower ())
                {
                    case "type":
                        this.m_BrushType = (enuBrushType)Enum.Parse(typeof(enuBrushType), tb[i + 1]);
                        break;
                    case "wrapmode":
                        this.m_wrapMode = (enuWrapMode)Enum.Parse(typeof(enuWrapMode), tb[i + 1]);
                        break;
                    case "linearmode":
                        this.m_LinearMode = (enuLinearMode)Enum.Parse(typeof(enuLinearMode), tb[i + 1]);
                        break;
                    case "pathbrushmode":
                        this.m_PathBrushMode = (enuPathBrushMode)Enum.Parse(typeof(enuPathBrushMode), tb[i + 1]);
                        break;
                    case "linearoperator":
                        this.m_linearOperator = (enuLinearOperator)Enum.Parse(typeof(enuLinearOperator), tb[i + 1]);
                        break;
                    case "focus": this.m_Focus = Convert.ToSingle(tb[i + 1]); break;
                    case "scale": this.m_Scale = Convert.ToSingle(tb[i + 1]); break;
                    case "pathfocusscale":
                        this.m_PathFocusScale = Vector2f.ConvertFromString(tb[i + 1]);
                        break;
                    case "angle":
                        this.m_Angle = float.Parse(tb[i + 1]);
                        break;
                    case "autosize":
                        this.m_autosize = Convert.ToBoolean(tb[i + 1]);
                        break;
                    case "autocenter":
                        this.m_autoCenter = Convert.ToBoolean(tb[i + 1]);
                        break;
                    case "bounds":
                        this.m_bound = Rectanglef.ConvertFromString(tb[i + 1]);
                        break;
                    case "onecolorpervertex":
                        this.m_OneColorPerVertex = Convert.ToBoolean(tb[i + 1]);
                        break;
                    case "gammacorrection":
                        this.m_gammaCorrection = Convert.ToBoolean(tb[i + 1]);
                        break;
                    case "colors":
                        string[] cls = tb[i + 1].Trim().Split(' ');
                        List<Colorf> tcls = new List<Colorf>();
                        foreach (string item in cls)
                        {
                            if (string.IsNullOrEmpty(item)) continue;
                            tcls.Add(Colorf.Convert(item));
                        }
                        this.m_Colors = tcls.ToArray();
                        break;
                    case "positions":
                        {
                            string[] v_tcls = tb[i + 1].Trim().Split(' ');
                            List<float> v_pos = new List<float>();
                            foreach (string item in v_tcls)
                            {
                                v_pos.Add(float.Parse(item));
                            }
                            this.m_positions = v_pos.ToArray();
                        }
                        break;
                    case "factors":
                        {
                            string[] v_tcls = tb[i + 1].Trim().Split(' ');
                            List<float> v_pos = new List<float>();
                            foreach (string item in v_tcls)
                            {
                                v_pos.Add(float.Parse(item));
                            }
                            this.m_factors = v_pos.ToArray();
                        }
                        break;
                    case "hatchstyle":
                        this.m_hatchStyle = (enuHatchStyle)Enum.Parse(typeof(enuHatchStyle), tb[i + 1]);
                        break;
                    case "center":
                        this.m_PathCenter = Vector2f.ConvertFromString(tb[i + 1]);
                        break;
                    case "filename":
                        if (this.m_owner != null)
                        {
                            this.m_filename = tb[i + 1];
                            try
                            {
                                ICoreBitmap bmp =CoreApplicationManager.Application.ResourcesManager .CreateBitmapFromFile (this.m_filename) as ICoreBitmap;
                                if (bmp != null)
                                {
                                    this.m_Bitmap = bmp;
                                    this.m_BrushType = enuBrushType.Texture;
                                }
                                else
                                    this.m_BrushType = enuBrushType.Solid;
                            }
                            catch
                            {
                            }
                        }
                        break;
                    default:
                        if (tb[i].Trim().StartsWith("#"))
                        {
                            string ct = tb[i].Trim();
                            if (CoreResourceIdentifier.MatchString(ct))
                            {
                                this.m_filename = CoreResourceIdentifier.GetResourceId(ct);
                                if (this.Owner != null)
                                {
                                    this.m_textureRes = CoreApplicationManager.Application.ResourcesManager.CreateBitmapFromFile (
                                        this.m_filename) as ICoreTextureResource;
                                    if (this.m_textureRes != null)
                                        this.m_BrushType = enuBrushType.Texture;
                                    else
                                    {
                                        this.m_BrushType = enuBrushType.Solid;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            OnBrushDefinitionChanged(EventArgs.Empty);
        }

        private bool SetShortCutValue(string p)
        {
            //get the property
            FieldInfo finfo = typeof(CoreBrushes).GetField(p, 
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic|
                 BindingFlags.IgnoreCase  );
            if (finfo != null)
            {
                CoreBrush r = finfo.GetValue(null) as CoreBrush;
                this.Copy(r);
                return true ;
            }
            PropertyInfo v_prinfo = typeof(Colorf).GetProperty(p, BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.Public );
            if ((v_prinfo != null) && (v_prinfo.PropertyType == typeof(Colorf)))
            {
                this.SetSolidColor((Colorf)v_prinfo.GetValue(null));
                return true ;
            }
            if (p.StartsWith("#")) {
               var g  = // Colorf.Convert (p);
                Colorf.FromString(p);
                this.SetSolidColor(g);
                return true;
            }
            return false;
        }
        private string GetWrapMode()
        {
            switch (this.BrushType)
            {
                case enuBrushType.LinearGradient:
                    if (this.WrapMode == enuWrapMode.Clamp)
                        return string.Empty;
                    return string.Format("WrapMode:{0};", this.m_wrapMode.ToString());
                case enuBrushType.PathGradient:
                case enuBrushType.Texture:
                    return string.Format("WrapMode:{0};", this.m_wrapMode.ToString());
            }
            return string.Empty;
        }
        private string GetFactors()
        {
            switch (this.BrushType)
            {
                case enuBrushType.LinearGradient:
                case enuBrushType.PathGradient:
                    if (this.m_factors != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Factors: ");
                        for (int i = 0; i < this.m_factors.Length; i++)
                        {
                            if (i > 0)
                                sb.Append(" ");
                            sb.Append(this.m_factors[i].ToString());
                        }
                        sb.Append(";");
                        return sb.ToString();
                    }
                    break;
            }
            return string.Empty;
        }
        private string GetPositions()
        {
            switch (this.BrushType)
            {
                case enuBrushType.LinearGradient:
                case enuBrushType.PathGradient:
                    if (this.m_positions != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Positions: ");
                        for (int i = 0; i < this.m_positions.Length; i++)
                        {
                            if (i > 0)
                                sb.Append(" ");
                            sb.Append(this.m_positions[i].ToString());
                        }
                        sb.Append(";");
                        return sb.ToString();
                    }
                    break;
            }
            return string.Empty;
        }
        /// <summary>
        /// copy brush
        /// </summary>
        /// <param name="iCoreBrush"></param>
        public virtual void Copy(ICoreBrush iCoreBrush)
        {
            if (iCoreBrush == null) return;
            Type t = typeof(CoreBrush);
            object v_obj = null;
            System.Reflection.FieldInfo[] v_tbinfo =
                   t.GetFields(System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance);
            int i = 0;
            foreach (System.Reflection.FieldInfo f in v_tbinfo)
            {
                i++;
                //ignore field 
                switch (f.Name)
                {
                    case "m_owner":
                    case "m_Brush":
                    case "m_pen":
                    case "m_Bitmap":
                    case "BrushDefinitionChanged":
                    case "m_textureRes":
                    case "m_suspendBrush":
                    case "m_isDisposed":
                        continue;
                }
                v_obj = f.GetValue(iCoreBrush);
                if (v_obj == null) continue;
                if (v_obj is ICloneable)
                {
                    v_obj = (v_obj as ICloneable).Clone();
                }
                f.SetValue(this, v_obj);
            }
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        public override bool Equals(object obj)
        {
            string h = null;
            if (obj is string)
            {
                h = this.GetDefinition().ToLower().Trim ();                
                return h == ((string)obj).Trim().ToLower();
            }
            else
            {
                ICoreBrush v_b = obj as ICoreBrush;
                if (v_b == null)
                    return false;
                h = this.GetDefinition().ToLower().Trim();
                string s = v_b.GetDefinition().ToLower().Trim();   
                return (s.Equals(h));
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// set the current brush
        /// </summary>
        /// <param name="brush"></param>
        public void SetBrush(ICoreBrush brush)
        {
            if ((brush == null) || (brush == this.m_Brush))
                return;
            this.m_Brush = brush;
            this.m_BrushType = enuBrushType.Custom;
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        public void LoadResources(ICoreDeserializerResources resources)
        {
            if (this.m_filename == null)
                return;
            if ((this.BrushType == enuBrushType.Texture) && this.m_filename.StartsWith("#"))
            {
                string v_f = this.m_filename.Replace("#", "");
                ICoreResourceItem v = resources[v_f];
                if (v != null)
                {
                    this.m_Brush  = v.GetData() as ICoreBrush;
                }
            }
        }
        public override object GetData()
        {
            return null;
        }
       
        public override bool Register(ICoreResourceContainer container)
        {
            if ((this.BrushType == enuBrushType.Texture) && (container != null))
            {
                return false;
            }
            return base.Register(container);
        }
     
        void reader_LoadingComplete(object sender, CoreLoadingCompleteEventArgs e)
        {
            if (this.BrushType == enuBrushType.Solid && !string.IsNullOrEmpty(this.m_filename))
            {
                //this.m_textureRes = e.Context.GetRes(this.m_filename) as ICoreTextureResource;
                //if (this.m_textureRes != null)
                //{
                //    this.m_BrushType = enuBrushType.Texture;                    
                //}
            }
        }
        public void ReplaceColor(Colorf oldColor, Colorf newColor)
        {
            bool v_cl = false;
            for (int i = 0; i < this.m_Colors.Length; i++)
            {
                if (this.m_Colors[i].Equals(oldColor))
                {
                    this.m_Colors[i] = newColor;
                    v_cl = true;
                }
            }
            if (v_cl)
            {
                this.OnBrushDefinitionChanged(EventArgs.Empty);
            }
        }
        public ICoreBrushOwner Owner
        {
            get { return this.m_owner; }
        }
        public void SetSolidColor(Colorf color)
        {
            if (this.m_BrushType == enuBrushType.Solid)
            {      
                if ((this.m_Colors !=null) &&  (this.m_Colors .Length == 1))
                {                
                    if (color.Equals(m_Colors[0]))
                    {//color don't changed
#if DEBUG
                        OnBrushDefinitionChanged(EventArgs.Empty);
#endif
                        return;
                    }
                    m_Colors[0] = color;                    
                }
                else
                {
                    if ((this.m_Colors == null) || (this.m_Colors.Length > 1))
                        this.m_Colors = new Colorf[] { color };
                    else
                        this.m_Colors[0] = color;
                }
            }
            else
            {
                this.m_BrushType = enuBrushType.Solid;
                if ((this.m_Colors == null) || (this.m_Colors.Length > 1))
                    this.m_Colors = new Colorf[] { color };
                else
                    this.m_Colors[0] = color;
            }
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        protected virtual void SuspendBrush()
        {
            if (!this.m_suspendBrush )
                this.m_suspendBrush = true;
        }
        protected virtual void ResumeBrush()
        {
            if (this.m_suspendBrush)
            {
                this.m_suspendBrush = false;
                OnBrushDefinitionChanged(EventArgs.Empty);
            }
        }
        /// <summary>
        /// dispose the brush
        /// </summary>
        public override  void Dispose()
        {
            CoreApplicationManager.Application.BrushRegister.Unregister(this);
            this.IsDisposed = true;
            OnDisposed(EventArgs.Empty);
        }

        private void OnDisposed(EventArgs eventArgs)
        {
            if (this.Disposed != null)
                this.Disposed(this, eventArgs);
        }

        public string GetDefinition(IGK.ICore.Codec.CoreXMLSerializer serializer ) {
            string b = GetDefinition();
            if ((this.BrushType == enuBrushType.Texture )&& (this.Bitmap !=null))
            {
                if (serializer.EmbedBitmap)
                {
                    string id = serializer.Resources.Register(this.Bitmap as ICoreResourceItem);
                    b = b + " " + string.Format("Resource: @resouces:bitmap/{0}", id);
       
                }
                else {
                    if (PathUtils.CreateDir(serializer.BaseDir))
                    {
                        string f = PathUtils.GetTempFileWithExtension("data");
                        string g = Path.Combine(serializer.BaseDir, Path.GetFileName(f));
                        this.Bitmap.Save(g, CoreBitmapFormat.Png);
                        File.Move(f, g);
                        b = b + " " + string.Format("FileName : {0}",PathUtils.GetRelativePath(serializer.BaseDir,  g));
                    }
                }
            }
            return b;
        }

        public override string GetDefinition()
        {
            if (this.BrushType == enuBrushType.Solid) {
                if (this.Colors[0].A == 0)
                {
                    return "transparent";
                }
                else {
                    return Colorf.ConvertToString(this.Colors[0]);
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Type:{0};", this.BrushType));
            sb.Append(GetColorsDefinition());
            switch (this.BrushType)
            {
                case enuBrushType.Solid:
                    break;
                case enuBrushType.Custom:
                    return sb.ToString();
                case enuBrushType.Hatch:
                    sb.Append(string.Format("HatchStyle:{0};", this.m_hatchStyle));
                    break;
                case enuBrushType.LinearGradient:
                    sb.Append(GetLinearDefinition());
                    break;
                case enuBrushType.PathGradient:
                    sb.Append(GetPathGradientDefinition());
                    break;
                case enuBrushType.Texture:
                    //create
                    if ((this.m_textureRes != null) && this.m_textureRes.IsRegistered())
                    {
                        sb.Clear();
                        sb.Append(new CoreResourceIdentifier(this.m_textureRes).ToString() + ";");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.m_filename))
                        {
                            sb.Append(string.Format("FileName:{0};", this.m_filename));
                        }
                    }
                    if (!this.m_autosize)
                    {
                        sb.Append(string.Format("AutoSize:False;"));
                        sb.Append(string.Format("Bounds:{0} {1} {2} {3};", this.m_bound.X,
                     this.m_bound.Y,
                     this.m_bound.Width,
                     this.m_bound.Height));
                    }
                    sb.Append(string.Format("WrapMode: {0};", this.WrapMode));
                    break;
                default:
                    break;
            }
            try
            {
                //if ((this.Transform != null) && (!this.Transform.IsIdentity))
                //{
                //    CoreMatrixTypeConverter c = new CoreMatrixTypeConverter();
                //    sb.Append(string.Format("Matrix:{0};", c.ConvertToString(this.Transform)));
                //}
            }
            catch
            {
            }
           string r = sb.ToString();
            if (true) {
                if (r == CoreConstant.TRANSPARENT_COLOR_DEF)
                    return Colorf.Transparent.GetName ();//"transparent";
            }
            return r;
        }
        protected string GetColorsDefinition()
        {
            switch (this.BrushType)
            {
                case enuBrushType.Texture:
                    return string.Empty;
                case enuBrushType.Solid:
                    break;
                case enuBrushType.Hatch:
                    break;
                case enuBrushType.LinearGradient:
                    break;
                case enuBrushType.PathGradient:
                    break;
                case enuBrushType.Custom:
                    return string.Empty;
                default:
                    break;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("Colors:");
            for (int i = 0; i < this.m_Colors.Length; i++)
            {
                if (i != 0)
                    sb.Append(" ");
                if (this.m_Colors[i].A == 0)
                {
                    sb.Append(Colorf.ConvertToString(Colorf.Transparent));
                }
                else
                    sb.Append(Colorf.ConvertToString(this.m_Colors[i]));
            }
            sb.Append(";");
            return sb.ToString();
        }
        private string GetPathGradientDefinition()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("LinearMode:{0};", this.m_LinearMode.ToString()));
            sb.Append(GetFactors());
            sb.Append(GetPositions());
            if (this.m_PathBrushMode != enuPathBrushMode.Path)
            {
                sb.Append(string.Format("PathBrushMode:{0};", this.m_PathBrushMode.ToString()));
            }
            if (!this.m_autosize)
            {
                sb.Append(string.Format("AutoSize:{0};", this.m_autosize.ToString()));
                sb.Append(string.Format("Bounds:{0};",
                    string.Format("{0} {1} {2} {3}", this.m_bound.X,
                    this.m_bound.Y,
                    this.m_bound.Width,
                    this.m_bound.Height)));
            }
            if (!this.m_autoCenter)
            {
                sb.Append(string.Format("AutoCenter:{0};", (!this.m_autosize).ToString()));
                sb.Append("Center:");
                sb.Append(PathCenter.X + " ");
                sb.Append(PathCenter.Y + ";");
            }
            sb.Append(GetWrapMode());
            sb.Append(GetLinearOperationDefinition());
            sb.Append("PathFocusScale:");
            sb.Append(PathFocusScale.X + " ");
            sb.Append(PathFocusScale.Y + ";");
            if (this.OneColorPerVertex)
                sb.Append("OneColorPerVertex:True;");
            return sb.ToString();
        }
        private string GetLinearDefinition()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("LinearMode:{0};", this.m_LinearMode.ToString()));
            if (this.m_Angle != 0.0f)
                sb.Append(string.Format("Angle:{0};", this.m_Angle.ToString()));
            if (!this.m_autosize)
            {
                sb.Append(string.Format("AutoSize:{0};", this.m_autosize.ToString()));
                sb.Append(string.Format("Bounds:{0};",
                    string.Format("{0} {1} {2} {3}", this.m_bound.X,
                    this.m_bound.Y,
                    this.m_bound.Width,
                    this.m_bound.Height)));
            }
            if (this.m_wrapMode != enuWrapMode.Tile)
                sb.Append(GetWrapMode());
            if (this.m_gammaCorrection)
                sb.Append(string.Format("GammaCorrection:{0};", this.m_gammaCorrection.ToString()));
            sb.Append(GetLinearOperationDefinition());
            sb.Append(GetFactors());
            sb.Append(GetPositions());
            return sb.ToString();
        }
        private string GetLinearOperationDefinition()
        {
            StringBuilder sb = new StringBuilder();
            if (this.m_linearOperator != enuLinearOperator.None)
            {
                sb.Append(string.Format("LinearOperator:{0};", this.m_linearOperator));
                sb.Append(string.Format("Focus:{0};", this.m_Focus));
                sb.Append(string.Format("Scale:{0};", this.m_Scale));
            }
            return sb.ToString();
        }
    }
}

