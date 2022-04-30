

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioTrackDocument.cs
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
file:AudioTrackDocument.cs
*/
using IGK.ICore;using IGK.DrSStudio.AudioBuilder.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioBuilder
{
    [CoreWorkingDocumentAttribute("AudioTrackBuilderDocument")]
    public class AudioTrackDocument : ICoreWorkingDocument 
    {
        #region ICoreIdentifier Members
        public string Id
        {
            get { throw new NotImplementedException(); }
        }
        #endregion
        #region ICoreSerializerService Members
        public void Serialize(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            throw new NotImplementedException();
        }
        public void Deserialize(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            throw new NotImplementedException();
        }
        #endregion
        public Type DefaultSurfaceType
        {
            get { return typeof(XAudioBuilderSurface); }
        }
    }
}

