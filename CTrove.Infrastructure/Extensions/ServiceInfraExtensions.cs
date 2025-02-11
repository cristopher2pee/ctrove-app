using CTrove.Core.Filters;
using CTrove.Core.Interface;
using CTrove.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Infrastructure.Extensions
{
    public static class ServiceInfraExtensions
    {
        public static IServiceCollection ServiceInfrastructureExtensions(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServiceTypesRepository, ServiceTypesRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IClassificationRepository, ClassificationRepository>();
            services.AddScoped<ITherapeuticAreaRepository, TherapeuticAreaRepository>();
            services.AddScoped<IPhaseRepository, PhaseRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IStudyCountryRepository, StudyCountryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccessRepository, AccessRepository>();
            services.AddScoped<IStudyRepository, StudyRepository>();
            services.AddScoped<IVisitsRepository, VisitsRepository>();
            services.AddScoped<ISitesRepository, SitesRepository>();
            services.AddScoped<ISitePhasesRepository, SitePhasesRepository>();
            services.AddScoped<ISettingsRepository, SettingsRepository>();
            services.AddScoped<IRolesPagesRepository, RolesPagesRepository>();
            services.AddScoped<IEthnicityRepository, EthnicityRepository>();
            services.AddScoped<IRaceRepository, RaceRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ISubjectPhasesRepository, SubjectPhasesRepository>();

            //HR
            services.AddScoped<IContributorRespository, ContributorRepository>();
            services.AddScoped<IContributorStudyRepository, ContributorStudyRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IContactTypeRepository, ContactTypeRepository>();
            services.AddScoped<IVendorTypeRespository, VendorTypeRepository>();
            return services;
        }
    }
}
