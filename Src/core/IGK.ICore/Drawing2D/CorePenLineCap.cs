

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePenLineCap.cs
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
file:CorePenLineCap.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.Settings;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Codec;
    public delegate void CorePenLineCapEventHandler(object sender, CorePenLineCapEventArgs e);
    public class CorePenLineCapEventArgs : EventArgs
    {
        private CorePenLineCap m_PenLineCap;
        public CorePenLineCap PenLineCap
        {
            get { return m_PenLineCap; }
        }
        public CorePenLineCapEventArgs(CorePenLineCap cap)
        {
            this.m_PenLineCap = cap;
        }
    }
    /// <summary>
    /// Pen Anchor element
    /// </summary>
    public struct CorePenLineCap : 
        ICoreLineCap,
        ICoreSerializerService,
        ICoreWorkingDefinitionObject,
        ICoreWorkingConfigurableObject
    {
        public string GetDefinition(IGK.ICore.Codec.IXMLSerializer seri)
        {
            return this.GetDefinition();
        }
        private string m_displayName;
        private enuLineCap m_lineCap;
        private CoreGraphicsPath  m_pathElement;
        private float m_BaseInset;
        private enuLineCap m_CustomCap;
        private float m_WidthScale;
        public override bool Equals(object obj)
        {
            //CorePenLineCap d = (CorePenLineCap)obj;
            return base.Equals(obj);// (d.m_displayName == this.m_displayName);            
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(CorePenLineCap cap, CorePenLineCap en)
        {
            return (cap.m_displayName == en.m_displayName);
        }
        public static bool operator !=(CorePenLineCap cap, CorePenLineCap en)
        {
            return (cap.m_displayName != en.m_displayName);
        }
        /// <summary>
        /// get or set the width scale
        /// </summary>
        public float WidthScale
        {
            get { return m_WidthScale; }
            set
            {
                if (m_WidthScale != value)
                {
                    m_WidthScale = value;
                }
            }
        }
        private static Dictionary<string, CorePenLineCap> sm_PensLineCap;
        public static event CorePenLineCapEventHandler CorePenLineCapAdded;
        public static string CapFolder
        {
            get
            {
                if (CoreApplicationSetting.Instance.Contains("CapFolder") == false)
                {
                    CoreApplicationSetting.Instance["CapFolder"].Value = "%startup%/PenCap";
                }
                return (string)CoreApplicationSetting.Instance["CapFolder"].Value;
            }
            set
            {
                CoreApplicationSetting.Instance["CapFolder"].Value = value;
            }
        }
        public enuLineCap CustomCap
        {
            get { return m_CustomCap; }
            set
            {
                if (m_CustomCap != value)
                {
                    m_CustomCap = value;
                }
            }
        }
        public float BaseInset
        {
            get { return m_BaseInset; }
            set
            {
                if (m_BaseInset != value)
                {
                    m_BaseInset = value;
                }
            }
        }
        static CorePenLineCap()
        {
            sm_PensLineCap = new Dictionary<string, CorePenLineCap>();
            Empty = new CorePenLineCap();
        }
        public static readonly CorePenLineCap Empty;
        public static CorePenLineCap GetLineCap(enuLineCap cap)
        {
            if (cap != enuLineCap.Custom)
                return new CorePenLineCap(cap);
            return CorePenLineCap.Empty;
        }
        private CorePenLineCap(enuLineCap lineCap)
        {
            if (lineCap == enuLineCap.Custom)
                throw new NotImplementedException();
            this.m_lineCap = lineCap;
            this.m_displayName = lineCap.ToString();
            this.m_pathElement = null;
            this.m_CustomCap = enuLineCap.Flat;
            this.m_BaseInset = 0.0f;
            this.m_WidthScale = 1.0f;
        }
        private CorePenLineCap(string DisplayName, CoreGraphicsPath pathElement)
        {
            if (pathElement == null)
                throw new ArgumentNullException("pathElement");
            this.m_lineCap = enuLineCap.Custom;
            this.m_pathElement = pathElement;
            this.m_displayName = DisplayName;
            this.m_CustomCap = enuLineCap.Flat;
            this.m_BaseInset = 0.0f;
            this.m_WidthScale = 1.0f;
        }
        public static CorePenLineCap[] LoadAnchor()
        {
            if (sm_PensLineCap.Count > 0)
            {
                return sm_PensLineCap.Values.ToArray<CorePenLineCap>();
            }
            List<CorePenLineCap> m_anchors = new List<CorePenLineCap>();
            foreach (enuLineCap item in Enum.GetValues(typeof(enuLineCap)))
            {
                if (item != enuLineCap.Custom)
                {
                    m_anchors.Add(new CorePenLineCap(item));
                }
            }
            string dir = IGK.ICore.IO.PathUtils.GetStartupFullPath("PenCap");
            IGK.ICore.Codec.IXMLDeserializer deseri = null;
            if (System.IO.Directory.Exists(dir))
            {
                foreach (string str in System.IO.Directory.GetFiles(dir, "*.gklc"))
                {
                    using (System.IO.Stream stream = System.IO.File.Open(str, System.IO.FileMode.Open))
                    {
                        deseri = IGK.ICore.Codec.CoreXMLDeserializer.Create(stream);
                        if (deseri.ReadToDescendant("Path"))
                        {
                            PathElement obj = CoreSystem.CreateWorkingObject(deseri.Name) as PathElement;
                            if (obj != null)
                            {
                                if (obj is ICoreSerializerService)
                                        (obj as ICoreSerializerService).Deserialize(deseri);
                                m_anchors.Add(new CorePenLineCap(
                                    System.IO.Path.GetFileNameWithoutExtension(str),
                                    obj.GetPath()));
                            }
                        }
                    }
                }
            }
            sm_PensLineCap.Clear();
            foreach (CorePenLineCap item in m_anchors)
            {
                sm_PensLineCap.Add(item.m_lineCap.ToString(), item);
            }
            return m_anchors.ToArray();
        }
        public override string ToString()
        {
            return this.DisplayName;
        }
        #region IPenAnchor Members
        public string DisplayName
        {
            get { return CoreSystem.GetString("linecap." + this.m_displayName); }
        }
        public CoreGraphicsPath PathElement
        {
            get { return this.m_pathElement; }
        }
        public enuLineCap LineCap
        {
            get { return this.m_lineCap; }
        }
        #endregion
        #region ICoreLineCap Members
        //public CustomLineCap GetCustomCap()
        //{
        //    if (this.m_pathElement == null)
        //        return null;
        //    CoreGraphicsPath v_path = this.m_pathElement.GetPath().Clone() as CoreGraphicsPath;
        //    Rectanglef rc = v_path.GetBounds();
        //    Vector2f  d = CoreMathOperation.GetCenter(rc);
        //    Matrix m = new Matrix();
        //    m.Translate(-d.X, -d.Y, enuMatrixOrder.Append);
        //    v_path.Transform(m);
        //    m.Dispose();
        //    System.Drawing.Drawing2D.CustomLineCap v_lcap = null;
        //    v_lcap = new CustomLineCap(null, v_path.Clone() as CoreGraphicsPath, this.CustomCap, this.BaseInset);
        //    v_lcap.WidthScale = this.WidthScale;
        //    return v_lcap;
        //}
        #endregion
        public static ICoreLineCap GetLineCap(string p)
        {
            if (sm_PensLineCap.ContainsKey(p))
            {
                return sm_PensLineCap[p];
            }
            else
            {
                CorePenLineCap v_dp = new CorePenLineCap();
                try
                {
                    v_dp.CopyDefinition(p);
                    return v_dp;
                }
                catch
                {
                }
            }
            return null;
        }
        public static bool Save(CoreGraphicsPath path, string p, enuLineCap lineCap, float baseInset)
        {
            if ((path == null) || string.IsNullOrEmpty(p))
                return false;
            IGK.ICore.Codec.IXMLSerializer deseri = null;
            string dir = IO.PathUtils.GetPath(CapFolder);
            if (!IO.PathUtils.CreateDir(dir))
            {
                CoreLog.WriteDebug(string.Format("Can't create folder {0}", dir));
                return false;
            }
            string file = IO.PathUtils.GetPath(string.Format(dir + "/{0}.gklc", p));
            deseri = CoreXMLSerializer.Create(file);
            deseri.WriteStartElement("Path");
            byte[] v_b = null;
            Vector2f[] v_p = null;
            path.GetAllDefinition (out v_p,out v_b);
            deseri.WriteElementString("Points", v_p.CoreConvertTo<string>());
            deseri.WriteElementString("Types", v_b.CoreConvertTo<string>());
            deseri.WriteEndElement();
            deseri.Close();
            CorePenLineCap cap = new CorePenLineCap(p, path.Clone() as CoreGraphicsPath);
            sm_PensLineCap.Add(p, cap);
            RaisePenLineCap(new CorePenLineCapEventArgs(cap));
            return true;
        }
        private static void RaisePenLineCap(CorePenLineCapEventArgs e)
        {
            if (CorePenLineCapAdded != null)
                CorePenLineCapAdded(Empty, e);
        }
        #region ICoreSerializerService Members
        public void Serialize(IGK.ICore.Codec.IXMLSerializer xwriter)
        {
            throw new NotImplementedException();
        }
        public void Deserialize(IGK.ICore.Codec.IXMLDeserializer xreader)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return this.DisplayName; }
        }
        #endregion
        #region ICoreWorkingDefinitionObject Members
        public string GetDefinition()
        {
            if (this.LineCap == enuLineCap.Custom)
            {
                return string.Format(this.DisplayName + " {0} {1} {2}",
                    this.CustomCap,
                    this.BaseInset,
                    this.WidthScale);
            }
            return this.LineCap.ToString();
        }
        public void CopyDefinition(string str)
        {
            if (string.IsNullOrEmpty(str))
                return;
            string[] v_tstr = str.Trim().Split(' ');
            switch (v_tstr.Length)
            {
                case 1:
                    if (Enum.IsDefined(typeof(enuLineCap), v_tstr[0]))
                    {
                        ICoreLineCap l = GetLineCap(v_tstr[0]);
                        if (l.LineCap != enuLineCap.Custom)
                        {
                            this.m_lineCap = l.LineCap;
                            this.m_pathElement = null;
                            this.m_BaseInset = 0.0f;
                        }
                    }
                    break;
                case 4:
                    this.m_displayName = v_tstr[0];
                    this.m_CustomCap = (enuLineCap)Enum.Parse(typeof(enuLineCap), v_tstr[1]);
                    this.m_BaseInset = float.Parse(v_tstr[2]);
                    this.m_WidthScale = float.Parse(v_tstr[3]);
                    if (sm_PensLineCap.ContainsKey(this.m_displayName))
                    {
                        if (sm_PensLineCap[this.m_displayName].m_lineCap == enuLineCap.Custom)
                        {
                            this.m_pathElement = sm_PensLineCap[this.m_displayName].PathElement;
                            this.m_lineCap = enuLineCap.Custom;
                        }
                    }
                    break;
            }
        }
        #endregion
        #region ICoreWorkingConfigurableObject Members
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections param = parameters;
            var group = param.AddGroup("Properties");
            group.AddItem("WidthScale", "lb.WidthScale.caption", enuParameterType.SingleNumber, ValueChanged);
            //param.AddEnumItem("CustomCap", "lb.CustomCap.caption", ValueChanged);
            group.AddItem("BaseInset", "lb.Offset.caption", ValueChanged);
            return param;
        }
        void ValueChanged(Object sender, CoreParameterChangedEventArgs e)
        {
            switch (e.Item.Name.ToLower())
            {
                case "widthscale":
                    if (e.Value != null)
                        this.m_WidthScale = Convert.ToSingle(e.Value);
                    break;
                case "customcap":
                    if (Enum.IsDefined(typeof(enuLineCap), e.Value.ToString()))
                    {
                        this.m_CustomCap = (enuLineCap)Enum.Parse(typeof(enuLineCap), e.Value.ToString());
                    }
                    break;
                case "baseinset":
                    this.m_BaseInset = Convert.ToSingle(e.Value);
                    break;
            }
        }
        public ICoreControl GetConfigControl()
        {
            return null;
        }
        #endregion

        public bool IsValid
        {
            get {return true ; }
        }
    }
}

