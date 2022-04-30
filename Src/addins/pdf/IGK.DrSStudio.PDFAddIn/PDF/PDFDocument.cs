using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PDF
{
    using IGK.ICore;
    using System.Reflection;
    using System.Threading;

    /// <summary>
    /// represent a pdf document . used by writer to render pdf document
    /// </summary>
    public class PDFDocument : PDFItemBase 
    {
        private PDFVersion m_PDFVersion;
        private int m_ObjectCount;
        private PDFDictionary m_trailer;
        private int last_crossRefOffset;
        private PDFObject m_catalog;
        private List<PDFObject> m_objects;
        private PDFPageCollector m_Pages;
        private PDFPage  m_CurrentPage;
        private PDFWriter m_pdf;
        private PDFResources m_resources;

        public PDFResources Resources { get { return this.m_resources; } }
        public enuPDFPageUnit Unit {
            get {
                return this.m_pdf.PdfUnit;
            }
        }
        public PDFPageCollector Pages
        {
            get { return m_Pages; }
        }
        public PDFPage  CurrentPage
        {
            get { return m_CurrentPage; }         
        }

        
        public PDFObject  Catalog
        {
            get { return m_catalog; }
        }
        public int ObjectCount
        {
            get { return m_ObjectCount; }
        }
        public PDFObject CreateObject() {
            PDFObject obj = new PDFObject(this);
            this.addObject(obj);
            return obj;
        }
        private void addObject(PDFObject obj)
        {
            if ((obj == null) || this.m_objects.Contains(obj))
                return;
            obj.NumberId = this.ObjectCount + 1;
            this.m_ObjectCount++;
            this.m_objects.Add(obj);
        }
        public PDFVersion Version
        {
            get { return m_PDFVersion; }
            set
            {
                if (m_PDFVersion != value)
                {
                    m_PDFVersion = value;
                }
            }
        }

        public PDFPage AddPage() {
            var p = CreateObject<PDFPage>("Page");
            this.m_CurrentPage = p;
            this.addObject(p);
            this.Pages.Add (p);
            return p;
        }
        /// <summary>
        /// add page 
        /// </summary>
        /// <param name="width">width in pixel</param>
        /// <param name="height">height in pixel</param>
        /// <returns>return the create page</returns>
        public PDFPage AddPage(float width, float height)
        {
            var p = CreateObject<PDFPage>("Page");
            this.m_CurrentPage = p;
            this.addObject(p);
            this.Pages.Add(p);
            var t = new PDFRectangle(0, 0, ((PDFUnit)(width+"px")).GetPoint(),((PDFUnit)(height+"px")) .GetPoint());
            if (!t.Equals(this.Pages.MediaBox))
                p.MediaBox = t;
            return p;
        }

        internal T CreateObject<T>(string name)  where T : PDFObject 
        {
            Type t = Type.GetType (GetType().Namespace + ".PDF" + name);
            if (t != null) { 
                var g = t.GetConstructor ( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,null, new Type[]{typeof(PDFDocument)},null);
                if (g != null)
                {
                  var r =    (T)g.Invoke(new object[] { this });
                  this.addObject(r);
                  return r;
                }
                
            }
            return default (T);

        }
        public PDFDocument()
        {
            this.Version = PDFVersion.VERSION_1_7;
            this.m_trailer = new PDFDictionary();
            this.m_objects = new List<PDFObject>();
            this.m_catalog = CreateObject();
            PDFDictionary dic = new PDFDictionary ();
            m_Pages = CreateObject<PDFPageCollector >("PageCollector");

            this.m_resources = new PDFResources(this);
            this.addObject(this.m_resources);
            this.AddPage();           
            this.m_catalog.Add( new PDFDictionary ()
                .Add (PDFNames.Type , PDFNames.Catalog)
                .Add(PDFNames.Pages, m_Pages));
            this.last_crossRefOffset = 0;
        }

        internal PDFDocument(PDFWriter pdf):this()
        {         
            this.m_pdf = pdf;
        }
        public override string Render()
        {
            var c = PDFUtils.InitCulture();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("%PDF-" + this.Version);
            int[] offsetbj = new int[this.m_objects.Count];
            int r = 0;
            foreach (var item in this.m_objects)
            {
                offsetbj[r] = sb.Length;
                sb.Append(item.Render ());
                r++;
            }
            last_crossRefOffset = sb.Length;
            sb.AppendLine("xref");
            sb.AppendLine(string.Format("{0} {1}", 0, this.ObjectCount+1));
            //Because no index have an id 0
            sb.AppendLine(string.Format("0000000000 65535 f "));
            for (int i = 0; i < this.ObjectCount; i++)
            {
                sb.AppendLine (string.Format ("{0} {1} {2} ",
                    offsetbj[i].ToBase(10, 10),
                    0.ToBase(10,5),
                    "n"
                    ));
            }
            //foreach (var item in offsetObject)
            //{
                
            //}
            this.m_trailer.Add(PDFNames.Size, this.ObjectCount);
            this.m_trailer.Add(PDFNames.Root, this.Catalog);

            sb.AppendLine("trailer");
            sb.Append(this.m_trailer.Render());
            sb.AppendLine("startxref");
            sb.AppendLine (this.last_crossRefOffset.ToString());
            sb.Append("%%EOF");

            PDFUtils.RestoreCulture(c);

            return sb.ToString();
        }

        Dictionary<string, int> m_names;
        internal PDFNameObject CreateName(string baseKey)
        {
            if (m_names == null)
                m_names = new Dictionary<string, int>();
            if (m_names.ContainsKey(baseKey))
            {
                var i = m_names[baseKey] + 1;
                m_names[baseKey] = i;
                return string.Format (baseKey +i);
            }
            m_names.Add(baseKey, 1);
            return string.Format(baseKey +"1");
        }
    }
}
