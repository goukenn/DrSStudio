using System;
using IGK.ICore.Xml;
using IGK.ICore.Xml.XSD;
using IGK.ICore;
using System.Collections.Generic;

namespace IGK.DrSStudio.SVGEngine
{
    internal class SVGFileResolver : ICoreXSDFileImportResolver
    {
        private List<string> m_listResolved;

        public SVGFileResolver()
        {
            m_listResolved = new List<string> ();
        }

        public void Resolv(ICoreXSDLoaderListener listener, string @namespace, string location)
        {
            if (this.IsResolved(@namespace))
                return ;


            string prefix = listener.GetPrefix(@namespace);
            if (Uri.IsWellFormedUriString(location, UriKind.Absolute)) {
                location = location.Split ('/').Last();
            }

            string bck = listener.CurrentPrefix;
            switch (location)
            {
                case "xlink.xsd":
                    //backup current prefix
                    listener.CurrentPrefix = prefix;
                    CoreXSDLoader.LoadXSDResource(this.GetType().Assembly,
                        "xlink", listener);
                    //restore current prefix
                    break;
                case "xml.xsd":
                    listener.CurrentPrefix = prefix;
                    CoreXSDLoader.LoadXSDResource(this.GetType().Assembly,
                        "xml", listener);
                    break;
                default:
                    break;
            }

            listener.CurrentPrefix = bck;
            this.m_listResolved.Add (@namespace);
        }

        public bool IsResolved(string @namespace)
        {
           return  (this.m_listResolved.Contains(@namespace));
        }
    }
}