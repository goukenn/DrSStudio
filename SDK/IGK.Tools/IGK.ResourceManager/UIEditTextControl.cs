using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.ResMan
{
    class UIEditTextControl : UserControl
    {
        private Button c_btn_next;
        private Label label1;
        private TextBox textBox1;
        private Button c_btn_ok;
        private int m_Index;
        private ListViewItem m_CurrentListViewItem;

        public ListViewItem CurrentListViewItem
        {
            get { return m_CurrentListViewItem; }
            set
            {
                if (m_CurrentListViewItem != value)
                {
                    m_CurrentListViewItem = value;
                }
            }
        }
        /// <summary>
        /// get or set the index
        /// </summary>
        public int Index
        {
            get { return m_Index; }
            set
            {
                if (m_Index != value)
                {
                    m_Index = value;
                }
            }
        }
        /// <summary>
        /// get or set the item count
        /// </summary>
        public int Count {
            get { 
               if (this.ListView !=null)
                    return this.ListView.Items.Count;
                return 0;
            }
        }
        public UIEditTextControl()
        {
            this.InitializeComponent();
            this.label1.Text = string.Empty;
            this.Load += _Load;
        }

        private void _Load(object sender, EventArgs e)
        {
            if ((this.Count > 0) && (this.Index < this.Count ))
            {
                GetData();
                this.Index++;
            }
        }

        private void GetData()
        {

            ListViewItem i = this.ListView.Items[this.Index];
            this.label1.Text = i.Text;
            this.textBox1.Text = i.SubItems[2].Text;
            this.CurrentListViewItem = i;
        }

        private void InitializeComponent()
        {
            this.c_btn_next = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.c_btn_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // c_btn_next
            // 
            this.c_btn_next.Location = new System.Drawing.Point(199, 169);
            this.c_btn_next.Name = "c_btn_next";
            this.c_btn_next.Size = new System.Drawing.Size(75, 23);
            this.c_btn_next.TabIndex = 1;
            this.c_btn_next.Text = "next";
            this.c_btn_next.UseVisualStyleBackColor = true;
            this.c_btn_next.Click += new System.EventHandler(this.c_btn_next_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 31);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(271, 132);
            this.textBox1.TabIndex = 0;
            // 
            // c_btn_ok
            // 
            this.c_btn_ok.Location = new System.Drawing.Point(118, 169);
            this.c_btn_ok.Name = "c_btn_ok";
            this.c_btn_ok.Size = new System.Drawing.Size(75, 23);
            this.c_btn_ok.TabIndex = 2;
            this.c_btn_ok.Text = "validate";
            this.c_btn_ok.UseVisualStyleBackColor = true;
            this.c_btn_ok.Click += new System.EventHandler(this.c_btn_ok_Click);
            // 
            // UIEditTextControl
            // 
            this.Controls.Add(this.c_btn_ok);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.c_btn_next);
            this.Name = "UIEditTextControl";
            this.Size = new System.Drawing.Size(277, 195);
            this.Load += new System.EventHandler(this.UIEditTextControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void c_btn_ok_Click(object sender, EventArgs e)
        {
            this.UpdateValue();
            this.FindForm().DialogResult = DialogResult.OK;
        }

        private void UpdateValue()
        {

            //register new value
            if (this.m_CurrentListViewItem != null)
            {
                this.CurrentListViewItem.SubItems[2].Text = this.textBox1.Text;
                this.CurrentListViewItem.Tag = this.textBox1.Text;
            }
        }

        private void c_btn_next_Click(object sender, EventArgs e)
        {
            this.UpdateValue();
            this.EditNext();
        }

        private void EditNext()
        {
            if (this.Index < this.Count)
            {
                this.GetData();
                this.Index++;
                this.textBox1.Focus();
            }
        }

        private void UIEditTextControl_Load(object sender, EventArgs e)
        {

        }

        public ListView ListView { get; set; }
    }
}
