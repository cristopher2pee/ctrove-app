using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IServiceTypesRepository _ServiceTypes { get; }
        IAuditRepository _Audit { get; }
        IClassificationRepository _Classification { get; }
        IPhaseRepository _Phase { get; }
        ITherapeuticAreaRepository _TherapeuticArea { get; } 
        IStudyCountryRepository _StudyCountry { get; }
        IRolesRepository _Roles { get; }

        IUserRepository _User { get; }

        IAccessRepository _Access { get; }

        IStudyRepository _Study { get; }

        IVisitsRepository _Visits { get; }

        ISitesRepository _Sites { get; }

        ISitePhasesRepository _SitePhases { get; }

        ISettingsRepository _Settings { get; }

        IRolesPagesRepository _RolesPages { get; }

        IEthnicityRepository _Ethnicity { get; }

        IRaceRepository _Race { get; }

        ISubjectRepository _Subject { get; }

        ISubjectPhasesRepository _SubjectPhases { get; }

        IContributorRespository _Contributor { get; }

        IContributorStudyRepository _ContributorStudy { get; }

        IOrganizationRepository _Organization { get; }

        ICountryRepository _Country { get; }

        IContactTypeRepository _ContactType { get; }

        IVendorTypeRespository _VendorType { get; }

        int Save();
        Task<int> SaveData(Guid userId, string remarks= "", bool isDelete = false, string location = "");
        Task auditTrailRetrieved(Guid userId, string location = "");
    }
}
