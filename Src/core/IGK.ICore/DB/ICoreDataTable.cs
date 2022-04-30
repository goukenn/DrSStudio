using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    public interface ICoreDataTable
    {
        /// <summary>
        /// get and retrieve the source interface of data table
        /// </summary>
        /// <returns></returns>
        Type GetSourceTableInterface();
        [CoreDataGui(GuiType = enuGuiType.NotVisible)]
        [CoreDataTableField (enuCoreDataField.IsPrimaryKey | enuCoreDataField.AutoIncrement | enuCoreDataField.IsNotNull)]
        int clId { get; set; }
    }
}
