

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreResources.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
CoreDataConstant.CL_IDor: C.A.D . BONDJE DOUE
file:CoreResources.cs
*/
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Resources
{
    public static class CoreResources
    {
        static Dictionary<string, ICore2DDrawingDocument> sm_documents;
        private static Dictionary<string, ICoreBitmap> cm_bmp;

        public static ICoreBitmap GetBitmapResourcesFromFile(string filename)
        {
            return CoreApplicationManager.Application.ResourcesManager.CreateBitmapFromFile(filename);
        }
        public static ICoreBitmap GetBitmapResourcesFromFileStream(Stream stream)
        {
            return CoreApplicationManager.Application.ResourcesManager.CreateBitmap(stream);
        }
      
        public static ICoreBitmap GetBitmapResources(string name)
        {
            name = name.ToLower();
            if (cm_bmp.ContainsKey(name))
                return cm_bmp[name];

            ICoreBitmap bmp = CoreSystem.Instance.Resources.GetImage(name);
            if (bmp != null)
            {
                cm_bmp.Add(name, bmp);
            }
            return bmp;
        }
        /// <summary>
        /// remove the bitmap resource register by name
        /// </summary>
        /// <param name="name"></param>
        public static void RemoveBitmapResources(string name)
        {
            name = name.ToLower();
            if (cm_bmp.ContainsKey(name))
            {
                cm_bmp[name].Dispose();
                cm_bmp.Remove(name);
            }
        }
        static CoreResources()
        {
            CoreLog.WriteLine("/!\\ Init CoreResources ... ");
            sm_documents = new Dictionary<string, ICore2DDrawingDocument>();
            cm_bmp = new Dictionary<string, ICoreBitmap>();
        }
        public static string GetString(string key, params object[] objects)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;
            if (CoreSystem.Instance == null)
                return key;
            return CoreSystem.GetString(key, objects);//.Instance.Resources.GetString(key, objects);
        }
        public static object GetCursor(string name)
        {
            return CoreSystem.Instance.Resources.GetCursor<object>(name);
        }
        public static string GetErrorString(int code)
        {
            return GetString(string.Format("ERR{0}", code));
        }
        /// <summary>
        /// get all documents presented in the name collections
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ICore2DDrawingDocument[] GetAllDocuments(string name)
        {            


            var c = CoreSystem.Instance;

            if ((c != null) && (CoreSystem.IsCoreTransparantProxy()))
            {
                var b =  c.Resources.GetAllDocument(name);
                string v =string.Format("<gkds><Documents>"+ b[0].Render()+"</Documents></gkds>");
                var o =  CoreResourcesManager.GetAllDocuments<ICore2DDrawingDocument>(UTF8Encoding.UTF8.GetBytes(v.ToCharArray()));

                return o;
            }

            if ((c == null) || (c.Resources == null))
                return null;
            return c.Resources.GetAllDocument(name);
        }
        /// <summary>
        /// get document from resource object
        /// </summary>
        /// <param name="obj">objecdt attached to the assembly where to locate resource</param>
        /// <param name="name">name of the resources</param>
        /// <returns></returns>
        public static ICore2DDrawingDocument GetDocument(object obj, string name)
        {
            name = GetResourceId(obj, name);   
            return GetDocument(name);
        }
        /// <summary>
        /// get a single doucment
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ICore2DDrawingDocument GetDocument(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            if (CoreSystem.IsCoreTransparantProxy())
            {
                if (sm_documents.ContainsKey(name.ToLower()))
                    return sm_documents[name.ToLower()];
                byte[] v_tab = CoreSystem.Instance.Resources.GetResource(name);
                ICore2DDrawingDocument[] t = CoreResourcesManager.GetAllDocuments(v_tab);
                if ((t != null) && (t.Length > 0))
                {
                    sm_documents.Add(name.ToLower(), t[0]);
                    return t[0];
                }
                return null;
            }
            return CoreSystem.Instance.Resources.GetDocument(name, 0);
        }
        public static void Register(string name, object obj)
        {
            if (CoreSystem.Instance == null)
                return ;
            CoreSystem.Instance.Resources.RegisterObject(name, obj);
        }
        public static object GetObject(string name)
        {
            if (CoreSystem.Instance == null)
                return null;
            return CoreSystem.Instance.Resources.GetObject(name);
        }
        public static string[] GetResourceKeys()
        {
            if (CoreSystem.Instance == null)
                return null;
            return CoreSystem.Instance.Resources.GetKeys();
        }

        //public static byte[] GetData(string name)
        //{
        //    return CoreSystem.Instance.Resources.GetObject(name);
        //}
        public static byte[] GetResource(string name)
        {
            var t = CoreSystem.Instance;
            if (t != null)
                return t.__GetIResources().GetResource(name);
            return null;
        }

        /// <summary>
        /// used to create a resources document 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static T LoadDocument<T>(string filename) where T:class 
        {
            if (string.IsNullOrEmpty(filename) || !File.Exists(filename))
                throw new ArgumentException(nameof(filename));

            var t = CoreSystem.Instance;
            if (t != null)
                return t.__GetIResources().LoadResources<T>(filename);
            return default(T);
        }
        /// <summary>
        /// get resources in assembly
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static byte[] GetResource(Assembly obj, string name) {
            name = GetResourceId(obj, name);
            var t = CoreSystem.Instance;
            if (t != null)
                return t.__GetIResources().GetResource(name);
            return null;
        }
        public static byte[] GetResource(object obj , string name)
        {
            name = GetResourceId(obj, name);           
            var t = CoreSystem.Instance;
            if (t != null)
                return t.__GetIResources().GetResource(name);
            return null;
        }

        /// <summary>
        /// build key
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetResourceId(object obj, string name)
        {
            if (!name.Contains(":"))
            {
                if (obj is Assembly ) 
                    return GetResourceId (obj as Assembly, name );
                name = (obj.GetType().Assembly.GetName().Name + ":/" + name).ToLower();
            }
            return name;
        }
        public static string GetResourceId(Assembly asm, string name) {
            if (!name.Contains(":"))
            {
                name = (asm.GetName().Name + ":/" + name).ToLower();
            }
            return name;
        }
        /// <summary>
        /// get resource string from calling assembly
        /// </summary>
        /// <param name="name"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetResourceString(string name, Encoding encoding = null) {
            Assembly asm = Assembly.GetCallingAssembly();
            return GetResourceString(name, asm, encoding);
        }
        /// <summary>
        /// get the name of the resources identifier 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="assembly"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetResourceString(string name,  System.Reflection.Assembly assembly, Encoding encoding = null)
        {
            if (!name.Contains(":"))
            {
                var vsm = assembly ?? System.Reflection.Assembly.GetCallingAssembly();
                if (vsm != null)
                {
                    name = (vsm.GetName().Name + ":/" + name).ToLower();
                }
            }
            var t = GetResource(name);
            if (t != null)
                if (encoding != null)
                    return encoding.GetString(t);
                else
                    return Encoding.UTF8.GetString(t);
            return null;
        }
        //public static string GetResourceString(string name, Encoding encoding=null)
        //{
        //    //return GetResourceString (name , 
        //    //if (!name.Contains(":")) {
        //    //    var vsm = System.Reflection.Assembly.GetCallingAssembly();
        //    //    if (vsm != null) {
        //    //        name = vsm.GetName().Name  + ":/" + name;
        //    //    }
        //    //}
        //    //var t = GetResource (name );
        //    //if (t != null)
        //    //    if (encoding != null)
        //    //        return encoding.GetString(t);
        //    //    else
        //    //        return Encoding.UTF8.GetString(t);
        //    //return null;
        //}
        /// <summary>
        /// get resource string
        /// </summary>
        /// <param name="name"></param>
        /// <param name="resFolder"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetResourceString(string name, string resFolder, Assembly asm,  Encoding encoding = null) {
            if (string.IsNullOrEmpty(resFolder) || !Directory.Exists (resFolder))
                return GetResourceString(name, asm, encoding);
            string g = Path.Combine(resFolder, name);
            if (File.Exists(g))
                return File.ReadAllText(g);
            return null;
        }
        public static object GetImage(string name)
        {
            if (CoreSystem.Instance == null)
                return null;
            return CoreSystem.Instance.Resources.GetImage(name);
        }
        /// <summary>
        /// get the object image
        /// </summary>
        /// <param name="name">name of the requested image</param>
        /// <param name="width">requested width</param>
        /// <param name="height">requested height</param>
        /// <returns></returns>
        public static object GetImage(string name, int width, int height)
        {
            if ((CoreSystem.Instance == null) || (width <= 0) || (height <= 0))
                return null;
            return CoreSystem.Instance.Resources.GetImage(name, width, height);
        }


        ///// <summary>
        ///// get the requested width
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="width">request width</param>
        ///// <param name="height">requested height</param>
        ///// <returns></returns>
        //public static object GetImage(string name, int width, int height)
        //{
        //    if (CoreSystem.Instance == null)
        //        return null;
        //    return CoreSystem.Instance.Resources.GetImage(name);
        //}
        public static string GetString(object obj)
        {
            if (obj is enuKeys c)
            {
                StringBuilder sb = new StringBuilder();
                List<string> m_p = new List<string>();
                if ((c & enuKeys.Control) == enuKeys.Control)
                {
                    m_p.Add("Ctrl");
                    c -= enuKeys.Control;
                }
                if ((c & enuKeys.Alt) == enuKeys.Alt)
                {
                    m_p.Add("Alt");
                    c -= enuKeys.Alt;
                }
                if ((c & enuKeys.Shift) == enuKeys.Shift)
                {
                    m_p.Add("Shift");
                    c -= enuKeys.Shift;
                }
                if (c != enuKeys.None)
                {
                    int i = (int)c;
                    int s = (int)enuKeys.KeyCode;
                    if (i > s)
                    {
#pragma warning disable IDE0054 // Use compound assignment
                        i = i - s;
#pragma warning restore IDE0054 // Use compound assignment
                        m_p.Add(((char)(int)i).ToString());
                    }
                    else
                    {
                        m_p.Add(c.ToString());
                    }
                }
                sb.Append(string.Join("+", m_p.ToArray()));
                return sb.ToString();
            }
            if (obj != null)
                return obj.ToString();
            return string.Empty;
        }
        /// <summary>
        /// reaload string resources
        /// </summary>
        public static void ReloadResourcesString()
        {
            if (CoreSystem.Instance != null)
                CoreSystem.Instance.Resources.ReloadString();
        }
        /// <summary>
        /// get the shortcut string presentation
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string GetShortcutText(enuKeys keys)
        {
            StringBuilder sb = new StringBuilder();
            if ((keys & enuKeys.Control) == enuKeys.Control)
            {
                if (sb.Length > 0)
                    sb.Append("+");
                sb.Append("Ctrl");
                keys -= enuKeys.Control;
            }
            if ((keys & enuKeys.Alt) == enuKeys.Alt)
            {
                if (sb.Length > 0)
                    sb.Append("+");
                sb.Append("Alt");
                keys -= enuKeys.Alt;
            }
            if ((keys & enuKeys.Shift) == enuKeys.Shift)
            {
                if (sb.Length > 0)
                    sb.Append("+");
                sb.Append("Shift");
                keys -= enuKeys.Shift;
            }
            if (sb.Length > 0)
                sb.Append("+");
            if (keys != enuKeys.None)
                sb.Append(keys.ToString());
            return sb.ToString();
        }
        internal static void ReloadString()
        {
            CoreSystem.GetResources().ReloadString();
        }





        //-----------------------------------------------------------------------------------------------------------
        // Resources Utility
        //-----------------------------------------------------------------------------------------------------------
        public interface IResourceCallbackInfo
        {
            string Name { get; }
            Assembly Assembly { get; }
            string GetString();
        };
        public interface IResourceCallback
        {
            void Load(IResourceCallbackInfo info);

        }
        public delegate void IResourceCallbackHandler(IResourceCallbackInfo i);

        class RessourcecallBack : IResourceCallbackInfo
        {
            private string m_name;
            private Assembly m_asm;
            public string Name { get => m_name; internal set { m_name = value; } }
            public Assembly Assembly { get => m_asm; internal set { m_asm = value; } }
            public string GetString() => new StreamReader(Assembly.GetManifestResourceStream(Name)).ReadToEnd();

        }
        public static void LoadEmbededResources(Type t, IResourceCallbackHandler callback)
        {
            ResourceManager r = new ResourceManager(t);
            var v_g = new RessourcecallBack()
            {
                Assembly = t.Assembly
            };

            foreach (string g in t.Assembly.GetManifestResourceNames())
            {
                v_g.Name = g;
                callback?.Invoke(v_g);
            }
            r.ReleaseAllResources();
        }
        public static void LoadEmbededResources(Type t, IResourceCallback callback)
        {

            ResourceManager r = new ResourceManager(t);
            var v_g = new RessourcecallBack()
            {
                Assembly = t.Assembly
            };

            foreach (string g in t.Assembly.GetManifestResourceNames())
            {

                v_g.Name = g;
                callback.Load(v_g);
            }
            r.ReleaseAllResources();

        }


    }
}

