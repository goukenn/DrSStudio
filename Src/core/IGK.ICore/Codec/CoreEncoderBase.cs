

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreEncoderBase.cs
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
file:CoreEncoderBase.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    using IGK.ICore;using IGK.ICore.Codec ;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    /// <summary>
    /// represent the base encoder class
    /// </summary>
    public abstract class CoreEncoderBase :
        CoreCodecBase ,
        ICoreEncoder ,
        ICoreWorkingConfigurableObject,
        ICoreWorkingParameterConfigurableObject,
        IComparable
    {
        private string m_id;
        private string m_Category;
        private string m_mimeType;

        private bool m_AlwayShowConfiguration;

        public virtual void SetConfigParameter(object t){ 

        }
        protected virtual void initParameterSetting(ICoreParameterConfigCollections parameters)
        {
            var v_group = parameters.AddGroup("setting");
            v_group.AddItem(this.GetType().GetProperty("AlwayShowConfiguration"));
        }
        /// <summary>
        /// used to indicate the this encoder require to show is setting value
        /// </summary>
        public virtual bool AlwayShowConfiguration
        {
            get { return m_AlwayShowConfiguration; }
            set
            {
                if (m_AlwayShowConfiguration != value)
                {
                    m_AlwayShowConfiguration = value;
                }
            }
        }
   
        public static ICoreCodec[] GetEncoders() { return CoreSystem.GetEncoders(); }
        public static ICoreCodec[] GetEncoders(string category) { return CoreSystem.GetEncoders(category); }
        public static ICoreCodec[] GetEncodersByExtension(string ext) { return CoreSystem.GetEncodersByExtension(ext); }

        public static bool SaveToFile(ICoreWorkingSurface surface, ICoreEncoder encoder, string filename, ICoreWorkingDocument[] documents)
        {
            if ((encoder == null) || (documents == null) || string.IsNullOrEmpty (filename))
                return false;

            bool m_saved = false;
            ICoreWorkbench v_bench = CoreSystem.GetWorkbench();

            if (encoder.GetConfigType() == enuParamConfigType.NoConfig)
            {
                encoder.Save(surface, filename, documents);
                if (!(encoder is CoreEncoder) && IGK.ICore.Settings.CoreApplicationSetting.Instance.SaveBothGkdsFileAndOther)
                {
                    CoreEncoder.Instance.Save(surface, filename, documents);
                }
                m_saved = true;
            }
            else
            {
                CoreEncoderBase b = encoder as CoreEncoderBase;
                //if (b.AlwayShowConfiguration)
                //{
                    b.SetConfigParameter(documents);
                //}
                if ((v_bench != null) && 
                    v_bench.ConfigureWorkingObject(encoder,
                    "title.configurecodec".R(encoder.MimeType), false,
                    new Size2i(320,420) ) == enuDialogResult.OK)
                {
                    //demand for show config
                    encoder.Save(surface, filename, documents);
                    if (!(encoder is CoreEncoder) && IGK.ICore.Settings.CoreApplicationSetting.Instance.SaveBothGkdsFileAndOther)
                    {
                        CoreEncoder.Instance.Save(surface, filename, documents);
                    }
                    m_saved = true;
                }
                else
                {
                    CoreMessageBox.NotifyMessage(
                        CoreConstant.WARN_TITLE.R(),
                        CoreConstant.WARN_YOUCANCELSAVE.R()                        
                        );
                }
            }
            return m_saved;
        }
        
        public override string ToString()
        {
            return string.Format("{0}[{1}]",
                this.Id,
                this.MimeType);
        }
        public string MimeType
        {
            get { return m_mimeType; }
        }
        public string Category
        {
            get { return m_Category; }
        }
        public virtual bool CanConfigure{get{return false;}}
        #region ICoreEncoder Members
        public abstract bool Save(IGK.ICore.WinUI.ICoreWorkingSurface surface, string filename, params  ICoreWorkingDocument[] documents);
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get { return this.m_id; }
        }
        #endregion
        #region ICoreEncoder Members
        CodecExtensionCollections m_Extensions;
        public ICoreCodecExtensionCollections Extensions
        {
            get { return this.m_Extensions; }
        }
        #endregion
        protected CoreEncoderBase()
        {

            this.m_Extensions = new CodecExtensionCollections();
            CoreCodecAttribute attr = (CoreCodecAttribute)
             Attribute.GetCustomAttribute(this.GetType (), typeof(CoreCodecAttribute));
            if (attr == null)
            {
                CoreLog.WriteDebug("Not a valid encoder ... ");
                //throw new CoreException(enuExceptionType.OperationNotValid,
                //    CoreConstant.ERR_ENCODER_ATTR_MUST_BE_SET,
                //    CoreConstant.ERROR_ENCODER_ATTR_MUST_BE_SET);
            }
            else {
                this.m_Extensions.Add(attr.Extensions);
                this.m_Category = attr.Category;
                this.m_mimeType = attr.MimeType;
                this.m_id = attr.Name;
            }
 
        }
        public static string GetFilter(string category)
        {
            StringBuilder sb = new StringBuilder();
            bool t = false;
            var tr = CoreSystem.GetEncoders(category);
            Array.Sort(tr);

            foreach (ICoreEncoder codec in tr)
            {
                if (t)
                    sb.Append("|");
                sb.Append(codec.MimeType);
                sb.Append("|");
                foreach (string ext in codec.Extensions)
	            {
                    sb.Append("*.");
                    sb.Append(ext);
                    sb.Append(";");
	            }
                t = true;
            }
            return sb.ToString ();
        }
        #region ICoreWorkingConfigurableObject Members
        public virtual  enuParamConfigType GetConfigType() { return enuParamConfigType.NoConfig; }
        public virtual ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters){return parameters;}
        public virtual ICoreControl GetConfigControl() { return null; }
        #endregion
        #region ICoreCodec Members
        public string GetFilter()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(CoreSystem.GetString (this.MimeType));
            sb.Append("|");
            foreach (string ext in this.Extensions)
            {
                sb.Append("*.");
                sb.Append(ext);
                sb.Append(";");
            }
            return sb.ToString();
        }
        #endregion

        public static ICoreEncoder GetEncoderById(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            foreach (var item in CoreSystem.GetEncoders())
            {
                if (item.Id == s)
                    return item as ICoreEncoder ;
            }
            return null;

        }

        public override  int CompareTo(object obj)
        {
            ICoreCodec c = obj as ICoreCodec;
            if (this.Category == c.Category) {
                return this.m_mimeType.CompareTo(c.MimeType);
            }
            return this.Category.CompareTo(c.Category);
        }
    }
}

