using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo
{
    public static class AVISequenceExtension
    {
        /// <summary>
        /// check in is in interval
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static bool IsInInterval(this TimeSpan timeSpan, TimeSpan from, TimeSpan to)
        {
            TimeSpan f = from;
            TimeSpan c = to;
            double m = timeSpan.TotalMilliseconds;
            bool r = (m >= f.TotalMilliseconds) &&
                 (m <= c.TotalMilliseconds);
            return r;
        }
    }
}
