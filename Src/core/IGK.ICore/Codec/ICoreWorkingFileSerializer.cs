using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Codec
{
    public interface  ICoreWorkingFileSerializer
    {
        ICoreWorkingDocument[] Documents { get; set;}
        ICoreWorkingFileEntity[] Entities { get; }
        void Add(ICoreWorkingFileEntity entity);
        void Remove(ICoreWorkingFileEntity entity);
    }
}
