

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DSaveCurrentDocumentMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.Menu;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.IO;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.Drawing2D.Menu;

namespace IGK.DrSStudio.Drawing2D.Menu.File
{
    [DrSStudioMenu("File.Drawing2DSaveCurrentDocument", CoreConstant.SAVE_MENU_INDEX + 10)]
    sealed class IGKD2DSaveCurrentDocumentMenu : Core2DDrawingMenuBase
    {
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null) && (this.CurrentSurface.Documents.Count > 1);
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null) && (this.CurrentSurface.Documents.Count > 1);
        }
        protected override bool PerformAction()
        {
            var tab = new ICoreWorkingDocument[] { this.CurrentSurface.CurrentDocument };
            bool f = false;
            ICoreSaveAsInfo info =
                (this.CurrentSurface is ICoreWorkingSaveSurface) ? (this.CurrentSurface as ICoreWorkingSaveSurface).GetSaveAsInfo() : null;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                //bind save as info                
                if (info != null)
                {
                    sfd.Title = info.Title;
                    sfd.Filter = info.Filter;
                    string dir = PathUtils.GetDirectoryName(info.FileName);
                    if (System.IO.Directory.Exists(dir))
                    {
                        Environment.CurrentDirectory = dir;
                    }
                    sfd.FileName = System.IO.Path.GetFileName(info.FileName);
                }

                if (sfd.ShowDialog() == DialogResult.OK)
                { 
                      ICoreCodec[] codec = CoreSystem.GetEncodersByExtension(System.IO.Path.GetExtension(sfd.FileName));
                      if (codec.Length == 1)
                      {
                          f = CoreEncoderBase.SaveToFile(
                              this.CurrentSurface,
                              codec[0] as ICoreEncoder,
                              sfd.FileName,
                              tab);
                      }
                      else {
                          if (codec.Length > 1) {
                              CoreCodecSelector c = new CoreCodecSelector(codec);
                              if (Workbench.ConfigureWorkingObject(c, 
                                  "title.choosecodec".R(), 
                                  false, Size2i.Empty ) == enuDialogResult.OK)
                              {
                                  var v_m = c.SelectedCodec as ICoreEncoder;
                                  if (v_m != null)
                                  {
                                      f = CoreEncoderBase.SaveToFile(
                                  this.CurrentSurface,
                                 v_m,
                                   sfd.FileName ,
                                   tab);
                                  }

                              }
                          }
                      }
                }
            }
            return f;
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>  e)
        {
            base.OnCurrentSurfaceChanged(e);            
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.DocumentAdded += surface_DocumentAdded;
            surface.DocumentRemoved += surface_DocumentAdded;
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.DocumentAdded -= surface_DocumentAdded;
            surface.DocumentRemoved -= surface_DocumentAdded;
            base.UnRegisterSurfaceEvent(surface);
        }

        void surface_DocumentAdded(object sender, CoreItemEventArgs<ICore2DDrawingDocument> e)
        {
            this.SetupEnableAndVisibility();
        }
    }
}
