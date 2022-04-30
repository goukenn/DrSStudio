using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS
{
    [GSDataGuiAttribute(GuiType = enuGuiType.LabelEditableComboBox)]
    /// <summary>
    /// represent a default data table entry
    /// </summary>
    public interface IGSDataTable : IGSDataCell, IGSDataValue
    {
        Type getSourceTableInterface();
      

        [GSDataGuiAttribute(GuiType = enuGuiType.NotVisible)]
        [GSDataField(enuGSDataField.IsPrimaryKey | enuGSDataField.AutoIncrement)]
        int clId { get; set; }
    }
}
