using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Extensions
{
    public static class ClassExtensions
    {
        public static bool VerifyDuplicatedIdPropertyValues<T>(this T obj) where T : class
        {
            var names = new List<string>();
            var strEmptyCount = 0;

            foreach (var item in obj.GetType().GetProperties().Where(x => x.Name.Contains("Id")))
            {
                var value = item.GetValue(obj)?.ToString() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(value))
                    strEmptyCount++;
                else
                    names.Add(value);
            }

            return names.Distinct().Count() + strEmptyCount != obj.GetType().GetProperties().Count(x => x.Name.Contains("Id"));
        }

        public static List<string> GetIdPropertyList<T>(this T obj) where T : class
        {
            var names = new List<string>();
            foreach (var item in obj.GetType().GetProperties().Where(x => x.Name.Contains("Id")))
            {
                names.Add(item.GetValue(obj).ToString());
            }

            return names;
        }

        public static void MapEntity<T>(this T entityTo, object entityFrom) where T : class
        {
            foreach (var item in entityTo.GetType().GetProperties())
            {
                var sameProp = entityFrom.GetType().GetProperty(item.Name);

                if (sameProp is not null)
                    item.SetValue(entityTo, sameProp.GetValue(entityFrom));
            }
        }

        public static bool HasDifferentPropertyValues<T>(this T entity, object entityUpdated) where T : class
        {
            foreach (var item in entity.GetType().GetProperties())
            {
                var sameProp = entityUpdated.GetType().GetProperty(item.Name);
                var value1 = item.GetValue(entity);
                var value2 = sameProp.GetValue(entityUpdated);                

                if (sameProp is not null && !object.Equals(value1, value2))
                    return true;
            }
            return false;
        }       
    }
}
