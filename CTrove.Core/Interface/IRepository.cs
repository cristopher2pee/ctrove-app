using CTrove.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Interface
{
    public interface IRepository
    {
    }

    public interface IServiceTypesRepository : IGenericRepository<ServiceType> { }
    public interface IAuditRepository : IGenericRepository<Audit> { }

    public interface IClassificationRepository : IGenericRepository<Classification> { }

    public interface IPhaseRepository : IGenericRepository<Phase> { }

    public interface ITherapeuticAreaRepository : IGenericRepository <TherapeuticArea> { }

    public interface IStudyCountryRepository : IGenericRepository<StudyCountry> { }

    public interface IRolesRepository : IGenericRepository<Roles> { }

    public interface IUserRepository : IGenericRepository<User> { }

    public interface IAccessRepository : IGenericRepository<Access> { }

    public interface IStudyRepository  : IGenericRepository<Study> { }

    public interface IVisitsRepository : IGenericRepository<Visits> { }
    public interface ISitesRepository : IGenericRepository<Sites> { }

    public interface ISitePhasesRepository : IGenericRepository<SitePhases> { }

    public interface ISettingsRepository : IGenericRepository<Settings> { }
    public interface IRolesPagesRepository : IGenericRepository<RolesPages> { }

    public interface IEthnicityRepository : IGenericRepository<Ethnicity> { }
    public interface IRaceRepository : IGenericRepository<Race> { }

    public interface ISubjectRepository : IGenericRepository<Subject> { }

    public interface ISubjectPhasesRepository : IGenericRepository<SubjectPhases> { }

    //HR-DB Table 
    public interface IContributorRespository : IGenericRepository<Contributor> { }

    public interface IContributorStudyRepository : IGenericRepository<ContributorStudy> { }
    public interface IOrganizationRepository  : IGenericRepository<Organization> { }

    public interface ICountryRepository : IGenericRepository<Country> { }

    public interface IContactTypeRepository  : IGenericRepository<ContactType> { }

    public interface IVendorTypeRespository : IGenericRepository<VendorType> { }

}
