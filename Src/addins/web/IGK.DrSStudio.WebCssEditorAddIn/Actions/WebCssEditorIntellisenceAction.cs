

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebCssEditorIntellisenceAction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
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
﻿using ICSharpCode.AvalonEdit.CodeCompletion;
using IGK.ICore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WebCssEditorAddIn.Actions
{
    class WebCssEditorIntellisenceAction : WebCssEditorActions
    {
          CompletionWindow m_codeWindow;
        private IList<ICompletionData> m_data;
        private Dictionary<string, WebCssAttributeDefinition> m_attributeDefinitions;

        public WebCssEditorIntellisenceAction()
        {
            this.m_attributeDefinitions = new Dictionary<string, WebCssAttributeDefinition>();
        }
        protected override bool PerformAction()
        {
            if (m_codeWindow == null)           
            {
                m_codeWindow = new CompletionWindow(this.TextArea);
                m_codeWindow.ShowInTaskbar = false;
                m_codeWindow.Closed += m_codeWindow_Closed;
                m_data = m_codeWindow.CompletionList.CompletionData;
                this.CurrentSurface.Disposed += Surface_Disposed;
            }
            initCompletionData();
             
            m_codeWindow.Show();                
            return false;
        }

        void Surface_Disposed(object sender, EventArgs e)
        {
            this.CurrentSurface.Disposed -= Surface_Disposed;
            if (this.m_codeWindow !=null)
                this.m_codeWindow.Close();
        }

        void m_codeWindow_Closed(object sender, EventArgs e)
        {
            this.CurrentSurface.Disposed -= Surface_Disposed;
            this.m_codeWindow = null;
        }

        /*
         * 
         * init completions window from android manifest propertiss list found in 
         * <android-sdk>/platforms/<target-platform>/data/res/manifest_attrs.xml
         * 
         * 
         * */
        private void initCompletionData()
        {
            
            System.Collections.IEnumerator e = WebCssAttributeManager.GetDefinitions();
            while(e.MoveNext())
            {
            //foreach (KeyValuePair<string, WebCssAttributeDefinition> item in m_attributeDefinitions)
            //{
                WebCssAttributeDefinition item = ((KeyValuePair<string, WebCssAttributeDefinition>)e.Current).Value;
                if ((item!=null) && (item.PropertyType != "cssEnumValues"))
                {
                    this.m_data.Add(new WebCssCmpletionData(item.Name, item));
                    //m_attributeDefinitions.Add(item.Name, item);
                }
            }
                
            
        }


        public override enuKeys Key
        {
            get {
                return (enuKeys)CoreSettings.GetSettingValue("EnvironmentShortCut.ShowIntellisence", enuKeys.Control | enuKeys.Space);
            }
        }

    
    }
}
