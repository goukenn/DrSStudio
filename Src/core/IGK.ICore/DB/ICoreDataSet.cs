

namespace IGK.ICore.DB
{
    public interface ICoreDataSet : ICoreDataQueryResult
    {
        void AddColumn(string name);
        void AddRow(ICoreDataRow row);
        ICoreDataRow CreateRow();
    }
}
