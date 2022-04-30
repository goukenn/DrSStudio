

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreFileBaseDataChain.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreFileBaseDataChain.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    /// <summary>
    /// abstract Core FileBase For data chain for visitor serializer
    /// </summary>
    public abstract class CoreFileBaseDataChain : ICoreDataChain 
    {
        ICoreDataEntityCreator m_EntityCreator;
        List<ICoreDataEntity> m_datas;
        public ICoreDataEntityCreator EntityCreator
        {
            get
            {
                return this.m_EntityCreator ;
            }
            set
            {
                this.m_EntityCreator = value ;
            }
        }
        public void Add(ICoreDataEntity entity)
        {
            if (entity == null) return;
            this.m_datas.Add(entity);
            entity.Chain = this;
        }
        public void Remove(ICoreDataEntity entity)
        {
            if (entity == null) return;
            this.m_datas.Remove(entity);
            entity.Chain = null;
        }
        public abstract  bool LoadFile(string filename);
        public virtual string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ICoreDataEntity  item in this)
            {
                sb.AppendLine(item.Render());
            }
            return sb.ToString ();
        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_datas.GetEnumerator();
        }
        public CoreFileBaseDataChain()
        {
            this.m_datas = new List<ICoreDataEntity>();
        }
        public ICoreDataEntity GetEntity(string name)
        {
            foreach (ICoreDataEntity item in this)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }
        public abstract void SaveTo(string filename);
    }
}

