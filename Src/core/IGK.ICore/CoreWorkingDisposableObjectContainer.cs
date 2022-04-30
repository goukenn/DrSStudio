

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingDisposableObjectContainer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public class CoreWorkingDisposableObjectContainer : IDisposable
    {
        List<ICoreDisposableObject> m_disposables;

        
        public int Count
        {
            get { return this.m_disposables.Count;}
        }
        public CoreWorkingDisposableObjectContainer()
        {
            m_disposables = new List<ICoreDisposableObject>();
        }
        public void Add(ICoreDisposableObject disposable)
        {
            this.m_disposables.Add(disposable);
        }
        public void Remove(ICoreDisposableObject disposable)
        {
            this.m_disposables.Remove(disposable);
        }
        public void Dispose()
        {
            foreach (var item in this.m_disposables)
            {
                item.Dispose();
            }
            this.m_disposables.Clear();
        }
    }
}
