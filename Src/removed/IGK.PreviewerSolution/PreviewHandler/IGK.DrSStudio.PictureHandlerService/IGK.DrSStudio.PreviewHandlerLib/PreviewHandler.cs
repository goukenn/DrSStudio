

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
//
// Thanks to : Stephen Toub form MsdnMag
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Microsoft.Win32;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Diagnostics;
using System.Security.Permissions;
namespace IGK.PrevHandlerLib
{
    [RegistryPermissionAttribute(SecurityAction.Demand, Unrestricted = true)]
    public abstract class PreviewHandler : IPreviewHandler, IPreviewHandlerVisuals, IOleWindow, IObjectWithSite
    {
        const string BASE_GUID_HANDLERTYPE = "{8895b1c6-b41f-4c1c-a562-0d564250836f}";
        #region "NATIVE FUNCTION"
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        #endregion 
        private bool _showPreview;
        private PreviewHandlerControl _previewControl;
        private IntPtr _parentHwnd;
        private Rectangle _windowBounds;
        private object _unkSite;
        private IPreviewHandlerFrame _frame;
        protected PreviewHandler()
        {
            _previewControl = CreatePreviewHandlerControl(); // NOTE: shouldn't call virtual function from constructor; see article for more information
            IntPtr forceCreation = _previewControl.Handle;
            _previewControl.BackColor = SystemColors.Window;
        }
        protected abstract PreviewHandlerControl CreatePreviewHandlerControl();
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
                    SetParent(_previewControl.Handle, _parentHwnd);
                    _previewControl.Bounds = _windowBounds;
                    _previewControl.Visible = true;
                });
            }
        }
        void IPreviewHandler.SetWindow(IntPtr hwnd, ref RECT rect)
        {
            _parentHwnd = hwnd;
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
                    RegisterPreviewHandler(attr.Name, attr.Extension, t.GUID.ToString("B"), attr.AppId);
                    PreviewHandler prev = t.Assembly.CreateInstance(t.FullName) as PreviewHandler;
                    prev.Register();
                }
            }
        }
        protected virtual  void Register()
        {
        }
        protected virtual void UnRegister() { 
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
                    PreviewHandler prev = t.Assembly.CreateInstance(t.FullName) as PreviewHandler ;
                    prev.UnRegister();
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
            using (RegistryKey clsidKey = Registry.ClassesRoot.OpenSubKey("CLSID", true))
            using (RegistryKey idKey = clsidKey.OpenSubKey(previewerGuid,true))
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
                   // extensionKey.SetValue(null, string.Empty );
                    using (RegistryKey shellexKey = extensionKey.CreateSubKey("shellex"))
                    using (RegistryKey previewKey = shellexKey.CreateSubKey(BASE_GUID_HANDLERTYPE))
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
                    try
                    {
                        shellexKey.DeleteSubKey(BASE_GUID_HANDLERTYPE);
                    }
                    catch (Exception ex){
                        Trace.WriteLine("Unregistering extension failed : '" + ex.Message  + "' with previewer '" + previewerGuid + "'");
                    }
                }
            }
            try
            {
                using (RegistryKey appIdsKey = Registry.ClassesRoot.OpenSubKey("AppID", true))
                {
                    try
                    {
                        appIdsKey.DeleteSubKey(appId);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine("Unregistering extension failed : '" + ex.Message + "' with previewer '" + previewerGuid + "'");
                    }
                }
            }
            catch (Exception ex){
                Debug.WriteLine(ex.Message);
            }
            using (RegistryKey classesKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\PreviewHandlers", true))
            {
                try
                {
                    classesKey.DeleteValue(previewerGuid);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Unregistering extension failed : '" + ex.Message + "' with previewer '" + previewerGuid + "'");
                }
            }
        }
    }
}

