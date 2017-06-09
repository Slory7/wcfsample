using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Framework.Core.Net.Http
{
    public static class HttpHelper
    {
        public static string BuildRequestParameters(IDictionary<string, string> parameters)
        {
            StringBuilder builder = new StringBuilder();
            bool flag = false;
            IEnumerator<KeyValuePair<string, string>> enumerator = parameters.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string str = enumerator.Current.Key;
                string str2 = enumerator.Current.Value;
                if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
                {
                    if (flag)
                    {
                        builder.Append("&");
                    }
                    builder.Append(str);
                    builder.Append("=");
                    builder.Append(Uri.EscapeDataString(str2));
                    flag = true;
                }
            }
            return builder.ToString();
        }

        public static IDictionary<string, string> ConvertParameters(NameValueCollection parameters)
        {
            var dict = parameters.Cast<string>()
                            .ToDictionary(p => p, p => parameters[p]);
            return dict;
        }
    }
}
