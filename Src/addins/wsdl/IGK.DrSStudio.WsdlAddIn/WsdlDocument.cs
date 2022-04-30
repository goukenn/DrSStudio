

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore;using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Wsdl
{

    public class WsdlDocument : IDisposable
    {
        private WsdlVersion m_Version;
        private string m_name;
        private WsdlServiceCollection m_services;
        private WsdlMessageCollection m_Messages;
        private WsdlTypes m_Types;

        public WsdlTypes Types
        {
            get { return m_Types; }

        }
        class WsdlServiceCollection : IEnumerable
        {
            private WsdlDocument wsdlDocument;
            private List<WsdlService> m_services;
            public override string ToString()
            {
                return GetType().Name + "["+this.Count+"]";
            }
            public WsdlServiceCollection(WsdlDocument wsdlDocument)
            {

                this.wsdlDocument = wsdlDocument;
                this.m_services = new List<WsdlService>();
            }
            public int Count { get { return this.m_services.Count; } }

            public IEnumerator GetEnumerator()
            {
                return this.m_services.GetEnumerator();
            }

            public void Add(WsdlService wsdlService)
            {
                this.m_services.Add(wsdlService);

            }
            public void Remove(WsdlService wsdlService)
            {
                this.m_services.Remove(wsdlService);

            }
        }
        class WsdlMessageCollection : IEnumerable
        {
            private WsdlDocument wsdlDocument;
            private List<WsdlMessage> m_messages;
            public WsdlMessageCollection(WsdlDocument wsdlDocument)
            {
                
                this.wsdlDocument = wsdlDocument;
                this.m_messages = new List<WsdlMessage>();
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_messages.GetEnumerator();
            }
            public int Count { get { return this.m_messages.Count; } }

         
            public override string ToString()
            {
                return GetType().Name + "["+this.Count+"]";
            
            }
            /// <summary>
            /// add a message
            /// </summary>
            /// <param name="name"></param>
            internal void Add(string name, string elementName)
            {
                WsdlMessage m = new WsdlMessage();
                m.Name = name;
                m.Part.Name = "parameters";
                m.Part.Element = new WsdlType(){ Name= elementName};
                //this.wsdlDocument.m_Types.Add("PartType");
                this.m_messages.Add (m);
            }
        }
        public WsdlVersion Version
        {
            get { return m_Version; }
            set
            {
                if (m_Version != value)
                {
                    m_Version = value;
                }
            }
        }
        public void Close()
        {
        }
        public WsdlDocument()
        {
            Version = WsdlVersions.Version_1_0;
            this.m_Types = new WsdlTypes();
            this.m_Messages = new WsdlMessageCollection(this);
            this.m_services = new WsdlServiceCollection(this);
            this.m_portType = new WsdlPortType();
        }
        /// <summary>
        /// get the port type
        /// </summary>
        public WsdlPortType portType { get { return this.m_portType; } }
        public string Name { get { return this.m_name; } set { this.m_name = value; } }
        public void Save(string filename)
        {
            CoreXmlElement def = null;
            if (this.Version == WsdlVersions.Version_1_0)
                def = CoreXmlElement.CreateXmlNode("definitions");
            else
                def = CoreXmlElement.CreateXmlNode("description");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");

            //write definition
            def["xmlns"] = "http://schemas.xmlsoap.org/wsdl/";
            def["xmlns:wsdl"] = "http://schemas.xmlsoap.org/wsdl/";
            def["xmlns:http"] = "http://schemas.xmlsoap.org/wsdl/http/"; ;
            def["xmlns:tm"] = "http://microsoft.com/wsdl/mime/textMatching/";
            def["xmlns:soapenc"] = "http://schemas.xmlsoap.org/soap/encoding/";
            def["xmlns:mime"] = "http://schemas.xmlsoap.org/wsdl/mime/";
            def["xmlns:soap"] = "http://schemas.xmlsoap.org/wsdl/soap/";
            def["xmlns:s"] = "http://www.w3.org/2001/XMLSchema";
            def["xmlns:soap12"] = "http://schemas.xmlsoap.org/wsdl/soap12/";
            def["xmlns:http"] = "http://schemas.xmlsoap.org/wsdl/http/";
            def["xmlns:tns"] = this.CiblingTarget;

            def["name"] = this.Name;
            def["targetNamespace"] = this.CiblingTarget ;


            //add types
            def.AddChild(this.Types.GetNode());
            //Creat port types
            foreach (WsdlMessage item in this.Messages)
            {
                def.AddChild(item.GetNode());
            }

            

            def.AddChild(this.m_portType.GetNode());


            //define binding
            WsdlBinding b = null;
            List<WsdlBinding> v_mBinding = new List<WsdlBinding> ();
            bool v_isop = false;
            
#pragma warning disable IDE0054 // Use compound assignment
            for (int i = 1; i<=8  ; i = i<<1)
#pragma warning restore IDE0054 // Use compound assignment
            {
                
                v_isop = false;
                switch(((enuWsdlOperationType) i ))
                {
                    case enuWsdlOperationType.Soap_1_2 :
                        break ;
                    case enuWsdlOperationType.HttpPost :
                        break ;
                    case enuWsdlOperationType.HttpGet :
                        break ;
                    case enuWsdlOperationType.Soap :
                    default :
                        foreach (WsdlOperation  op in this.m_portType.operations)
                        {
                            if (op.Support(enuWsdlOperationType.Soap))
                            {
                                
                                v_isop = true;
                                if (b == null)
                                {
                                    b = new WsdlBinding();
                                    b.Name = "bindingSoap";
                                    b.type = m_portType;
                                    b.soap = new WsdlSoapBinding();
                                }
                                b.operations.Add(new WsdlSoapBindingOperation()
                                {
                                    Name = op.Name,
                                    style = enuWsdlStyle.document,
                                    soapAction = this.CiblingTarget + "/" + op.Name
                                });
                            }
                        }
                        if (v_isop)
                        {
                            def.AddChild(b.GetNode());
                            v_mBinding.Add(b);
                        }
                        b = null;
                        break;
                }
            }



            //add service
            int v_bindex = 0;// for bidding index
            foreach (WsdlService service in this.m_services)
            {

                CoreXmlElement e = service.GetNode();

                WsdlPort p = new WsdlPort();
                p.binding = v_mBinding[v_bindex];//portType;
                CoreXmlElement h = p.GetNode();
                
                h.Add(WsdlUtility.GetNamespace(service.ServiceType)+":address")["location"] = (service.Uri ==null) ? this.CiblingTarget :  service.Uri.ToString();
                e.AddChild(h);

                def.AddChild(e);
                v_bindex++;
            }





            sb.AppendLine(def.RenderXML(null));
            File.WriteAllText(filename, sb.ToString());
        }
        public static WsdlDocument Load(string filename)
        {
            return null;
        }

        public void Dispose()
        {
            this.Close();
        }
        private string m_CiblingTarget;
        private WsdlPortType m_portType;


        public string CiblingTarget
        {
            get { return m_CiblingTarget; }
            set
            {
                if (m_CiblingTarget != value)
                {
                    m_CiblingTarget = value;
                }
            }
        }

        public WsdlService AddService(string servicename)
        {
            var c = new WsdlService(servicename);
            this.m_services.Add(c);
            return c;
        }
        public WsdlService AddService(string servicename, Uri uri)
        {
            var c = new WsdlService(servicename);
            c.Uri = uri;
            this.m_services.Add(c);
            return c;
        }

        public IEnumerable Messages
        {
            get { return this.m_Messages; }
        }
        /// <summary>
        /// add a method with readonly property
        /// </summary>
        /// <param name="name"></param>
        public void AddMethod(string name, string inputTypeElementName, string outputElementName, enuWsdlOperationType operation)
        {
            string inMsg = name + "InputMessage";
            string outMsg = name + "outputMessage";
            this.m_Messages.Add(inMsg , inputTypeElementName );
            this.m_Messages.Add(outMsg, outputElementName );



            //------------------------------------------------------
            //add soap function
            //------------------------------------------------------
            WsdlOperation op = this.m_portType.operations.Add(name);
            op.inputs.Add(inMsg);
            op.outputs.Add(outMsg);

#pragma warning disable IDE0054 // Use compound assignment
            for (int i = 1; i<=8  ; i = i<<1)
#pragma warning restore IDE0054 // Use compound assignment
            {
                switch(((enuWsdlOperationType) i & operation ))
                {
                    case enuWsdlOperationType.Soap_1_2 :
                        break ;
                    case enuWsdlOperationType.HttpPost :
                        break ;
                    case enuWsdlOperationType.HttpGet :
                        break ;
                }
            }
        }



        public WsdlService AddService(string servicename, Uri uri, string initDocumentations)
        {
            WsdlService c = this.AddService(servicename, uri);
            if (c != null) c.Documentation = initDocumentations;
            return c;
        }
    }
}
