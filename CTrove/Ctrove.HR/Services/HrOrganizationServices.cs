using CTrove.Core.Interface;
using CTrove.Core.DTO.HR;
using Ctrove.HR.Common;

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ctrove.HR.Extensions;
using System.Net.Http.Json;
using System.Text.Json;

namespace Ctrove.HR.Services
{
    public interface IHrOrganizationServices
    {
        Task<HrOrganizationResponse?> Add(HrOrganizationRequest req);
    }
    public class HrOrganizationServices : IHrOrganizationServices
    {
        private IOptions<HrContributorConfig> _HrConfig;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;

        public HrOrganizationServices(IOptions<HrContributorConfig> HrConfig, IUnitOfWork unitOfWork)
        {
            _HrConfig = HrConfig;
            _unitOfWork = unitOfWork;
            _httpClient = new HttpClient();
        }

        public async Task<HrOrganizationResponse?> Add(HrOrganizationRequest req)
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
                        .PostAsJsonAsync(HR_API_URL.URL_ORGANIZATION, req,
                         new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if(response.IsSuccessStatusCode)
                    {
                        var contentBody = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<HrOrganizationResponse>(contentBody,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                }
            }
            return null;
        }


    }
}
