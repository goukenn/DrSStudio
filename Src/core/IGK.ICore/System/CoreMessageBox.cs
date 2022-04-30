

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMessageBox.cs
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
file:CoreMessageBox.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a system messagebox
    /// </summary>
    public class CoreMessageBox
    {
        private static ICoreMessageBox sm_instance;

        public class CoreMessageBoxInstance : ICoreMessageBox         
        {
            public virtual enuDialogResult Show(Exception exception)
            {
                CoreLog.WriteLine("Exception : "+ exception.Message);
                return enuDialogResult.No;
            }
            public virtual enuDialogResult Show(string message)
            {
                CoreLog.WriteLine(message);
                return enuDialogResult.No;
            }
            public virtual enuDialogResult ShowError(CoreException ex)
            {
                CoreLog.WriteLine(ex.Message);
                return enuDialogResult.No;
            }

            /// <summary>
            /// show message box with a custom title
            /// </summary>
            /// <param name="message"></param>
            /// <param name="title"></param>
            /// <returns></returns>
            public virtual enuDialogResult Show(string message, string title)
            {
                CoreLog.WriteLine(title+":"+message);
                return enuDialogResult.No;
            }

            public virtual enuDialogResult Show(Exception ex, string title)
            {
                CoreLog.WriteLine(ex.Message );
                return enuDialogResult.No;
            }

            public virtual enuDialogResult Confirm(string message)
            {
                CoreLog.WriteLine(message);
                return enuDialogResult.Yes;
            }


            public virtual enuDialogResult ShowWarning(string title, string message)
            {
                CoreLog.WriteLine("waring : "+message);
                return enuDialogResult.Yes;
            }

            public enuDialogResult ShowInfo(string title, string message)
            {
                CoreLog.WriteLine("Info : " + message);
                return enuDialogResult.Yes;
            }

          

            public enuDialogResult ShowError(string title, string message)
            {
                CoreLog.WriteLine("Error : " + message);
                return enuDialogResult.Yes;
            }


            public virtual enuDialogResult Show(string message, string title, enuCoreMessageBoxButtons boxbutton)
            {
                CoreLog.WriteLine("Message : " + message);
                return enuDialogResult.Yes;
            }


            public virtual void NotifyMessage(string title, string message)
            {
                CoreLog.WriteLine(string.Format ("[Notify] {0} - {1}",title,  message));
            }
        }
        private CoreMessageBox()
        {
        }
        static CoreMessageBox()
        {
            Type t = CoreSystemEnvironment.GetEntryAssembly().FindAttribute<CoreMessageBoxAttribute>();
            if (t == null)
            {
                CoreLog.WriteLine("No MessageBoxFound .... ");
                sm_instance = new CoreMessageBoxInstance();
            }
            else
            {
                sm_instance = t.Assembly.CreateInstance(t.FullName) as ICoreMessageBox;
                if (sm_instance is CoreMessageBox)
                {
                    CoreMessageBoxAttribute attr = Attribute.GetCustomAttribute(t, typeof(CoreMessageBoxAttribute)) as CoreMessageBoxAttribute;
                }
            }
        }
        public static enuDialogResult Show(Exception ex)
        {
            return sm_instance.Show(ex);
            //show Exception
            //return enuDialogResult.None;
        }
        public static enuDialogResult Confirm(string message){
            return sm_instance.Confirm(message);
        }
        public static enuDialogResult Show(string message)
        {
            return sm_instance.Show(message);
        }
        public static ICoreMessageBox GetInstance()
        {
            return sm_instance;
        }
        public static enuDialogResult Show(string message, string title, enuCoreMessageBoxButtons boxbutton)
        {
            return sm_instance.Show(message, title, boxbutton );
        }
        public static enuDialogResult Show(Exception ex, string title)
        {
            return sm_instance.Show(ex, title);
        }
        /// <summary>
        /// show the message box
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static enuDialogResult Show(string message, string title)
        {
            return sm_instance.Show(message, title);
        }

        internal static void Show(string message, string title, enuCoreMessageBoxType enuMessageBoxType)
        {
            switch (enuMessageBoxType)
            {
                case enuCoreMessageBoxType.Info:
                    sm_instance.ShowInfo(title, message);
                    break;
                case enuCoreMessageBoxType.Warning:
                    sm_instance.ShowWarning (title, message);
                    break;
                case enuCoreMessageBoxType.Error:
                    sm_instance.ShowError(title, message);
                    break;
                default:
                    break;
            }        }

        /// <summary>
        /// notify message error
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public static void NotifyMessage(string title, string message)
        {
            if (sm_instance != null)
            {
                sm_instance.NotifyMessage(title, message);
            }
        }
    }
}

