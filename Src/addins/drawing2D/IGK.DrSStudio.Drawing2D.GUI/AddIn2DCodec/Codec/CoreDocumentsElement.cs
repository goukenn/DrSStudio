

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreDocumentsElement.cs
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
file:CoreDocumentsElement.cs
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
using System.Text;
namespace IGK.DrSStudio.Codec
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio ;
    using IGK.DrSStudio.Codec ;
    //[CoreWorkingObject ("Documents")]
    //[Serializable ()]
    //public sealed class CoreDocumentsElement : 
    //    MarshalByRefObject ,
    //    ICoreWorkingObject ,
    //    ICoreSerializerService
    //{
    //    private ICoreWorkingDocument[] m_Documents;
    //    public ICoreWorkingDocument[] Documents
    //    {
    //        get { return m_Documents; }
    //        set
    //        {
    //            if (m_Documents != value)
    //            {
    //                m_Documents = value;
    //            }
    //        }
    //    }
    //    public static CoreDocumentsElement Create(ICoreWorkingDocument[] documents)
    //    {
    //        if ((documents ==null) || (documents .Length == 0))
    //            return null;
    //        CoreDocumentsElement v_document = new CoreDocumentsElement ();
    //        v_document.m_Documents = documents;
    //        return v_document;
    //    }
    //    #region ICoreSerializerService Members
    //    public void Serialize(IXMLSerializer xwriter)
    //    {
    //        IGK.DrSStudio.Codec.CoreXMLSerializerUtility.WriteStartElement(this, xwriter);
    //        for (int i = 0; i < this.m_Documents.Length; i++)
    //        {
    //            if (this.m_Documents[i] != null)
    //            {
    //                this.m_Documents[i].Serialize(xwriter);
    //            }
    //        }
    //        xwriter.WriteEndElement();
    //    }
    //    public void Deserialize(IXMLDeserializer xreader)
    //    {
    //        this.m_IsLoading = true;
    //        List<ICoreWorkingDocument> m_doc = new List<ICoreWorkingDocument>();
    //        if (xreader.NodeType == System.Xml.XmlNodeType.None)
    //        {
    //            xreader.MoveToContent();
    //        }
    //        while (xreader.Read())
    //        {
    //            switch (xreader.NodeType)
    //            {
    //                case System.Xml.XmlNodeType.Element :
    //                    xreader.CreateWorkingObject(xreader.Name);
    //                    ICoreWorkingDocument
    //                                    obj = CoreSystem.CreateWorkingObject(xreader.Name)
    //                                    as ICoreWorkingDocument;
    //                    if (obj is ICoreWorkingDocument)
    //                    {
    //                        m_doc.Add(obj);
    //                        obj.Deserialize(xreader.ReadSubtree());
    //                    }
    //                    else
    //                        xreader.Skip();
    //                    break;
    //            }
    //        }
    //        this.m_Documents = m_doc.ToArray();
    //        this.m_IsLoading = false;
    //        OnLoadingComplete(EventArgs.Empty);
    //    }
    //    #endregion
    //    #region ICoreIdentifier Members
    //    public string Id
    //    {
    //        get {
    //            return null;
    //        }
    //    }
    //    #endregion
    //    public override string ToString()
    //    {
    //        return string.Format("Documents [Count: {0}] ", this.m_Documents.Length);
    //    }
    //    private bool m_IsLoading;
    //    public bool IsLoading
    //    {
    //        get { return m_IsLoading; }
    //    }
    //    public event EventHandler LoadingComplete;
    //    ///<summary>
    //    ///raise the LoadingComplete 
    //    ///</summary>
    //    void OnLoadingComplete(EventArgs e)
    //    {
    //        if (LoadingComplete != null)
    //            LoadingComplete(this, e);
    //    }
    //}
}

