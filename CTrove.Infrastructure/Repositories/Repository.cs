using CTrove.Core.Entity;
using CTrove.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Infrastructure.Repositories
{
    internal class Repository
    {
    }

    public class ServiceTypesRepository : GenericRepository<ServiceType>, IServiceTypesRepository
    {
        public ServiceTypesRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class AuditRepository : GenericRepository<Audit>, IAuditRepository
    {
        public AuditRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class ClassificationRepository : GenericRepository<Classification>, IClassificationRepository
    {
        public ClassificationRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class PhaseRepository : GenericRepository<Phase>, IPhaseRepository
    {
        public PhaseRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class TherapeuticAreaRepository : GenericRepository<TherapeuticArea>, ITherapeuticAreaRepository
    {
        public TherapeuticAreaRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class StudyCountryRepository : GenericRepository<StudyCountry>, IStudyCountryRepository
    {
        public StudyCountryRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class RolesRepository : GenericRepository<Roles>, IRolesRepository
    {
        public RolesRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class AccessRepository : GenericRepository<Access>, IAccessRepository
    {
        public AccessRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class StudyRepository : GenericRepository<Study>, IStudyRepository
    {
        public StudyRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class VisitsRepository : GenericRepository<Visits>, IVisitsRepository
    {
        public VisitsRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class SitesRepository : GenericRepository<Sites>, ISitesRepository
    {
        public SitesRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class SitePhasesRepository : GenericRepository<SitePhases>, ISitePhasesRepository
    {
        public SitePhasesRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class SettingsRepository : GenericRepository<Settings>, ISettingsRepository
    {
        public SettingsRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class RolesPagesRepository : GenericRepository<RolesPages>, IRolesPagesRepository
    {
        public RolesPagesRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class EthnicityRepository : GenericRepository<Ethnicity>, IEthnicityRepository
    {
        public EthnicityRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class RaceRepository : GenericRepository<Race>, IRaceRepository
    {
        public RaceRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class SubjectPhasesRepository : GenericRepository<SubjectPhases>, ISubjectPhasesRepository
    {
        public SubjectPhasesRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class ContributorRepository : GenericRepository<Contributor>, IContributorRespository
    {
        public ContributorRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class ContributorStudyRepository : GenericRepository<ContributorStudy>, IContributorStudyRepository
    {
        public ContributorStudyRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class ContactTypeRepository : GenericRepository<ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }

    public class VendorTypeRepository : GenericRepository<VendorType>, IVendorTypeRespository
    {
        public VendorTypeRepository(CtroveDatabaseContext dbContext) : base(dbContext)
        { }
    }
}
