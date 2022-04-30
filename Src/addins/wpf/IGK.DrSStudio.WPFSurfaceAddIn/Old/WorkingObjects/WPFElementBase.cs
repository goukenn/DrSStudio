

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFElementBase.cs
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
file:WPFElementBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent a base element for wpf
    /// </summary>
    public class WPFElementBase :
        MarshalByRefObject, 
        ICoreWorkingObject ,
        ICoreWorkingConfigurableObject ,
        ICoreSerializerService,
        ICoreSerializerLoadingService,
        ICoreWorkingObjectPropertyEvent,
        IDisposable 
    {
        private string m_id;
        private bool m_isLoading;
        WPFElementBase m_Parent;
        /// <summary>
        /// get or set the element parent
        /// </summary>
        public WPFElementBase Parent {
            get {
                return m_Parent;
            }
            set {
                if (this.m_Parent != value)
                {
                    m_Parent = value;
                    OnParentChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ParentChanged;
        protected virtual  void OnParentChanged(EventArgs eventArgs)
        {
            if (this.ParentChanged != null)
                this.ParentChanged(this, eventArgs);
        }
        public virtual void Dispose()
        {}
        public WPFElementBase()
        {
        }
        #region ICoreIdentifier Members
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public string Id
        {
            get {
                if (string.IsNullOrEmpty(m_id))
                {
                    string tname = this.GetObjectName ();
                    this.m_id = string.Format ("{0}_{1}", tname, this.GetHashCode ());
                }
                return m_id;
            }
        }
        public string GetObjectName()
        {
            CoreWorkingObjectAttribute obj =
                Attribute.GetCustomAttribute(this.GetType(),
                typeof(CoreWorkingObjectAttribute), false) as CoreWorkingObjectAttribute;
            if (obj != null)
                return obj.Name;
            return null;
        }
        #endregion
        #region ICoreWorkingConfigurableObject Members
        public virtual enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public virtual IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            IGK.DrSStudio.WinUI.ICoreParameterGroup group = parameters.AddGroup("DEFAULT");
            group.AddItem(GetType().GetProperty("Id"));
            return parameters;
        }
        public virtual  ICoreControl GetConfigControl()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region ICoreSerializerLoadingService Members
        public bool IsLoading
        {
            get { return this.m_isLoading; }
        }
        public event EventHandler LoadingComplete;
        #endregion
        #region ICoreSerializerService Members
        public void Serialize(IXMLSerializer xwriter)
        {
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.WriteStartElement(this, xwriter);        
            WriteAttributes(xwriter);
            WriteElements(xwriter);
            xwriter.WriteEndElement();
        }
        public void Deserialize(IXMLDeserializer xreader)
        {
            this.m_isLoading = true;
            if (xreader.NodeType == System.Xml.XmlNodeType.None)
                xreader.MoveToContent();
            this.ReadAttributes(xreader);
            this.ReadElements(xreader);
            this.m_isLoading = false;
            OnLoadingComplete(EventArgs.Empty);
        }
        protected virtual  void ReadElements(IXMLDeserializer xreader)
        {
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.ReadElements(this, xreader);
        }
        protected virtual void ReadAttributes(IXMLDeserializer xreader)
        {
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.ReadAttributes(this, xreader);
        }
        protected virtual void WriteAttributes(IXMLSerializer xwriter)
        {
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.WriteAttributes(this, xwriter);
        }
        protected virtual void WriteElements(IXMLSerializer xwriter)
        {
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.WriteElements(this, xwriter);
        }
        protected  virtual void OnLoadingComplete(EventArgs eventArgs)
        {
            if (this.LoadingComplete != null)
                this.LoadingComplete(this, eventArgs);
        }
        #endregion
        #region ICoreWorkingObjectPropertyEvent Members
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, e);
        }
        #endregion
    }
}

