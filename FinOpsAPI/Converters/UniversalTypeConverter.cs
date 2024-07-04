
using FinOpsAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace FinOpsAPI.Converters
{
    public class UniversalTypeConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(UniversalType<T>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);

            if (token.Type == JTokenType.Object)
            {
                return new UniversalType<T> { Value = token.ToObject<T>() };
            }
            else if (token.Type == JTokenType.Array)
            {
                var array = token.ToObject<T[]>();
                // Check if T is already an array type (e.g., T[]), if so, return as is
                if (typeof(T).IsArray)
                {
                    return new UniversalType<T> { Value = (T)(object)array };
                }
                // Otherwise, return the first element of the array
                else
                {
                    return new UniversalType<T> { Value = array.Length > 0 ? array[0] : default(T) };
                }
            }
            else if (token.Type == JTokenType.String)
            {
                // Handle special case where T is string
                if (typeof(T) == typeof(string))
                {
                    return new UniversalType<T> { Value = (T)(object)token.ToString() };
                }
                else
                {
                    throw new JsonSerializationException($"Cannot convert string to type {typeof(T).Name}");
                }
            }
            else
            {
                throw new JsonSerializationException("Unexpected token type: " + token.Type);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var universalType = (UniversalType<T>)value;

            if (universalType.Value is T)
            {
                serializer.Serialize(writer, universalType.Value);
            }
            else if (universalType.Value is T[])
            {
                serializer.Serialize(writer, universalType.Value);
            }
            else if (universalType.Value is string)
            {
                serializer.Serialize(writer, universalType.Value);
            }
            else
            {
                throw new JsonSerializationException("Unexpected value type: " + universalType.Value.GetType());
            }
        }
    }

}