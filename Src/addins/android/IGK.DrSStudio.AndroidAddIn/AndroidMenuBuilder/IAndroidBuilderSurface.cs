using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder
{
    public interface  IAndroidBuilderSurface
    {
        void AddNewMenuItem();
        void ClearAllMenu();
        void SaveMenu();
    }
}
