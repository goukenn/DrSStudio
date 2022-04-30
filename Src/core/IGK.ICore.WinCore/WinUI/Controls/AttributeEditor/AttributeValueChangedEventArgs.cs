using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    /// <summary>
    /// represent a attribute value editor
    /// </summary>
    public class AttributeValueChangedEventArgs : EventArgs 
    {
        private IGKXAttributeEditor m_Editor;
        private IGKXAttributeEditor.AttributeItem m_Item;

        public IGKXAttributeEditor.AttributeItem Item { get { return this.m_Item; } }
        public IGKXAttributeEditor Editor
        {
            get { return m_Editor; }           
        }
        
        public AttributeValueChangedEventArgs(IGKXAttributeEditor editor, IGKXAttributeEditor.AttributeItem item)
        {
            this.m_Editor = editor;
            this.m_Item = item;
        }

    }
}
