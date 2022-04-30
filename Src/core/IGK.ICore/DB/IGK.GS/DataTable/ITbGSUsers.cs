using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;






﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//contains data table basics declaration
namespace IGK.GS.DataTable
{
    [GSDataSystemTable(GSSystemDataTables.Users)]
    /// <summary>
    /// represent a TbUser used to login
    /// </summary>
    public interface ITbGSUsers  : IGSDataTable 
    {   
        [GSDataField(enuGSDataField.Unique, TypeName=GSConstant.VARCHAR , Length = 50 ) ]
        string clLogin { get; set; }
        string clPwd { get; set; }
        int clLevel { get; set; }
        DateTime clLastLogin { get; set; }
    }
}
