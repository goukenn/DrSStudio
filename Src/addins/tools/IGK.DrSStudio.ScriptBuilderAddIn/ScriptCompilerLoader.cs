using System;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using IGK.ICore.WinCore;
using IGK.ICore.Drawing2D;
using IGK.ICore;
using System.Text.RegularExpressions;

namespace IGK.DrSStudio.ScriptBuilderAddIn
{
    [Serializable()]
    public class ScriptCompilerLoader        
    {
        private bool m_Success;
        private string m_gAssembly;
        private string[] m_errors;

        /// <summary>
        /// get errors
        /// </summary>
        /// <returns></returns>
        public string[] GetErrors ()
        {
            return m_errors;
        }
        
        /// <summary>
        /// Get if this compilation success
        /// </summary>
        public bool Success
        {
            get { return m_Success; }
        }

        public ScriptCompilerLoader(string filename, params object[] tobj)
        {
            CoreSystem.InitWithEntryAssembly(GetType().Assembly);
            if (File.Exists(filename))
            {
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                string v_script = GetScriptFromFile(filename);
                string v_docsrc = tobj[0].ToString();
                string members = string.Empty;

                ICore2DDrawingDocument doc =
                  IGK.ICore.Codec.CoreXMLSerializerUtility.GetAllObjects(v_docsrc)[0]
                  as ICore2DDrawingDocument;
    
                if (doc != null) {
                    ScriptTarget.Source = v_docsrc;
                    ScriptTarget.Target = doc;
                    StringBuilder sb = new StringBuilder ();
                    string tfullname = doc.GetType().FullName;
                    sb.AppendLine(string.Format("private string ini_document; "));
                    sb.AppendLine(string.Format("private {0} document;", tfullname));
                    sb.AppendLine(string.Format("private void ReloadDocument(){{ document =  IGK.ICore.Codec.CoreXMLSerializerUtility.GetAllObjects(ini_document)[0] as {0}; }}", tfullname));
                    sb.AppendLine(string.Format("public void Init(object document, string source){{ this.document = document as {0}; this.ini_document = source; }}", tfullname )); 

                    members = sb.ToString();
                }
                

                string script = GetScript("ScriptModel", string.Empty, members, v_script);
                CSharpCodeProvider com = CSharpCodeProvider.CreateProvider("CS") as CSharpCodeProvider ;
                CompilerParameters param = new CompilerParameters();
                param.GenerateInMemory = true;
              //  param.OutputAssembly = "script_ex";
                param.ReferencedAssemblies.Add ("System.dll");
                param.ReferencedAssemblies.Add("System.dll");
                param.ReferencedAssemblies.Add("System.Drawing.dll");
                param.ReferencedAssemblies.Add("System.Data.dll");
                param.ReferencedAssemblies.Add(GetType().Assembly.Location);
                param.ReferencedAssemblies.Add(typeof(CoreSystem).Assembly.Location);
                param.ReferencedAssemblies.Add(typeof(DrSStudioWinCoreApp).Assembly.Location);
                param.ReferencedAssemblies.Add(typeof(IGK.ICore.WinCore.WinCoreApplication).Assembly.Location);
                
                CompilerResults r = com.CompileAssemblyFromSource(param, script);

                if (r.Errors.Count == 0)
                {
                    this.m_Success = true;
                    this.m_gAssembly = r.CompiledAssembly.FullName;
                }
                else
                {
                    List<string> te = new List<string> ();
                    foreach (CompilerError  item in r.Errors)
                    {
                        te.Add(item.ToString());
                        CoreLog.WriteLine("Error : "+item);
                    }
                    this.m_errors = te.ToArray();
                    this.m_Success = false;
                }
            }
        }

        private string GetScriptFromFile(string filename)
        {
            string v_str = File.ReadAllText(filename);
            v_str = Regex.Replace(v_str, "#include \"(?<filename>((.)+))\"", new MatchEvaluator((m) => { 
                    string s = Path.Combine (Path.GetDirectoryName (filename), m.Groups["filename"].Value );
                    if (File.Exists(s))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine(string.Format ("#line 1 \"{0}\"", s));
                        sb.AppendLine(GetScriptFromFile(s));
                        return sb.ToString();
                    }
                    return string.Empty;
            }));
            return v_str;
        }


        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            foreach( Assembly asm in AppDomain.CurrentDomain.GetAssemblies ())
            {
                if (args.Name == asm.FullName )
                    return asm ;
            }
            return null;
        }

        private string GetScript(string model, string @namespace, string members, string script)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                string s = typeof(Properties.Resources).GetProperty(model, BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) as string;
                if (!string.IsNullOrEmpty(s))
                    sb.Append(
                        s.Replace("$members$", members).Replace("$script$", script).Replace("$namespace$", @namespace));
            }
            catch { 

            }
            return sb.ToString();
        }
        internal void ExectuteB()
        {
            CoreLog.WriteLine("Domain " + AppDomain.CurrentDomain.FriendlyName);

            Assembly sm = Assembly.Load(new AssemblyName(this.m_gAssembly));
            Type t = sm.GetType("IGK.Script.SurfaceScriptContext");
            object b = t.Assembly.CreateInstance(t.FullName);
            IScriptModel rb =b as IScriptModel;
            if (rb != null)
            {
                rb.Init(ScriptTarget.Target, ScriptTarget.Source );
                rb.Execute(null);
            }
        }

    }
}
