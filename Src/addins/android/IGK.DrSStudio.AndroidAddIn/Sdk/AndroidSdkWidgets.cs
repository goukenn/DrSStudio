using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IGK.DrSStudio.Android.Sdk
{
    public class AndroidSdkWidgets : AndroidSdkItemBase, IEnumerable
    {
        private AndroidWidget[] m_widget;


        public class AndroidWidget 
        {
            private string m_Name;

            public string Name
            {
                get { return m_Name; }              
            }
        
            public AndroidWidget(string name)
            {
                this.m_Name = name ;
            }
            public override string ToString()
            {
                return this.m_Name;
            }
            private AndroidWidget m_Parent;
            /// <summary>
            /// get the android Widget Parent
            /// </summary>
            public AndroidWidget Parent
            {
                get { return m_Parent; }
                set
                {
                    if (m_Parent != value)
                    {
                        m_Parent = value;
                    }
                }
            }



            public bool IsMatch(string name)
            {
                var s = this.Name.Split('.');
                return s[s.Length - 1] == name;
            }
        }
        private AndroidSdkWidgets(AndroidTargetInfo target)
            : base(target)
        {
            this.Name = "Widgets";
        }
        public static AndroidSdkWidgets LoadWidgets(string filename, AndroidTargetInfo info)
        {
            if (!File.Exists(filename))
                return null;
            string[] widget = File.ReadAllLines(filename);
            Dictionary<string, AndroidWidget> lst = new Dictionary<string, AndroidWidget>();
            for (int i = 0; i < widget.Length; i++)
            {
                string[] t = widget[i].Split(' ');
                if (t.Length > 0)
                {
                    string key = t[0];
                    AndroidWidget mm = null;
                    for (int m = 0; m < t.Length; m++)
                    {
                        key = t[m];
                        if (m == 0)
                        {

                            Match cc = Regex.Match(key, "^(W|P|L)(?<name>(android\\.(.+)))");
                            if ((cc != null) && (cc.Value.Length > 0))
                            {
                                string g = cc.Groups["name"].Value;
                                key = g;
                            }
                        }
                        if ((key == "java.lang.Object"))
                        {
                            continue;
                        }
                        if (lst.ContainsKey(key))
                        {
                            if (m == 0)
                            {
                                mm = lst[key];
                            }
                            else
                            {
                                mm.Parent = lst[key];
                                mm = mm.Parent;
                            }
                        }
                        else
                        {
                            mm = new AndroidWidget(key);
                            lst.Add(key, mm);
                        }
                    }


                }
            }
            var s = new AndroidSdkWidgets(info);
            List<AndroidWidget> dd = new List<AndroidWidget>();
            foreach (var bs in lst.OrderBy(i => i.Key))
            {
                dd.Add(bs.Value);
            }
            s.m_widget = dd.ToArray();
            return s;
        }


        public IEnumerator GetEnumerator()
        {
            return this.m_widget.GetEnumerator();
        }

        public AndroidWidget GetWidget(string name)
        {
            foreach (var item in this.m_widget)
            {
                if (item.IsMatch(name))
                    return item;
            }
            return null;
        }
    } 
      
}
