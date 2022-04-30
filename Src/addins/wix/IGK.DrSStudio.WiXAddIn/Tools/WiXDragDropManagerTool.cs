

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXDragDropManagerTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXDragDropManagerTool.cs
*/
using IGK.DrSStudio.WiXAddIn.WinUI;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn.Tools
{
    [CoreTools("Tool.WiX.DragDropManager")]
    class WiXDragDropManagerTool : WiXToolBase 
    {
        private static WiXDragDropManagerTool sm_instance;
        private WiXDragDropManagerTool()
        {
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }
        public static WiXDragDropManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WiXDragDropManagerTool()
        {
            sm_instance = new WiXDragDropManagerTool();
        }
        protected override void RegisterWixSurface(WiXSurface surface)
        {
            surface.ListViewZone.DragDrop += ListViewZone_DragDrop;
            surface.ListViewZone.DragEnter += ListViewZone_DragEnter;
            surface.ListViewZone.DragOver += ListViewZone_DragOver;
        }
        protected override void UnRegisterWixSurface(WiXSurface surface)
        {
            surface.ListViewZone.DragDrop -= ListViewZone_DragDrop;
            surface.ListViewZone.DragEnter -= ListViewZone_DragEnter;
            surface.ListViewZone.DragOver -= ListViewZone_DragOver;
        }
        void ListViewZone_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
        }
        void ListViewZone_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (!this.CurrentSurface.CanDoDragDrop)
                return;
            string[] format = e.Data.GetFormats();
            const string FileName = "FileDrop";
            if (e.Data.GetDataPresent(FileName))
            {
                e.Effect = System.Windows.Forms.DragDropEffects.Copy;
            }
        }
        void ListViewZone_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (!this.CurrentSurface.CanDoDragDrop)
                return;
            const string FileName = "FileDrop";
            string v_filename = string.Empty;
            if (e.Effect == System.Windows.Forms.DragDropEffects.Copy)
            {
                object obj = e.Data.GetData(FileName);
                if (obj is string[])
                {
                    foreach (var item in (obj as string[]))
                    {
                        this.Import(item);
                    }
                }
                else
                {
                    v_filename = (string)e.Data.GetData(FileName);
                    this.Import(v_filename);
                }
            }
        }
        private void Import(string item)
        {
            if (System.IO.File.Exists(item))
            {
                this.CurrentSurface.AddFiles(new WiXProjectFile(item));
            }
            else { 
                WiXProjectFile dir  = new WiXProjectFile (item);
                BuildDir(dir, item);
                this.CurrentSurface.AddFiles(dir);
            }
        }
        private void BuildDir(WiXProjectFile dir, string path)
        {
            foreach (string f in Directory.GetFiles(path))
            {
                dir.AddChild(new WiXProjectFile(f));
            }
            foreach (string d  in Directory.GetDirectories (path))
            {
                WiXProjectFile v_dir  = new WiXProjectFile (System.IO.Path.GetFileName (d));
                BuildDir(v_dir, d);
                dir.AddChild(v_dir);
            }
        }
    }
}

