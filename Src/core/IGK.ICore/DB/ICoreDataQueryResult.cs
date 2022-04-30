namespace IGK.ICore.DB
{
    /// <summary>
    /// represent a core query result
    /// </summary>
    public  interface ICoreDataQueryResult
    {
        bool Error { get; }
        string ErrorMessage { get; }
        /// <summary>
        /// get the rows collection
        /// </summary>
        ICoreDataRowCollections Rows { get; }
        /// <summary>
        /// get the data column collection
        /// </summary>
        ICoreDataColumnCollections Columns { get; }
        /// <summary>
        /// get the number of row in this query result
        /// </summary>
        int RowCount { get; }
        /// <summary>
        /// get the number of the column
        /// </summary>
        int FieldCount { get; }
        /// <summary>
        /// get the query affected row
        /// </summary>
        int AffectedRow { get; }
        /// <summary>
        /// get the row at the index
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        ICoreDataRow GetRowAt(int p);

        void SortBy(string p);
        /// <summary>
        /// create the row
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <returns></returns>
        //T GetRowAt<T>(int v);
    }
}