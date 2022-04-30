

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GlobalSolutionTreeNode.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    public class GlobalSolutionTreeNode : TreeNode
    {
        private ICoreWorkingProjectSolution coreWorkingSolution;

        public ICoreWorkingProjectSolution Solution { get { return coreWorkingSolution; } }

        public GlobalSolutionTreeNode(ICoreWorkingProjectSolution coreWorkingSolution)
        {         
            this.coreWorkingSolution = coreWorkingSolution;
            
        }

    }
}
