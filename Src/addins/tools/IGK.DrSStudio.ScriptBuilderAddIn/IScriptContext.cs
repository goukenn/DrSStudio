using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.ScriptBuilderAddIn
{
    /// <summary>
    /// represent a script context
    /// </summary>
    public interface IScriptContext
    {
        string Response { get; set; }
    }
}
