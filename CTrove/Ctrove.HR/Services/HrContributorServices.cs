using CTrove.Core.Interface;
using Ctrove.HR.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctrove.HR.DTO;
using Microsoft.EntityFrameworkCore;
using Ctrove.HR.Extensions;
using System.Text.Json;
using Ctrove.HR.Filters;
using CTrove.Core.DTO.HR;
using System.Net.Http.Json;
using CTrove.Core.Filters;

namespace Ctrove.HR.Services
{
    public interface IHrContributorServices
    {
        //Task<IEnumerable<HrContributorResponse>?> GetList();
        //Task<HrContributPageResponse?> GetListPage(ContributorFilters filters);
        //Task<HrContributorResponse?> Get(Guid id, Guid objectId);

        //Task<HrContributPageResponse?> Add(HrContributorResponse hrContributorResponse);
        Task<HrContributorResponse?> Add(HrContributorRequest req);
        Task<HrContributorResponse?> Update(HrContributorRequest req);
        Task<HrContributorPageResponse?> GetListPage(ContributorFiltes filters);

        Task<List<HrContributorResponse>?> SearchContributor(string paramSearch);

    }
    public class HrContributorServices : IHrContributorServices
    {
        private IOptions<HrContributorConfig> _HrConfig;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        public HrContributorServices(IOptions<HrContributorConfig> HrConfig, IUnitOfWork unitOfWork)
        {
            _HrConfig = HrConfig;
            _unitOfWork = unitOfWork;
            _httpClient = new HttpClient();
        }

