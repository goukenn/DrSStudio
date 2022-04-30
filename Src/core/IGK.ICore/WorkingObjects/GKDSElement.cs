

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GKDSElement.cs
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
file:GKDSElement.cs
*/
using System;
using System.Collections;
using System.Collections.Generic ;
using System.Runtime.InteropServices;
using IGK.ICore;using IGK.ICore.WorkingObjects;
namespace IGK.ICore
{
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    using IGK.ICore.Xml;
    using IGK.ICore.Resources;
    using IGK.ICore.Drawing2D;
    /// <summary>
    /// represent the base GKDS element for document project
    /// </summary>
    [Serializable()]
    [CoreWorkingObject(CoreConstant.TAG_GKDS_HEADER)]
    public sealed class GKDSElement : CoreWorkingObjectBase, ICoreSerializerService, ICoreLoadingContext       
    {

        public class GKDSElementNode : CoreXmlElement
        {
            public GKDSElementNode(string name, ICoreWorkingObject tag):base(name )
            {
                this.Tag = tag;
            }
            public override string RenderXML(IXmlOptions option)
            {
                return base.RenderXML(option);
            }

          

            protected internal override void Serialize(IXMLSerializer seri)
            {
                
                if (this.Tag is ICoreSerializerService)
                (this.Tag as ICoreSerializerService).Serialize(seri);
            }
            
            public void Deserialize(IXMLDeserializer xreader)
            {
                //.do nothing
            }
        }
        private CoreXmlElement m_gkdsNode;
        private string m_Namespace;
        private ICoreWorkingSurface  m_Surface;

        /// <summary>
        /// get or set the surface
        /// </summary>
        public ICoreWorkingSurface  Surface
        {
            get { return m_Surface; }
            set
            {
                if (m_Surface != value)
                {
                    m_Surface = value;
                }
            }
        }

        public DocumentElement GetDocument()
        {
            return this.getElementTagObjectByTagName(CoreConstant.TAG_DOCUMENTS) as DocumentElement;
        }
        /// <summary>
        /// get the project element
        /// </summary>
        /// <returns></returns>
        public ProjectElement GetProject()
        {
            return this.getElementTagObjectByTagName(CoreConstant.TAG_PROJECT) as ProjectElement;
        }
        /// <summary>
        /// get Resource item by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ICoreResourceItem GetResById(string name)
        {
            var r = GetResourceElement();
            if (r != null)
            {
                return r.Resources.GetResourceById(name);
            }
            return null;
        }
        /// <summary>
        /// get the resource elemement
        /// </summary>
        /// <returns></returns>
        public ResourceElement GetResourceElement()
        {
            return this.getElementTagObjectByTagName(CoreConstant.TAG_RESOURCES) as ResourceElement;
        }
        /// <summary>
        /// get the resource elemement
        /// </summary>
        /// <returns></returns>
        public ResourceElement GetResourceElement(bool createNew)
        {
            var e = GetResourceElement();
            
            if ((e==null) && createNew )
            {
                var p = this.GetProject();
                if (p != null)
                {
                    e = new ResourceElement();
                    this.AddItem(e);
                }
            }
            return e;
        }

        [CoreXMLAttribute ()]
        [CoreXMLDefaultAttributeValue(CoreConstant.DEFAULT_NAMESPACE)]
        public string Namespace
        {
            get { return m_Namespace; }
            set
            {
                if (m_Namespace != value)
                {
                    m_Namespace = value;
                }
            }
        }
        /// <summary>
        /// Get the element root node
        /// </summary>
        public CoreXmlElement RootNode {
            get {
                return this.m_gkdsNode;
            }
        }
        ///// <summary>
        ///// get gkds element
        ///// </summary>
        //public ICoreWorkingObject[] Elements {
        //    get {
        //        return this.m_objects.ToArray();
        //    }
        //}
        public string this[string attribute] {
            get {
                return this.m_gkdsNode[attribute];
            }
        }       
        public GKDSElement()
        {
            //this.m_objects = new List<ICoreWorkingObject>();
            this.m_Namespace = CoreConstant.DEFAULT_NAMESPACE;
            this.m_gkdsNode = CoreXmlElement.CreateXmlNode(CoreConstant.TAG_GKDS_HEADER) as CoreXmlElement ;
        }
        /// <summary>
        /// create a new GDKS Element From a surface
        /// </summary>
        /// <param name="surface"></param>
        /// <returns></returns>
        public static GKDSElement Create(ICoreWorkingSurface surface)
        {
            if (surface == null)
                return null;

            //var ss = surface as IGK.ICore.WinUI.ICoreWorkingProjectManagerSurface;
       
            //var p = ss.GkdsElement;
           
            //var r = p.GetResourceElement();
         

            GKDSElement l = new GKDSElement();
            l.m_surface = surface;
            ResourceElement v_resources = null;
            var mmm = surface.GetProjectElement();
            ProjectElement v_project = mmm ?? new ProjectElement (surface );
            var s = v_project.Parent!=null ? v_project.Parent.GetResourceElement() : null;
            if (s != null) { 
                //link resources definition
                v_resources = s;
            }
            else 
                 v_resources =  new ResourceElement();
            l.AddItem(v_project);
            l.AddItem(v_resources);
            return l;
        }
     
