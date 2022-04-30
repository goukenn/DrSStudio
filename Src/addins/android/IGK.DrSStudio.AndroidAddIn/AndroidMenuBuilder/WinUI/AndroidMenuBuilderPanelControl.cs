

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder.WinUI
{
    using IGK.DrSStudio.Android.IO;
    using IGK.DrSStudio.Android.Resources;
    using IGK.DrSStudio.Android.Sdk;
    using IGK.DrSStudio.Android.WinUI;
    
    using IGK.ICore.WinCore.Dispatch;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.Xml;



    public class AndroidMenuBuilderPanelControl : 
        IGKXUserControl,
        IAttributeEditorLoader,
        IAttributeEditorStoreListener,
        IAndroidResourceItemSelectedListener
    {
        private IGKXPanel c_pan_left;
        private IGKXPanel c_pan_content;
        private IGKXSplitter igkxSplitter1;
        private IGKXAttributeEditor c_attributeEditor;
        private AndroidMenuBuilderDesignSurface c_surface;
        private string m_PlatForm;
        /// <summary>
        /// get the surface
        /// </summary>
        public AndroidMenuBuilderDesignSurface Surface {
            get
            {
                return this.c_surface;
            }
        }
        public string PlatForm
        {
            get { return m_PlatForm; }
            set
            {
                if (m_PlatForm != value)
                {
                    m_PlatForm = value;
                }
            }
        }
        public bool ShowPropertiesTreeview
        {
            get
            {
                return this.c_attributeEditor.ShowTreeView;
            }
            set { this.c_attributeEditor.ShowTreeView = value; }
        }

        public AndroidMenuBuilderPanelControl()
        {
            this.InitializeComponent();
            this.SuspendLayout();
            this.c_surface = new AndroidMenuBuilderDesignSurface();
            this.c_attributeEditor = new IGKXAttributeEditor();

            this.PlatForm = "android-20";
            this.c_surface.Dispatcher = new WinCoreControlDispatcher(this);
            this.c_surface.Dock = System.Windows.Forms.DockStyle.Fill;

            this.c_attributeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_content.Controls.Add(c_surface);
            this.c_pan_left.Controls.Add(c_attributeEditor);
            this.ResumeLayout();

            this.c_attributeEditor.SetAttributeLoaderListener(this);
            this.c_attributeEditor.SetStoreAttributeListener(this);
            this.c_attributeEditor.AttributeValueChanged += c_attributeEditor_AttributeValueChanged;



            AndroidMenuBuilderViewDataAdapter c = new AndroidMenuBuilderViewDataAdapter();
            c.SetSelectedChangedListener(this);
            c_surface.Adapter = c;
        }
        /// <summary>
        /// Get the adapter
        /// </summary>
        public AndroidMenuBuilderViewDataAdapter Adapter {
            get {
                return this.c_surface.Adapter as AndroidMenuBuilderViewDataAdapter ;
            }
        }

        void c_attributeEditor_AttributeValueChanged(object sender, AttributeValueChangedEventArgs e)
        {
            AndroidResourceItemBase n = this.c_attributeEditor.CurrentNode 
                as AndroidResourceItemBase 
                ;

          //  int i = c_surface.Adapter.GetView();
            //update the value
             c_surface.OnDataChanged(enuChangedType.DataValueChanged, c_surface.Adapter.IndexOf(n));
        }
        public void ClearMenu()
        {
            this.c_surface.Adapter.Clear();
        }
        private void InitializeComponent()
        {
            this.c_pan_left = new IGKXPanel();
            this.c_pan_content = new IGKXPanel();
            this.igkxSplitter1 = new IGKXSplitter();
            this.SuspendLayout();
            // 
            // c_pan_left
            // 
            this.c_pan_left.CaptionKey = null;
            this.c_pan_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_pan_left.Location = new System.Drawing.Point(0, 0);
            this.c_pan_left.Name = "c_pan_left";
            this.c_pan_left.Size = new System.Drawing.Size(200, 293);
            this.c_pan_left.TabIndex = 0;
            // 
            // c_pan_content
            // 
            this.c_pan_content.CaptionKey = null;
            this.c_pan_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_content.Location = new System.Drawing.Point(200, 0);
            this.c_pan_content.Name = "c_pan_content";
            this.c_pan_content.Size = new System.Drawing.Size(350, 293);
            this.c_pan_content.TabIndex = 1;
            // 
            // igkxSplitter1
            // 
            this.igkxSplitter1.Location = new System.Drawing.Point(200, 0);
            this.igkxSplitter1.Name = "igkxSplitter1";
            this.igkxSplitter1.Size = new System.Drawing.Size(3, 293);
            this.igkxSplitter1.TabIndex = 0;
            this.igkxSplitter1.TabStop = false;
            // 
            // AndroidMenuBuilderPanelControl
            // 
            this.Controls.Add(this.igkxSplitter1);
            this.Controls.Add(this.c_pan_content);
            this.Controls.Add(this.c_pan_left);
            this.Name = "AndroidMenuBuilderPanelControl";
            this.Size = new System.Drawing.Size(550, 293);
            this.ResumeLayout(false);
        }        

        public void OnItemSelected(CoreXmlElement item)
        {
            this.c_attributeEditor.LoadNode(item);
            if (item != null)
            {
                this.c_attributeEditor.Title = item.GetProperty(AndroidMenuResource.ATTRIBUTENAME);
            }
        }

        public void StoreAttribute(IGKXAttributeEditor editor, string filename)
        {
            var adapt = this.c_surface.Adapter;
            if (adapt == null)
                return;
            CoreXmlElement t = adapt.GetObject(0) as CoreXmlElement;
            if (t != null)
            {
                AndroidResourceSaver.WriteAllText(filename, t.RenderXML(new CoreXmlSettingOptions() { 
                    Indent = true   
                }));
            }
        }

        public void LoadAttribute(IGKXAttributeEditor editor, CoreXmlElement node)
        {
            editor.Attributes.Clear();
            string h = node.GetProperty("android:AttrName");
            CoreXmlElement r = AndroidSdkExtensions.LoadAttributes(this.PlatForm, h);
            if (r != null)
            {
                foreach (CoreXmlElement b in r.getElementsByTagName("attr"))
                {

                    AndroidAttributeItem attr = AndroidAttributeItem.CreateAttributeItem(b);
                    attr.Value = node[attr.Name] ?? node["android:" + attr.Name];
                    editor.Attributes.Add(attr);
                }
                editor.Attributes.Sort();
            }
            else { 
                //no attribute

            }
        }
        [Obsolete()]
        public void Save()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "xml | *.xml";
               
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.c_attributeEditor.Save(sfd.FileName);
                    //this.StoreAttribute(this , sfd.FileName);
                }
            }
        }
        public void Save(string filename)
        {
            this.c_attributeEditor.Save(filename);         
        }
        public int Position { get; set; }

        internal void LoadFile(string filename)
        {
            ///load file hierarchi
            this.c_attributeEditor.LoadFile (filename);
            ///clear adapter
            this.c_surface.Adapter.Clear();
            
            ///load items

            this.Adapter.LoadData(this.c_attributeEditor.CurrentNode);
        }
    }
}
