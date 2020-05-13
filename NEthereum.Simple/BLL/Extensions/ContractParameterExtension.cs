using Nethereum.ABI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEthereum.Simple.BLL.Extensions
{
    public static class ContractParameterExtension
    {
        public static object[] GetArguments<T>(this IEnumerable<Parameter> parameters, T obj)
        {
            if (obj == null || !parameters.Any())
                return new object[] { };

            var result = new object[parameters.Count()];
            var properties = obj.GetPropertiesInfo();
            var orderedParameters = parameters.OrderBy(x => x.Order);

            var i = 0;
            foreach (var parameter in orderedParameters)
            {
                var property = properties.FirstOrDefault(x => x.Name.ToLowerInvariant() == parameter.Name.ToLowerInvariant());
                if (property == null) continue;

                var value = property.GetValue(obj, null);
                result[i++] = value.ToString();
            }

            return result;
        }
    }
}
