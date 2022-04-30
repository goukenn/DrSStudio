using IGK.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS.DataTable
{
    public interface ITbGSContactInfo : IGSDataTable
    {
        string clTel { get; set; }
        string clEmail { get; set; }
    }
}
