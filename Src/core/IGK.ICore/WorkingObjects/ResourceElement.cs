

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ResourceElement.cs
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
file:ResourceElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml;
namespace IGK.ICore.WorkingObjects
{
    using IGK.ICore;using IGK.ICore.Resources;
    using IGK.ICore.Codec ;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent a resource tag elements. is a gkds resources element container
    /// </summary>
     [CoreWorkingObject(CoreConstant.TAG_RESOURCES)]
    public sealed class ResourceElement : GKDSElementItemBase , ICoreGKDSElementItem , ICoreDeserializable 
         //ProjectElement.CoreProjectContainerItemBase, 
         //ICoreProjectItem ,
    {
         private ICoreResourceContainer m_resources;

         /// <summary>
         /// get the resources
         /// </summary>
         public ICoreResourceContainer Resources { get { return this.m_resources; } }

         /// <summary>
         /// .ctrl
         /// </summary>
         internal ResourceElement()//:base(CoreConstant.TAG_RESOURCES, null)
         {
             this.m_resources = new ResourceElelmentContainer(this);
         }
         //internal ResourceElement(ICoreProject project)
         //    : base(CoreConstant.TAG_RESOURCES, project)
         //{
         //    this.m_resources = new ResourceElelmentContainer(this);
         //}
         public static ResourceElement Create(ICoreResourceContainer rs, ICoreProject project)
         {
             if (rs == null)
                 return null;
             ResourceElement v_rs = new ResourceElement();
             v_rs.m_resources = rs;
             return v_rs;
         }
         void ICoreDeserializable.Deserialize(IXMLDeserializer xreader)
         {
             this.Deserialize(xreader);
         }

         protected override void ReadAttributes(IXMLDeserializer xreader)
         {
             base.ReadAttributes(xreader);
         }
         protected override void ReadElements(IXMLDeserializer xreader)
         {
             base.ReadElements(xreader);
         }
         public  void Deserialize(IXMLDeserializer xreader)
         {
             if (xreader.NodeType == System.Xml.XmlNodeType.None )
             xreader.MoveToContent();
             ICoreResourceItem v_rs = null;
             while (xreader.Read())
             {
                 switch (xreader.NodeType )
                 { 
                     case  XmlNodeType.Element :
                         string v_id = xreader.GetAttribute("Id");
                         string v_n = xreader.Name;
                         v_rs = CoreResourceItemBase.CreateResource(v_n, v_id);
                         if (v_rs != null)
                         {
                             CoreResourceItemBase b = v_rs as CoreResourceItemBase;
                             if (b != null)
                             {
                                 b.SetValue(xreader.ReadString());
                                 this.m_resources.Register(v_rs);
                             }
                         }
                         else {
                             var obj = CoreSystem.CreateWorkingObject(xreader.Name)
                                 as ICoreResourceItem;
                             if (obj != null)
                             {
                                 (obj as ICoreSerializerService).Deserialize(xreader.ReadSubtree());
                                 this.m_resources.Register(obj);
                             }
                         }
                         break;
                 }
             }
         }
         
         internal protected override void Serialize(IXMLSerializer xwriter)
         {
             if (this.m_resources.Count == 0)
                 return;
             xwriter.WriteStartElement(CoreWorkingObjectAttribute.GetObjectName(this));
             foreach (KeyValuePair<string, ICoreResourceItem> item in this.m_resources)
             {
                 if (item.Value is ICoreSerializable)
                     (item.Value as ICoreSerializable).Serialize(xwriter);
                 else
                 {
                     xwriter.WriteStartElement(item.Value.ResourceType.ToString());
                     xwriter.WriteAttributeString("Id", item.Value.Id);
                     xwriter.WriteValue(item.Value.GetDefinition());
                     xwriter.WriteEndElement();
                 }
             }
             xwriter.WriteEndElement();
         }
      
         internal object GetElementById(string id)
         {
             if (this.m_resources.Contains(id))
             {
                 return this.m_resources.GetResourceById(id);
             }
             return null;
         }

         internal class ResourceElelmentContainer : ICoreResourceContainer
         {
             private Dictionary<string, ICoreResourceItem> m_resources;
             private ResourceElement m_owner;

             /// <summary>
             /// .ctr
             /// </summary>
             /// <param name="owner"></param>
             public ResourceElelmentContainer(ResourceElement owner)
             {
                 this.m_owner = owner;
                 this.m_resources = new Dictionary<string, ICoreResourceItem>();
             }
             public int Count
             {
                 get { return this.m_resources.Count; }
             }
             public ICoreResourceItem GetResourceById(string name)
             {
                 return this.m_resources[name];
             }
             public bool Contains(string name)
             {
                 if (string.IsNullOrEmpty(name))
                     return false;
                 return this.m_resources.ContainsKey(name);
             }
             public bool Register(ICoreResourceItem resource)
             {
                 if ((resource == null) || this.Contains(resource.Id))
                     return false;
                 this.m_resources.Add(resource.Id, resource);
                 resource.Register(this);
                 return true;
             }
             public void Unregister(ICoreResourceItem resource)
             {
                 if ((resource == null) || !this.Contains(resource.Id))
                     return;
                 this.m_resources.Remove(resource.Id);
             }
             public System.Collections.IEnumerator GetEnumerator()
             {
                 return this.m_resources.GetEnumerator();
             }
             public ICoreWorkingResourcesContainerSurface Surface
             {
                 get {
                     return GetSurface(); 
                 }
             }

             private ICoreWorkingResourcesContainerSurface GetSurface()
             {
                 if (this.m_owner.Parent !=null)
                     return this.m_owner.Parent.Surface as ICoreWorkingResourcesContainerSurface;
                 return null;
             }


             public bool Contains(enuCoreResourceType enuCoreResourceType, string stringDataIdentifier)
             {
                 foreach (var item in this.m_resources)
                 {
                     if ((item.Value.ResourceType == enuCoreResourceType)
                         && item.Value.IsMatch(stringDataIdentifier))
                     {
                        return true;   
                     }
                 }
                 return false;
             }
         }

         
    }
}

