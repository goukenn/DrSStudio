using IGK.DrSStudio.Drawing2D;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder
{
    public class AndroidMenuItemViewAdapter : IListViewDataAdapter
    {
        private List<string> m_list;
        public AndroidMenuItemViewAdapter()
        {
            this.m_list = new List<String>();
            m_list.Add("Info 1 ");
            m_list.Add("Info 2 ");
            m_list.Add("Info 3 ");
            m_list.Add("Info 4 ");
        }
        public int Count
        {
            get { return this.m_list.Count; }
        }

        /// <summary>
        /// get the object item represented by a view
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public object GetItem(int position) {
            return this.m_list[position];
        }

        public ICore2DDrawingLayeredElement GetView(IGK.ICore.WinUI.ICoreWorkingApplicationContextSurface context, int position)
        {
            RectangleElement rc = new RectangleElement();
            rc.Bounds = new IGK.ICore.Rectanglef(0, 0, 150, "1cm".ToPixel());
            rc.FillBrush.SetSolidColor(Colorf.IndianRed);
            return rc;
        }
    }
}
