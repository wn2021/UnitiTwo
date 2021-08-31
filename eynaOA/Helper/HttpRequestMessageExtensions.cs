using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace eynaOA.Helper
{
    public static class HttpRequestMessageExtensions {
        public static IDictionary<string, string> GetAllQueryParameters(this HttpRequestMessage request) {
            NameValueCollection queryString = HttpContext.Current.Request.QueryString;
            NameValueCollection form = HttpContext.Current.Request.Form;
            IDictionary<string, string> queryParameters = new Dictionary<string, string>();
            foreach (string key in queryString) {
                if (!queryParameters.ContainsKey(key)) {
                    queryParameters.Add(key, queryString[key]);
                }
            }
            foreach (string key in form) {
                if (!queryParameters.ContainsKey(key)) {
                    queryParameters.Add(key, form[key]);
                }
            }
            return queryParameters;
        }
    }

}