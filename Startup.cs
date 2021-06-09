using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.Configuration;
using Food.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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

            services.AddControllers();

			services.Configure<IISServerOptions>(options =>
					{
						options.AutomaticAuthentication = false;
					});

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
					{
						options.RequireHttpsMetadata = false;
						options.SaveToken = false;
						options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidAudience = Configuration["ProjectConfiguration:Jwt:Audience"],
							ValidIssuer = Configuration["ProjectConfiguration:Jwt:Issuer"],
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ProjectConfiguration:Jwt:Key"]))
						};
					});
			services.AddDbContext<Context>(x => {
					x.UseSqlServer(Configuration["ProjectConfiguration:DatabaseConfiguration:ConnectionString"]);
					x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
					});

			services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
						{
							 builder.AllowAnyOrigin()
									.AllowAnyMethod()
									.AllowAnyHeader();
						}));
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			var config = new ProjectConfiguration();
			Configuration.Bind("ProjectConfiguration", config);
			services.AddSingleton(config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Context dataContext)
        {

            app.UseRouting();

			app.UseHttpsRedirection();

			app.UseForwardedHeaders(new ForwardedHeadersOptions
					{
						ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
					});

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseCors("MyPolicy");

			dataContext.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
