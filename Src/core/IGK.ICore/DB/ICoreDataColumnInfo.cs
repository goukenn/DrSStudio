namespace IGK.ICore.DB
{
    /// <summary>
    /// represent a column info
    /// </summary>
    public interface ICoreDataColumnInfo
    {
        string clName { get; set; }
        bool clNotNull { get; set; }
        string clType { get; set; }
        int clTypeLength { get; set; }
        string clDefault { get; set; }
        bool clIsUniqueColumnMember { get; set; }
        int clColumnMemberIndex { get; set; }
        bool clAutoIncrement { get; set; }
        bool clIsPrimary { get; set; }
        bool clIsUnique { get; set; }
        bool clIsIndex { get; set; }
        string clDescription { get; set; }
        string clLinkType { get; }//used if the type of this column is a datadefinition type
        string clInsertFunction { get; set; }
        string clUpdateFunction { get; set; }
        string clInputType { get; set; }
    }
}