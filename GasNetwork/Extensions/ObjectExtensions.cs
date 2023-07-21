using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DynamicData;

namespace GasNetwork.Extensions
{
    public static class ObjectExtensions
    {
        public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
            where T : class
            where TU : class
        {
            var sourceProps = typeof(T)
               .GetProperties()
               .Where(x => x.CanRead);

            var destProps = typeof(TU)
               .GetProperties()
               .Where(x => x.CanWrite)
               .ToArray();

            foreach (var sourceProp in sourceProps)
            {
                Array.Find(destProps, x => x.Name.IsEqualTo(sourceProp.Name))
                  ?.SetValue(dest, sourceProp.GetValue(source, null), null);
            }
        }

        public static PropertyInfo[] GetPropertiesByOrderAttribute<T, U>(this T source) 
            where T : IDataGridRepresenter
            where U : DisplayDataGridAttribute
        {
            var properties = source
                .GetType()
                .GetProperties()
                .Where(x => x.IsDefined(typeof(U)));

            var sortedProperties = 
                from p in properties 
                orderby p.GetCustomAttribute<U>().Order 
                select p;

            return sortedProperties.ToArray();
        }
    }
}
