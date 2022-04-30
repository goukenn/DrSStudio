

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreDeserializerResources.cs
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
file:CoreDeserializerResources.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Resources
{
    using IGK.ICore;using IGK.ICore.Codec;
    /// <summary>
    /// represent a deserializer resources
    /// </summary>
    public class CoreDeserializerResources : ICoreDeserializerResources 
    {
        CoreXMLDeserializer v_deserie;
        Dictionary<string, ICoreResourceItem > m_resources;
        public IXMLDeserializer Deserializer { get { return v_deserie; } }
        public override string ToString()
        {
            return string.Format("{0} [Count:{1}]", this.GetType().Name, m_resources.Count);
        }
        public CoreDeserializerResources(CoreXMLDeserializer deseri)
        {
            this.v_deserie = deseri;
            m_resources = new Dictionary<string, ICoreResourceItem>();
        }
        public void Add(string key, ICoreResourceItem value)
        {
            if (string.IsNullOrEmpty(key) || (value == null))
                return;
            if (!this.m_resources .ContainsKey (key))
                this.m_resources.Add(key, value);
            else
                this.m_resources[key ]=value ;
        }
        #region ICoreDeserializerResources Members
        public ICoreResourceItem this[string id]
        {
            get { 
                if (this.m_resources .ContainsKey (id))                    
                    return this.m_resources[id];
                return null;
            }
        }
        public bool Contains(string id)
        {
            if (string.IsNullOrEmpty (id))
                return false ;
            return this.m_resources.ContainsKey(id);
        }
        public event EventHandler LoadingComplete;
        public void RaiseLoadingComplete()
        {
            if (this.LoadingComplete != null)
                LoadingComplete(this, EventArgs.Empty);
        }
        #endregion
    }
}

