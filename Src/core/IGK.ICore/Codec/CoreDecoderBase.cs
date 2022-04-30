

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreDecoderBase.cs
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
file:CoreDecoderBase.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    using IGK.ICore;using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    using System.Text.RegularExpressions;
    /// <summary>
    /// represent the base abstract class for decoding file for DRSStudio
    /// </summary>
    public abstract class CoreDecoderBase : CoreCodecBase , ICoreDecoder        
    {        
        /// <summary>
        /// choose decoder from filname extension
        /// </summary>
        /// <param name="bench"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static ICoreCodec ChooseDecoderFromFileExtension(IGK.ICore.WinUI.ICoreWorkbench bench, string filename)
        {
            ICoreCodec[] d =
                        IGK.ICore.Codec.CoreDecoderBase.GetDecodersByExtension(System.IO.Path.GetExtension(filename));
            if ((d==null) || (d.Length == 0))
                return null;
            if (d.Length != 1)
            {
                CoreCodecSelector select = new CoreCodecSelector(d);
                if (bench.ConfigureWorkingObject(select, "title.selectcodec".R(), false, Size2i.Empty ) == enuDialogResult.OK)
                {
                    return select.SelectedCodec;
                }
            }
            else
                return d[0];
            return null;
        }
        #region ICoreDecoder Members
        public abstract bool Open(IGK.ICore.WinUI.ICoreWorkbench bench, string filename, bool selectCreatedSurface);
        #endregion
        public override string ToString()
        {
            return string.Format("{0} {1} {2}",
                this.Id,
                CoreSystem.GetString("lb.for.caption"),
                this.MimeType);
        }
        public static ICoreCodec[] GetDecoders() { return CoreSystem.GetDecoders(); }
        public static ICoreCodec[] GetDecoders(string category) { return CoreSystem.GetDecoders(category ); }
        public static ICoreCodec[] GetDecodersByExtension(string ext) { return CoreSystem.GetDecodersByExtension(ext); }
        #region ICoreIdentifier Members
        public string Id
        {
            get { return this.m_id; }
        }
        #endregion
        private string m_id;
        CodecExtensionCollections m_Extensions;
        #region ICoreDecoder Members
        public ICoreCodecExtensionCollections Extensions
        {
            get { return this.m_Extensions; }
        }
        private string m_Category;
        private string m_mimeType;
        public string MimeType
        {
            get { return m_mimeType; }
        }
        public string Category
        {
            get { return m_Category; }
        }
        #endregion
        protected CoreDecoderBase()
        {
            CoreCodecAttribute attr = (CoreCodecAttribute)
   Attribute.GetCustomAttribute(this.GetType(), typeof(CoreCodecAttribute));
            if (attr == null)
                throw new CoreException(enuExceptionType.OperationNotValid,
                    CoreConstant.ERR_DECODER_ATTR_MUST_BE_SET,
                    CoreConstant.ERROR_DECODER_ATTR_MUST_BE_SET);
            this.m_Extensions = new CodecExtensionCollections();
            this.m_Extensions.Add(attr.Extensions);
            this.m_Category = attr.Category;
            this.m_mimeType = attr.MimeType;
            this.m_id = attr.Name;
        }

        public override int CompareTo(object obj)
        {
            ICoreCodec c = obj as ICoreCodec;
            if (this.Category == c.Category)
            {
                return this.m_mimeType.CompareTo(c.MimeType);
            }
            return this.Category.CompareTo(c.Category);
        }

        /// <summary>
        /// get the filter string by category
        /// </summary>
        /// <param name="category">category name</param>
        /// <returns></returns>
        public static string GetFilter(string category)
        {
            StringBuilder sb = new StringBuilder();
            
            List<string> ext = new List<string>();
            
            
            foreach (ICoreDecoder deco in CoreSystem.GetDecoders())
            {
                if (Regex.IsMatch(deco.Category, category, RegexOptions.IgnoreCase ) == false)
                    continue;
                
                foreach (string item in deco.Extensions)
                {
                    if (string.IsNullOrEmpty(item)) continue;
                    if (ext.Contains(item.ToLower()) == false)
                        ext.Add(item);
                    
                           
                }                
            }
            sb.Append("*." + string.Join(";*.", ext.ToArray()));
            return sb.ToString();
        }
        public static string  GetAllFilters()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder v_all = new StringBuilder();
            v_all.Append(string.Format("{0}|", CoreSystem.GetString("ALLSupportedFileFilter")));
            bool t = false;
            bool h = false;
            bool v_a = false;
            foreach (ICoreDecoder deco in CoreSystem.GetDecoders())
            {
                if (t)
                    sb.Append("|");
                sb.Append(deco.MimeType);
                sb.Append("|");
                h = false;
                foreach (string item in deco.Extensions)
	            {
                    if (string.IsNullOrEmpty (item ))continue ;
                    if (h)
                    {
                        sb.Append(";");                        
                    }
                    if (v_a )
                        v_all.Append(";");
                    sb.Append("*.");
                    v_all.Append("*.");
                    sb.Append(item);
                    v_all.Append(item);
                    h = true;
                    if (!v_a)
                        v_a = true;
	            }
                t = true;
            }
            v_all.Append("|");
            v_all.Append(sb.ToString());
            return v_all.ToString();
        }
        /// <summary>
        /// get the filter attached to this encoder
        /// </summary>
        /// <returns></returns>
        public string GetFilter()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CoreSystem.GetString(this.MimeType));
            sb.Append("|");
            foreach (string ext in this.Extensions)
            {
                sb.Append("*.");
                sb.Append(ext);
                sb.Append(";");
            }
            return sb.ToString();
        }
    }
}

