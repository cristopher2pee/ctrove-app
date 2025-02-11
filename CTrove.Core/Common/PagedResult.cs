using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Common
{
    public class PagedResult<T>
    {
        public PagedResult(int page, int limit, int count)
        {
            var lastPage = (int)Math.Ceiling((decimal)count / limit);

            Meta = new PagedMeta
            {
                Page = lastPage < page ? lastPage : page,
                Total = count,
                Limit = limit,
                LastPage = lastPage
            };

        }
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
        public PagedMeta Meta { get; set; }
    }
}
