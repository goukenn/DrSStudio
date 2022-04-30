using System.Collections.Generic;

namespace IGK.ICore.DB
{
    public interface ICoreDataTableColumnIdentifier
    {
        Dictionary<string, object> ToDictionary();

        ICoreDataTableColumnIdentifier Add(ICoreDataTableColumnIdentifier iGSTableColumnIdentifier);

    }
}