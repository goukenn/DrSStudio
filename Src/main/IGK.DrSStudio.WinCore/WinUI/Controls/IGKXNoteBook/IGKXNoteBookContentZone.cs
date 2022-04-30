

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXNoteBookContentZone.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.WinUI
{
    public class IGKXNoteBookContentZone : IGKXUserControl 
    {
        private IGKXNoteBook c_NoteBook;
        public  IGKXNoteBookContentZone()
        {
        }
        public IGKXNoteBookContentZone(IGKXNoteBook iGKXNoteBook):this()
        {            
            this.c_NoteBook = iGKXNoteBook;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_NoteBook.SelectedPageChanged += c_NoteBook_SelectedTabChanged;
            this.Paint += _Paint;
        }

        void c_NoteBook_SelectedTabChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.Controls.Clear();
            this.Controls.Add(this.c_NoteBook.SelectedPage);
            this.ResumeLayout();
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            e.Graphics.Clear(Colorf.SkyBlue);
        }
     
    }
}
