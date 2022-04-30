using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
$namespace$


namespace IGK.Script
{
    
    using IGK.ICore;
    using IGK.ICore.WinCore;
    using IGK.ICore.Drawing2D;
    using IGK.DrSStudio.ScriptBuilderAddIn;

    //define some script delegate
    delegate void ScriptFunc<T>(T firstParam);
    delegate void ScriptFunc2<T,M>(T firstParam, M secondParam);

    [CoreApplication("ScriptApplication")]
    class ScriptApplication : IGK.DrSStudio.WinCoreApplication
    {
    }
    class SurfaceScriptContext : IScriptModel
    {
        //initialize member and methods
        $members$

        public void Execute(IScriptContext context) { 

          //
          // script to execute
          //
          #line 1 "scriptmodel.igkcs"
          $script$
        }
    }
}
