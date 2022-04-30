using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    /// <summary>
    /// every pdf item 
    /// </summary>
    public interface IPDFItem
    {
        string Render();
    }
}
