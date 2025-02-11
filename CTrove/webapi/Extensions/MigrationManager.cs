using CTrove.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CTrove.Api.Extensions
{
    public static class MigrationManager
    {
        public static WebApplication MigrateDatabase(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<CtroveDatabaseContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return webApplication;
        }
    }
}
