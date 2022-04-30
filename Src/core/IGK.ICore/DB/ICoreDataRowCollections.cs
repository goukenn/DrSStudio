using System.Collections;

namespace IGK.ICore.DB
{
    public interface ICoreDataRowCollections : IEnumerable
    {
        /// <summary>
        /// get the row at index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        ICoreDataRow this[int index] { get; }
        /// <summary>
        /// get the number of row
        /// </summary>
        int Count { get; }
    }
}