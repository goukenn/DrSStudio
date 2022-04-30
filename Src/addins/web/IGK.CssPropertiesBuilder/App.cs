
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.PreviewWebDocumentDemo
{
    using IGK.ICore;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinCore.Tools;
    using IGK.ICore.WinUI;
    using System.IO;
    using System.Reflection;

    [CoreApplication("CSSBuilderApp")]
    class App : WinCoreApplication
    {
        public override void Initialize()
        {
            base.Initialize();
            WinCoreService.RegisterIE11WebService();

            var v_asm = GetType().Assembly;
            if (Assembly.GetEntryAssembly() == v_asm) {
                //init entry assembly
                string[] t = v_asm.GetManifestResourceNames();
                //extract zlib to output folder
                string n = string.Empty;
                int subidx = typeof(IGK.CssPropertiesBuilder.Program).Namespace.Length+1;
                string fn = string.Empty;
                for (int i = 0; i < t.Length; i++)
                {
                    n = t[i];
                    
                    if (n.EndsWith(".dll") && (n.Length>subidx)&& 
                        !File.Exists(fn = Path.Combine(Path.GetDirectoryName(v_asm.Location), n.Substring(subidx))))
                       {

                        //fn = Path.Combine(v_asm.Location, n.Substring(GetType().Namespace.Length + 1));


                        byte[] buffer = new byte[4096];
                        int c = 0;
                        using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(
                            fn)))
                        {
                            using (BinaryReader sm = new BinaryReader(v_asm.GetManifestResourceStream(t[i])))
                            {
                                while ((c = sm.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    bw.Write ( buffer, 0, c);
                                }
                            }
                        }
                    }
                }
                
            }
        }

        public override void AddMessageFilter(ICoreMessageFilter messageFilter)
        {
            
        }

        public override void RemoveMessageFilter(ICoreMessageFilter messageFilter)
        {
        }

        public override bool RegisterServerSystem(Func<CoreSystem> __initInstance)
        {
            __initInstance();
            return true;
        }

        public override bool RegisterClientSystem(Func<CoreSystem> __initInstance)
        {
            return false;
        }
    }
}
