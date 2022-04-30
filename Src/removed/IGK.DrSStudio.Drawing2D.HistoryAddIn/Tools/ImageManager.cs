

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImageManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:ImageManager.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------
This file is part of IGK-DEV DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI ;
    using IGK.DrSStudio.Drawing2D.HistoryActions;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D.History;
    /// <summary>
    /// represent tool that will manage the ImageElement
    /// </summary>
    public class ImageManager
        : HistoryToolManagerBase
    {
        public ImageManager(HistorySurfaceManager tool):base(tool)
        {
            System.Windows.Forms.Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingSurfaceChangedEventArgs e)
        {
            if (e.OldSurface is ICore2DDrawingSurface)
                UnRegisterSurfaceEvent(e.OldSurface as ICore2DDrawingSurface);
            //else if (e.OldSurface is ICoreWorkingConfigElementSurface)
            //    UnRegisterSurfaceEvent(e.OldSurface as ICoreWorkingConfigElementSurface);
            if (e.NewSurface is ICore2DDrawingSurface)
                RegisterSurfaceEvent(e.NewSurface as ICore2DDrawingSurface);
            //else if (e.NewSurface is ICoreWorkingConfigElementSurface)
            //{
            //    RegisterSurfaceEvent(e.NewSurface as ICoreWorkingConfigElementSurface);
            //}
        }
        //private void UnRegisterSurfaceEvent(ICoreWorkingConfigElementSurface surface)
        //{
        //    surface.ElementToConfigureChanged -= new EventHandler(surface_ElementToConfigureChanged);
        //}
        //private void RegisterSurfaceEvent(ICoreWorkingConfigElementSurface surface)
        //{
        //    surface.ElementToConfigureChanged += new EventHandler(surface_ElementToConfigureChanged);
        //    Select(surface.ElementToConfigure as ImageElement);
        //}
        //void surface_ElementToConfigureChanged(object sender, EventArgs e)
        //{
        //    ICoreWorkingConfigElementSurface s = sender as ICoreWorkingConfigElementSurface ;
        //    this.Select(s.ElementToConfigure as ImageElement);
        //}
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {            
            base.RegisterLayerEvent(layer);
            layer.SelectedElementChanged += new EventHandler(layer_SelectedElementChanged);
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {            
            base.UnRegisterLayerEvent(layer);
            layer.SelectedElementChanged -= new EventHandler(layer_SelectedElementChanged);
        }
        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            if (this.CurrentSurface.CurrentLayer.SelectedElements.Count != 1)
                return;
            Select(this.CurrentSurface.CurrentLayer.SelectedElements[0] as ImageElement);
        }
        Dictionary<ImageElement, SingleImageHistoryManager> m_rsfile;
        private void Select(ImageElement image)
        {
            if (image == null)return ;
            if (m_rsfile == null)
                m_rsfile = new Dictionary<ImageElement, SingleImageHistoryManager>();
            if (!m_rsfile.ContainsKey(image) &&
                (Tools.HistorySurfaceManager.Instance.ActionsList !=null))
            {
                //TODO: INIt IMAGE HISTORY
                //SingleImageHistoryManager m = new SingleImageHistoryManager(
                //    Tools.HistorySurfaceManager.Instance.ActionsList ,
                //    image,                    
                //    this.Workbench.CurrentSurface);
              //  m_rsfile.Add(image, m);                
            }
        }
        void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (m_rsfile == null) return;
            foreach (KeyValuePair<ImageElement, SingleImageHistoryManager> item in m_rsfile)
            {
                item.Value.Dispose();
            }
        }
        //public class SingleImageManager : IDisposable 
        //{
        //    private string m_folder;
        //    private Guid m_identifier;
        //    private ImageElement m_image;   //image to manager
        //    private int m_index;                  //index of the current image file
        //    private List<string> m_savedfile;
        //    public ImageElement Image { get { return m_image; } }
        //    private ICoreWorkingSurface m_surface;
        //    private ImageManager m_imgManager;
        //    public SingleImageManager(ImageElement image, 
        //        ImageManager imgManager,
        //        ICoreWorkingSurface surface) 
        //    {
        //        this.m_surface = surface;
        //        this.m_image = image;
        //        this.m_imgManager = imgManager;
        //        this.m_identifier = Guid.NewGuid();
        //        this.m_savedfile = new List<string> ();
        //        this.m_folder = IGK.DrSStudio.IO.PathUtils.GetPath ( IGK.DrSStudio.Settings.CoreApplicationSetting.Instance.TempFolder 
        //        +"/"+this.m_identifier.ToString());
        //        IGK.DrSStudio.IO.PathUtils.CreateDir(m_folder);
        //        SaveFirstImage();
        //        image.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(image_PropertyChanged);
        //        this.m_surface.Disposed += new EventHandler(m_surface_Disposed);
        //    }
        //    void m_surface_Disposed(object sender, EventArgs e)
        //    {
        //        this.Dispose();
        //        this.m_imgManager.m_rsfile.Remove(this.m_image);
        //    }
        //    void image_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        //    {
        //        switch ((IGK.DrSStudio.Drawing2D.enu2DPropertyChangedType  ) e.ID)
        //        {
        //            case enu2DPropertyChangedType.BitmapChanged :
        //                AddNewBitmapToCollection();                        
        //                break;
        //        }
        //    }
        //    void Clear(){
        //        for (int i = 0; i < this.m_savedfile .Count; i++)
        //        {
        //            if (System.IO.File.Exists(this.m_savedfile[i]))
        //            {
        //                System.IO.File.Delete(this.m_savedfile[i]);
        //            }
        //        }
        //    }
        //    public void Dispose() 
        //    {
        //        Clear();
        //        if (Directory.Exists(m_folder))
        //        {
        //            Directory.Delete(m_folder, true);
        //        }
        //    }
        //    private void SaveFirstImage()
        //    {
        //        string sf = string.Format (this.m_folder +"/ 0x{0:X}.data", this.m_savedfile.Count );
        //        CoreBitmapData.SaveData(sf, this.m_image.Bitmap);
        //        this.m_savedfile.Add(sf);
        //        this.m_index = 0;
        //    }
        //    private void AddNewBitmapToCollection()
        //    {
        //        if (!Tools.HistorySurfaceManager.Instance.CanAdd)
        //            return;
        //        if (this.m_index < this.m_savedfile.Count-1)
        //        {
        //            this.ClearAt(this.m_index+1);
        //        }
        //        if (this.m_index == 0)
        //        {
        //            this.m_index++;
        //        }
        //        else if (this.m_index < m_savedfile.Count)
        //        {
        //            this.m_index++;
        //        }
        //        else
        //            m_index++;
        //        string sf = string.Format(this.m_folder + "/ 0x{0:X}.data", this.m_savedfile.Count);
        //        CoreBitmapData.SaveData(sf, this.m_image.Bitmap);
        //        this.m_savedfile.Add(sf);                
        //        if (Tools.HistorySurfaceManager.Instance.CanAdd)
        //        {
        //            Tools.HistorySurfaceManager.Instance.Add(new _BitmapHistoryActions(this,this.m_index));
        //        }
        //    }
        //    private void ClearAt(int index)
        //    {
        //        //dispose files 
        //        for (int i = index; i < this.m_savedfile .Count ; i++)
        //        {
        //            if (File.Exists (this.m_savedfile[i] ))
        //                File.Delete(this.m_savedfile[i]);
        //        }
        //        this.m_savedfile.RemoveRange(index, this.m_savedfile.Count - index);
        //    }
        //    public override string ToString()
        //    {
        //        return "imageManager :" + this.Image.Id;
        //    }
        //    public void Undo(_BitmapHistoryActions action)
        //    {
        //        if (action .CurrentImageIndex > 0)
        //        {
        //            m_index = action.CurrentImageIndex ;
        //            m_index--;
        //            this.m_image.SetBitmap(CoreBitmapData.ReadData(m_savedfile[m_index]),false );
        //            this.m_image.Invalidate(true);
        //        }
        //    }
        //    public void Redo(_BitmapHistoryActions action)
        //    {
        //        if (action.CurrentImageIndex < m_savedfile .Count)
        //        {
        //            m_index = action.CurrentImageIndex ;
        //       //m_index++;
        //        this.m_image.SetBitmap(CoreBitmapData.ReadData(m_savedfile[m_index]),false );
        //        this.m_image.Invalidate(true);
        //        }
        //    }
        //}
    }
}

