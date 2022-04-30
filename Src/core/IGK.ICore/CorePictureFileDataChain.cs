

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePictureFileDataChain.cs
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
file:CorePictureFileDataChain.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
namespace IGK.ICore
{
    /// <summary>
    /// represent a picture file data chain
    /// </summary>
    public class CorePictureFileDataChain : CoreFileBaseDataChain , ICoreDataChain 
    {
        /// <summary>
        /// .ctr
        /// </summary>
        public CorePictureFileDataChain()
        {
        }
        public override void SaveTo(string filename)
        {
            Stream stream = File.Create(filename);
            this.Save(stream);
            stream.Flush();
            stream.Dispose();
        }
        public virtual void Save(Stream mem)
        {
            BinaryReader binf = new BinaryReader(mem);
            {
                foreach (ICoreDataEntity data in this)
                {
                    data.Write(binf.BaseStream);
                }
            }
        }
        public override bool LoadFile(string filename)
        {
            if (!File.Exists(filename))
                return false;
            using (BinaryReader binf = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                foreach (ICoreDataEntity data in this)
                {
                    data.Read(binf.BaseStream);
                    if (data is ICoreDataChainHeader)
                    {
                        if (!(data as ICoreDataChainHeader).IsValid)
                        {
                            return false;
                        }
                    }
                }
            }
            return true ;
        }
        public virtual bool LoadStream(Stream stream)
        {
            foreach (ICoreDataEntity data in this)
            {
                data.Read(stream);
                if (data is ICoreDataChainHeader)
                {
                    if (!(data as ICoreDataChainHeader).IsValid)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

