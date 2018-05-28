using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Api.DBModel
{
    public class TestDataSeeder
    {
        readonly ILogger _logger;

        public TestDataSeeder(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("TestDataSeeder");
        }

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

        /* public static async Task InitializeDeficiencyDatabaseAsync(IServiceProvider serviceProvider, bool seedData = false)
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
        } */

        public async Task InsertTestData(HRContext db)
        {
            var employees = GetEmployees();
            db.Employees.AddRange(employees);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception exp)
            {                
                _logger.LogError($"Error in {nameof(TestDataSeeder)}: " + exp.Message);
                throw; 
            }

            var projects = GetProjects();
            db.Projects.AddRange(projects);

            try
            {
              await db.SaveChangesAsync();
            }
            catch (Exception exp)
            {
              _logger.LogError($"Error in {nameof(TestDataSeeder)}: " + exp.Message);
              throw;
            }

            var roleAssignments = GetRoleAssignments();
            db.RoleAssignments.AddRange(roleAssignments);

            try
            {
              await db.SaveChangesAsync();
            }
            catch (Exception exp)
            {
              _logger.LogError($"Error in {nameof(TestDataSeeder)}: " + exp.Message);
              throw;
            }
        }

        private static List<Employee> GetEmployees()
        {
            var employees = new List<Employee>
            {
                new Employee{EmployeeId = 1, FirstName = "wukong", LastName = "sun", BirthDay = Convert.ToDateTime("1988-06-05 00:00:00")},
                new Employee{EmployeeId = 3, FirstName = "bajie", LastName = "zhu", BirthDay = Convert.ToDateTime("1988-06-05 00:00:00")},
                new Employee{EmployeeId = 4, FirstName = "wujing", LastName = "sha", BirthDay = Convert.ToDateTime("1988-06-05 00:00:00")},
                new Employee{EmployeeId = 5, FirstName = "seng", LastName = "tang", BirthDay = Convert.ToDateTime("1988-06-05 00:00:00")},
                new Employee{EmployeeId = 6, FirstName = "yu", LastName = "guan", BirthDay = Convert.ToDateTime("1988-06-05 00:00:00")},
                new Employee{EmployeeId = 7, FirstName = "yun", LastName = "zhao", BirthDay = Convert.ToDateTime("1988-06-05 00:00:00")},
            };
            return employees;
        }

        private static List<Project> GetProjects()
        {
            var projects = new List<Project>
            {
                new Project{ProjectId=1,Description="",Title="project 1"},
                new Project{ProjectId=2,Description="",Title="project 2"},
                
            };
            return projects;
        }

        private static List<RoleAssignment> GetRoleAssignments()
        {
            var roleAssignments = new List<RoleAssignment>
            {
                new RoleAssignment{RoleAssignmentId =1, EmployeeId = 1,ProjectId =1, Capacity=3,Position="Architector",Start=Convert.ToDateTime("2016-01-01 00:00:00"),End=Convert.ToDateTime("2018-01-01 00:00:00")},
                new RoleAssignment{RoleAssignmentId =2, EmployeeId = 3,ProjectId =1, Capacity=3,Position="Software developer",Start=Convert.ToDateTime("2016-01-01 00:00:00"),End=Convert.ToDateTime("2018-01-01 00:00:00")},
                new RoleAssignment{RoleAssignmentId =3, EmployeeId = 4,ProjectId =1, Capacity=5,Position="Software developer",Start=Convert.ToDateTime("2016-01-01 00:00:00"),End=Convert.ToDateTime("2018-01-01 00:00:00")},
                new RoleAssignment{RoleAssignmentId =4, EmployeeId = 5,ProjectId =2, Capacity=6,Position="Project manager",Start=Convert.ToDateTime("2016-01-01 00:00:00"),End=Convert.ToDateTime("2018-01-01 00:00:00")},
                new RoleAssignment{RoleAssignmentId =5, EmployeeId = 6,ProjectId =2, Capacity=8,Position="Project manager",Start=Convert.ToDateTime("2016-01-01 00:00:00"),End=Convert.ToDateTime("2018-01-01 00:00:00")},
                new RoleAssignment{RoleAssignmentId =6, EmployeeId = 7,ProjectId =2, Capacity=8,Position="Team leader",Start=Convert.ToDateTime("2016-01-01 00:00:00"),End=Convert.ToDateTime("2018-01-01 00:00:00")},
            };
            return roleAssignments;
        }
    }
}