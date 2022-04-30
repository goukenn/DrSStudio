


using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidLinearLayout.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IGK.DrSStudio.Android
{


    public class AndroidLinearLayout : AndroidLayout
    {
        private enuAndroidOrientation m_Orientation;

        public enuAndroidOrientation Orientation
        {
            get { return m_Orientation; }
            set
            {
                if (m_Orientation != value)
                {
                    m_Orientation = value;
                    InitLayout();
                }
            }
        }
        public AndroidLinearLayout():base()
        {
      
        }
        protected override void InitLayout()
        {
            float x = 0;
            float y = 0;
            float w = 0;
            float h = 0;
            Rectanglef v_rc = Rectanglef.Empty;
            foreach (AndroidLayoutItem item in this.Elements)
            {
                w = item.Bounds.Width;
                h = item.Bounds.Height;
                v_rc = new Rectanglef(x, y, item.Bounds.Width, item.Bounds.Height);
                item.SuspendLayout();
                item.Bounds = v_rc;
                item.ResumeLayout();

                if (this.m_Orientation == enuAndroidOrientation.Vertical)
                    y += h;
                else
                    x += w;
            }
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.DefaultStringKey = "android.linear.layout";
            this.m_Orientation = enuAndroidOrientation.Vertical;
#if DEBUG
            this.Elements.Add(new AndroidButton());
            this.Elements.Add(new AndroidButton());
            this.Elements.Add(new AndroidButton());
            this.Elements.Add(new AndroidButton());
#endif
        }
    }
}
