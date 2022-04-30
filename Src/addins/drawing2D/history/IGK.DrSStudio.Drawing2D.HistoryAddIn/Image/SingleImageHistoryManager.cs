

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SingleImageHistoryManager.cs
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
using IGK.ICore.History;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using IGK.ICore.IO;
using IGK.ICore.Settings;
namespace IGK.DrSStudio.Drawing2D.History
{

    
    /// <summary>
    /// single image manager
    /// </summary>
    public class SingleImageHistoryManager : IDisposable
    {
        private string m_folder;
        private Guid m_identifier;
        private ImageElement m_image;   //image to manager
        private int m_index;                  //index of the current image file
        private List<string> m_savedfile;
        private IHistoryList m_list;

        public ImageElement Image { get { return m_image; } }
        private ICoreWorkingSurface  m_surface;
        

        public SingleImageHistoryManager(
            IHistoryList list, 
            ImageElement image           ,
            ICoreWorkingSurface surface)
            
        {
            this.m_list = list;
            this.m_surface = surface;
            this.m_image = image;
            this.m_identifier = Guid.NewGuid();
            this.m_savedfile = new List<string>();
            this.m_folder = PathUtils.GetPath(CoreApplicationSetting.Instance.TempFolder
            + "/" + this.m_identifier.ToString());
            PathUtils.CreateDir(m_folder);
            SaveFirstImage();
            image.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(image_PropertyChanged);
            this.m_surface.Disposed += new EventHandler(m_surface_Disposed);
        }

        void m_surface_Disposed(object sender, EventArgs e)
        {
            this.m_list.Clear();
            this.Dispose();
            
            //this.m_imgManager.m_rsfile.Remove(this.m_image);
        }

        void image_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            switch ((enu2DPropertyChangedType)e.ID)
            {
                case enu2DPropertyChangedType.BitmapChanged:
                    AddNewBitmapToCollection();
                    break;
            }
        }

        void Clear()
        {
            for (int i = 0; i < this.m_savedfile.Count; i++)
            {
                if (System.IO.File.Exists(this.m_savedfile[i]))
                {
                    System.IO.File.Delete(this.m_savedfile[i]);
                }
            }
        }
        public void Dispose()
        {
            Clear();
            if (Directory.Exists(m_folder))
            {
                Directory.Delete(m_folder, true);
            }
        }

        private void SaveFirstImage()
        {
            string sf = string.Format(this.m_folder + "/ 0x{0:X}.data", this.m_savedfile.Count);
            WinCoreBitmapData.SaveData(sf, this.m_image.Bitmap);
            this.m_savedfile.Add(sf);
            this.m_index = 0;
        }

        private void AddNewBitmapToCollection()
        {
            if (this.m_configure)
                return;

            if (this.m_index < this.m_savedfile.Count - 1)
            {
                this.ClearAt(this.m_index + 1);
            }
            if (this.m_index == 0)
            {
                this.m_index++;
            }
            else if (this.m_index < m_savedfile.Count)
            {
                this.m_index++;
            }
            else
                m_index++;

            string sf = string.Format(this.m_folder + "/ 0x{0:X}.data", this.m_savedfile.Count);
            WinCoreBitmapData.SaveData(sf, this.m_image.Bitmap);
            this.m_savedfile.Add(sf);
            this.m_list .Add (           
                new ImageHistoryAction (this, this.m_index));
            
        }

        private void ClearAt(int index)
        {
            //dispose files 
            for (int i = index; i < this.m_savedfile.Count; i++)
            {
                if (File.Exists(this.m_savedfile[i]))
                    File.Delete(this.m_savedfile[i]);
            }
            this.m_savedfile.RemoveRange(index, this.m_savedfile.Count - index);
        }

        public override string ToString()
        {
            return "SingleImageManager :" + this.Image.Id;
        }

        private bool m_configure;
        public void Undo(IImageHistory action)
        {
            if (action.CurrentImageIndex > 0)
            {
                m_index = action.CurrentImageIndex;
                m_index--;
                this.m_configure = true;
                this.m_image.SetBitmap(WinCoreBitmapData.ReadData(m_savedfile[m_index]), false);
                this.m_image.Invalidate(true);
                this.m_configure = false;
            }
        }
        public void Redo(IImageHistory action)
        {
            if (action.CurrentImageIndex < m_savedfile.Count)
            {
                m_index = action.CurrentImageIndex;
                //m_index++;
                this.m_configure = true;
                this.m_image.SetBitmap(WinCoreBitmapData.ReadData(m_savedfile[m_index]), false);
                this.m_image.Invalidate(true);
                this.m_configure = false;
            }
        }
    }
  
 
}
