using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.AVIApi.AVI
{
    public class AVIFileUtility
    {
        /// <summary>
        /// Convert video file utilser
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        /// <param name="chooseEncoder"></param>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static bool Convert(string sourceFile, string destinationFile, bool chooseEncoder, IntPtr hwnd)
        {
            var s = AVIFile.Open(sourceFile);
            if (s == null)
                return false;
            bool result = false;
            using (var vid = s.GetVideoStream())
            {
                var svid = vid.Compress(hwnd, enuDialogFlag.All, chooseEncoder, null);
                if (svid != null)
                {
                    using (var v_hf = AVIFile.CreateFile(destinationFile))
                    {
                        if ((v_hf != null) && v_hf.AddVideoStream(svid))
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
    }
}
