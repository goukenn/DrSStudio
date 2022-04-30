

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingObjectCollections.cs
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
file:CoreWorkingObjectCollections.cs
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace IGK.ICore
{
    [Serializable()]
    public sealed class CoreWorkingObjectCollections:
        CoreSystemCollections ,
        ICoreWorkingObjectCollections
    {
        public override string ToString()
        {
            return string.Format("RegisteredObjects[#{0}]", this.Count);
        }
        /// <summary>
        /// private string, iCoreWorkingobjectInfo
        /// </summary>
        private Dictionary<string, ICoreWorkingObjectInfo> m_wobjects;
        private Dictionary<Type, List<Type>> sm_designer;

        public override object InitializeLifetimeService()
        {
            return null;
        }
        internal CoreWorkingObjectCollections(CoreSystem core):base(core )
        {
            m_wobjects = new Dictionary<string, ICoreWorkingObjectInfo>();
            sm_designer = new Dictionary<Type, List<Type>>();
        }
        public string[] GetWorkingObjectList()
        {
            return (from s in m_wobjects.Keys select s).ToArray();
        }
        #region ICoreWorkingObjectCollections Members
        public Type this[string key]
        {
            get 
            {
                if (this.m_wobjects .ContainsKey (key))
                    return this.m_wobjects[key].Type ;
                return null;
            }
        }
        public int Count
        {
            get { return this.m_wobjects.Count ; }
        }
        #endregion
        #region IEnumerable Members
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_wobjects.GetEnumerator();
        }
        #endregion
       
        protected override void RegisterType(Type t)
        {
            if (!t.IsClass)
                return;     
            CoreWorkingObjectAttribute v_attr =(CoreWorkingObjectAttribute)
                Attribute.GetCustomAttribute(t, typeof(CoreWorkingObjectAttribute), false );
            if (v_attr != null)
            {
                string v_n = v_attr.FullName;
                if (!this.m_wobjects.ContainsKey(v_n))
                {
                    this.m_wobjects.Add(v_n, new CoreWorkingObjectInfo(t, v_attr));

                    if ((v_attr is ICoreWorkingDesignerAttribute f)&&(f.Edition !=null)) {
                        RegisterDesigner(f, t);
                    }
                }
                else {
                    CoreLog.WriteDebug($"Already contains object type by name \"{v_attr.Name}\"");

                }
            }
        }

        private void RegisterDesigner(ICoreWorkingDesignerAttribute f, Type src)
        {
            foreach (var item in f.Edition)
            {
                if (!sm_designer.ContainsKey(item)) {
                    sm_designer.Add(item,new List<Type>());
                }
                sm_designer[item].Add(src);
            }
            
        }

        public static ICoreWorkingObject CreateObject(Type t)
        {
            if (t == null)
                return null;
            return t.Assembly.CreateInstance(t.FullName, true,
                     System.Reflection.BindingFlags.CreateInstance |
                     System.Reflection.BindingFlags.Public |
                     System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                     null, null,
                     IGK.ICore.Threading.CoreThreadManager.CultureInfo,
                     null) as ICoreWorkingObject;
        }
        internal ICoreWorkingObject CreateObject(string p)
        {
            string v_turi = p ;
            if (!this.m_wobjects.ContainsKey(v_turi ) && !Uri.IsWellFormedUriString(p, UriKind.Absolute)) {
                v_turi = $"{CoreConstant.DRAWING2D_NAMESPACE}/{p}";
            }

            if (this.m_wobjects.ContainsKey(v_turi))
            {
                Type t = this.m_wobjects[v_turi].Type;
                if (AppDomain.CurrentDomain.IsDefaultAppDomain())
                {
                    ICoreWorkingObject obj = t.Assembly.CreateInstance(t.FullName, true,
                         System.Reflection.BindingFlags.CreateInstance |
                         System.Reflection.BindingFlags.Public |
                         System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                         null, null,
                         IGK.ICore.Threading.CoreThreadManager.CultureInfo,
                         null) as ICoreWorkingObject;
                    if (obj is ICoreWorkingInitializableObject)
                    {
                        (obj as ICoreWorkingInitializableObject).Initialize();
                    }
                    return obj;
                }
                else
                    return CreateObject(t);
            }
            else { 
                //find type 
                foreach (System.Reflection.Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var t = asm.GetType(p);
                    if ((t != null) && (!t.IsAbstract) && (!t.IsInterface ))
                    {
                        this.m_wobjects.Add(t.FullName,
                            new CoreWorkingObjectInfo(t, new CoreWorkingObjectAttribute(t.FullName)));
                        return CreateObject(t.FullName );
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// return the containt type
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Type GetWorkingType(string key)
        {
            if (!Uri.IsWellFormedUriString(key, UriKind.Absolute))
            {
                key = $"{CoreConstant.DRAWING2D_NAMESPACE}/{key}";
            }

            if (!string.IsNullOrEmpty (key) && this.m_wobjects.ContainsKey(key))
            {
                Type t = this.m_wobjects[key].Type ;
                return t;
            }            
            return null;
        }
        /// <summary>
        /// register type 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attrib"></param>
        public void RegisterType(Type type, CoreWorkingObjectAttribute attrib)
        {
            if (type == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "type");
            if (attrib == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "attrib");
            if (!this.m_wobjects.ContainsKey(attrib.Name))
            {
                this.m_wobjects.Add (attrib.Name, 
                    new CoreWorkingObjectInfo (type, attrib ));
            }
        }

        public Type[] GetEditionTools(Type type)
        {
            if (this.sm_designer.ContainsKey(type))
                return this.sm_designer[type].ToArray();
            return null;
        }

        public sealed class CoreWorkingObjectInfo : ICoreWorkingObjectInfo
        {
            private System.Type m_Type;
            private CoreWorkingObjectAttribute m_Attribute;
            public CoreWorkingObjectAttribute Attribute
            {
                get { return m_Attribute; }
            }
            public System.Type Type
            {
                get { return m_Type; }
            }
            public CoreWorkingObjectInfo(Type type, CoreWorkingObjectAttribute attrib)
            {
                if (type == null)
                    throw new CoreException (enuExceptionType .ArgumentIsNull , "type");
                if (attrib == null)
                    throw new CoreException (enuExceptionType.ArgumentIsNull , "attrib");
                this.m_Attribute = attrib;
                this.m_Type = type;
            }
        }
    }
}

