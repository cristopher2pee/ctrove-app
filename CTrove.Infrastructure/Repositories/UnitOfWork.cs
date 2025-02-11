using CTrove.Core.Interface;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTrove.Core;
using Microsoft.EntityFrameworkCore;
using CTrove.Core.Enum;
using CTrove.Core.Entity;

namespace CTrove.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CtroveDatabaseContext _dbContext;

        public IServiceTypesRepository _ServiceTypes { get; }
        public IAuditRepository _Audit { get; }

        public ITherapeuticAreaRepository _TherapeuticArea { get; }

        public IPhaseRepository _Phase { get; }

        public IClassificationRepository _Classification { get; }

        public IRolesRepository _Roles { get; }

        public IStudyCountryRepository _StudyCountry { get; }

        public IUserRepository _User { get; }

        public IAccessRepository _Access { get; }

        public IStudyRepository _Study { get; }

        public IVisitsRepository _Visits { get; }

        public ISitesRepository _Sites { get; }

        public ISitePhasesRepository _SitePhases { get; }

        public ISettingsRepository _Settings { get; }

        public IRolesPagesRepository _RolesPages { get; }

        public IEthnicityRepository _Ethnicity { get; }

        public IRaceRepository _Race { get; }

        public ISubjectRepository _Subject { get; }

        public ISubjectPhasesRepository _SubjectPhases { get; }

        public IContributorRespository _Contributor { get; }

        public IContributorStudyRepository _ContributorStudy { get; }

        public IOrganizationRepository _Organization { get; }

        public ICountryRepository _Country { get; }

        public IContactTypeRepository _ContactType { get; }

        public  IVendorTypeRespository _VendorType { get; }

        public UnitOfWork(
            CtroveDatabaseContext dbContext,
            IServiceTypesRepository serviceTypes,
            IAuditRepository auditTrails,
            ITherapeuticAreaRepository therapeuticArea,
            IPhaseRepository phase,
            IClassificationRepository classification,
            IRolesRepository roles,
            IStudyCountryRepository studyCountry,
            IUserRepository user,
            IAccessRepository access,
            IStudyRepository study,
            IVisitsRepository visits,
            ISitesRepository sites,
            ISitePhasesRepository sitePhases,
            ISettingsRepository settings,
            IRolesPagesRepository rolesPages,
            IEthnicityRepository ethnicity,
            IRaceRepository race,
            ISubjectRepository subject,
            ISubjectPhasesRepository subjectPhases,
            IContributorRespository contributor,
            IContributorStudyRepository contributorStudy,
            IOrganizationRepository organization,
            ICountryRepository country,
            IContactTypeRepository contactType,
            IVendorTypeRespository vendorType
            )
        {
            _dbContext = dbContext;
            _ServiceTypes = serviceTypes;
            _Audit = auditTrails;
            _TherapeuticArea = therapeuticArea;
            _Phase = phase;
            _Classification = classification;
            _Roles = roles;
            _StudyCountry = studyCountry;
            _User = user;
            _Access = access;
            _Study = study;
            _Visits = visits;
            _Sites = sites;
            _SitePhases = sitePhases;
            _Settings = settings;
            _RolesPages = rolesPages;
            _Ethnicity = ethnicity;
            _Race = race;
            _Subject = subject;
            _SubjectPhases = subjectPhases;
            _Contributor = contributor;
            _ContributorStudy = contributorStudy;
            _Organization = organization;
            _Country = country;
            _ContactType = contactType;
            _VendorType = vendorType;
        }
        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveData(Guid guid, string remarks, bool isDelete = false, string location ="")
        {
            await OnBeforeSaveChanged(guid, remarks, isDelete, location);
            return _dbContext.SaveChanges();
        }

        private async Task OnBeforeSaveChanged(Guid userId, string remarks = "", bool isDelete = false, string location="")
        {
            _dbContext.ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                if (entry.Entity is AuditEntry || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntry.Remarks = remarks;
                auditEntry.Location = location;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        //get primary key
                        //auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        auditEntry.TablePKey = property.CurrentValue.ToString();
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Bin;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = isDelete ? AuditType.Bin : AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;

                    }
                }

            }

            foreach (var auditEntry in auditEntries)
            {
                _dbContext.Add(auditEntry.ToAudit());
            }

        }

        public async Task auditTrailRetrieved(Guid userId, string location ="")
        {
            await PerformRetrieved(userId, location);
            _dbContext.SaveChanges();

        }

        private async Task PerformRetrieved (Guid userId, string location = "")
        {
            _dbContext.ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                var TableName = entry.Entity.GetType().Name;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntry.Remarks = "";
                auditEntry.Location = "";

                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        //get primary key
                        //auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        auditEntry.TablePKey = property.CurrentValue.ToString();
                        continue;
                    }
                    auditEntry.AuditType = AuditType.View;
                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                }
            }

            foreach (var auditEntry in auditEntries)
            {
                _dbContext.Audit.Add(auditEntry.ToAudit());
            }
        }

        private async Task getAuditPerformed()
        {

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
