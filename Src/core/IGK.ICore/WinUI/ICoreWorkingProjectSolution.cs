

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingSolution.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a product solution
    /// </summary>
    public interface ICoreWorkingProjectSolution : ICoreWorkingObject 
    {
        /// <summary>
        /// get or set the name of the solution
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// get or set the image key of this solution
        /// </summary>
        string ImageKey { get; set; }
        /// <summary>
        /// get the filename of this solution
        /// </summary>
        string FileName { get; }
        ICoreWorkingProjectSolutionItemCollections Items { get; }
        void Open(ICoreSystemWorkbench coreWorkbench, ICoreWorkingProjectSolutionItem item);

        void SaveAs(string p);

        void Save();

        IEnumerable GetSolutionToolActions();

        ICoreSaveAsInfo GetSolutionSaveAsInfo();
        /// <summary>
        /// close this solution
        /// </summary>
        void Close();
    }
}
