using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.DataTable
{
    public interface ITbGSHuman : IGSDataTable 
    {
        string clFirstName { get; set; }
        string clLastName { get; set; }
    }
}
