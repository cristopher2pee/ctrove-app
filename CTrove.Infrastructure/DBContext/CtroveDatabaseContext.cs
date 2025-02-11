using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CTrove.Core.Entity;
using CTrove.Core.Enum;

namespace CTrove.Infrastructure
{
    public class CtroveDatabaseContext : DbContext
    {
        public CtroveDatabaseContext(DbContextOptions<CtroveDatabaseContext> contextOptions) : base(contextOptions)
        { }
        public DbSet<Audit> Audit { get; set; }
        public DbSet<ServiceType> ServiceType { get; set; }
        public DbSet<TherapeuticArea> TherapeuticArea { get; set; }
        public DbSet<Classification> Classification { get; set; }
        public DbSet<Phase> Phase { get; set; }
        public DbSet<StudyCountry> StudyCountry { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Study> Study { get; set; }
        public DbSet<Access> Access { get; set; }
        public DbSet<Sites> Sites { get; set; }
        public DbSet<SitePhases> SitePhases { get; set; }
        public DbSet<Visits> Visits { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<SubjectPhases> SubjectPhases { get; set; }
        //Settings User
        public DbSet<Settings> Settings { get; set; }
        public DbSet<RolesPages> RolesPages { get; set; }

        public DbSet<Ethnicity> Ethnicity { get; set; }

        public DbSet<Race> Race { get; set; }

        //Hr Contributor

        public DbSet<Country> Country { get; set; }
        public DbSet<ContactType> ContactType { get; set; }
        public DbSet<VendorType> VendorType { get; set; }

        public DbSet<ContributorStudy> ContributorStudy { get; set; }

        public DbSet<Contributor> Contributor { get; set; }

        public DbSet<Organization> Organization { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Roles>().HasData
                (
                  new { Id = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"), Code = "Admin", Name = "Administrator", Blinded = false, Status = true },
                   new { Id = Guid.NewGuid(), Code = "User", Name = "Normal User", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "SPPM-UBLD", Name = "Sponsor - Project Manager", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "SPCRA-UBLD", Name = "Sponsor - CRA", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "SPCTA-UBLD", Name = "Sponsor - CTA", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "SPDM-UBLD", Name = "Sponsor - Data Manager", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "SPREG-UBLD", Name = "Sponsor - Regulatory", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "SPQA-UBLD", Name = "Sponsor - Quality Assurance", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "SPMD-UBLD", Name = "Sponsor - Medical", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "CLPM-UBLD", Name = "CL - Project Manager", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "CLCRA-UBLD", Name = "CL - CRA", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "CLCTA-UBLD", Name = "CL - CTA", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "CLDM-UBLD", Name = "CL - Data Manager", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "CLREG-UBLD", Name = "CL - Regulatory", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "CLQA-UBLD", Name = "CL - Quality Assurance", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "CLCIS-UBLD", Name = "CL - CIS", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "STPI-UBLD", Name = "Site - Principal Investigator", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "STSI-UBLD", Name = "Site - Sub Investigator", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "STPM-UBLD", Name = "Site - Project Manager", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "STDM-UBLD", Name = "Site - Data Manager", Blinded = false, Status = true },

                 new { Id = Guid.NewGuid(), Code = "STSTF-UBLD", Name = "Site - Staff", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "STCOR-UBLD", Name = "Site - Coordinator", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "STSPLD-UBLD", Name = "Site - Support Lead", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "STSPAS-UBLD", Name = "Site - Support Lead Associate", Blinded = false, Status = true },

                 new { Id = Guid.NewGuid(), Code = "SPPM-BLD", Name = "Sponsor - Project Manager", Blinded = true, Status = true },
                 new { Id = Guid.NewGuid(), Code = "SPCRA-BLD", Name = "Sponsor - CRA", Blinded = true, Status = true },
                new { Id = Guid.NewGuid(), Code = "SPCTA-BLD", Name = "Sponsor - CTA", Blinded = true, Status = true },
                 new { Id = Guid.NewGuid(), Code = "SPDM-BLD", Name = "Sponsor - Data Manager", Blinded = true, Status = true },
                 new { Id = Guid.NewGuid(), Code = "SPREG-BLD", Name = "Sponsor - Regulatory", Blinded = true, Status = true },
                 new { Id = Guid.NewGuid(), Code = "SPQA-BLD", Name = "Sponsor - Quality Assurance", Blinded = true, Status = true },
                new { Id = Guid.NewGuid(), Code = "SPMD-BLD", Name = "Sponsor - Medical", Blinded = true, Status = true },
                new { Id = Guid.NewGuid(), Code = "CLPM-BLD", Name = "CL - Project Manager", Blinded = true, Status = true },
                 new { Id = Guid.NewGuid(), Code = "CLCRA-BLD", Name = "CL - CRA", Blinded = true, Status = true },
                new { Id = Guid.NewGuid(), Code = "CLCTA-BLD", Name = "CL - CTA", Blinded = true, Status = true },
                 new { Id = Guid.NewGuid(), Code = "CLDM-BLD", Name = "CL - Data Manager", Blinded = true, Status = true },
                 new { Id = Guid.NewGuid(), Code = "CLREG-BLD", Name = "CL - Regulatory", Blinded = true, Status = true },
                 new { Id = Guid.NewGuid(), Code = "CLQA-BLD", Name = "CL - Quality Assurance", Blinded = true, Status = true },
                new { Id = Guid.NewGuid(), Code = "CLCIS-BLD", Name = "CL - CIS", Blinded = true, Status = true },
                new { Id = Guid.NewGuid(), Code = "STPI-BLD", Name = "Site - Principal Investigator", Blinded = true, Status = true },
                 new { Id = Guid.NewGuid(), Code = "STSI-BLD", Name = "Site - Sub Investigator", Blinded = true, Status = true },
                 new { Id = Guid.NewGuid(), Code = "STPM-BLD", Name = "Site - Project Manager", Blinded = true, Status = true },
                new { Id = Guid.NewGuid(), Code = "STDM-BLD", Name = "Site - Data Manager", Blinded = true, Status = true },

                  new { Id = Guid.NewGuid(), Code = "STSTF-BLD", Name = "Site - Staff", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "STCOR-BLD", Name = "Site - Coordinator", Blinded = false, Status = true },
                 new { Id = Guid.NewGuid(), Code = "STSPLD-BLD", Name = "Site - Support Lead", Blinded = false, Status = true },
                new { Id = Guid.NewGuid(), Code = "STSPAS-BLD", Name = "Site - Support Lead Associate", Blinded = false, Status = true }

                );

            modelBuilder.Entity<User>().HasData(new
            {
                Id = Guid.NewGuid(),
                ObjectId = Guid.Parse("b3fd282f-169a-4722-b805-ed3a1c873e74"),
                Firstname = "Administrator",
                Lastname = "Administrator",
                Email = "hrisadmin@gertnewx.onmicrosoft.com",
                Mobile = "",
                Prefix = "",
                Suffix = "",
                Middlename = "",
                StartDate = DateTime.Now.ToUniversalTime(),
                EndDate = DateTime.Now.ToUniversalTime(),
                Organization = "Ctrove",
                Status = true,
                RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f")
            });

            modelBuilder.Entity<RolesPages>().HasData
                (
                new { Id = Guid.NewGuid(), RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"), Pages = Pages.Subject, Status = true },
                new { Id = Guid.NewGuid(), RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"), Pages = Pages.Sites, Status = true },
                new { Id = Guid.NewGuid(), RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"), Pages = Pages.Config, Status = true },
                new { Id = Guid.NewGuid(), RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"), Pages = Pages.Access, Status = true },
                new { Id = Guid.NewGuid(), RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"), Pages = Pages.Roles, Status = true },
                new { Id = Guid.NewGuid(), RolesId = Guid.Parse("ec651c37-7161-4807-8538-d21cffe73b6f"), Pages = Pages.User, Status = true }
                );

            modelBuilder.Entity<VendorType>()
                 .HasData
                (
                new { Id = Guid.Parse("6ac9587d-fd98-4b36-a95f-d074079371b8"), Code = "", Name = "Commercialization Data", Status = true },
                 new { Id = Guid.Parse("d6f5fa5c-aca0-4a28-8239-691984e713e6"), Code = "", Name = "Contractor", Status = true },
                  new { Id = Guid.Parse("a0f6d3f0-0f75-49a8-880b-9448ca9a9324"), Code = "", Name = "Data Management", Status = true },
                   new { Id = Guid.Parse("9f5585d4-e81c-471c-a541-769b85bc2658"), Code = "", Name = "Data Management & Statistics", Status = true },
                   new { Id = Guid.Parse("5cece5c8-02d3-41fb-a857-268525778cf9"), Code = "", Name = "DCT services", Status = true },
                    new { Id = Guid.Parse("4aee7231-6355-4f9c-833f-7849909ad6a1"), Code = "", Name = "Full-service CRO", Status = true },
                    new { Id = Guid.Parse("f02820af-12dc-460f-9219-c7e712802a9c"), Code = "", Name = "Information Technology", Status = true },
                    new { Id = Guid.Parse("120674ed-f1a4-4fa1-9467-0d408569adca"), Code = "", Name = "Laboratory", Status = true },
                    new { Id = Guid.Parse("f5fc5212-7deb-4a8c-973a-e36e374ca1f5"), Code = "", Name = "Legal", Status = true },
                    new { Id = Guid.Parse("dee3183d-7d92-4c48-90c0-3e95e6e33b73"), Code = "", Name = "Logistics (Cold Chain, Drug Supply)", Status = true },
                    new { Id = Guid.Parse("d46f5395-a904-43c2-b090-f4fe93174eef"), Code = "", Name = "Marketing/Branding", Status = true },
                    new { Id = Guid.Parse("0bdeaa11-707a-49d8-a878-d4a29f2efbe0"), Code = "", Name = "Medical Liaison/Translation", Status = true },
                    new { Id = Guid.Parse("faad8494-4bf7-4cb0-9396-69a9c3a01ed9"), Code = "", Name = "Medical Writing", Status = true },
                    new { Id = Guid.Parse("493156c8-b3d6-4313-b9a8-47e8ae44c328"), Code = "", Name = "Meeting Planner", Status = true },
                    new { Id = Guid.Parse("793b5063-6bcf-443f-8d24-eee50e52a5d5"), Code = "", Name = "Monitoring (Clinical, Medical)", Status = true },
                    new { Id = Guid.Parse("e7be11ec-afdb-48c5-9f72-e0a4dd793c9c"), Code = "", Name = "Other", Status = true },
                    new { Id = Guid.Parse("49183e5b-6104-422d-a20d-1980499aad57"), Code = "", Name = "Patient Monitoring", Status = true },
                    new { Id = Guid.Parse("a244993a-2019-4817-b925-ec10e999fda5"), Code = "", Name = "Patient Recruitment & Engagement", Status = true },
                    new { Id = Guid.Parse("d6428db7-d0c0-4253-aa72-535bcffed481"), Code = "", Name = "Pharmacokinetics (PK) & Pharmacodynamics (PD)", Status = true },
                    new { Id = Guid.Parse("7d1e3425-4b49-4b0a-85d0-f25fd6767a27"), Code = "", Name = "Pharmacovigiliance (PVG) & Safety", Status = true },
                    new { Id = Guid.Parse("17bc6ea8-7a9b-482b-993b-8d913e548cb7"), Code = "", Name = "Project Management", Status = true },
                    new { Id = Guid.Parse("94477f9e-2d7b-481a-bc93-2735eac9df6c"), Code = "", Name = "Quality Management ", Status = true },
                    new { Id = Guid.Parse("1b5e6947-8c88-41de-846c-f1b8114d3310"), Code = "", Name = "Real world Evidence ", Status = true },
                    new { Id = Guid.Parse("630da100-d716-4afc-8898-5183b60532e8"), Code = "", Name = "Regulatory", Status = true },
                    new { Id = Guid.Parse("360d1789-cb28-4c71-b0ff-c9bc882283fb"), Code = "", Name = "Site Feasibility, Selection & Management", Status = true },
                    new { Id = Guid.Parse("0569b581-2070-469e-a732-be241e478063"), Code = "", Name = "Statistics", Status = true },
                    new { Id = Guid.Parse("e7ae8400-e63e-4801-a0b6-da6aa5617195"), Code = "", Name = "Wearables & (Remote) data collection", Status = true }
                );

            modelBuilder.Entity<ContactType>()
                .HasData
                (
                new { Id = Guid.Parse("c1652f6b-e90a-4dce-a8fc-ee3607c1fdf9"), Code = "", Name = "Academic", Status = true },
                 new { Id = Guid.Parse("9894c381-9134-4079-8b76-ed96cd114aa9"), Code = "", Name = "Central - Local Ethics Comitee", Status = true },
                  new { Id = Guid.Parse("e2f151f5-d973-433e-a32d-a372e62df683"), Code = "", Name = "Central Ethics Committee", Status = true },
                   new { Id = Guid.Parse("172b483a-39d5-400c-8fd7-337106ff08dd"), Code = "", Name = "Competent Authority", Status = true },
                    new { Id = Guid.Parse("97faa1d9-1cfa-48e4-9fb1-56a02a3d07b1"), Code = "", Name = "CRO", Status = true },
                     new { Id = Guid.Parse("d0f79d03-d115-4107-bfa7-3816f689f129"), Code = "", Name = "Investigational Research Site ", Status = true },
                      new { Id = Guid.Parse("37178517-c349-44dc-9cce-0206ae124057"), Code = "", Name = "Local Ethics Committee", Status = true },
                       new { Id = Guid.Parse("17405f81-d077-4beb-bc7d-6efd485142ce"), Code = "", Name = "Research Network ", Status = true },
                        new { Id = Guid.Parse("a589868a-b503-45ed-8391-03a45364055a"), Code = "", Name = "Solution provider", Status = true },
                         new { Id = Guid.Parse("9db6b2e5-cee5-4956-a888-9b3f1c4a7129"), Code = "", Name = "Sponsor", Status = true },
                          new { Id = Guid.Parse("34584406-8c51-4074-9bc5-1c93d16c2b61"), Code = "", Name = "Vendor", Status = true },
                            new { Id = Guid.Parse("3449e465-9c7f-48f7-9283-0e959964f439"), Code = "", Name = "Other", Status = true }
                );


            modelBuilder.Entity<Country>()
               .HasData(
                 new { Id = Guid.Parse("683a822e-79c6-4c3f-8a42-78bd9cef2244"), Code = "AFG", Name = "Afghanistan", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("c9884320-4826-4497-83cc-29dc65a4f6dc"), Code = "ALB", Name = "Albania", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("abd658aa-bd9a-41eb-8521-8b5bee5b8bbe"), Code = "DZA", Name = "Algeria", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("bdba9f2d-83d2-42eb-bfff-83293260f3f4"), Code = "AND", Name = "Andorra", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("48da5d42-a914-486d-ad31-66ad228b6d16"), Code = "AGO", Name = "Angola", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("e6779ca1-1c47-4164-863a-64aa2f998a15"), Code = "ATG", Name = "Antigua & Barbuda", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("992474d8-4f9e-4e56-bff5-0dcc7b05817c"), Code = "ARG", Name = "Argentina", Continent = "South America", Status = true },
                 new { Id = Guid.Parse("a9160ae8-4a91-402c-bbec-fc8b669c1b97"), Code = "ARM", Name = "Armenia", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("c863bd2f-5640-4282-a88e-a862adce6696"), Code = "AUS", Name = "Australia", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("d49819ae-0fd7-47d2-b4af-f0e1309bd61d"), Code = "AUT", Name = "Austria", Continent = "Europe", Status = true },

                 new { Id = Guid.Parse("33da36d6-9b75-462a-9753-c6bcc88677a0"), Code = "AZE", Name = "Azerbaijan", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("67863ec1-cb48-4945-ba16-949b7b5414a9"), Code = "BHS", Name = "Bahamas", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("bf7bab03-b479-4622-ab6f-82ca624f025f"), Code = "BHR", Name = "Bahrain", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("caf2ff09-b155-4895-beca-e2a921410ab6"), Code = "BGD", Name = "Bangladesh", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("447406e7-b94d-4077-8543-c4cf4100b12a"), Code = "BRB", Name = "Barbados", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("b6c20209-5b30-466f-afb8-2d48c7960326"), Code = "BLR", Name = "Belarus", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("2a07e5dc-c545-4c9a-b597-5eb1833caee0"), Code = "BEL", Name = "Belgium", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("fca88150-7daf-4608-bc2f-61658b0f1c39"), Code = "BLZ", Name = "Belize", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("c72e9253-2be4-4314-a640-d1fc99177801"), Code = "BEN", Name = "Benin", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("6dfcfe7d-a076-4062-b006-e8ee970fab00"), Code = "BTN", Name = "Bhutan", Continent = "Asia", Status = true },

                 new { Id = Guid.Parse("1c99665d-996a-4908-863d-af942e5a5d9a"), Code = "BOL", Name = "Bolivia", Continent = "South America", Status = true },
                 new { Id = Guid.Parse("72f1f4c2-bb93-4a44-ad44-d027f77efaa8"), Code = "BIH", Name = "Bosnia & Herzegovina", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("de70c9fd-3265-4db1-b913-f669b84895b4"), Code = "BWA", Name = "Botswana", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("af29751f-c9aa-4f0f-8623-36984da95e5f"), Code = "BRA", Name = "Brazil", Continent = "South America", Status = true },
                 new { Id = Guid.Parse("380efeb5-8e1a-4ee5-a0a5-946ddca9176f"), Code = "BRN", Name = "Brunei", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("f453ed23-6f28-4fe9-bd79-769e8bae02cc"), Code = "BGR", Name = "Bulgaria", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("455ad691-0f63-40b8-b115-da9a5a6284d3"), Code = "BFA", Name = "Burkina Faso", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("c54361be-09b0-4a31-9278-77bcce9c637a"), Code = "BDI", Name = "Burundi", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("b187fd6a-7c97-4598-900e-2fd75019c936"), Code = "CPV", Name = "Cabo Verde", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("4f8fedf2-2ece-4d81-88bf-6482428e6f10"), Code = "KHM", Name = "Cambodia", Continent = "Asia", Status = true },

                 new { Id = Guid.Parse("6cf1bd42-271b-44ad-94ee-1eda5489955c"), Code = "CMR", Name = "Cameroon", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("e31c2668-9cea-4570-a0d8-a8285646992f"), Code = "CAN", Name = "Canada", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("18bffa92-8c99-4502-8b27-f79a3846bdb1"), Code = "CAF", Name = "Central African Republic", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("7a26e0bb-cc2f-41dd-8491-87bd315a5b0b"), Code = "TCD", Name = "Chad", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("3c7b3d4d-7da8-482f-9b85-2f70e3cafa8f"), Code = "CHL", Name = "Chile", Continent = "South America", Status = true },
                 new { Id = Guid.Parse("762e5512-a7c5-4531-8142-2148059a62d9"), Code = "CHN", Name = "China", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("bcc39bb9-5c15-4306-94f9-230c190ec3fe"), Code = "COL", Name = "Colombia", Continent = "South America", Status = true },
                 new { Id = Guid.Parse("c6744a96-ca01-4c7c-8f87-076223e50ab6"), Code = "COM", Name = "Comoros", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("9a41ab1d-6fb0-46b0-a279-7e39ecc36770"), Code = "COG", Name = "Congo", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("6133f68f-6e1e-43ed-837c-f8c1acd9eccb"), Code = "CRI", Name = "Costa Rica", Continent = "North America", Status = true },

                 new { Id = Guid.Parse("340e2ca0-9360-4643-a3cd-fc31f0a343ab"), Code = "CIV", Name = "Côte d’Ivoire", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("d38b31ed-f96e-4039-bc5c-6305829045ba"), Code = "HRV", Name = "Croatia", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("c52978f9-f3ea-4750-91a8-969f58bec690"), Code = "CUB", Name = "Cuba", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("d2614b80-11ec-4bf9-82f3-cae41041075b"), Code = "CYP", Name = "Cyprus", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("f28f3776-dc02-4e47-a6ca-39458c24194a"), Code = "CZE", Name = "Czech Republic", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("b87f4e1f-0dbf-41ad-bcf3-d4a3d7757b38"), Code = "DNK", Name = "Denmark", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("bfaa08fb-7e95-4e43-b2be-482da8f7e4b3"), Code = "DJI", Name = "Djibouti", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("d481b737-39d4-4d3a-82db-4c2a872e4e88"), Code = "DMA", Name = "Dominica", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("3c1a608d-3aaa-4170-bd2c-8211afb72e4d"), Code = "DOM", Name = "Dominican Republic", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("6146d41a-5eb1-4701-b3b4-dda14943a037"), Code = "COD", Name = "DR Congo", Continent = "Africa", Status = true },

                 new { Id = Guid.Parse("dd732503-fc99-4eb3-a9a7-e28ede97b0cb"), Code = "ECU", Name = "Ecuador", Continent = "South America", Status = true },
                 new { Id = Guid.Parse("08622df7-4c8e-4e73-b98d-bb2632d2ace4"), Code = "EGY", Name = "Egypt", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("197f364f-1fc1-4a39-9c4b-be399ac15e1c"), Code = "SLV", Name = "El Salvador", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("75bc119e-37d0-4780-99ca-0eab28f21d1d"), Code = "GNQ", Name = "Equatorial Guinea", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("0243bf58-dbcf-49eb-a029-f807bcd097ee"), Code = "ERI", Name = "Eritrea", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("a5a26188-06f5-4f76-ab41-3c83c052b082"), Code = "EST", Name = "Estonia", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("5f1e1bb3-5166-4cf3-aea6-120dd2aac290"), Code = "SWZ", Name = "Eswatini", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("865ebd39-fe50-4aa7-a10d-f59b4ad8eca7"), Code = "ETH", Name = "Ethiopia", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("2214785d-4495-4539-84fa-49bc0c55f2cd"), Code = "FJI", Name = "Fiji", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("183f1dd4-e911-4a6b-a3df-c2b4da7a5728"), Code = "FIN", Name = "Finland", Continent = "Europe", Status = true },

                 new { Id = Guid.Parse("00e7968a-fd6f-4d78-b805-ec48ac1791f0"), Code = "FRA", Name = "France", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("28886fcf-87ff-4d9f-a58f-822e4783efb1"), Code = "GAB", Name = "Gabon", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("c55d072a-f199-44ff-b416-99950e025c8c"), Code = "GMB", Name = "Gambia", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("4deecde9-266f-4ad3-8894-5beb9d9d0dbd"), Code = "GEO", Name = "Georgia", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("a4e758d6-a9fd-4fe8-8ca0-0542783de0c6"), Code = "DEU", Name = "Germany", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("450048c2-5681-41a3-94c9-5385c53b21c4"), Code = "GHA", Name = "Ghana", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("9bffae9e-9c18-4863-a1ca-da1a14faee4d"), Code = "GRC", Name = "Greece", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("4b9683c7-3786-49ad-aeab-e41e2c69a492"), Code = "GRD", Name = "Grenada", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("64030f33-8c67-4507-b0b4-6b51c108e915"), Code = "GTM", Name = "Guatemala", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("cc9bc0ed-fb1d-4704-8d98-361a8e59dd8a"), Code = "GIN", Name = "Guinea", Continent = "Africa", Status = true },

                 new { Id = Guid.Parse("967bc401-e5dd-46f3-ae5f-26e62aa2531e"), Code = "GNB", Name = "Guinea-Bissau", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("f8f388d5-a3b1-4e14-b690-3468a93348c5"), Code = "GUY", Name = "Guyana", Continent = "South America", Status = true },
                 new { Id = Guid.Parse("25b0fa6d-4129-44ce-8d37-3fc8ba09cffa"), Code = "HTI", Name = "Haiti", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("e44b45d8-0301-4ca1-9003-f7a87ca2a71a"), Code = "VAT", Name = "Holy See", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("7748dba5-2b38-457f-86fe-900b3425cd12"), Code = "HND", Name = "Honduras", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("fadfb4d2-82c2-4abb-b19e-edbd745f078b"), Code = "HUN", Name = "Hungary", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("477bcd4d-d546-4487-bfd9-2686efd180aa"), Code = "ISL", Name = "Iceland", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("eec94dd5-343a-4692-a48b-42578ffb223a"), Code = "IND", Name = "India", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("70db6b87-8ef9-4a2b-9519-97aeeb49a31c"), Code = "IDN", Name = "Indonesia", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("6d3c034c-2a3c-4478-941d-f6a5970b97ef"), Code = "IRN", Name = "Iran", Continent = "Asia", Status = true },

                 new { Id = Guid.Parse("8e687f37-5dd4-46fd-95de-7de4321382be"), Code = "IRQ", Name = "Iraq", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("97697207-af31-4ae8-8864-8b09bf0bcf8c"), Code = "IRL", Name = "Ireland", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("d8ffcab7-0677-47d8-b6b0-5cccd3d2101e"), Code = "ISR", Name = "Israel", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("436189ec-d9ac-45e2-92ef-5cfbee1cc37b"), Code = "ITA", Name = "Italy", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("abd14286-3ac9-4e96-a11f-46e9c81d18c7"), Code = "JAM", Name = "Jamaica", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("15b13053-6ac3-431f-b7b6-a76798885abb"), Code = "JPN", Name = "Japan", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("94930388-4b8e-4ea3-b9f9-fad03f854fbe"), Code = "JOR", Name = "Jordan", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("bf3eeb10-0805-43f2-82f7-fb65717f7412"), Code = "KAZ", Name = "Kazakhstan", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("0ab3c6d8-ebe7-4f0c-a674-636ea9ec4f57"), Code = "KEN", Name = "Kenya", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("fdf9053b-0503-496a-b019-d79a5bccad5b"), Code = "KIR", Name = "Kiribati", Continent = "Australia", Status = true },

                 new { Id = Guid.Parse("d405756e-d91f-4c87-835d-6a7b96618696"), Code = "KWT", Name = "Kuwait", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("83c2611b-16cf-48ed-90d7-a608078f94b0"), Code = "KGZ", Name = "Kyrgyzstan", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("1b4a7fb7-21b6-4f3b-ac0b-6b34a839c4f0"), Code = "LAO", Name = "Laos", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("4c7db75f-bb66-4da1-836a-4f1abd9845a7"), Code = "LVA", Name = "Latvia", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("d9a6a371-62e9-42dd-b924-219c0386573a"), Code = "LBN", Name = "Lebanon", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("2c576019-a1c1-43d5-bd8f-257516700c87"), Code = "LSO", Name = "Lesotho", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("fb68b48b-1852-46e4-86c4-30a6d873627f"), Code = "LBR", Name = "Liberia", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("0adbdf13-1098-4694-8758-f83e1f49ef4d"), Code = "LBY", Name = "Libya", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("2381816c-c090-4411-8e86-058a2790c2de"), Code = "LIE", Name = "Liechtenstein", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("90243d26-edea-4f8b-bb08-86017d79c3a7"), Code = "LTU", Name = "Lithuania", Continent = "Europe", Status = true },

                 new { Id = Guid.Parse("ff0dcdc3-7f78-4d6b-b034-7a00fc74e79e"), Code = "LUX", Name = "Luxembourg", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("2afe7d35-e8e3-4290-aec4-74f14be8a7a4"), Code = "MDG", Name = "Madagascar", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("731d7006-aee1-4661-8b52-e1b46fff926f"), Code = "MWI", Name = "Malawi", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("7af37c62-33b9-4932-a7a5-11c6a2c91feb"), Code = "MYS", Name = "Malaysia", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("7b5be21c-e6ec-4ea8-9059-b5e6fe94e577"), Code = "MDV", Name = "Maldives", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("3810f316-a30c-44d2-9a4a-9c8a6ebb16cc"), Code = "MLI", Name = "Mali", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("da3e3367-1d4c-438c-97b6-4f1bd3b6277b"), Code = "MLT", Name = "Malta", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("b9467f11-3a64-463c-a9c5-2211bd444cab"), Code = "MHL", Name = "Marshall Islands", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("f7f740a2-0c9c-4eb0-ac13-c38cee9b5e04"), Code = "MRT", Name = "Mauritania", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("8596519e-32d7-4344-919b-943205dd1f2a"), Code = "MUS", Name = "Mauritius", Continent = "Africa", Status = true },

                 new { Id = Guid.Parse("c896c1ec-6a97-433f-9d4e-87301d4e919e"), Code = "MEX", Name = "Mexico", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("614cb2ea-e5ae-4094-a95f-96bc09795dc0"), Code = "FSM", Name = "Micronesia", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("4f2228db-0531-45da-99c2-0b40b3d4f0d9"), Code = "MDA", Name = "Moldova", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("b8112541-cd5d-4d93-af63-74c2e00e8815"), Code = "MCO", Name = "Monaco", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("fb33ebcd-ebcb-4384-a02c-e12ea13365af"), Code = "MNG", Name = "Mongolia", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("a873c39d-a96e-4f39-941c-149b7b343774"), Code = "MNE", Name = "Montenegro", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("ff6ac4d4-4a7e-4c4c-8566-9368d54cf127"), Code = "MAR", Name = "Morocco", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("81930246-368f-40da-8db4-8fed1b53ba4d"), Code = "MOZ", Name = "Mozambique", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("9cd69b94-e87b-4438-97f1-7c7a005f73fd"), Code = "MMR", Name = "Myanmar", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("f78c634a-bc83-40fc-a83e-11948c3e92f8"), Code = "NAM", Name = "Namibia", Continent = "Africa", Status = true },

                 new { Id = Guid.Parse("e7596855-3b80-4e9f-a101-c7b1195de61d"), Code = "NRU", Name = "Nauru", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("653666c1-1a92-412d-9011-699820257c79"), Code = "NPL", Name = "Nepal", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("39d3a47f-fac1-459c-92af-5b2ab4059cbb"), Code = "NLD", Name = "Netherlands", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("c03779eb-2c9a-4ae0-9002-97e5246cc464"), Code = "NZL", Name = "New Zealand", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("bbee0acf-175c-42a5-b054-fb3127a7b393"), Code = "NIC", Name = "Nicaragua", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("55552df6-3620-4af2-8926-d7203c7eff9d"), Code = "NER", Name = "Niger", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("aab1087a-08d5-43ba-9756-ad9f1e1e3eec"), Code = "NGA", Name = "Nigeria", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("1b51a8dd-93e2-43cb-ba6a-012859ffd7ab"), Code = "PRK", Name = "North Korea", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("58aec110-796b-4e44-bfa7-795a0a7ece3d"), Code = "MKD", Name = "North Macedonia", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("a0ba1ade-d0fc-4a1f-bb65-95542884ac3d"), Code = "NOR", Name = "Norway", Continent = "Europe", Status = true },

                 new { Id = Guid.Parse("0cfdda22-884d-43b4-b02f-8608e11d2545"), Code = "OMN", Name = "Oman", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("e0ff8e3e-5b36-4c5c-a5ab-638a7998716e"), Code = "PAK", Name = "Pakistan", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("af20aa28-5466-48c9-9a0a-53f43c7bc4a5"), Code = "PLW", Name = "Palau", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("77629003-ce75-4fb0-a329-7ec0ed604e50"), Code = "PAN", Name = "Panama", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("2a9289ed-2de6-4b88-b0fc-1842e23e7af8"), Code = "PNG", Name = "Papua New Guinea", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("a074d743-1061-4885-b81f-9a25cb1c5fbe"), Code = "PRY", Name = "Paraguay", Continent = "South America", Status = true },
                 new { Id = Guid.Parse("1fcf4541-c259-481c-aee2-c8b3f60d1e15"), Code = "PER", Name = "Peru", Continent = "South America", Status = true },
                 new { Id = Guid.Parse("2ff99895-b688-442f-a1e6-c923c126efa2"), Code = "PHL", Name = "Philippines", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("0d58286a-eb71-463d-aa5b-1e854d8aab1b"), Code = "POL", Name = "Poland", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("f0d567ce-de48-4212-ae7e-962aa8cae363"), Code = "PRT", Name = "Portugal", Continent = "Europe", Status = true },

                 new { Id = Guid.Parse("f6c3c7d1-edb3-41ea-93df-d6007f2f945c"), Code = "QAT", Name = "Qatar", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("103906d9-879b-4f34-bf6a-0b284e65e128"), Code = "ROM", Name = "Romania", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("b6e199d3-50e8-416b-b11c-fe9ce920b72b"), Code = "RUS", Name = "Russia", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("5f1eb7ec-6de9-4785-8a8f-21c5942c87c3"), Code = "RWA", Name = "Rwanda", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("087c54a1-a167-4efe-b90a-4bf0a0eb98ff"), Code = "KNA", Name = "Saint Kitts & Nevis", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("cf8fcb6f-a4c9-4009-9853-fb8c0e253ba1"), Code = "LCA", Name = "Saint Lucia", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("0a9797fd-c9cb-4b22-8202-15e0ad57c817"), Code = "WSM", Name = "Samoa", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("f13c200c-6a22-4678-8ccb-dbf908ef3ea4"), Code = "SMR", Name = "San Marino", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("a94a82b6-d6e9-43b2-ad2e-d7d1297f692b"), Code = "STP", Name = "Sao Tome & Principe", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("ae394d09-af98-427f-ad8a-466ad7e79ca9"), Code = "SAU", Name = "Saudi Arabia", Continent = "Asia", Status = true },

                 new { Id = Guid.Parse("06bcd468-6a44-4f78-b489-d7d49b36b728"), Code = "SEN", Name = "Senegal", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("74fa7f74-0134-4f91-aaef-d6d10dc46768"), Code = "SRB", Name = "Serbia", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("30d8b4bc-2c40-408a-b4a4-f4c8073df206"), Code = "SYC", Name = "Seychelles", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("3268b571-e792-41bd-88ee-315a6720a44e"), Code = "SLE", Name = "Sierra Leone", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("d9df5027-b862-4971-8b12-4170c1d6d8fd"), Code = "SGP", Name = "Singapore", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("4d39c9d2-1d84-40ff-b331-82490b60811c"), Code = "SVK", Name = "Slovakia", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("31a720e8-f349-4caa-8fcc-8909680071f8"), Code = "SVN", Name = "Slovenia", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("f28eb399-447b-4b33-840b-18419958ebea"), Code = "SLB", Name = "Solomon Islands", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("6d42924d-e76c-4909-8c06-99c460c5b968"), Code = "SOM", Name = "Somalia", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("826c5a15-d74c-455c-9b81-ca1f5bebf548"), Code = "ZAF", Name = "South Africa", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("6a64a628-ea3c-432e-859e-eb6af30411c3"), Code = "KOR", Name = "South Korea", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("0dd85f94-20d7-45b5-b3d5-28d0d2914bbe"), Code = "SSD", Name = "South Sudan", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("934d44d5-2596-4471-83cc-337ea9ca40cb"), Code = "ESP", Name = "Spain", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("f83a6c05-5000-445a-b03d-2af7f47b9f2e"), Code = "LKA", Name = "Sri Lanka", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("8a605925-5c47-413a-be19-36149a480262"), Code = "VCT", Name = "St. Vincent & Grenadines", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("dfe9e0c5-abd1-4b2e-98d8-92c4ad32663a"), Code = "PSE", Name = "State of Palestine", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("b4508c9b-020c-4b73-87fa-ed472c3c7cfc"), Code = "SDN", Name = "Sudan", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("0fa4de3e-2f05-4cbb-b5d8-4e5b1cc1e19d"), Code = "SWE", Name = "Sweden", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("1fbb4b7d-2540-483e-84cf-7ceec9d1d9ab"), Code = "CHE", Name = "Switzerland", Continent = "Europe", Status = true },

                 new { Id = Guid.Parse("60590da0-2bef-45d2-a6a5-f4500be313ec"), Code = "SYR", Name = "Syria", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("00673d07-d045-4568-8a1c-05f40051614c"), Code = "TJK", Name = "Tajikistan", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("4d9554b4-de1c-470f-89b9-1d0289bfcf83"), Code = "TZA", Name = "Tanzania", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("1fd51cc4-e9a0-4aaa-9926-2c34637fbc6a"), Code = "THA", Name = "Thailand", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("ea5c1893-8cd4-4092-b029-390829ff7806"), Code = "TLS", Name = "Timor-Leste", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("a08544cb-54cc-4f37-91f5-90f571916845"), Code = "TGO", Name = "Togo", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("eba0b617-7669-4072-a132-ee508ecb8be9"), Code = "TON", Name = "Tonga", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("9b01ba74-9f63-45ef-92a5-8d5b026e0af5"), Code = "TTO", Name = "Trinidad & Tobago", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("1f216737-0637-48ae-a00b-bded0f193ffb"), Code = "TUN", Name = "Tunisia", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("fad613a4-2a7f-4b46-8bb1-92df59e2a457"), Code = "TUR", Name = "Turkey", Continent = "Asia", Status = true },

                 new { Id = Guid.Parse("cf65be93-a637-4f3b-83d7-df1e00fe8bd7"), Code = "TKM", Name = "Turkmenistan", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("9f8d2758-cf9c-4385-8cb2-b2d181c52592"), Code = "TUV", Name = "Tuvalu", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("b460795d-7953-4195-a3e6-9bdcbbbfa70e"), Code = "UGA", Name = "Uganda", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("810cd8c7-5456-4b9b-8945-0aa0e8c5f853"), Code = "UKR", Name = "Ukraine", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("baf98adb-c89d-47e6-9714-4a8867e24e15"), Code = "ARE", Name = "United Arab Emirates", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("8d4ad696-ff64-4531-b548-8b51829168cd"), Code = "GBR", Name = "United Kingdom", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("ceb64541-31e4-46da-ba19-e24b268d7fe7"), Code = "USA", Name = "United States Of America", Continent = "North America", Status = true },
                 new { Id = Guid.Parse("ac942fe3-f0d7-4d4a-a107-3f6a807c7c58"), Code = "URY", Name = "Uruguay", Continent = "South America", Status = true },
                 new { Id = Guid.Parse("095a307b-5cf0-4d3a-89a7-df7cbaec7dc6"), Code = "UZB", Name = "Uzbekistan", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("2954e603-8ea5-4937-9ebe-335ac9324883"), Code = "VUT", Name = "Vanuatu", Continent = "Australia", Status = true },

                 new { Id = Guid.Parse("7a641706-67c2-49f7-a62e-b03ed3be48f3"), Code = "VEN", Name = "Venezuela", Continent = "Asia", Status = true },
                 new { Id = Guid.Parse("f458903e-f5fd-4657-be19-e4b66e1b9005"), Code = "VNM", Name = "Vietnam", Continent = "Australia", Status = true },
                 new { Id = Guid.Parse("429c355c-a3cd-4ea2-9a85-f6529c4c92fb"), Code = "YEM", Name = "Yemen", Continent = "Africa", Status = true },
                 new { Id = Guid.Parse("8350d558-3c81-4b32-8037-ba5b55dd778e"), Code = "ZMB", Name = "Zambia", Continent = "Europe", Status = true },
                 new { Id = Guid.Parse("3a15962f-35c5-4752-b504-b0a9527ebe00"), Code = "ZWE", Name = "Zimbabwe", Continent = "Asia", Status = true }
               );
        }

    }
}
