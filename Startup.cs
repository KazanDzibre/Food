using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food.Configuration;
using Food.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace Food
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

            services.AddControllers().AddNewtonsoftJson(s => {s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();});

			services.AddDbContext<Context>(x => {
					x.UseSqlServer(Configuration["ProjectConfiguration:DatabaseConfiguration:ConnectionString"]);
					x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
					});

			services.AddCors();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			var config = new ProjectConfiguration();
			Configuration.Bind("ProjectConfiguration", config);
			services.AddSingleton(config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Context dataContext)
        {

            app.UseRouting();

			dataContext.Database.Migrate();
			app.UseCors(x => x
					.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
