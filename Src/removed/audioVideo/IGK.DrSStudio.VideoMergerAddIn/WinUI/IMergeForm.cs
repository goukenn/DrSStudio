

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IMergeForm.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:IMergeForm.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace IGK.DrSStudio.WinUI
{
    public interface IMergeForm : System.ComponentModel.IComponent 
    {
        string Text { get; set; }
        //
        // Summary:
        //     Executes the specified delegate asynchronously on the thread that the control's
        //     underlying handle was created on.
        //
        // Parameters:
        //   method:
        //     A delegate to a method that takes no parameters.
        //
        // Returns:
        //     An System.IAsyncResult that represents the result of the System.Windows.Forms.Control.BeginInvoke(System.Delegate)
        //     operation.
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        IAsyncResult BeginInvoke(Delegate method);
        void LoadTitle();
    }
}

