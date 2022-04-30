

/*
IGKDEV @ 2008 - 2014
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
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:CoreBrush.cs
*/
using IGK.ICore;using IGK.DrSStudio.ComponentModel;
using IGK.DrSStudio.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    public class CoreBrush :CoreResourceItemBase,
        ICoreBrush,
        ICoreResourceItem,
        ICoreWorkingLoadingIdentifiableDefinition
    {
        private ICore2DBrushOwner m_owner;
        private enuBrushType m_BrushType;
        private ICoreBrush m_Brush; 
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
        public Colorf[] Colors { get { return this.m_Colors; } }
        public enuHatchStyle HatchStyle { get { return this.m_hatchStyle; } }
        public bool AutoSize { get { return this.m_autosize; } }
        public enuWrapMode WrapMode { get { return this.m_wrapMode; } }
        public Rectanglef Bound { get { return this.m_bound; } }
        public ICoreBitmap Bitmap { get { return this.m_Bitmap; } }
        public float[] Factors { get { return this.m_factors; } }
        public float[] Positions { get { return this.m_positions; } }
        public float Scale { get { return this.m_Scale; } }
        public float Focus { get { return this.m_Focus; } }
        internal static readonly CoreBrush White;
        public float Angle { get { return this.m_Angle; } }
        public enuLinearOperator LinearOperator { get { return this.m_linearOperator; } }
        public bool GammaCorrection { get { return this.m_gammaCorrection; } }
        public bool AutoCenter { get { return this.m_autoCenter; } }
        public bool TextureAutoTranform { get { return this.m_textureAutoTransform; } }
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
        static CoreBrush()
        {
            White = new CoreBrush(null);
            White.SetSolidColor(Colorf.White);
        }
        //for internal purpose
        private CoreBrush()
        {
            this.SetSolidColor(Colorf.Black);
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
            //this.m_Bitmap = CoreSystem.Open.FromFile(bitmapfile) as Bitmap;
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
        public void SetTextureResource(ICoreTextureResource texture, enuWrapMode wrapMode,
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
        public virtual void ReloadBrush()
        {
        }
        public CoreBrush(Colorf color)
            : this()
        {
            this.SetSolidColor(color);
        }
        public CoreBrush(ICore2DBrushOwner obj)
        {
            this.m_owner = obj;
            this.m_Brush = null;
            this.m_filename = string.Empty;
            this.m_positions = null;
            this.m_factors = null;
            this.m_BrushType = enuBrushType.Solid;
            this.m_Colors = new Colorf[] { Colorf.Black };
            this.m_wrapMode = enuWrapMode.TileFlipXY;
            this.m_Angle = 0.0f;
            this.m_autosize = true;
            this.m_autoCenter = true;
            this.m_hatchStyle = enuHatchStyle.ZigZag;
            this.m_PathBrushMode = enuPathBrushMode.Path;
            this.m_gammaCorrection = false;
            this.m_matrix = new Matrix();
            try
            {
                this.SetSolidColor(Colorf.Black);
            }
            catch (Exception)
            {
            }
            if (this.m_owner != null)
            {
                this.m_owner.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(owner_PropertyChanged);
                if (this.m_owner is ICoreLoadableComponent)
                {
                    (this.m_owner as ICoreLoadableComponent).LoadingComplete += new EventHandler(_LoadingComplete);
                }
            }
        }
        void owner_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (e is Core2DDrawingElementPropertyChangeEventArgs)
            {
                switch ((enu2DPropertyChangedType)e.ID)
                {
                    case enu2DPropertyChangedType.DefinitionChanged:
                    case enu2DPropertyChangedType.MatrixChanged:
                        ReloadBrush();
                        break;
                }
            }
            else
            {
                switch (e.ID)
                {
                    case enuPropertyChanged.Definition:
                        ReloadBrush();
                        break;
                }
            }
        }
        #region ICoreBrush Members
        public ICore2DBrushOwner Owner
        {
            get
            {
                return this.m_owner;
            }
        }
        #endregion
        #region ICoreDisposableObject Members
        public bool IsDisposed
        {
            get
            {
                return (this.m_Brush == null);
            }
        }
        #endregion
        #region IDisposable Members
        public virtual void Dispose()
        {
            if (this.m_matrix != null)
            {
                this.m_matrix.Dispose();
                this.m_matrix = null;
            }            
        }
        #endregion
        #region ICoreBrush Members
        public void SetSolidColor(Colorf color)
        {
            if (this.m_BrushType == enuBrushType.Solid)
            {
                    m_Colors[0] = color;
            }
            else {
                 this.m_Colors = new Colorf[] { color };
                this.m_BrushType = enuBrushType.Solid ;
            }
            OnBrushDefinitionChanged(EventArgs.Empty);
        }
        #endregion
        #region ICoreBrush Members
        public virtual string GetDefinition()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Type:{0};", this.BrushType));
            sb.Append(GetColorsDefinition());
            switch (this.BrushType)
            {
                case enuBrushType.Solid:
                    break;
                case enuBrushType.Custom:
                    //sb.Append(GetCustomBrushDefinition());
                    throw new NotImplementedException();
                    //return sb.ToString();
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
                        sb.Append(string.Format("Bound:{0} {1} {2} {3};", this.m_bound.X,
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
                if ((this.Transform != null) && (!this.Transform.IsIdentity))
                {
                    CoreMatrixTypeConverter c = new CoreMatrixTypeConverter();
                    sb.Append(string.Format("Matrix:{0};", c.ConvertToString(this.Transform)));
                }
            }
            catch
            {
            }
            return sb.ToString();
        }
        private string GetColorsDefinition()
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
                sb.Append(string.Format("Bound:{0};",
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
                sb.Append(string.Format("Bound:{0};",
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
        public virtual void CopyDefinition(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;
            string[] tb = value.Split(new char[] { ';', ':' });
            bool v_fromResources = false;
            for (int i = 0; i < tb.Length; i += 2)
            {
                if ((i + 1) >= tb.Length)
                    break;
                switch (tb[i].Trim())
                {
                    case "Type":
                        this.m_BrushType = (enuBrushType)Enum.Parse(typeof(enuBrushType), tb[i + 1]);
                        break;
                    case "WrapMode":
                        this.m_wrapMode = (enuWrapMode)Enum.Parse(typeof(enuWrapMode), tb[i + 1]);
                        break;
                    case "LinearMode":
                        this.m_LinearMode = (enuLinearMode)Enum.Parse(typeof(enuLinearMode), tb[i + 1]);
                        break;
                    case "PathBrushMode":
                        this.m_PathBrushMode = (enuPathBrushMode)Enum.Parse(typeof(enuPathBrushMode), tb[i + 1]);
                        break;
                    case "LinearOperator":
                        this.m_linearOperator = (enuLinearOperator)Enum.Parse(typeof(enuLinearOperator), tb[i + 1]);
                        break;
                    case "Focus": this.m_Focus = Convert.ToSingle(tb[i + 1]); break;
                    case "Scale": this.m_Scale = Convert.ToSingle(tb[i + 1]); break;
                    case "PathFocusScale":
                        this.m_PathFocusScale = Vector2f.ConvertFromString(tb[i + 1]);
                        break;
                    case "Angle":
                        this.m_Angle = float.Parse(tb[i + 1]);
                        break;
                    case "AutoSize":
                        this.m_autosize = Convert.ToBoolean(tb[i + 1]);
                        break;
                    case "AutoCenter":
                        this.m_autoCenter = Convert.ToBoolean(tb[i + 1]);
                        break;
                    case "Bound":
                        this.m_bound = Rectanglef.ConvertFromString(tb[i + 1]);
                        break;
                    case "OneColorPerVertex":
                        this.m_OneColorPerVertex = Convert.ToBoolean(tb[i + 1]);
                        break;
                    case "GammaCorrection":
                        this.m_gammaCorrection = Convert.ToBoolean(tb[i + 1]);
                        break;
                    case "Colors":
                        string[] cls = tb[i + 1].Trim().Split(' ');
                        List<Colorf> tcls = new List<Colorf>();
                        foreach (string item in cls)
                        {
                            if (string.IsNullOrEmpty(item)) continue;
                            tcls.Add(Colorf.Convert(item));
                        }
                        this.m_Colors = tcls.ToArray();
                        break;
                    case "Positions":
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
                    case "Factors":
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
                    case "HatchStyle":
                        this.m_hatchStyle = (enuHatchStyle)Enum.Parse(typeof(enuHatchStyle), tb[i + 1]);
                        break;
                    case "Center":
                        this.m_PathCenter = Vector2f.ConvertFromString(tb[i + 1]);
                        break;
                    case "FileName":
                        if (this.m_owner != null)
                        {
                            this.m_filename = tb[i + 1];
                            try
                            {
                                v_fromResources = true;
                                ICoreBitmap bmp = CoreResourceManager.OpenFile(this.m_filename) as ICoreBitmap;
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
                                v_fromResources = true;
                                if (this.Owner != null)
                                {
                                    this.m_textureRes = CoreServices.GetRes(this.m_filename) as ICoreTextureResource;
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
            if (v_fromResources)
            { 
                //definition changed  form resources
            }
            OnBrushDefinitionChanged(EventArgs.Empty);
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
        #endregion
        #region ICoreBrush Members
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
                switch (f.Name)
                {
                    case "m_owner":
                    case "m_Brush":
                    case "m_pen":
                    case "m_Bitmap":
                    case "BrushDefinitionChanged":
                    case "m_textureRes":
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
        #endregion
        #region ICloneable Members
        public override object Clone()
        {
            CoreBrush bp = (CoreBrush)GetType().Assembly.CreateInstance(GetType().FullName, true,
                 System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic
                 | System.Reflection.BindingFlags.Public,
                 null,
                 null,
                 null,
                 null);
            bp.CopyDefinition(this.GetDefinition());
            return bp;
        }
        #endregion
        public override bool Equals(object obj)
        {
            ICoreBrush v_b = obj as ICoreBrush;
            if (v_b == null)
                return false;
            string s = v_b.GetDefinition();
            string h = this.GetDefinition();
            return (s.Equals(h));
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #region ICoreBrush Members
        public event EventHandler BrushDefinitionChanged;
        #endregion
        /// <summary>
        /// raise the brush event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBrushDefinitionChanged(EventArgs e)
        {
            if (this.BrushDefinitionChanged != null)
                this.BrushDefinitionChanged(this, e);
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
        #region ICoreResourcesElement Members
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
                    this.m_Bitmap = v.GetData() as ICoreBitmap;
                }
            }
        }
        #endregion
        /// <summary>
        /// get the resources type
        /// </summary>
        public override enuCoreResourceType ResourceType
        {
            get { return enuCoreResourceType.Brush; }
        }
        public override object GetData()
        {
            return null;
        }
        public override string GetStringData()
        {
            return this.GetDefinition();
        }
        public override bool Register(ICoreResourceContainer container)
        {
            if ((this.BrushType == enuBrushType.Texture) && (container != null))
            {
                return false;
            }
            return base.Register(container);
        }
        public override void SetValue(string value)
        {
            this.CopyDefinition(value);
        }
        void _LoadingComplete(object sender, EventArgs e)
        {
            ReloadBrush();
        }
        void reader_LoadingComplete(object sender, CoreLoadingCompleteEventArgs e)
        {
            if (this.BrushType == enuBrushType.Solid && !string.IsNullOrEmpty(this.m_filename))
            {
                this.m_textureRes = e.Context.GetRes(this.m_filename) as ICoreTextureResource;
                if (this.m_textureRes != null)
                {
                    this.m_BrushType = enuBrushType.Texture;
                    this.m_Bitmap = this.m_textureRes.GetBitmap();
                }
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
    }
}

