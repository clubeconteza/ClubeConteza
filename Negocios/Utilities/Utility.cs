using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Negocios.Utilities
{
    public class Utility
    {
        public static List<T> EnumToList<T>()
        {
            if (!typeof(T).IsEnum)
            {
                return default(List<T>);
            }

            var lista = new List<T>();
            var tipo = typeof(T);
            if (tipo != null)
            {
                var enumValores = Enum.GetValues(tipo);
                foreach (T valor in enumValores)
                {
                    lista.Add(valor);
                }
            }

            return lista;
        }

        public static string GetDescription(Enum item)
        {
            var tipo = item.GetType();
            var campo = tipo.GetField(item.ToString());
            var atributos = campo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (atributos.Length > 0)
            {
                return atributos[0].Description;
            }
            return string.Empty;
        }

        public static T GetEnumByDescription<T>(string descricao)
        {
            if (!typeof(T).IsEnum)
            {
                return default(T);
            }

            var lista = EnumToList<T>();
            foreach (T item in lista)
            {
                if (GetDescription((Enum)Enum.Parse(typeof(T), item.ToString())).ToUpper() == descricao.ToUpper())
                {
                    return item;
                }
            }

            return default(T);
        }

        public static T GetObjectByJson<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
