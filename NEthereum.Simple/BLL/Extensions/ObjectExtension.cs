using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NEthereum.Simple.BLL.Extensions
{
    public static class ObjectExtension
    {
        public static IEnumerable<PropertyInfo> GetPropertiesInfo(this object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties().ToList();

            return properties;
        }
    }
}
