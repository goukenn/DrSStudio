

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreResourcesCollections.cs
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
file:CoreResourcesCollections.cs
*/

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Resources;
namespace IGK.ICore
{
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.IO.Files;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.Settings;
    using IGK.ICore.IO;
    using IGK.ICore.Codec;
    /// <summary>
    /// represent the core resources manager object
    /// </summary>
    [Serializable()]
    public class CoreResourcesManager :
        MarshalByRefObject,
        ICoreResources,
        IDisposable
    {
        private bool m_stringLoadedFromFile;
        private bool m_objLoaded;
        private CoreSystem m_core;
        private Dictionary<string, string> m_strings;
        private Dictionary<string, ICore2DDrawingDocument[]> m_documents;
        private Dictionary<string, ICoreBitmap> m_pictures; //bitmap objects
        private Dictionary<string, object> m_Object; //object query
        private Dictionary<string, XCursor> m_rCursors; //cursor object
        private List<Assembly> m_asm; //list of assembly loaded
        private Assembly m_defres;
        
        /// <summary>
        /// override to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "CoreResourcesCollections[Document:" + this.m_documents.Count + "; Strings:" + this.m_strings.Count + "]";
        }
        /// <summary>
        /// private .ctr
        /// </summary>
        private CoreResourcesManager()
        {
            m_strings = new Dictionary<string, string>();
            m_documents = new Dictionary<string, ICore2DDrawingDocument[]>();
            m_pictures = new Dictionary<string, ICoreBitmap>();// new Dictionary<string, Bitmap>();
            m_Object = new Dictionary<string, object>();
            m_rCursors = new Dictionary<string, XCursor>();
            m_asm = new List<Assembly>();
            this.m_objLoaded = false;
            this.m_stringLoadedFromFile = false;
        }

        /// <summary>
        /// get array keys object present
        /// </summary>
        /// <returns></returns>
        public string[] GetKeys()
        {
            List<string> g = new List<string>();
            foreach (var item in this.m_Object.Keys)
            {
                g.Add(item);
            }
            return g.ToArray();//this.m_Object.Keys.CoreConvertTo<string[]>();
        }
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="m_core"></param>
        internal CoreResourcesManager(CoreSystem m_core)
            : this()
        {
            this.m_core = m_core;
            m_core.RegisterAssemblyLoader(InitLoadAssemblyResources);
            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                InitLoadAssemblyResources(item);
            }
            m_core.RegisterLoadingComplete(LoadAsmResources);

