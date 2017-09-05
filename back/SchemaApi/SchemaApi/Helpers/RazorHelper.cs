using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections;

namespace Molecular.Helpers
{

    public class RazorHelperTypescript
    {

        private int indented = 0;
        private readonly RazorPage page;


        public string WriteType(Type type)
        {

            StringBuilder sb = new StringBuilder();

            if (type.IsConstructedGenericType)
            {

                var _type = type.GetTypeInfo().GetGenericTypeDefinition();

                if (_type.IsArray || typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(_type))
                {
                    sb.Append("Array");
                }
                else
                    sb.Append(WriteType(_type));

                sb.Append("<");

                string comma = string.Empty;
                foreach (Type item in type.GenericTypeArguments)
                {
                    sb.Append(comma);
                    sb.Append(WriteType(item));
                    comma = ", ";
                }

                sb.Append(">");

            }
            else if (type.GetTypeInfo().IsArray)
            {
                //sb.Append("<");

                //sb.Append(">");
            }
            else
            {

                if (type == typeof(object))
                    sb.Append("any");

                else if (type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
                    sb.Append("number");

                else if (type == typeof(Int16?) || type == typeof(Int32?) || type == typeof(Int64?))
                    sb.Append("number?");

                else if (type == typeof(DateTime))
                    sb.Append("Date");

                else if (type == typeof(DateTime?))
                    sb.Append("Date?");

                else if (type == typeof(bool))
                    sb.Append("boolean");

                else if (type == typeof(bool?))
                    sb.Append("boolean?");

                else if (type == typeof(void))
                    sb.Append("{}");

                else
                    sb.Append(type.Name);

            }

            return sb.ToString();

        }

        public IDisposable AddModel(string modelName)
        {
            AddIndentation();
            page.WriteLiteral($"export class {modelName} {{");
            return new disposable(this.page, this);
        }

        public void AddInterfaceMethod(string methodName, string typeResult, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            AddIndentation();

            string p = string.Join(", ", parameters.Select(c => $"{c.Key} : {c.Value}"));

            if (!string.IsNullOrEmpty(typeResult))
                typeResult = $" : {typeResult}";

            AddLine($"{methodName}({p}){typeResult};");
        }

        public RazorHelperTypescript(RazorPage page)
        {
            this.page = page;
        }

        public void AddStartComment()
        {
            AddIndentation();
            AddLine("/**");
        }

        public void AddEndComment()
        {
            AddIndentation();
            AddLine("*/");
        }

        public void AddCommentLine(string txt)
        {
            AddIndentation();
            AddLine("* " + txt);
        }

        public void Add(string txt)
        {
            AddIndentation();
            page.WriteLiteral(txt);
        }

        public void AddLine(string txt)
        {
            AddIndentation();
            page.WriteLiteral(txt);
            page.WriteLiteral("\r\n");
        }

        public void AddProperty(string name, string type, ExpositionEnum isPublic = ExpositionEnum.None , string initValue = null)
        {
            AddIndentation();
            string p = string.Empty;
            switch (isPublic)
            {
                case ExpositionEnum.None:
                    break;
                case ExpositionEnum.Public:
                    p = "public ";
                    break;
                case ExpositionEnum.Protected:
                    p = "protected ";
                    break;
                case ExpositionEnum.Private:
                    p = "private ";
                    break;
                default:
                    break;
            }
            if (string.IsNullOrEmpty(initValue))
                initValue = string.Empty;

            else
                initValue = $" = {initValue}";

            page.WriteLiteral($"{p}{name}: {type}{initValue};\r\n");
        }

        public void AddEnumValue(string name, object value)
        {
            AddIndentation();
            page.WriteLiteral($"{name} = {value},\r\n");
        }

        public IDisposable AddExportedInterface(string serviceName)
        {
            AddIndentation();
            page.WriteLiteral($"export interface {serviceName} {{");
            return new disposable(this.page, this);
        }

        public IDisposable AddBlock(string startCode)
        {
            AddIndentation();
            page.WriteLiteral($"{startCode} {{");
            return new disposable(this.page, this);
        }

        public IDisposable AddMethod(string method, string typeResult, IEnumerable<KeyValuePair<string, string>> parameters, ExpositionEnum exposition = ExpositionEnum.None)
        {
            AddIndentation();
            string p = string.Join(", ", parameters.Select(c => $"{c.Key} : {c.Value}"));
            if (!string.IsNullOrEmpty(typeResult))
                typeResult = $": {typeResult} ";

            string exp = string.Empty;
            switch (exposition)
            {
                case ExpositionEnum.None:
                    break;
                case ExpositionEnum.Public:
                    exp = "public ";
                    break;
                case ExpositionEnum.Protected:
                    exp = "protected ";
                    break;
                case ExpositionEnum.Private:
                    exp = "private ";
                    break;
                default:
                    break;
            }

            page.WriteLiteral($"{exp}{method}({p}) {typeResult}{{");
            return new disposable(this.page, this);
        }

        public IDisposable AddIf(string condition)
        {
            AddIndentation();
            page.WriteLiteral($"if ({condition}) {{");
            return new disposable(this.page, this);
        }

        public IDisposable AddElse()
        {
            AddIndentation();
            page.WriteLiteral($"else {{");
            return new disposable(this.page, this);
        }

        public IDisposable AddElseIf(string condition)
        {
            AddIndentation();
            page.WriteLiteral($"else if ({condition}) {{");
            return new disposable(this.page, this);
        }

        public IDisposable AddExportedClass(string serviceName, string implements = null)
        {
            AddIndentation();
            string impl = string.Empty;
            if (string.IsNullOrEmpty(implements))
            {
                impl = $"implements {implements}";
            }
            page.WriteLiteral($"export class {serviceName} {impl}{{");
            return new disposable(this.page, this);
        }

        public IDisposable AddExportedEnum(string serviceName)
        {
            AddIndentation();
            page.WriteLiteral($"export enum {serviceName} {{");
            return new disposable(this.page, this);
        }

        public void AddEmpty()
        {
            page.WriteLiteral("\r\n");
        }

        public void CutFile(string filename)
        {
            AddIndentation();
            page.WriteLiteral($"!!cutefile - - - - - - {filename}\r\n");
        }

        public void AddImport(string importName, string @from)
        {
            AddIndentation();
            page.WriteLiteral("import { " + importName + " } from '" + @from + "';\r\n");
        }

        private class disposable : IDisposable
        {

            private RazorPage page;
            private readonly RazorHelperTypescript parent;

            public disposable(RazorPage page, RazorHelperTypescript parent)
            {
                this.page = page;
                this.parent = parent;
                this.parent.AddIndent();
                page.WriteLiteral("\r\n");
            }

            public void Dispose()
            {
                this.parent.DelIndent();
                this.parent.AddIndentation();
                page.WriteLiteral("}\r\n");
            }
        }

        private void DelIndent()
        {
            this.indented -= 3;
        }

        private void AddIndent()
        {
            this.indented += 3;
        }

        public void AddIndentation()
        {
            if (this.indented > 0)
                page.WriteLiteral("".PadLeft(this.indented, ' '));
        }

        public enum ExpositionEnum
        {
            None,
            Public,
            Protected,
            Private
        }

    }

}
