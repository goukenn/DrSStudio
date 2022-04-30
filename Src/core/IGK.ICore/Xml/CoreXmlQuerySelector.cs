using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IGK.ICore.Xml
{
    /// <summary>
    /// represent a query selector result
    /// </summary>
    public class CoreXmlQuerySelector
    {
        private List<CoreXmlElement> m_result;

        ///<summary>
        ///public .ctr
        ///</summary>
        private CoreXmlQuerySelector()
        {
            this.m_result = new List<CoreXmlElement>();
        }

        /// <summary>
        /// get the number of element in 
        /// </summary>
        public int Count => m_result.Count;

        /// <summary>
        /// invoke listener on all item of this 
        /// </summary>
        /// <param name="listener"></param>
        public void EachAll(CoreXmlQueryListener listener) {
            foreach (var i in this.m_result) {
                listener(i);
            }
        }

        private delegate void FilterCall(string k, FilterCallCallback i);
        private delegate void FilterCallCallback(CoreXmlElement i, string k);


        public static CoreXmlQuerySelector Select(CoreXmlElement e, string pattern)
        {

            var tab = pattern.Split(new string[]{ ","}, StringSplitOptions.RemoveEmptyEntries);
            //if (tab.Length > 1) {

            //    CoreXmlQuerySelector jm = new CoreXmlQuerySelector();
            //    for (int i = 0; i < tab.Length; i++)
            //    {
            //        var cf = Select(e, tab[i]);
            //        if (cf.m_result.Count > 0)
            //        jm.m_result.AddRange (cf.m_result.ToArray());
            //    }
            //    return jm;
            //}

            CoreXmlQuerySelector g = new CoreXmlQuerySelector();
            const string ID_SEARCH = "^#[\\w\\-_]+$";
            const string CLASS_SEARCH = @"^\.[\w_]+[\w0-9_\-]*$";
            const string TAG_SEARCH = @"^[\w_]+[\w0-9_\-]*$";
            List<CoreXmlElement> it = null;
            //CoreMethodInvoker _getAll = ()=> {
            //    Queue<CoreXmlElement> c = new Queue<CoreXmlElement>();
            //    c.Enqueue(e);
            //    CoreXmlElement q = null;
            //    while (c.Count > 0)
            //    {
            //        q = c.Dequeue();
            //        foreach (CoreXmlElement m in q.Childs)
            //        {
            //            it.Add(m);
            //            c.Enqueue(m);
            //        }
            //    }
            //};

            FilterCall _filter_all = (string k, FilterCallCallback callback) => {
                Queue<CoreXmlElement> c = new Queue<CoreXmlElement>();
                c.Enqueue(e);
                CoreXmlElement q = null;
                while (c.Count > 0)
                {
                    q = c.Dequeue();
                    foreach (CoreXmlElement m in q.Childs)
                    {
                        it.Add(m);
                        c.Enqueue(m);
                        callback(m, k);
                    }
                }
            };
            for (var ii = 0; ii < tab.Length; ii++)
            {
                pattern = tab[ii].Trim();
                switch (pattern)
                {
                    case "*"://push all element in a result list
                        if (ii > 0)
                        {
                            throw new Exception("Not valid operation");
                        }
                        Queue<CoreXmlElement> c = new Queue<CoreXmlElement>();
                        c.Enqueue(e);
                        CoreXmlElement q = null;
                        while (c.Count > 0)
                        {
                            q = c.Dequeue();
                            foreach (CoreXmlElement m in q.Childs)
                            {
                                g.m_result.Add(m);
                                c.Enqueue(m);
                            }
                        }
                        break;
                    case ">>"://child only
                        if (ii > 0)
                        {
                            throw new Exception("Not valid operation");
                        }
                        foreach (CoreXmlElement m in e.Childs)
                        {
                            g.m_result.Add(m);
                        }
                        break;
                    default:
                        //pattern condition
                        it = new List<CoreXmlElement>();
                        // _getAll();
                        //string searchreg = $"((?<class>{CLASS_SEARCH})|(?<id>{ID_SEARCH})|(?<tag>{TAG_SEARCH}))";

                        //var v_m = new Regex(searchreg, RegexOptions.IgnoreCase);
                        //var v_o = v_m.Match(pattern);
                        //if (v_o.Success) {

                        //    var t = v_o.Groups[0];
                        //}
                        var r = new Regex(ID_SEARCH, RegexOptions.IgnoreCase).IsMatch(pattern) ? "id" :
                                new Regex(CLASS_SEARCH, RegexOptions.IgnoreCase).IsMatch(pattern) ? "class" :
                                new Regex(TAG_SEARCH, RegexOptions.IgnoreCase).IsMatch(pattern) ? "tag" :
                                null
                            ;
                        string k = string.Empty;
                        FilterCallCallback callback = null;
                        switch (r)
                        {
                            case "id":
                                k = pattern.Substring(1).ToLower();
                                callback = (item, hk) =>
                                {
                                    if (item["id"]?.Value?.ToString().ToLower() == hk)
                                    {
                                        g.m_result.Add(item);
                                    }
                                };
                                break;
                            case "class":
                                k = pattern.Substring(1).ToLower();
                                var match = new Regex("(^|\\s+)" + k + "(\\s+|$)", RegexOptions.IgnoreCase);
                                callback = (item, hk) =>
                                {
                                //if (item["id"]?.Value?.ToString().ToLower() == hk)
                                //{
                                //    g.m_result.Add(item);
                                //}
                                string v = item["class"]?.Value?.ToString().ToLower();
                                    if (v != null && match.IsMatch(v))
                                    {
                                        g.m_result.Add(item);
                                    }
                                };
                                break;
                            case "tag":
                                k = pattern.ToLower();
                                callback = (item, hk) =>
                                {
                                    if (item.TagName.ToLower() == k)
                                    {
                                        g.m_result.Add(item);
                                    }
                                };
                                break;
                        }
                        if (callback != null)
                            _filter_all(k, callback);


                        //var r = new Regex(ID_SEARCH, RegexOptions.IgnoreCase);
                        //if (r.IsMatch(pattern))
                        //{
                        //    string k = pattern.Substring(1).ToLower();
                        //    _filter_all(k, (item,hk) =>
                        //    {
                        //        if (item["id"]?.Value?.ToString().ToLower() == hk)
                        //        {
                        //            g.m_result.Add(item);
                        //        }
                        //    });
                        //    //foreach (var item in it)
                        //    //{
                        //    //    if (item["id"]?.Value?.ToString().ToLower() == k)
                        //    //    {
                        //    //        g.m_result.Add(item);
                        //    //    }
                        //    //}

                        //}
                        //else {
                        //    r = new Regex(CLASS_SEARCH, RegexOptions.IgnoreCase);
                        //    if (r.IsMatch(pattern))
                        //    {
                        //        string k = pattern.Substring(1).ToLower();
                        //        var match = new Regex("(^|\\s+)" + k + "(\\s+|$)", RegexOptions.IgnoreCase);
                        //        //foreach (var item in it)                           //{

                        //        _filter_all(k, (item, hk) =>
                        //        {
                        //                //if (item["id"]?.Value?.ToString().ToLower() == hk)
                        //                //{
                        //                //    g.m_result.Add(item);
                        //                //}
                        //                string v = item["class"]?.Value?.ToString().ToLower();
                        //            if (v != null && match.IsMatch(v))
                        //            {
                        //                g.m_result.Add(item);
                        //            }

                        //        });

                        //        //}
                        //    }
                        //    else {
                        //        //target search

                        //    }
                        //}


                        break;
                }
            }
            return g;
        }

    }
}