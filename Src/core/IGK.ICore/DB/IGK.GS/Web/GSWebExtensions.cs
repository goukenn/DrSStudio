using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.Web
{
    public static class GSWebExtensions
    {
        /// <summary>
        /// get posted data string
        /// </summary>
        /// <param name="tItem"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string ToPostData(this IGSDataTable tItem, IGSValueProvider provider = null)
        {
            var d = GSDBExtensions.ToDictionary(tItem, GSSystem.Instance.DataAdapter);
            StringBuilder sb = new StringBuilder();
            foreach (var item in d)
            {

                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                if (provider != null)
                {
                    sb.Append(string.Format("{0}={1}", item.Key, provider.GetValue(item.Key, item.Value)));
                }
                else
                    sb.Append(string.Format("{0}={1}", item.Key, item.Value));
            }
            return sb.ToString();
        }

        /// <summary>
        /// send item to uri
        /// </summary>
        /// <param name="tItem"></param>
        /// <param name="uri"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string SendUri(this IGSDataTable tItem, string uri, IGSValueProvider provider = null)
        {
            var p = (HttpWebRequest)HttpWebRequest.Create(uri);

            string data = tItem.ToPostData(provider);
            byte[] tdata = Encoding.Default.GetBytes(data);
            p.Method = "POST";

            p.ContentLength = tdata.Length;
            p.ContentType = "application/x-www-form-urlencoded";
            var m = p.GetRequestStream();

            m.Write(tdata, 0, tdata.Length);
            m.Flush();
            var rep = p.GetResponse();
            StreamReader r = new StreamReader(rep.GetResponseStream());
            var pp = r.ReadToEnd();
            r.Close();
            rep.Close();
            return pp;
        }
    }
}
