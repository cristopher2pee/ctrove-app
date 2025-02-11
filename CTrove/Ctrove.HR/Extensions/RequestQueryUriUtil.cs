using Ctrove.HR.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ctrove.HR.Extensions
{
    public static class RequestQueryUriUtil
    {
        public static string CreateUriWithQueryString(string url,
            QueryParamBuilder queryParams)
        {
            bool startingQuestionMarkAdded = false;
            var stringQueryBuilder = new StringBuilder();

            stringQueryBuilder.Append(url);
            foreach (var param in queryParams)
            {
                if (param.Value is null) continue;

                stringQueryBuilder.Append(startingQuestionMarkAdded ? "&" : "?");
                stringQueryBuilder.Append(param.Key);
                stringQueryBuilder.Append("=");
                stringQueryBuilder.Append(param.Value);
                startingQuestionMarkAdded = true;
            }

            return stringQueryBuilder.ToString();
        }
    }
}
