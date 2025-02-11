using CTrove.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CTrove.Infrastructure.Extensions;
using CTrove.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using CTrove.Api.Settings;
using Ctrove.HR;
using Ctrove.HR.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.GetSection("UserClaims").Bind(ApplicationSettings.UserClaims);
builder.Configuration.GetSection("Security").Bind(ApplicationSettings.SecurityConfig);
builder.Configuration.GetSection(AzureAdConfig.Name).Bind(ApplicationSettings.AzureAdConfig);

builder.Services.Configure<AzureAdConfig>(builder.Configuration.GetSection(AzureAdConfig.Name));

builder.Services.AddDbContext<CtroveDatabaseContext>(
    options => options.UseSqlServer(builder.Configuration["Data:ConnectionString:DefaultConnection"]));

builder.Services.Configure<HrContributorConfig>(builder.Configuration.GetSection("HRContributorAPIConfig"));
builder.Services.AddOptions();

//CORS Configuration

builder.Services.AddCors(options =>
{
    options.AddPolicy(ApplicationSettings.SecurityConfig.Name!, policy =>
    {
        policy.AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(ApplicationSettings.SecurityConfig.AllowedHosts!);
    });
});

//MSIdentity AD
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(builder.Configuration);


//Swagger AD Provider
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Ctrove API Endpoints",
        Version = "v1"
    });

    c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {

        Description = "Oauth2.0",
        Name = "oauth2.0",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,
        Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows
        {
            AuthorizationCode = new Microsoft.OpenApi.Models.OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(builder.Configuration["SwaggerAzureAD:AuthorizationUrl"]!),
                TokenUrl = new Uri(builder.Configuration["SwaggerAzureAD:TokenUrl"]!),
                Scopes = new Dictionary<string, string>
                        {
                            {builder.Configuration["SwaggerAzureAD:Scope"]!,"Access API" },
                            { "openid","Token" },
                            { "profile","profile"},
                            { "offline_access","Offline Access"}
                        }
            }
        }

    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference= new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="oauth2" }
                    },
                    new[]{builder.Configuration["SwaggerAzureAD:Scope"]}
                }
            });
});


//Ctrove.Infrastructure Extensions
builder.Services.ServiceInfrastructureExtensions();

//Api Services Extensions
builder.Services.ServicesApiExtensions();



var app = builder.Build();

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId(builder.Configuration["SwaggerAzureAD:ClientId"]);
        c.OAuthUsePkce();
        c.OAuthScopeSeparator(" ");
    });

}

app.UseCors(ApplicationSettings.SecurityConfig.Name!);



//Global Custom Error Handling
app.CustomErrorHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

if(app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.MigrateDataSuperUser();
    app.MigrateTestUser();
    app.MigrateRolesPagesSuperUser();
}

app.Run();
