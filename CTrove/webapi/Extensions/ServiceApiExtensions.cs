using CTrove.Services.Interface;
using CTrove.Services.Services;
using CTrove.Api.Azure;
using Ctrove.HR.Services;

namespace CTrove.Api.Extensions
{
    public static class ServiceApiExtensions
    {
        public static IServiceCollection ServicesApiExtensions(this IServiceCollection services)
        {
            //entity services
            services.AddScoped<IServiceTypesServices, ServiceTypeServices>();
            services.AddScoped<IAuditTrailServices, AuditTrailServices>();
            services.AddScoped<IClassificationServices, ClassificationServices>();
            services.AddScoped<IPhaseServices, PhaseServices>();
            services.AddScoped<ITherapeuticAreaServices, TherapeuticAreaServices>();
            services.AddScoped<IRolesServices, RolesServices>();
            services.AddScoped<IStudyCountryServices, StudyCountryServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IPermissionServices, PermissionService>();
            services.AddScoped<IAccessServices, AccessServices>();
            services.AddScoped<IStudyServices, StudyServices>();
            services.AddScoped<ISitesServices, SitesServices>();
            services.AddScoped<ISettingsServices, SettingsServices>();
            services.AddScoped<IVisitsServices, VisitsServices>();
            services.AddScoped<IRolesPagesServices, RolesPagesServices>();
            services.AddScoped<IEthnicityServices, EthnicityServices>();
            services.AddScoped<IRaceServices, RaceServices>();
            services.AddScoped<ISubjectServices, SubjectServices>();

            services.AddScoped<IOrganizationServices, OrganizationServices>();
            services.AddScoped<IContributorServices, ContributorServices>();
            services.AddScoped<IContributorStudyServices, ContributorStudyServices>();
            services.AddScoped<ICountryServices, CountryServices>();
            services.AddScoped<IContactTypeServices, ContactTypeServices>();
            services.AddScoped<IVendorTypeServices, VendorTypeServices>();

            //Other Services
            services.AddScoped<ITimeZoneServices, TimeZoneServices>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AzureAdServices, AzureAdServices>();
            services.AddScoped<AuthService, AuthService>();

            //HR
            services.AddScoped<IHrCountryServices, HrCountryServices>();
            services.AddScoped<IHrContributorServices, HrContributorServices>();
            services.AddScoped<IHrAuthenticationServices, HrAuthenticationServices>();
            services.AddScoped<IHrOrganizationServices, HrOrganizationServices>();
            services.AddScoped<IHrContributorStudyServices, HrContributorStudyServices>();
            services.AddScoped<IHrContactTypeServices, HrContactTypeServices>();

            return services;
        }
    }
}
