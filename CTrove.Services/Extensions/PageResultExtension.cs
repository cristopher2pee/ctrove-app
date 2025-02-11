using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core.Common;

namespace CTrove.Services.Extensions
{
    public static class PageResultExtension
    {
        public static PagedResult<T> ToPagedList<T>(this IEnumerable<T> query, int page, int limit)
        {
            var result = new PagedResult<T>(page, limit, query.Count());

            result.Data = query.Skip((page - 1) * limit).Take(limit).ToList();

            return result;
        }

        public static PagedResult<T> ToPagedListQueryable<T>(this IQueryable<T> query, int page, int limit)
        {
            var result = new PagedResult<T>(page, limit, query.Count());

            result.Data = query.Skip((page - 1) * limit).Take(limit).ToList();

            return result;
        }
    }
}
