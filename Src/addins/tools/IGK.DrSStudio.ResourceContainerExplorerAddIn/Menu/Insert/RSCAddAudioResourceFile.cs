using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Menu;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Menu.Insert
{
    [CoreMenu("Insert.AudioResourceFile", 0x80)]
    public class RSCAddAudioResourceFile : RSCMenuBase 
    {
        protected override bool PerformAction()
        {
            var s = this.Workbench.CurrentSurface as IGK.ICore.WinUI.ICoreWorkingProjectManagerSurface;
            if (s == null)
                return false;
            var p = s.GkdsElement;
            if (p == null)
                return false;
            var r = p.GetResourceElement();
            if (r == null)
                return false;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "title.dlg.insertAudioFile".R();
                ofd.Filter = "Audio file | *.mp3;*.wav;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (!r.Resources.Contains(enuCoreResourceType.SoundFile, ofd.FileName))
                    {
                        r.Resources.Register(
                            new RSCAudioResourceItem(ofd.FileName));
                    }
                }
            }
            return base.PerformAction();
        }
        protected override void InitMenu()
        {
            
            base.InitMenu();
        }
    }
}
