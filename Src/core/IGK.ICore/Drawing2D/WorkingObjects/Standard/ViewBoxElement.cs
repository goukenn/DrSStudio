

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ViewBoxElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingStandardElement("ViewBox", typeof(Mecanism))]
    public  class ViewBoxElement : 
        RectangleElement  , 
        ICore2DDrawingVisitable ,
        ICoreWorkingElementContainer,
        ICoreSerializerAdditionalPropertyService,
        ICore2DDrawingViewPort 
        
    {
        private ViewBoxElementCollection m_Elements;
        private string m_DefaultStringCaption;


        public T GetElementById<T>(string id) where T : class
        {
            var o = this.m_Elements.GetElementById(id);
            return o as T;
        }
        public object GetElementById(string name)
        {
            return GetElementById<Object>(name);
        }

        public string DefaultStringKey
        {
            get { return m_DefaultStringCaption; }
            set
            {
                if (m_DefaultStringCaption != value)
                {
                    m_DefaultStringCaption = value;
                }
            }
        }
        [CoreXMLElement()]
        public ViewBoxElementCollection Elements
        {
            get { return m_Elements; }
        }
        private bool m_Clipped;

        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (false )]
        [CoreConfigurableProperty(Group="Default")]
        /// <summary>
        /// Get or set if this view box is clipped on the current document
        /// </summary>
        public bool Clipped
        {
            get { return m_Clipped; }
            set
            {
                if (m_Clipped != value)
                {
                    m_Clipped = value;

                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        private void UnRegisterElementEvent(Core2DDrawingObjectBase core2DDrawingObject)
        {
            core2DDrawingObject.PropertyChanged -= core2DDrawingObject_PropertyChanged;
        }

        private void RegisterElementEvent(Core2DDrawingObjectBase core2DDrawingObject)
        {
            core2DDrawingObject.PropertyChanged += core2DDrawingObject_PropertyChanged;
        }

        void core2DDrawingObject_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            OnPropertyChanged(Core2DDrawingChangement.Definition);
        }
        public ViewBoxElement():base()
        {
            
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_DefaultStringCaption = "msg.viewbox.elementrequired";
            this.m_Elements = new ViewBoxElementCollection(this); 
        }
        public override void Dispose()
        {
            if (this.m_Elements != null)
            {
                this.m_Elements.Clear();
            }
            base.Dispose();
        }

        public sealed class ViewBoxElementCollection :
            CoreWorkingObjectCollections<Core2DDrawingLayeredElement>,
          ICoreWorkingElementCollections,
          ICoreSerializable
        {
            private ViewBoxElement  m_viewBoxElement;
            public ViewBoxElementCollection(ViewBoxElement groupElement)
            {
                this.m_viewBoxElement = groupElement;
            }
            public void Serialize(IXMLSerializer seri)
            {
                foreach (ICoreSerializerService item in this.Elements)
                {
                    if (item == null)
                        continue;
                    item.Serialize(seri);
                }
            }
            public ICoreWorkingObject this[int index]
            {
                get
                {
                    return this.Elements[index];
                }
            }
            public bool IsReadOnly
            {
                get { return false; }
            }
            public override void Add(Core2DDrawingLayeredElement element)
            {
                if (element == null)
                    return;
                if (element.Parent != this.m_viewBoxElement)
                {
                    base.Add(element);
                    element.Parent = this.m_viewBoxElement;
                    this.m_viewBoxElement.OnElementAdded(new CoreItemEventArgs<Core2DDrawingLayeredElement>(element));
                    this.m_viewBoxElement.InitElement();
                }
            }
            public override void Remove(Core2DDrawingLayeredElement element)
            {
                if (this.Contains(element))
                {
                    base.Remove(element);
                    this.m_viewBoxElement.OnElementRemoved(new CoreItemEventArgs<Core2DDrawingLayeredElement>(element));
                }

            }
            internal object GetElementById(string id)
            {
                foreach (var item in this.Elements)
                {
                    if (item.Id == id)
                        return item;
                }
                return null;
            }
        }


        ICoreWorkingElementCollections ICoreWorkingElementContainer.Elements
        {
            get { return this.m_Elements; }
        }

       new class Mecanism : RectangleElement.Mecanism
       { 
       }

       protected override void WriteElements(IXMLSerializer xwriter)
       {
           base.WriteElements(xwriter);
       }
       protected override void ReadElements(IXMLDeserializer xreader)
       {
           this.m_Elements.Clear();
           base.ReadElements(xreader);
       }

       public bool Accept(ICore2DDrawingVisitor visitor)
       {
           return visitor != null;
       }

       public void Visit(ICore2DDrawingVisitor visitor)
       {
           if (visitor == null) return;

           Object obj = visitor.Save();
           visitor.SetupGraphicsDevice(this);
           Matrix m = this.GetMatrix().Clone() as Matrix ;
           visitor.MultiplyTransform(m, enuMatrixOrder.Prepend);
           m.Dispose();
           if (this.Clipped )
           visitor.SetClip(this.Bounds);
           if (this.Elements.Count >0)
           {
               float dx = this.Bounds.X;
               float dy = this.Bounds.Y;
               visitor.TranslateTransform(dx, dy, enuMatrixOrder.Prepend);
               foreach (Core2DDrawingLayeredElement  item in this.m_Elements)
               {
                   item.Draw(visitor);
               }
           }
           else
           {
               CoreFont ft = CoreFont.CreateFont("consolas", 8, enuFontStyle.Regular, enuRenderingMode.Vector);
               ft.HorizontalAlignment = enuStringAlignment.Center;
               ft.VerticalAlignment = enuStringAlignment.Center;
               visitor.DrawString(this.DefaultStringKey.R(), ft,
                   CoreBrushes.Black,
                   this.Bounds);
               ft.Dispose();
           }
           visitor.Restore(obj);
       }

       public CoreReadAdditionalElementPROC GetProc()
       {
           return (CoreReadAdditionalElementPROC)ReadChild;
       }
       protected virtual bool ReadChild(IXMLDeserializer seri)
       {
           var t = CoreXMLSerializerUtility.GetObject(seri);
           if (t != null)
           {
               if (t is Core2DDrawingLayeredElement)
               {
                   this.m_Elements.Add(t as Core2DDrawingLayeredElement);
                   return true;
               }
           }
           return false;
       }


       protected virtual void OnElementRemoved(CoreItemEventArgs<Core2DDrawingLayeredElement> e)
       {

           this.UnRegisterElementEvent(e.Item);
           if (this.ElementRemoved != null)
           {
               this.ElementRemoved(this, e);
           }
           OnPropertyChanged(Core2DDrawingChangement.Definition);
       }

       protected virtual void OnElementAdded(CoreItemEventArgs<Core2DDrawingLayeredElement> e)
       {
           this.RegisterElementEvent(e.Item);
           if (this.ElementAdded != null)           
           {
               this.ElementAdded(this, e);
           }
           OnPropertyChanged(Core2DDrawingChangement.Definition);
       }
       public event EventHandler<CoreItemEventArgs<Core2DDrawingLayeredElement>> ElementAdded;
       public event EventHandler<CoreItemEventArgs<Core2DDrawingLayeredElement>> ElementRemoved;

       public void Remove(ICoreWorkingObject obj)
       {
           var s = obj as Core2DDrawingLayeredElement;
           if ((s != null) && (this.m_Elements.Contains(s)))
           {
               this.m_Elements.Remove(s);
               this.InitElement();
               OnPropertyChanged(Core2DDrawingChangement.Definition);
           }
       }

       public System.Collections.IEnumerator GetEnumerator()
       {
           return this.m_Elements.GetEnumerator();
       }
    }
}
