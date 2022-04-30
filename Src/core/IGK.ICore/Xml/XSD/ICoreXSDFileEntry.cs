namespace IGK.ICore.Xml.XSD
{
    public interface ICoreXSDFileEntry
    {
        /// <summary>
        /// get the source type
        /// </summary>
        ICoreXSDObjectType Item { get; }
        /// <summary>
        /// get childs items
        /// </summary>
        CoreXSDItem[] ValueItems { get;}
        /// <summary>
        /// add sub item to entries
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        ICoreXSDFileEntry AddData(CoreXSDItem item);
        /// <summary>
        /// used to store  data to xml element
        /// </summary>
        /// <param name="e"></param>
        void SaveData(CoreXmlElement e);
    }
}