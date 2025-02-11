using Ctrove.HR.Common;
using Ctrove.HR.DTO;
using Ctrove.HR.Extensions;
using CTrove.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ctrove.HR.Services
{
    public interface IHrCountryServices
    {
        Task<IEnumerable<CountryResponse>?> GetList();
    }
    public class HrCountryServices : IHrCountryServices
    {
        private IOptions<HrContributorConfig> _HrConfig;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        public HrCountryServices(IOptions<HrContributorConfig> HrConfig, IUnitOfWork unitOfWork)
        {
            _HrConfig = HrConfig;
            _unitOfWork = unitOfWork;
            _httpClient = new HttpClient();
        }


        public async Task<IEnumerable<CountryResponse>?> GetList()
        {
            var studyList = await _unitOfWork._Study.GetDbSet()
                .ToListAsync();

            if (studyList.Any())
            {
                if (!string.IsNullOrEmpty(studyList[0].ApiKeyToken))
                {
                    _httpClient.BaseAddress = new Uri(_HrConfig.Value.ApiUrl);
                    var response = await _httpClient
                        .AddApiKeyTokenHeaders(studyList[0].ApiKeyToken!)
                        .GetAsync(HR_API_URL.GET_COUNTRY_LIST);

                    if(response.IsSuccessStatusCode)
                    {
                        var contentBody = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<List<CountryResponse>>
                            (contentBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
                    }
                }
            } 

            return null;
        }
    }
}
