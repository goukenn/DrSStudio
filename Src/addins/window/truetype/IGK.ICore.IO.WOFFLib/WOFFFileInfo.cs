using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.IO
{
    /// <summary>
    /// information data needed to pass to WOFFFile.Save Method
    /// </summary>
    /// for support unicode euro character please make a range that contain : 0x20AC
    public class WOFFFileInfo
    {
        public static readonly DateTime StartTime = new DateTime(1904, 1, 1);

        private WOFFFileNameCollections m_names;
        private int m_unitPerEm;
        private WOFFGlyfCollections m_glyfs;
        private WOFFFileCharMapRangeCollections m_charRanges;
        private List<int> m_glyfoffsets;
        private ushort m_numOfMetrics;
        private WOFFFilePanose m_panose;

        public WOFFFileNameCollections Names => m_names;
        public WOFFBoxSize BoxSize { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public WOFFGlyfCollections Glyfs => m_glyfs;
        public byte EuroCharacterIndex { get; set; }
        public byte AppleLogoCharacterIndex { get; set; }

        //public ushort FirstCharId{get;set;}
        //public ushort LastCharId { get; set; }
        //public ushort RangeShift { get; private set; }
        public int UnitPerEm {
            get { return this.m_unitPerEm; }
            set {
                //if ((value>=16) && (value < 16384)){
                    if ((value >= 64) && (value < 16384))
                    {
                        m_unitPerEm = value;
                }
            }
        }

        internal List<int> GlyfOffsets => m_glyfoffsets;
        public WOFFFileCharMapRangeCollections CharRanges => m_charRanges;
        public short Ascender { get; set; }
        public short Descender { get;  set; }
        public bool FontProportional { get;  set; }
        public int Weight { get;  set; }
        public enuWOFFWidth WidthType { get; set; }
        public enuWOFFLocaIndexFormat LocaIndexFormat { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public ushort NumberOfHMetrics { get=>m_numOfMetrics;
            set {
                if (value >= 1)
                {
                    m_numOfMetrics = value;
                }
                else {
                    throw new ArgumentOutOfRangeException(nameof(NumberOfHMetrics));
                }
            }
        }
        /// <summary>
        /// global advanced width
        /// </summary>
        public int HGlobalAdvanceWidth { get;  set; }
        /// <summary>
        /// global leftSideBearing
        /// </summary>
        public int HGlobalLSB { get;  set; }

        public WOFFFilePanose Panose => m_panose;

        public int StrikeOutPosition { get; internal set; }
        public Vector2i SuperSize { get; internal set; }
        public Vector2i SubSize { get; internal set; }

        ///<summary>
        ///public .ctr
        ///</summary>
        public WOFFFileInfo()
        {
            m_panose = new WOFFFilePanose();
            m_glyfoffsets = new List<int>();
            m_names = new WOFFFileNameCollections(this);
            m_glyfs = new WOFFGlyfCollections(this);
            CreatedAt =  DateTime.Now;
            this.m_charRanges = new WOFFFileCharMapRangeCollections(this);
            this.Ascender = 100; //yMax=100
            this.Descender = 0;//Descender must be  >= head.yMin
            this.Weight = 400;
            this.WidthType = enuWOFFWidth.Normal;
            this.UnitPerEm = 100;// 64-16384;
            this.FontProportional  =false;
            this.m_numOfMetrics = 1;//mono space font

            //setup the char range
            //'a' = x0061
            //this.FirstCharId = 0x0001;
            //this.LastCharId = 0x0001;
            //this.RangeShift = (ushort)(0xFFFF-this.FirstCharId+1);

#if DEBUG && TEST
            unchecked
            {
                this.CharRanges.Add(0x0001, 0x0002, (ushort)(0xFFFF - 0x0001 + 2));
                this.CharRanges.Add(0x0031, 0x0032, (ushort)(0xFFFF - 0x0031 + 2));
                this.CharRanges.Add(0x0041, 0x0044, (ushort)(0xFFFF - 0x0041 + 2));
            }
#endif

        }

        /// <summary>
        /// exosed panose data
        /// https://developer.apple.com/fonts/TrueType-Reference-Manual/RM06/Chap6OS2.html
        /// </summary>
        public class WOFFFilePanose {
            ///<summary>
            ///public .ctr
            ///</summary>
            public WOFFFilePanose()
            {
                this.Proportion = enuWOFFPanoseProportion.Monospace;
                this.FamilyType = enuWOFFFileFamilyType.None;
            }
            public enuWOFFFileFamilyType FamilyType{get;set;}
            public enuWOFFPanoseProportion Proportion { get; set; }
        }
        public class WOFFFileCharMapRangeCollections : IEnumerable {

            List<WOFFFileCharMapRange> m_list;
            private WOFFFileInfo wOFFFileInfo;
            private ushort m_max;
            private ushort m_min;

            public ushort Min => m_min;
            public ushort Max => m_max;


            public enuWOFFFileUnicodeRange1 Range1 { get; set; }
            public enuWOFFFileUnicodeRange2 Range2 { get; set; }
            public enuWOFFFileUnicodeRange3 Range3 { get; set; }
            public enuWOFFFileUnicodeRange4 Range4 { get; set; }


            public WOFFFileCharMapRange this[int index]{
                get {
                    return this.m_list[index];
                }
            }
            public WOFFFileCharMapRangeCollections(WOFFFileInfo wOFFFileInfo)
            {
                this.wOFFFileInfo = wOFFFileInfo;
                this.m_list = new List<WOFFFileCharMapRange>();
                this.AutoIndex = true;
            }

            public int Count => m_list.Count;

            /// <summary>
            /// get of set if this collection use automatic index for 
            /// </summary>
            public bool AutoIndex { get; set; }
            /// <summary>
            /// if AutoIndex == false then the Indexes will be used to set extra data. Note that you must macth shift range size
            /// </summary>
            public ushort[] Indexes { get; set; }

            public void Add(ushort min, ushort max, ushort shiftoffset, ushort offRange = 0) {
                if (!(max>=min))
                    return;

                if (this.m_list.Count == 0)
                {
                    this.m_min = min;
                    this.m_max = max;

                }
                else {
                    if ((m_max == min-1)&&(this.m_list[this.m_list.Count - 1].ShiftRange == shiftoffset))
                    {
                        //var s = this.m_list[this.m_list.Count - 1];
                        //s.MaxChar = max;

                        //this.m_list[this.m_list.Count - 1] = s;

                    }

                }
                
                this.m_list.Add(new WOFFFileCharMapRange() {
                    MaxChar = max,
                    MinChar = min,
                    ShiftRange = shiftoffset ,
                    OffsetRange = offRange
                });
                m_min = Math.Min(m_min, min);
                m_max= Math.Max(m_max, max);
            }
            public void Clear() {
                this.m_list.Clear();
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }


            /// <summary>
            /// get the first character
            /// </summary>
            /// <returns></returns>
            internal ushort GetFirstChar()
            {
                ushort c = 0xffff;//init with last  char
                var i = 0;
                foreach (var item in this.m_list)
                {
                    
                    if( ((item.MinChar == 0) && (item.MaxChar != 0)) || (item.MinChar>0))
                    {
                        c = (ushort)(item.MinChar);// + item.ShiftRange);
                        break;
                    }
                    i++;
                }

                return c;
            }
        }
        public class WOFFFileNameCollections
        {
            private WOFFFileInfo wOFFFileInfo;
            private int m_count;
            private Dictionary<enuWOFFFileLanguage, Dictionary<enuWOFFFileNameID, string >> m_values;

            public int Count => m_count;
            public WOFFFileNameCollections(WOFFFileInfo wOFFFileInfo)
            {
                this.wOFFFileInfo = wOFFFileInfo;
                m_values = new Dictionary<enuWOFFFileLanguage, Dictionary<enuWOFFFileNameID, string>>();
            }
            public void Clear() {
                this.m_values.Clear();
                m_count = 0;
            }
            public void Add(enuWOFFFileLanguage langid, enuWOFFFileNameID nameid, string value) {

                if (!m_values.ContainsKey(langid))
                    m_values[langid] = new Dictionary<enuWOFFFileNameID, string>();

                if (m_values[langid].ContainsKey(nameid))
                    m_values[langid][nameid] = value;
                else{
                    m_values[langid].Add(nameid, value);
                    m_count++;
                }
            }
            public object[] GetRecords() {
                List<object> c = new List<object>();
                foreach (var item in this.m_values)
                {
                    foreach (var item1 in item.Value)
                    {
                        c.Add(new object[] {
                            (short)item.Key,
                            (short)item1.Key,
                            item1.Value
                        });
                    }
                }
                //c.Sort(this);
                return c.ToArray();
            }

            //public int Compare(object x, object y)
            //{
            //    object[] t1 = (object[])x;
            //    object[] t2 = (object[])y;
            //    short xs = (short)t1[1];
            //    short xy = (short)t2[1];
            //    return xy.CompareTo(xs);// ((int)t1[1]).CompareTo((int)t2[1]);
            //}
        }


        
        internal long GetTimeFromStart(DateTime time)
        {
            DateTime t = time;
            var g = t - StartTime;
            return (long)g.TotalSeconds;                        
        }
        /// <summary>
        /// h metrix
        /// </summary>
        public class HMetrix
        {
            public ushort AdvanceWidth { get; set; }
            public short LSB { get; set; }
        }
        /// <summary>
        /// represent a horizontal metix collection
        /// </summary>
        public class HMetrixCollection {
            private Dictionary<int, HMetrix> m_metrics;
            public void Clear() {
                m_metrics.Clear();
            }
            public int Count => m_metrics.Count;
            ///<summary>
            ///public .ctr
            ///</summary>
            public HMetrixCollection()
            {
                this.m_metrics = new Dictionary<int, HMetrix>();
            }
            public bool Contains(int index) {
                return this.m_metrics.ContainsKey(index);
            }
            public HMetrix GetMetrix(int index) {
                return this.m_metrics[index];
            }
            public void Add(int index, HMetrix e) {
                this.m_metrics.Add(index, e);
            }
        }
        public class WOFFGlyfCollections
        {
            private WOFFFileInfo wOFFFileInfo;
            private List<WOFFFileGlyfData> m_glysData;
            private List<WOFFGlyHMetrics> m_gmetrics;

            private bool _dim;
            private int m_minx;
            private int m_miny;
            private int m_maxx;
            private int m_maxy;
            private ushort m_maxContour;
            private HMetrixCollection m_hMetrix;

            public int Count => m_glysData.Count;
            public int MinX => m_minx;
            public int MinY => m_miny;
            public int MaxX => m_maxx;
            public int MaxY => m_maxy;
            public HMetrixCollection HMetrix => m_hMetrix;
            /// <summary>
            /// get the maximum point in the graphics
            /// </summary>
            public ushort MaxPoints {
                get {
                    int v_max = 0;
                    foreach (var m in m_glysData)
                    {
                        v_max = Math.Max(v_max, m.Points.Length);
                    }
                    return (ushort)v_max;
                }
            }
            /// <summary>
            /// get the maximum point in the graphics
            /// </summary>
            public ushort MaxContours {
                get
                {
                    return m_maxContour;
                }
                internal set{
                    m_maxContour = value;
                 }
            }
            internal WOFFFileGlyfData this[int index] => m_glysData[index];

            public WOFFGlyfCollections(WOFFFileInfo wOFFFileInfo)
            {
                this.wOFFFileInfo = wOFFFileInfo;
                m_glysData = new List<WOFFFileGlyfData>();
                m_gmetrics = new List<WOFFGlyHMetrics>();
                m_hMetrix = new HMetrixCollection();
            }

            public void Add(int unicode, Vector2f[] point, byte[] type, bool toQuadric=true)
            {

                if ((point == null) || (point.Length != type?.Length))
                    return;
                if (toQuadric)
                {
                    var old = point;
                    WOFFFileGlyfData.CurveToQuadrics(ref point, ref type);
                }

                WOFFFileGlyfData g = new WOFFFileGlyfData()
                {
                    Unicode = unicode,
                    Points = point,
                    Types = type
                };
                this.m_glysData.Add(g);
                
            }
            public void Add(int unicode, Vector2f[] point, byte[] type, int width, int height)
            {
                WOFFFileGlyfData g = new WOFFFileGlyfData()
                {
                    Unicode = unicode,
                    Points = point,
                    Types = type,
                    Size = new Vector2i (width, height)
                };
                this.m_glysData.Add(g);

            }

            internal void UpdateDimension(int minx, int miny, int w, int h)
            {
                if (_dim)
                {
                    m_minx = Math.Min(minx, m_minx);
                    m_miny = Math.Min(miny, m_miny);
                    m_maxx = Math.Max(minx+w, m_maxx);
                    m_maxy = Math.Max(miny+h, m_maxy);
                }
                else {
                    m_minx = minx;
                    m_miny = miny;
                    m_maxx = minx+w;
                    m_maxy = miny + h;
                    _dim = true;
                }

            }

            //...metrics functions
            public WOFFGlyHMetrics GetHGMetrics(int index)
            {
                return m_gmetrics[index];
            }
            public void RegGlobalMetrics(WOFFGlyHMetrics m) {
                m_gmetrics.Add(m);
            }
            public void ClearGlobalMetrics(WOFFGlyHMetrics m)
            {
                m_gmetrics.Clear();
            }
        }
    }
}
