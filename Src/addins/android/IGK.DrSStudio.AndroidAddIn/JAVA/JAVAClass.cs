

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: JAVAClass.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.JAVA
{
    public class JAVAClass : JAVACodeItem 
    {
        private static Dictionary<string, JAVAClass> sm_javas;


        private JAVAClass m_Parent;
        private JAVAChildCollections m_Childs;
        /// <summary>
        /// get the childs
        /// </summary>
        public JAVAChildCollections Childs
        {
            get { return m_Childs; }
        }

        public static readonly JAVAClass Object;

        public sealed class JAVAChildCollections : IEnumerable 
        {
            List<JAVAClass> m_childs;
            private JAVAClass m_clOwner;
            public override string ToString()
            {
                return string.Format("JavaChilds[Count:{0}]", this.Count);
            }
            public JAVAChildCollections(JAVAClass clOwner)
            {
                this.m_clOwner = clOwner;
                this.m_childs = new List<JAVAClass>();
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_childs.GetEnumerator();
            }
            public int Count {
                get {
                    return this.m_childs.Count;
                }
            }
            public void Add(JAVAClass cl)
            { 
                if ((cl == this.m_clOwner ) || (cl == null) || this.m_childs.Contains (cl))
                return ;
                this.m_childs.Add (cl);
                if (!sm_javas.ContainsKey(cl.Name))
                    sm_javas.Add(cl.Name, cl);
            }
            public void Remove(JAVAClass cl) {
                this.m_childs.Remove(cl);
            }
        }
        static JAVAClass() {

            sm_javas = new Dictionary<string, JAVAClass>();
            Object = new JAVAClass("java.lang.Object");            
            sm_javas.Add(Object.Name, Object);
        }
        
        internal JAVAClass(string name):base()
        {
            this.Name = name;
            this.m_Childs = new JAVAChildCollections(this);
            if (Object != null)
            {
                this.m_Parent = Object;
                Object.Childs.Add(this);
            }
        }
       
      /// <summary>
      /// get the parent
      /// </summary>
        public JAVAClass Parent {
            get {
                return this.m_Parent;
            }
            internal set {
                if (this.m_Parent != value)
                {
                    if (this.m_Parent != null)
                        this.m_Parent.Childs.Remove(this);
                    this.m_Parent = value;
                    if (this.m_Parent != null)
                    {
                        this.m_Parent.Childs.Add(this);
                    }
                }
            }
        }
        public override string ToString()
        {
            return "javaclass:" + this.Name;
        }


        internal static JAVAClass GetClass(string p)
        {
            if (sm_javas.ContainsKey(p))
            {
                return sm_javas[p];
            }
            JAVAClass c = new JAVAClass(p);
            if (!sm_javas.ContainsKey(p))
                sm_javas.Add(p, c);
            return c;
        }
    }
}
