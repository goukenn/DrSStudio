using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.ScriptBuilderAddIn
{
    public interface  IScriptModel
    {
        void Init(object target, string source);
        void Execute(IScriptContext context);
    }
}
