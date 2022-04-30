#define CAN_GEN_MSI

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _NewDrSStudioInstaller.cs
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
file:_NewDrSStudioInstaller.cs
*/



using IGK.DrSStudio.WiXAddIn.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace IGK.DrSStudio.WiXAddIn.Menu.File
{


#if CAN_GEN_MSI
    [DrSStudioMenu("File.NewDrSStudioInstaller", 151)]
    class _NewDrSStudioInstaller : CoreApplicationMenu
    {
        protected override void InitMenu()
        {
            base.InitMenu();
        }
        protected override bool IsVisible()
        {
            return base.IsVisible();
        }
        protected override bool PerformAction()
        {
            if (!WiXUtils.CheckEnvironment())
            {
                MessageBox.Show("WiX.Msg.FileNotExists".R(), "WiX.Title.FileNotExists".R());
                return false;
            }
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "msi package|*.msi";
                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                return new WiXDrSStudioInstaller().Generate(sfd.FileName);
            } 
        }
    }
#endif
}

