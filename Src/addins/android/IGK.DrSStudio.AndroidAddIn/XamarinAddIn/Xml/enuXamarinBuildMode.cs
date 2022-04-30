using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.Xamarin.Xml
{
    public enum enuXamarinBuildMode
    {
        None,
        Compile,
        Reference,        
        Include,
        EmbeddedResource,
        AndroidResource,
        /// <summary>
        /// represent a special building type
        /// </summary>
        //Folder,
    }
}
