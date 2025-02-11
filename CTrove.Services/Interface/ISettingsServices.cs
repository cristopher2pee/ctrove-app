using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using CTrove.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Interface
{
    public interface ISettingsServices
    {
        Task<PagedResult<Settings>> GetListPage(BaseFilters filters);

        Task<Settings?> GetById(Guid id);

        Task<Settings?> Save(SettingsRequest entity, Guid objId);

        Task<Settings?> Update(SettingsRequest entity, Guid objId);

        Task<bool> Deactivate(DeactivateRequest req, Guid objId);

        Task<Settings?> GetByUserId(Guid userId);

        Task<Settings> SavedSettingsOnBoarding(SettingsRequest req);

        Task<string> GetTimeZoneSetting(Guid userId);
    }
}
