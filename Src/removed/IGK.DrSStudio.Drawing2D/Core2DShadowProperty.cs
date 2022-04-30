

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DShadowProperty.cs
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
file:Core2DShadowProperty.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    using System.ComponentModel;
    using System.Drawing.Design;
    [TypeConverter(typeof(Core2DShadowProperty.Core2DShadowPropertyConvertor ))]
    /// <summary>
    /// return true
    /// </summary>
    public class Core2DShadowProperty :
        CoreDependencyObject ,
        ICoreWorkingConfigurableObject,
        ICoreWorkingDefinitionObject 
    {
        private Colorf m_ShadowColor;
        private bool m_IsClipped;
        private enuShadowPropertyBrushMode m_BrushMode;
        private Vector2f m_Offset;
        private int m_Blur;
        private ICoreBrush m_brush;
        /// <summary>
        ///  get the current brush
        /// </summary>
        public ICoreBrush Brush
        {
            get {
                return this.m_brush;
            }
        }
        public static readonly CoreDependencyProperty BlurProperty = CoreDependencyProperty.Register(
            "Blur", typeof(int), typeof(Core2DShadowProperty), new CoreDependencyPropertyMetadata(0));
        public static readonly CoreDependencyProperty OpacityProperty = CoreDependencyProperty.Register(
            "Opacity", typeof(float), typeof(Core2DShadowProperty), new CoreDependencyPropertyMetadata(1.0f));
        /// <summary>
        /// get or set blur
        /// </summary>
        public int Blur
        {
            get { 
                return (int)GetValue (BlurProperty); 
            }
            set
            {
                this.SetValue(BlurProperty, value);            
                OnPropertyChanged(EventArgs.Empty);
            }
        }
        public override string ToString()
        {
            return "ShadowProperties";
        }
        class Core2DShadowPropertyConvertor : TypeConverter 
        {
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
 	             return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                List<PropertyDescriptor > d = new List<PropertyDescriptor> ();
                foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(value))
                {
                    if (item.IsBrowsable)
                        d.Add(item);
                }
                return new PropertyDescriptorCollection(d.ToArray());
            }
        }
        /// <summary>
        /// get or set brushmode
        /// </summary>
        public enuShadowPropertyBrushMode BrushMode
        {
            get { return m_BrushMode; }
            set
            {
                if (m_BrushMode != value)
                {
                    m_BrushMode = value;                    
                    OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// get or set if this shadow property is clipped
        /// </summary>
        public bool IsClipped
        {
            get { return m_IsClipped; }
            set
            {
                if (m_IsClipped != value)
                {
                    m_IsClipped = value;
                    OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// get or set opacity
        /// </summary>
        public float Opacity
        {
            get { 
                return (float)this.GetValue(OpacityProperty) ; 
            }
            set
            {
                this.SetValue(OpacityProperty, Colorf.Trim(value));
            }
        }
        /// <summary>
        /// get or set offset
        /// </summary>       
        public Vector2f Offset
        {
            get { return m_Offset; }
            set
            {
                if (m_Offset != value)
                {
                    m_Offset = value;
                    OnPropertyChanged(EventArgs.Empty);
                }
            }
        }
        private void OnPropertyChanged(EventArgs eventArgs)
        {
            if (this.m_Element !=null)
                this.m_Element.InvalidateDesignSurface(true);
        }
        #region ICoreWorkingConfigurableObject Members
        public  ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        public IGK.DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            var group = parameters.AddGroup("ShadowProperty");
            group.AddActions (new CoreParameterAction("BrushColor", "lb.ShadowBrush.Caption", new EditBrushAction(this.m_brush , enuBrushSupport.All )));
            group.AddItem(this.GetType().GetProperty("IsClipped"));
            group.AddItem(this.GetType().GetProperty("BrushMode"));
            group.AddItem("Offset", "lb.Offset.caption", offsetChanged);
            group.AddTrackbar ("Opacity", "lb.Opacity", 0, 100,Convert .ToInt32 (this.Opacity * 100.0f), opacityChanged);
            group.AddItem(this.GetType().GetProperty("Blur"));
            return parameters;
        }
        void opacityChanged(object sender, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            this.Opacity = Convert.ToSingle(e.Value) / 100.0f;
        }
        void offsetChanged(object sender, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            string s = e.Value as string;
            if (string.IsNullOrEmpty(s))
                return;
            string[] tab = s.Split(';');
            switch (tab.Length)
            {
                case 1:
                    CoreUnit d = tab[0];
                    float c =  (d as ICoreUnitPixel).Value ;
                    this.Offset = new Vector2f(c, c);
                    break;
                case 2:
                    CoreUnit d1 = tab[0];
                    CoreUnit d2 = tab[1];
                    float x = (d1 as ICoreUnitPixel).Value;
                    float y = (d2 as ICoreUnitPixel).Value;
                    this.Offset = new Vector2f(x, y);
                    break;
            }
        }
        [Browsable (false )]
        public new string Id
        {
            get { return "ShadowProprety"; }
        }
        public void CopyDefinition(string str)
        {
            if (string.IsNullOrEmpty(str))
                return;
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(
@"shadowbrush\s*:\s*\[(?<value>(.)+);\]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            string v_out = str;//copy string
            foreach (System.Text.RegularExpressions.Match m in rg.Matches(str))
            {
                this.m_brush.CopyDefinition (m.Groups["value"].Value);
                v_out = v_out.Remove (m.Index, m.Length);
            }
            str = v_out;
            string[] tab =str.Split(new char[]{';',':'});
            this.IsLoading = true;
            for (int i = 0; i < tab.Length; i+=2)
			{
                if ((i + 1) >= tab.Length) break;
                switch (tab[i].Trim().ToLower())
                {
                    case "color": this.m_ShadowColor = Colorf.FromString(tab[i + 1]); break;
                    case "offset": this.m_Offset = Vector2f.ConvertFromString(tab[i + 1]); break;
                    case "brushmode": this.m_BrushMode =  (enuShadowPropertyBrushMode)Enum.Parse (typeof (enuShadowPropertyBrushMode ), tab[i+1]); break ;
                    case "isclipped": this.m_IsClipped = Convert.ToBoolean(tab[i + 1]); break;
                    case "blur": this.m_Blur = Convert.ToInt32(tab[i + 1]); break;
                    case "shadowbrush"://ignore
                        break;
                }
			}
            this.IsLoading = false;
            OnLoadingComplete(EventArgs.Empty);
        }
        public virtual string GetDefinition()
        {
            if (this.m_Element.AllowShadow == false)
                return null;
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Offset:{0};",
                this.Offset.X + " "+ this.Offset.Y ));
            sb.Append(string.Format ("BrushMode:{0};", this.BrushMode));
            sb.Append(string.Format("IsClipped:{0};", this.IsClipped));
            sb.Append (string.Format ("ShadowBrush:{0};", this.m_brush.GetDefinition ()));
            return sb.ToString();
        }
        public string GetDefinition(IGK.DrSStudio.Codec.IXMLSerializer seri)
        {
            return this.GetDefinition();
        }
        private ICore2DDrawingShadowElement m_Element;
        [Browsable(false)]
        public ICore2DDrawingShadowElement Element
        {
            get { return m_Element; }
        }
        #endregion
        public Core2DShadowProperty(ICore2DDrawingShadowElement target)
        {
            this.m_Element = target;
            this.m_ShadowColor = Colorf.Black;
            this.m_IsClipped = false;
            this.m_Offset = new Vector2f(2, 2);
            this.m_brush = new CoreBrush(target);
            this.m_brush.SetSolidColor(Colorf.Black);
            this.m_brush.BrushDefinitionChanged += m_brush_BrushDefinitionChanged;
        }
        void m_brush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BrushChanged));
        }
    }
}

