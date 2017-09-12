using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SchemaApi.Models.Generators
{

    public class TypeDiscoverRepository
    {


        public TypeDiscoverRepository(IApiDescriptionGroupCollectionProvider apiDescription)
        {

            this.apiDescription = apiDescription;

            if (this.apiDescription != null)
            {

                foreach (ApiDescriptionGroup group in apiDescription.ApiDescriptionGroups.Items)
                    foreach (ApiDescription api in group.Items)
                    {

                        foreach (var item in api.SupportedResponseTypes)
                            if ((item.Type != null))
                                Add(item.Type);

                        foreach (ApiParameterDescription parameter in api.ParameterDescriptions)
                            if (parameter.Type != null)
                                Add(parameter.Type);

                    }
            }

        }


        public void Add(Type type)
        {

            if (Accept(type) && this._unique.Add(type))
            {

                _types.Add(type);

                var infos = type.GetTypeInfo();

                if (infos != null)
                {
                    foreach (PropertyInfo item in infos.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                        Add(item.PropertyType);
                }
                else
                {

                }

                var _base = type.GetTypeInfo().BaseType;
                if (_base != null)
                    Add(_base);

            }

            if (type.IsConstructedGenericType)
            {

                Add(type.GetTypeInfo().GetGenericTypeDefinition());

                foreach (Type arg in type.GetGenericArguments())
                    Add(arg);
            }

        }

        public IEnumerable<Type> Items { get { return this._types; } }

        private bool Accept(Type type)
        {

            bool a = !ExcludedTypes.Contains(type);

            if (typeof(System.Collections.IEnumerable).IsAssignableFrom(type))
                a = false;

            //if (type.IsConstructedGenericType)
            //    return false;

            var __t = type.GetTypeInfo();

            if (__t.IsGenericParameter)
                a = false;

            return a;
        }

        public static readonly HashSet<Type> ExcludedTypes = new HashSet<Type>()
        {
            typeof(void), typeof(Enum), typeof(object), typeof(ValueType),
            typeof(string), typeof(char), typeof(char?),
            typeof(DateTime), typeof(bool),
            typeof(DateTime?), typeof(bool?),
            typeof(Int16),typeof(Int32),typeof(Int64),typeof(UInt16),typeof(UInt32),typeof(UInt64),
            typeof(Int16?),typeof(Int32?),typeof(Int64?),typeof(UInt16?),typeof(UInt32?),typeof(UInt64?),
            // typeof(System.Collections.Generic.List<>), typeof(System.Collections.Generic.Queue<>),typeof(System.Collections.Generic.Stack<>),typeof(System.Collections.Generic.SortedSet<>),typeof(System.Collections.Generic.LinkedListNode<>), typeof(System.Collections.Generic.LinkedList<>), typeof(System.Collections.ArrayList), typeof(System.Collections.Concurrent)
        };

        public void FindTypeToImport(Type type, HashSet<Type> _types)
        {

            if (type.IsConstructedGenericType)
            {
                var t = type.GetTypeInfo().GetGenericTypeDefinition();
                if (Accept(t))
                    _types.Add(t);

                var t2 = type.GetTypeInfo().GetGenericArguments();

                foreach (var item in t2)
                    if (Accept(item))
                        _types.Add(item);
            }
            else if (Accept(type))
                _types.Add(type);

        }

        public void FindTypeToImportInProperties(Type type, HashSet<Type> _types)
        {
            foreach (var property in type.GetTypeInfo().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                Type _type = property.PropertyType;
                FindTypeToImport(_type, _types);
            }
        }

        private List<Type> _types = new List<Type>();
        private HashSet<Type> _unique = new HashSet<Type>();
        private readonly IApiDescriptionGroupCollectionProvider apiDescription;

    }

}
