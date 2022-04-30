

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCodecCollections.cs
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
file:CoreCodecCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
namespace IGK.ICore.Codec
{
    using IGK.ICore.Codec;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Resources;
    [Serializable()]
    /// <summary>
    /// codec collections and utility 
    /// </summary>
    public sealed class CoreCodecCollections : MarshalByRefObject, ICoreCodecCollections
    {
        Dictionary<string, List<ICoreEncoder>> m_catEncoders;
        Dictionary<string, List<ICoreDecoder>> m_catDecoders;
        List<ICoreEncoder> m_encoders;
        List<ICoreDecoder> m_decoders;
        ICoreSystem m_core;
        Dictionary<string, List<Type>> m_encoderT;
        Dictionary<string, List<Type>> m_decoderT;
        /// <summary>
        /// .ctr 
        /// </summary>
        /// <param name="core"></param>
        public CoreCodecCollections(ICoreSystem core)
        {
            this.m_catDecoders = new Dictionary<string, List<ICoreDecoder>>();
            this.m_catEncoders = new Dictionary<string, List<ICoreEncoder>>();
            this.m_decoders = new List<ICoreDecoder>();
            this.m_encoders = new List<ICoreEncoder>();
            this.m_decoderT = new Dictionary<string, List<Type>>();
            this.m_encoderT = new Dictionary<string, List<Type>>();
            this.m_core = core;
            this.m_core.RegisterTypeLoader(TypeLoader);
        }
        void TypeLoader(Type t)
        {
            CoreCodecAttribute attr = (CoreCodecAttribute)
                Attribute.GetCustomAttribute(t, typeof(CoreCodecAttribute));
            if (attr == null) return;
            PropertyInfo pr = t.GetProperty("Instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            ICoreCodec codec = null;
            if ((pr != null) && (pr.PropertyType == t))
            {
                codec = pr.GetValue(null, null) as ICoreCodec;
            }
            else
                codec = t.Assembly.CreateInstance(t.FullName) as ICoreCodec;
            if (codec is ICoreEncoder)
            {
                RegisterEncoder(codec as ICoreEncoder);
            }
            if (codec is ICoreDecoder)
                RegisterDecoder(codec as ICoreDecoder);
        }
        private void RegisterDecoder(ICoreDecoder decoder)
        {
            if (decoder == null)
                return;
            this.RegDecoder(decoder);
            if (!string.IsNullOrEmpty(decoder.Category))
            {
                if (this.m_catDecoders.ContainsKey(decoder.Category))
                {
                    this.m_catDecoders[decoder.Category].Add(decoder);
                }
                else
                {
                    this.m_catDecoders.Add(decoder.Category, new List<ICoreDecoder>());
                    this.m_catDecoders[decoder.Category].Add(decoder);
                }
            }
        }
        private void RegDecoder(ICoreDecoder decoder)
        {
            if (decoder == null) return;
            this.m_decoders.Add(decoder);
            string v_n = string.Empty;
            foreach (string item in decoder.Extensions)
            {
                v_n = item + "=" + decoder.GetType().FullName;
                CoreLog.WriteDebug("RegisterCodec:" + v_n);
                if (!this.m_decoderT.ContainsKey(item.ToLower()))
                {
                    List<Type> vt = new List<Type>();
                    vt.Add(decoder.GetType());
                    m_decoderT.Add(item.ToLower(), vt);
                }
                else
                    m_decoderT[item.ToLower()].Add(decoder.GetType());
            }
        }
        private void RegisterEncoder(ICoreEncoder encoder)
        {
            this.m_encoders.Add(encoder);
            RegisterEncoderType(encoder);
            if (this.m_catEncoders.ContainsKey(encoder.Category))
            {
                this.m_catEncoders[encoder.Category].Add(encoder);
            }
            else
            {
                string[] p = encoder.Category.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string n = string.Empty;
                for (int i = 0; i < p.Length; i++)
                {
                    n = p[i].Trim();
                    if (!this.m_catEncoders.ContainsKey(n))
                    {
                        this.m_catEncoders.Add(n, new List<ICoreEncoder>());
                    }
                    this.m_catEncoders[n].Add(encoder);
                }
            }
        }
        private void RegisterEncoderType(ICoreEncoder encoder)
        {
        }
        public bool RegisterDecoder(string extension, string path)
        {
            CorePathDecoder pdeco = new CorePathDecoder(extension, path);
            this.RegisterDecoder(pdeco);
            return true;
        }
        public ICoreCodec[] GetDecoders(string category)
        {
            if (m_catDecoders .ContainsKey (category ))
                return m_catDecoders[category].ToArray();
            return null;
        }
        public ICoreCodec[] GetDecoders()
        {
            return this.m_decoders.ToArray();
        }
        public ICoreCodec[] GetEncoders(string category)
        {
            return this.m_catEncoders[category].ToArray();
        }
        #region ICoreCodecCollections Members
        public ICoreCodec[] GetEncoders()
        {
            return this.m_encoders.ToArray();
        }
        #endregion
        #region ICoreCodecCollections Members
        public ICoreCodec[] GetEncodersByExtension(string ext)
        {
            List<ICoreCodec> v_codec = new List<ICoreCodec>();
            foreach (ICoreEncoder codec in this.m_encoders)
            {
                if (codec.Extensions.Contains(ext))
                {
                    v_codec.Add(codec);
                }
            }
            return v_codec.ToArray();
        }
        public ICoreCodec[] GetDecodersByExtension(string ext)
        {
            List<ICoreCodec> v_decoder = new List<ICoreCodec>();
            foreach (ICoreDecoder codec in this.m_decoders)
            {
                if ((codec.Extensions != null) && codec.Extensions.Contains(ext))
                {
                    v_decoder.Add(codec);
                }
            }
            return v_decoder.ToArray();
        }
        #endregion
        #region ICoreCodecCollections Members
        public void ClearExtraDecoder()
        {
            ICoreDecoder[] tab = this.m_decoders.ToArray();
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] is CorePathDecoder)
                {
                    CorePathDecoder deco = tab[i] as CorePathDecoder;
                    this.m_decoders.Remove(deco);
                    this.m_catDecoders[deco.Category].Remove(tab[i]);
                    if (this.m_catDecoders[deco.Category].Count == 0)
                    {
                        this.m_catDecoders.Remove(deco.Category);
                    }
                }
            }
        }
        #endregion
        public string GetDecoderTypeByExtension(string ext)
        {
            foreach (ICoreDecoder codec in this.m_decoders)
            {
                if ((codec.Extensions != null) && codec.Extensions.Contains(ext))
                {
                    return codec.GetType().AssemblyQualifiedName;
                }
            }
            return null;
        }
    }
}

