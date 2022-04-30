

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ToolPictureMenu.cs
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
file:ToolPictureMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.Menu
{
    [CoreMenu (PicEditorConstant.TOOL_PICTUREMENU, 502)]
    class ToolPictureMenu : CoreMenuActionBase 
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                string filter = IGK.DrSStudio.Codec.CoreEncoderBase.GetFilter(CoreConstant.CAT_Picture);
                StringBuilder sb = new StringBuilder();
                sb.Append ("alls picturefiles |");
                string[] t = filter.Split('|');
                for (int i = 1; i < t.Length; i+=2)
                {
                    sb.Append(t[i]);
                }
                filter = sb.ToString() + "|" + filter;
                ofd.Filter = filter;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                        WinUI.XWindowsPictureEditor editor = new IGK.DrSStudio.WinUI.XWindowsPictureEditor();
                    editor.FileName = ofd.FileName;
                    this.Workbench.Surfaces.Add(editor);
                    this.Workbench.CurrentSurface = editor;
                    editor.ContextMenuStrip  = this.Workbench.MainForm.ContextMenuStrip;
                }
            }
            return base.PerformAction();
        }
    }
}

