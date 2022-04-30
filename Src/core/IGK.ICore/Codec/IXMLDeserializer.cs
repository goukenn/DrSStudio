

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IXMLDeserializer.cs
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
file:IXMLDeserializer.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Schema;
namespace IGK.ICore.Codec
{
    using IGK.ICore;using IGK.ICore.Resources;


    public interface IXMLDeserializer :  
        ICoreDisposableObject ,
        ICoreXmlReader 
    {
        XmlReader XmlReader { get; }
        string BaseDir { get; }
        void ReadToEndElement();
        ICoreWorkingObject CreateWorkingObject(string name);
        event CoreLoadingCompleteEventHandler LoadingComplete;
        /// <summary>
        /// used to register loading context. gkds element first user.
        /// </summary>
        /// <param name="context"></param>
        void RegisterLoadingEvent(ICoreLoadingContext context);
        /// <summary>
        /// get the parent deserializer
        /// </summary>
        IXMLDeserializer Parent { get; }
        /// <summary>
        /// get the loaded working by tag name
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        ICoreWorkingObject GetElementByTagName(string p);

        T GetAttribute<T>(string p, T defaultValue=default(T));
    }
}

