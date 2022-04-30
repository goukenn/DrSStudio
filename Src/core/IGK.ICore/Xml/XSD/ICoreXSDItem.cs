namespace IGK.ICore.Xml.XSD
{
    /// <summary>
    /// represent a xsd item
    /// </summary>
    public interface ICoreXSDItem
    {
        /// <summary>
        /// get or set the value attached to this xsd item
        /// </summary>
       object Value { get;set;}
        /// <summary>
        /// get the type of this item
        /// </summary>
        ICoreXSDType Type { get;}
        /// <summary>
        /// get the source type of this item
        /// </summary>
        ICoreXSDSourceType SourceType { get;}
    }
}