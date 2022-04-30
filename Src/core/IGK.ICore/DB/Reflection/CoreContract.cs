using IGK.ICore.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB.Reflection
{
    public sealed class CoreContract
    {

        const string CONTRACTASM_NAME = "IGK.ICore.DB.Reflection.Contract.Assembly";

        private static Dictionary<Type, Type> sm_contracts;
        private static Dictionary<object, object> sm_objcontracts;
        private static Assembly sm_dynamicDataAssembly;
        private static ModuleBuilder sm_module;
        private static AssemblyBuilder sm_asmBuilder;

        static CoreContract()
        {
            sm_contracts = new Dictionary<Type, Type>();
            sm_objcontracts = new Dictionary<object, object>();
        }
        /// <summary>
        /// create a contract of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="strict">to strictly match the contract</param>
        /// <returns></returns>
        public static T CreateContract<T>(ICoreDataRow obj, bool strict)
        {
            T c = CreateContract<T>(obj);
            if ((c != null) && strict)
            {
                //check for type that match contract
                if (((GSContractClassBase)(object)c).CheckContract() == false)
                {
                    throw new InvalidOperationException("Contract not valid. method not implement");
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
        public static T CreateContract<T>(ICoreDataRow obj)
        {
            if (obj == null)
                return default(T);
            Type t = obj.GetType();
            Type i = typeof(T);
            if (t.GetInterface(i.FullName) != null)
                return (T)obj;

            if (sm_objcontracts.ContainsKey(obj))
            {
                return (T)sm_objcontracts[obj];
            }
            /*
             * create and bind interface
             * */
            if (sm_contracts.ContainsKey(i))
            {
                Type v_tt = sm_contracts[i];
                GSContractClassBase b = v_tt.Assembly.CreateInstance(v_tt.FullName) as GSContractClassBase;
                b.host = obj;
                b.contractType = i;
                //register value
                sm_objcontracts.Add(obj, b);
                return (T)(object)b;
            }
            else
            {
                //
                Type v_tt = BuildTypeInterface(i, obj);
                if (v_tt != null)
                {
                    sm_contracts.Add(i, v_tt);
                    GSContractClassBase b = v_tt.Assembly.CreateInstance(v_tt.FullName) as GSContractClassBase;
                    b.host = obj;
                    b.contractType = i;
                    //register value
                    sm_objcontracts.Add(obj, b);
                    return (T)(object)b;
                }
            }
            return default(T);
        }
        private static Assembly DynamicDataAssembly
        {
            get
            {
                return sm_dynamicDataAssembly;
            }
        }
        private static Type BuildTypeInterface(Type interfaceType, object source)
        {
            if ((interfaceType == null) || (!interfaceType.IsInterface))
                return null;
            string v_tname = "IGK.ICore.Reflexion.ContractsImplement." + interfaceType.Assembly.GetName().Name + "_" +
                interfaceType.Name;
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
                typeof(GSContractClassBase));
            //add interface
            v_tbuilder.AddInterfaceImplementation(interfaceType);

           

            MethodInfo[] method = interfaceType.GetMethods();//method of the current interfaces            
            List<MethodInfo> tmethodInfo = new List<MethodInfo>();

            Dictionary<GSCoreStructContractMethodInfo, List<Type>> tmeth = new Dictionary<GSCoreStructContractMethodInfo, List<Type>>();
            __mergeMethod(tmeth, tmethodInfo, method);
            foreach (var item in interfaceType.GetInterfaces())
            {
                __mergeMethod(tmeth, tmethodInfo, item.GetMethods());

            }
            // MethodInfo v_src = null;
            // Type v_srcType = source.GetType();
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

                //declare local variable
                LocalBuilder v_loc0 = null;
                
                if (item.ReturnType != typeof (void))
                {
                    v_loc0 = setIL.DeclareLocal(item.ReturnType);
                }


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

                    //setIL.Emit(OpCodes.Ldarga);

                    for (int i = 0; i < param.Length; i++)
                    {
                        setIL.Emit(OpCodes.Ldloc, 0);
                        setIL.Emit(OpCodes.Ldc_I4, i);
                        setIL.Emit(OpCodes.Ldarg, i + 1);
                        setIL.Emit(OpCodes.Stelem_Ref);
                    }
                    //mis en mémoire du tableau
                    setIL.Emit(OpCodes.Ldloc, 0);
                }
                setIL.Emit(OpCodes.Callvirt, typeof(GSContractClassBase).GetMethod("InvokeMethod",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null,
                    new Type[] { typeof(string), typeof(object[]) }, null));

                if (item.ReturnType == typeof(void))
                {
                    setIL.Emit(OpCodes.Nop);
                    setIL.Emit(OpCodes.Pop);//nettoyer la pile
                }
                else { 
                    //unbox any to return the correct data
                    setIL.Emit(OpCodes.Unbox_Any, item.ReturnType);
                    setIL.Emit(OpCodes.Stloc_0 );
                    setIL.Emit(OpCodes.Ldloc_0);
                }
               
                setIL.Emit(OpCodes.Ret);
                GSCoreStructContractMethodInfo key = new GSCoreStructContractMethodInfo();
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

        private static void __mergeMethod(Dictionary<GSCoreStructContractMethodInfo,
            List<Type>> tmeth,
            List<MethodInfo> methodInfos, 
            MethodInfo[] method)
        {
            for (int i = 0; i < method.Length; i++)
            {
                GSCoreStructContractMethodInfo m = new GSCoreStructContractMethodInfo();
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


        /// <summary>
        /// unregister
        /// </summary>
        /// <param name="obj"></param>
        private static void UnRegister(GSContractClassBase obj)
        {
            sm_objcontracts.Remove(obj.Host);
        }

        /// <summary>
        /// represent struct to identifier
        /// </summary>
        struct GSCoreStructContractMethodInfo
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
                GSCoreStructContractMethodInfo t = (GSCoreStructContractMethodInfo)obj;
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

        /// <summary>
        /// represente la classe de base de contract entre une instance et une interface
        /// </summary>
        public abstract class GSContractClassBase :
            IDisposable
        {
            internal ICoreDataRow host;
            internal Type contractType;

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
            public static object GetDefault(Type type)
            {
                // If no Type was supplied, if the Type was a reference type, or if the Type was a System.Void, return null
                if (type == null || !type.IsValueType || type == typeof(void))
                    return null;

                // If the supplied Type has generic parameters, its default value cannot be determined
                if (type.ContainsGenericParameters)
                    throw new ArgumentException(
                        "{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" + type +
                        "> contains generic parameters, so the default value cannot be retrieved");

                // If the Type is a primitive type, or if it is another publicly-visible value type (i.e. struct/enum), return a 
                //  default instance of the value type
                if (type.IsPrimitive || !type.IsNotPublic)
                {
                    try
                    {
                        return Activator.CreateInstance(type);
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException(
                            "{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe Activator.CreateInstance method could not " +
                            "create a default instance of the supplied value type <" + type +
                            "> (Inner Exception message: \"" + e.Message + "\")", e);
                    }
                }

                // Fail with exception
                throw new ArgumentException("{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" + type +
                    "> is not a publicly-visible type, so the default value cannot be retrieved");
            }

            protected internal object InvokeMethod(string name, params object[] param)
            {
                if (host != null)
                {
                    if (name.StartsWith("get_"))
                    {
                        Type t = GetType().GetMethod(name).ReturnType;
                        name = name.Substring(4);
                        var s = CoreTypeDescriptor.GetConverter(t);
                        if (host.Contains(name))
                        {

                            var k = host[name];
                            var r = s.ConvertFrom(k);
                            return r;
                        }
                        return GetDefault(t);// null;
                    }
                    else if (name .StartsWith ("set_"))
                    {
                        name = name.Substring(4);
                        string k = null;
                        if ((param != null) && (param.Length == 1) && (param[0] != null))
                        {
                            var s =CoreTypeDescriptor.GetConverter(param[0]);
                            k = s.ConvertToString(param[0]);
                        }

                        host[name] = k;
                       // return null;
                    }

                    //MethodInfo meth = null;
                    //try
                    //{

                    //    meth = host.GetType().GetMethod(name,
                    //        BindingFlags.Instance | BindingFlags.Public);
                    //}
                    //catch (AmbiguousMatchException)
                    //{

                    //    //MethodInfo[] em = __findMethod(host.GetType(), name);
                    //    MethodInfo[] erm = __findMethod(contractType, name);
                    //    if (erm.Length == 1)
                    //        meth = erm[0];

                    //}

                    //if (meth != null)
                    //{
                    //    return meth.Invoke(host, param);
                    //}
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
                    return (host.GetType().GetMethod(name) != null);
                return false;
            }

            internal bool CheckContract()
            {
                if (contractType != null)
                {
                    foreach (var c in contractType.GetMethods())
                    {
                        if (!IsMethodExist(c.Name))
                        {

                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }

            public void Dispose()
            {
                __freeMethodProxy();
            }
        }

        public static void FreeContract(object  s)
        {
            if (s is GSContractClassBase)
            {
                (s as GSContractClassBase).Dispose();
            }
        }
    }

}