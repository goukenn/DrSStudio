

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CorePositionnableUtility.cs
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
file:CorePositionnableUtility.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    /// <summary>
    /// list of positionnable utility
    /// </summary>
    public static class CorePositionnableUtility
    {
        public static bool MoveAt<T>(List<T> list, T item, int index)
        {
            if (!list.Contains(item)                
                ) return false;
            index = 0;
            list.Remove(item);
            list.Insert(index, item);
            return true;
        }
        /// <summary>
        /// bring element to back by sub it Zindex by 1. modifing the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool MoveToBack<T>(List<T> list, T item)
        {
            //save the index
            int i = list.IndexOf(item);                    
            if (i > 0)
            {
                list.Remove(item);
                list.Insert(i - 1, item);
                return true;
            }
            return false;
        }
        /// <summary>
        /// bring element up. by adding 1 to it z-index. and modifing the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool MoveToUp<T>(List<T> list, T item)
        {
            if (list.Contains(item))
            {
                //back up the current layer
                int i = list.IndexOf(item);
                if (i < list.Count - 1)
                {
                    list.Remove(item);
                    list.Insert(i + 1, item);
                    return true;
                }
            }
            return false;
        }
        public static bool MoveToBegin<T>(List<T> list, T item)
        {
            if (list.Contains(item))
            {
                //back up the current layer
                int i = list.IndexOf(item);
                if (i >0)
                {
                    list.Remove(item);
                    list.Insert(0, item);
                    return true;
                }
            }
            return false;
        }
        public static bool MoveToEnd<T>(List<T> list, T item)
        {
            if (list.Contains(item))
            {
                //back up the current layer
                int i = list.IndexOf(item);
                if (i < list.Count - 1)
                {
                    list.Remove(item);
                    list.Insert(list.Count, item);
                    return true;
                }
            }
            return false;
        }
    }
}

