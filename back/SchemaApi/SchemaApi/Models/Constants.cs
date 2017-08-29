using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaApi.Models
{
    public static class Constants
    {

        static Constants()
        {
            Constants.Roots = new List<string>();
        }

        public static List<string> Roots { get; private set; }

        public static class Roles
        {

            public static readonly string Administrator = "Administrator";


        }

        public static class Types
        {
            public static readonly string String = "String";
            public static readonly string DateTime = "DateTime";
            public static readonly string Integer = "Integer";
            public static readonly string Decimal = "Decimal";
            public static readonly string Guid = "Guid";
        }

    }




    public enum TypeKind
    {
        Value,
        Structure,
    }
}
