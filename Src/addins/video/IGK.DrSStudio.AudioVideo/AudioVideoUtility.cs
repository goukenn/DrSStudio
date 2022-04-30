using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo
{
    public static class AudioVideoUtility
    {
        public static bool BuildVideo(string filename, ICore2DDrawingDocument[] documents, string contextType)
        {
            if ((documents == null) || (documents.Length == 0))
                return false;
            var fmanager = AVIApi.AVI.AVIFileManager.Instance;
            bool v_result = false;
            for (int i = 0; i < documents.Length; i++)
            {
                var doc = documents[i];
                AudioVideoAnimationContext c = doc.Extensions[contextType] as AudioVideoAnimationContext;
                if (c == null)
                {
                    c = AudioVideoAnimationContext.CreateNewContext(TimeSpan.FromSeconds(2), 30);
                }
                string f = Path.GetTempFileName();
                string t = f + ".avi";
                File.Move(f, t);
                f = t;
                using (AudioVideoProgressCallBack r = new AudioVideoProgressCallBack(doc))
                {
                    if (fmanager.BuildVideo(f, c.Duration, c.FramePerSecond, r.Update, null))
                    {
                        try
                        {
                            File.Copy(f, filename, true);
                            v_result = true;
                        }
                        catch
                        {
                            CoreLog.WriteDebug("[AudioVideo] - Can't copy file to destination");
                        }
                    }
                }
                File.Delete(f);
            }
            return v_result;
        }
        public static bool BuildAnimation(string filename, ICore2DDrawingDocument[] documents)
        {
            return BuildVideo(filename, documents, "AnimationContext");
        }

        public static bool BuildVideo(string outfile, ICore2DDrawingDocument[] documents, 
            AudioVideoAnimationContext context)
        {
  
            if ((documents == null) || (documents.Length == 0) || (context == null))
                return false;
            var fmanager = AVIApi.AVI.AVIFileManager.Instance;
           bool v_result = false;

           AudioVideoAnimationContext c = context;   
            for (int i = 0; i < documents.Length; i++)
            {
                var doc = documents[i];           
                string f = Path.GetTempFileName();
                string t = f + ".avi";
                File.Move(f, t);
                f = t;
                using (AudioVideoProgressCallBack r = new AudioVideoProgressCallBack(doc))
                {
                    if (fmanager.BuildVideo(f, c.Duration, c.FramePerSecond, r.Update, null))
                    {
                        try
                        {
                            CoreLog.WriteDebug("[AudioVideo] - Copying to output folder");
                            File.Copy(f, outfile, true);
                            v_result = true;
                        }
                        catch
                        {
                            CoreLog.WriteDebug("[AudioVideo] - Can't copy file to destination");
                        }
                    }
                }
                File.Delete(f);
            }
            return v_result;
        }
    }
}
