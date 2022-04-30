using IGK.DrSStudio.Balafon.DataBaseBuilder.XML;
using IGK.ICore;
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

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.WinUI
{
    [CoreSurface("{3254790D-2F23-4B90-909F-7C7632D42CB5}")]
    public class BalafonDBBEditorSurface : 
        IGKXWinCoreWorkingFileManagerSurface
    {
        private readonly BalafonDBBSurface c_surface;

        /// <summary>
        /// get the schema definition of this data
        /// </summary>
        public BalafonDBBSchemaDataDefinition DataSchema
        {
            get {
                return c_surface.CurrentTable;
            }
        }
        public BalafonDBBSchemaDocument Document
        {
            get { return c_surface.Document; }
            set
            {
                c_surface.Document = value;
            }
        }
        public CoreXmlWebDocument WebDocument
        {
            get { return c_surface.WebDocument ; }
           
        }
        public event EventHandler DocumentChanged { 
            add{
                c_surface.DocumentChanged += value;
            }
            remove {
                c_surface.DocumentChanged -= value;
            }
        }

        public BalafonDBBEditorSurface()
        {
            c_surface = new BalafonDBBSurface
            {
                Dock = DockStyle.Fill,
                Document = new BalafonDBBSchemaDocument()
            };
            this.Controls.Add(c_surface);
            this.Load += _Load;            
        }
     
        public override ICoreSaveAsInfo GetSaveAsInfo()
        {

            return new CoreSaveAsInfo(
                "title.saveschema".R(),
                "Balafon Data schema xml files | *.xml; | (*.*)|*.*",
                this.Document.FileName);
        }
        
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
        }

        private void _Load(object sender, EventArgs e)
        {
            this.Title = this.FileName != null ? Path.GetFileNameWithoutExtension(this.FileName) :
                "title.balafonDbEditor".R();
        }

        /// <summary>
        /// save with this filename
        /// </summary>
        public override void Save()
        {

            this.SaveAs(this.FileName);            
        }
        public override void SaveAs(string filename)
        {
            this.c_surface.SaveAs(filename);
            this.FileName = filename;
            this.NeedToSave = false;
        }

        internal static BalafonDBBEditorSurface CreateFrom(CoreXmlElement e, string filename){
            BalafonDBBEditorSurface v_s = new BalafonDBBEditorSurface();
            v_s.Document.Load(e);
            v_s.FileName = filename;
            v_s.CreateControl();
            v_s.c_surface.RefreshView ();
            v_s.Document.FileName = filename;
            return v_s;
        }
        public override void SetParam(ICoreInitializatorParam p)
        {
            base.SetParam(p);
            //empty document
            CoreXmlElement e = CoreXmlElement.CreateXmlNode(BalafonDBBConstant.DATA_SCHEMAS_TAG);
            this.Document.Load(e);
            this.FileName = Path.Combine(Environment.CurrentDirectory, BalafonDBBConstant.FILE_NAME);
            this.CreateControl();
            this.c_surface.RefreshView();
        }
    }
}
