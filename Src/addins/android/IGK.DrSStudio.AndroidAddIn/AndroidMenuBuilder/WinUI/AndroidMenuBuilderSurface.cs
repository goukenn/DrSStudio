
using IGK.DrSStudio.Android.Menu.Android;
using IGK.DrSStudio.Android.WinUI;

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder.WinUI
{
    [CoreSurface("{D8FE05EC-2E37-45C5-A322-561EDE9C1022}",
        EnvironmentName=AndroidConstant.ENVIRONMENT+".BuildMenuEditor")]
    public class AndroidMenuBuilderSurface : 
        AndroidResourceEditorSurfaceBase,
        ICoreWorkingSurface ,
        ICoreWorkingFilemanagerSurface 
    {
        private AndroidMenuBuilderPanelControl c_panel;

        public override string Title
        {
            get
            {
                return base.Title;
            }
            protected set
            {
                base.Title = value;
            }
        }

        public AndroidMenuBuilderSurface()
        {
            this.c_panel = new AndroidMenuBuilderPanelControl(); 
            this.c_panel.Dock = DockStyle.Fill;
            this.c_panel.ShowPropertiesTreeview = false;
            this.Controls.Add(this.c_panel);
            this.Title = "AndroidMenuBuilder";
            this.FileNameChanged += _FileNameChanged;
        }

        void _FileNameChanged(object sender, EventArgs e)
        {
            this.Title = Path.GetFileName(this.FileName);
        }
        public override ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo(
                "title.saveandroidMenu".R(),                
                "Android menu | *.xml; |Gdks file | *.gkds; |Pictures (.png,jpeg,bmp)|*.png;*.jpg;*.jpeg;*.bmp",
                this.FileName);
        }

        public override void Save()
        {
            this.SaveAs(this.FileName);
            this.NeedToSave = false;
        }
        public override void SaveAs(string filename)
        {
            string v_ext = Path.GetExtension(filename).ToLower();
            switch (v_ext)
            {
                case ".gkds":
                    IGK.ICore.Codec.CoreEncoder.Instance.Save(filename, this.c_panel.Surface.CurrentDocument);
                    break;
                case ".png":
                case ".jpg":
                case ".bmp":
                    var c = CoreSystem.GetEncodersByExtension(v_ext).CoreGetValue<IGK.ICore.Codec.ICoreCodec>(0);
                    if (c != null)
                    {
                        IGK.ICore.Codec.CoreEncoder.Instance.SaveWithEncoder(c,
                            this.c_panel.Surface, filename,
                            new ICoreWorkingDocument[] { this.c_panel.Surface.CurrentDocument });
                    }
                    break;
                default:                    
                    this.c_panel.Save(filename);
                    this.FileName = filename;
                    break;
            }
        }
        /// <summary>
        /// create menu builder from file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static AndroidMenuBuilderSurface CreateFromFile(string filename)
        {
            AndroidMenuBuilderSurface c = new AndroidMenuBuilderSurface();
            c.c_panel.LoadFile(filename);
            c.FileName = filename;
            c.NeedToSave = false;
            return c;
        }
        /// <summary>
        /// clear all menu
        /// </summary>
        public  void ClearAll()
        {
            this.c_panel.ClearMenu();
        }
    }
}
