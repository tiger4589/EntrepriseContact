using ContactManagementApi.MapperConfiguration;
using ContactManagementService.Context;
using ContactManagementService.Services;
using ContactManagementService.Services.Interfaces;
using ContactManagementService.StorageAccess;
using ContactManagementService.StorageAccess.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagementApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContactManagementContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IContactManager, ContactManager>();
            services.AddScoped<IEntrepriseAddressManager, EntrepriseAddressManager>();
            services.AddScoped<IEntrepriseContactManager, EntrepriseContactManager>();
            services.AddScoped<IEntrepriseManager, EntrepriseManager>();
            services.AddScoped<IContactStorageManager, ContactStorageManager>();
            services.AddScoped<IEntrepriseStorageManager, EntrepriseStorageManager>();
            services.AddScoped<IEntrepriseAddressStorageManager, EntrepriseAddressStorageManager>();
            services.AddScoped<IEntrepriseContactStorageManager, EntrepriseContactStorageManager>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactManagementApi", Version = "v1" });
            });
            services.AddAutoMapper(typeof(MapperProfile));
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactManagementApi v1"));
            }

            //create database on first run!
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ContactManagementContext>();
                context.Database.Migrate();
            }
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
