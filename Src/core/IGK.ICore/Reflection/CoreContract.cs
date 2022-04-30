using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace IGK.ICore.Reflection
{
    public sealed class CoreContract
    {
        private static Dictionary<Type, Type> sm_contracts;
        private static Dictionary<CoreContractObjectKey, object> sm_objcontracts;
        private static Assembly sm_dynamicDataAssembly;
        private static ModuleBuilder sm_module;
        private static AssemblyBuilder sm_asmBuilder;


        public static void FreeContract(object s)
        {
            if (s is ContractClassBase)
            {
                (s as ContractClassBase).Dispose();
            }
        }

        static CoreContract()
        {
            sm_contracts = new Dictionary<Type, Type>();
            sm_objcontracts = new Dictionary<CoreContractObjectKey, object>();
        }
        /// <summary>
        /// create a contract of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="strict">to strictly match the contract</param>
        /// <returns></returns>
        public static T CreateContract<T>(object obj, bool strict, bool throwExeption=true)
        {
            T c = CreateContract<T>(obj);
            if ((c != null) && strict)
            {
                //check for type that match contract
                if (((ContractClassBase)(object)c).CheckContract() == false)
                {
                    if (throwExeption)
                        throw new InvalidOperationException("Contract not valid. method not implement");
                    else
                    {
                        UnRegister(c as ContractClassBase);
                        return default(T);
                    }
                }
            }
            return c;

        }
        /// <summary>
        /// create a contract of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CreateContract<T>(object obj)
        {
            if (obj == null)
                return default(T);
            Type t = obj.GetType();
            Type i = typeof(T);
            //if (t.GetInterface(i.FullName) != null)
            //{
            //   // sm_contracts.Add(i, v_tt);
            //    var b = new SystemContract();
            //    b.host = obj;
            //    //register value
            //    //sm_objcontracts.Add(c, b);
            //    return (T)(object)b;
            //}
            CoreContractObjectKey c = new CoreContractObjectKey();
            c.m_target = obj;
            c.m_type = i;
            if (sm_objcontracts.ContainsKey(c))
            {
                return (T)sm_objcontracts[c];
            }
            /*
             * create and bind interface
             * */
            if (sm_contracts.ContainsKey(i))
            {
                Type v_tt = sm_contracts[i];
                var b = GetNewDef(v_tt, obj, i);  
                //register value
                sm_objcontracts.Add(c, b);
                return (T)(object)b;
            }
            else
            {
                //
                Type v_tt = BuildTypeInterface(i, obj);
                if (v_tt != null)
                {
                    sm_contracts.Add(i, v_tt);
                    var b = GetNewDef(v_tt, obj, i); 
                    //register value
                    sm_objcontracts.Add(c, b);
                    return (T)(object)b;
                }
            }
            return default(T);
        }

        private static ContractClassBase GetNewDef(Type v_tt, object obj, Type contractType)
        {
            ContractClassBase b = v_tt.Assembly.CreateInstance(v_tt.FullName) as ContractClassBase;
            b.host = obj;
            b.contractType = contractType ;
            b.init_definition();
            return b;
        }
        private static Assembly DynamicDataAssembly
        {
            get
            {
                return sm_dynamicDataAssembly;
            }
        }
        const string CONTRACTASM_NAME = "IGK.ICore.Reflexion.Contract.Assembly";
        private static Type BuildTypeInterface(Type interfaceType, object source)
        {
            if ((interfaceType == null) || (!interfaceType.IsInterface))
                return null;
            string v_tname = "IGK.ICore.Reflexion.ContractsImplement." + interfaceType.Assembly.GetName().Name + "." +
                interfaceType.FullName;
            Type v_tt = null;
            Assembly asm = DynamicDataAssembly;
            //FieldBuilder fb = null;
            if (asm != null)
            {
                v_tt = asm.GetType(v_tname);
                if (v_tt != null)
                {
                    return v_tt;// CreateDateObj(v_tt, v_name, row, context);
                }
            }

            //  GSLog.WriteLog("create new type " + v_name);
            AssemblyBuilder v_asmBuilder = null;
            if (sm_asmBuilder == null)
            {

                AssemblyName v_asmName = new AssemblyName();
                v_asmName.Name = CONTRACTASM_NAME;
                //create assembly builder
                v_asmBuilder = sm_asmBuilder ??
                    AppDomain.CurrentDomain.DefineDynamicAssembly(v_asmName, AssemblyBuilderAccess.Run);
                sm_asmBuilder = v_asmBuilder;
                sm_module = v_asmBuilder.DefineDynamicModule("ContractModule");
            }
            else
            {
                v_asmBuilder = sm_asmBuilder;
            }


            //create type
            TypeBuilder v_tbuilder = sm_module.DefineType(
                v_tname,
                TypeAttributes.Public,
                typeof(ContractClassBase));
            //add interface
            v_tbuilder.AddInterfaceImplementation(interfaceType);
            MethodInfo[] method = interfaceType.GetMethods();//method of the current interfaces            
            List<MethodInfo> tmethodInfo = new List<MethodInfo>();

            Dictionary<CoreStructContractMethodInfo, List<Type>> tmeth = new Dictionary<CoreStructContractMethodInfo, List<Type>>();
            __mergeMethod(tmeth, tmethodInfo, method);

            foreach (var item in interfaceType.GetInterfaces())
            {
                __mergeMethod(tmeth, tmethodInfo, item.GetMethods());

            }
            Dictionary<string, MethodBuilder> v_methBuildes = new Dictionary<string, MethodBuilder>();
            #region //----- build method info --------------------------------
            foreach (MethodInfo item in tmethodInfo)
            {
                if (item.Name == "Dispose")
                {
                    continue;//implemented internally
                }
                //define a method that is implementation of the interface
                var param = __GetParameters(item);
                MethodBuilder v_builder = v_tbuilder.DefineMethod(item.Name,
                     MethodAttributes.Public | MethodAttributes.Virtual,
                     item.ReturnType,
                     param//signature must match
                     );
                ILGenerator setIL = v_builder.GetILGenerator();
                LocalBuilder v_loc0 = null;
                if (item.ReturnType != typeof(void))
                    v_loc0 = setIL.DeclareLocal(item.ReturnType);



                setIL.Emit(OpCodes.Ldarg_0);
                setIL.Emit(OpCodes.Ldstr, item.Name);
                if ((param == null) || (param.Length == 0))
                    setIL.Emit(OpCodes.Ldnull);
                else
                {
                    //Create new object array and store to a local variable
                    int arraySize = param.Length;
                    LocalBuilder paramValues = setIL.DeclareLocal(typeof(object[]));
                    //paramValues.SetLocalSymInfo("p");
                    setIL.Emit(OpCodes.Ldc_I4_S, arraySize);
                    setIL.Emit(OpCodes.Newarr, typeof(object));
                    setIL.Emit(OpCodes.Stloc, paramValues);

                    for (int i = 0; i < param.Length; i++)
                    {
                        setIL.Emit(OpCodes.Ldloc, paramValues);
                        setIL.Emit(OpCodes.Ldc_I4, i);
                        setIL.Emit(OpCodes.Ldarg, i + 1);
                        setIL.Emit(OpCodes.Stelem_Ref);
                    }
                    //mis en mémoire du tableau
                    setIL.Emit(OpCodes.Ldloc, paramValues);
                }
                setIL.Emit(OpCodes.Callvirt, typeof(ContractClassBase).GetMethod("InvokeMethod",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null,
                    new Type[] { typeof(string), typeof(object[]) }, null));

                if (item.ReturnType == typeof(void))
                {
                    setIL.Emit(OpCodes.Nop);
                    setIL.Emit(OpCodes.Pop);//nettoyer la pile
                }
                else
                {
                    //casting to correct value data !!!!! important
                    setIL.Emit(OpCodes.Unbox_Any, item.ReturnType);
                    setIL.Emit(OpCodes.Stloc, v_loc0);
                    setIL.Emit(OpCodes.Ldloc, v_loc0);
                }
                setIL.Emit(OpCodes.Ret);

                if (!v_methBuildes.ContainsKey(item.Name))
                {
                    v_methBuildes.Add(item.Name, v_builder);
                }
                else { 

                }
                CoreStructContractMethodInfo key = new CoreStructContractMethodInfo();
                key.Name = item.Name;
                key.ReturnType = item.ReturnType;
                key.Arguments = __GetParameters(item);
                if (tmeth.ContainsKey(key))
                {
                    // v_tbuilder.DefineMethodOverride(v_builder, item);
                    int i = 0;
                    foreach (var sitem in tmeth[key])
                    {
                        MethodInfo meth = sitem.GetMethod(item.Name);

                        if (i > 0)
                        {
                        }
                        else
                        {
                            v_tbuilder.DefineMethodOverride(v_builder, meth);
                        }
                        i++;
                    }
                }
            }
            #endregion


            #region //----- build properties

                //1 get all properties for contract
            Dictionary<string, PropertyInfo> v_props = GetProperties(interfaceType);
            foreach (KeyValuePair<string, PropertyInfo> item in v_props)
	        {
	
             //define a property builder 
                PropertyInfo prInfo = item.Value;
                var propertyBuilder = v_tbuilder.DefineProperty(prInfo.Name, PropertyAttributes.None, prInfo.PropertyType, null);               

                //add a writeable property
                if (prInfo.CanWrite)
                {
                    //v_tbuilder.AddME
                    MethodBuilder v_builder = v_methBuildes["set_" + prInfo.Name];
                    propertyBuilder.SetSetMethod(v_builder);
                }
                //set a readable property
                if (prInfo.CanRead)
                {
                    MethodBuilder v_builder = v_methBuildes["get_" + prInfo.Name];
                    propertyBuilder.SetGetMethod(v_builder);
                }

            }
            #endregion


            try
            {
                Type v_type = v_tbuilder.CreateType();
                sm_dynamicDataAssembly = sm_dynamicDataAssembly ?? FindAssembly(AppDomain.CurrentDomain.GetAssemblies(),
                    CONTRACTASM_NAME);
                return v_type;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Impossible de créer le type : " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v_pros"></param>
        /// <param name="tab"></param>
        private static void __mergePropertiesList(Dictionary<string, PropertyInfo> v_pros, PropertyInfo[] tab)
        {
            foreach (var item in tab)
            {
                if (v_pros.ContainsKey(item.Name))
                {
                    PropertyInfo rinfo = v_pros[item.Name];
                    v_pros[item.Name] = item;
                    if (rinfo.PropertyType != item.PropertyType)
                    {//for properties that doesn't have the same return type but have the same name
                        v_pros[rinfo.DeclaringType + "::" + item.Name] = rinfo;
                    }
                }
                else
                    v_pros.Add(item.Name, item);

            }
        }
        private static Dictionary<string, PropertyInfo> GetProperties(Type interfaceType)
        {
            var v_pros = new Dictionary<string, PropertyInfo>();
            //prepend all properties from parent interface
            foreach (Type t in interfaceType.GetInterfaces())
            {
                __mergePropertiesList(v_pros, t.GetProperties());
            }

            PropertyInfo[] tab = interfaceType.GetProperties();
            __mergePropertiesList(v_pros, tab);

            return v_pros;
        }

        private static void __mergeMethod(Dictionary<CoreStructContractMethodInfo,
            List<Type>> tmeth,
            List<MethodInfo> methodInfos, MethodInfo[] method)
        {
            for (int i = 0; i < method.Length; i++)
            {
                CoreStructContractMethodInfo m = new CoreStructContractMethodInfo();
                var v_m = method[i];
                m.Name = v_m.Name;
                m.ReturnType = v_m.ReturnType;
                m.Arguments = __GetParameters(v_m);
                if (tmeth.ContainsKey(m))
                {
                    tmeth[m].Add(v_m.DeclaringType);
                }
                else
                {
                    List<Type> interfaceTypes = new List<Type>();
                    interfaceTypes.Add(v_m.DeclaringType);
                    tmeth.Add(m, interfaceTypes);
                    methodInfos.Add(v_m);
                }
            }
        }

        private static Type[] __GetParameters(MethodInfo item)
        {
            List<Type> t = new List<Type>();
            foreach (var p in item.GetParameters())
            {
                t.Add(p.ParameterType);
            }
            return t.ToArray();
        }
        private static Assembly FindAssembly(Assembly[] assembly, string p)
        {
            foreach (var item in assembly)
            {

                if (item.FullName.Split(',')[0].ToLower() == p.ToLower())
                {
                    return item;
                }

            }
            return null;
        }

        private static void __mergeMethod(Dictionary<string, MethodInfo> v_pros, MethodInfo[] tab)
        {
            foreach (var item in tab)
            {
                if (v_pros.ContainsKey(item.Name))
                {
                    MethodInfo rinfo = v_pros[item.Name];
                    v_pros[item.Name] = item;
                }
                else
                    v_pros.Add(item.Name, item);

            }
        }

        /// <summary>
        /// unregister
        /// </summary>
        /// <param name="obj"></param>
        private static void UnRegister(ContractClassBase obj)
        {
            CoreContractObjectKey c = new CoreContractObjectKey();
            c.m_type = obj.contractType;
            c.m_target = obj.host;
            sm_objcontracts.Remove(c);
        }

        /// <summary>
        /// represente la classe de base de contract entre une instance et une interface
        /// </summary>
        public abstract class ContractClassBase :
            IDisposable
        {
            internal object host;
            internal Type contractType;
            private List<string> m_meth;

            public object Host { get { return host; } }
            public Type ContractType { get { return contractType; } }

            /// <summary>
            /// dispose the contract
            /// </summary>
            public void __freeMethodProxy()
            {
                if (host != null)
                {
                    if (host is IDisposable)
                    {
                        (host as IDisposable).Dispose();
                    }
                    UnRegister(this);
                    host = null;
                }
            }
            protected object GetProperty(string name)
            {
                return host.GetType().GetProperty(name).GetValue(host);
            }
            protected void SetProperty(string name, object value)
            {
                host.GetType().GetProperty(name).SetValue(host, value);
            }

            public MethodInfo[] __findMethod(Type t, string name)
            {
                List<MethodInfo> rt = new List<MethodInfo>();
                foreach (MethodInfo s in t.GetMethods())
                {
                    if (s.Name == name)
                        rt.Add(s);
                }
                if (t.IsInterface)
                {
                    foreach (var tr in t.GetInterfaces())
                    {
                        rt.AddRange(__findMethod(tr, name));
                    }
                }
                return rt.ToArray();
            }

            /// <summary>
            /// invoke the method
            /// </summary>
            /// <param name="name"></param>
            /// <param name="param"></param>
            /// <returns></returns>
            public  object InvokeMethod(string name, params object[] param)
            {
                if (host != null)
                {
                    MethodInfo meth = null;
                    try
                    {
                        Type t = host.GetType();
                        MethodInfo[] erm = __findMethod(t, name);
                        //__findMethod(contractType, name);
                        if (erm.Length == 1)
                            meth = erm[0];
                        else
                        {

                            meth = host.GetType().GetMethod(name,
                                BindingFlags.Instance | BindingFlags.Public, null,
                                param.GetTypes(), null);
                        }
                    }
                    catch (AmbiguousMatchException)
                    {

                        //MethodInfo[] em = __findMethod(host.GetType(), name);
                        MethodInfo[] erm = __findMethod(contractType, name);
                        if (erm.Length == 1)
                            meth = erm[0];

                    }

                    if (meth != null)
                    {
                        return meth.Invoke(host, param);
                    }
                }
                return null;
            }
            /// <summary>
            /// get if method exists in contract
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            public bool IsMethodExist(string name)
            {
                if (host != null)
                {
                    return this.m_meth.Contains(name);
                }
                return false;
            }

            internal bool CheckContract()
            {
                if (contractType != null)
                {
                    foreach (var c in contractType.GetMethods())
                    {
                        if (!IsMethodExist(c.Name))
                            return false;
                    }
                    return true;
                }
                return false;
            }

            public void Dispose()
            {
                __freeMethodProxy();
            }

            internal void init_definition()
            {
                m_meth = new List<string> ();
                m_meth.AddRange(host.GetType().GetMethodLists(BindingFlags.Public | BindingFlags.Instance));                
            }
        }
        
        struct CoreStructContractMethodInfo
        {
            internal string Name;
            internal Type ReturnType;
            internal Type[] Arguments;

            public override string ToString()
            {
                return "Contract[Name : " + Name + " ] ";
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                CoreStructContractMethodInfo t = (CoreStructContractMethodInfo)obj;
                if (this.Name == t.Name)
                {
                    if (t.ReturnType == this.ReturnType)
                    {
                        if ((Arguments != null) && (t.Arguments != null))
                        {
                            if (Arguments.Length == t.Arguments.Length)
                            {
                                //Check types 
                                for (int i = 0; i < this.Arguments.Length
                                    ; i++)
                                {
                                    if (this.Arguments[i] != t.Arguments[i])
                                        return false;
                                }
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }

        struct CoreContractObjectKey
        {
            internal Type m_type;
            internal object m_target;

        }


        class SystemContract : ContractClassBase
        {
 
        }
    }
}
