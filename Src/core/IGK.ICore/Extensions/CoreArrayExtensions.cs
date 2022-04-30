using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Extensions
{
    public static class CoreArrayExtensions
    {
        public static  T[] Slice<T>(this T[] array, int start, int length) {
            var tab = new T[length];
            Array.Copy (array , start, tab, 0, length);
            return tab;

        }
    }
}
