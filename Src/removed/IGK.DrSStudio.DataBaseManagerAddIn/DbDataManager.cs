

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DbDataManager.cs
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
file:DbDataManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb ;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace IGK.DrSStudio.DataBaseManagerAddIn
{
    using IGK.ICore;using IGK.DrSStudio.DataBaseManagerAddIn.WinUI;
    /// <summary>
    /// represent the system db manager
    /// </summary>
    class DbDataManager
    {
        private static DbDataManager sm_instance;
        private IGK.DrSStudio.DataBaseManagerAddIn.WinUI.IDbManagerSurface m_Surface;
        public IGK.DrSStudio.DataBaseManagerAddIn .WinUI.IDbManagerSurface  Surface
        {
            get { return m_Surface; }
        }
        private DbDataManager()
        {
        }
        public static DbDataManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static DbDataManager()
        {
            sm_instance = new DbDataManager();
        }
        public static bool IsProviderDefined(string name)
        {
            return false;
        }
        /// <summary>
        /// create a new dbgrid view surface
        /// </summary>
        internal void CreateNewSurface()
        {
            UXDbManagerGridView v_surface = new UXDbManagerGridView();
            this.m_Surface = v_surface;
        }
    }
}

