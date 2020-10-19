using System;
using System.Collections.Generic;
using System.Linq;

namespace JogoDaVelha.Biblioteca.Ultilidades
{
    public static class ConversorEnum
    {
        public static List<T> ListEnum<T>()
        {
            return ((T[])Enum.GetValues(typeof(T))).ToList();
        }

        public static List<string> ListEnumItens<T>()
        {
            return (from ret in ListEnum<T>()
                    select ret.ToString()).ToList();
        }

        public static T EnumItem<T>(string value)
        {
            if (string.IsNullOrEmpty(value)) return ListEnum<T>().First();
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string EnumItemString<T>(T value)
        {
            if (value == null) return string.Empty;
            return Enum.GetName(typeof(T), value);
        }
    }
}