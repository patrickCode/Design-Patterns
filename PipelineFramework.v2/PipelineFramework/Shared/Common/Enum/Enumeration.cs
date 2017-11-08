using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace PipelineFramework.Common.Enum
{
    public abstract class Enumeration
    {
        private readonly int _code;
        private readonly string _name;

        public int Code { get => _code; }
        public string Name { get => _name; }

        protected Enumeration() { }

        protected Enumeration(int code, string name)
        {
            _code = code;
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }

        public static IEnumerable<T> GetAll<T>() where T: Enumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var field in fields)
            {
                var instance = new T();
                if (field.GetValue(instance) is T locatedValue)
                    yield return locatedValue;
            }
        }

        public override bool Equals(object obj)
        {
            var otherEnum = obj as Enumeration;

            if (otherEnum == null)
                return false;

            var typeMatched = GetType().Equals(otherEnum.GetType());
            var codeMatched = _code == otherEnum.Code;
            var nameMatched = _name.Equals(otherEnum.Name);

            return typeMatched && codeMatched && nameMatched;
        }

        public override int GetHashCode()
        {
            return _code.GetHashCode();
        }

        public static T Parse<T, K>(K value, Func<T, bool> predicate, bool isNullAllowed = true) where T: Enumeration, new()
        {
            var item = GetAll<T>().FirstOrDefault(predicate);
            if (item == null && !isNullAllowed)
                throw new Exception("Enum not found");
            return item;
        }

        public static T FromCode<T>(int code, bool isNullAllowed = true) where T : Enumeration, new()
        {
            return Parse<T, int>(code, enumeration => enumeration.Code == code, isNullAllowed);
        }

        public static T FromName<T>(string name, bool isNullAllowed = true) where T : Enumeration, new()
        {
            return Parse<T, string>(name, enumeration => enumeration.Name == name, isNullAllowed);
        }

    }
}