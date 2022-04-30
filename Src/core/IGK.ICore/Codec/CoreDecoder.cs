

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreDecoder.cs
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
file:CoreDecoder.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection ;
using System.IO;
using System.Diagnostics;
using System.Linq;
namespace IGK.ICore.Codec
{
    using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WorkingObjects;
    using IGK.ICore.Resources;
    using IGK.ICore.IO;
    /// <summary>
    /// Represent the base gkds system decoder.
    /// </summary>
    [CoreCodec("gkds", "gkds/decoder", "gkds", Category="All")]
    public sealed class CoreDecoder : 
        CoreDecoderBase,
        ICoreBitmapDecoder,
        IGK.ICore.Codec.ICoreDocumentDecoder
    {
        private static CoreDecoder sm_instance;
        public CoreDecoder()
        {
        }
        public static CoreDecoder Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreDecoder()
        {
            sm_instance = new CoreDecoder();
        }
//        /// <summary>
//        /// open file and add it to Workbench
//        /// </summary>
//        /// <param name="bench"></param>
//        /// <param name="filename"></param>
//        /// <returns></returns>
//        public override bool Open(IGK.ICore.WinUI.ICoreWorkBench bench, string filename)
//        {
//            if (bench == null)
//                throw new CoreException(enuExceptionType.ArgumentIsNull, "bench");
//            if (string.IsNullOrEmpty (filename))
//                throw new CoreException(enuExceptionType.ArgumentIsNull, "filename");
//            if (bench.MainForm.InvokeRequired)
//            {
//                bool cv = false;
//                bench.MainForm.Invoke((CoreMethodInvoker)delegate() {
//                    cv = Open(bench, filename);
//                });
//                return cv;
//            }
//            string v_surfaceType = string.Empty;
//            XmlReaderSettings v_xsetting = new XmlReaderSettings();
//            v_xsetting.IgnoreComments = true;
//            v_xsetting.IgnoreWhitespace = true;
//            List<ICoreWorkingSurface> m_surfaces = new List<ICoreWorkingSurface>();
//            CoreXMLDeserializer v_deseri = null;
//            IGK.ICore.Xml.CoreXmlElement v_gkdsnode = null;
//            ProjectElement pr = null;
//            //list of dictionary element
//            Dictionary<string, ICoreWorkingObject> m_list = new Dictionary<string, ICoreWorkingObject>();
//            string f = filename ;
//            ICoreWorkingSurface v_s = null;
//                try
//                {
//                    v_deseri = CoreXMLDeserializer.Create(XmlReader.Create(f, v_xsetting));
//                    if (v_deseri == null)
//                        return false ;
//                        bool v_goodfile = false ;
//                        while (v_deseri.Read())
//                        {
//                            switch (v_deseri.NodeType)
//                            {
//                                case XmlNodeType.Element:
//                                    if (v_deseri.Name.ToLower() == CoreConstant.XMLHeader)
//                                    {
//                                        v_goodfile = true;
//                                        v_gkdsnode = IGK.ICore.Xml.CoreXmlElement.CreateXmlNode (CoreConstant.XMLHeader )
//                                            as IGK.ICore.Xml.CoreXmlElement;
//                                        if (v_deseri.MoveToFirstAttribute ())
//                                        {
//                                            for (int i = 0; i < v_deseri.AttributeCount; i++)
//                                            {
//                                                v_gkdsnode[v_deseri.Name] = v_deseri.Value ;
//                                            }
//                                        }
//                                        //good file
//                                        continue;
//                                    }
//                                    if (v_goodfile)
//                                    {
//                                        IGK.ICore.Codec.ICoreSerializerService
//                                            obj = CoreSystem.CreateWorkingObject(v_deseri.Name)
//                                            as IGK.ICore.Codec.ICoreSerializerService;
//                                        if (obj != null)
//                                        {
//                                            m_list.Add(v_deseri.Name, obj);
//                                            obj.Deserialize(v_deseri.ReadSubtree());
//                                        }
//                                    }
//                                    break;
//                            }
//                        }
//                        if (m_list.ContainsKey(CoreConstant.TAG_PROJECT))
//                        {
//                            pr = m_list[CoreConstant.TAG_PROJECT] as ProjectElement;
//                            v_surfaceType = pr.SurfaceType;
//                        }
//                        else {
//                            v_surfaceType = v_gkdsnode[CoreConstant.TAG_SURFACETYPE];
//                        }
//                            Type v_t = CoreSystem.GetWorkingObjectType(v_surfaceType);
//                            if (v_t != null)
//                            {
//                                MethodInfo m = v_t.GetMethod(
//                                    CoreConstant.METHOD_CREATESURFACE,
//                                     System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic,
//                                     null,
//                                     new Type[]{
//                                         typeof (ICoreProject ),
//                                         typeof (ICoreWorkingDocument[])
//                                     },
//                                     null);
//                                //set the method
//                                if (m == null)
//                                {
//                                    throw new CoreException(enuExceptionType.RequiredMethodNotFound, CoreConstant.METHOD_CREATESURFACE);
//                                }
//                                v_s = m.Invoke(null,
//                                    new object[] {
//                                        pr,
//                                    (m_list[CoreConstant.TAG_DOCUMENTS] as DocumentElement).Documents.ToArray ()
//                                    })
//                                    as ICoreWorkingSurface;
//                                if (v_s != null)
//                                {
//                                    bench.AddSurface(v_s,true);
//                                }
//                            }
//                            else
//                                throw new CoreException(string.Format("Surface {0} Not found", pr.SurfaceType));
//                        v_deseri.Resources.RaiseLoadingComplete();
//                }
//                catch (Exception ex)
//                {
//                    CoreLog.WriteDebug("Error when read elements : " + ex.Message);
//#if DEBUG
//                    if (ex is CoreException)
//                        CoreMessageBox.ShowError(ex as CoreException);
//                    else {
//                        CoreMessageBox.ShowError(string.Format ("Message: {0}\nTrace: {1}",ex.Message,ex.StackTrace));
//                    }
//#endif
//                }
//                finally
//                {
//                    if (v_deseri != null)
//                        v_deseri.Close();
//                }
//                return true;
//        }
        private static ICoreWorkingSurface CreateSurface(Type surfaceType, GKDSElement node)
        {
            if ((surfaceType == null)&&(node == null))
                return null;
            ICoreWorkingSurface v_s = null;
            if (surfaceType != null)
            {
                MethodInfo m = surfaceType.GetMethod(
                    CoreConstant.METHOD_CREATESURFACE,
                     System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic,
                     null,
                     new Type[]{
                            typeof (GKDSElement )
                            },
                     null);
                //set the method
#if DEBUG
                Debug.Assert(m != null, surfaceType.FullName + "." + CoreConstant.METHOD_CREATESURFACE + " Not found");
#endif
                if (m != null)
                {
                    v_s = m.Invoke(null,
                        new object[] { node })
                        as ICoreWorkingSurface;
                }
            }
            return v_s;
        }
       
        public override bool Open(ICoreWorkbench bench, string filename,  bool selectCreatedSurface)
        {
            bool v_out = true;
            ICoreApplicationWorkbench c= ((ICoreApplicationWorkbench)bench);
            if (c == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "bench");
            if (string.IsNullOrEmpty(filename))
                throw new CoreException(enuExceptionType.ArgumentIsNull, "filename");
            if (!File.Exists(filename)) return false;
            if (c.MainForm.InvokeRequired)
            {
                bool cv = false;
                c.MainForm.Invoke((CoreMethodInvoker)delegate()
                {
                    cv = Open(bench, filename, selectCreatedSurface);
                });
                return cv;
            }

            Environment.CurrentDirectory = PathUtils.GetDirectoryName(filename);

            var g = new FileInfo(filename);
            if (g.Length == 0) {

                var s = CoreSystem.CreateWorkingObject(CoreConstant.DRAWING2D_SURFACE_TYPE)
                    as ICoreWorkingSurface ;
                if (s != null)
                {
                    bench.AddSurface(s, selectCreatedSurface);
                    return true;
                }

            }

            string v_surfaceType = string.Empty;
            XmlReaderSettings v_xsetting = new XmlReaderSettings();
            v_xsetting.IgnoreComments = true;
            v_xsetting.IgnoreWhitespace = true;
            CoreXMLDeserializer v_deseri = null;
            ProjectElement pr = null;
            //list of dictionary element
            Dictionary<string, ICoreWorkingObject> m_list = new Dictionary<string, ICoreWorkingObject>();
            string f = filename;
            ICoreWorkingSurface v_s = null;
            GKDSElement v_gkds = null;
            try
            {
                v_deseri = CoreXMLDeserializer.Create(XmlReader.Create(f, v_xsetting));
                if (v_deseri == null)
                    return false;
                bool v_goodfile = false;
                if (v_deseri.ReadToDescendant(CoreConstant.TAG_GKDS_HEADER))
                {
                    v_goodfile = true;
                    v_gkds = new GKDSElement();
                    v_gkds.Deserialize(v_deseri);
                }
                //while (v_deseri.Read())
                //{
                //    switch (v_deseri.NodeType)
                //    {
                //        case XmlNodeType.Element:
                //            if (v_deseri.Name.ToLower() == CoreConstant.TAG_GKDS_HEADER)
                //            {
                //                v_goodfile = true;
                //                v_gkds = new GKDSElement();
                //                v_gkds.Deserialize(v_deseri);
                //                continue;
                //            }
                //            break;
                //    }
                //}
                if (!v_goodfile || (!v_gkds.RootNode.HasChild))
                {
                    return false;
                }
                ICoreWorkingDocument[] v_documents = (v_gkds.getElementTagObjectByTagName(CoreConstant.TAG_DOCUMENTS) as DocumentElement).Documents.ToArray();
                pr = v_gkds.getElementTagObjectByTagName(CoreConstant.TAG_PROJECT) as ProjectElement;
                if (pr != null)
                {
                    v_surfaceType = pr.SurfaceType;
                    pr.Add("FileName", filename, null);
                }
                else
                {
                    pr = new ProjectElement();
                    pr.Add("FileName", filename, null);
                    v_surfaceType = v_gkds[CoreConstant.TAG_SURFACETYPE];
                    var node = v_gkds.RootNode.Add(CoreConstant.TAG_PROJECT);
                    node.Tag = pr;
                    node.Add("FileName").Content = filename;

                }
                //check for document validity
                if (v_surfaceType != null)
                {
                    Type t = CoreSystem.GetWorkingObjectType(v_surfaceType);
                    v_s = CreateSurface(t, v_gkds);
                    if (v_s != null)
                    {
                        bench.AddSurface(v_s, selectCreatedSurface);
                    }
                    else
                        throw new CoreException(string.Format("Surface {0} can't be created ", pr.SurfaceType));
                }
                else
                {
                    //no surface type determine to render document
                    v_s = CreateSurface(v_documents[0].DefaultSurfaceType, v_gkds);// pr, v_documents);
                    if (v_s != null)
                    {
                        bench.AddSurface(v_s, selectCreatedSurface);
                        return true;
                    }
                    return false;
                }
            }
            catch (OutOfMemoryException ex) {
                CoreLog.WriteDebug($"OutOfMemory : {ex.Message}");
                v_out = false;
            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug("Error when read elements : " + ex.Message);
#if DEBUG
                if (ex is CoreException)
                    CoreMessageBox.Show(ex);
                else
                {
                    CoreMessageBox.Show(string.Format("Message: {0}\nTrace: {1}", ex.Message, ex.StackTrace));
                }
#endif
            }
            finally
            {
                if (v_deseri != null)
                    v_deseri.Close();
            }
            return v_out;
        }
        public ICoreWorkingDocument[] OpenFileDocument(string file)
        {
            return Open2DDocument(file);
        }
        public ICore2DDrawingDocument[] Open2DDocument(string file)
        {
            byte[] t = File.ReadAllBytes(file);
            ICoreWorkingDocument[] tab = GetDocuments(t);
            if ((tab == null) || (tab.Length == 0))
                return null;
            List<ICore2DDrawingDocument> v_document = new List<ICore2DDrawingDocument>();
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] is ICore2DDrawingDocument)
                {
                    v_document.Add(tab[i] as ICore2DDrawingDocument);
                }
            }
            return v_document.ToArray();
        }

        public ICoreWorkingDocument[] Open2DDocument(Stream stream)
        {
            try
            {
                StreamReader r = new StreamReader(stream);
                Byte[] t = UTF8Encoding.Default.GetBytes(r.ReadToEnd());
                //dont close the stream
                r.Close();
                return GetDocuments(t);
            }
            catch {
                CoreLog.WriteLine("Open2DDocument:: can't extract stream");
            }
            return null;
        }
        /// <summary>
        /// open 2D document from bytes, resources
        /// </summary>
        /// <param name="data">data byte to converts</param>
        /// <returns></returns>
        public ICore2DDrawingDocument[] Open2DDocument(byte[] data)
        {
            ICoreWorkingDocument[] doc=  GetDocuments(data);
            if (doc != null) {
                return doc.ConvertTo<ICore2DDrawingDocument>();
            }
            return null;
        }
        /// <summary>
        /// get the document form  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ICoreWorkingDocument[] GetDocuments(byte[] data)
        {
            XmlReaderSettings v_xsetting = new XmlReaderSettings();
            v_xsetting.IgnoreComments = true;
            v_xsetting.IgnoreWhitespace = true;
            v_xsetting.CloseInput = true;
            CoreXMLDeserializer v_deseri = null;            
            MemoryStream mem = new MemoryStream();
            mem.Write(data, 0, data.Length);
            mem.Seek(0, SeekOrigin.Begin);
            v_deseri = CoreXMLDeserializer.Create (XmlReader.Create(mem, v_xsetting ));
            Dictionary<string, ICoreWorkingObject> m_list = GetListObject(v_deseri);
            v_deseri.Close();
            if (m_list.ContainsKey(CoreConstant.TAG_DOCUMENTS ))
            {
               // object obj = m_list[CoreConstant.TAG_DOCUMENTS];
                DocumentElement doc = m_list[CoreConstant.TAG_DOCUMENTS] as DocumentElement;
                if (doc !=null)
                return doc.Documents.ToArray ();
            }
            return null;
        }

        
        
        /// <summary>
        /// return a list of objet containt in this document form string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Dictionary<string, ICoreWorkingObject> GetListObject(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            MemoryStream mem = new MemoryStream();
            StreamWriter m = new StreamWriter(mem);
            m.Write(value);
            m.Flush();
            mem.Seek(0, SeekOrigin.Begin);
            CoreXMLDeserializer v_deseri = CoreXMLDeserializer.Create(XmlReader.Create(mem));
            Dictionary<string, ICoreWorkingObject> m_list = new Dictionary<string, ICoreWorkingObject>();

            if (v_deseri.ReadToDescendant(CoreConstant.TAG_GKDS_HEADER))
            {
                while (v_deseri.Read())
                {
                    switch (v_deseri.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (v_deseri.Name == CoreConstant.TAG_GKDS_HEADER)
                            {
                                //bad structure
                                continue;
                            }
                            IGK.ICore.Codec.ICoreSerializerService
                                obj = CoreSystem.CreateWorkingObject(v_deseri.Name)
                                as IGK.ICore.Codec.ICoreSerializerService;
                            if (obj != null)
                            {
                                m_list.Add(v_deseri.Name, obj);
                                obj.Deserialize(v_deseri.ReadSubtree());
                            }
                            else
                            {
                                v_deseri.Skip();
                                CoreLog.WriteLine("CoreDecoder: Element Skip : " + v_deseri.Name);
                            }
                            break;
                    }
                }
            }
            v_deseri.Close();
            return m_list;
        }

        public Dictionary<string, ICoreWorkingObject> GetListObject(byte[] value)
        {
            if (value==null)
                return null;
            return GetListObject(UTF8Encoding.UTF8.GetString(value));
        }
        public Dictionary<string, ICoreWorkingObject> GetListObject(IXMLDeserializer deseri)
        {
            Dictionary<string, ICoreWorkingObject> v_list = new Dictionary<string, ICoreWorkingObject>();
            ICoreWorkingObject v_cobj = null;
            ICoreSerializerService v_obj = null;
            if (deseri.ReadToDescendant(CoreConstant.TAG_GKDS_HEADER))
            {
                while (deseri.Read())
                {
                    switch (deseri.NodeType)
                    {
                        case XmlNodeType.Element:
                            v_cobj = deseri.CreateWorkingObject (deseri.Name);
                            v_obj = v_cobj as ICoreSerializerService;
                            if (v_cobj !=null)
                                v_list.Add(deseri.Name, v_cobj);
                            if (v_obj != null)
                            {
                                v_obj.Deserialize(deseri.ReadSubtree());
                            }
                            else
                            {
                               deseri.ReadToEndElement ();                            
                            }
                            break;
                    }
                }
            }
            return v_list;
        }

        public ICore2DDrawingLayeredElement Inflate(byte[] resourcePresentation)
        {

            if (resourcePresentation == null)
                return null;
            Dictionary<string, ICoreWorkingObject> s = GetListObject(resourcePresentation);
            if ((s == null) || (s.Count == 0))
            {
                return null;
            }
            return s.Values.First() as ICore2DDrawingLayeredElement;
        }
        public ICore2DDrawingLayeredElement Inflate(string resourcePresentation)
        {

            Dictionary<string, ICoreWorkingObject> s = GetListObject(resourcePresentation);
            if (s.Count == 0)
            {
                return null;
            }
            return s.Values.First() as ICore2DDrawingLayeredElement;
        }
        public ICoreBitmap GetBitmap(string file)
        {
            var v_doc = Open2DDocument(file);
            if ((v_doc != null)&& (v_doc.Length > 0)) {
                return v_doc[0].ToBitmap();
            }
            return null;
        }
        /// <summary>
        /// open a single working document
        /// </summary>
        /// <param name="coreWorkbench"></param>
        /// <param name="document"></param>
        public void OpenDocument(ICoreSystemWorkbench coreWorkbench, ICoreWorkingDocument document)
        {
            if (document.DefaultSurfaceType != null)
            {
                GKDSElement l = GKDSElement.Create(null, new ICoreWorkingDocument[] { document });                
                var v = CreateSurface(document.DefaultSurfaceType, l);
                if (v != null) {
                    coreWorkbench.AddSurface(v, true);
                }
            }
        }
    }
}

