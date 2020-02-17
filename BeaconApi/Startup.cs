using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BeaconApi.Models;
using Microsoft.AspNetCore.Http;
using BeaconApi.Data;
using BeaconApi.Extensions;

namespace BeaconApi
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
            //services.AddDbContext<BeaconDbContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("BeaconDbContext")));

            services.AddCors();

            services.AddTransient(typeof(IBeaconRepository), typeof(BeaconRepository));

            services.AddControllers();            
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run(async (context) =>
            {                
                await context.Response.WriteAsync("Resource Server has been started successfully");
            });
        }
    }
}
