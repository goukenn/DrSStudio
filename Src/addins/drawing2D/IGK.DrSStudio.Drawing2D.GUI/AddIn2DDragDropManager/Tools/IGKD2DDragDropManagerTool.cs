

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDragDropManagerTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Tools;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Tools;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DDragDropManagerTool.cs
*/
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace IGK.DrSStudio.Drawing2D.Tools
{
    [CoreTools("Tool.IGKD2DDragDropManagerTool")]
    /// <summary>
    /// represent the drag drop manager tools
    /// </summary>
    public class IGKD2DDragDropManagerTool : Core2DDrawingToolBase 
    {
        private static IGKD2DDragDropManagerTool sm_instance;
        private IGKD2DDragDropManagerTool()
        {
        }
        public static IGKD2DDragDropManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static IGKD2DDragDropManagerTool()
        {
            sm_instance = new IGKD2DDragDropManagerTool();
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            Control control = surface as Control;
            if (control !=null)
            {
                control.AllowDrop = true;
                control.DragEnter += c_control_DragEnter;
                control.DragLeave += c_control_DragLeave;
                control.DragDrop += c_control_DragDrop;
            }
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            Control control = surface as Control;
            if (control != null)
            {
                control.AllowDrop = true;
                control.DragEnter -= c_control_DragEnter;
                control.DragLeave -= c_control_DragLeave;
                control.DragDrop -= c_control_DragDrop;
            }
            base.UnRegisterSurfaceEvent(surface);
        }
        void c_control_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if ((e.Effect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    if (e.Data.GetDataPresent(IGKD2DDragDropManager.DATA))
                    {
                        System.IO.MemoryStream mem = (System.IO.MemoryStream)e.Data.GetData(IGKD2DDragDropManager.DATA);
                        string[] d = IGKD2DDragDropManager.GetFileFormIdList(mem);
                        mem.Close();
                        this.LoadString(d);
                    }
                    else
                    {
                        //string[] t = e.Data.GetFormats();
                        //var s = e.Data.GetData("HTML Format");
                        // Bitmap bmp = null;
                        //bmp = new Bitmap(mem);
                        if ((e.Data.GetData("FileName") is string[] o) && (o.Length > 0))
                        {
                            ImageElement l = ImageElement.CreateFromFile(o[0]);
                            if (l != null)
                            {
                                ICoreWorkingInsertItemSurface v_c = this.CurrentSurface as
                ICoreWorkingInsertItemSurface;
                                v_c.Insert(l);
                            }
                        }
                        else if (e.Data.GetData("System.String") is string s)
                        {

                            if (Uri.IsWellFormedUriString(s, UriKind.Absolute))
                            {
                                var r = HttpWebRequest.Create(s);
                                using (StreamReader sr = new StreamReader(r.GetResponse().GetResponseStream()))
                                {

                                    if (Image.FromStream(sr.BaseStream) is Bitmap bmp)
                                    {
                                        this.LoadBitmap(bmp);
                                    }
                                }


                            }
                            else
                            {
                                //check is is base 64 string data
                                //ImageElement l = ImageElement.CreateFromBitmap(bmp);
                                byte[] data = Convert.FromBase64String(s.Substring(s.IndexOf(",") + 1).Trim());
                                using (MemoryStream mem = new MemoryStream())
                                {
                                    mem.Write(data, 0, data.Length);
                                    mem.Seek(0, SeekOrigin.Begin);
                                    if (Image.FromStream(mem) is Bitmap bmp)
                                    {
                                        this.LoadBitmap(bmp);

                                        //            ImageElement l = ImageElement.CreateFromBitmap(WinCoreBitmap.Create(bmp));
                                        //            if (l != null)
                                        //            {
                                        //                ICoreWorkingInsertItemSurface v_c = this.CurrentSurface as
                                        //ICoreWorkingInsertItemSurface;
                                        //                v_c.Insert(l);
                                        //            }

                                    }
                                }
                            }

                        }
                        ////bmp.Dispose();


                        //Stream sm = (Stream)e.Data.GetData("FileName");
                        //StreamReader sw = new StreamReader(sm);
                        //string ul = sw.ReadToEnd();
                        //Stream mem = PathUtils.WgetFile(ul.Trim());

                        //for (int i = 0; i < t.Length; i++)
                        //{
                        //    var  h = e.Data.GetData(t[i]);

                        //    if (h is string)
                        //    {
                        //        string st = h as string;
                        //        if (st.ToLower().StartsWith(Uri.UriSchemeHttp.ToLower()))
                        //        {
                        //            mem = PathUtils.WgetFile(h as string);
                        //            if (mem != null)
                        //            {
                        //                try
                        //                {
                        //                    bmp = new Bitmap(mem);
                        //                    ImageElement l = ImageElement.CreateFromBitmap(WinCoreBitmap.Create(bmp));
                        //                    bmp.Dispose();
                        //                    break;
                        //                }
                        //                catch { 

                        //                }
                        //            }
                        //        }
                        //    }
                        //   // System.Drawing.Bitmap bmp = System.Drawing.Bitmap.FromStream((System.IO.MemoryStream)h) as System.Drawing.Bitmap;
                        //}

                    }
                }
            }
            catch (Exception ex) {
                CoreLog.WriteLine(ex.Message);
            }
        }

        private void LoadBitmap(Bitmap bmp)
        {
            ImageElement l = ImageElement.CreateFromBitmap(WinCoreBitmap.Create(bmp));
            if (l != null)
            {
                ICoreWorkingInsertItemSurface v_c = this.CurrentSurface as
ICoreWorkingInsertItemSurface;
                v_c.Insert(l);
            }

        }

        private void LoadString(string[] d)
        {
            ImageElement c_img = null;
            if (this.CurrentSurface is ICoreWorkingInsertItemSurface v_c)
            {
                foreach (string s in d)
                {
                    c_img = ImageElement.CreateFromFile(s);
                    if (c_img != null)
                    {
                        v_c.Insert(c_img);
                    }
                    else
                    {
                        DocumentBlockElement[] c = DocumentBlockElement.CreateElementFromFile(s);
                        if (c != null)
                        {
                            for (int i = 0; i < c.Length; i++)
                            {
                                v_c.Insert(c[i]);
                            }

                        }
                    }
                }
            }
        }
        void c_control_DragLeave(object sender, EventArgs e)
        {
        }
        void c_control_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
}

