

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingSolutionOpenedManager.cs
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
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a solution manager opened item
    /// </summary>
    public class CoreWorkingSolutionOpenedManager : IEnumerable 
    {
        Dictionary<ICoreWorkingProjectSolutionItem, HostingSurface> m_surfaces;
        private ICoreWorkingProjectSolution  m_solution;


        public CoreWorkingSolutionOpenedManager(ICoreWorkingProjectSolution Solution)
        {
            this.m_surfaces = new Dictionary<ICoreWorkingProjectSolutionItem, HostingSurface>();
            this.m_solution = Solution;
        }
        public IEnumerator GetEnumerator()
        {
            return m_surfaces.GetEnumerator();
        }
        public bool Contains(ICoreWorkingProjectSolutionItem item)
        {
            if (item == null)
                return false;
            return m_surfaces.ContainsKey(item);
        }
        public void Add(ICoreWorkingProjectSolutionItem item, ICoreWorkingSurface surface)
        {
            if (item == null)
                return;
            this.m_surfaces.Add(item, new HostingSurface(this, item, surface));
        }

        internal void Remove(ICoreWorkingProjectSolutionItem item)
        {
            this.m_surfaces.Remove(item);
        }

        class HostingSurface
        {
            private CoreWorkingSolutionOpenedManager m_collection;
            private ICoreWorkingProjectSolutionItem m_item;
            private ICoreWorkingSurface m_surface;

            public HostingSurface(
                CoreWorkingSolutionOpenedManager collection,
                ICoreWorkingProjectSolutionItem item,
                ICoreWorkingSurface surface)
            {
                this.m_collection = collection;
                this.m_item = item;
                this.m_surface = surface;
                this.m_surface.Disposed += m_surface_Disposed;
            }

            void m_surface_Disposed(object sender, EventArgs e)
            {
                this.m_collection.Remove(this.m_item);
            }
            /// <summary>
            /// get the surface
            /// </summary>
            public ICoreWorkingSurface Surface { get { return this.m_surface; } }
        }


        public ICoreWorkingSurface GetSurface(ICoreWorkingProjectSolutionItem item)
        {
            if (this.Contains(item))
            {
                return this.m_surfaces[item].Surface;
            }
            return null;
        }
    }
}
