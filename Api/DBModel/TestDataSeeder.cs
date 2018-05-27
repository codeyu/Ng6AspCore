using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Api.DBModel
{
    public class TestDataSeeder
    {
        public async Task SeedAsync(IServiceProvider serviceProvider)
        {
            //Based on EF team's example at https://github.com/aspnet/MusicStore/blob/dev/samples/MusicStore/Models/SampleData.cs
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var hrContext = serviceScope.ServiceProvider.GetService<HRContext>();
                if (await hrContext.Database.EnsureCreatedAsync())
                {
                    if (!await hrContext.Employees.AnyAsync()) {
                      await InsertTestData(hrContext);
                    }
                }
            }
        }

        public static async Task InitializeDeficiencyDatabaseAsync(IServiceProvider serviceProvider, bool seedData = false)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<HRContext>();

                var appliedMigrations = await db.Database.GetAppliedMigrationsAsync();

                if (!appliedMigrations.Any())
                {
                    Console.WriteLine("Creating Databse; Applying all migrations; Adding seed data");
                    await db.Database.MigrateAsync();
                    if (seedData) 
                    {
                        await InsertTestData(db);
                    }
                }
                else
                {
                    var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
                    if (pendingMigrations.Any())
                    {
                        Console.WriteLine("Applying pending migrations");
                        await db.Database.MigrateAsync();
                        if (seedData) 
                        {
                            await InsertTestData(db);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No pending migrations");
                    }
                }
            }
        }

        public static async Task InsertTestData(HRContext db)
        {
            throw new NotImplementedException();
        }
    }
}