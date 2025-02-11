using CTrove.Core.Interface;
using Ctrove.HR.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core.DTO.HR;
using Ctrove.HR.Extensions;
using System.Text.Json;

namespace Ctrove.HR.Services
{
    public interface IHrAuthenticationServices
    {
        Task<HrTokenResponse?> GetTokenResponse(Guid objId, Guid studyId);
    }
    public class HrAuthenticationServices : IHrAuthenticationServices
    {
        private IOptions<HrContributorConfig> _HrConfig;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;

        public HrAuthenticationServices(IOptions<HrContributorConfig> HrConfig, IUnitOfWork unitOfWork)
        {
            _HrConfig = HrConfig;
            _unitOfWork = unitOfWork;
            _httpClient = new HttpClient();
        }

        public async Task<HrTokenResponse?> GetTokenResponse(Guid objId, Guid studyId)
        {
            if (!object.Equals(objId, Guid.Empty) || !object.Equals(studyId, Guid.Empty))
            {
                _httpClient.BaseAddress = new Uri(_HrConfig.Value.ApiUrl);
                var queryString = new QueryParamBuilder();

                queryString.Add("objectId", objId);
                queryString.Add("StudyId", studyId);

                var response = await _httpClient
                    .GetWithQueryStringAsync(HR_API_URL.URL_GENERATE_TOKEN, queryString);

                if (response.IsSuccessStatusCode)
                {
                    var contentBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<HrTokenResponse>(contentBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
                }
            }

            return null;
        }
    }
}
