

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingObjectSerializable.cs
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
file:CoreWorkingObjectSerializable.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.Codec;
    /// <summary>
    /// reprent a base seriable object
    /// </summary>
    public abstract class CoreWorkingObjectSerializable : 
        ICoreSerializerService ,
        ICoreSerializerLoadingService
    {
        private bool m_IsLoading;
        public bool IsLoading
        {
            get { return m_IsLoading; }
        }
        public event EventHandler LoadingComplete;
        private bool m_isValid;
        ///<summary>
        ///raise the LoadingComplete 
        ///</summary>
        protected virtual void OnLoadingComplete(EventArgs e)
        {
            if (LoadingComplete != null)
                LoadingComplete(this, e);
        }
        #region ICoreSerializerService Members
        public void Serialize(IGK.ICore.Codec.IXMLSerializer xwriter)
        {
            WriteAttributes(xwriter);
            WriteElements(xwriter);
        }
        protected virtual  void WriteAttributes(IGK.ICore.Codec.IXMLSerializer xwriter)
        {
        }
        protected virtual void WriteElements(IGK.ICore.Codec.IXMLSerializer xwriter)
        {
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
        }
        protected virtual void ReadAttributes(IGK.ICore.Codec.IXMLDeserializer xreader)
        {
        }
        #endregion
        #region ICoreIdentifier Members
        public abstract string Id
        {
            get;
        }
        #endregion

        public virtual bool IsValid
        {
            get { return this.m_isValid; }
            protected set {
                this.m_isValid = value;
            }
        }
    }
}

