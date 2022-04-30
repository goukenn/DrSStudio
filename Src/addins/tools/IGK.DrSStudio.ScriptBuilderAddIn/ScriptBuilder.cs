
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;

namespace IGK.DrSStudio.ScriptBuilderAddIn
{
    class ScriptBuilder
    {
        static AppDomain sm_currentDomain;
    
        /// <summary>
        /// build script and appy it to the document
        /// </summary>
        /// <param name="document"></param>
        /// <param name="filename"></param>
        internal static bool BuildScriptFile(IScriptResponseListener reponse, ICore2DDrawingDocument document, string filename)
        {
            sm_currentDomain = AppDomain.CurrentDomain;
            System.Security.Policy.Evidence e = new System.Security.Policy.Evidence();
            e.AddAssemblyEvidence<ScriptBuilderEvidence>(new ScriptBuilderEvidence());
            AppDomainSetup v_settup = new AppDomainSetup ();
            v_settup.PrivateBinPath =PathUtils.GetPath ("%startup%/AddIn")+";"+
                PathUtils.GetPath ("%startup%/Lib")+";"+PathUtils.GetPath ("%startup%/Bin");
            
            AppDomain d = AppDomain.CreateDomain("script-domain", null, v_settup );
            

            sm_currentDomain.AssemblyResolve += sm_currentDomain_AssemblyResolve;
            try
            {
                
                d.AssemblyResolve += d_AssemblyResolve;
                //foreach (var s in sm_currentDomain.GetAssemblies())
                //{
                //    d.Load(s.GetName());
                //}
                //ICoreWorkingStringExpression doc =
                //    IGK.ICore.Codec.CoreXMLSerializerUtility.GetAllObjects("<LayerDocument></LayerDocument>")[0]
                //    as ICoreWorkingStringExpression;

                ObjectHandle obj = d.CreateInstance(
                  MethodInfo.GetCurrentMethod().DeclaringType.Assembly.GetName().FullName,
                   typeof(ScriptCompilerLoader).FullName,
                   true,
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public,
                     null,
                     new object[] { filename, new object[]{
                         document.Render ()
                     } },
                     Thread.CurrentThread.CurrentCulture,
                     null);
                ScriptCompilerLoader v_c = (ScriptCompilerLoader)obj.Unwrap();
                CoreLog.WriteLine("Building ... "+v_c.Success);
                reponse.CompilationSuccess = v_c.Success;
                if (v_c.Success)
                {
                    d.DoCallBack(v_c.ExectuteB);
                }
                else {
                    reponse.CompilationErrors = v_c.GetErrors();
                }
                AppDomain.Unload(d);
                return reponse.CompilationSuccess;
            }
            catch(Exception ex) {
                Debug.WriteLine("Exception : " + ex.Message);
                Console.WriteLine("error "+ex.Message );
            }
            return false;
        }

        static Assembly sm_currentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }

        static Assembly d_AssemblyResolve(object sender, ResolveEventArgs args)
        {
          
            return null;
        }
    }
}
