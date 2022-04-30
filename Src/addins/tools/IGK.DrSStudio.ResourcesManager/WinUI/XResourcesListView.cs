

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XResourcesListView.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XListView.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.ResourcesManager.WinUI
{
    /// <summary>
    /// represent a custum list view
    /// </summary>
    class XResourcesListView : ListView
    {
        ColumnHeader c_lsc_Name;
        ColumnHeader c_lsc_Type;
        ColumnHeader c_lsc_Value;
        class XResourcesListViewTextEditor : TextBox
        {
            private XResourcesListView c_listview;
            private ListViewItem.ListViewSubItem c_subitem;
            private string m_textBeforeEdit;
            private ListViewItem c_item;

            public ListViewItem.ListViewSubItem SubItem { get { return c_subitem; } }
            public ListViewItem Item { get { return c_item; } } 
            public XResourcesListViewTextEditor(XResourcesListView listview,ListViewItem item,  ListViewItem.ListViewSubItem  subitem)
            {
                this.c_listview = listview;
                this.c_subitem = subitem;
                this.c_item = item;
                this.m_textBeforeEdit = subitem.Text;
                this.Font = subitem.Font;
                this.c_listview.SelectedIndexChanged += new EventHandler(c_listview_SelectedIndexChanged);
            }
            void c_listview_SelectedIndexChanged(object sender, EventArgs e)
            {
                this.c_listview.EndEdit();
            }
            public override string ToString()
            {
                return base.ToString();
            }
            protected override void OnValidated(EventArgs e)
            {
                base.OnValidated(e);
            }
            protected override void OnKeyUp(KeyEventArgs e)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        {
                            if (!string.IsNullOrEmpty(this.Text))
                            {
                                this.c_subitem.Text = this.Text;
                            }
                            this.c_listview.EndEdit();
                            e.Handled = true;
                        }
                        break;
                    case Keys.Up:
                        {
                        int i = this.c_item.Index - 1;
                        if (i > 0)
                        {
                            this.c_subitem.Text = this.Text;
                            this.c_listview.OnEndEdit(new XResourcesItemChangedEventArgs(this.c_item , this.c_subitem));
                            
                            int editOld = this.c_item.SubItems.IndexOf(this.c_subitem);
                            this.c_item = this.c_listview.Items[i];
                            this.c_subitem = this.c_item.SubItems[editOld];
                            this.m_textBeforeEdit = c_subitem.Text;
                            this.Text = c_subitem.Text;
                            this.Bounds = c_subitem.Bounds;
                            e.Handled = true;
                        }
                }
                        break;
                    case Keys.Down:
                        {
                            int i = this.c_item.Index + 1;
                            if (i < this.c_listview.Items.Count)
                            {
                                this.c_subitem.Text = this.Text;
                                this.c_listview.OnEndEdit(new XResourcesItemChangedEventArgs(this.c_item, this.c_subitem));
                                
                                int editOld = this.c_item.SubItems.IndexOf(this.c_subitem);
                                this.c_item = this.c_listview.Items[i];
                                this.c_subitem = this.c_item.SubItems[editOld];
                                this.m_textBeforeEdit = c_subitem.Text;
                                this.Text = c_subitem.Text;
                                this.Bounds = c_subitem.Bounds;
                                e.Handled = true;
                            }
                        }
                        break;
                }

                base.OnKeyUp(e);
            }
            protected override bool IsInputKey(Keys keyData)
            {
                switch (keyData)
                { 
                    case Keys.Up:
                    case Keys.Down:
                        return true;
                }
                return base.IsInputKey(keyData);
            }
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                base.OnKeyPress(e);                
            }

        }
        XResourcesListViewTextEditor c_editor;
        public void LoadDisplayText()
        {
            this.c_lsc_Name.Text = CoreSystem.GetString("lb.Name.Caption");
            this.c_lsc_Type.Text = CoreSystem.GetString("lb.Type.Caption");
            this.c_lsc_Value.Text = CoreSystem.GetString("lb.Value.Caption");
        }
        public void Edit(ListViewItem item, int index)
        {
            ListViewItem.ListViewSubItem  i = item.SubItems[index];
            if (c_editor == null)
                c_editor = new XResourcesListViewTextEditor(this, item, i);
            c_editor.Bounds = i.Bounds;
            c_editor.Text = i.Text;
            this.Controls.Add (c_editor );
            c_editor.SelectAll();
            c_editor.Focus();
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (this.SelectedItems.Count == 1)
            {
                this.Edit(this.SelectedItems[0], 2);
            }
        }
        public XResourcesListView()
        {
            this.View = View.Details;
            this.DrawItem += new DrawListViewItemEventHandler(_DrawItem);
            this.DrawSubItem += new DrawListViewSubItemEventHandler(_DrawSubItem);
            this.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(_DrawColumnHeader);
            this.GridLines = true;
            this.FullRowSelect = true;
            c_lsc_Name = new ColumnHeader("Name")
            {
                Width = 150
            };
            c_lsc_Type = new ColumnHeader("Type")            
            {
                Width = 150
            };
            c_lsc_Value = new ColumnHeader("Value") { 
                Width = 300
            };
            this.LabelEdit = true;
            this.Columns.Add(c_lsc_Name);
            this.Columns.Add(c_lsc_Type);
            this.Columns.Add(c_lsc_Value);
            this.LoadDisplayText();
            this.ColumnClick += new ColumnClickEventHandler(XListView_ColumnClick);
        }
        void XListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
               if (this.Sorting == SortOrder.Ascending )
               {
                   this.Sorting = SortOrder.Descending ;
               }
               else
                   this.Sorting = SortOrder.Ascending ;
               this.ListViewItemSorter = new ListViewSorter(this, e.Column);
               this.Sort();
        }
     
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            base.OnDrawColumnHeader(e);
        }
        void _DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            e.DrawBackground();
            e.DrawText();
        }
        void _DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawText();
            e.DrawFocusRectangle(e.Bounds);
        }
        void _DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if ((e.ItemIndex % 2) == 0)
            {
                e.Graphics.FillRectangle(
                    CoreBrushRegisterManager.GetBrush<Brush>(XRSRenderer.RSBackgroundItem1Color),
                    e.Bounds);
            }
            else {
                e.Graphics.FillRectangle(
                    CoreBrushRegisterManager.GetBrush<Brush>(XRSRenderer.RSBackgroundItem2Color),
                    e.Bounds);
            }
            switch (e.State)
            {
                case ListViewItemStates.Checked:
                    break;
                case ListViewItemStates.Default:
                    break;
                case ListViewItemStates.Focused:
                    break;
                case ListViewItemStates.Grayed:
                    break;
                case ListViewItemStates.Hot:
                    break;
                case ListViewItemStates.Indeterminate:
                    break;
                case ListViewItemStates.Marked:
                    break;
                case ListViewItemStates.Selected:
                    break;
                case ListViewItemStates.ShowKeyboardCues:
                    break;
                default:
                    break;
            }
            //e.DrawText ();
            //e.Graphics.DrawString (e.Item.Text , 
            //    e.Item.Font ,
            //    e.Item.ForeColor ,
        }
        internal void EndEdit()
        {
            if (c_editor != null)
            {
                OnEndEdit(new XResourcesItemChangedEventArgs(c_editor.Item, c_editor.SubItem));
                this.Controls.Remove(c_editor);
                c_editor.Dispose();
                c_editor = null;
            }
        }


        public event EventHandler<XResourcesItemChangedEventArgs> EndEditEvent;

        private void OnEndEdit(XResourcesItemChangedEventArgs e)
        {
            if (EndEditEvent != null)
            {
                EndEditEvent(this, e);
            }
        }
        class ListViewSorter : System.Collections.IComparer 
        {
            #region IComparer Members
            public int Compare(object x, object y)
            {                
                ListViewItem lst1 = (ListViewItem)x;
                ListViewItem lst2 = (ListViewItem)y;
                return lst1.SubItems[m_index].Text.CompareTo(
                    lst2.SubItems[m_index].Text);
            }
            #endregion
            ListView listview;
            int m_index;
            public ListViewSorter(ListView listview, int index)
            {
                this.listview = listview;
                this.m_index = index;
            }
        }
    }


}

