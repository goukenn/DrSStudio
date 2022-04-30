using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WebGLEngine
{

    /// <summary>
    /// utility class
    /// </summary>
    static class WebGLUtils
    {
        /// <summary>
        /// get shader script
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetShaderTreatScript(string value)
        {
            StringBuilder sb = new StringBuilder();

            //remove comment
            value = Regex.Replace(value.Replace("\r",""), @"\/\/(.)*$", "", RegexOptions.Multiline );
            //explose line
            var tvalue  = value.Split(new string[] { "\n","\t" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tvalue.Length; i++)
            {
                sb.Append(tvalue[i].Trim());
            }
            return sb.ToString();
        }
    }
}
