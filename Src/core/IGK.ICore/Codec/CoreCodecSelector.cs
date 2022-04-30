

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreCodecSelector.cs
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
file:CoreCodecSelector.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;

    public sealed class CoreCodecSelector : CoreWorkingObjectBase , ICoreWorkingConfigurableObject 
    {
        ICoreCodec[] codec;
        private ICoreCodec m_SelectedCodec;
        public ICoreCodec SelectedCodec
        {
            get { return m_SelectedCodec; }
            set
            {
                if (m_SelectedCodec != value)
                {
                    m_SelectedCodec = value;
                }
            }
        }
        public CoreCodecSelector(ICoreCodec[] codec)
        {
            if ((codec == null) || (codec.Length == 0))
                throw new CoreException(enuExceptionType.ArgumentIsNull, "codec");
            this.codec = codec;
        }
        #region ICoreWorkingConfigurableObject Members
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterGroup group =  parameters.AddGroup("Codecs");
            object obj = CoreControlFactory.CreateControl("IGKXListBox");
            ICoreListBox c = obj as ICoreListBox;
            if (c != null)
            {
                c.Width = 210;
                foreach (ICoreCodec vc in codec)
                {
                    c.AddItem(new ListCodecItem(vc));
                }
                c.SelectedItem = c.GetItemAt(0);
                this.m_SelectedCodec = codec[0];
                c.SelectedIndexChanged += new EventHandler(c_SelectedIndexChanged);
                group.AddItem("Codec", "lb.codec.caption", c);
            }
            else {
                CoreLog.WriteLine("No Group listbox ");
            }
            return parameters;
        }

        class ListCodecItem
        {
            private ICoreCodec vc;
            /// <summary>
            /// get the codec
            /// </summary>
            public ICoreCodec Codec { get { return this.vc; } }

            public ListCodecItem(ICoreCodec vc)
            {
                this.vc = vc;
            }
            public override string ToString()
            {
                return string.Format ("{0} ({1})", vc.Id, vc.MimeType);
            }
        }
        void c_SelectedIndexChanged(object sender, EventArgs e)
        {
            var h = (sender as ICoreListBox).SelectedItem as ListCodecItem;
            this.m_SelectedCodec  = h.Codec as ICoreCodec ;
        }
        public ICoreControl GetConfigControl()
        {
            return null;
        }
        #endregion
        #region ICoreIdentifier Members
        public override string Id
        {
            get { return "WorkingObject.CodecSelector"; }
            set {
                base.Id = value;
            }
        
        }
        #endregion
    }
}

