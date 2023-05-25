using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static object CompareValues(this object value1, object value2)
        {
            return !object.Equals(value1, value2) ? value1 : null;
        }
    }
}
