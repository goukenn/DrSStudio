

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _newWiXFile.cs
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
file:_newWiXFile.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using IGK.ICore.IO;
namespace IGK.DrSStudio.WiXAddIn.Menu.File
{
    [DrSStudioMenu("File.New.NewWixFile", 150)]
    class _newWiXFile : CoreApplicationMenu
    {
        protected override void InitMenu()
        {
            base.InitMenu();
        }
        protected override bool PerformAction()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "wix project| *." + WiXConstant.EXTENSION;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    WiXWriter writer = WiXWriter.Create(sb );
                    WiXDocument doc = new WiXDocument();
                    WiXDirectory dir = doc.Features.GetElementById("TARGETDIR") as WiXDirectory;
                    if (dir != null)
                    {
                        WiXDirectory hdir = dir.AddDir("ProgramFilesFolder", null);
                        WiXDirectory cdir = hdir.AddDir("APP", "IGKDEV");
                        WiXDirectoryComponent comp = new WiXDirectoryComponent ();
                        comp.Id = "MyComponent";
                        foreach (string f in Directory.GetFiles(PathUtils.GetDirectoryName(sfd.FileName), "*.txt", SearchOption.TopDirectoryOnly))
                        {
                            comp.Children.Add(new WiXFileEntry(Path.GetFileNameWithoutExtension (f), Path.GetFileName(f), Path.GetFileName(f)));
                        }
                        WiXFeatureEntry fet = doc.GetFeature(0);
                        fet.Add(comp);
                        cdir.Children.Add(comp);
                    }
                    writer.Visit(doc);
                    writer.Flush();
                    System.IO.File.WriteAllText(sfd.FileName, sb.ToString());
                }
            }
            return base.PerformAction();
        }
    }
}

