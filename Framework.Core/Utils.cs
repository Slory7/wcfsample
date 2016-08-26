using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.Core
{
    public static class Utils
    {
        public static void CopyProperty(object a, object b)
        {
            var typeOfA = a.GetType();
            var typeOfB = b.GetType();
            // copy fields
            //foreach (var fieldOfA in typeOfA.GetFields())
            //{
            //    var fieldOfB = typeOfB.GetField(fieldOfA.Name);
            //    if (fieldOfB != null)
            //        fieldOfB.SetValue(b, fieldOfA.GetValue(a));
            //}
            // copy properties
            foreach (var propertyOfA in typeOfA.GetProperties())
            {
                var propertyOfB = typeOfB.GetProperty(propertyOfA.Name);
                if (propertyOfB != null && propertyOfB.CanWrite && propertyOfB.PropertyType == propertyOfA.PropertyType)
                    propertyOfB.SetValue(b, propertyOfA.GetValue(a));
            }
        }
    }
}