using System;
using System.Collections.Generic;
using System.Text;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.Core
{
    public class TypeMetaModel
    {

        private readonly Type _type;
        private bool _isPrimitive;
        private readonly TypeNote _note = new TypeNote();

        public TypeMetaModel(Type type)
        {
            _type = type;

            if (IsDefaultPrimitive())
            {
                this.IsPrimitive = true;
            }
            else
            {
                this.IsComplexType = IsDefactoComplexType(_type);
                this.IsPrimitive = !this.IsComplexType;
            }

            this.Name = GetFriendlyDataType(type);
            this.HideAsBaseClass = _type == typeof (object);
        }

        private bool IsDefaultPrimitive()
        {
            return !_type.IsEnumerable() && _type.IsMsCoreLibType() && IsDefactoComplexType(_type);
        }

        public bool IsPrimitive
        {
            get { return _isPrimitive; }
            internal set
            {
                _isPrimitive = value;

                this.IsComplexType = !this.IsPrimitive;
            }
        }

        public bool IsComplexType { get; internal set; }

        public string Name { get; internal set; }

        public TypeNote Note { get { return _note; } }

        public bool Hidden { get; internal set; }
        
        public bool HideAsBaseClass { get; internal set; }

        public bool TreatAllMembersAsPrimitives { get; internal set; }

        public static bool IsDefactoComplexType(Type type)
        {
            if (type.IsString())
                return false;

            if (type.IsEnumerable())
                return true;

            return (type.IsClass || type.IsInterface || type.IsEnum);
        }

        private static string GetFriendlyDataType(Type type)
        {
            if (type.IsGenericType)
            {
                var def = type.GetGenericTypeDefinition();
                var genericArguments = type.GetGenericArguments();


                string outerName;
                string innerName;

                if (typeof(Nullable<>) == def)
                {
                    outerName = "Nullable";
                    var nullableType = genericArguments[0];
                    innerName = nullableType.Name;
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

                    innerName = builder.ToString();
                }

                return "{0}<{1}>".FormatWith(outerName, innerName);
            }

            return type.Name;
        }
    }

    public class TypeNote
    {
        private NoteDirection _direction;
        private readonly List<string> _lines = new List<string>();

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

        public override string ToString()
        {
            if (_lines.Count > 0)
            {
                return "note {0}: {1}".FormatWith(_direction, string.Join("\\n", _lines));
            }

            return string.Empty;
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