        public async Task<HrContributorResponse?> Update(HrContributorRequest req)
        {
            if (req == null) return null;

            var studyList = await _unitOfWork._Study.GetDbSet()
                .ToListAsync();

            if (studyList.Any())
            {
                if (!string.IsNullOrEmpty(studyList[0].ApiKeyToken))
                {
                    _httpClient.BaseAddress = new Uri(_HrConfig.Value.ApiUrl);
                    var response = await _httpClient
                         .AddApiKeyTokenHeaders(studyList[0].ApiKeyToken!)
                         .PutAsJsonAsync(HR_API_URL.URL_CONTRIBUTOR, req,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if(response.IsSuccessStatusCode)
                    {
                        var contentBody = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<HrContributorResponse>(contentBody,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                }
            }

            return null;
        }

        public async Task<HrContributorResponse?> Add(HrContributorRequest req)
        {
            if (req == null) return null;

            var studyList = await _unitOfWork._Study.GetDbSet()
                .ToListAsync();

            if (studyList.Any())
            {
                if (!string.IsNullOrEmpty(studyList[0].ApiKeyToken))
                {
                    _httpClient.BaseAddress = new Uri(_HrConfig.Value.ApiUrl);
                    var response = await _httpClient
                        .AddApiKeyTokenHeaders(studyList[0].ApiKeyToken!)
                        .PostAsJsonAsync(HR_API_URL.URL_CONTRIBUTOR, req,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (response.IsSuccessStatusCode)
                    {
                        var contentBody = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<HrContributorResponse>(contentBody,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }

                }
            }
            return null;
        }

        //public async Task<HrContributorResponse?> Get(Guid id, Guid objectId)
        //{
        //    var studyList = await _unitOfWork._Study.GetDbSet()
        //        .ToListAsync();

        //    if (studyList.Any())
        //    {
        //        if (!string.IsNullOrEmpty(studyList[0].ApiKeyToken))
        //        {
        //            _httpClient.BaseAddress = new Uri(_HrConfig.Value.ApiUrl);
        //            var queryString = new QueryParamBuilder();

        //            if (id == Guid.Empty || objectId == Guid.Empty)
        //                return null;

        //            queryString.Add("Id", id);
        //            queryString.Add("ObjectId", objectId);

        //            var response = await _httpClient
        //                    .AddApiKeyTokenHeaders(studyList[0].ApiKeyToken!)
        //                    .GetWithQueryStringAsync(HR_API_URL.GET_CONTRIBUTOR_BY_ID, queryString);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var contentBody = await response.Content.ReadAsStringAsync();
        //                return JsonSerializer.Deserialize<HrContributorResponse>
        //                    (contentBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //            }
        //        }
        //    }
        //    return null;
        //}

        public async Task<List<HrContributorResponse>?> SearchContributor(string paramSearch)
        {
            if(string.IsNullOrEmpty(paramSearch)) return null;
            var studyList = await _unitOfWork._Study.GetDbSet()
                .AsNoTracking()
                .ToListAsync();


            if (studyList.Any())
            {
                if (!string.IsNullOrEmpty(studyList[0].ApiKeyToken))
                {
                    _httpClient.BaseAddress = new Uri(_HrConfig.Value.ApiUrl);
                    QueryParamBuilder queryString = new QueryParamBuilder();

                    if (!string.IsNullOrEmpty(paramSearch))
                        queryString.Add("paramSearch", paramSearch);

                    var response = await _httpClient
                        .AddApiKeyTokenHeaders(studyList[0].ApiKeyToken!)
                        .GetWithQueryStringAsync(HR_API_URL.URL_SEARCH_CONTRIBUTOR, queryString);

                    if (response.IsSuccessStatusCode)
                    {
                        var contentBody = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<List<HrContributorResponse>>
                             (contentBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }

                }
            }
            return null; 
        }

        public async Task<HrContributorPageResponse?> GetListPage(ContributorFiltes filters)
        {
            var studyList = await _unitOfWork._Study.GetDbSet()
                .AsNoTracking()
                .ToListAsync();

            if (studyList.Any())
            {
                if (!string.IsNullOrEmpty(studyList[0].ApiKeyToken))
                {
                    _httpClient.BaseAddress = new Uri(_HrConfig.Value.ApiUrl);
                    QueryParamBuilder queryString = new QueryParamBuilder();

                    if (!string.IsNullOrEmpty(filters.Search)) 
                        queryString.Add("Search", filters.Search);

                    queryString.Add("Status", filters.Status);
                    queryString.Add("Page", filters.Page);
                    queryString.Add("Limit", filters.Limit);

                    var response = await _httpClient
                        .AddApiKeyTokenHeaders(studyList[0].ApiKeyToken!)
                        .GetWithQueryStringAsync(HR_API_URL.URL_CONTRIBUTOR_PAGE_LIST, queryString);

                    if(response.IsSuccessStatusCode)
                    {
                        var contentBody = await response.Content.ReadAsStringAsync();
                       return JsonSerializer.Deserialize<HrContributorPageResponse>
                            (contentBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }

                }
            }


            return null;
        }

        //public async Task<HrContributPageResponse?> GetListPage(ContributorFilters filters)
        //{
        //    var studyList = await _unitOfWork._Study.GetDbSet()
        //         .ToListAsync();

        //    if (studyList.Any())
        //    {
        //        if (!string.IsNullOrEmpty(studyList[0].ApiKeyToken))
        //        {
        //            _httpClient.BaseAddress = new Uri(_HrConfig.Value.ApiUrl);
        //            QueryParamBuilder queryString = new QueryParamBuilder();

        //            if (filters.OrganizationIds != null)
        //            {
        //                foreach (var item in filters.OrganizationIds)
        //                {
        //                    queryString.Add("OrganizationIds", item.ToString());
        //                }
        //            }

        //            if (filters.CountryIds != null)
        //            {
        //                foreach (var item in filters.CountryIds)
        //                {
        //                    queryString.Add("CountryIds", item.ToString());
        //                }
        //            }

        //            if (!string.IsNullOrEmpty(filters.Search)) queryString.Add("Search", filters.Search);

        //            queryString.Add("Active", filters.Active);
        //            queryString.Add("Page", filters.Page);
        //            queryString.Add("Limit", filters.Limit);

        //            var response = await _httpClient
        //                .AddApiKeyTokenHeaders(studyList[0].ApiKeyToken!)
        //                .GetWithQueryStringAsync(HR_API_URL.GET_CONTRIBUTOR_PAGE_LIST, queryString);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var contentBody = await response.Content.ReadAsStringAsync();
        //                return JsonSerializer.Deserialize<HrContributPageResponse>
        //                    (contentBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //            }
        //        }
        //    }

        //    return null;
        //}
        //public async Task<IEnumerable<HrContributorResponse>?> GetList()
        //{
        //    var studyList = await _unitOfWork._Study.GetDbSet()
        //        .ToListAsync();

        //    if (studyList.Any())
        //    {
        //        if (!string.IsNullOrEmpty(studyList[0].ApiKeyToken))
        //        {
        //            _httpClient.BaseAddress = new Uri(_HrConfig.Value.ApiUrl);
        //            var response = await _httpClient
        //                  .AddApiKeyTokenHeaders(studyList[0].ApiKeyToken!)
        //                  .GetAsync(HR_API_URL.GET_CONTRIBUTOR_LIST);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var contentBody = await response.Content.ReadAsStringAsync();
        //                return JsonSerializer.Deserialize<List<HrContributorResponse>>
        //                    (contentBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //            }
        //        }
        //    }

        //    return null;
        //}
    }
}
