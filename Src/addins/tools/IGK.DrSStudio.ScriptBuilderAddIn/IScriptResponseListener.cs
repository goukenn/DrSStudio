using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ScriptBuilderAddIn
{
    public interface IScriptResponseListener
    {

        bool CompilationSuccess { get; set; }

        string[] CompilationErrors { get; set; }
    }
}
