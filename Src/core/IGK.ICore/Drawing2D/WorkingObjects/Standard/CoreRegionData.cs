

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreRegionData.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;
using IGK.ICore.GraphicModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent a region data
    /// </summary>
    public class CoreRegionData : ICoreRegionBuildListener
    {
        CoreRegionDataList m_list;

        public bool IsEmpty
        {
            get { return !(m_list.Count > 0); }
        }
        private CoreRegionData() {
            this.m_list = new CoreRegionDataList(this);
        }
        public CoreRegionData(ICoreGraphicsRegion rg):this()
        {
        }
        public CoreRegionData(ICoreGraphicsPath data):this()
        {
            this.m_list.Add(new CoreRegionListSetting(enumCoreRegionOperation.Init, data));
        }
        public void Complement(ICoreGraphicsPath path) {
            this._AddItem(enumCoreRegionOperation.Complement, path);
        }
        public void Exclude(ICoreGraphicsPath path) {
            this._AddItem(enumCoreRegionOperation.Exclude, path);
        }
        public void Intersect(ICoreGraphicsPath path) {
            this._AddItem(enumCoreRegionOperation.Intersect, path);
        }
        public void Xor(ICoreGraphicsPath path) {
            this._AddItem(enumCoreRegionOperation.Xor, path);
        }
        public void Union(ICoreGraphicsPath path)
        {
            this._AddItem(enumCoreRegionOperation.Union, path);
           
        }

        public CoreGraphicsPath GetOutLinePath()
        {
            if (IsEmpty)
                return null;
            CoreGraphicsPath cp = new CoreGraphicsPath();
            CoreApplicationManager.Application.GraphicsPathUtils.CreateNewRegion(cp,
                this);      
         
            return cp;
        }
       
        private void _AddItem(enumCoreRegionOperation type, ICoreGraphicsPath path)
        {
            this.m_list.Add(new CoreRegionListSetting(type, path));
        }

        public bool RegionBuildItem(ICoreRegionBuildAction cp)
        {
            if (this.m_list.Count == 0)
            return false;
            foreach (CoreRegionListSetting item in this.m_list)
            {
                item.Load(cp);
            }
            return true;
        }

        class CoreRegionListSetting {
            private ICoreGraphicsPath data;
            private enumCoreRegionOperation init;

            public CoreRegionListSetting(enumCoreRegionOperation init, ICoreGraphicsPath data)
            {
                this.init = init;
                this.data = data;
            }

            internal void Load(ICoreRegionBuildAction cp)
            {
                string n = Enum.GetName(typeof(enumCoreRegionOperation), this.init);
                var meth =  cp.GetType().GetMethod(n);
                meth.Invoke(cp, new object[] {
                    this.data
                });
                //switch (this.init)
                //{
                //    case enumCoreRegionOperation.Init:
                //        cp.Init(this.data);
                //        break;
                //    case enumCoreRegionOperation.Exclude:
                //        cp.Exclude(this.data);
                //        break;
                //    case enumCoreRegionOperation.Union:
                //        cp.Union(this.data);
                //        break;
                //    case enumCoreRegionOperation.Xor:
                //        break;
                //    case enumCoreRegionOperation.Complement:
                //        cp.Complement(this.data);
                //        break;
                //    case enumCoreRegionOperation.Intersect:
                //        cp.Intersect(this.data);
                //        break;
                //    default:
                //        break;
                //}
            }
        }
        class CoreRegionDataList : IEnumerable   {
            private List<CoreRegionListSetting> m_list;
            private CoreRegionData m_owner;
            public int Count { get { return m_list.Count; } }
            internal  CoreRegionDataList(CoreRegionData owner)
            {
                this.m_list = new List<CoreRegionListSetting>();
                this.m_owner = owner;
            } 
            public void Add(CoreRegionListSetting op) {
                this.m_list.Add(op);
            }
            public void Remove(CoreRegionListSetting op)
            {
                this.m_list.Remove(op);
            }
            public void Clear()
            {
                this.m_list.Clear();
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }
        }
    }
}
