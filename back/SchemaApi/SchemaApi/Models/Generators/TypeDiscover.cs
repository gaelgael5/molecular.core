//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;

//namespace SchemaApi.Models.Generators
//{
//    public class TypeDiscover
//    {

//        public TypeDiscover(Type type)
//        {
//            this.type = type;
//            this.reader = new TypeReader(type);
//        }

//        public string Name
//        {
//            get
//            {
//                return this.type.FullName;
//            }
//        }

//        internal IEnumerable<object> Properties()
//        {
            
//        }

//        public TypeDiscover GetBase()
//        {
//            var type = this.reader.BaseType();
//            if (type != typeof(object))
//                return new TypeDiscover(type);
//            return null;
//        }

//        private readonly Type type;
//        private readonly TypeReader reader;

//        private class TypeReader
//        {

//            public TypeReader(Type type)
//            {

//                this.type = type;
//                this._properties = new Dictionary<string, PropertyDescriptor>();

//                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(type.GetType()))
//                    this._properties.Add(descriptor.Name, descriptor);

//            }

//            public Type BaseType()
//            {
//                return this._properties[_BaseType].GetValue(this.type) as Type;
//            }


//            public IEnumerable<MethodInfo> GetProperties()
//            {
//                return System.Reflection.TypeExtensions.GetMethods(this.type);
//            }

//            public IEnumerable<MethodInfo> GetMethods()
//            {

//                foreach (PropertyDescriptor descriptor in TypeDescriptor.Get(this.type))
//                    yield return descriptor;

//            }

//            private Dictionary<string, PropertyDescriptor> _properties;
//            private readonly Type type;


//            private static readonly string _BaseType = "BaseType";

//            private static readonly string _Module = "Module";
//            private static readonly string _Assembly = "Assembly";
//            private static readonly string _TypeHandle = "TypeHandle";
//            private static readonly string _DeclaringMethod = "DeclaringMethod";
//            private static readonly string _UnderlyingSystemType = "UnderlyingSystemType";
//            private static readonly string _FullName = "FullName";
//            private static readonly string _AssemblyQualifiedName = "AssemblyQualifiedName";
//            private static readonly string _Namespace = "Namespace";
//            private static readonly string _GUID = "GUID";
//            private static readonly string _GenericParameterAttributes = "GenericParameterAttributes";
//            private static readonly string _IsSecurityCritical = "IsSecurityCritical";
//            private static readonly string _IsSecuritySafeCritical = "IsSecuritySafeCritical";
//            private static readonly string _IsSecurityTransparent = "IsSecurityTransparent";
//            private static readonly string _IsGenericTypeDefinition = "IsGenericTypeDefinition";
//            private static readonly string _IsGenericParameter = "IsGenericParameter";
//            private static readonly string _GenericParameterPosition = "GenericParameterPosition";
//            private static readonly string _IsGenericType = "IsGenericType";
//            private static readonly string _IsConstructedGenericType = "IsConstructedGenericType";
//            private static readonly string _ContainsGenericParameters = "ContainsGenericParameters";
//            private static readonly string _StructLayoutAttribute = "StructLayoutAttribute";
//            private static readonly string _Name = "Name";
//            private static readonly string _MemberType = "MemberType";
//            private static readonly string _DeclaringType = "DeclaringType";
//            private static readonly string _ReflectedType = "ReflectedType";
//            private static readonly string _MetadataToken = "MetadataToken";
//            private static readonly string _GenericTypeParameters = "GenericTypeParameters";
//            private static readonly string _DeclaredConstructors = "DeclaredConstructors";
//            private static readonly string _DeclaredEvents = "DeclaredEvents";
//            private static readonly string _DeclaredFields = "DeclaredFields";
//            private static readonly string _DeclaredMembers = "DeclaredMembers";
//            private static readonly string _DeclaredMethods = "DeclaredMethods";
//            private static readonly string _DeclaredNestedTypes = "DeclaredNestedTypes";
//            private static readonly string _DeclaredProperties = "DeclaredProperties";
//            private static readonly string _ImplementedInterfaces = "ImplementedInterfaces";
//            private static readonly string _TypeInitializer = "TypeInitializer";
//            private static readonly string _IsNested = "IsNested";
//            private static readonly string _Attributes = "Attributes";
//            private static readonly string _IsVisible = "IsVisible";
//            private static readonly string _IsNotPublic = "IsNotPublic";
//            private static readonly string _IsPublic = "IsPublic";
//            private static readonly string _IsNestedPublic = "IsNestedPublic";
//            private static readonly string _IsNestedPrivate = "IsNestedPrivate";
//            private static readonly string _IsNestedFamily = "IsNestedFamily";
//            private static readonly string _IsNestedAssembly = "IsNestedAssembly";
//            private static readonly string _IsNestedFamANDAssem = "IsNestedFamANDAssem";
//            private static readonly string _IsNestedFamORAssem = "IsNestedFamORAssem";
//            private static readonly string _IsAutoLayout = "IsAutoLayout";
//            private static readonly string _IsLayoutSequential = "IsLayoutSequential";
//            private static readonly string _IsExplicitLayout = "IsExplicitLayout";
//            private static readonly string _IsClass = "IsClass";
//            private static readonly string _IsInterface = "IsInterface";
//            private static readonly string _IsValueType = "IsValueType";
//            private static readonly string _IsAbstract = "IsAbstract";
//            private static readonly string _IsSealed = "IsSealed";
//            private static readonly string _IsEnum = "IsEnum";
//            private static readonly string _IsSpecialName = "IsSpecialName";
//            private static readonly string _IsImport = "IsImport";
//            private static readonly string _IsSerializable = "IsSerializable";
//            private static readonly string _IsAnsiClass = "IsAnsiClass";
//            private static readonly string _IsUnicodeClass = "IsUnicodeClass";
//            private static readonly string _IsAutoClass = "IsAutoClass";
//            private static readonly string _IsArray = "IsArray";
//            private static readonly string _IsByRef = "IsByRef";
//            private static readonly string _IsPointer = "IsPointer";
//            private static readonly string _IsPrimitive = "IsPrimitive";
//            private static readonly string _IsCOMObject = "IsCOMObject";
//            private static readonly string _HasElementType = "HasElementType";
//            private static readonly string _IsMarshalByRef = "IsMarshalByRef";
//            private static readonly string _GenericTypeArguments = "GenericTypeArguments";
//            private static readonly string _CustomAttributes = "CustomAttributes";


//        }

//    }

//}
