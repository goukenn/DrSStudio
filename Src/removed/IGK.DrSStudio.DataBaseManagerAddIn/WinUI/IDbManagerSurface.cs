

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IDbManagerSurface.cs
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
file:IDbManagerSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace IGK.DrSStudio.DataBaseManagerAddIn.WinUI
{
    /// <summary>
    /// represent a db manager surface
    /// </summary>
    public interface IDbManagerSurface : IGK.DrSStudio.WinUI.ICoreWorkingSurface 
    {
        DataColumn SelectedColumn { get; }
        DataTable SelectedTable { get; }
        /// <summary>
        /// load xml file to the data set
        /// </summary>
        /// <param name="xmlfile"></param>
        void LoadXMLFile(string xmlfile);
        /// <summary>
        /// Get the data set
        /// </summary>
        DataSet DataSet { get; }
    }
}

