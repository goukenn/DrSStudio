using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.DB
{
    public interface ICoreDataPropertyInfo
    {
        Type PropertyType { get; }
        CoreDataGuiAttribute GetGuiAttribute();
        CoreDataTableDisplayInfoAttribute GetDisplayInfoAttribute();
    }
}
