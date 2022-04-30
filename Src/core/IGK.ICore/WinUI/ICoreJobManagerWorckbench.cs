using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    public interface ICoreJobManagerWorckbench : ICoreWorkbench 
    {
        event EventHandler JobStart;
        event CoreJobProgressEventHandler JobProgress;
        event EventHandler JobComplete;

        void BeginJob(object obj);
        void UpdateJob(object obj, int value);
        void EndJob(object obj);
    }
}
