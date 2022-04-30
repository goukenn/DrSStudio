

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingFilterBase.cs
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
file:Core2DDrawingFilterBase.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Filters
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    public abstract class Core2DDrawingFilterBase : 
        ICore2DDrawingFilter,
        ICoreWorkingConfigurableObject 
    {
        public abstract bool Activated { get; }
        public abstract string Name { get; }
        public event EventHandler FilterPropertyChanged;
        static Core2DDrawingFilterBase()
        {   
        }
        public static Core2DDrawingFilterBase CreateFilter(string name)
        {
            Type t = typeof(Core2DDrawingFilterBase);
            Type o = Type.GetType (string.Format ("{0}.{1}Filter",t.Namespace , name ));
                if (o!=null)
                {
                    return o.Assembly .CreateInstance (o.FullName ) as Core2DDrawingFilterBase ;
                }
            return null;
        }
        protected virtual void OnFilterPropertyChanged(EventArgs e)
        {
            if (this.FilterPropertyChanged != null)
                this.FilterPropertyChanged(this, e);
        }
        #region ICore2DDrawingFilter Members
        public abstract bool ApplyFilter(ref System.Drawing.Bitmap bmp);
        #endregion
        #region ICoreSerializerService Members
        public void Serialize(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            this.SaveAttributes(xwriter);
            this.SaveElements(xwriter);
        }
        protected virtual void SaveAttributes(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
        }
        protected virtual void SaveElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
        }
        public void Deserialize(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            this.m_IsLoading = true;
            this.ReadAttributes(xreader);
            this.ReadElements(xreader);
            this.m_IsLoading = false;
            OnLoadingComplete(EventArgs.Empty);
        }
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
        protected virtual void ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
        }
        protected virtual void ReadAttributes(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
        }
        #endregion
        #region ICoreIdentifier Members
        string ICoreIdentifier.Id
        {
            get { return this.Name; }
        }
        #endregion
        #region ICoreWorkingConfigurableObject Members
        public virtual enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public virtual IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            return parameters;
        }
        public virtual  ICoreControl GetConfigControl()
        {
            return null;            
        }
        #endregion
    }
}

