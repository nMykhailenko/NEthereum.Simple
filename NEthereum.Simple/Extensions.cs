using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NEthereum.Simple
{
    public static class Extensions
    {
        public static IEnumerable<PropertyInfo> GetPropertiesInfo(this object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties().ToList();

            return properties;
        }

        public static object[] GetArguments<T>(this IEnumerable<Parameter> parameters, T obj)
        {
            var result = new object[parameters.Count()];
            var properties = obj.GetPropertiesInfo();
            var orderedParameters = parameters.OrderBy(x => x.Order);

            var i = 0;
            foreach (var parameter in orderedParameters)
            {
                var property = properties.First(x => x.Name.ToLowerInvariant() == parameter.Name.ToLowerInvariant());
                var value = property.GetValue(obj, null);

                result[i++] = value.ToString();
            }

            return result;
        }

        public static T SetPropertiesValue<T>(this T obj, IEnumerable<ParameterOutput> parameters)
        {
            var properties = obj.GetPropertiesInfo();
            foreach (var parameter in parameters)
            {
                var property = properties.FirstOrDefault(x => x.Name.ToLowerInvariant() == parameter.Parameter.Name.ToLowerInvariant());
                if (property == null) throw new ArgumentNullException($"{parameter.Parameter.Name} is not found for object with type: {obj.GetType()}.");

                if (Type.GetTypeCode(property.PropertyType) == TypeCode.Int32 || Type.GetTypeCode(property.PropertyType) == TypeCode.UInt32)
                {
                    var value = parameter.Result.ToString();
                    property.SetValue(obj, Convert.ToInt32(value));
                }
                else
                {
                    property.SetValue(obj, parameter.Result);
                }

            }

            return obj;
        }
    }
}
