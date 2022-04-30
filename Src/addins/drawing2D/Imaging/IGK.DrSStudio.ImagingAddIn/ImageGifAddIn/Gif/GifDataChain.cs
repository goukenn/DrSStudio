

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GifDataChain.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GifDataChain.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace IGK.DrSStudio.GifAddIn.Gif
{
    class GifDataChain : CorePictureFileDataChain 
    {
        private bool m_loop;
        private int m_frameInterval;
        private GifDataChain()
        {
            m_loop = false;
        }
        public bool IsEmpty {
            get {
                return (this.FrameCount == 0);
            }
        }
        public int FrameCount {
            get {
                return (this.GetEntity (GifConstant.GIF_DATAENTITY) as GifDataChainEntity).FrameCount;
            }
        }
        /// <summary>
        /// get or set the looping
        /// </summary>
        public bool Loop {
            get {
                return m_loop;
            }
            set {
                m_loop = value;
            }
        }
        public int FrameInterval
        {
            get {
                return this.m_frameInterval;
            }
            set {
                this.m_frameInterval = value;
            }
        }
        public static GifDataChain Create()
        {
            GifDataChain chain = new GifDataChain();
            chain.EntityCreator = new GifEntityCreator();
            chain.Add(new GifHeader());
            chain.Add(new GifScreenDescription());
            chain.Add(new GifGlobalColorTable());
            chain.Add(new GifDataChainEntity());
            return chain;
        }
        internal Bitmap getBitmap(int p)
        {
            GifDataChainEntity entity =  this.GetEntity(GifConstant.GIF_DATAENTITY) as GifDataChainEntity ;
            GifGlobalColorTable colorTable = this.GetEntity(GifConstant.GIF_GLOBAL_COLOR) as GifGlobalColorTable;
            if (entity == null) return null;
            GifFrameInfo v_frameInfo =  entity.GetFrameInfo(p);
            GifDataChain chain = GifDataChain.Create();
            chain.GetEntity (GifConstant.GIF_SCREEN_DESC ).Copy(this.GetEntity  (GifConstant.GIF_SCREEN_DESC ));
            chain.GetEntity(GifConstant.GIF_GLOBAL_COLOR).Copy(this.GetEntity(GifConstant.GIF_GLOBAL_COLOR));
            (chain.GetEntity(GifConstant.GIF_DATAENTITY) as GifDataChainEntity).LoadInfo(entity, p);
            MemoryStream mem = new MemoryStream();
            chain.Save(mem );
            mem.Seek(0, SeekOrigin.Begin);
            return Bitmap.FromStream(mem) as Bitmap ;
        }
        public override void SaveTo(string filename)
        {
            base.SaveTo(filename);
        }
        /// <summary>
        /// get the chain entity
        /// </summary>
        /// <returns></returns>
        public GifDataChainEntity getChainEntity() {
            return (GifDataChainEntity)this.GetEntity(GifConstant.GIF_DATAENTITY);
        }
        public GifGlobalColorTable getGlobalColorTable()
        {
            return (GifGlobalColorTable)this.GetEntity(GifConstant.GIF_GLOBAL_COLOR);
        }
        /// <summary>
        /// add bitmap frame
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public  bool AddFrame(Bitmap bmp)
        {
            using (MemoryStream mem = new MemoryStream())
            {
                bmp.Save(mem, ImageFormat.Gif);
                mem.Seek(0, SeekOrigin.Begin);
                GifDataChain chain = GifDataChain.Create();
                chain.LoadStream (mem);
                if (this.IsEmpty)
                {
                    this.GetEntity(GifConstant.GIF_SCREEN_DESC).Copy(chain.GetEntity(GifConstant.GIF_SCREEN_DESC));                  
                    this.GetEntity(GifConstant.GIF_GLOBAL_COLOR).Copy(chain.GetEntity(GifConstant.GIF_GLOBAL_COLOR));                    
                    (this.GetEntity(GifConstant.GIF_DATAENTITY) as GifDataChainEntity).LoadInfo(chain.GetEntity(GifConstant.GIF_DATAENTITY) as GifDataChainEntity, 0);
                }
                else { 
                    //dont copy screen just verify that is good
                    GifDataChainEntity v_data = this.GetEntity(GifConstant.GIF_DATAENTITY) as GifDataChainEntity;
                    if (!v_data.getContainsApplicationExtension())
                        v_data.AddApplicationExtension();
                    v_data.LoadInfo(chain.GetEntity(GifConstant.GIF_DATAENTITY) as GifDataChainEntity, 0);
                    v_data.setInterval(1000, this.FrameCount-1);
                }
            }
            return true;
        }
    }
}

