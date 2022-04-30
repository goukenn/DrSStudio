using IGK.ICore.Xml;
using IGK.ICore.Xml.XSD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.SVGEngine
{
    /// <summary>
    /// atomic file manager
    /// </summary>
    class SVGFileManager : 
        ICoreXSDFileManager,
        ICoreXSDManagerListener
    {
        private static SVGFileManager sm_instance;
        private CoreXSDFileLoaderListener listener;

        private SVGFileManager()
        {
        }

        public static SVGFileManager Instance
        {
            get
            {
                return sm_instance;
            }
        }

        public  bool ContainsType(string typeName)
        {
            return false ;
        }

        static SVGFileManager()
        {
            sm_instance = new SVGFileManager();
            sm_instance.listener = new CoreXSDFileLoaderListener();
            sm_instance.listener.SetResolvImportListener(new SVGFileResolver());
            CoreXSDLoader.LoadXSDResource(sm_instance.GetType().Assembly,
                "SVG",
                sm_instance.listener
                );
        }

        internal object CreateItem(ICoreXSDType type)
        {
            return CoreXSDItem.Create(sm_instance.listener, type );
        }

        public ICoreXSDType GetItem(string node)
        {
            return this.listener.GetItem(node);
        }

        public CoreXSDItem CreateItem(string name)
        {
            if (name.StartsWith("svg:"))
                name =name.Split(':').Last();
            return this.listener.CreateItem(name); 
        }

        public ICoreXSDType GetItemType(string typeName)
        {
            if (typeName.StartsWith("svg:"))
                typeName = typeName.Split(':').Last();
            return this.listener.GetItemType (typeName);
        }
    }
}
