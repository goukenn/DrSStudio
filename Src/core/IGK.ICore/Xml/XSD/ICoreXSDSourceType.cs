namespace IGK.ICore.Xml.XSD
{
    public interface ICoreXSDSourceType
    {
        /// <summary>
        /// create a new section item type from ICoreXSDType
        /// </summary>
        /// <param name="coreXSDType"></param>
        /// <returns></returns>
        object CreateItem(ICoreXSDType coreXSDType);
        /// <summary>
        /// determine if this item contains type name
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        bool ContainsType(string typeName);
        /// <summary>
        /// get type info for declaring type
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        ICoreXSDDeclaringType GetTypeInfo(string typeName);
        /// <summary>
        /// get item type
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        ICoreXSDType GetItemType(string typeName);
        /// <summary>
        /// treat the type name
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        string GetTypeName(string typeName);
    }
}