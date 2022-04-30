using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    [Flags()]
    public enum enuCoreDataField
    {
        None = 0x0,
        IsPrimaryKey = 0x01,
        IsNotNull = 0x02,
        Unique = 0x04,
        UniqueColumnMember = 0x08,
        AutoIncrement = 0x10
    }
}
