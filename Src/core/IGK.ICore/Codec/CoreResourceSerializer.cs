

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreResourceSerializer.cs
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
file:CoreResourceSerializer.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace IGK.ICore.Codec
{
    using IGK.ICore;using IGK.ICore.Drawing2D;
    using IGK.ICore.Resources;
    /// <summary>
    /// represent the resources serializer info
    /// </summary>
    sealed class CoreResourceSerializer : ICoreResourceSerializer
    {
        private IXMLSerializer m_serializer;
        private Dictionary<string, object> m_res;
        private List<ICoreResourceItem> m_list;


        /// <summary>
        /// get the serialiser
        /// </summary>
        public IXMLSerializer Serializer { get { return this.m_serializer; } }
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="serializer"></param>
        public CoreResourceSerializer(IXMLSerializer serializer)
        {
            this.m_list = new List<ICoreResourceItem>();
            this.m_serializer = serializer;
            this.m_res = new Dictionary<string, object>();
        }
        public string Add(ICoreWorkingObject obj, object resources)
        {
            string s = GenerateId();
            if (!this.m_res.ContainsKey(s))
            {
                this.m_res.Add(s, resources);
                return s;
            }
            return null;
        }
        public string getId(object res)
        {
            foreach (KeyValuePair<string, object > item in this.m_res )
            {
                if (item.Value == res)
                    return item.Key;
            }
            return null;
        }
        private string GenerateId()
        { 
            return "res_";
        }

        /// <summary>
        /// register a resource items
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string Register(ICoreResourceItem item)
        {
            if (item == null)
                return null;
            if (this.Contains(item))
            {
                return this.getId(item);
            }
            string v_newId = item.ResourceType + "_" + item.GetHashCode();
            this.m_res.Add(v_newId, item);
            this.m_list.Add(item);
            return v_newId;
        }

        /// <summary>
        /// get if contains the data
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool Contains(ICoreResourceItem item)
        {
            return this.m_list.Contains(item);
        }
    }
}

