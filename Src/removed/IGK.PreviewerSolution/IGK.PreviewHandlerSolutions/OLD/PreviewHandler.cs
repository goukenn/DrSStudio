

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PreviewHandler.cs
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
file:PreviewHandler.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
namespace IGK.PreviewHandlerLib
{
    using IGK.ICore;using IGK.PreviewHandlerLib.WinUI;
    using System.ComponentModel;
    public abstract class PreviewHandler : IPreviewHandler, IPreviewHandlerVisuals, IOleWindow, IObjectWithSite
    {
        private bool _showPreview;
        private PreviewHandlerControl _previewControl;
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
        protected PreviewHandler()
        {
           // m_RNativeWindow = new ResiziNativeWindow(this);
            _previewControl = CreatePreviewHandlerControl(); // NOTE: shouldn't call virtual function from constructor; see article for more information
            _previewControl.CreateControl();
            IntPtr forceCreation = _previewControl.Handle;
        }
        protected abstract PreviewHandlerControl CreatePreviewHandlerControl();
        private void InvokeOnPreviewThread(MethodInvoker d)
        {
            _previewControl.Invoke(d);
        }
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        private void UpdateWindowBounds()
        {
            if (_showPreview)
            {
                InvokeOnPreviewThread(delegate()
                {
                    SetParent(_previewControl.Handle, _parentHwnd);
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
        protected abstract void Load(PreviewHandlerControl c);
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
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetFocus();
        void IPreviewHandler.QueryFocus(out IntPtr phwnd)
        {
            IntPtr result = IntPtr.Zero;
            InvokeOnPreviewThread(delegate() { result = GetFocus(); });
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
        [ComRegisterFunction]
        public static void Register(Type t)
        {
            if (t != null && t.IsSubclassOf(typeof(PreviewHandler)))
            {
                object[] attrs = (object[])t.GetCustomAttributes(typeof(PreviewHandlerAttribute), true);
                if (attrs != null && attrs.Length == 1)
                {
                    PreviewHandlerAttribute attr = attrs[0] as PreviewHandlerAttribute;
#if DEBUG
                    Console.WriteLine("Register " + t.FullName); 
#endif
                    RegisterPreviewHandler(attr.Name, attr.Extension, t.GUID.ToString("B"), attr.AppId);
                }
            }
        }
        [ComUnregisterFunction]
        public static void Unregister(Type t)
        {
            if (t != null && t.IsSubclassOf(typeof(PreviewHandler)))
            {
                object[] attrs = (object[])t.GetCustomAttributes(typeof(PreviewHandlerAttribute), true);
                if (attrs != null && attrs.Length == 1)
                {
                    PreviewHandlerAttribute attr = attrs[0] as PreviewHandlerAttribute;
                    UnregisterPreviewHandler(attr.Extension, t.GUID.ToString("B"), attr.AppId);
                }
            }
        }
        protected static void RegisterPreviewHandler(string name, string extensions, string previewerGuid, string appId)
        {
            // Create a new prevhost AppID so that this always runs in its own isolated process
            using (RegistryKey appIdsKey = Registry.ClassesRoot.OpenSubKey("AppID", true))
            using (RegistryKey appIdKey = appIdsKey.CreateSubKey(appId))
            {
                appIdKey.SetValue("DllSurrogate", @"%SystemRoot%\system32\prevhost.exe", RegistryValueKind.ExpandString);
            }
            // Add preview handler to preview handler list
            using (RegistryKey handlersKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\PreviewHandlers", true))
            {
                handlersKey.SetValue(previewerGuid, name, RegistryValueKind.String);
            }
            // Modify preview handler registration
            using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID"))
            using (RegistryKey idKey = clsidKey.OpenSubKey(previewerGuid, true))
            {
                idKey.SetValue("DisplayName", name, RegistryValueKind.String);
                idKey.SetValue("AppID", appId, RegistryValueKind.String);
                idKey.SetValue("DisableLowILProcessIsolation", 1, RegistryValueKind.DWord); // optional, depending on what preview handler needs to be able to do
            }
            foreach (string extension in extensions.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Trace.WriteLine("Registering extension '" + extension + "' with previewer '" + previewerGuid + "'");
                // Set preview handler for specific extension
                using (RegistryKey extensionKey = Registry.ClassesRoot.CreateSubKey(extension))
                {
                    extensionKey.SetValue(null, string.Empty );
                    using (RegistryKey shellexKey = extensionKey.CreateSubKey("shellex"))
                    using (RegistryKey previewKey = shellexKey.CreateSubKey("{8895b1c6-b41f-4c1c-a562-0d564250836f}"))
                    {
                        previewKey.SetValue(null, previewerGuid, RegistryValueKind.String);
                    }
                }
            }
        }
        protected static void UnregisterPreviewHandler(string extensions, string previewerGuid, string appId)
        {
            foreach (string extension in extensions.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Trace.WriteLine("Unregistering extension '" + extension + "' with previewer '" + previewerGuid + "'");
                using (RegistryKey shellexKey = Registry.ClassesRoot.OpenSubKey(extension + "\\shellex", true))
                {
                    shellexKey.DeleteSubKey("{8895b1c6-b41f-4c1c-a562-0d564250836f}",false );
                }
            }
            using (RegistryKey appIdsKey = Registry.ClassesRoot.OpenSubKey("AppID", true))
            {
                appIdsKey.DeleteSubKey(appId,false );
            }
            using (RegistryKey classesKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\PreviewHandlers", true))
            {
                classesKey.DeleteValue(previewerGuid,false );
            }
        }
    }
}

