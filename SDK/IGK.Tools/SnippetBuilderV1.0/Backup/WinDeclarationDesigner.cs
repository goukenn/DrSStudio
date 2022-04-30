/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
  
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSnippetBuilder
{
    public partial class WinDeclarationDesigner : Form
    {
        ISnippetDeclarationCollection m_declarationCollection;

        public WinDeclarationDesigner(ISnippetDeclarationCollection declarationCollection)
        {
            InitializeComponent();
            m_declarationCollection = declarationCollection;
            Populate();

            this.c_cmbType.Items.Add("Literal");
            this.c_cmbType.Items.Add("Object");
            this.c_cmbType.SelectedIndex = 0;
            this.c_cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void Populate()
        {
            this.c_lsbDeclaration.Items.Clear();
            foreach (ISnippetDeclarationItem item in m_declarationCollection)
            {
                this.c_lsbDeclaration.Items.Add(item);
            }
            
        }

        private void c_btnAdd_Click(object sender, EventArgs e)
        {
            int v_c = this.m_declarationCollection.Count;
            switch (this.c_cmbType.Text)
            {
                case "Literal":
                    SnippetItemLitteral v_sn = new SnippetItemLitteral();
                    this.m_declarationCollection.Add(v_sn);
                    this.c_prGrid.SelectedObject = v_sn;
                    break;
                case "Object":
                    SnippetItemObject v_ob = new SnippetItemObject("System.Type");
                    this.m_declarationCollection.Add(v_ob);
                    this.c_prGrid.SelectedObject = v_ob;
                    break;
            }
            if (this.m_declarationCollection.Count > v_c)
            {
                this.Populate();
                //this.c_lsbDeclaration.Items.Add(this.c_prGrid.SelectedObject);
            }
        }

        private void c_btnRemove_Click(object sender, EventArgs e)
        {
            foreach (object i in this.c_lsbDeclaration .SelectedItems )
            {
                this.m_declarationCollection .Remove (i as ISnippetDeclarationItem );
            }
            this.Populate();
        }

        private void c_lsbDeclaration_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.c_prGrid.SelectedObject = this.c_lsbDeclaration.SelectedItem;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void c_prGrid_Click(object sender, EventArgs e)
        {

        }

        private void c_prGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "ID")
            {
                this.c_lsbDeclaration.Update ();//this.c_lsbDeclaration .Items[e.]
                //this.c_lsbDeclaration.Refresh();
                //this.c_lsbDeclaration.ResumeLayout(true);
            }

        }
    }
}
