

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreExtensionContextBase.cs
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
file:CoreExtensionContextBase.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// repres
    /// </summary>
    public class CoreExtensionContextBase :        
        ICoreExtensionContext 
    {
        /// <summary>
        /// id namd
        /// </summary>
        string m_id;
        /// <summary>
        /// targate
        /// </summary>
        ICoreWorkingObjectExtensionContainer m_target;
        /// <summary>
        /// get the target
        /// </summary>
        public ICoreWorkingObjectExtensionContainer  Target { get {
            return m_target;
            }
            set {
                if (this.m_target != value)
                {
                    if (this.m_target !=null)
                    UnregisterTargetEvent();
                    this.m_target = value;
                    if (this.m_target !=null)
                    RegisterTargetEvent();
                }
            }
        }
        protected virtual void RegisterTargetEvent()
        {            
        }
        protected virtual void UnregisterTargetEvent()
        {
        }
        #region ICoreIdentifier Members
        public string Id
        {
            get { return m_id; }
        }
        #endregion
        /// <summary>
        /// get if the current extension is currently active
        /// </summary>
        public virtual bool IsActive { get { return false; } }
        public CoreExtensionContextBase()
        {
            m_id = CoreExtensionAttribute.GetObjectName(GetType());
        }
        #region ICoreSerializerService Members
        public void Serialize(IGK.ICore.Codec.IXMLSerializer xwriter)
        {
            this.WriteAttributes(xwriter);
            this.WriteElements(xwriter);
        }
        protected virtual void WriteAttributes(IGK.ICore.Codec.IXMLSerializer xwriter)
        {
            IGK.ICore.Codec.CoreCodecUtils.WriteAttributes(this, xwriter);
        }
        protected virtual void WriteElements(IGK.ICore.Codec.IXMLSerializer xwriter)        
        {
            IGK.ICore.Codec.CoreCodecUtils.WriteElements(this, xwriter);
        }
        public void Deserialize(IGK.ICore.Codec.IXMLDeserializer xreader)
        {
            this.m_IsLoading = true;
            this.ReadAttributes(xreader);
            this.ReadElements(xreader);
            this.m_IsLoading = false;
            OnLoadingComplete(EventArgs.Empty);
        }
        protected virtual void ReadElements(IGK.ICore.Codec.IXMLDeserializer xreader)
        {
            IGK.ICore.Codec.CoreCodecUtils.ReadElements(this, xreader);
        }
        protected virtual void ReadAttributes(IGK.ICore.Codec.IXMLDeserializer xreader)
        {
            IGK.ICore.Codec.CoreCodecUtils.ReadAttributes(this, xreader);
        }
        #endregion
        private bool m_IsLoading;
        public bool IsLoading
        {
            get { return m_IsLoading; }
        }
        public event EventHandler LoadingComplete;
        ///<summary>
        ///raise the LoadingComplete 
        ///</summary>
        protected virtual void OnLoadingComplete(EventArgs e)
        {
            if (LoadingComplete != null)
                LoadingComplete(this, e);
        }

        public bool IsValid
        {
            get { return true; }
        }
    }
}

