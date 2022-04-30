using System;
using System.Collections;

namespace IGK.ICore.Extensions
{
    public static class WorkingObjectExtension
    {

        public static object GetElementById(this Array Elements, string id)
        {
            foreach (ICoreIdentifier item in Elements)
            {
                if (item.Id == id)
                    return item;
                if (item is ICoreWorkingElementContainer)
                {
                    var b = (item as ICoreWorkingElementContainer).GetElementById(id);
                    if (b != null)
                        return b;
                }
            }
            return null;
        }
        public static object GetElementById(this IList Elements, string id)
        {
            foreach (ICoreIdentifier item in Elements)
            {
                if (item.Id == id)
                    return item;
                if (item is ICoreWorkingElementContainer)
                {
                    var b = (item as ICoreWorkingElementContainer).GetElementById(id);
                    if (b != null)
                        return b;
                }
            }
            return null;
        }

        public static T GetElementById<T>(this IList Elements, string id) where T : class
        {
            var o = Elements?.GetElementById(id);
            return o as T;
        }

    }
}
