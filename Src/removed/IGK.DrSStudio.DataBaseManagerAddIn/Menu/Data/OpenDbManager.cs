

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OpenDbManager.cs
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
file:OpenDbManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.DataBaseManagerAddIn.Menu.Data
{
    [DbManagerMenu ("Data.OpenDB", 0)]
    sealed class OpenDbManager : DbManagerBaseMenu 
    {
        protected override bool IsEnabled()
        {
            return true;
        }
        protected override bool PerformAction()
        {
            if (DbDataManager.Instance.Surface == null)
            {
                DbDataManager.Instance.CreateNewSurface();
                this.Workbench.Surfaces.Add(DbDataManager.Instance.Surface);
            }
            else {
                this.Workbench.CurrentSurface = DbDataManager.Instance.Surface;
            }
            return true;
        }
    }
}

