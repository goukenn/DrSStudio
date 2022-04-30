

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidJavaFileCodeBuilder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Integration;

using IGK.ICore.WinUI;
using System.IO;
using ICSharpCode.AvalonEdit.CodeCompletion;
using IGK.DrSStudio.Android.CodeCompletion;
using IGK.ICore;
namespace IGK.DrSStudio.Android.WinUI
{
    [CoreSurface ("AndroidJavaCodeEditorSurface", EnvironmentName=AndroidConstant.ENVIRONMENT)]
    class AndroidJavaFileCodeBuilder : AndroidCodeFileBuilder, ICoreWorkingFilemanagerSurface 
    {

        
        
        public AndroidJavaFileCodeBuilder():base()
        {
            this.InitializeComponent();
          
                
        }

        protected override void InitControl()
        {
            this.TextEditor.SyntaxHighlighting =
        ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinitionByExtension(".java");
        }

        

      
        CompletionWindow m_completionWindow;

        void TextArea_TextEntered(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Text == ".")
            {
                // Open code completion after the user has pressed dot:
                m_completionWindow = new CompletionWindow(this.TextEditor.TextArea);
                IList<ICompletionData> data = m_completionWindow.CompletionList.CompletionData;
                data.Add(new AndroidJavaCompletionData("this"));
                data.Add(new AndroidJavaCompletionData("else"));
                data.Add(new AndroidJavaCompletionData("void"));
                m_completionWindow.Show();
                m_completionWindow.Closed += delegate
                {
                    m_completionWindow = null;
                };
            }
        }

        void TextArea_TextEntering(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && m_completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    m_completionWindow.CompletionList.RequestInsertion(e);
                }
            }
        }

        private void InitializeComponent()
        {
           

        }
      


        public override  ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo(
                "Save source file As...",
                "JavaFile|*.java",
                this.FileName 
                );
        }

    

    }
}
