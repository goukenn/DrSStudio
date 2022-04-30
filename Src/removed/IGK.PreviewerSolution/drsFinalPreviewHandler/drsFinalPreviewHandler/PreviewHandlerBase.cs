

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PreviewHandlerBase.cs
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
file:PreviewHandlerBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms ;
using System.Runtime.InteropServices ;
namespace IGK.DrSStudio.PreviewHandler
{
    using IGK.ICore;using IGK.DrSStudio.PreviewHandler.WinUI;
    using IGK.DrSStudio.PreviewHandler.Native;
    using System.ComponentModel;
    /// <summary>
    /// represent a preview Handler Base Class
    /// </summary>
    public abstract class PreviewHandlerBase : IPreviewHandler, IPreviewHandlerVisuals, IOleWindow, IObjectWithSite
    {
        private bool _showPreview;
        private IPreviewHandlerControl _previewControl;
        private IntPtr _parentHwnd;
        private Rectangle _windowBounds;
        private object _unkSite;
        private IPreviewHandlerFrame _frame;
        //ResiziNativeWindow m_RNativeWindow;
        //class ResiziNativeWindow : NativeWindow
        //{
        //    PreviewHandler PreviewHandler;
        //    public ResiziNativeWindow(PreviewHandler c)
        //    {
        //        PreviewHandler = c;
        //    }
        //    protected override void WndProc(ref Message m)
        //    {
        //        base.WndProc(ref m);
        //        string file = "d:\\temp\\rd.txt";
        //        if (System.IO.File.Exists (file))
        //        System.IO.File.AppendAllText(file, m.ToString() + "\n");
        //    }
        //}
        protected PreviewHandlerBase()
        {
            // m_RNativeWindow = new ResiziNativeWindow(this);
            _previewControl = CreatePreviewHandlerControl(); // NOTE: shouldn't call virtual function from constructor; see article for more information
            _previewControl.CreateControl();
            IntPtr forceCreation = _previewControl.Handle;
        }
        protected abstract IPreviewHandlerControl CreatePreviewHandlerControl();
        private void InvokeOnPreviewThread(MethodInvoker d)
        {
            _previewControl.Invoke(d);
        }
        private void UpdateWindowBounds()
        {
            if (_showPreview)
            {
                InvokeOnPreviewThread(delegate()
                {
                    User32.SetParent(_previewControl.Handle, _parentHwnd);
                    _previewControl.Bounds = _windowBounds;
                    _previewControl.Visible = true;
                });
            }
        }
        void IPreviewHandler.SetWindow(IntPtr hwnd, ref RECT rect)
        {
            _parentHwnd = hwnd;
            //m_RNativeWindow.AssignHandle(hwnd);
            _windowBounds = rect.ToRectangle();
            UpdateWindowBounds();
        }
        void IPreviewHandler.SetRect(ref RECT rect)
        {
            _windowBounds = rect.ToRectangle();
            UpdateWindowBounds();
        }
        protected abstract void Load(IPreviewHandlerControl c);
        void IPreviewHandler.DoPreview()
        {
            _showPreview = true;
            InvokeOnPreviewThread(delegate()
            {
                try
                {
                    Load(_previewControl);
                }
                catch (Exception exc)
                {
                    _previewControl.Controls.Clear();
                    TextBox text = new TextBox();
                    text.ReadOnly = true;
                    text.Multiline = true;
                    text.Dock = DockStyle.Fill;
                    text.Text = exc.ToString();
                    _previewControl.Controls.Add(text);
                }
                UpdateWindowBounds();
            });
        }
        void IPreviewHandler.Unload()
        {
            _showPreview = false;
            InvokeOnPreviewThread(delegate()
            {
                _previewControl.Visible = false;
                _previewControl.Unload();
            });
        }
        void IPreviewHandler.SetFocus()
        {
            InvokeOnPreviewThread(delegate() { _previewControl.Focus(); });
        }
        void IPreviewHandler.QueryFocus(out IntPtr phwnd)
        {
            IntPtr result = IntPtr.Zero;
            InvokeOnPreviewThread(delegate() { result = User32.GetFocus(); });
            phwnd = result;
            if (phwnd == IntPtr.Zero) throw new Win32Exception();
        }
        uint IPreviewHandler.TranslateAccelerator(ref MSG pmsg)
        {
            if (_frame != null) return _frame.TranslateAccelerator(ref pmsg);
            const uint S_FALSE = 1;
            return S_FALSE;
        }
        void IPreviewHandlerVisuals.SetBackgroundColor(COLORREF color)
        {
            Color c = color.Color;
            InvokeOnPreviewThread(delegate() { _previewControl.BackColor = c; });
        }
        void IPreviewHandlerVisuals.SetTextColor(COLORREF color)
        {
            Color c = color.Color;
            InvokeOnPreviewThread(delegate() { _previewControl.ForeColor = c; });
        }
        void IPreviewHandlerVisuals.SetFont(ref LOGFONT plf)
        {
            Font f = Font.FromLogFont(plf);
            InvokeOnPreviewThread(delegate() { _previewControl.Font = f; });
        }
        void IOleWindow.GetWindow(out IntPtr phwnd)
        {
            phwnd = IntPtr.Zero;
            phwnd = _previewControl.Handle;
        }
        void IOleWindow.ContextSensitiveHelp(bool fEnterMode)
        {
            throw new NotImplementedException();
        }
        void IObjectWithSite.SetSite(object pUnkSite)
        {
            _unkSite = pUnkSite;
            _frame = _unkSite as IPreviewHandlerFrame;
        }
        void IObjectWithSite.GetSite(ref Guid riid, out object ppvSite)
        {
            ppvSite = _unkSite;
        }
    }
}

