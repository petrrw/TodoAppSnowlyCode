using Microsoft.EntityFrameworkCore;
using TodoAppSnowlyCode.Data.DbSetup;

namespace TodoAppSnowlyCode.Extensions
{
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Extension method to apply pending migrations to the database.
        /// </summary>
        /// <param name="app">Instance of <see cref="WebApplication"/></param>
        /// <returns><see cref="WebApplication"/></returns>
        public static WebApplication UpdateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (dbContext.Database.GetPendingMigrations().Any())
                    dbContext.Database.Migrate();
            }

            return app;
        }
    }
}
