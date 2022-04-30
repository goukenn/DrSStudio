namespace IGK.ICore.Xml.XSD
{
    public interface ICoreXSDManagerListener
    {
        ICoreXSDType GetItem(string fname);
        CoreXSDItem CreateItem(string typeName);
    }
}