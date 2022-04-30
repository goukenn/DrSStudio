
using IGK.DrSStudio.Drawing2D;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent a code bar element base
    /// </summary>
    public abstract class CodeBarElementBase : RectangleElement,
        ICoreCodeBarElement,
         ICodeBar,
        ICoreTextElement
    {

        private string m_Value;
        private CoreFont m_Font;
        private bool m_ShowValue;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(false)]
        [CoreConfigurableProperty (Group="Definition")]
        public bool ShowValue
        {
            get { return m_ShowValue; }
            set
            {
                if (m_ShowValue != value)
                {
                    m_ShowValue = value;
                    OnPropertyChanged(
                     CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public CoreFont Font
        {
            get { return m_Font; }
        }
        ICoreFont ICoreTextElement.Font
        {
            get { return this.Font; }
        }
        [CoreXMLAttribute()]
        [CoreConfigurableProperty(Group="Text")]
        public virtual string Text
        {
            get { return m_Value; }
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var c = base.GetParameters(parameters);
          
            ICoreParameterItem item = parameters.GetItem("Text");
            if (item != null)
            {
                var s = item.Parent as ICoreParameterGroup;
                s.ReplaceItem("Text", "lb.Text", enuParameterType.MultiTextLine, (o, e) =>
                {
                    this.Text = ((string)e.Value) ?? string.Empty;
                });
            }
            else
            {
                var g = c.AddGroup("Text");
                g.ReplaceItem("Text", "lb.Text", enuParameterType.MultiTextLine, (o, e) =>
                {
                    this.Text = ((string)e.Value) ?? string.Empty;
                });
            }
            return c;
        }
        public CodeBarElementBase():base()
        {

        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            
            this.m_Font = CoreFont.CreateFrom(CoreFont.DefaultFontName, this);
            this.m_Font.FontSize = 12;
            this.m_ShowValue = false;
            this.SmoothingMode = enuSmoothingMode.None;
            this.FillBrush.SetSolidColor(Colorf.Black);
            this.StrokeBrush.SetSolidColor(Colorf.White);
            this.m_Font.FontDefinitionChanged += _FontDefinitionChanged;
        }
        void _FontDefinitionChanged(object sender, EventArgs e)
        {
            if (this.IsEditing)
                this.InitElement();                
            OnPropertyChanged(Core2DDrawingChangement.FontChanged);
        }
        protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (!this.IsLoading &&  (e.ID == Core2DDrawingChangement.FontChanged.ID ))
            {
                this.InitElement();
            }
            base.OnPropertyChanged(e);
        }
        public new class Mecanism : RectangleElement.Mecanism
        {

        }
    }
}
