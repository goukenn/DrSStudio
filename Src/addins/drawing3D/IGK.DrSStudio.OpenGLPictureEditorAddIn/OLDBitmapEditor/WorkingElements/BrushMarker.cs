

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BrushMarker.cs
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
file:BrushMarker.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing ;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio.OGLBitmapEditor.WorkingElements
{
    
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.Codec;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Codec;
    /// <summary>
    /// represent a pixel marquer
    /// </summary>
    [Core2DDrawingStandardItem("BrushMarquer", typeof(Mecanism))]
    class BrushMarquer : Core2DDrawingLayeredElement,
        ICoreSerializerAdditionalPropertyService,
        ICore2DSymbolElement
    {
        List<Vector2i> m_vectors;
        ICoreBrush m_FillBrush;
        private enuBrushMarquerStyle m_BrushStyle;
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue (enuBrushMarquerStyle .Rectangle)]
        public enuBrushMarquerStyle BrushStyle
        {
            get { return m_BrushStyle; }
            set
            {
                if (m_BrushStyle != value)
                {
                    m_BrushStyle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public Vector2i[] Marks
        {
            get
            {
                return m_vectors.ToArray();
            }
        }
        private int m_Size;
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue(enuBrushMarquerStyle.Rectangle)]
        /// <summary>
        /// get or set the size
        /// </summary>
        public int Size
        {
            get { return m_Size; }
            set
            {
                if (m_Size != value)
                {
                    m_Size = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public ICoreBrush FillBrush
        {
            get
            {
                return this.m_FillBrush;
            }
        }
        public override IGK.DrSStudio.Drawing2D.enuBrushSupport BrushSupport
        {
            get { return IGK.DrSStudio.Drawing2D.enuBrushSupport.FillOnly; }
        }
        protected override void WriteAttributes(IXMLSerializer xwriter)
        {
            //xwriter.Resources.Add(this.SymbolItem);
            base.WriteAttributes(xwriter);
        }
        protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            Vector2i[] tab = this.m_vectors.ToArray();
            Vector2i.Vector2iConverter conv = new Vector2i.Vector2iConverter();
            string v_g = conv.ConvertToString(tab);
            xwriter.WriteElementString("Marks", v_g);
            //write value
        }
        protected override void ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            base.ReadElements(xreader);
        }
        public override IGK.DrSStudio.Drawing2D.ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (mode == enuBrushMode.Fill)
            {
                return m_FillBrush;
            }
            return null;
        }
        protected override void GeneratePath()
        {
            this.SetPath(null);
        }
        public BrushMarquer()
        {
            this.m_vectors = new List<Vector2i>();
            this.m_Size = 1;
            this.m_BrushStyle = enuBrushMarquerStyle.Rectangle;
            this.m_FillBrush = new CoreBrush(this);
            this.m_FillBrush.SetSolidColor(Colorf.Black);
            this.m_FillBrush.BrushDefinitionChanged += new EventHandler(m_FillBrush_BrushDefinitionChanged);
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup("Properties");
            g.AddItem(GetType().GetProperty("BrushStyle"));
            g.AddItem(GetType().GetProperty("Size"));
            return parameters;
        }
        public override void Dispose()
        {
            if (this.m_symbol != null)
            {
                this.m_symbol.Dispose();
                this.m_symbol = null;
            }
            this.m_FillBrush.Dispose();
            base.Dispose();
        }
        void m_FillBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BrushChanged));
        }
        public override void Draw(Graphics g)
        {
            System.Drawing.Drawing2D.GraphicsState v_globalState
                = g.Save();
            this.SetGraphicsProperty(g);
            Rectanglei rc = Rectanglei.Empty;
            System.Drawing.Brush br = this.m_FillBrush.GetBrush();
            float v_scalex = 1.0f;
            float v_scaley = 1.0f;
            System.Drawing.Drawing2D.GraphicsState v_state = null;
            if (this.BrushStyle == enuBrushMarquerStyle.Custom ) 
            {
                if (this.SymbolItem != null)
                {
                    v_scalex = 1 / this.SymbolItem.GetBound().Width;
                    v_scaley = 1 / this.SymbolItem.GetBound().Height;
                }
                else {
                    //change the brush style
                    this.m_BrushStyle = enuBrushMarquerStyle.Ellipse;
                }
            }
            try
            {
                switch (this.BrushStyle)
                    {
                        case enuBrushMarquerStyle.Rectangle:
                            foreach (Vector2i item in this.m_vectors)
                            {
                                rc = new Rectanglei(item, Size2i.Empty );
                                rc.Inflate(this.Size, this.Size);                                
                                        g.FillRectangle(
                                   br,
                                   rc);
                            }
                            break;
                        case enuBrushMarquerStyle.Ellipse:
                        foreach (Vector2i item in this.m_vectors)
                            {
                                rc = new Rectanglei(item, Size2i.Empty );
                                rc.Inflate(this.Size, this.Size);
                            g.FillEllipse(
                       br,
                       rc);
                        }
                            break;
                        case enuBrushMarquerStyle.Custom :
                            GraphicsPath p = this.SymbolItem .GetPath();
                            if (p != null)
                            {
                                foreach (Vector2i item in this.m_vectors)
                                {
                                    rc = new Rectanglei(item, Size2i.Empty );
                                    rc.Inflate(this.Size, this.Size);  
                                    v_state = g.Save();
                                    g.TranslateTransform(rc.X, rc.Y, System.Drawing.Drawing2D.enuMatrixOrder.Prepend);
                                    g.ScaleTransform(rc.Width * v_scalex, rc.Height * v_scaley, System.Drawing.Drawing2D.enuMatrixOrder.Prepend);
                                    g.FillPath(br, p);
                                    g.Restore(v_state);
                                }
                            }
                            break;
                        default:
                            break;
                    }                
            }
            catch (Exception ex){
                CoreLog.WriteDebug(ex.Message);
            }
            g.Restore(v_globalState);
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
            class SettingManager
            {
                BrushMarquer Element;
                Mecanism m_mecanism;
                public SettingManager(Mecanism mecanism, BrushMarquer marker)
                {
                    this.Element = marker;
                    this.m_mecanism = mecanism;
                    marker.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(marker_PropertyChanged);
                }
                void marker_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
                {
                    IGK.DrSStudio.Settings.BrushMarkerSettings.Instance["Size"].Value  = Element.Size;
                    IGK.DrSStudio.Settings.BrushMarkerSettings.Instance["BrushStyle"].Value  = Element.BrushStyle;                    
                    this.m_mecanism.m_customObject = Element.SymbolItem;
                }
                internal void Unregister()
                {
                    this.Element.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(marker_PropertyChanged);
                }
            }
            SettingManager m_sManager;
            new BrushMarquer  Element
            {
                get
                {
                    return base.Element as BrushMarquer;
                }
            }
            ICore2DDrawingLayeredElement m_customObject;
            protected override void OnElementChanged(CoreElementChangedEventArgs <ICoreWorkingObject> eventArgs)
            {
                base.OnElementChanged(eventArgs);
                if (m_sManager != null)
                {
                    m_sManager.Unregister();
                    m_sManager = null;
                }
                if (this.Element != null)
                {
                    m_sManager = new SettingManager(this, this.Element);
                }
            }
            protected override void UpdateDrawing(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                base.UpdateDrawing(e);
                BrushMarquer marquer = this.Element;
                Vector2i v = Vector2i.Round(e.FactorPoint);
                Rectanglei rc = new Rectanglei(0, 0, this.CurrentSurface.CurrentDocument.Width - 1,
                    this.CurrentSurface.CurrentDocument.Height - 1);
                if (rc.Contains(v))
                {
                    marquer.m_vectors.Add(v);
                    this.CurrentSurface.Invalidate();
                }
            }
            protected override void InitNewCreateElement(ICore2DDrawingElement element)
            {
                base.InitNewCreateElement(element);
                BrushMarquer marquer = element as BrushMarquer;
                if (marquer == null) return;
                marquer.m_Size = (int)IGK.DrSStudio.Settings.BrushMarkerSettings.Instance["Size"].Value ;
                marquer.m_BrushStyle  = (enuBrushMarquerStyle)IGK.DrSStudio.Settings.BrushMarkerSettings.Instance["BrushStyle"].Value;
                if (marquer.m_BrushStyle == enuBrushMarquerStyle.Custom)
                {
                    marquer.m_symbol = this.m_customObject;
                }
                marquer.m_FillBrush.Copy (this.CurrentSurface.FillBrush);
            }
        }
        bool ReadAdditionalInfo(IXMLDeserializer deseri)
        {
            if (deseri.Name == "Marks")
            {
                Vector2f.Vector2fArrayTypeConverter conv = new Vector2f.Vector2fArrayTypeConverter();
                string st = deseri.ReadElementContentAsString();
                Vector2f[] c = (Vector2f[])conv.ConvertFromString(st);
                Vector2i[] cTab = new Vector2i[c.Length];
                for (int i = 0; i < cTab.Length; i++)
                {
                    cTab[i] = Vector2i.Round(c[i]);
                }
                this.m_vectors.Clear();
                this.m_vectors.AddRange(cTab);
                return true;
            }
            return false;
        }
        #region ICoreSerializerAdditionalPropertyService Members
        CoreReadAdditionalElementPROC ICoreSerializerAdditionalPropertyService.GetProc()
        {
            return new CoreReadAdditionalElementPROC(ReadAdditionalInfo);
        }
        #endregion
        #region ICore2DSymbolElement Members
        ICore2DDrawingLayeredElement m_symbol;
        [IGK.DrSStudio.Codec.CoreXMLAttribute (IsResource = true )]
        public ICore2DDrawingLayeredElement SymbolItem
        {
            get
            {
                return m_symbol;
            }
            set
            {
                if (this.m_symbol != value)
                {
                    this.m_symbol = value;
                    if (value != null)
                    {
                        this.m_BrushStyle = enuBrushMarquerStyle.Custom;
                    }
                    else {
                        this.m_BrushStyle = enuBrushMarquerStyle.Ellipse;
                    }
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        #endregion
    }
}
namespace IGK.DrSStudio.Settings
{
    using IGK.DrSStudio.OGLBitmapEditor.WorkingElements;
    //[CoreAppSetting(Name = "BrushMarkerSettings", Category="General")]
    class BrushMarkerSettings : CoreSettingBase 
    {
        private static BrushMarkerSettings sm_instance;
        private BrushMarkerSettings()
        {
        }
        public static BrushMarkerSettings Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static BrushMarkerSettings()
        {
            sm_instance = new BrushMarkerSettings();
        }
        [CoreSettingDefaultValue (1)]
        public int Size {
            get {
                return (int)this["Size"].Value;
            }
            set {
                this["Size"].Value = value;
            }
        }
        [CoreSettingDefaultValue(enuBrushMarquerStyle.Ellipse )]
        public enuBrushMarquerStyle BrushStyle
        {
            get
            {
                return (enuBrushMarquerStyle)this["BrushStyle"].Value;
            }
            set
            {
                this["BrushStyle"].Value = value;
            }
        }
        //[CoreSettingDefaultValue(null)]
        //public IGK.DrSStudio.Drawing2D.ICore2DDrawingLayeredElement  SymbolItem
        //{
        //    get {
        //        return this["SymbolItem"].Value as IGK.DrSStudio.Drawing2D.ICore2DDrawingLayeredElement;
        //    }
        //    set {
        //        this["SymbolItem"].Value = value;
        //    }
        //}
        public override void Load(ICoreSetting setting)
        {
            base.Load(setting);
        }
        protected override bool LoadDummyChildSetting(KeyValuePair<string, ICoreApplicationSetting> d)
        {
            return base.LoadDummyChildSetting(d);
        }
    }
}

