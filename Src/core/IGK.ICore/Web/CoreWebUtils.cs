using IGK.ICore.IO;
using IGK.ICore.Web.WinUI;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.ICore.Web
{
    /// <summary>
    /// represent a web utility class 
    /// </summary>
    public static class CoreWebUtils
    {
        public const string IMG_URI = "res";


        public static string GetImgUri(string name)
        {
            string g = PathUtils.GetPath("%startup%/sdk/img/" + name + ".png");
            if (File.Exists(g))
                return string.Format("{0}://{1}", Uri.UriSchemeFile, g.Replace("\\", "/"));
            return null;
        }

        public static string[] GetInnerHtmlText(string tag, string text)
        {
            var m1 = Regex.Matches(text, string.Format(@"\<{0}\s*.*\>", tag), RegexOptions.IgnoreCase);
            var m2 = Regex.Matches(text, string.Format(@"\</{0}\>", tag), RegexOptions.IgnoreCase);
            List<string> info = new List<string>();
            if (m1.Count == m2.Count)
            { //well formed html 
                int v_sindex = 0;
                for (int i = 0; i < m1.Count; i++)
                {
                    v_sindex = (m1[i].Index + m1[i].Length);
                    info.Add(text.Substring(v_sindex,
                        m2[i].Index - v_sindex));
                }
            }
            else
            {
                //no 
                throw new Exception("no corresponding ");
            }
            return info.ToArray();

        }

       

        public static string TreatHtmlText(string tag, string text, CoreStringEvaluationCallback m)
        {
            if (m == null)
                return text;
            var m1 = Regex.Matches(text, string.Format(@"\<{0}\s*.*\>", tag), RegexOptions.IgnoreCase);
            var m2 = Regex.Matches(text, string.Format(@"\</{0}\>", tag), RegexOptions.IgnoreCase);
            List<string> info = new List<string>();
            int v_index = 0;
            string output = string.Empty;
            if (m1.Count == m2.Count)
            { //well formed html 
                int v_sindex = 0;
                int v_length = 0;
                for (int i = 0; i < m1.Count; i++)
                {

                    v_sindex = (m1[i].Index + m1[i].Length);
                    v_length = m2[i].Index - v_sindex;
                    if (v_length > 0)
                    {
                        output += text.Substring(v_index, v_sindex - v_index);

                        string g = m(text.Substring(v_sindex, v_length));
                        output += g + m2[i].Value;
                        v_index = (m2[i].Index + m2[i].Length);
                    }
                    else break;

                }
            }
            else
            {
                //no 
                throw new Exception("no corresponding ");
            }
            if (v_index < text.Length)
            {
                output += text.Substring(v_index);
            }
            return output;
        }

        /// <summary>
        /// evaluate css string expression
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="evaluable"></param>
        /// <returns></returns>
        /// %=Name%
        public static string EvalCssWebStringExpression(string expression, object evaluable)
        {
            if (string.IsNullOrEmpty(expression))
                return string.Empty;

            string reg = @"\%\=(?<name>([a-z]|[_]+[a-z0-9])([a-z0-9_]*))\%";
            Regex rg = new Regex(reg, RegexOptions.IgnoreCase| RegexOptions.Multiline);
            string n = string.Empty;
            Type c = evaluable.GetType();
            StringBuilder out_expression = new StringBuilder(expression);//.Clone() as string;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (Match match in rg.Matches(expression)) //Regex.Matches(expression, reg))
            {
                n = match.Groups["name"].Value;
                if (dic.ContainsKey(n)) continue;
                dic[n] = match.Value;
            }

            foreach (var item in dic)
            {
                n = item.Key;
                object o = c.GetProperty(n, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetProperty
                      )?.GetValue(evaluable);
                if (o != null)
                {
                    out_expression = out_expression.Replace(item.Value, o.ToString());
                }
            }


            return out_expression.ToString();

        }

        public static string CoreEvalWebStringExpression(this string value) {
            return EvalWebStringExpression( value);
        }

        /// <summary>
        /// eval string expression to match target
        /// </summary>
        /// <param name="system"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EvalWebStringExpression( this string value, object row=null)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            string v_out = igk_html_databinding_treatresponse(value, row);
            return v_out;
        }
        ///<summary>treat string data binding response before eval</summary>
        ///<param name="rep">reponse</param>
        ///<param name="ctrl">controller</param>
        private static string igk_html_databinding_treatresponse(string rep, object row)
        {
            //\s*(\w+)\s*:\s*([a-zA-Z0-9_]+)\s*\
            //regex for : [def:value]
            string reg = @"\[\s*(?<name>\w+)\s*:(?<value>([^\]:\[])+)]";
            string reg_comment = @"(?<comment>(\<\!--(?<value>(.)+)--\>))";
            Dictionary<string, string> comment = new Dictionary<string, string>();
            int i = 0;

            string type = string.Empty;
            string value = string.Empty;
            string key = string.Empty;
            foreach (Match match in Regex.Matches(rep, reg_comment, RegexOptions.IgnoreCase))
            {

                value = match.Groups["comment"].Value;
                key = "__COMMENT__" + i + "__";
                rep = rep.Replace(value, key);//c.Replace(value, ,rep);
                comment["__COMMENT__" + i + "__"] = value;
                i++;
            }
            i = 0;
            string v = rep;
            string v_m = string.Empty;
            foreach (Match match in Regex.Matches(rep, reg))
            {
                v_m = match.Value;
                type = match.Groups["name"].Value;
                value = match.Groups["value"].Value;
                switch (type.ToLower())
                {
                    case "curi": //replace  with controller base uri
                        //if (ctrl)
                        //{				
                        //    v = str_replace(v_m, 
                        //        igk_io_baseuri()+"/"+ctrl.getUri(value),v);
                        //}
                        //else
                        //v = str_replace(v_m, 
                        //    igk_io_baseuri()+"/"+igk_html_uri(value),v);
                        break;
                    case IMG_URI:
                        v = v.Replace(v_m, GetImgUri(value));
                        break;
                    case "uri"://replace with global uri
                        //v = str_replace(v_m, 
                        //    igk_io_baseuri()+"/"+igk_html_uri(value),v);
                        break;
                    case "guri": //get uri
                        //uri = igk_io_baseuri()+"/"+igk_html_uri(value);
                        //result = igk_io_baseuri()+"/"+igk_getctrl(IGK_SYSACTION_CTRL).getUri(value);
                        //v = str_replace(v_m, result,v);
                        break;
                    case "lang"://do language replacement
                        v = v.Replace(v_m, value.R());
                        break;
                    case "conf": //do configuration zone replacement"
                        //v = str_replace(v_m, igk_sys_getconfig(value), v);
                        break;
                    case "imeth":
                    case "func":
                        if (row != null)
                        {
                            var m= row.GetType().GetMethod(value);
                            if (m != null)
                            {
                                string g = m.Invoke(row, new object[] { }) as string;
                                v = v.Replace(v_m, g);
                            }
                            else {
                                v = v.Replace (v_m, $"[/!\\ Method {value} not found !!!]");
                            }
                        }
                        break;
                    case "funcv":

                        //rm = igk_html_eval_script(value, ctrl, row);
                        //m = eval("return ".rm.";");
                        //obj = igk_html_databinding_getobjForScripting(ctrl,__FUNCTION__);				
                        //obj.args["param1"] = m;
                        //v = str_replace(v_m, "\param1", v);		

                        break;
                    case "funcn":
                    case "funce":
                        //tab = explode(":", value);
                        //if (igk_count(tab)>1)
                        //{
                        //    obj = igk_html_databinding_getobjForScripting(ctrl,__FUNCTION__);										

                        //    name = tab[0];
                        //    value = substr(value, IGKString::IndexOf(value, ":")+1);						

                        //    rm = igk_html_eval_script(value, ctrl, row);
                        //    m = eval("return ".rm.";");

                        //    obj.args[name] = m;
                        //    if (strtolower(type)=="funcn")
                        //        v = str_replace(v_m, "\{name}", v);					
                        //    else{
                        //        v = str_replace(v_m, "", v);					
                        //    }
                        //}				
                        break;
                    case "exp":                       
                        break;
                    default:
                        //no action
                        break;
                }
            }


            //restore comment
            foreach (var item in comment)
            {
                v = v.Replace(item.Key, item.Value);
            }
            //foreach($comment as $k=>$s)
            //{
            //    $v = str_replace($k, $s, $v);
            //}	
            return v;
        }

        /// <summary>
        /// post data with ascii encoder
        /// </summary>
        /// <param name="url"></param>
        /// <param name="urlEncodedData"></param>
        /// <returns></returns>
        public static  string PostData(string url, string urlEncodedData)
        {
            HttpWebRequest request = null;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                if (!string.IsNullOrEmpty(urlEncodedData))
                {
                    byte[] data = ASCIIEncoding.ASCII.GetBytes(urlEncodedData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    using (BinaryWriter sm = new BinaryWriter(request.GetRequestStream()))
                    {
                        sm.Write(data, 0, data.Length);
                    }
                }
                var rep = request.GetResponse();
                var s = new StreamReader(rep.GetResponseStream()).ReadToEnd();
                return s;
            }
           
            catch (WebException ex) {
                return "connection failed : "+ex.Message;
            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug(CoreConstant.DEBUG_TAG, $"Posting data error : {ex.Message}");
            }
            finally
            {

            }
            return string.Empty;
        }

        public static string SendRestData(string method, string url, string urlEncodedData, string contentType="application/x-www-form-urlencoded") {
            HttpWebRequest request = null;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = true;
                //request.AllowReadStreamBuffering = true;
                request.AllowWriteStreamBuffering = true;
                request.ServicePoint.Expect100Continue = false;
                if (!string.IsNullOrEmpty(urlEncodedData))
                {
                    byte[] data = ASCIIEncoding.ASCII.GetBytes(urlEncodedData);
                    request.Method = method;
                    //request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentType = contentType; 
                    request.ContentLength = data.Length;
                  

                    var stream = request.GetRequestStream();

                    using (BinaryWriter sm = new BinaryWriter(stream))
                    {
                        sm.Write(data, 0, data.Length);
                    }
                    stream.Close();
                }
                var rep = request.GetResponse();
                var s = new StreamReader(rep.GetResponseStream()).ReadToEnd();
                
                return s;
            
            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug(CoreConstant.DEBUG_TAG, $"{method} data error : {ex.Message}");
            }
            finally
            {

            }
            return string.Empty;
        }

        public static string PostDataAsync(string url, string urlEncodedData)
        {
            HttpWebRequest request = null;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                if (!string.IsNullOrEmpty(urlEncodedData))
                {
                    byte[] data = ASCIIEncoding.ASCII.GetBytes(urlEncodedData);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;

                    using (BinaryWriter sm = new BinaryWriter(request.GetRequestStream()))
                    {
                        sm.Write(data, 0, data.Length);
                    }
                }
                var rep = request.GetResponseAsync ();//.GetResponse();
                rep.Start();

                string s = null;//new StreamReader(rep<.GetResponseStream()).ReadToEnd();
                return s;
            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug("[IGK]- Error : " + ex.Message);
            }
            finally
            {

            }
            return string.Empty;
        }

        public static ICoreWebDialogForm BuildWebDialog(
            this ICoreSystemWorkbench bench,
            ICoreWebDialogProvider provider,
            string title,
            string document,
            object row = null)
        {
            CoreXmlWebDocument v_doc = CoreXmlWebDocument.CreateICoreDocument();
            v_doc.ForWebBrowserDocument = true;
            v_doc.Body.LoadString(
                CoreWebUtils.EvalWebStringExpression(                
                document,
                row));
            provider.Document = v_doc;

            var d = bench.CreateWebBrowserDialog(provider);
            d.Title = title;
            //d.WebControl.AttachDocument(v_doc);
            
            return d;
        }

        public static string GetFileContent(string filename)
        {
            if (filename.StartsWith("file:///"))
                filename = filename.Substring(8);
            if (File.Exists(filename))
            {
                return File.ReadAllText(filename);
            }
            return null;
        }
    }
}