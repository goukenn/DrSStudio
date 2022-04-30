using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.ICore
{
   public static class CoreRegexUtils
    {
        public static bool IsInteger(string g) {
            return Regex.IsMatch(g, "\\d+");
        }
    }
}
