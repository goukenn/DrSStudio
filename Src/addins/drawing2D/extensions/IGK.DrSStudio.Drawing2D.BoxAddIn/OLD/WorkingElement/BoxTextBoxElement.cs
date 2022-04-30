

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BoxTextBoxElement.cs
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
file:BoxTextBoxElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{   
    /// <summary>
    /// represent a text box round element
    /// </summary>
    [BoxCategoryAttribute("TextBox", typeof(Mecanism), ImageKey = "DE_TextBox")]
    public class BoxTextBoxElement : 
        RoundRectangleElement, 
        ICore2DElementsContainer,
        ICore2DTextElement ,
        ICoreWorkingElementContainer
    {
        TextElement m_textElement;
        Items m_items;
        private bool m_AutoSize;
        /// <summary>
        /// get or set if this elemenet is auto sized
        /// </summary>
        public bool AutoSize
        {
            get { return m_AutoSize; }
            set
            {
                if (m_AutoSize != value)
                {
                    m_AutoSize = value;
                }
            }
        }
        public BoxTextBoxElement()
        {
            m_textElement = new TextElement();
            m_items = new Items(this, m_textElement);
            m_textElement.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(m_textElement_PropertyChanged);
        }
        void m_textElement_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            this.OnPropertyChanged(e);
        }
        protected override void BuildBeforeResetTransform()
        {
            base.BuildBeforeResetTransform();
            this.m_textElement.Bound = this.Bound ;
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            base.InitGraphicPath(p);
            this.m_textElement.InitElement();
        }
        protected override void GeneratePath()
        {
            //attach bound            
            this.m_textElement.Bound = this.Bound;
            base.GeneratePath();
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            parameters.AddGroup(CoreConstant.PARAM_DEFINITION).AddItem (GetType().GetProperty ("Content" ));
            var tab = parameters.AddTab("TextElement", "TextElement");                        
            tab.AddConfigObject(this.m_textElement);
            return parameters;
        }
        protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            foreach (ICore2DDrawingLayeredElement item in this.m_items )
            {
                item.Serialize(xwriter);
            }
        }
        protected override void ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            if (xreader.NodeType == System.Xml.XmlNodeType.None)
                xreader.MoveToContent();
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.ReadElements(this, xreader, ReadAdditionnalPROC);
        }
        bool ReadAdditionnalPROC(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        { 
            switch(xreader.Name .ToLower ())
            {
                case "text":            
                this.m_textElement.Deserialize ( xreader );
                return true;
            }
            return false;
        }
        //public override void Draw(Graphics g)
        //{
        //    base.Draw(g);
        //    System.Drawing.Drawing2D.GraphicsState v_state = g.Save();
        //    g.MultiplyTransform(this.GetMatrix(), System.Drawing.Drawing2D.MatrixOrder.Prepend);
        //    this.m_textElement.Render (g);
        //    g.Restore(v_state);
        //}
        public override void Dispose()
        {
            this.m_textElement.Dispose();
            base.Dispose();            
        }
        new class Mecanism : RoundRectangleElement.Mecanism 
        { 
        }
        sealed class Items : ICore2DElementsContainerCollections, ICoreWorkingElementCollections
        {
            BoxTextBoxElement owner;
            List<TextElement > m_text;
            public Items(BoxTextBoxElement owner,TextElement item )
            {
                this.owner = owner;
                this.m_text = new List<TextElement> ();
                this.m_text.Add (item);
            }
            #region ICoreWorkingObjectCollections Members
            public int Count
            {
                get { return 1; }
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_text.GetEnumerator();
            }
            #endregion
            #region ICore2DElementsContainerCollections Members
            public ICore2DDrawingElement this[int index]
            {
                get {
                    if ((index >= 0) && (index < this.Count))
                        return this.m_text[index];
                    return null;
                }
            }
            public ICore2DDrawingElement[] ToArray()
            {
                return this.m_text.ToArray();
            }
            public bool Remove(ICore2DDrawingElement element)
            {
                return false;
            }
            #endregion
            public bool IsReadOnly
            {
                get { return true; }
            }
            ICoreWorkingObject ICoreWorkingElementCollections.this[int index]
            {
                get { return this[index]; }
            }
        }
        #region ICore2DElementsContainer Members
        ICore2DElementsContainerCollections ICore2DElementsContainer.Elements
        {
            get { return this.m_items; }
        }
        #endregion
        #region ICore2DTextElement Members
        public string Content
        {
            get
            {
                return this.m_textElement.Content;
            }
            set
            {
                this.m_textElement.Content = value;
            }
        }
        public bool AutoAdjust
        {
            get
            {
                return this.m_textElement.AutoAdjust;
            }
            set
            {
                this.m_textElement.AutoAdjust = value;
            }
        }
        public enuTextElementRenderingMode RenderingMode
        {
            get
            {
                return this.m_textElement.RenderingMode;
            }
            set
            {
                this.m_textElement.RenderingMode = value;
            }
        }
        #endregion
        #region ICoreTextElement Members
        public ICoreFont Font
        {
            get { return this.m_textElement.Font; }
        }
        #endregion
        //ICoreWorkingObjectCollections ICoreWorkingObjectElementContainer.Elements
        //{
        //    get { return this.m_items; }
        //}
        ICoreWorkingElementCollections ICoreWorkingElementContainer.Elements
        {
            get { return this.m_items; }
        }
    }
}

