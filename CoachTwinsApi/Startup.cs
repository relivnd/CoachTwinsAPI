using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CoachTwinsAPI.Auth.Extensions;
using CoachTwinsApi.Db;
using CoachTwinsApi.Db.Extensions;
using CoachTwinsApi.Db.Seeders;
using CoachTwinsAPI.Extensions.Startup;
using CoachTwinsApi.Graph.Extensions;
using CoachTwinsApi.Logic.Extensions;
using CoachTwinsAPI.SignalR;
using CoachTwinsApi.Templating.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using CoachTwinsApi.Db.Entities;

namespace CoachTwinsAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "local_dev", builder =>
                {
                    builder.WithOrigins("http://localhost:4200");
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration)
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddInMemoryTokenCaches()
                .AddMicrosoftGraph(defaultScopes: "https://graph.microsoft.com/calendars.readwrite");
            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoachTwinsAPI", Version = "v1" });
            });
            services.ConfigureEf(Configuration);
            services.RegisterRepositories();
            services.ConfigureAutomapper();
            services.AddTemplating();
            services.RegisterServices();
            services.AddSignalR();
            services.AddAuthServices();
            services.AddGraphServices();
            services.AddScoped<DbSeeder>();
            services.AddApplicationInsightsTelemetry("148b3efb-17fe-483d-a60b-b417f47e291b");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoachTwinsAPI v1"));


            if (!env.IsDevelopment())
                app.UseHttpsRedirection();
            else
                app.UseCors("local_dev");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllers();
            });

            var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using var scope = scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<CoachTwinsDbContext>();
            //context?.Database.Migrate();
          //  var seeder = scope?.ServiceProvider.GetService<DbSeeder>();
           // seeder?.Initialize();

        }
    }
}