        /// <summary>
        /// create a gkds object
        /// </summary>
        /// <param name="project">project info . it can be null</param>
        /// <param name="documents">list of document to store</param>
        /// <returns></returns>
        public static GKDSElement Create(ICoreProject project , ICoreWorkingDocument[] documents)
        {
            if ((documents == null) || (documents.Length == 0))
                return null;
            GKDSElement l = new GKDSElement();

            if (project != null)
            {
               l.AddItem(project);
            }
            DocumentElement doc = new DocumentElement();
            doc.Documents.AddRange(documents);
            l.AddItem(doc);
            return l;
        }       
        /// <summary>
        /// add item internally to document
        /// </summary>
        /// <param name="item"></param>
        internal void AddItem(ICoreGKDSElementItem item)
        {
            if (item ==null )
                return ;
            string v_name = CoreWorkingObjectAttribute.GetObjectName(item.GetType());
            this.m_gkdsNode.AddChild(new GKDSElementNode(v_name, item));
            
            if (item is GKDSElementItemBase)
            (item as GKDSElementItemBase ).Parent = this;
            
        }
        
        internal protected override void Serialize(IXMLSerializer xwriter)
        {
            xwriter.WriteStartElement(CoreWorkingObjectAttribute.GetObjectName(this.GetType()));
            
            foreach (CoreXmlElement obj in this.m_gkdsNode)
            {
                if (obj is GKDSElementNode)
                    (obj as GKDSElementNode).Serialize(xwriter);
                else {
                    xwriter.WriteRaw(obj.RenderXML(null));
                }
            }
            xwriter.WriteEndElement();
        }
        /// <summary>
        /// deserialize item
        /// </summary>
        /// <param name="xreader"></param>
        public void Deserialize(IXMLDeserializer xreader)
        {
            this.m_gkdsNode.Clear();
            xreader.RegisterLoadingEvent(this);
            if (xreader.Name.ToLower() == this.m_gkdsNode.TagName.ToLower())
            {
                if (xreader.MoveToFirstAttribute())
                {
                    for (int i = 0; i < xreader.AttributeCount ; i++)
                    {
                        this.m_gkdsNode[xreader.Name] = xreader.Value;
                        xreader.MoveToNextAttribute();
                    }
                }
            }
            DocumentElement v_doc = null;
            while (xreader.Read ())
            {
                switch (xreader.NodeType)
                { 
                    case System.Xml.XmlNodeType.Element :
                           IGK.ICore.Codec.ICoreSerializerService
                                            obj = CoreSystem.CreateWorkingObject(xreader.Name)
                                            as IGK.ICore.Codec.ICoreSerializerService;
                                        if (obj != null)
                                        {
                                            obj.Deserialize(xreader.ReadSubtree());
                                            if (obj is ICoreGKDSElementItem)
                                            {
                                                AddItem(obj as ICoreGKDSElementItem);
                                            }
                                            else {
                                                if (v_doc == null)
                                                {
                                                    v_doc = new DocumentElement();
                                                    AddItem(v_doc);
                                                }
                                                if (v_doc.Documents.Count == 0)
                                                {
                                                    ICoreWorkingDocument v_document = GetNewDocument(obj);
                                                    v_doc.Documents.Add(v_document);
                                                }
                                                else { 
                                                    //alway add to the first document
                                                    ICoreWorkingDocument d = v_doc.Documents[0];
                                                    new GKDSElementSetting().AddElementTo(obj, d);
                                                }
                                            }
                                        }
                        break;
                }
            }
            OnLoadingComplete();
        }

        private ICoreWorkingDocument GetNewDocument(ICoreWorkingObject obj)
        {           
            //demand for document 
            ICoreWorkingDocumentContainerName n = Attribute.GetCustomAttribute(obj.GetType(), typeof(CoreWorkingObjectAttribute))
                as ICoreWorkingDocumentContainerName;
            if (n != null)
            {
                var doc = CoreSystem.CreateWorkingObject(n.DefaultDocumentName) as ICoreWorkingDocument;
                if (doc != null)
                {                    
                    new GKDSElementSetting().SetupDocument(doc, obj);
                    return doc;
                }
            }
            return null;
        }
        private void OnLoadingComplete()
        {
            if (this.LoadingComplete != null)
            {
                this.LoadingComplete(this, new CoreLoadingCompleteEventArgs (this));
            }
        }
        /// <summary>
        /// retrieve element by tag name
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public ICoreWorkingObject  getElementTagObjectByTagName(string tagName)
        {
           CoreXmlElementBase[] tab =  m_gkdsNode.getElementsByTagName(tagName);
           if (tab.Length == 1)
           {
               return tab[0].Tag as ICoreWorkingObject ;
           }
           return null;
        }
        public CoreXmlElementBase[] getElementsByTagName(string tagName)
        {
            return m_gkdsNode.getElementsByTagName(tagName);
        }
        /// <summary>
        /// get element by id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ICoreWorkingObject getElementById(string name)
        {
            var m = this.m_gkdsNode.getElementById(name);
            if (m != null)
                return m.Tag as ICoreWorkingObject;
            return null;
            //foreach (ICoreWorkingObject c in this.m_objects)
            //{
            //    if (c.Id == name)
            //        return c;
            //}
            //return null;
        }

      
        public new event CoreLoadingCompleteEventHandler LoadingComplete;
        private ICoreWorkingSurface m_surface;


        public IXMLDeserializer Source
        {
            get {
                return null;
            }
        }

        public ICoreWorkingObject GetElementByTagName(string name)
        {
            return this.getElementTagObjectByTagName(name) as ICoreWorkingObject;
        }

        public ICoreWorkingObject GetElementById(string id)
        {
            throw new NotImplementedException();
        }
    }
}

