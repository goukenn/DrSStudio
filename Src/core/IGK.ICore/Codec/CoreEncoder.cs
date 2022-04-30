

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreEncoder.cs
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
file:CoreEncoder.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml ;
namespace IGK.ICore.Codec
{
    using IGK.ICore;
    using IGK.ICore.IO;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;

    /// <summary>
    /// represent a system core encoder
    /// </summary>
    [CoreCodec("gkds", "gkds/project", "gkds", Category = "SYSTEM;"+CoreConstant.CAT_PICTURE )]
    public class CoreEncoder : CoreEncoderBase
    {
        private static CoreEncoder sm_instance;
        private CoreEncoder()
        {
        }
        public static CoreEncoder Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreEncoder()
        {
            sm_instance = new CoreEncoder();
            sm_instance.EmbedBitmap = true;
            sm_instance.OmmitXMLDeclaration = true;
            sm_instance.AlwayShowConfiguration = true;
            sm_instance.CopyRight = CoreConstant.COPYRIGHT;
            sm_instance.Author = CoreConstant.AUTHOR;
        }
        private bool m_EmbedBitmap;
        private string m_Author;
        private string m_CopyRight;
        /// <summary>
        /// get/set the copyright
        /// </summary>
        public string CopyRight
        {
            get { return m_CopyRight; }
            set
            {
                if (m_CopyRight != value)
                {
                    m_CopyRight = value;
                }
            }
        }
        /// <summary>
        /// get/set the author
        /// </summary>
        public string Author
        {
            get { return m_Author; }
            set
            {
                if (m_Author != value)
                {
                    m_Author = value;
                }
            }
        }
        public bool EmbedBitmap
        {
            get { return m_EmbedBitmap; }
            set
            {
                if (m_EmbedBitmap != value)
                {
                    m_EmbedBitmap = value;
                }
            }
        }
        private bool m_ZipContains;
        public bool ZipContains
        {
            get { return m_ZipContains; }
            set
            {
                if (m_ZipContains != value)
                {
                    m_ZipContains = value;
                }
            }
        }
        public bool Save(        
           Stream stream,
           ICoreProject project, 
           ICoreWorkingDocument[] documents)
        {
            return Save(null, stream, null, documents);
        }
        /// <summary>
        /// save this surface into a stream
        /// </summary>
        /// <param name="surface">base surface</param>
        /// <param name="stream">stream where to save</param>
        /// <param name="documents">document to save</param>
        /// <returns></returns>
        public bool Save(
            ICoreWorkingSurface surface,
            Stream stream,
            ICoreProject project, 
            ICoreWorkingDocument[] documents)
        {
            XmlWriterSettings v_setting = new XmlWriterSettings();
            v_setting.Indent = true;
            v_setting.CloseOutput = false;
            v_setting.OmitXmlDeclaration = this.OmmitXMLDeclaration;
            v_setting.Encoding = UTF8Encoding.UTF8;
            StringBuilder v_data = new StringBuilder();



            

            string tempFile = Path.GetTempFileName();
            try
            {
                StreamWriter swd = new StreamWriter(tempFile, false, UTF8Encoding.UTF8);
                //var m = swm.Encoding;
                // swm.Encoding = UTF8Encoding.UTF8;

                GKDSElement v_gkds = GKDSElement.Create(surface);

                DocumentElement v_doc = new DocumentElement();
                v_doc.Documents.AddRange(documents);
                v_gkds.AddItem(v_doc);

                CoreXMLSerializer v_seri = CoreXMLSerializer.Create(XmlWriter.Create(swd, v_setting));
                WriteHeader(v_seri.XmlWriter);
                v_seri.EmbedBitmap = this.EmbedBitmap;
                (v_gkds as ICoreSerializable).Serialize(v_seri);
                v_seri.Flush();
                swd.Dispose();

                using (StreamReader sw = new StreamReader(tempFile))
                {
                    sw.BaseStream.WriteTo(stream);
                }
            }
            catch
            {
            }
            finally
            {
                File.Delete(tempFile);
            }


            //StreamWriter sw = new StreamWriter(stream);
            ////  sw.Write(v_data.ToString());
            //swd.BaseStream.Seek(0, SeekOrigin.Begin);
            //sw.Write(new StreamReader(swd.BaseStream).ReadToEnd());
            //sw.Flush();

            //mem.Dispose();




            //writing to memory throw out of memory exception

            // MemoryStream mem = new MemoryStream ();


            // StreamWriter swd = new StreamWriter(mem, UTF8Encoding.UTF8);
            // //var m = swm.Encoding;
            //// swm.Encoding = UTF8Encoding.UTF8;

            // GKDSElement v_gkds = GKDSElement.Create(surface);  

            // DocumentElement v_doc = new DocumentElement();
            // v_doc.Documents.AddRange(documents);            
            // v_gkds.AddItem(v_doc);

            // CoreXMLSerializer v_seri =CoreXMLSerializer.Create (XmlWriter.Create(swd, v_setting));
            // WriteHeader(v_seri.XmlWriter);
            // v_seri.EmbedBitmap = this.EmbedBitmap;
            // (v_gkds as ICoreSerializable ).Serialize(v_seri);
            // v_seri.Flush();

            // StreamWriter sw = new StreamWriter(stream);
            // //  sw.Write(v_data.ToString());
            // swd.BaseStream.Seek(0, SeekOrigin.Begin);
            // sw.Write(new StreamReader(swd.BaseStream).ReadToEnd());
            // sw.Flush();

            // mem.Dispose();

            //StringBuilder v_infosb = new StringBuilder();
            //StringBuilder v_projectsb = new StringBuilder();
            //StringBuilder v_documentsb = new StringBuilder();
            ////store document
            //CoreXMLSerializer v_docserial = CoreXMLSerializer.Create(XmlWriter.Create(v_documentsb, v_setting));
            //v_docserial.EmbedResource = this.EmbedBitmap;

            // CoreXMLSerializer v_cserial = CoreXMLSerializer.Create(XmlWriter.Create(v_projectsb, v_setting));

            ////new document element 
            //_storeDocument(v_docserial, documents);

            //_storeProjectInfo(surface as ICoreWorkingProjectManagerSurface, v_cserial);


            //StringBuilder v_sf = new StringBuilder();
            //XmlWriter xw = XmlWriter.Create(v_sf, v_setting);
            //xw.WriteRaw(v_infosb.ToString());
            ////writer header
            //this.WriteHeader(xw);
            //xw.WriteStartElement(CoreConstant.TAG_GKDS_HEADER);
            ////write project info
            //xw.WriteRaw(v_projectsb.ToString());
            ////write document
            ////XmlDocument doc = new XmlDocument();
            ////doc.LoadXml(v_documentsb.ToString());
            ////doc.WriteTo(xw);
            ////xw.WriteNode(XmlReader.Create(new StringReader(v_documentsb.ToString())), true);
            //xw.WriteRaw(v_documentsb.ToString());
            ////end 
            //xw.WriteEndElement();
            //xw.Flush();
            //xw.Close();
            //StreamWriter sw = new StreamWriter(stream);
            //v_setting.CloseOutput = false;
            //xw = XmlWriter.Create(sw, v_setting);
            //XmlReaderSettings rSetting = new XmlReaderSettings() { IgnoreWhitespace = true };
            //xw.WriteNode(XmlReader.Create(new StringReader(v_sf.ToString()), rSetting), true);
            //xw.Flush();
            //xw.Close();
            return true;
        }

     

