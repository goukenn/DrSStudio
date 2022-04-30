using System.Collections.Generic;

namespace IGK.ICore.DB
{
    internal class CoreDataDummyIdentifier : ICoreDataTableCLIDIdentifier
    {
        public int clId { get; set; }
        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>() { { CoreDataConstant.CL_ID, this.clId } };
        }
        public ICoreDataTableColumnIdentifier Add(ICoreDataTableColumnIdentifier iGSTableColumnIdentifier)
        {
            return this;
        }
    }
}