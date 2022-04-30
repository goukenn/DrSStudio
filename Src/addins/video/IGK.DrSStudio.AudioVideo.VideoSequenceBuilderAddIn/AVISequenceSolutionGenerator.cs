using IGK.DrSStudio.AudioVideo.Xml;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo
{
    using IGK.ICore;
    using IGK.ICore.WinCore;
#pragma warning disable 1998 //await warning disable
    public class AVISequenceSolutionGenerator
    {
        public static AVISequenceProject Generate(string name, string outDir)
        {
            AVISequenceProject p = new AVISequenceProject();
            p.OutDir = outDir;
            p.Name = name;

            PathUtils.CreateDir(Path.Combine(outDir, "Text"));//text raw
            PathUtils.CreateDir(Path.Combine(outDir, "Raw"));//video raw
            PathUtils.CreateDir(Path.Combine(outDir, "Res"));//Resources. document and layout
            PathUtils.CreateDir(Path.Combine(outDir, "Src"));//where to store animation source script. cs script file
            PathUtils.CreateDir(Path.Combine(outDir, "out"));//where to store the output
            return p;
        }

        public static bool Build(AVISequenceProject project)
        {
            return false;
        }

        public static void Build(IAVISequenceProject project)
        {
#if DEMO
            Core2DDrawingLayerDocument doc = 
                CoreDecoder.Instance.GetDocuments(File.ReadAllBytes(CoreConstant.DEBUG_TEMP_FOLDER+"\\out.gkds")).ConvertTo<Core2DDrawingLayerDocument>()
                .GetCoreValue<Core2DDrawingLayerDocument>(0);
                
            //    new Core2DDrawingLayerDocument(720,480);
            //var txt = new TextElement();
            //txt.Text = "AVISequece Video";

            //txt.Font.CopyDefinition("FontName:Tahoma;Size:12pt");
            //txt.FillBrush.SetSolidColor(Colorf.White);
            //txt.StrokeBrush.SetSolidColor(Colorf.Transparent);
            //txt.RenderMode = enuTextElementMode.Text;
            //txt.Bounds = new Rectanglef(0, 0, 720, 480);
            ////txt.SetAttachedProperty(Core2DMarginDependency.Left.Name, "0px");
            ////txt.SetAttachedProperty(Core2DMarginDependency.Top.Name, "0px");

            //doc.CurrentLayer.Elements.Add(txt);
            //doc.CurrentLayer.Elements.Add(txt);

            AudioVideoUtility.BuildVideo(
                Path.Combine(project.OutFolder, project.Name + ".avi"), new ICore2DDrawingDocument[]{
                    doc
            }, new AudioVideoAnimationContext() { 
                Duration = new TimeSpan (0,0,5),
                FramePerSecond =30,//for NTSC
                Target = null                 
            });
            System.Windows.Forms.MessageBox.Show("done");

#else 
            AVISequenceBuilder b = new AVISequenceBuilder();
             //b.BuildProject(project);
            var r = b.BuildProject(project);
            System.Windows.Forms.MessageBox.Show("Wait done " + r);

#endif


        }

        public static async Task<bool> BuildAsync(IAVISequenceProject project)
        {
            //await Task.Delay(10000);
            //return false;

            AVISequenceBuilder b = new AVISequenceBuilder();
            //b.BuildProject(project);
            var r = await Task.Run<bool>(() => { return b.BuildProject(project); });

            System.Windows.Forms.MessageBox.Show("Wait done " + r);
            return r;
        }
    }
}
