namespace IGK.ICore.Xml.XSD
{
    public interface ICoreXSDFileImportResolver
    {
        bool IsResolved(string @namespace);
        void Resolv(ICoreXSDLoaderListener listener, string @namespace, string location);
    }
}