using AutoMapper;
using EmployeeDetails.BusinessLayer.BusinessClasses;
using EmployeeDetails.BusinessLayer.Interfaces;
using EmployeeDetails.Common;
using EmployeeDetails.DataAccessLayer.Interfaces;
using EmployeeDetails.DataAccessLayer.Repositories;
using EmployeeDetails.EF;
using EmployeeDetails.ExceptionHandling;
using EmployeeDetails.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetails
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
            services.AddControllers().AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen();
            services.AddTransient<IEmployeeBAL, EmployeeBAL>();
            services.AddTransient<IDepartmentBAL, DepartmentBAL>();
            services.AddTransient<IManagerBAL, ManagerBAL>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<Serilog.ILogger>(Log.Logger);
            //Datababse service Configuration
            services.AddDbContext<EmployeeDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("EmployeeConnectionString"),
                          sqlServerOptionsAction: sqlOptions =>
                          {
                              sqlOptions.EnableRetryOnFailure();
                          }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            });
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseGlobalExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
