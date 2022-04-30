

/*
IGKDEV @ 2008-2016
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
﻿using IGK.ICore;
using IGK.ICore.Actions;
using IGK.ICore.Dependency;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    [TypeConverter(typeof(Core2DShadowProperty.Core2DShadowPropertyConvertor))]
    /// <summary>
    /// return true
    /// </summary>
    public class Core2DShadowProperty :
        CoreDependencyObject,
        ICoreWorkingConfigurableObject,
        ICoreWorkingDefinitionObject,
        ICore2DDrawingClippedObject,
        ICoreBrushOwner 
    {

        private bool m_IsClipped;        
        private Vector2f m_Offset;
        private CoreBrush m_brush;
        private int m_BlurRadius;
        private bool m_Blur;
        private bool m_BlurEdge;

    //    public static readonly CoreDependencyProperty BlurProperty = CoreDependencyProperty.Register(
    //"Blur", typeof(int), typeof(Core2DShadowProperty), new CoreDependencyPropertyMetadata(0));

    //    public static readonly CoreDependencyProperty OpacityProperty = CoreDependencyProperty.Register(
    //        "Opacity", typeof(float), typeof(Core2DShadowProperty), new CoreDependencyPropertyMetadata(1.0f));

        /// <summary>
        /// get or set the blur edge
        /// </summary>
        public bool BlurEdge
        {
            get { return m_BlurEdge; }
            set
            {
                if (m_BlurEdge != value)
                {
                    m_BlurEdge = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        /// <summary>
        /// get or set if blur is active
        /// </summary>
        public bool Blur
        {
            get { return m_Blur; }
            set
            {
                if (m_Blur != value)
                {
                    m_Blur = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        /// <summary>
        /// get or set the radius
        /// </summary>
        public int BlurRadius
        {
            get { return m_BlurRadius; }
            set
            {
                if (m_BlurRadius != value)
                {
                    m_BlurRadius = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        public override void Dispose()
        {
            if (this.m_brush != null)
            {
                this.m_brush.Dispose();
            }
            base.Dispose();
        }
        /// <summary>
        ///  get the current brush
        /// </summary>
        public CoreBrush Brush
        {
            get
            {
                return this.m_brush;
            }
        }


        ///// <summary>
        ///// get or set blur factor
        ///// </summary>
        //public int Blur
        //{
        //    get
        //    {
        //        return (int)GetValue(BlurProperty);
        //    }
        //    set
        //    {
        //        this.SetValue(BlurProperty, value);
        //        OnPropertyChanged(Core2DDrawingChangement.Definition);
        //    }
        //}

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
                List<PropertyDescriptor> d = new List<PropertyDescriptor>();
                foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(value))
                {
                    if (item.IsBrowsable)
                        d.Add(item);

                }
                return new PropertyDescriptorCollection(d.ToArray());
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
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
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
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

       
        #region ICoreWorkingConfigurableObject Members

        public ICoreControl GetConfigControl()
        {
            return null;
        }

        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }

        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var group = parameters.AddGroup("ShadowProperty");

            group.AddActions(new CoreParameterActionBase("BrushColor", "lb.ShadowBrush.Caption", new CoreEditBrushAction(this.m_brush, 
                enuBrushSupport.Fill | enuBrushSupport.Solid | enuBrushSupport .PathGradient )));
            var t = this.GetType();
            group.AddItem(t.GetProperty("IsClipped"));
           // group.AddItem(this.GetType().GetProperty("BrushMode"));
            group.AddItem("Offset", "lb.Offset.caption", offsetChanged);
            //group.AddTrackbar("Opacity", "lb.Opacity", 0, 100, Convert.ToInt32(this.Opacity * 100.0f), opacityChanged);
            group.AddItem(t.GetProperty("Blur")); 
            group.AddItem(t.GetProperty("BlurEdge"));
            group.AddTrackbar("BlurRadius", "lb.BlurRadius", 0, 255,
                this.BlurRadius, opacityChanged);


            return parameters;
        }
        void opacityChanged(object sender, IGK.ICore.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            this.BlurRadius = Convert.ToInt32(e.Value);
        }
        void offsetChanged(object sender, IGK.ICore.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            string s = e.Value as string;

            if (string.IsNullOrEmpty(s))
                return;

            string[] tab = s.Split(';');
            switch (tab.Length)
            {
                case 1:
                    CoreUnit d = tab[0];
                    float c = (d as ICoreUnitPixel).Value;
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



        [Browsable(false)]
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
                //copy brush definition
                this.m_brush.CopyDefinition(m.Groups["value"].Value);
                v_out = v_out.Remove(m.Index, m.Length);
            }
            str = v_out;
            string[] tab = str.Split(new char[] { ';', ':' });
            this.IsLoading = true;
            for (int i = 0; i < tab.Length; i += 2)
            {
                if ((i + 1) >= tab.Length) break;

                switch (tab[i].Trim().ToLower())
                {
                    case "offset": this.m_Offset = Vector2f.ConvertFromString(tab[i + 1]); break;                    
                    case "isclipped": this.m_IsClipped = Convert.ToBoolean(tab[i + 1]); break;
                    case "isblur": this.m_Blur = Convert.ToBoolean(tab[i + 1]); break;
                    case "blurradius": this.m_BlurRadius= Convert.ToInt32 (tab[i + 1]); break;
                    case "bluredge": this.m_BlurEdge = Convert.ToBoolean(tab[i + 1]); break;
                    case "shadowbrush":
                        //ignore cause found in regex
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
                this.Offset.X + " " + this.Offset.Y));
            sb.Append(string.Format("IsClipped:{0};", this.IsClipped));
            if (this.Blur)
            {
                sb.Append(string.Format("IsBlur:true; BlurRadius:{0}; BlurEdge:{1};", 
                    this.BlurRadius,  this.BlurEdge));
            }
            sb.Append(string.Format("ShadowBrush:[{0}]", this.m_brush.GetDefinition()));
            return sb.ToString();
        }

        public string GetDefinition(IGK.ICore.Codec.IXMLSerializer seri)
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
            this.m_IsClipped = false;
            this.m_Offset = new Vector2f(2, 2);
            this.m_brush = new CoreBrush(this);
            this.m_brush.SetSolidColor(Colorf.Black);
            this.m_brush.BrushDefinitionChanged += m_brush_BrushDefinitionChanged;
        }

        void m_brush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            this.OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BrushChanged));
        }

        public ICore2DDrawingObject Parent
        {
            get
            {
                return this.m_Element as ICore2DDrawingObject;
            }
            set
            {
                //not implement
            }
        }

        public enuBrushSupport BrushSupport
        {
            get {
                return enuBrushSupport.Fill | enuBrushSupport.Solid | enuBrushSupport.PathGradient;
            }
        }

        public CoreGraphicsPath GetPath()
        {
            return this.m_Element.GetPath();
        }

        public Matrix GetMatrix()
        {
            return Matrix.Identity;
        }

        public ICoreBrush GetBrush(enuBrushMode enuBrushMode)
        {
            return this.m_brush;
        }
    }
}