        private void _storeProjectInfo(ICoreWorkingProjectManagerSurface surface, CoreXMLSerializer v_cserial)
        {
            //if (surface == null)
            //    return;
            //ProjectElement pr = null;
            //if (surface != null)
            //{
            //    pr = ProjectElement.Create(surface.Pr.ProjectInfo);
            //}
            //else if (project != null)
            //    pr = ProjectElement.Create(project);
            //if (pr != null)
            //{
            //    //      pr.Add(CoreConstant.TAG_RESOURCES, ResourceElement.Create(v_s.Resources, pr));

            //    (pr as ICoreSerializerService).Serialize(v_cserial);
            //    v_cserial.Flush();
            //}
            //else
            //{
            //    ProjectElement v_project = new ProjectElement();
            //    v_project.Add("TargetSurface", CoreWorkingObjectAttribute.GetObjectName(surface.GetType()), null);
            //    CoreXMLSerializer v_cserial = CoreXMLSerializer.Create(
            //                    XmlWriter.Create(v_projectsb, v_setting));
            //    (v_project as ICoreSerializerService).Serialize(v_cserial);
            //    v_cserial.Flush();
            //}
        }

        private void _storeDocuments(CoreXMLSerializer v_docserial, ICoreWorkingDocument[] documents)
        {
            DocumentElement v_doc = new DocumentElement();
            v_doc.Documents.AddRange(documents);
            (v_doc as ICoreSerializerService).Serialize(v_docserial);
            v_docserial.Flush();
        }


