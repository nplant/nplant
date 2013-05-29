using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.Core
{
    public class TypeMetaModel
    {
        private static readonly Assembly mscorelib = typeof(string).Assembly;

        private readonly Type _type;

        public TypeMetaModel(Type type)
        {
            _type = type;

            this.IsComplexType = IsDefactoComplexType(_type);
            this.IsPrimitive = !this.IsComplexType && _type.Assembly != mscorelib;

            this.Name = GetFriendlyDataType(type);
            this.Note = TypeNote.Null;
        }

        public bool IsPrimitive { get; internal set; }

        public bool IsComplexType { get; internal set; }

        public string Name { get; internal set; }

        public TypeNote Note { get; internal set; }

        public bool Hidden { get; internal set; }

        public static bool IsDefactoComplexType(Type type)
        {
            return (type.IsClass || type.IsInterface) && !type.IsString();
        }

        private static string GetFriendlyDataType(Type type)
        {
            if (type.IsGenericType)
            {
                var def = type.GetGenericTypeDefinition();
                var genericArguments = type.GetGenericArguments();


                string outerName;
                string innterName;

                if (typeof(Nullable<>) == def)
                {
                    outerName = "Nullable";
                    var nullableType = genericArguments[0];
                    innterName = nullableType.Name;
                }
                else
                {
                    outerName = def.Name.Substring(0, def.Name.IndexOf("`", StringComparison.Ordinal));

                    var builder = new StringBuilder();

                    for (var x = 0; x < genericArguments.Length; x++)
                    {
                        if (x > 0)
                            builder.Append(", ");

                        var genericArgument = genericArguments[x];

                        builder.Append(GetFriendlyDataType(genericArgument));
                    }

                    innterName = builder.ToString();
                }

                return "{0}<{1}>".FormatWith(outerName, innterName);
            }

            return type.Name;
        }
    }

    public class TypeNote
    {
        private NoteDirection _direction;
        private readonly List<string> _lines = new List<string>();

        public static TypeNote Null = new TypeNote();

        public TypeNote AddLine(string line)
        {
            _lines.Add(line);

            return this;
        }

        public TypeNote DisplayLeft()
        {
            _direction = NoteDirection.left;
            return this;
        }

        public TypeNote DisplayRight()
        {
            _direction = NoteDirection.right;
            return this;
        }

        public TypeNote DisplayTop()
        {
            _direction = NoteDirection.top;
            return this;
        }

        public TypeNote DisplayBottom()
        {
            _direction = NoteDirection.bottom;
            return this;
        }

        public void Write(IGenerationContext context)
        {
            if (_lines.Count > 0)
            {
                context.WriteLine("note {0}: {1}".FormatWith(_direction, string.Join("\\n", _lines)));
            }
        }
    }

    public enum NoteDirection
    {
// ReSharper disable InconsistentNaming
        left,
        right,
        top,
        bottom
// ReSharper restore InconsistentNaming
    }
}