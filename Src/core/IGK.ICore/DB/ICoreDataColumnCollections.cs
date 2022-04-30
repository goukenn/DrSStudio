using System.Collections;

namespace IGK.ICore.DB
{
    public interface ICoreDataColumnCollections : IEnumerable
    {
        int FieldCount { get; }
        /// <summary>
        /// get the name of the columns at index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        string this[int index] { get; }
    }
}