using Ctrove.HR.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrove.HR.Extensions
{
    public static class HttpClientExtensions
    {
        public static HttpClient AddApiKeyTokenHeaders(this HttpClient cli, string apiKeyToken)
        {
            cli.DefaultRequestHeaders.Add(Constants.ApiKeyHeaderName, apiKeyToken);
            return cli;
        }

        public static async Task<HttpResponseMessage> GetWithQueryStringAsync(this HttpClient client, string uri, 
           QueryParamBuilder queryParamString)
        {
            var url = RequestQueryUriUtil.CreateUriWithQueryString(uri, queryParamString);
            return await client.GetAsync(url);
        }
    }
}
