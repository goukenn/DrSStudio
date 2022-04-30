using System.Collections.Generic;
using System.Xml;

namespace IGK.ICore.Xml
{
    public interface ICoreXSDLoaderListener
    {
        string CurrentPrefix { get; set; }

        void LoadSimpleType(string n, XmlReader xmlReader);
        void LoadComplexType(string n, XmlReader xmlReader);
        void LoadGroup( XmlReader xmlReader);
        void LoadElement(string n, XmlReader xmlReader);
        void setAnnotation(string documentation, string appInfo);
        string GetPrefix(string @namespace);
        void ExitElement();
        void LoadSequence(XmlReader _xr);
        void LoadChoice(XmlReader _xr);
        void LoadRestriction(XmlReader _xr);
        void LoadAttribute(string v_n, XmlReader _xr);
        void LoadList(XmlReader _xr);
        void LoadComplexContent(XmlReader _xr);
        void LoadSimpleContent(XmlReader _xr);
        void LoadUnion(string[] tab, XmlReader _xr);
        void LoadAttributeGroup(string v_n, XmlReader _xr);
        void LoadSchemaInfo(Dictionary<string, string>.Enumerator enumerator);
        /// <summary>
        /// resolv schema location
        /// </summary>
        /// <param name="namespace"></param>
        /// <param name="location"></param>
        void ResolvImport(string @namespace, string location);
    }
}