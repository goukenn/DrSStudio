

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioManagerDocument.cs
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
file:AudioManagerDocument.cs
*/
using IGK.ICore;using IGK.DrSStudio.AudioBuilder.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioBuilder
{
    [IGK.DrSStudio.CoreWorkingObject ("AudioManagerDocument")]
    public class AudioManagerDocument : ICoreWorkingDocument 
    {
        IAudioManagerSurface m_surface;
        //for desirialization
        public AudioManagerDocument()
        {
        }
        public AudioManagerDocument(IAudioManagerSurface surface)
        {
            this.m_surface = surface;
        }
        #region ICoreIdentifier Members
        public string Id
        {
            get {
                return "AudioManager";
            }
        }
        #endregion
        #region ICoreSerializerService Members
        public void Serialize(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.WriteAttributes(this, xwriter);
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.WriteElements (this, xwriter);
        }
        public void Deserialize(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            this.m_IsLoading = true;
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.ReadAttributes (this, xreader);
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.ReadAttributes(this, xreader);
            this.m_IsLoading = false;
            OnLoadingComplete(EventArgs.Empty);
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
        public Type DefaultSurfaceType
        {
            get { return typeof(XAudioBuilderSurface); }
        }
    }
}