            CoreApplicationManager.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }
        void Application_ApplicationExit(object sender, EventArgs e)
        {
            SaveResources();
            this.Dispose();
        }

        private void SaveResources()
        {
            //save language on application exit
            //this.SaveLangResources();
            //this.SaveDocumentAndObjectResources();
#if !DEBUG
            CoreLog.WriteDebug("::SaveResources...");
            this.SaveLangResources();
            this.SaveDocumentAndObjectResources();
#endif
            //realase resource
        }
        #region ICoreResources Members
        /// <summary>
        /// load assembly resources
        /// </summary>
        void LoadAsmResources()
        {
            if (!this.m_stringLoadedFromFile || !this.m_objLoaded)
            {
                foreach (Assembly item in this.m_asm)
                {
                    LoadAssemblyResources(item);
                }
            }
            else
            {
                InitLoadedObject();
            }
            m_asm.Clear();
            
            CoreApplicationManager.Application.ResourcesManager?.Init();
        }
        private void InitLoadedObject()
        {
            foreach (KeyValuePair<string, object> item in this.m_Object)
            {
                if (item.Value is byte[])
                {
                    this.LoadDocumentObject(item.Key.ToLower(), item.Value as byte[]);
                }
            }
        }
        public string GetString(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;
            const string regex = @"([\[]|[^\[]*)\[(?<value>(([^\[\]])+))\]([\]]|[^\]]*)";
            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            string vkey = key.ToLower();
            string rs = vkey;
            if (!rg.IsMatch(key))
            {
                if (this.m_strings.ContainsKey(vkey))
                {
                    rs = this.m_strings[vkey];
                }
                else
                {
#if DEBUG
                    if (key.ToUpper().StartsWith("DE_"))
                    {
                    }
                    CoreLog.WriteLine("New keys : " + key);

#endif
                    this.m_strings[vkey] = key;
                    rs = key;
                }
            }
            //checking for result
            if (rg.IsMatch(rs))
            {
                System.Text.RegularExpressions.MatchEvaluator m = delegate(System.Text.RegularExpressions.Match s)
                {
                    if (s.Value.StartsWith("[["))
                        return s.Value.Replace ("[[","[").Replace ("]]","]");;
                    string group = s.Groups["value"].Value.ToLower();                    
                    if (m_strings.ContainsKey (group))
                        return GetString(group);
                    if (s.Value == rs)
                        return rs;
                    return s.Value;//no replace
                };
                rs = rg.Replace(rs, m);                
            }
            return CoreSystemEnvironment.EvalString(rs);
        }
        /// <summary>
        /// get string width parameters
        /// </summary>
        /// <param name="lkey"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetString(string lkey, params object[] obj)
        {
            string v = GetString(lkey);
            if ((obj == null) || string.IsNullOrEmpty(v))
                return v;
            if ((obj != null) && (v == lkey))
            {
                //append string to argument
                //for (int i = 0; i < obj.Length; i++)
                //{
                //    v += " {" + i + "}";
                //}
                return string.Format(v, obj);
            }
            //check if argument
            if ((obj == null) || (obj.Length == 0))
                return v;
            else
                return string.Format(v, obj);
        }
        /*  public XCursor GetCursor(string key, ICoreScreen screen)
          {
             // m_rCursor.Clear();
              key = key.ToLower();
              if (string.IsNullOrEmpty(key))
                  return null;
              if (m_rCursor.ContainsKey(key))
              {
                  return m_rCursor[key];
              }
              //#if WINDOWS
              ICore2DDrawingDocument doc = GetDocument(key, 0);
              ICore2DDrawingDocument mergeDoc = null;
              ICoreBitmap bmp = null;
              if (doc == null)
              {//merge 2 document
                  doc = GetDocument("cross", 0);
                  if (doc == null)
                      return XCursor.Default;
                  bmp = new Bitmap(64, 64);
                  string n = key.Split('_')[1];
                  mergeDoc = GetDocument("DE_" + n, 0);
                  using (Graphics g = Graphics.FromImage(bmp))
                  {
                      doc.Draw(g, new Rectanglei(0, 0, 32, 32));
                      if (mergeDoc != null)
                      {
                          mergeDoc.Draw(g, new Rectanglei(18, 18, 16, 16));
                      }
                  }
              }
              else
              {
                  bmp = CoreBitmapOperation.GetBitmap(doc, screen.DpiX, screen.DpiY);
              }
              if (bmp != null)
              {
                  //Cursor cr = CoreBitmapOperation.GetCursorFrom(bmp, 1);   
                  try
                  {
                      IntPtr h = bmp.GetHbitmap();
                      Bitmap mask = CoreBitmapOperation.InvertColor(bmp);
                      Graphics g = Graphics.FromImage(mask);
                      g.Clear(Color.Transparent);
                      g.Flush();
                      g.Dispose();
                      mask.MakeTransparent();
                      IntPtr vd = mask.GetHbitmap();
                      IconInfo info = new IconInfo();
                      info.hbmColor = h;
                      byte[] t = CoreBitmapOperation.GetMaskData(bmp, 1);
                      IntPtr v = Marshal.AllocCoTaskMem(t.Length);
                      Marshal.Copy(t, 0, v, t.Length);
                      info.hbmMask = vd;
                      Marshal.FreeCoTaskMem(v);
                      info.isIcon = 0;
                      info.hotspotx = 16;
                      info.hotspoty = 16;
                      IntPtr cursor = CreateIconIndirect(ref info);
                      DeleteObject(h);
                      DeleteObject(vd);
                      bmp.Dispose();
                      XCursor dd = XCursor.CreateFromHWND(cursor);
                      //Cursor c = new System.Windows.Forms.Cursor(cursor);
                      if (dd != null)
                          m_rCursor.Add(key, dd);
                      return dd;
                  }
                  catch { 
                  }
              }
              return XCursor.Default;
          }*/
        public ICore2DDrawingDocument GetDocument(string key, int index)
        {
            if (string.IsNullOrEmpty(key)) return null;
            key = getDocumentKeys(key);
            
            if (m_documents.ContainsKey(key))
            {
                if ((m_documents[key].Length > 0) && (m_documents[key].Length > index))
                    return m_documents[key][index];
            }
            else if (this.m_Object.ContainsKey(key) && (this.m_Object[key] is byte[]))
            {
                try
                {
                    ICore2DDrawingDocument[] v_docs = this.LoadDocumentObject(key, this.m_Object[key] as byte[]);
                    if ((v_docs != null) && v_docs.IndexExists(index))
                        return v_docs[index];
                }
                catch
                {
                }
            }
            return null;
        }
        #endregion
        void InitLoadAssemblyResources(Assembly asm)
        {
            if ((asm != null) && CoreAddInAttribute.IsAddIn(asm) && !this.m_asm.Contains(asm))
            {
                this.m_asm.Add(asm);
            }
        }
        /// <summary>
        /// load objet resource
        /// </summary>
        /// <returns>true if object ressources found</returns>
        private bool LoadObjectResources()
        {
            IGK.ICore.Settings.CoreApplicationSetting setting = IGK.ICore.Settings.CoreApplicationSetting.Instance;
            string v_file = string.Format(CoreConstant.OBJECTRESOURCES, setting.StartUpFolder);
            if (File.Exists(v_file))
            {
                ResourceReader v_rsR = null;
                try
                {
                    v_rsR = new ResourceReader(v_file);
                    IDictionaryEnumerator e = v_rsR.GetEnumerator();
                    string key = null;
                    while (e.MoveNext())
                    {
                        key = e.Key.ToString().ToLower();
                        RegisterObject(key, e.Value);                       
                    }
                }
                finally
                {
                    if (v_rsR != null)
                        v_rsR.Close();
                }
                this.m_objLoaded = true;
                return true;
            }
            return false;
        }
        private void SaveDocumentAndObjectResources()
        {
            IGK.ICore.Settings.CoreApplicationSetting setting = IGK.ICore.Settings.CoreApplicationSetting.Instance;
            string v_lang = setting.Lang;
            string v_file = Path.GetFullPath(IO.PathUtils.GetStartupFullPath(CoreConstant.OBJECTRESOURCES));
            ResourceWriter v_rsw = null;
            try
            {
                v_rsw = new ResourceWriter(v_file);
                SortObject(this.m_Object);
                foreach (KeyValuePair<string, object> item in this.m_Object)
                {
                    v_rsw.AddResource(item.Key, item.Value);
                }
            }
            catch (Exception ex)
            {
                CoreMessageBox.Show(ex.Message);
            }
            finally
            {
                if (v_rsw != null)
                {
                    v_rsw.Generate();
                    v_rsw.Close();
                }
            }
        }
        /// <summary>
        /// sort dictionary object
        /// </summary>
        /// <param name="dictionary"></param>
        private void SortObject(Dictionary<string, object> dictionary)
        {
            List<string> h = new List<string>();
            Dictionary<string, object> bdictionary = new Dictionary<string, object>();
            foreach (string s in dictionary.Keys)
            {
                h.Add(s);
                bdictionary.Add(s, dictionary[s]);
            }
            h.Sort();
            dictionary.Clear();
            foreach (var item in h)
            {
                dictionary.Add(item, bdictionary[item]);
            }
            bdictionary.Clear();
        }


        /// <summary>
        /// load language resource from file according to selected language.
        /// </summary>
        /// <returns>return false if not loaded</returns>
        bool LoadLangResources()
        {
            IGK.ICore.Settings.CoreApplicationSetting setting = IGK.ICore.Settings.CoreApplicationSetting.Instance;
            string v_file = IO.PathUtils.GetPath(string.Format(CoreConstant.LANGRESOURCES, setting.LangFolder, setting.Lang));
            if (File.Exists(v_file))
            {
                ResourceReader v_rsR = null;
                try
                {
                    v_rsR = new ResourceReader(v_file);
                    this.m_strings.Clear();
                    this.LoadResFrom(Path.GetFileName(v_file), "/resources", v_rsR);
                }
                finally
                {
                    if (v_rsR != null)
                        v_rsR.Close();
                }
                this.m_stringLoadedFromFile = true;
                return true;
            }
            return false;
        }
        /// <summary>
        /// save language resources to
        /// </summary>
        /// <param name="file"></param>
        /// <param name="v2"></param>
        public static void SaveLangResources(string file)
        {
            ResourceWriter v_rsw = null;
            var s = CoreSystem.GetResources()?.m_strings;
            try
            {
                v_rsw = new ResourceWriter(file);
                foreach (KeyValuePair<string, string> item in s)
                {
                    v_rsw.AddResource(item.Key, item.Value);
                }
            }
            catch (Exception ex)
            {
                CoreMessageBox.Show(ex);
            }
            finally
            {
                if (v_rsw != null)
                {
                    v_rsw.Generate();
                    v_rsw.Close();
                }
            }
        }
        /// <summary>
        /// save language resources
        /// </summary>
        private void SaveLangResources()
        {
            
            CoreApplicationSetting setting = CoreApplicationSetting.Instance;
            string v_lang = setting.Lang;
            string v_file = string.Format(CoreConstant.LANGRESOURCES, PathUtils.GetPath(setting.LangFolder), v_lang);
            if (PathUtils.CreateDir(PathUtils.GetDirectoryName(v_file)) == false)
            {
                return;
            }

            SaveLangResources(v_file);

        }
        void LoadAssemblyResources(Assembly asm)
        {
            if (asm == null) return;

            string v_asn = asm.GetName().Name;
            //if (v_asn.ToLower() == "igk.icore.wincore") {
            //}
            CoreLog.WriteLine("[IGK] - Load Assembly Resources... " + v_asn);
            Stream v_stream = null;
            string[] t = asm.GetManifestResourceNames();
            ResourceReader v_reader = null;
            string v_name = null;
            for (int i = 0; i < t.Length; i++)
            {
                v_name = t[i];
                //CoreLog.WriteLine("debugging ::::: " + t[i].ToLower());
                if (!t[i].ToLower().EndsWith(".resources"))
                {
                    Byte[] g = CoreFileUtils.ReadAllBytes(asm.GetManifestResourceStream(v_name));
                    RegisterObject((v_asn + ":/" + t[i]).ToLower(), g);
                    continue;
                }
                //if (!t[i].ToLower().EndsWith(".resources"))
                //    continue;
                //v_name = t[i];
                v_stream = asm.GetManifestResourceStream(v_name);
                if ((v_stream == null) || (v_stream.Length == 0))
                    continue;
                v_reader = null;
                try
                {
                    try
                    {
                        v_reader = new ResourceReader(v_stream);
                    }
                    catch
                    {
                        continue;
                    }
                    LoadResFrom(v_asn, v_name, v_reader);
                }
                catch (Exception ex)
                {
                    CoreLog.WriteDebug("Error CoreResources  :" + ex.Message + ": " + v_name);
                }
                finally
                {
                    if (v_reader != null)
                        v_reader.Close();
                    if (v_stream != null)
                        v_stream.Close();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">debug name</param>
        /// <param name="v_reader"></param>
        private void LoadResFrom(string assemblyName, string resName,  ResourceReader v_reader)
        {
            string v_name = assemblyName + "::" + resName;
//#if DEBUG
//            CoreLog.WriteDebug("Loading Resources : "+v_name);
//#endif
            IDictionaryEnumerator e = v_reader.GetEnumerator();
            bool v_c = (v_name.ToLower() == "app::storeresource");
            if (v_c)
            {
                m_strings.Clear();
            }

            string key = string.Empty;
            while (e.MoveNext())
            {
                if (e.Key == null)
                    continue;
            
                if (e.Value is String)
                {
                    key = e.Key.ToString().ToLower();
                    if (System.Text.RegularExpressions.Regex.IsMatch(key, @"\[[a-z\.]+\]", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    {
                        continue;
                    }
                    if (key.StartsWith("text_"))
                    {//ignore text
                        continue;
                    }

                    if (this.m_stringLoadedFromFile)
                    {//consider only element new added element created
                        if (!m_strings.ContainsKey(key)){

                            m_strings.Add(key, e.Value as String);
                        }
                        continue;
                    }
                    else
                    {
                        if (!m_strings.ContainsKey(key))
                            m_strings.Add(key, e.Value as String);
                        else
                            m_strings[key] = e.Value as string;
                    }
                    continue;
                }

                key = string.Format ("{0}:/{1}", assemblyName ,e.Key).ToLower();
                if ((e.Value is byte[]) && !this.NotIsADocument(key))
                {
                    byte[] data = e.Value as Byte[];
                    RegisterObject(key, data);
                    LoadDocumentObject(key, data);
                }
                else{
                    RegisterObject(key, e.Value);
                }
            }
            if (v_c)
            {
                //raise the language changed event
                if (this.m_core.MainForm != null)
                {
                    CoreApplicationSetting.Instance.OnLangReloaded();
                    this.m_core.MainForm.Invalidate(true);
                }
            }
        }
        
        public  bool NotIsADocument(string key)
        {
            var rs = CoreApplicationManager.Application.ResourcesManager;
            return rs == null? false : rs.IsNotAGkdsDocument(key); //this.m_resNotADocuments.Contains(key);
        }
       
        /// <summary>
        /// get the document at index. not registrated saved
        /// </summary>
        /// <param name="resbyte">resource byte data</param>
        /// <param name="index">index data</param>
        /// <returns></returns>
        public static ICore2DDrawingDocument GetDocument(byte[] resbyte, int index)
        {
            if (resbyte == null) return null;
            ICoreWorkingDocument[] v_tab = Codec.CoreDecoder.Instance.GetDocuments(resbyte);
            if ((v_tab != null) && (v_tab.Length >= 0) && (index < v_tab.Length))
            {
                return v_tab[index] as ICore2DDrawingDocument;
            }
            return null;
        }
        /// <summary>
        /// load a resource from file name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public T LoadResources<T>(string filename) where T :class {
#if !DEBUG
            if (this.m_documents.ContainsKey(filename)){
                return this.m_documents[filename] as T;
            }
#endif

            if (Path.GetExtension(filename).ToLower() == ".gkds") {

                ICoreWorkingDocument[] v_tab = Codec.CoreDecoder.Instance.GetDocuments(
                    File.ReadAllBytes (filename));
                if (v_tab?.Length > 0)
                {
                    if (this.m_documents.ContainsKey(filename))
                        this.m_documents[filename] = v_tab.CoreConvertTo<ICore2DDrawingDocument[]>();
                    else 
                        this.m_documents.Add(filename, v_tab.CoreConvertTo<ICore2DDrawingDocument[]>());
                    return v_tab[0] as T;
                }

            }
            return default(T);

        }
        /// <summary>
        /// get or documcne tin the specifib byte document
        /// </summary>
        /// <param name="p">data</param>
        /// <returns></returns>
        public static ICore2DDrawingDocument[] GetAllDocuments(byte[] resbyte)
        {
            if (resbyte == null)
                return null;
            List<IGK.ICore.Drawing2D.ICore2DDrawingDocument> doc = new List<ICore2DDrawingDocument>();
            ICoreWorkingDocument[] v_tab = Codec.CoreDecoder.Instance.GetDocuments(resbyte);
            if (v_tab != null)
            {
                for (int i = 0; i < v_tab.Length; i++)
                {
                    if (v_tab[i] is ICore2DDrawingDocument)
                    {
                        doc.Add(v_tab[i] as ICore2DDrawingDocument);
                    }
                }
            }
            return doc.ToArray();
        }
        /// <summary>
        /// get all documents in the specified byte document.
        /// </summary>
        /// <param name="p">data</param>
        /// <returns></returns>
        public static T[] GetAllDocuments<T>(byte[] resbyte) where T: ICoreWorkingDocument 
        {
            if (resbyte == null)
                return null;
            List<T> doc = new List<T>();
            ICoreWorkingDocument[] v_tab = CoreDecoder.Instance.GetDocuments(resbyte);
            if (v_tab != null)
            {
                for (int i = 0; i < v_tab.Length; i++)
                {
                    if (v_tab[i] is T)
                    {
                        doc.Add((T)v_tab[i]);
                    }
                }
            }
            return doc.ToArray();
        }
        private ICore2DDrawingDocument[] LoadDocumentObject(string key, byte[] p)
        {

#if DEBUG
            CoreLog.WriteDebug(string.Format("Try to load document [{0}] ", key));
#endif
            //if (key.ToLower() == "btn_close") 
            //{ 
            //    //
            //}
            if (key.ToLower().EndsWith(CoreConstant.RS_ENDEXTENSION) && !m_documents.ContainsKey(key))
            {
                try
                {
                    ICore2DDrawingDocument[] v_docs = GetAllDocuments(p);
                    if ((v_docs != null) && (v_docs.Length > 0))
                        m_documents.Add(key, v_docs);
#if DEBUG
                    else
                    {
                        CoreLog.WriteLine(key + " not a gkds resources file");
                    }
#endif
                    return v_docs;
                }
                catch (Exception ex)
                {
                    CoreLog.WriteLine("Exception Raised : " + ex.Message + " on GetAllDocument from [ " + key + " ]");
                }
            }
#if DEBUG
            else
                CoreLog.WriteDebug(string.Format("Document already registered or key not valid: {0}", key));
#endif
            return null;
        }

        /// <summary>
        /// get primary resources assembly
        /// </summary>
        public Assembly ResAssembly {
            get {
                
                if (m_defres == null)
                    m_defres = CoreSystem.GetAddIns().GetAssembly(CoreConstant.RES_LIB);
                return m_defres;
            }
        }
#region ICoreResources Members
        public ICore2DDrawingDocument[] GetAllDocument(string p)
        {
            if (string.IsNullOrEmpty(p))
                return null;
            p = getDocumentKeys(p);
         
            if (this.m_documents.ContainsKey(p))
            {
                return this.m_documents[p];
            }
            return null;
        }

        private string getDocumentKeys(string p)
        {
            p = p.ToLower();
            if (!p.Contains(":"))
            {
                //default 
                var def = ResAssembly;
                if (def != null)
                {
                    p = string.Format("{0}:/{1}", def.GetName().Name, p).ToLower();
                }
            }
            return p;
        }
#endregion
#region ICoreResources Members
        /// <summary>
        /// get image form keys
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ICoreBitmap GetImage(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;
            //be sure of the key
            key = key.ToLower();
            if (this.m_pictures.ContainsKey(key))
                return m_pictures[key];
            var pk = getDocumentKeys(key);
            if (this.m_Object.ContainsKey(pk))
            {
                object obj = this.m_Object[pk];
                ICoreBitmap c = CoreApplicationManager.Application.ResourcesManager.CreateBitmap(obj);
                if (c != null)
                {
                    this.m_pictures.Add(key, c);
                    return c;
                }
            }
            return null;
        }
        public object GetImage(string name, int width, int height)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            //be sure of the key
            name = name.ToLower();
            if (m_pictures.ContainsKey(name))
            {
                ICoreBitmap inBmp = m_pictures[name];
                ICoreBitmap bmp = CoreApplicationManager.Application.ResourcesManager.CreateBitmap(width, height);
                ICoreGraphics g = CoreApplicationManager.Application.ResourcesManager.CreateDevice(bmp);
                g.Draw(inBmp, new Rectanglei(0, 0, width, height));
                g.Dispose();
                return bmp;
            }
            else
            {
                ICore2DDrawingDocument document = GetDocument(name, 0);
                if (document != null)
                {
                    ICoreBitmap bmp = CoreApplicationManager.Application.ResourcesManager.CreateBitmap(width, height);
                    using (ICoreGraphics g = CoreApplicationManager.Application.ResourcesManager.CreateDevice(bmp))
                    {
                        if (g != null)
                        {
                            document.Draw(g, new Rectanglei(0, 0, width, height));
                        }
                    }
                    
                    return bmp;
                }
            }
            return null;
        }
#endregion
#region IDisposable Members
        public void Dispose()
        {
            //dispose all used resources
            foreach (KeyValuePair<string, ICoreBitmap> item in this.m_pictures)
            {
                item.Value.Dispose();
            }
            this.m_pictures.Clear();
            foreach (KeyValuePair<string, XCursor> item in this.m_rCursors)
            {
                item.Value.Dispose();
            }
            this.m_rCursors.Clear();
            foreach (KeyValuePair<string, ICore2DDrawingDocument[]> item in this.m_documents)
            {
                Dispose(item.Value);
            }
            this.m_documents.Clear();
        }
        private void Dispose(params ICore2DDrawingDocument[] documents)
        {
            if (documents == null)
                return;

            for (int i = 0; i < documents.Length; i++)
            {
                documents[i].Dispose();
            }
        }
#endregion
        public void ReloadString()
        {
            this.m_stringLoadedFromFile = false;
            this.m_strings.Clear();
            if (
            this.LoadLangResources())
            {
                if (this.m_core.MainForm != null)
                {
                    this.m_core.MainForm.Invalidate(true);
                }
            }
            else {
                //load entry resources
            }
        }
        public static void Register(string resourcename, Stream stream)
        {
            throw new NotImplementedException();

            //if (string.IsNullOrEmpty (resourcename))
            //    throw new CoreException (enuExceptionType.ArgumentIsNull , "stream");
            //Bitmap bmp = null;
            //CoreResourcesCollections rs = CoreSystem.Instance.Resources as CoreResourcesCollections;
            //try
            //{
            //    bmp = new Bitmap(stream);
            //    rs.m_pictures.Add(resourcename.ToLower (), bmp);
            //}
            //catch { 
            //}
        }


        /// <summary>
        /// get string expression 
        /// </summary>
        /// <returns></returns>
        public static System.Collections.Generic.Dictionary<string, string>.Enumerator GetStrings()
        {
            return (CoreSystem.Instance.Resources as CoreResourcesManager).m_strings.GetEnumerator();
        }

        public static void StoreObjectCollection(ICoreResourceObjectListener listener) {
            if (listener == null)
                return ;
            var g = (CoreSystem.Instance.Resources as CoreResourcesManager);

            foreach (var item in g.m_Object)
            {
                listener.Store(item.Key, item.Value );
            }

        }

        /// <summary>
        /// load resources reader
        /// </summary>
        /// <param name="rsReader"></param>
        public static void StoreStringResources(ResourceReader rsReader)
        {
            if (rsReader != null)
            {
                var g = (CoreSystem.Instance.Resources as CoreResourcesManager);
                g.LoadResFrom("App", "StoreResource", rsReader);
                g.SaveResources();
            }
        }
        public byte[] GetDefinition(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;
            if (this.m_Object.ContainsKey(key.ToLower()))
            {
                return (byte[])m_Object[key.ToLower()];
            }
            return null;
        }
        public T GetCursor<T>(string key) where T : class
        {
            if (m_rCursors.ContainsKey(key))
                return m_rCursors["Key"] as T;
            else
            {
                XCursor cursor = CoreApplicationManager.Application.ResourcesManager.CreateCursor(key);
                if (cursor != null)
                {
                    this.m_rCursors.Add(key, cursor);
                    return cursor as T;
                }
            }
            return null;
        }
        public T GetDocument<T>(string key, int index)
        {
            if (this.m_documents.ContainsKey(key.ToLower()))
            {
                return (T)this.m_documents[key.ToLower()][index];
            }
            return default(T);
        }
        public T[] GetAllDocument<T>(string key)
        {
            if (this.m_documents.ContainsKey(key.ToLower()))
            {
                return this.m_documents[key.ToLower()] as T[];
            }
            return default(T[]);
        }
        public object GetObject(string name)
        {
            name = name.ToLower();
            if (this.m_Object.ContainsKey(name))
            {
                return this.m_Object[name];               
            }
            return null;
        }
        internal void RegisterObject(string name, object obj)
        {
            name = name.ToLower();
            if (!this.m_Object.ContainsKey(name) && (obj !=null))
            {
                this.m_Object.Add (name, obj );
            }
        }
       
        /// <summary>
        /// get the global resources as bytes
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public byte[] GetResource(string name)
        {
            name = name.ToLower();
            if (this.m_Object.ContainsKey(name))
            {
                var b = this.m_Object[name];
                if (b is Byte[])
                {
                    return b as Byte[];
                }
                else if (b is UnmanagedMemoryStream)
                {
                    var mem = b as UnmanagedMemoryStream;
                    if ((mem.CanSeek) && (mem.Position > 0))
                        mem.Seek(0, SeekOrigin.Begin);
                    Byte[] t = new  Byte[mem.Length];
                    mem.Read(t, 0, t.Length);
                    return t;
                }
            }
            return null;
        }

        public static void LoadString(string name, string value)
        {
            var g = CoreSystem.GetResources().m_strings;
            g.Add(name, value);
        }
    }
}

