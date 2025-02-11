using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.DTO.Response;
using CTrove.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Interface
{
    public interface ISitesServices
    {
        Task<SitesResponseDto?> GetbyId(Guid objId, Guid Id);

        Task<PagedResult<SitesResponse>> GetAllPagedList(SitesFilters filters, Guid objId);

        Task<List<SitesResponse>> GetList(Guid objId);

        Task<SitesResponse?> Save(Guid objId, SitesRequest req);

        Task<SitesResponse?> Update(Guid objId, SitesRequest req);

        Task<bool> Deactivate(Guid objectId, DeactivateRequest req);

        Task<bool> DeactivateSitePhases(Guid objectId, DeactivateRequest req);

        Task<SitePhasesResponseDto?> GetSitePhasesById(Guid id, Guid objId);

        Task<SitePhasesResponseDto?> SaveSitePhases(SitePhasesRequest req, Guid objId);

        Task<SitePhasesResponseDto?> UpdateSitePhases(SitePhasesRequest req, Guid objId);

        Task<bool> IsExist(string param);
    }
}
