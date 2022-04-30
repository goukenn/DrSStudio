

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingFileSolution.cs
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

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent the base solution item
    /// </summary>
    public class CoreWorkingFileSolution : ICoreWorkingProjectSolution
    {
        private string m_FileName;

        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                }
            }
        }
        public CoreWorkingFileSolution(string filename)
        {
            this.m_FileName = filename;
        }

        public virtual string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public virtual string ImageKey
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ICoreWorkingProjectSolutionItemCollections Items
        {
            get { throw new NotImplementedException(); }
        }

        public virtual  void Open(ICoreSystemWorkbench coreWorkbench, ICoreWorkingProjectSolutionItem item)
        {
            
        }

        public void SaveAs(string filename)
        {
            
        }

        public virtual  void Save()
        {
            
        }

        public virtual System.Collections.IEnumerable GetSolutionToolActions()
        {
            return null;
        }

        public string Id
        {
            get {
                return this.Name;
            }
        }


        public virtual ICoreSaveAsInfo GetSolutionSaveAsInfo()
        {
            return null;
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
