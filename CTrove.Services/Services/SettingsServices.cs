using CTrove.Core.Common;
using CTrove.Core.DTO.Request;
using CTrove.Core.Entity;
using CTrove.Core.Filters;
using CTrove.Core.Interface;
using CTrove.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Services.Services
{
    public class SettingsServices : ISettingsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public SettingsServices(IUnitOfWork unitOfwork)
        {
            _unitOfWork = unitOfwork;
        }
        public async Task<PagedResult<Settings>> GetListPage(BaseFilters filters)
        {
            return null;
        }

        public async Task<Settings?> GetById(Guid id)
        {
            var settings = await _unitOfWork._Settings.GetDbSet()
                .Include(f => f.User)
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();

            if (settings == null)
                return null;

            return settings;
        }

        public async Task<Settings?> Save(SettingsRequest entity, Guid objId)
        {
            var _entity = new Settings
            {
                Id = Guid.NewGuid(),
                TimeZone = entity.TimeZone,
                UserId = entity.UserId
            };

            await _unitOfWork._Settings.Add(_entity);
            var res = await _unitOfWork.SaveData(objId, remarks: entity.Remarks ?? "", location: entity.Location ?? "");
            if (res > 0) return _entity; else return null;
        }

        public async Task<Settings?> Update(SettingsRequest entity, Guid objId)
        {
            if (entity is null) return null;

            var settings = await _unitOfWork._Settings.GetById(entity.Id);
            if (settings is null) return null;

            settings.UserId = entity.UserId;
            settings.TimeZone = entity.TimeZone;

            await _unitOfWork._Settings.Update(settings);
            var result = await _unitOfWork.SaveData(objId, remarks: entity.Remarks, location: entity.Location);

            if (result > 0) return settings; else return null;
        }

        public async Task<bool> Deactivate(DeactivateRequest req, Guid objId)
        {
            return false;
        }

        public async Task<Settings?> GetByUserId(Guid userId)
        {
            var entity = await _unitOfWork._Settings.GetDbSet()
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            if (entity is null) return null;
            return entity;
        }

        public async Task<Settings> SavedSettingsOnBoarding(SettingsRequest req)
        {
            return null;
        }

        public async Task<string> GetTimeZoneSetting(Guid userId)
        {
            var setting = await _unitOfWork._Settings.GetDbSet()
                .AsNoTracking()
                .Where(f => f.UserId == userId)
                .FirstOrDefaultAsync();

            return setting!.TimeZone;
        }

    }
}
