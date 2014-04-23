using System;
using System.Text;

namespace ChatterBox.Core.Extensions
{
    public static class TypeExtensions
    {
        public static string ToTypeNameString(this Type type)
        {
            var nullableType = Nullable.GetUnderlyingType(type);
            if (nullableType != null)
                return nullableType.Name + "?";

            if (!type.IsGenericType)
                switch (type.Name)
                {
                    case "String":
                        return "string";
                    case "Int32":
                        return "int";
                    case "Decimal":
                        return "decimal";
                    case "Object":
                        return "object";
                    case "Void":
                        return "void";
                    default:
                        {
                            return String.IsNullOrWhiteSpace(type.FullName) ? type.Name : type.FullName;
                        }
                }

            var sb = new StringBuilder(type.Name.Substring(0,
                type.Name.IndexOf('`'))
                );
            sb.Append('<');
            var first = true;
            foreach (var t in type.GetGenericArguments())
            {
                if (!first)
                    sb.Append(',');
                sb.Append(t.ToTypeNameString());
                first = false;
            }
            sb.Append('>');
            return sb.ToString();
        }
    }
}