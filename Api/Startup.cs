using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Api.DBModel;

using Microsoft.EntityFrameworkCore;
using Api.Repository;
using Api.Utils;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            
            services.AddDbContext<HRContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PgConn")));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info());
                c.DescribeStringEnumsInCamelCase();
                c.DescribeAllEnumsAsStrings();

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, Assembly.GetExecutingAssembly().GetName().Name + ".xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            var startupLogger = loggerFactory.CreateLogger<Startup>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(o => o.AllowAnyOrigin().WithMethods("POST", "DELETE", "GET", "PUT").AllowAnyHeader().AllowCredentials());
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", string.Empty);
            });
            app.UseMvc();

            //CircuitBreaker.Do(() => TestDataSeeder.InitializeDeficiencyDatabaseAsync(app.ApplicationServices, true).Wait(), TimeSpan.FromSeconds(3));

            var applicantSeeder = new TestDataSeeder(loggerFactory);
            applicantSeeder.SeedAsync(app.ApplicationServices).Wait();
            startupLogger.LogInformation("Data seed completed.");
        }
    }
}
