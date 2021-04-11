//This file is the Startup.cs if our RouterLogsApi project
//DEV NOTE: Despite multiple attempts at workarounds and different solutions listed here: "https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0", I could not get Cross-Origin Requests (CORS) to work, thus had to use a work around in my webapp.
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RouterLogsApi
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
            // This code SHOULD allow Cross-Origin Requests (Cors) to work with out endpoints but for whatever reason it will not be enabled  
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200",
                                       "http://localhost:4100",
                                       "http://localhost:4300")
                                       .AllowCredentials()
                                       .AllowAnyHeader()
                                       .AllowAnyMethod();
                                                 
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //Should tell the API to use Cors, it is not working at the moment 
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
