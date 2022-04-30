

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CursorHelper.cs
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
file:CursorHelper.cs
*/
using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32.SafeHandles;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Utils
{
  public class CursorHelper
  {
    private static class NativeMethods
    {
      public struct IconInfo
      {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
      }
      [DllImport("user32.dll")]
      public static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);
      [DllImport("user32.dll")]
      public static extern bool DestroyIcon(IntPtr hIcon);
      [DllImport("user32.dll")]
      [return: MarshalAs(UnmanagedType.Bool)]
      public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
    }
      /// <summary>
      /// create a cursor from handle. System.Windows.Forms.Cursor.Handle
      /// </summary>
      /// <param name="hptr"></param>
      /// <returns></returns>
    public static Cursor FromHandle(IntPtr hptr)
    {
       SafeHandle sh = new MyIconHandle(hptr );
       return CursorInteropHelper.Create(sh);
    }
    class MyIconHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public MyIconHandle(IntPtr handle):base(true )
        {
            this.handle = handle;
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        protected override bool ReleaseHandle()
        {
            return true;
        }
    }
    [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
    private class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
      public SafeIconHandle()
        : base(true)
      {
      }
      override protected bool ReleaseHandle()
      {
        return NativeMethods.DestroyIcon(handle);
      }
    }
    private static Cursor InternalCreateCursor(System.Drawing.Bitmap bmp, int xHotSpot, int yHotSpot)
    {
      var iconInfo = new NativeMethods.IconInfo();
      NativeMethods.GetIconInfo(bmp.GetHicon(), ref iconInfo);
      iconInfo.xHotspot = xHotSpot;
      iconInfo.yHotspot = yHotSpot;
      iconInfo.fIcon = false;
      SafeIconHandle cursorHandle = NativeMethods.CreateIconIndirect(ref iconInfo);
      return CursorInteropHelper.Create(cursorHandle);
    }
    public static Cursor CreateCursor(UIElement element, int xHotSpot, int yHotSpot)
    {
      element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      element.Arrange(new Rect(new Point(), element.DesiredSize));
      RenderTargetBitmap rtb =
        new RenderTargetBitmap(
          (int)element.DesiredSize.Width,
          (int)element.DesiredSize.Height,
          96, 96, PixelFormats.Pbgra32);
      rtb.Render(element);
      var encoder = new PngBitmapEncoder();
      encoder.Frames.Add(BitmapFrame.Create(rtb));
      using (var ms = new MemoryStream())
      {
        encoder.Save(ms);
        using (var bmp = new System.Drawing.Bitmap(ms))
        {
          return InternalCreateCursor(bmp, xHotSpot, yHotSpot);
        }
      }
    }
  }
}

