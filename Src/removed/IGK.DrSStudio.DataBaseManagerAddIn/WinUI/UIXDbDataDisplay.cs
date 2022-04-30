

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXDbDataDisplay.cs
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
file:UIXDbDataDisplay.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.DataBaseManagerAddIn.WinUI
{
    public partial class UIXDbDataDisplay : UserControl
    {
        /// <summary>
        /// get the selected table name
        /// </summary>
        public String SelectTableName {
            get {
                if (this.c_tables.SelectedItem != null)
                    return ((DataTable)this.c_tables.SelectedItem).TableName;
                return null;
            }
        }
        public object Tables {
            get {
                return this.c_tables.DataSource;
            }
            set {
                this.c_tables.DataSource = value ;
                this.c_tables.DisplayMember = "TableName";
            }
        }
        public object StroredProcedure {
            get {
                return this.c_storedProcedure.DataSource;
            }
            set {
                this.c_storedProcedure.DataSource = value;
            }
        }
        /// <summary>
        /// get thet select table changed
        /// </summary>
        public event EventHandler SelectTableChanged {
            add { this.c_tables.SelectedIndexChanged += value; }
            remove { this.c_tables.SelectedIndexChanged -= value; }
        }
        public UIXDbDataDisplay()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.FixedHeight, true);
        }
    }
}

