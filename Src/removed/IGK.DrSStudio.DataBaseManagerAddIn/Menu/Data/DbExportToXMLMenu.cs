

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DbExportToXMLMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:DbExportToXMLMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.DataBaseManagerAddIn.Menu.Data
{
    [DbManagerMenu ("Data.ExportDataSetToXML", 50)]
    public class DbExportToXMLMenu : DbManagerBaseMenu 
    {
        protected override bool PerformAction()
        {
            if ((this.CurrentSurface != null) && (this.CurrentSurface.DataSet  != null))
            {
                using (SaveFileDialog ofd = new SaveFileDialog())
                {
                    ofd.Filter = "XML Files| *.xml;";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        this.CurrentSurface.DataSet.WriteXml(ofd.FileName);
                    }
                }
            }
            return base.PerformAction();
        }
    }
}

