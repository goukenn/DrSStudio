
using IGK.ICore;
using IGK.ICore.Codec;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IGK.DrSStudio.Android.AndroidThemeBuilder
{
    using Entities;
    using ICore.Xml;
    using IGK.ICore.Extensions;

    /// <summary>
    /// represent an android Theme files 
    /// </summary>
    public class ATBThemeFile
    {
        private readonly AndroiThemeCollection m_themes;
        /// <summary>
        /// get or set the default platform name
        /// </summary>
        public AndroidTargetInfo DefaultPlateForm { get; set; }


        class AndroiThemeCollection : IEnumerable, IEnumerable<ATBTheme>
        {
            private readonly List<ATBTheme> m_themes;
            private readonly ATBThemeFile m_owner;
            private ATBTheme m_primaryTheme;
            
            public ATBTheme  this[int index]
            {
                get { return m_themes[index]; }
            }
            public AndroiThemeCollection(ATBThemeFile owner)
            {
                 this.m_themes = new List<ATBTheme> ();
                 this.m_owner = owner;

                //build primary theme
                 this.m_primaryTheme = new ATBTheme()
                 {
                     Name = Path.GetFileNameWithoutExtension(owner.FileName)
                 };
                this.m_primaryTheme.File = this.m_owner ;
                this.m_themes.Add(m_primaryTheme);
                owner.FileNameChanged += owner_FileNameChanged;
            }

            void owner_FileNameChanged(object sender, EventArgs e)
            {
                if (this.m_primaryTheme.Name == null)
                {
                    this.m_primaryTheme.Name = Path.GetFileNameWithoutExtension( this.m_owner.FileName);
                }
            }
            internal void Clear() {
                this.m_themes.Clear();
            }
            /// <summary>
            /// get the number of theme
            /// </summary>
            public int Count {
                get {
                    return this.m_themes.Count;
                }
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_themes.GetEnumerator();
            }

            IEnumerator<ATBTheme> IEnumerable<ATBTheme>.GetEnumerator()
            {
                return this.m_themes.GetEnumerator();
            }

            

            internal void Add(ATBTheme androidTheme)
            {
                if ((androidTheme != null) && !this.m_themes.Contains(androidTheme))
                {
                    this.m_themes.Add(androidTheme);
                    androidTheme.File = this.m_owner ;
                }
            }

            internal void SetThemes(ATBTheme[] aTBTheme)
            {
                if (aTBTheme?.Length > 0)
                {
                    this.m_themes.Clear();
                    this.m_themes.AddRange(aTBTheme);
                    this.m_primaryTheme = aTBTheme[0];
                    this.m_owner.m_CurrentTheme = this.m_primaryTheme;
                }
            }
        }


        private ATBTheme m_CurrentTheme;

        public ATBTheme CurrentTheme
        {
            get { return m_CurrentTheme; }
            set
            {
                if (m_CurrentTheme != value)
                {
                    m_CurrentTheme = value;
                    OnCurrentThemeChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler CurrentThemeChanged;

        protected virtual void OnCurrentThemeChanged(EventArgs e)
        {
                CurrentThemeChanged?.Invoke(this, e);         
        }


        public ATBThemeFile()
        {
            this.m_themes = new AndroiThemeCollection(this);
            this.m_CurrentTheme = this.m_themes[0];
        }
        public void Save(string filename)
        {
            StreamWriter sw = new StreamWriter (File.Create(filename), Encoding.UTF8 );
            XmlWriterSettings v_setting = new XmlWriterSettings();
            v_setting.Indent = true;
            v_setting.Encoding = Encoding.UTF8;
            v_setting.OmitXmlDeclaration = false;

            XmlWriter writer = XmlWriter.Create(sw, v_setting);
            var v_seri = CoreXMLSerializer.Create (writer);
            v_seri.WriteStartElement(AndroidConstant.RESOURCE_TAG);
            if (this.m_themes.Count == 0)
            {
                this.m_themes.Add(new ATBTheme() { 
                    Name = Path.GetFileNameWithoutExtension (filename) 
                });
            }
            if (this.m_CurrentTheme != null)
            {
                this.m_CurrentTheme.Serialize(v_seri);
            }
            else
            {
                foreach (ICoreSerializable item in this.m_themes)
                {
                    item.Serialize(v_seri);
                }
            }
            v_seri.WriteEndElement();
            writer.Flush();
            sw.Flush();
            sw.Close();
        }

        public static ATBThemeFile LoadFile(string filename)
        {
            var c = global::IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("dummy");
            c.LoadString (File.ReadAllText (filename ));
            var t = c.getElementsByTagName(AndroidConstant.RESOURCE_TAG);
            if (t != null) {
                ATBThemeFile e = new ATBThemeFile();
                e.FileName = filename;
                foreach (var item in t)
                {
                    //load style
                    List<ATBTheme> v_lg = new List<ATBTheme> ();
                    foreach (CoreXmlElement style in item.getElementsByTagName(AndroidConstant.STYLE_TAG))
                    {
                        ATBTheme m = new ATBTheme();
                        m.Name = style["name"].RValue();
                        m.Parent = style["parent"].RValue()?.MatchExpression(AndroidConstant.STYLE_VALUE_REGEX)?["name"];//?[0].Groups["name"].Value;
                        m.DeclarationFile = filename;                                                                                                //g.DefaultPlateForm

                        //load properties
                        if (style.HasChild)
                        {
                            foreach (var ch in style.getElementsByTagName("item"))
                            {
                                string k = ch["name"]?.Value.ToString().MatchExpression(AndroidConstant.ATTR_VALUE_REGEX)?["name"];
                                if (k != null)
                                m.SetValue(k, ch.Value);
                            }
                        }
                        v_lg.Add (m);
                    }
                    if (v_lg.Count > 0)

                   {
                        e.m_themes.SetThemes(v_lg.ToArray());
                    }
                }
                return e;
            }
            return null;

        }

     
        private string m_FileName;

        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                    OnFileNameChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler FileNameChanged;

        protected virtual void OnFileNameChanged(EventArgs e)
        {
            FileNameChanged?.Invoke(this, e);
        }



        public IEnumerable<ATBTheme> Themes { get {
            return this.m_themes;
        } }

        internal void Clear()
        {
            this.m_themes.Clear();
        }
     
    }
}
