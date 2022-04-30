

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: NewAndroidManifestFile.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:OpenManifestFile.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Android.Menu.File
{
    
using IGK.ICore;using IGK.DrSStudio.Android.WinUI;
    
    using IGK.ICore.Menu;
    using IGK.ICore.WinCore;
    
    [CoreMenu("File.New.Android.ManifestFile", 100)]
    class NewAndroidManifestFile : CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
            //using (OpenFileDialog ofd = new OpenFileDialog())
            //{
            //    ofd.Filter = "Android Manifest File(*.xml) | *.xml;";
            //    if (ofd.ShowDialog() == DialogResult.OK)
            //    {
                    //AndroidManifest manifest = AndroidManifest.LoadManifest(ofd.FileName);
                    //if ((manifest != null) && (manifest.IsValid))
                    //{
            this.MainForm.SetCursor(Cursors.WaitCursor);
                        AndroidManifestEditorSurface v_sr = Workbench.GetSurface("ManifestEditor") as
                            AndroidManifestEditorSurface;
                        if (v_sr == null)
                        {
                            v_sr = new AndroidManifestEditorSurface();
                            v_sr.Name = "ManifestEditor";
                            this.Workbench.AddSurface (v_sr,true );
                        }
                        string f = System.IO.Path.GetTempFileName();
#pragma warning disable IDE0054 // Use compound assignment
                        System.IO.File.Move(f, f = f + ".xml");
#pragma warning restore IDE0054 // Use compound assignment
                        v_sr.FileName = f;
                        this.Workbench.SetCurrentSurface(v_sr);
                        this.MainForm.SetCursor(Cursors.Default);
            //        }
            //    }
            //}
            return base.PerformAction();
        }
    }
}