        public bool Save(string outfile, ICoreWorkingFileSerializer seri) {
            var document  = seri.Documents;
            if ((document != null) && (document.Length == 0))
                return false;
            using (GKDSElement c = GKDSElement.Create(null, document))
            {
                foreach (ICoreWorkingFileEntity f in seri.Entities) {

                    c.AddItem (f);
                }
                if (c != null)
                {
                    File.WriteAllText(outfile, c.Render());
                    return true;
                }
            }
            return true ;
        }
        public bool Save(string filename, params ICoreWorkingDocument[] document)
        {
            if ((document!=null) && (document.Length == 0))
                return false;
            using (GKDSElement c = GKDSElement.Create(null, document))
            {
                if (c != null)
                {
                    File.WriteAllText(filename, c.Render());
                    return true;
                }
            }
            return false;
        }
        public override bool Save(
            IGK.ICore.WinUI.ICoreWorkingSurface surface,
            string filename,
            ICoreWorkingDocument[] documents
            )
        {
            if ((documents == null) || (documents.Length == 0))
                return false;
            string ext = Path.GetExtension(filename);
            if (ext.ToLower() != CoreConstant.DEFAULTFILEEXTENTION)
#pragma warning disable IDE0054 // Use compound assignment
                filename = filename + CoreConstant.DEFAULTFILEEXTENTION;
#pragma warning restore IDE0054 // Use compound assignment
            string v_temp = Path.GetTempFileName();
            FileStream fs = new FileStream(v_temp, FileMode.Create, FileAccess.ReadWrite);
            string v_dir = Environment.CurrentDirectory;
            //this.BaseDir = PathUtils.GetDirectoryName(filename);
            string v_sdir = PathUtils.GetDirectoryName(filename);
            if (System.IO.Directory.Exists(v_sdir))
                Environment.CurrentDirectory = v_sdir;
            bool v_result = this.Save(surface, fs, null, documents);
            //restore directory
            Environment.CurrentDirectory = v_dir;
            fs.Close();
            //move the tempory file to end
            try
            {
                if (File.Exists(filename))
                    File.Delete(filename);
                File.Move(v_temp, filename);
            }
            catch {
                return false;
            }
            return v_result;
        }
        private void WriteHeader(XmlWriter xwriter)
        {
            xwriter.WriteComment(CoreConstant.SYSTEM_APPINFO);
            xwriter.WriteComment(this.Author );
            xwriter.WriteComment(this.CopyRight);
        }
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var v_group = parameters.AddGroup("UserInfo", "lb.userInfo.caption");
            v_group.AddItem("Author", "lb.author.caption", this.Author, enuParameterType.Text, null);
            v_group.AddItem("SystemVersion", "lb.sysversion.caption", this.CopyRight, enuParameterType.Text, null);
            v_group = parameters.AddGroup("CodecDefinition", "lb.CodecDefinition.caption");
            Type t = this.GetType();
            v_group.AddItem(t.GetProperty("EmbedBitmap"));
            v_group.AddItem(t.GetProperty("OmmitXMLDeclaration"));

            
            v_group.AddItem(t.GetProperty("AlwayShowConfiguration"));
            return parameters;
        }
         public override ICoreControl GetConfigControl()
        {
            return null;
        }
        /// <summary>
        /// save with this current encoder
        /// </summary>
        /// <param name="v_encod"></param>
        /// <param name="surface"></param>
        /// <param name="filename"></param>
        /// <param name="documents"></param>
        /// <returns></returns>
        public bool SaveWithEncoder(ICoreCodec v_encod, 
            ICoreWorkingSurface surface, 
            string filename,
            ICoreWorkingDocument[] documents)
        {
            CoreEncoderBase enc = v_encod as CoreEncoderBase ;
            if ((enc !=null) && !(enc is CoreEncoder))
            {
                enc.Save(surface, filename, documents);
            }
            if (!(v_encod is CoreEncoder) && IGK.ICore.Settings.CoreApplicationSetting.Instance.SaveBothGkdsFileAndOther)
            {
                return this.Save(surface , filename, documents);
            }
            return false;
        }

        public bool OmmitXMLDeclaration { get; set; }
    }
}

