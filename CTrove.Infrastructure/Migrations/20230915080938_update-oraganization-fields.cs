using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CTrove.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateoraganizationfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuditType = table.Column<int>(type: "int", nullable: false),
                    PerformedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatePerformed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Continent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ethnicity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ethnicity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrevPhase = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Blinded = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyCountry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyCountry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TherapeuticArea",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapeuticArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitType = table.Column<int>(type: "int", nullable: false),
                    TargetDays = table.Column<int>(type: "int", nullable: false),
                    TimeWindow = table.Column<int>(type: "int", nullable: false),
                    VisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    isRequired = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolesPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolesPages_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Middlename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Suffix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Landline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyCountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SiteStatus = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sites_ServiceType_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sites_StudyCountry_StudyCountryId",
                        column: x => x.StudyCountryId,
                        principalTable: "StudyCountry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Study",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TherapeuticAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillingCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sponsor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiKeyToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Study", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Study_Classification_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classification",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Study_TherapeuticArea_TherapeuticAreaId",
                        column: x => x.TherapeuticAreaId,
                        principalTable: "TherapeuticArea",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parent = table.Column<int>(type: "int", nullable: false),
                    ContactTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendorTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SecondaryContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organization_ContactType_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Organization_VendorType_VendorTypeId",
                        column: x => x.VendorTypeId,
                        principalTable: "VendorType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Access",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rights = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Access", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Access_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SitePhases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SitesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SitePhases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SitePhases_Phase_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SitePhases_Sites_SitesId",
                        column: x => x.SitesId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScreeningNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SitesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YearOfBirth = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    EthnicityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectStatus = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subject_Ethnicity_EthnicityId",
                        column: x => x.EthnicityId,
                        principalTable: "Ethnicity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subject_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subject_Sites_SitesId",
                        column: x => x.SitesId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contributor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryJobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryJobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicData = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Consent = table.Column<bool>(type: "bit", nullable: false),
                    DateOfConsent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributor_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contributor_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectPhases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectPhases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectPhases_Phase_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectPhases_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContributorStudy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContributorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SponsorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorStudy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContributorStudy_Contributor_ContributorId",
                        column: x => x.ContributorId,
                        principalTable: "Contributor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContactType",
                columns: new[] { "Id", "Code", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("172b483a-39d5-400c-8fd7-337106ff08dd"), "", "Competent Authority", true },
                    { new Guid("17405f81-d077-4beb-bc7d-6efd485142ce"), "", "Research Network ", true },
                    { new Guid("3449e465-9c7f-48f7-9283-0e959964f439"), "", "Other", true },
                    { new Guid("34584406-8c51-4074-9bc5-1c93d16c2b61"), "", "Vendor", true },
                    { new Guid("37178517-c349-44dc-9cce-0206ae124057"), "", "Local Ethics Committee", true },
                    { new Guid("97faa1d9-1cfa-48e4-9fb1-56a02a3d07b1"), "", "CRO", true },
                    { new Guid("9894c381-9134-4079-8b76-ed96cd114aa9"), "", "Central - Local Ethics Comitee", true },
                    { new Guid("9db6b2e5-cee5-4956-a888-9b3f1c4a7129"), "", "Sponsor", true },
                    { new Guid("a589868a-b503-45ed-8391-03a45364055a"), "", "Solution provider", true },
                    { new Guid("c1652f6b-e90a-4dce-a8fc-ee3607c1fdf9"), "", "Academic", true },
                    { new Guid("d0f79d03-d115-4107-bfa7-3816f689f129"), "", "Investigational Research Site ", true },
                    { new Guid("e2f151f5-d973-433e-a32d-a372e62df683"), "", "Central Ethics Committee", true }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Code", "Continent", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("00673d07-d045-4568-8a1c-05f40051614c"), "TJK", "Asia", "Tajikistan", true },
                    { new Guid("00e7968a-fd6f-4d78-b805-ec48ac1791f0"), "FRA", "Europe", "France", true },
                    { new Guid("0243bf58-dbcf-49eb-a029-f807bcd097ee"), "ERI", "Africa", "Eritrea", true },
                    { new Guid("06bcd468-6a44-4f78-b489-d7d49b36b728"), "SEN", "Africa", "Senegal", true },
                    { new Guid("08622df7-4c8e-4e73-b98d-bb2632d2ace4"), "EGY", "Africa", "Egypt", true },
                    { new Guid("087c54a1-a167-4efe-b90a-4bf0a0eb98ff"), "KNA", "North America", "Saint Kitts & Nevis", true },
                    { new Guid("095a307b-5cf0-4d3a-89a7-df7cbaec7dc6"), "UZB", "Asia", "Uzbekistan", true },
                    { new Guid("0a9797fd-c9cb-4b22-8202-15e0ad57c817"), "WSM", "Australia", "Samoa", true },
                    { new Guid("0ab3c6d8-ebe7-4f0c-a674-636ea9ec4f57"), "KEN", "Africa", "Kenya", true },
                    { new Guid("0adbdf13-1098-4694-8758-f83e1f49ef4d"), "LBY", "Africa", "Libya", true },
                    { new Guid("0cfdda22-884d-43b4-b02f-8608e11d2545"), "OMN", "Asia", "Oman", true },
                    { new Guid("0d58286a-eb71-463d-aa5b-1e854d8aab1b"), "POL", "Europe", "Poland", true },
                    { new Guid("0dd85f94-20d7-45b5-b3d5-28d0d2914bbe"), "SSD", "Africa", "South Sudan", true },
                    { new Guid("0fa4de3e-2f05-4cbb-b5d8-4e5b1cc1e19d"), "SWE", "Europe", "Sweden", true },
                    { new Guid("103906d9-879b-4f34-bf6a-0b284e65e128"), "ROM", "Europe", "Romania", true },
                    { new Guid("15b13053-6ac3-431f-b7b6-a76798885abb"), "JPN", "Asia", "Japan", true },
                    { new Guid("183f1dd4-e911-4a6b-a3df-c2b4da7a5728"), "FIN", "Europe", "Finland", true },
                    { new Guid("18bffa92-8c99-4502-8b27-f79a3846bdb1"), "CAF", "Africa", "Central African Republic", true },
                    { new Guid("197f364f-1fc1-4a39-9c4b-be399ac15e1c"), "SLV", "North America", "El Salvador", true },
                    { new Guid("1b4a7fb7-21b6-4f3b-ac0b-6b34a839c4f0"), "LAO", "Asia", "Laos", true },
                    { new Guid("1b51a8dd-93e2-43cb-ba6a-012859ffd7ab"), "PRK", "Asia", "North Korea", true },
                    { new Guid("1c99665d-996a-4908-863d-af942e5a5d9a"), "BOL", "South America", "Bolivia", true },
                    { new Guid("1f216737-0637-48ae-a00b-bded0f193ffb"), "TUN", "Africa", "Tunisia", true },
                    { new Guid("1fbb4b7d-2540-483e-84cf-7ceec9d1d9ab"), "CHE", "Europe", "Switzerland", true },
                    { new Guid("1fcf4541-c259-481c-aee2-c8b3f60d1e15"), "PER", "South America", "Peru", true },
                    { new Guid("1fd51cc4-e9a0-4aaa-9926-2c34637fbc6a"), "THA", "Asia", "Thailand", true },
                    { new Guid("2214785d-4495-4539-84fa-49bc0c55f2cd"), "FJI", "Australia", "Fiji", true },
                    { new Guid("2381816c-c090-4411-8e86-058a2790c2de"), "LIE", "Europe", "Liechtenstein", true },
                    { new Guid("25b0fa6d-4129-44ce-8d37-3fc8ba09cffa"), "HTI", "North America", "Haiti", true },
                    { new Guid("28886fcf-87ff-4d9f-a58f-822e4783efb1"), "GAB", "Africa", "Gabon", true },
                    { new Guid("2954e603-8ea5-4937-9ebe-335ac9324883"), "VUT", "Australia", "Vanuatu", true },
                    { new Guid("2a07e5dc-c545-4c9a-b597-5eb1833caee0"), "BEL", "Europe", "Belgium", true },
                    { new Guid("2a9289ed-2de6-4b88-b0fc-1842e23e7af8"), "PNG", "Australia", "Papua New Guinea", true },
                    { new Guid("2afe7d35-e8e3-4290-aec4-74f14be8a7a4"), "MDG", "Africa", "Madagascar", true },
                    { new Guid("2c576019-a1c1-43d5-bd8f-257516700c87"), "LSO", "Africa", "Lesotho", true },
                    { new Guid("2ff99895-b688-442f-a1e6-c923c126efa2"), "PHL", "Asia", "Philippines", true },
                    { new Guid("30d8b4bc-2c40-408a-b4a4-f4c8073df206"), "SYC", "Africa", "Seychelles", true },
                    { new Guid("31a720e8-f349-4caa-8fcc-8909680071f8"), "SVN", "Europe", "Slovenia", true },
                    { new Guid("3268b571-e792-41bd-88ee-315a6720a44e"), "SLE", "Africa", "Sierra Leone", true },
                    { new Guid("33da36d6-9b75-462a-9753-c6bcc88677a0"), "AZE", "Asia", "Azerbaijan", true },
                    { new Guid("340e2ca0-9360-4643-a3cd-fc31f0a343ab"), "CIV", "Africa", "Côte d’Ivoire", true },
                    { new Guid("380efeb5-8e1a-4ee5-a0a5-946ddca9176f"), "BRN", "Asia", "Brunei", true },
                    { new Guid("3810f316-a30c-44d2-9a4a-9c8a6ebb16cc"), "MLI", "Africa", "Mali", true },
                    { new Guid("39d3a47f-fac1-459c-92af-5b2ab4059cbb"), "NLD", "Europe", "Netherlands", true },
                    { new Guid("3a15962f-35c5-4752-b504-b0a9527ebe00"), "ZWE", "Asia", "Zimbabwe", true },
                    { new Guid("3c1a608d-3aaa-4170-bd2c-8211afb72e4d"), "DOM", "North America", "Dominican Republic", true },
                    { new Guid("3c7b3d4d-7da8-482f-9b85-2f70e3cafa8f"), "CHL", "South America", "Chile", true },
                    { new Guid("429c355c-a3cd-4ea2-9a85-f6529c4c92fb"), "YEM", "Africa", "Yemen", true },
                    { new Guid("436189ec-d9ac-45e2-92ef-5cfbee1cc37b"), "ITA", "Europe", "Italy", true },
                    { new Guid("447406e7-b94d-4077-8543-c4cf4100b12a"), "BRB", "North America", "Barbados", true },
                    { new Guid("450048c2-5681-41a3-94c9-5385c53b21c4"), "GHA", "Africa", "Ghana", true },
                    { new Guid("455ad691-0f63-40b8-b115-da9a5a6284d3"), "BFA", "Africa", "Burkina Faso", true },
                    { new Guid("477bcd4d-d546-4487-bfd9-2686efd180aa"), "ISL", "Europe", "Iceland", true },
                    { new Guid("48da5d42-a914-486d-ad31-66ad228b6d16"), "AGO", "Africa", "Angola", true },
                    { new Guid("4b9683c7-3786-49ad-aeab-e41e2c69a492"), "GRD", "North America", "Grenada", true },
                    { new Guid("4c7db75f-bb66-4da1-836a-4f1abd9845a7"), "LVA", "Europe", "Latvia", true },
                    { new Guid("4d39c9d2-1d84-40ff-b331-82490b60811c"), "SVK", "Europe", "Slovakia", true },
                    { new Guid("4d9554b4-de1c-470f-89b9-1d0289bfcf83"), "TZA", "Africa", "Tanzania", true },
                    { new Guid("4deecde9-266f-4ad3-8894-5beb9d9d0dbd"), "GEO", "Asia", "Georgia", true },
                    { new Guid("4f2228db-0531-45da-99c2-0b40b3d4f0d9"), "MDA", "Europe", "Moldova", true },
                    { new Guid("4f8fedf2-2ece-4d81-88bf-6482428e6f10"), "KHM", "Asia", "Cambodia", true },
                    { new Guid("55552df6-3620-4af2-8926-d7203c7eff9d"), "NER", "Africa", "Niger", true },
                    { new Guid("58aec110-796b-4e44-bfa7-795a0a7ece3d"), "MKD", "Europe", "North Macedonia", true },
                    { new Guid("5f1e1bb3-5166-4cf3-aea6-120dd2aac290"), "SWZ", "Africa", "Eswatini", true },
                    { new Guid("5f1eb7ec-6de9-4785-8a8f-21c5942c87c3"), "RWA", "Africa", "Rwanda", true },
                    { new Guid("60590da0-2bef-45d2-a6a5-f4500be313ec"), "SYR", "Asia", "Syria", true },
                    { new Guid("6133f68f-6e1e-43ed-837c-f8c1acd9eccb"), "CRI", "North America", "Costa Rica", true },
                    { new Guid("6146d41a-5eb1-4701-b3b4-dda14943a037"), "COD", "Africa", "DR Congo", true },
                    { new Guid("614cb2ea-e5ae-4094-a95f-96bc09795dc0"), "FSM", "Australia", "Micronesia", true },
                    { new Guid("64030f33-8c67-4507-b0b4-6b51c108e915"), "GTM", "North America", "Guatemala", true },
                    { new Guid("653666c1-1a92-412d-9011-699820257c79"), "NPL", "Asia", "Nepal", true },
                    { new Guid("67863ec1-cb48-4945-ba16-949b7b5414a9"), "BHS", "North America", "Bahamas", true },
                    { new Guid("683a822e-79c6-4c3f-8a42-78bd9cef2244"), "AFG", "Asia", "Afghanistan", true },
                    { new Guid("6a64a628-ea3c-432e-859e-eb6af30411c3"), "KOR", "Asia", "South Korea", true },
                    { new Guid("6cf1bd42-271b-44ad-94ee-1eda5489955c"), "CMR", "Africa", "Cameroon", true },
                    { new Guid("6d3c034c-2a3c-4478-941d-f6a5970b97ef"), "IRN", "Asia", "Iran", true },
                    { new Guid("6d42924d-e76c-4909-8c06-99c460c5b968"), "SOM", "Africa", "Somalia", true },
                    { new Guid("6dfcfe7d-a076-4062-b006-e8ee970fab00"), "BTN", "Asia", "Bhutan", true },
                    { new Guid("70db6b87-8ef9-4a2b-9519-97aeeb49a31c"), "IDN", "Asia", "Indonesia", true },
                    { new Guid("72f1f4c2-bb93-4a44-ad44-d027f77efaa8"), "BIH", "Europe", "Bosnia & Herzegovina", true },
                    { new Guid("731d7006-aee1-4661-8b52-e1b46fff926f"), "MWI", "Africa", "Malawi", true },
                    { new Guid("74fa7f74-0134-4f91-aaef-d6d10dc46768"), "SRB", "Europe", "Serbia", true },
                    { new Guid("75bc119e-37d0-4780-99ca-0eab28f21d1d"), "GNQ", "Africa", "Equatorial Guinea", true },
                    { new Guid("762e5512-a7c5-4531-8142-2148059a62d9"), "CHN", "Asia", "China", true },
                    { new Guid("7748dba5-2b38-457f-86fe-900b3425cd12"), "HND", "North America", "Honduras", true },
                    { new Guid("77629003-ce75-4fb0-a329-7ec0ed604e50"), "PAN", "North America", "Panama", true },
                    { new Guid("7a26e0bb-cc2f-41dd-8491-87bd315a5b0b"), "TCD", "Africa", "Chad", true },
                    { new Guid("7a641706-67c2-49f7-a62e-b03ed3be48f3"), "VEN", "Asia", "Venezuela", true },
                    { new Guid("7af37c62-33b9-4932-a7a5-11c6a2c91feb"), "MYS", "Asia", "Malaysia", true },
                    { new Guid("7b5be21c-e6ec-4ea8-9059-b5e6fe94e577"), "MDV", "Asia", "Maldives", true },
                    { new Guid("810cd8c7-5456-4b9b-8945-0aa0e8c5f853"), "UKR", "Europe", "Ukraine", true },
                    { new Guid("81930246-368f-40da-8db4-8fed1b53ba4d"), "MOZ", "Africa", "Mozambique", true },
                    { new Guid("826c5a15-d74c-455c-9b81-ca1f5bebf548"), "ZAF", "Africa", "South Africa", true },
                    { new Guid("8350d558-3c81-4b32-8037-ba5b55dd778e"), "ZMB", "Europe", "Zambia", true },
                    { new Guid("83c2611b-16cf-48ed-90d7-a608078f94b0"), "KGZ", "Asia", "Kyrgyzstan", true },
                    { new Guid("8596519e-32d7-4344-919b-943205dd1f2a"), "MUS", "Africa", "Mauritius", true },
                    { new Guid("865ebd39-fe50-4aa7-a10d-f59b4ad8eca7"), "ETH", "Africa", "Ethiopia", true },
                    { new Guid("8a605925-5c47-413a-be19-36149a480262"), "VCT", "North America", "St. Vincent & Grenadines", true },
                    { new Guid("8d4ad696-ff64-4531-b548-8b51829168cd"), "GBR", "Europe", "United Kingdom", true },
                    { new Guid("8e687f37-5dd4-46fd-95de-7de4321382be"), "IRQ", "Asia", "Iraq", true },
                    { new Guid("90243d26-edea-4f8b-bb08-86017d79c3a7"), "LTU", "Europe", "Lithuania", true },
                    { new Guid("934d44d5-2596-4471-83cc-337ea9ca40cb"), "ESP", "Europe", "Spain", true },
                    { new Guid("94930388-4b8e-4ea3-b9f9-fad03f854fbe"), "JOR", "Asia", "Jordan", true },
                    { new Guid("967bc401-e5dd-46f3-ae5f-26e62aa2531e"), "GNB", "Africa", "Guinea-Bissau", true },
                    { new Guid("97697207-af31-4ae8-8864-8b09bf0bcf8c"), "IRL", "Europe", "Ireland", true },
                    { new Guid("992474d8-4f9e-4e56-bff5-0dcc7b05817c"), "ARG", "South America", "Argentina", true },
                    { new Guid("9a41ab1d-6fb0-46b0-a279-7e39ecc36770"), "COG", "Africa", "Congo", true },
                    { new Guid("9b01ba74-9f63-45ef-92a5-8d5b026e0af5"), "TTO", "North America", "Trinidad & Tobago", true },
                    { new Guid("9bffae9e-9c18-4863-a1ca-da1a14faee4d"), "GRC", "Europe", "Greece", true },
                    { new Guid("9cd69b94-e87b-4438-97f1-7c7a005f73fd"), "MMR", "Asia", "Myanmar", true },
                    { new Guid("9f8d2758-cf9c-4385-8cb2-b2d181c52592"), "TUV", "Australia", "Tuvalu", true },
                    { new Guid("a074d743-1061-4885-b81f-9a25cb1c5fbe"), "PRY", "South America", "Paraguay", true },
                    { new Guid("a08544cb-54cc-4f37-91f5-90f571916845"), "TGO", "Africa", "Togo", true },
                    { new Guid("a0ba1ade-d0fc-4a1f-bb65-95542884ac3d"), "NOR", "Europe", "Norway", true },
                    { new Guid("a4e758d6-a9fd-4fe8-8ca0-0542783de0c6"), "DEU", "Europe", "Germany", true },
                    { new Guid("a5a26188-06f5-4f76-ab41-3c83c052b082"), "EST", "Europe", "Estonia", true },
                    { new Guid("a873c39d-a96e-4f39-941c-149b7b343774"), "MNE", "Europe", "Montenegro", true },
                    { new Guid("a9160ae8-4a91-402c-bbec-fc8b669c1b97"), "ARM", "Asia", "Armenia", true },
                    { new Guid("a94a82b6-d6e9-43b2-ad2e-d7d1297f692b"), "STP", "Africa", "Sao Tome & Principe", true },
                    { new Guid("aab1087a-08d5-43ba-9756-ad9f1e1e3eec"), "NGA", "Africa", "Nigeria", true },
                    { new Guid("abd14286-3ac9-4e96-a11f-46e9c81d18c7"), "JAM", "North America", "Jamaica", true },
                    { new Guid("abd658aa-bd9a-41eb-8521-8b5bee5b8bbe"), "DZA", "Africa", "Algeria", true },
                    { new Guid("ac942fe3-f0d7-4d4a-a107-3f6a807c7c58"), "URY", "South America", "Uruguay", true },
                    { new Guid("ae394d09-af98-427f-ad8a-466ad7e79ca9"), "SAU", "Asia", "Saudi Arabia", true },
                    { new Guid("af20aa28-5466-48c9-9a0a-53f43c7bc4a5"), "PLW", "Australia", "Palau", true },
                    { new Guid("af29751f-c9aa-4f0f-8623-36984da95e5f"), "BRA", "South America", "Brazil", true },
                    { new Guid("b187fd6a-7c97-4598-900e-2fd75019c936"), "CPV", "Africa", "Cabo Verde", true },
                    { new Guid("b4508c9b-020c-4b73-87fa-ed472c3c7cfc"), "SDN", "Africa", "Sudan", true },
                    { new Guid("b460795d-7953-4195-a3e6-9bdcbbbfa70e"), "UGA", "Africa", "Uganda", true },
                    { new Guid("b6c20209-5b30-466f-afb8-2d48c7960326"), "BLR", "Europe", "Belarus", true },
                    { new Guid("b6e199d3-50e8-416b-b11c-fe9ce920b72b"), "RUS", "Europe", "Russia", true },
                    { new Guid("b8112541-cd5d-4d93-af63-74c2e00e8815"), "MCO", "Europe", "Monaco", true },
                    { new Guid("b87f4e1f-0dbf-41ad-bcf3-d4a3d7757b38"), "DNK", "Europe", "Denmark", true },
                    { new Guid("b9467f11-3a64-463c-a9c5-2211bd444cab"), "MHL", "Australia", "Marshall Islands", true },
                    { new Guid("baf98adb-c89d-47e6-9714-4a8867e24e15"), "ARE", "Asia", "United Arab Emirates", true },
                    { new Guid("bbee0acf-175c-42a5-b054-fb3127a7b393"), "NIC", "North America", "Nicaragua", true },
                    { new Guid("bcc39bb9-5c15-4306-94f9-230c190ec3fe"), "COL", "South America", "Colombia", true },
                    { new Guid("bdba9f2d-83d2-42eb-bfff-83293260f3f4"), "AND", "Europe", "Andorra", true },
                    { new Guid("bf3eeb10-0805-43f2-82f7-fb65717f7412"), "KAZ", "Asia", "Kazakhstan", true },
                    { new Guid("bf7bab03-b479-4622-ab6f-82ca624f025f"), "BHR", "Asia", "Bahrain", true },
                    { new Guid("bfaa08fb-7e95-4e43-b2be-482da8f7e4b3"), "DJI", "Africa", "Djibouti", true },
                    { new Guid("c03779eb-2c9a-4ae0-9002-97e5246cc464"), "NZL", "Australia", "New Zealand", true },
                    { new Guid("c52978f9-f3ea-4750-91a8-969f58bec690"), "CUB", "North America", "Cuba", true },
                    { new Guid("c54361be-09b0-4a31-9278-77bcce9c637a"), "BDI", "Africa", "Burundi", true },
                    { new Guid("c55d072a-f199-44ff-b416-99950e025c8c"), "GMB", "Africa", "Gambia", true },
                    { new Guid("c6744a96-ca01-4c7c-8f87-076223e50ab6"), "COM", "Africa", "Comoros", true },
                    { new Guid("c72e9253-2be4-4314-a640-d1fc99177801"), "BEN", "Africa", "Benin", true },
                    { new Guid("c863bd2f-5640-4282-a88e-a862adce6696"), "AUS", "Australia", "Australia", true },
                    { new Guid("c896c1ec-6a97-433f-9d4e-87301d4e919e"), "MEX", "North America", "Mexico", true },
                    { new Guid("c9884320-4826-4497-83cc-29dc65a4f6dc"), "ALB", "Europe", "Albania", true },
                    { new Guid("caf2ff09-b155-4895-beca-e2a921410ab6"), "BGD", "Asia", "Bangladesh", true },
                    { new Guid("cc9bc0ed-fb1d-4704-8d98-361a8e59dd8a"), "GIN", "Africa", "Guinea", true },
                    { new Guid("ceb64541-31e4-46da-ba19-e24b268d7fe7"), "USA", "North America", "United States Of America", true },
                    { new Guid("cf65be93-a637-4f3b-83d7-df1e00fe8bd7"), "TKM", "Asia", "Turkmenistan", true },
                    { new Guid("cf8fcb6f-a4c9-4009-9853-fb8c0e253ba1"), "LCA", "North America", "Saint Lucia", true },
                    { new Guid("d2614b80-11ec-4bf9-82f3-cae41041075b"), "CYP", "Asia", "Cyprus", true },
                    { new Guid("d38b31ed-f96e-4039-bc5c-6305829045ba"), "HRV", "Europe", "Croatia", true },
                    { new Guid("d405756e-d91f-4c87-835d-6a7b96618696"), "KWT", "Asia", "Kuwait", true },
                    { new Guid("d481b737-39d4-4d3a-82db-4c2a872e4e88"), "DMA", "North America", "Dominica", true },
                    { new Guid("d49819ae-0fd7-47d2-b4af-f0e1309bd61d"), "AUT", "Europe", "Austria", true },
                    { new Guid("d8ffcab7-0677-47d8-b6b0-5cccd3d2101e"), "ISR", "Asia", "Israel", true },
                    { new Guid("d9a6a371-62e9-42dd-b924-219c0386573a"), "LBN", "Asia", "Lebanon", true },
                    { new Guid("d9df5027-b862-4971-8b12-4170c1d6d8fd"), "SGP", "Asia", "Singapore", true },
                    { new Guid("da3e3367-1d4c-438c-97b6-4f1bd3b6277b"), "MLT", "Europe", "Malta", true },
                    { new Guid("dd732503-fc99-4eb3-a9a7-e28ede97b0cb"), "ECU", "South America", "Ecuador", true },
                    { new Guid("de70c9fd-3265-4db1-b913-f669b84895b4"), "BWA", "Africa", "Botswana", true },
                    { new Guid("dfe9e0c5-abd1-4b2e-98d8-92c4ad32663a"), "PSE", "Asia", "State of Palestine", true },
                    { new Guid("e0ff8e3e-5b36-4c5c-a5ab-638a7998716e"), "PAK", "Asia", "Pakistan", true },
                    { new Guid("e31c2668-9cea-4570-a0d8-a8285646992f"), "CAN", "North America", "Canada", true },
                    { new Guid("e44b45d8-0301-4ca1-9003-f7a87ca2a71a"), "VAT", "Europe", "Holy See", true },
                    { new Guid("e6779ca1-1c47-4164-863a-64aa2f998a15"), "ATG", "North America", "Antigua & Barbuda", true },
                    { new Guid("e7596855-3b80-4e9f-a101-c7b1195de61d"), "NRU", "Australia", "Nauru", true },
                    { new Guid("ea5c1893-8cd4-4092-b029-390829ff7806"), "TLS", "Asia", "Timor-Leste", true },
                    { new Guid("eba0b617-7669-4072-a132-ee508ecb8be9"), "TON", "Australia", "Tonga", true },
                    { new Guid("eec94dd5-343a-4692-a48b-42578ffb223a"), "IND", "Asia", "India", true },
                    { new Guid("f0d567ce-de48-4212-ae7e-962aa8cae363"), "PRT", "Europe", "Portugal", true },
                    { new Guid("f13c200c-6a22-4678-8ccb-dbf908ef3ea4"), "SMR", "Europe", "San Marino", true },
                    { new Guid("f28eb399-447b-4b33-840b-18419958ebea"), "SLB", "Australia", "Solomon Islands", true },
                    { new Guid("f28f3776-dc02-4e47-a6ca-39458c24194a"), "CZE", "Europe", "Czech Republic", true },
                    { new Guid("f453ed23-6f28-4fe9-bd79-769e8bae02cc"), "BGR", "Europe", "Bulgaria", true },
                    { new Guid("f458903e-f5fd-4657-be19-e4b66e1b9005"), "VNM", "Australia", "Vietnam", true },
                    { new Guid("f6c3c7d1-edb3-41ea-93df-d6007f2f945c"), "QAT", "Asia", "Qatar", true },
                    { new Guid("f78c634a-bc83-40fc-a83e-11948c3e92f8"), "NAM", "Africa", "Namibia", true },
                    { new Guid("f7f740a2-0c9c-4eb0-ac13-c38cee9b5e04"), "MRT", "Africa", "Mauritania", true },
                    { new Guid("f83a6c05-5000-445a-b03d-2af7f47b9f2e"), "LKA", "Asia", "Sri Lanka", true },
                    { new Guid("f8f388d5-a3b1-4e14-b690-3468a93348c5"), "GUY", "South America", "Guyana", true },
                    { new Guid("fad613a4-2a7f-4b46-8bb1-92df59e2a457"), "TUR", "Asia", "Turkey", true },
                    { new Guid("fadfb4d2-82c2-4abb-b19e-edbd745f078b"), "HUN", "Europe", "Hungary", true },
                    { new Guid("fb33ebcd-ebcb-4384-a02c-e12ea13365af"), "MNG", "Asia", "Mongolia", true },
                    { new Guid("fb68b48b-1852-46e4-86c4-30a6d873627f"), "LBR", "Africa", "Liberia", true },
                    { new Guid("fca88150-7daf-4608-bc2f-61658b0f1c39"), "BLZ", "North America", "Belize", true },
                    { new Guid("fdf9053b-0503-496a-b019-d79a5bccad5b"), "KIR", "Australia", "Kiribati", true },
                    { new Guid("ff0dcdc3-7f78-4d6b-b034-7a00fc74e79e"), "LUX", "Europe", "Luxembourg", true },
                    { new Guid("ff6ac4d4-4a7e-4c4c-8566-9368d54cf127"), "MAR", "Africa", "Morocco", true }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Blinded", "Code", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("0183827a-2139-49b4-887b-aa6ec686f8e8"), false, "CLCIS-UBLD", "CL - CIS", true },
                    { new Guid("0d6ba513-2413-4305-8aa3-65b9593012c5"), false, "STSTF-BLD", "Site - Staff", true },
                    { new Guid("1a9d0966-e3cc-4e2a-b275-a2cf22b6e9c2"), true, "CLCRA-BLD", "CL - CRA", true },
                    { new Guid("1b75bdcb-d59d-4cb9-8170-5f0cd88f1b8a"), false, "STDM-UBLD", "Site - Data Manager", true },
                    { new Guid("2405f609-ad11-4782-9260-cf926fbd11f8"), false, "User", "Normal User", true },
                    { new Guid("380791b9-9c1b-47d9-8e72-8e1ad3f1f2d9"), true, "SPMD-BLD", "Sponsor - Medical", true },
                    { new Guid("3aec65b5-7e55-4dfe-9b76-a1a3885801a9"), false, "CLDM-UBLD", "CL - Data Manager", true },
                    { new Guid("3af84911-d633-4388-bb2f-a862a73b223a"), false, "STSPAS-UBLD", "Site - Support Lead Associate", true },
                    { new Guid("477df6f3-61dd-4f5d-84a8-beff1db86460"), true, "STPI-BLD", "Site - Principal Investigator", true },
                    { new Guid("4b5aab1f-c671-4395-955c-9b9e5e207cea"), false, "CLQA-UBLD", "CL - Quality Assurance", true },
                    { new Guid("510c288c-3375-4226-8f3b-585379bac391"), false, "SPREG-UBLD", "Sponsor - Regulatory", true },
                    { new Guid("5ac7284c-890d-4b70-b687-e6de79629ca7"), false, "STCOR-UBLD", "Site - Coordinator", true },
                    { new Guid("5c224419-3feb-4259-b57b-833dd57fb231"), true, "SPQA-BLD", "Sponsor - Quality Assurance", true },
                    { new Guid("5d231a63-934e-435a-a41f-6666414d8825"), true, "STDM-BLD", "Site - Data Manager", true },
                    { new Guid("5d567e66-f0f4-452a-9143-39031eac031a"), true, "SPPM-BLD", "Sponsor - Project Manager", true },
                    { new Guid("5f8ac6de-32c8-47d2-8792-8b8ea5878dbf"), false, "SPCTA-UBLD", "Sponsor - CTA", true },
                    { new Guid("712e4acc-d36e-4e98-b255-7dfc59a6732d"), true, "CLREG-BLD", "CL - Regulatory", true },
                    { new Guid("722d75ae-b699-4bc4-8e98-98314bb3d44e"), true, "STSI-BLD", "Site - Sub Investigator", true },
                    { new Guid("7c74666b-adbb-44c5-b527-1db78da12d9f"), false, "STSTF-UBLD", "Site - Staff", true },
                    { new Guid("829baaf4-7f32-4b24-b240-e3f3463f3590"), false, "STSPAS-BLD", "Site - Support Lead Associate", true },
                    { new Guid("86fc852f-35a6-41ad-97d7-c09f252a2ade"), true, "CLCTA-BLD", "CL - CTA", true },
                    { new Guid("872724d5-bdcf-4394-9d1f-07136d8b081e"), false, "STSPLD-BLD", "Site - Support Lead", true },
                    { new Guid("8cb93861-9fcd-42fd-bc71-18a00030992b"), true, "CLDM-BLD", "CL - Data Manager", true },
                    { new Guid("9202cd18-9934-488d-8925-f8570134fddb"), true, "SPCRA-BLD", "Sponsor - CRA", true },
                    { new Guid("93273c5e-115f-4c53-8c05-b4ad07f67c5a"), false, "STCOR-BLD", "Site - Coordinator", true },
                    { new Guid("93a80fc2-7bed-475e-aa8a-d3c96db39a32"), true, "CLCIS-BLD", "CL - CIS", true },
                    { new Guid("97d40680-b2b9-4371-a1f9-46b13bc2da52"), false, "CLCRA-UBLD", "CL - CRA", true },
                    { new Guid("9910dcf9-f556-41f9-bbbb-75ad9435081c"), true, "SPREG-BLD", "Sponsor - Regulatory", true },
                    { new Guid("9b20b4cd-3f1d-43b2-9636-1ee7835cb7e4"), false, "SPQA-UBLD", "Sponsor - Quality Assurance", true },
                    { new Guid("a13e6740-9cae-4e0e-be9f-9f1db6e67882"), false, "SPPM-UBLD", "Sponsor - Project Manager", true },
                    { new Guid("a33762dd-6ce7-473c-aaa1-a5c2c146d62b"), false, "STSPLD-UBLD", "Site - Support Lead", true },
                    { new Guid("a7f85043-d99d-4d9c-9424-3ed21a0d7168"), false, "CLPM-UBLD", "CL - Project Manager", true },
                    { new Guid("acfb3cfd-98e7-41b7-9622-2d217ed21bd0"), false, "STPI-UBLD", "Site - Principal Investigator", true },
                    { new Guid("afd70000-ea6b-401f-9f9e-4dd5a9e6e835"), false, "SPDM-UBLD", "Sponsor - Data Manager", true },
                    { new Guid("b136720b-9583-4527-b2c9-1872301d123f"), true, "SPDM-BLD", "Sponsor - Data Manager", true },
                    { new Guid("b4fc56f0-4e2d-44c9-8008-8f5cc2573ec8"), false, "STPM-UBLD", "Site - Project Manager", true },
                    { new Guid("d52cb4f0-3956-414a-ac11-9d8d417f6873"), true, "SPCTA-BLD", "Sponsor - CTA", true },
                    { new Guid("d94a9549-54f5-4be2-9fb0-f1a10395fa53"), true, "CLQA-BLD", "CL - Quality Assurance", true },
                    { new Guid("dbef119f-756b-41a9-a455-044dd514dcb7"), false, "CLCTA-UBLD", "CL - CTA", true },
                    { new Guid("dc4bfb8f-e838-439e-899d-14208ac50981"), false, "SPMD-UBLD", "Sponsor - Medical", true },
                    { new Guid("e8306232-e7fd-4c0b-b397-ebc023d88006"), false, "SPCRA-UBLD", "Sponsor - CRA", true },
                    { new Guid("ec651c37-7161-4807-8538-d21cffe73b6f"), false, "Admin", "Administrator", true },
                    { new Guid("ef8eccb1-84bf-4c23-9b38-532292d16043"), true, "STPM-BLD", "Site - Project Manager", true },
                    { new Guid("f367474a-00ab-474f-a892-10af47caa3f2"), false, "STSI-UBLD", "Site - Sub Investigator", true },
                    { new Guid("f50d75b2-f196-4344-b451-a9c25ae71105"), false, "CLREG-UBLD", "CL - Regulatory", true },
                    { new Guid("f708d1f8-cf03-4053-b064-bd00d45554bc"), true, "CLPM-BLD", "CL - Project Manager", true }
                });

            migrationBuilder.InsertData(
                table: "VendorType",
                columns: new[] { "Id", "Code", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("0569b581-2070-469e-a732-be241e478063"), "", "Statistics", true },
                    { new Guid("0bdeaa11-707a-49d8-a878-d4a29f2efbe0"), "", "Medical Liaison/Translation", true },
                    { new Guid("120674ed-f1a4-4fa1-9467-0d408569adca"), "", "Laboratory", true },
                    { new Guid("17bc6ea8-7a9b-482b-993b-8d913e548cb7"), "", "Project Management", true },
                    { new Guid("1b5e6947-8c88-41de-846c-f1b8114d3310"), "", "Real world Evidence ", true },
                    { new Guid("360d1789-cb28-4c71-b0ff-c9bc882283fb"), "", "Site Feasibility, Selection & Management", true },
                    { new Guid("49183e5b-6104-422d-a20d-1980499aad57"), "", "Patient Monitoring", true },
                    { new Guid("493156c8-b3d6-4313-b9a8-47e8ae44c328"), "", "Meeting Planner", true },
                    { new Guid("4aee7231-6355-4f9c-833f-7849909ad6a1"), "", "Full-service CRO", true },
                    { new Guid("5cece5c8-02d3-41fb-a857-268525778cf9"), "", "DCT services", true },
                    { new Guid("630da100-d716-4afc-8898-5183b60532e8"), "", "Regulatory", true },
                    { new Guid("6ac9587d-fd98-4b36-a95f-d074079371b8"), "", "Commercialization Data", true },
                    { new Guid("793b5063-6bcf-443f-8d24-eee50e52a5d5"), "", "Monitoring (Clinical, Medical)", true },
                    { new Guid("7d1e3425-4b49-4b0a-85d0-f25fd6767a27"), "", "Pharmacovigiliance (PVG) & Safety", true },
                    { new Guid("94477f9e-2d7b-481a-bc93-2735eac9df6c"), "", "Quality Management ", true },
                    { new Guid("9f5585d4-e81c-471c-a541-769b85bc2658"), "", "Data Management & Statistics", true },
                    { new Guid("a0f6d3f0-0f75-49a8-880b-9448ca9a9324"), "", "Data Management", true },
                    { new Guid("a244993a-2019-4817-b925-ec10e999fda5"), "", "Patient Recruitment & Engagement", true },
                    { new Guid("d46f5395-a904-43c2-b090-f4fe93174eef"), "", "Marketing/Branding", true },
                    { new Guid("d6428db7-d0c0-4253-aa72-535bcffed481"), "", "Pharmacokinetics (PK) & Pharmacodynamics (PD)", true },
                    { new Guid("d6f5fa5c-aca0-4a28-8239-691984e713e6"), "", "Contractor", true },
                    { new Guid("dee3183d-7d92-4c48-90c0-3e95e6e33b73"), "", "Logistics (Cold Chain, Drug Supply)", true },
                    { new Guid("e7ae8400-e63e-4801-a0b6-da6aa5617195"), "", "Wearables & (Remote) data collection", true },
                    { new Guid("e7be11ec-afdb-48c5-9f72-e0a4dd793c9c"), "", "Other", true },
                    { new Guid("f02820af-12dc-460f-9219-c7e712802a9c"), "", "Information Technology", true },
                    { new Guid("f5fc5212-7deb-4a8c-973a-e36e374ca1f5"), "", "Legal", true },
                    { new Guid("faad8494-4bf7-4cb0-9396-69a9c3a01ed9"), "", "Medical Writing", true }
                });

            migrationBuilder.InsertData(
                table: "RolesPages",
                columns: new[] { "Id", "Pages", "RolesId", "Status" },
                values: new object[,]
                {
                    { new Guid("0446d8b5-7107-4cad-b915-9a947df9bdac"), 5, new Guid("ec651c37-7161-4807-8538-d21cffe73b6f"), true },
                    { new Guid("0e5329cd-241b-4830-83a2-1c81528efd6c"), 4, new Guid("ec651c37-7161-4807-8538-d21cffe73b6f"), true },
                    { new Guid("a51d9934-28e9-412f-9355-6cf353f94318"), 3, new Guid("ec651c37-7161-4807-8538-d21cffe73b6f"), true },
                    { new Guid("acec7615-8838-49af-ac3f-69c6e0627ea0"), 1, new Guid("ec651c37-7161-4807-8538-d21cffe73b6f"), true },
                    { new Guid("b3d3e497-a6ba-48c4-a450-681bd834a634"), 2, new Guid("ec651c37-7161-4807-8538-d21cffe73b6f"), true },
                    { new Guid("b65e33be-5ddd-4482-83d3-1d27a5a6a52f"), 6, new Guid("ec651c37-7161-4807-8538-d21cffe73b6f"), true }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "EndDate", "Firstname", "Landline", "Lastname", "Middlename", "Mobile", "ObjectId", "Organization", "Prefix", "RolesId", "StartDate", "Status", "Suffix" },
                values: new object[] { new Guid("5d0fce3f-5d16-40fb-b80d-0f5175d0a864"), "hrisadmin@gertnewx.onmicrosoft.com", new DateTime(2023, 9, 15, 8, 9, 38, 616, DateTimeKind.Utc).AddTicks(3817), "Administrator", null, "Administrator", "", "", new Guid("b3fd282f-169a-4722-b805-ed3a1c873e74"), "Ctrove", "", new Guid("ec651c37-7161-4807-8538-d21cffe73b6f"), new DateTime(2023, 9, 15, 8, 9, 38, 616, DateTimeKind.Utc).AddTicks(3753), true, "" });

            migrationBuilder.CreateIndex(
                name: "IX_Access_UserId",
                table: "Access",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributor_CountryId",
                table: "Contributor",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributor_OrganizationId",
                table: "Contributor",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributorStudy_ContributorId",
                table: "ContributorStudy",
                column: "ContributorId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_ContactTypeId",
                table: "Organization",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_VendorTypeId",
                table: "Organization",
                column: "VendorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RolesPages_RolesId",
                table: "RolesPages",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_UserId",
                table: "Settings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SitePhases_PhaseId",
                table: "SitePhases",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SitePhases_SitesId",
                table: "SitePhases",
                column: "SitesId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_ServiceTypeId",
                table: "Sites",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_StudyCountryId",
                table: "Sites",
                column: "StudyCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Study_ClassificationId",
                table: "Study",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Study_TherapeuticAreaId",
                table: "Study",
                column: "TherapeuticAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_EthnicityId",
                table: "Subject",
                column: "EthnicityId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_RaceId",
                table: "Subject",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SitesId",
                table: "Subject",
                column: "SitesId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPhases_PhaseId",
                table: "SubjectPhases",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPhases_SubjectId",
                table: "SubjectPhases",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RolesId",
                table: "User",
                column: "RolesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Access");

            migrationBuilder.DropTable(
                name: "Audit");

            migrationBuilder.DropTable(
                name: "ContributorStudy");

            migrationBuilder.DropTable(
                name: "RolesPages");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "SitePhases");

            migrationBuilder.DropTable(
                name: "Study");

            migrationBuilder.DropTable(
                name: "SubjectPhases");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Contributor");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropTable(
                name: "TherapeuticArea");

            migrationBuilder.DropTable(
                name: "Phase");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Ethnicity");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "ContactType");

            migrationBuilder.DropTable(
                name: "VendorType");

            migrationBuilder.DropTable(
                name: "ServiceType");

            migrationBuilder.DropTable(
                name: "StudyCountry");
        }
    }
}
