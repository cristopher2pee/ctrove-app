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
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Net.Http.Json;

namespace Ctrove.HR.Services
{
    public interface IHrContactTypeServices
    {
        Task<HrContactTypeResponse?> Add(HrContactTypeRequest req);
    }
    public class HrContactTypeServices : IHrContactTypeServices
    {
        private IOptions<HrContributorConfig> _HrConfig;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        public HrContactTypeServices(IOptions<HrContributorConfig> HrConfig, IUnitOfWork unitOfWork)
        {
            _HrConfig = HrConfig;
            _unitOfWork = unitOfWork;
            _httpClient = new HttpClient();
        }

        public async Task<HrContactTypeResponse?> Add(HrContactTypeRequest req)
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
                        .PostAsJsonAsync(HR_API_URL.URL_CONTACT_TYPE, req,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (response.IsSuccessStatusCode)
                    {
                        var contentBody = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<HrContactTypeResponse>(contentBody,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }

                }
            }
            return null;
        }
    }
}
